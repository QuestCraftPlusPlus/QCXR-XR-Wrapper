using System;
using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Helper component to constrain a drawer along the desired movement axis handling the joint creation and limiting for you.
    /// Joint is constrained between the start and end position which is defined in the component inspector.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class HVRPhysicsDrawer : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("Axis the drawer will travel on in local space.")]
        public HVRAxis Axis;

        [Tooltip("Rigidbody to joint to.")]
        public Rigidbody ConnectedBody;

        [Tooltip("Optional spring that will return to the starting position")]
        public float Spring = 0;

        [Tooltip("Damper to provide 'friction' to the drawer.")]
        public float Damper = 10;

        [Header("SFX")]
        public float SFXResetThreshold = .02f;
        public AudioClip SFXOpened;
        public AudioClip SFXClosed;

        [Header("Editor Fields")]
        [Tooltip("The resting position of the button")]
        public Vector3 StartPosition;

        [Tooltip("Furthest position the button can travel")]
        public Vector3 EndPosition;

        public Vector3 OpenPosition;


        [Header("Debug")]
        public bool UpdateSpring;
        public bool PreviousOpened;
        public bool Opened;
        public bool PreviousClosed;
        public bool Closed;

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

            //set initial values to prevent sfx on start
            GetValues(out var distance, out var openedDistance, out var resetThreshold);

            if (distance > openedDistance)
            {
                Opened = true;
            }
            else if (distance < openedDistance)
            {
                Closed = true;
            }
        }

        private void SetupJoint()
        {
            _joint = gameObject.AddComponent<ConfigurableJoint>();
            _joint.connectedBody = ConnectedBody;
            _joint.autoConfigureConnectedAnchor = false;
            _joint.anchor = Vector3.zero;

            var worldStartPosition = StartPosition;
            if (transform.parent)
                worldStartPosition = transform.parent.TransformPoint(StartPosition);

            var worldEndPosition = EndPosition;
            if (transform.parent)
                worldEndPosition = transform.parent.TransformPoint(EndPosition);


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
            _limitJoint.connectedBody = ConnectedBody;
            _limitJoint.autoConfigureConnectedAnchor = false;
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



        private void Update()
        {
            GetValues(out var distance, out var openedDistance, out var resetThreshold);

            var closeReset = openedDistance + resetThreshold;
            var openReset = openedDistance - resetThreshold;

            if (!Opened && distance > openedDistance)
            {
                Opened = true;
                if(SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(SFXOpened, transform.position);
            }
            else if (!Closed && distance < openedDistance)
            {
                Closed = true;
                if(SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(SFXClosed, transform.position);
            }
            else if (Opened && distance < openReset)
            {
                Opened = false;
            }
            else if (Closed && distance > closeReset)
            {
                Closed = false;
            }

            PreviousClosed = Closed;
            PreviousOpened = Opened;
        }

        private void GetValues(out float distance, out float openDistance, out float resetDelta)
        {
            distance = 0f;
            openDistance = 0f;
            resetDelta = SFXResetThreshold;
            switch (Axis)
            {
                case HVRAxis.X:
                case HVRAxis.NegX:
                    distance = transform.localPosition.x - StartPosition.x;
                    openDistance = OpenPosition.x - StartPosition.x;
                    break;
                case HVRAxis.Y:
                case HVRAxis.NegY:
                    distance = transform.localPosition.y - StartPosition.y;
                    openDistance = OpenPosition.y - StartPosition.y;
                    break;
                case HVRAxis.Z:
                case HVRAxis.NegZ:
                    distance = transform.localPosition.z - StartPosition.z;
                    openDistance = OpenPosition.z - StartPosition.z;
                    break;
            }

            distance = Mathf.Abs(distance);
            openDistance = Mathf.Abs(openDistance);

            if (resetDelta > openDistance)
            {
                resetDelta = openDistance * .5f;
            }
        }

        private void OnDrawGizmosSelected()
        {
            var forward = (EndPosition - StartPosition).normalized;
            var closeReset = transform.parent.TransformPoint(OpenPosition + (forward * SFXResetThreshold));
            var openReset = transform.parent.TransformPoint(OpenPosition - (forward * SFXResetThreshold));

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(closeReset, .005f);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(openReset, .005f);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.parent.TransformPoint(OpenPosition), .005f);
        }
    }

}