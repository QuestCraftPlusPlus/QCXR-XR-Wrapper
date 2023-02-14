using System;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Handles offsettings the physics hand target based on the connected device. Also offsets the target based on grab point settings if desired.
    /// To manage custom offsets please use the SetMiscPositionOffset(Vector3 position, Vector3 rotation) and ResetGrabPointOffsets() functions.
    /// </summary>
    public class HVRControllerOffset : MonoBehaviour
    {
        /// <summary>
        /// The hand that this controller offset represents.
        /// </summary>
        public HVRHandSide HandSide;

        /// <summary>
        /// The transform that aim's the teleport line, it will be counter rotated when a grabbable requests the hand to rotate when held.
        /// </summary>
        [Tooltip("The transform that aim's the teleport line, it will be counter rotated when a grabbable requests the hand to rotate when held.")]
        public Transform Teleport;

        /// <summary>
        /// Smooth lerp speed to move towards the target grab point rotation offset
        /// </summary>
        [Tooltip("Smooth lerp speed to move towards the target grab point rotation offset")]
        public float GrabPointSmoothSpeed = 8f;

        private HVRDevicePoseOffset _offsets;

        public Vector3 ControllerPositionOffset => _offsets != null ? _offsets.Position : Vector3.zero;
        public Vector3 ControllerRotationOffset => _offsets != null ? _offsets.Rotation : Vector3.zero;

        [Header("Debugging")]

        [Tooltip("Enable to test live updating of the Misc offset fields.")]
        public bool LiveUpdateOffsets;

        [SerializeField] private Vector3 TargetGrabPointPositionOffset;
        [SerializeField] private Vector3 TargetGrabPointRotationOffset;

        [SerializeField] private Vector3 GrabPointPositionOffset;
        [SerializeField] private Vector3 GrabPointRotationOffset;

        [SerializeField] private Vector3 MiscPositionOffset;
        [SerializeField] private Vector3 MiscRotationOffset;

        
        private Quaternion _teleportStartRotation;

        public bool _updatingRotation;
        public bool _updatingPosition;

        protected virtual void Awake()
        {
            if (Teleport)
            {
                _teleportStartRotation = Teleport.localRotation;
            }
        }

        private void Start()
        {
            if (HandSide == HVRHandSide.Left)
            {
                if (HVRInputManager.Instance.LeftController)
                    ControllerConnected(HVRInputManager.Instance.LeftController);
                HVRInputManager.Instance.LeftControllerConnected.AddListener(ControllerConnected);
            }
            else
            {
                if (HVRInputManager.Instance.RightController)
                    ControllerConnected(HVRInputManager.Instance.RightController);
                HVRInputManager.Instance.RightControllerConnected.AddListener(ControllerConnected);
            }


        }

        public void Update()
        {
            if (_updatingRotation)
            {
                GrabPointRotationOffset = Vector3.Lerp(GrabPointRotationOffset, TargetGrabPointRotationOffset, GrabPointSmoothSpeed * Time.deltaTime);
                if (Vector3.Distance(GrabPointRotationOffset, TargetGrabPointRotationOffset) < .01f)
                {
                    _updatingRotation = false;
                    GrabPointRotationOffset = TargetGrabPointRotationOffset;
                    ApplyOffsets();
                }
            }

            if (_updatingPosition)
            {
                GrabPointPositionOffset = Vector3.Lerp(GrabPointPositionOffset, TargetGrabPointPositionOffset, GrabPointSmoothSpeed * Time.deltaTime);
                if (Vector3.Distance(GrabPointPositionOffset, TargetGrabPointPositionOffset) < .01f)
                {
                    _updatingPosition = false;
                    GrabPointPositionOffset = TargetGrabPointPositionOffset;
                    ApplyOffsets();
                }
            }

            if (LiveUpdateOffsets || _updatingPosition || _updatingRotation)
            {
                ApplyOffsets();
            }
        }

        public void SetMiscPositionOffset(Vector3 position, Vector3 rotation)
        {
            MiscPositionOffset = position;
            MiscRotationOffset = rotation;
            ApplyOffsets();
        }


        public void SetGrabPointOffsets(Vector3 position, Vector3 rotation)
        {
            TargetGrabPointPositionOffset = position;
            TargetGrabPointRotationOffset = rotation;
            _updatingRotation = true;
            _updatingPosition = true;
        }

        public void ResetGrabPointOffsets()
        {
            TargetGrabPointPositionOffset = Vector3.zero;
            TargetGrabPointRotationOffset = Vector3.zero;
            _updatingRotation = true;
            _updatingPosition = true;
        }

        public void ApplyOffsets()
        {
            var position = ControllerPositionOffset + GrabPointPositionOffset + MiscPositionOffset;

            if (HandSide == HVRHandSide.Left)
            {
                position.x *= -1f;
            }

            transform.localPosition = position;

            var controllerRotation = Quaternion.Euler(ControllerRotationOffset);
            var grabPointRotation = Quaternion.Euler(GrabPointRotationOffset);
            var miscRotation = Quaternion.Euler(MiscRotationOffset);

            var finalRotation = controllerRotation * grabPointRotation * miscRotation;
            var angles = finalRotation.eulerAngles;

            if (HandSide == HVRHandSide.Left)
            {
                angles.y *= -1f;
                angles.z *= -1f;
            }

            if (Teleport)
            {
                Teleport.localRotation = _teleportStartRotation * Quaternion.Inverse(grabPointRotation);
            }

            transform.localEulerAngles = angles;
        }

        private void ControllerConnected(HVRController controller)
        {
            var offsets = HVRInputManager.Instance.ControllerOffsets;
            if (!offsets)
            {
                Debug.LogWarning($"HVRInputManager.ControllerOffsets are not assigned.");
                return;
            }

            _offsets = offsets.GetDeviceOffset(controller.Side);

            ApplyOffsets();
        }

    }

}
