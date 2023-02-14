using System;
using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Creates a joint constrained along one axis with limits based on start and end points defined in the inspector editor.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class HVRPhysicsButton : MonoBehaviour
    {
        [Header("Settings")]

        [Tooltip("Axis the button will travel on in local space.")]
        public HVRAxis Axis;

        [Tooltip("Rigidbody this button will connect to with a joint.")]
        public Rigidbody ConnectedBody;

        [Tooltip("Spring value of the joint")]
        public float Spring = 1000f;

        [Tooltip("Damper of the joint")]
        public float Damper = 50f;



        [Header("Button Positions")]
        [Tooltip("How far the button must travel to become pressed.")]
        public float DownThreshold;

        [Tooltip("Threshold to hit on the return to allow the button to be pressed again.")]
        public float ResetThreshold;

        [Tooltip("The resting position of the button")]
        public Vector3 StartPosition;

        [Tooltip("Furthest position the button can travel")]
        public Vector3 EndPosition;



        [Header("SFX")]
        public AudioClip SFXButtonDown;
        public AudioClip SFXButtonUp;

        public HVRButtonEvent ButtonDown = new HVRButtonEvent();
        public HVRButtonEvent ButtonUp = new HVRButtonEvent();

        [Header("Debug")]
        public bool IsPressed = false;
        public bool InvokeButtonDown;
        public bool UpdateSpring;

        public Rigidbody Rigidbody { get; private set; }

        private Vector3 _axis;
        private ConfigurableJoint _joint;
        private ConfigurableJoint _limitJoint;


        protected virtual void Awake()
        {
            transform.localPosition = StartPosition;
            Rigidbody = GetComponent<Rigidbody>();
            _axis = Axis.GetVector();
            Rigidbody.useGravity = false;
            SetupJoint();
        }

        private void SetupJoint()
        {
            var worldStartPosition = StartPosition;
            if (transform.parent)
                worldStartPosition = transform.parent.TransformPoint(StartPosition);

            var worldEndPosition = EndPosition;
            if (transform.parent)
                worldEndPosition = transform.parent.TransformPoint(EndPosition);

            _joint = gameObject.AddComponent<ConfigurableJoint>();
            _joint.autoConfigureConnectedAnchor = false;
            _joint.connectedBody = ConnectedBody;
            _joint.anchor = Vector3.zero;
            


            if (ConnectedBody)
            {
                _joint.connectedAnchor = ConnectedBody.transform.InverseTransformPoint(worldStartPosition);
            }
            else
            {
                _joint.connectedAnchor = worldStartPosition;
            }

            _joint.SetXDrive(Spring, Damper, Spring);

            _joint.LimitXMotion();
            _joint.LockYMotion();
            _joint.LockZMotion();
            _joint.LockAllAngularMotion();
            _joint.axis = _axis;
            _joint.secondaryAxis = _joint.axis.OrthogonalVector();
            _joint.SetLinearLimit(Vector3.Distance(StartPosition, EndPosition));

            _limitJoint = gameObject.AddComponent<ConfigurableJoint>();
            _limitJoint.autoConfigureConnectedAnchor = false;
            _limitJoint.connectedBody = ConnectedBody;
            _limitJoint.anchor = Vector3.zero;
            

            if (ConnectedBody)
            {
                _limitJoint.connectedAnchor = ConnectedBody.transform.InverseTransformPoint(worldEndPosition);
            }
            else
            {
                _limitJoint.connectedAnchor = worldEndPosition;
            }

            _limitJoint.LockYMotion();
            _limitJoint.LockZMotion();
            _limitJoint.LockAllAngularMotion();
            _limitJoint.axis = _axis;
            _limitJoint.secondaryAxis = _joint.axis.OrthogonalVector();
            _limitJoint.LimitXMotion();
            _limitJoint.SetLinearLimit(Vector3.Distance(StartPosition, EndPosition));
        }
        private void FixedUpdate()
        {
            if (UpdateSpring)
            {
                _joint.SetXDrive(Spring, Damper, Spring);
                UpdateSpring = false;
            }

            var distance = Mathf.Abs(GetDistance());

            if (!IsPressed && distance >= DownThreshold || InvokeButtonDown)
            {
                InvokeButtonDown = false;
                IsPressed = true;
                OnButtonDown();
            }
            else if (IsPressed && distance < ResetThreshold)
            {
                IsPressed = false;
                OnButtonUp();
            }
        }

        private float GetDistance()
        {
            switch (Axis)
            {
                case HVRAxis.X:
                case HVRAxis.NegX:
                    return transform.localPosition.x - StartPosition.x;
                case HVRAxis.Y:
                case HVRAxis.NegY:
                    return transform.localPosition.y - StartPosition.y;
                case HVRAxis.Z:
                case HVRAxis.NegZ:
                    return transform.localPosition.z - StartPosition.z;
            }

            return 0f;
        }

        protected virtual void OnButtonDown()
        {
            if (SFXButtonDown)
            {
                if(SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(SFXButtonDown, transform.position);
            }

            ButtonDown.Invoke(this);
        }

        protected virtual void OnButtonUp()
        {
            if (SFXButtonUp)
            {
                if(SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(SFXButtonUp, transform.position);
            }
            ButtonUp.Invoke(this);
        }
    }

    [Serializable]
    public class HVRButtonEvent : UnityEvent<HVRPhysicsButton> { }
}