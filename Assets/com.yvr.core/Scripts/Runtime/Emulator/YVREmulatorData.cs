using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;

namespace YVR.Core
{
    [ExcludeFromDocs]
    [Serializable]
    public struct EmulatorRotationSensitivity
    {
        public float pitch;
        public float yaw;
        public float roll;

        public EmulatorRotationSensitivity(float pitch, float yaw, float roll)
        {
            this.pitch = pitch;
            this.yaw = yaw;
            this.roll = roll;
        }

        public EmulatorRotationSensitivity(float sensitivity)
        {
            this.pitch = sensitivity;
            this.yaw = sensitivity;
            this.roll = sensitivity;
        }
    }

    [ExcludeFromDocs]
    [Serializable]
    public struct EmulatorTranslationSensitivity
    {
        public float front;
        public float back;
        public float left;
        public float right;
        public float up;
        public float down;

        public EmulatorTranslationSensitivity(float front, float back, float left, float right, float up, float down)
        {
            this.front = front;
            this.back = back;
            this.left = left;
            this.right = right;
            this.up = up;
            this.down = down;
        }

        public EmulatorTranslationSensitivity(float sensitivity)
        {
            this.front = sensitivity;
            this.back = sensitivity;
            this.left = sensitivity;
            this.right = sensitivity;
            this.up = sensitivity;
            this.down = sensitivity;
        }
    }

    public partial class YVRHMDEmulator : MonoBehaviorSingleton<YVRHMDEmulator>
    {
        [ExcludeFromDocs]
        [Serializable]
        public struct TranslationKeys
        {
            public KeyCode forward;
            public KeyCode backward;
            public KeyCode left;
            public KeyCode right;
            public KeyCode up;
            public KeyCode down;

            public TranslationKeys(KeyCode front, KeyCode back, KeyCode left, KeyCode right, KeyCode up, KeyCode down)
            {
                this.forward = front;
                this.backward = back;
                this.left = left;
                this.right = right;
                this.up = up;
                this.down = down;
            }
        }
    }

    public partial class YVRControllerEmulator : MonoBehaviorSingleton<YVRControllerEmulator>
    {
        [ExcludeFromDocs]
        [Serializable]
        public enum EmulateController // Should using Native Controller value
        {
            LeftController = 0x00000000,
            RightController = 0x0000001
        }

        [ExcludeFromDocs]
        [Serializable]
        public struct EmulatorKeyMapping
        {
            public Dictionary<YVRInput.RawButton, KeyCode> emulatorKeysMap;
            public KeyCode A;
            public KeyCode B;
            public KeyCode X;
            public KeyCode Y;
            public KeyCode LeftIndexTrigger;
            public KeyCode RightIndexTrigger;
            public KeyCode LeftThumbStick;
            public KeyCode RightThumbStick;
            public KeyCode LeftHandTrigger;
            public KeyCode RightHandTrigger;

            public EmulatorKeyMapping(int index)
            {
                A = KeyCode.A;
                B = KeyCode.B;
                X = KeyCode.X;
                Y = KeyCode.Y;
                LeftIndexTrigger = KeyCode.Alpha1;
                RightIndexTrigger = KeyCode.Alpha2;
                LeftThumbStick = KeyCode.Alpha3;
                RightThumbStick = KeyCode.Alpha4;
                LeftHandTrigger = KeyCode.Alpha5;
                RightHandTrigger = KeyCode.Alpha6;
                emulatorKeysMap = new Dictionary<YVRInput.RawButton, KeyCode>();

                GenerateEmulatorKeysMap();
            }

            private void GenerateEmulatorKeysMap()
            {
                emulatorKeysMap.Add(YVRInput.RawButton.A, A);
                emulatorKeysMap.Add(YVRInput.RawButton.B, B);
                emulatorKeysMap.Add(YVRInput.RawButton.X, X);
                emulatorKeysMap.Add(YVRInput.RawButton.Y, Y);
                emulatorKeysMap.Add(YVRInput.RawButton.LIndexTrigger, LeftIndexTrigger);
                emulatorKeysMap.Add(YVRInput.RawButton.RIndexTrigger, RightIndexTrigger);
                emulatorKeysMap.Add(YVRInput.RawButton.LThumbstick, LeftThumbStick);
                emulatorKeysMap.Add(YVRInput.RawButton.RThumbstick, RightThumbStick);
                emulatorKeysMap.Add(YVRInput.RawButton.LHandTrigger, LeftHandTrigger);
                emulatorKeysMap.Add(YVRInput.RawButton.RHandTrigger, RightHandTrigger);
            }
        }
    }
}