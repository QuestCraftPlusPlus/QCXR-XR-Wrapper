using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;


// TODO: making a emulator base class
namespace YVR.Core
{
    [ExcludeFromDocs]
    public partial class YVRControllerEmulator
    {
        public EmulateController targetController = EmulateController.LeftController;

        public bool isEmulatingLeftController => targetController == EmulateController.LeftController;
        public bool isEmulatingRightController => targetController == EmulateController.RightController;


        public List<KeyCode> activateKeys = new List<KeyCode>() {KeyCode.Space};
        public List<KeyCode> rotationKeys = new List<KeyCode>() {KeyCode.LeftAlt};

        public EmulatorKeyMapping keyMapping = new EmulatorKeyMapping(0);
        public EmulatorRotationSensitivity rotationSensitivity = new EmulatorRotationSensitivity(2.0f);

        public YVRPlugin.ControllerState controllerState = new YVRPlugin.ControllerState();

        private bool m_CurrentFrameEmulationActivated = false;
        private Vector3 m_CurrentMousePositionScreen = Vector3.zero;

        [NonSerialized] public Vector3 currentControllerPosition = Vector3.zero;

        [NonSerialized] public Vector3 currentControllerRotation = Vector3.zero;

        private Camera m_MainCamera = null;

        private Camera mainCamera
        {
            get
            {
                if (!m_MainCamera)
                    m_MainCamera = Camera.main;
                return m_MainCamera;
            }
        }

        private Transform m_Transform = null;

        protected override void Init()
        {
            base.Init();
            if (YVRManager.createdInstance == null) return;

#if UNITY_ANDROID && UNITY_EDITOR
            YVRManager.createdInstance.eventsManager.onUpdate += EmulatorUpdate;
#endif

            m_Transform = transform;

            currentControllerRotation = new Vector3(90, 0, isEmulatingLeftController ? 90 : -90);
        }

        public void EmulatorUpdate()
        {
            try
            {
                m_CurrentFrameEmulationActivated = IsEmulatorActivated();
                if (m_CurrentFrameEmulationActivated)
                {
                    HandleEmulatorButtons();

                    if (IsControllerRotating())
                        ModifyControllerRotation();
                    else
                        ModifyControllerPosition();
                }
            } catch (Exception exception)
            {
                Debug.LogError("Exception in Controller EmulatorUpdate: " + exception);
            }
        }

        private void ModifyControllerRotation()
        {
            // According to mouse moving
            float deltaX = Input.GetAxis("Mouse X");
            float deltaY = Input.GetAxis("Mouse Y");
            currentControllerRotation.y += deltaX * rotationSensitivity.yaw;
            currentControllerRotation.x -= deltaY * rotationSensitivity.pitch;
        }

        private void ModifyControllerPosition()
        {
            // According to mouse position
            m_CurrentMousePositionScreen.x = Input.mousePosition.x;
            m_CurrentMousePositionScreen.y = Input.mousePosition.y;
            m_CurrentMousePositionScreen.z = mainCamera.nearClipPlane + 0.1f;

            Vector3 currentMousePositionViewport = mainCamera.ScreenToViewportPoint(m_CurrentMousePositionScreen);
            currentMousePositionViewport.x *= 2; // WORKAROUND: the rightest screen part is viewport 1.0, but why?

            currentControllerPosition = mainCamera.ViewportToWorldPoint(currentMousePositionViewport);

            currentControllerPosition = m_Transform.worldToLocalMatrix *
                                        new Vector4(currentControllerPosition.x, currentControllerPosition.y,
                                                    currentControllerPosition.z, 1.0f);
        }

        private void HandleEmulatorButtons()
        {
            // Enumerate all buttons in type RawButton to modify ControllerState.Buttons
            foreach (YVRInput.RawButton rawButton in (YVRInput.RawButton[]) Enum.GetValues(typeof(YVRInput.RawButton)))
            {
                if (keyMapping.emulatorKeysMap.ContainsKey(rawButton))
                {
                    if (Input.GetKeyDown(keyMapping.emulatorKeysMap[rawButton]))
                        controllerState.Buttons |= (UInt32) rawButton;
                    else if (Input.GetKeyUp(keyMapping.emulatorKeysMap[rawButton]))
                        controllerState.Buttons &= (UInt32) ~rawButton;
                }
            }
        }

        private bool IsEmulatorActivated()
        {
            foreach (KeyCode key in activateKeys)
            {
                if (Input.GetKey(key))
                    return true;
            }

            return false;
        }

        private bool IsControllerRotating()
        {
            foreach (KeyCode key in rotationKeys)
            {
                if (Input.GetKey(key))
                    return true;
            }

            return false;
        }
    }
}