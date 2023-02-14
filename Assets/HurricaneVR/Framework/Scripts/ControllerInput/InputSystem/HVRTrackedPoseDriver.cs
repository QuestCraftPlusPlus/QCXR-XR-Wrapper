#if ENABLE_INPUT_SYSTEM
using Assets.HurricaneVR.Framework.Shared.Utilities;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;


namespace HurricaneVR.Framework.ControllerInput.InputSystem
{
    public class HVRTrackedPoseDriver : MonoBehaviour
    {
        public HVRHandSide Side;

        /// <summary>
        /// Options for which <see cref="Transform"/> properties to update.
        /// </summary>
        /// <seealso cref="trackingType"/>
        public enum TrackingType
        {
            /// <summary>
            /// Update both rotation and position.
            /// </summary>
            RotationAndPosition,

            /// <summary>
            /// Update rotation only.
            /// </summary>
            RotationOnly,

            /// <summary>
            /// Update position only.
            /// </summary>
            PositionOnly,
        }

        [SerializeField]
        TrackingType m_TrackingType;
        /// <summary>
        /// The tracking type being used by the Tracked Pose Driver
        /// to control which <see cref="Transform"/> properties to update.
        /// </summary>
        /// <seealso cref="TrackingType"/>
        public TrackingType trackingType
        {
            get => m_TrackingType;
            set => m_TrackingType = value;
        }

        /// <summary>
        /// Options for which phases of the player loop will update <see cref="Transform"/> properties.
        /// </summary>
        /// <seealso cref="updateType"/>
        public enum UpdateType
        {
            /// <summary>
            /// Update after the Input System has completed an update and right before rendering.
            /// </summary>
            /// <seealso cref="InputUpdateType.Dynamic"/>
            /// <seealso cref="InputUpdateType.BeforeRender"/>
            UpdateAndBeforeRender,

            /// <summary>
            /// Update after the Input System has completed an update.
            /// </summary>
            /// <seealso cref="InputUpdateType.Dynamic"/>
            Update,

            /// <summary>
            /// Update right before rendering.
            /// </summary>
            /// <seealso cref="InputUpdateType.BeforeRender"/>
            BeforeRender,
        }

        [SerializeField]
        UpdateType m_UpdateType = UpdateType.UpdateAndBeforeRender;
        /// <summary>
        /// The update type being used by the Tracked Pose Driver
        /// to control which phases of the player loop will update <see cref="Transform"/> properties.
        /// </summary>
        /// <seealso cref="UpdateType"/>
        public UpdateType updateType
        {
            get => m_UpdateType;
            set => m_UpdateType = value;
        }

        [SerializeField]
        InputActionProperty m_PositionInput;
        /// <summary>
        /// The action to read the position value of a tracked device.
        /// Must support reading a value of type <see cref="Vector3"/>.
        /// </summary>
        public InputActionProperty positionInput
        {
            get => m_PositionInput;
            set
            {
                if (Application.isPlaying)
                    UnbindPosition();

                m_PositionInput = value;

                if (Application.isPlaying && isActiveAndEnabled)
                    BindPosition();
            }
        }

        [SerializeField]
        InputActionProperty m_RotationInput;
        /// <summary>
        /// The action to read the rotation value of a tracked device.
        /// Must support reading a value of type <see cref="Quaternion"/>.
        /// </summary>
        public InputActionProperty rotationInput
        {
            get => m_RotationInput;
            set
            {
                if (Application.isPlaying)
                    UnbindRotation();

                m_RotationInput = value;

                if (Application.isPlaying && isActiveAndEnabled)
                    BindRotation();
            }
        }

        Vector3 m_CurrentPosition = Vector3.zero;
        Quaternion m_CurrentRotation = Quaternion.identity;
        bool m_RotationBound;
        bool m_PositionBound;

        void BindActions()
        {
            BindPosition();
            BindRotation();
        }

        void BindPosition()
        {
            if (m_PositionBound)
                return;

            var action = m_PositionInput.action;
            if (action == null || action.bindings.Count == 0)
            {
                if (Side == HVRHandSide.Left)
                    action = HVRInputSystemController.InputActions.LeftHand.ControllerPosition;
                else
                    action = HVRInputSystemController.InputActions.RightHand.ControllerPosition;
            }

            if (!action.enabled)
                action.Enable();

            action.performed += OnPositionPerformed;
            action.canceled += OnPositionCanceled;
            m_PositionBound = true;
            _positionAction = action;
        }

        void BindRotation()
        {
            if (m_RotationBound)
                return;

            var action = m_RotationInput.action;
            if (action == null || action.bindings.Count == 0)
            {
                if (Side == HVRHandSide.Left)
                    action = HVRInputSystemController.InputActions.LeftHand.ControllerRotation;
                else
                    action = HVRInputSystemController.InputActions.RightHand.ControllerRotation;
            }

            if (!action.enabled)
                action.Enable();

            action.performed += OnRotationPerformed;
            action.canceled += OnRotationCanceled;
            m_RotationBound = true;
            _rotationAction = action;
        }

        private InputAction _positionAction;
        private InputAction _rotationAction;



        void UnbindActions()
        {
            UnbindPosition();
            UnbindRotation();
        }

        void UnbindPosition()
        {
            if (!m_PositionBound)
                return;

            var action = _positionAction;
            if (action == null)
                return;

            if (m_PositionInput.action.bindings.Count > 0 && m_PositionInput.reference == null)
                action.Disable();

            action.performed -= OnPositionPerformed;
            action.canceled -= OnPositionCanceled;
            m_PositionBound = false;
            _positionAction = null;
        }

        void UnbindRotation()
        {
            if (!m_RotationBound)
                return;

            var action = _rotationAction;
            if (action == null)
                return;

            if (m_RotationInput.action.bindings.Count > 0 && m_RotationInput.reference == null)
                action.Disable();

            action.performed -= OnRotationPerformed;
            action.canceled -= OnRotationCanceled;
            m_RotationBound = false;
            _rotationAction = null;
        }

        void OnPositionPerformed(InputAction.CallbackContext context)
        {
            Debug.Assert(m_PositionBound, this);
            m_CurrentPosition = context.ReadValue<Vector3>();
        }

        void OnPositionCanceled(InputAction.CallbackContext context)
        {
            Debug.Assert(m_PositionBound, this);
            m_CurrentPosition = Vector3.zero;
        }

        void OnRotationPerformed(InputAction.CallbackContext context)
        {
            Debug.Assert(m_RotationBound, this);
            m_CurrentRotation = context.ReadValue<Quaternion>();
        }

        void OnRotationCanceled(InputAction.CallbackContext context)
        {
            Debug.Assert(m_RotationBound, this);
            m_CurrentRotation = Quaternion.identity;
        }

        /// <summary>
        /// This function is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
#if UNITY_INPUT_SYSTEM_ENABLE_VR && ENABLE_VR
            if (HasStereoCamera())
            {
                UnityEngine.XR.XRDevice.DisableAutoXRCameraTracking(GetComponent<Camera>(), true);
            }
#endif
        }

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        protected void OnEnable()
        {
            if (HVRInputManager.Instance.IsOpenXR)
            {
                UnityEngine.InputSystem.InputSystem.onAfterUpdate += UpdateCallback;
                HVRInputSystemController.Init();
                this.ExecuteNextUpdate(BindActions);
            }
        }

        /// <summary>
        /// This function is called when the object becomes disabled or inactive.
        /// </summary>
        protected void OnDisable()
        {
            if (HVRInputManager.Instance.IsOpenXR)
            {
                UnbindActions();
                UnityEngine.InputSystem.InputSystem.onAfterUpdate -= UpdateCallback;
            }
        }

        /// <summary>
        /// This function is called when the <see cref="MonoBehaviour"/> will be destroyed.
        /// </summary>
        protected virtual void OnDestroy()
        {
#if UNITY_INPUT_SYSTEM_ENABLE_VR && ENABLE_VR
            if (HasStereoCamera())
            {
                UnityEngine.XR.XRDevice.DisableAutoXRCameraTracking(GetComponent<Camera>(), false);
            }
#endif
        }

        protected void UpdateCallback()
        {
            if (InputState.currentUpdateType == InputUpdateType.BeforeRender)
                OnBeforeRender();
            else
                OnUpdate();
        }

        protected virtual void OnUpdate()
        {
            if (m_UpdateType == UpdateType.Update ||
                m_UpdateType == UpdateType.UpdateAndBeforeRender)
            {
                PerformUpdate();
            }
        }

        protected virtual void OnBeforeRender()
        {
            if (m_UpdateType == UpdateType.BeforeRender ||
                m_UpdateType == UpdateType.UpdateAndBeforeRender)
            {
                PerformUpdate();
            }
        }

        protected virtual void SetLocalTransform(Vector3 newPosition, Quaternion newRotation)
        {
            if (m_TrackingType == TrackingType.RotationAndPosition ||
                m_TrackingType == TrackingType.RotationOnly)
            {
                transform.localRotation = newRotation;
            }

            if (m_TrackingType == TrackingType.RotationAndPosition ||
                m_TrackingType == TrackingType.PositionOnly)
            {
                transform.localPosition = newPosition;
            }
        }

        bool HasStereoCamera()
        {
            var cameraComponent = GetComponent<Camera>();
            return cameraComponent != null && cameraComponent.stereoEnabled;
        }

        protected virtual void PerformUpdate()
        {
            SetLocalTransform(m_CurrentPosition, m_CurrentRotation);
        }
    }
}

#endif