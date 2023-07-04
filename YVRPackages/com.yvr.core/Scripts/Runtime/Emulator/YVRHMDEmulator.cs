using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;

namespace YVR.Core
{
    [ExcludeFromDocs]
    public partial class YVRHMDEmulator : MonoBehaviorSingleton<YVRHMDEmulator>
    {
        public List<KeyCode> activateKeys = new List<KeyCode>() { KeyCode.LeftShift, KeyCode.RightShift };
        public List<KeyCode> rollKeys = new List<KeyCode>() { KeyCode.LeftAlt, KeyCode.RightAlt };
        public TranslationKeys translationKeys = new TranslationKeys(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Q, KeyCode.E);
        public EmulatorRotationSensitivity rotationSensitivity = new EmulatorRotationSensitivity(1.0f);
        public EmulatorTranslationSensitivity translationSensitivity = new EmulatorTranslationSensitivity(1.0f);

        public bool resetHmdPoseOnRelease = false;
        public bool resetHmdPoseByMiddleMouseButton = true;

        private YVRManager manager = null;
        private Vector3 recordedHeadPoseRelativeOffsetTranslation = Vector3.zero;
        private Vector3 recordedHeadPoseRelativeOffsetRotation = Vector3.zero;
        private Vector3 currentEmulateAngles = Vector3.zero; // Using global variable to reduce memory allocation every Frame
        private Vector3 currentEmulatePosition = Vector3.zero;

        private bool lastFrameEmulationActivated = false;
        private bool currentFrameEmulationActivated = false;

        protected override void Init()
        {
            base.Init();
            manager = YVRManager.instance;

#if UNITY_ANDROID && UNITY_EDITOR
            YVRManager.instance.eventsManager.onUpdate += EmulatorUpdate;
#endif
        }

        public void EmulatorUpdate()
        {
            try
            {
                currentFrameEmulationActivated = isEmulatorActivated();

                if (!ResetHeadsetOnRequired(manager))
                {
                    if (currentFrameEmulationActivated)
                    {
                        ModifyHeadRotation(manager);
                        ModifyHeadPosition(manager);
                    }
                }

                lastFrameEmulationActivated = currentFrameEmulationActivated;
            }
            catch (System.Exception exception)
            {
                Debug.LogError("Exception in Headset EmulatorUpdate: " + exception.ToString());
            }
        }

        private bool ResetHeadsetOnRequired(YVRManager manager)
        {
            // if required, restore to default position while exiting emulating
            if (resetHmdPoseOnRelease && !currentFrameEmulationActivated && lastFrameEmulationActivated)
            {
                // Recording the emulation pose when exit emulating
                recordedHeadPoseRelativeOffsetTranslation = currentEmulatePosition;
                recordedHeadPoseRelativeOffsetRotation = currentEmulateAngles;

                manager.headPoseRelativeOffsetRotation = Vector3.zero;
                manager.headPoseRelativeOffsetTranslation = Vector3.zero;
                return true;
            }

            // if required, restore to the recorded Pose while entering emulating
            if (resetHmdPoseOnRelease && currentFrameEmulationActivated && !lastFrameEmulationActivated)
            {
                manager.headPoseRelativeOffsetTranslation = recordedHeadPoseRelativeOffsetTranslation;
                manager.headPoseRelativeOffsetRotation = recordedHeadPoseRelativeOffsetRotation;
                return true;
            }

            // Reset Head Pose when clicking mouse middle button
            if (resetHmdPoseByMiddleMouseButton && Input.GetMouseButton(2))
            {
                manager.headPoseRelativeOffsetTranslation = Vector3.zero;
                manager.headPoseRelativeOffsetRotation = Vector3.zero;
                return true;
            }

            return false;
        }

        private void ModifyHeadRotation(YVRManager manager)
        {
            currentEmulateAngles = manager.headPoseRelativeOffsetRotation;

            float deltaX = Input.GetAxis("Mouse X");
            float deltaY = Input.GetAxis("Mouse Y");

            float emulatePitch = currentEmulateAngles.x;
            float emulatorYaw = currentEmulateAngles.y;
            float emulatorRoll = currentEmulateAngles.z;

            if (isTweakingRoll())
            {
                emulatorRoll -= deltaX * rotationSensitivity.roll;
                Mathf.Clamp(emulatorRoll, -89, 89); // Limit roll angle o avoid camera flipping
            }
            else
            {
                emulatePitch -= deltaY * rotationSensitivity.pitch;
                emulatorYaw += deltaX * rotationSensitivity.yaw;
            }
            manager.headPoseRelativeOffsetRotation = new Vector3(emulatePitch, emulatorYaw, emulatorRoll);
        }

        private void ModifyHeadPosition(YVRManager manager)
        {
            Quaternion currentEmulateRotation = Quaternion.Euler(currentEmulateAngles);
            currentEmulatePosition = manager.headPoseRelativeOffsetTranslation;

            if (Input.GetKey(translationKeys.forward))
                currentEmulatePosition += currentEmulateRotation * Vector3.forward * Time.deltaTime * translationSensitivity.front;
            if (Input.GetKey(translationKeys.backward))
                currentEmulatePosition += currentEmulateRotation * Vector3.back * Time.deltaTime * translationSensitivity.back;
            if (Input.GetKey(translationKeys.left))
                currentEmulatePosition += currentEmulateRotation * Vector3.left * Time.deltaTime * translationSensitivity.left;
            if (Input.GetKey(translationKeys.right))
                currentEmulatePosition += currentEmulateRotation * Vector3.right * Time.deltaTime * translationSensitivity.right;
            if (Input.GetKey(translationKeys.up))
                currentEmulatePosition += currentEmulateRotation * Vector3.up * Time.deltaTime * translationSensitivity.up;
            if (Input.GetKey(translationKeys.down))
                currentEmulatePosition += currentEmulateRotation * Vector3.down * Time.deltaTime * translationSensitivity.down;
            manager.headPoseRelativeOffsetTranslation = currentEmulatePosition;
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

        private bool isTweakingRoll()
        {
            foreach (KeyCode key in rollKeys)
            {
                if (Input.GetKey(key))
                    return true;
            }

            return false;
        }
    }
}