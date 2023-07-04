#if UNITY_INPUT_SYSTEM
using UnityEngine.Scripting;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace YVR.Core.XR
{
    /// <summary>
    /// A YVR Headset
    /// </summary>
    [Preserve]
    [InputControlLayout]
    public class YVRXRHMD : XRHMD
    {
        [Preserve]
        [InputControl]
        public ButtonControl userPresence { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control deviceAngularVelocity { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control deviceAcceleration { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control deviceAngularAcceleration { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control leftEyeAngularVelocity { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control leftEyeAcceleration { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control leftEyeAngularAcceleration { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control rightEyeAngularVelocity { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control rightEyeAcceleration { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control rightEyeAngularAcceleration { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control centerEyeAngularVelocity { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control centerEyeAcceleration { get; private set; }
        [Preserve]
        [InputControl]
        public Vector3Control centerEyeAngularAcceleration { get; private set; }

        protected override void FinishSetup()
        {
            base.FinishSetup();

            userPresence = GetChildControl<ButtonControl>("userPresence");
            deviceAngularVelocity = GetChildControl<Vector3Control>("deviceAngularVelocity");
            deviceAcceleration = GetChildControl<Vector3Control>("deviceAcceleration");
            deviceAngularAcceleration = GetChildControl<Vector3Control>("deviceAngularAcceleration");
            leftEyeAngularVelocity = GetChildControl<Vector3Control>("leftEyeAngularVelocity");
            leftEyeAcceleration = GetChildControl<Vector3Control>("leftEyeAcceleration");
            leftEyeAngularAcceleration = GetChildControl<Vector3Control>("leftEyeAngularAcceleration");
            rightEyeAngularVelocity = GetChildControl<Vector3Control>("rightEyeAngularVelocity");
            rightEyeAcceleration = GetChildControl<Vector3Control>("rightEyeAcceleration");
            rightEyeAngularAcceleration = GetChildControl<Vector3Control>("rightEyeAngularAcceleration");
            centerEyeAngularVelocity = GetChildControl<Vector3Control>("centerEyeAngularVelocity");
            centerEyeAcceleration = GetChildControl<Vector3Control>("centerEyeAcceleration");
            centerEyeAngularAcceleration = GetChildControl<Vector3Control>("centerEyeAngularAcceleration");
        }
    }
}
#endif