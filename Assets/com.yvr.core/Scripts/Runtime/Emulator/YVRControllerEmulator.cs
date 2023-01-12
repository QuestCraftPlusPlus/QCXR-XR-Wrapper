using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Internal;


// TODO: makeing a emulator base class
namespace YVR.Core
{
    [ExcludeFromDocs]
    public partial class YVRControllerEmulator : MonoBehaviorSingleton<YVRControllerEmulator>
    {
        public EmulateController targetController = EmulateController.LeftController;

        public bool isEmulatingLeftController => targetController == EmulateController.LeftController;
        public bool isEmulatingRightController => targetController == EmulateController.RightController;


        public List<KeyCode> activateKeys = new List<KeyCode>() {KeyCode.Space};
        public List<KeyCode> rotationKeys = new List<KeyCode>() {KeyCode.LeftAlt};

        public EmulatorKeyMapping keyMapping = new EmulatorKeyMapping(0);
        public EmulatorRotationSensitivity rotationSensitivity = new EmulatorRotationSensitivity(2.0f);

        public YVRPlugin.ControllerState controllerState = new YVRPlugin.ControllerState();

        private bool lastFrameEmulationActivated = false;
        private bool currentFrameEmulationActivated = false;
        private Vector3 currentMousePosition_Screen = Vector3.zero;

        [NonSerialized]
        public Vector3 currentControllerPosition = Vector3.zero;

        [NonSerialized]
        public Vector3 currentControllerRotation = Vector3.zero;

        private Camera _mainCamera = null;

        private Camera mainCamera
        {
            get
            {
                if (!_mainCamera)
                    _mainCamera = Camera.main;
                return _mainCamera;
            }
        }

        private new Transform transform = null;

        protected override void Init()
        {
            base.Init();

#if UNITY_ANDROID && UNITY_EDITOR
            YVRManager.instance.eventsManager.onUpdate += EmulatorUpdate;
#endif

            this.transform = base.transform;

            currentControllerRotation = new Vector3(90, 0, isEmulatingLeftController ? 90 : -90);
        }

        public void EmulatorUpdate()
        {
            try
            {
                currentFrameEmulationActivated = isEmulatorActivated();
                if (currentFrameEmulationActivated)
                {
                    HandleEmulatorButtons();

                    if (isControllerRotating())
                        ModifyControllerRotation();
                    else
                        ModifyControllerPosition();
                }

                lastFrameEmulationActivated = currentFrameEmulationActivated;
            } catch (System.Exception exception)
            {
                Debug.LogError("Exception in Controller EmulatorUpdate: " + exception.ToString());
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
            currentMousePosition_Screen.x = Input.mousePosition.x;
            currentMousePosition_Screen.y = Input.mousePosition.y;
            currentMousePosition_Screen.z = mainCamera.nearClipPlane + 0.1f;

            Vector3 currentMousePosition_Viewport = mainCamera.ScreenToViewportPoint(currentMousePosition_Screen);
            currentMousePosition_Viewport.x *= 2; // WORKAROUND: the rightest screen part is viewport 1.0, but why?

            currentControllerPosition = mainCamera.ViewportToWorldPoint(currentMousePosition_Viewport);

            currentControllerPosition = transform.worldToLocalMatrix * new Vector4(currentControllerPosition.x, currentControllerPosition.y, currentControllerPosition.z, 1.0f);
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

        private bool isEmulatorActivated()
        {
            foreach (KeyCode key in activateKeys)
            {
                if (Input.GetKey(key))
                    return true;
            }

            return false;
        }

        private bool isControllerRotating()
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