using System;
using HurricaneVR.Framework.Core.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Basic button check for travel along a defined axis, requires the user to have setup their own constraint system.
    /// Superceded by the new HVRPhysicsButton component which helps create joints and limits for you
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class HVRButton : MonoBehaviour
    {
        public VRButtonEvent ButtonDown = new VRButtonEvent();
        public VRButtonEvent ButtonUp = new VRButtonEvent();


        public Vector3 Axis;

        [Tooltip("How far the button must travel to become pressed.")]
        public float Threshold;
        [Tooltip("Threshold to hit on the return to allow the button to be pressed again.")]
        public float UpThreshold;
        [Tooltip("The resting position of the button")]
        public Vector3 StartPosition;
        public bool IsPressed = false;

        public AudioClip AudioButtonDown;
        public AudioClip AudioButtonUp;

        public Rigidbody Rigidbody { get; private set; }

        protected virtual void Awake()
        {
            //for older projects that wouldn't have set this in the editor
            if(StartPosition == Vector3.zero)
            {
                StartPosition = transform.localPosition;
            }    
          
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var distance = (StartPosition - transform.localPosition).magnitude;

            if (!IsPressed && distance >= Threshold)
            {
                IsPressed = true;
                OnButtonDown();
            }
            else if (IsPressed && distance < UpThreshold)
            {
                IsPressed = false;
                OnButtonUp();
            }

            ClampBounds();
        }

        private void ClampBounds()
        {
            var test = new Vector3(transform.localPosition.x * Axis.x, transform.localPosition.y * Axis.y, transform.localPosition.z * Axis.z);

            if (test.x > StartPosition.x || test.y > StartPosition.y || test.z > StartPosition.z)
            {
                transform.localPosition = StartPosition;
                Rigidbody.velocity = Vector3.zero;
            }
        }

        private void LateUpdate()
        {
            ClampBounds();
        }

        protected virtual void OnButtonDown()
        {
            if (AudioButtonDown)
                if(SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(AudioButtonDown, transform.position);

            ButtonDown.Invoke(this);
        }

        protected virtual void OnButtonUp()
        {
            if (AudioButtonUp)
                if(SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(AudioButtonUp, transform.position);
            ButtonUp.Invoke(this);
        }
    }

    [Serializable]
    public class VRButtonEvent : UnityEvent<HVRButton> { }
}
