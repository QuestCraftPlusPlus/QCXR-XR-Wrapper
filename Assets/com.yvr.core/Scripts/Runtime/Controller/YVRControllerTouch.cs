using System;
using UnityEngine;
using UnityEngine.XR;

namespace YVR.Core
{
    public static partial class YVRInput
    {
        private class YVRControllerLeftTouch : YVRControllerBase
        {
            public YVRControllerLeftTouch() { controllerType = ControllerType.LeftTouch; }

            protected override InputDevice inputDevice =>
                device == default ? InputDevices.GetDeviceAtXRNode(XRNode.LeftHand) : device;

            public override ControllerType UpdateControllerState()
            {
                ControllerType baseControllerType = base.UpdateControllerState();

                if (baseControllerType == ControllerType.None)
                    return baseControllerType;

                if (!inputDevice.isValid)
                {
                    currentState = YVRControllerEmulator.instance.controllerState;
                    return controllerType;
                }

                if (GetBooleanAction(CommonUsages.primaryButton)) currentState.Buttons |= (int) RawButton.X;
                if (GetBooleanAction(CommonUsages.secondaryButton)) currentState.Buttons |= (int) RawButton.Y;
                if (GetBooleanAction(CommonUsages.triggerButton)) currentState.Buttons |= (int) RawButton.LIndexTrigger;
                if (GetBooleanAction(CommonUsages.gripButton)) currentState.Buttons |= (int) RawButton.LHandTrigger;
                if (GetBooleanAction(CommonUsages.menuButton)) currentState.Buttons |= (int) RawButton.Start;
                if (GetBooleanAction(CommonUsages.primary2DAxisClick))
                    currentState.Buttons |= (int) RawButton.LThumbstick;

                if (GetBooleanAction(CommonUsages.primaryTouch)) currentState.Touches |= (int) RawButton.X;
                if (GetBooleanAction(CommonUsages.secondaryTouch)) currentState.Touches |= (int) RawButton.Y;
                if (GetBooleanAction(new InputFeatureUsage<bool>("IndexTouch")))
                    currentState.Touches |= (int) RawButton.LIndexTrigger;
                if (GetBooleanAction(CommonUsages.primary2DAxisTouch))
                    currentState.Touches |= (int) RawButton.LThumbstick;

                currentState.Thumbstick = GetVector2Action(CommonUsages.primary2DAxis);
                currentState.IndexTrigger = GetFloatAction(CommonUsages.trigger);
                currentState.BatteryPercentRemaining = GetFloatAction(CommonUsages.batteryLevel);
                currentState.isCharging = GetBooleanAction(ExtendUsages.batteryCharging);

                if (currentState.IndexTrigger >= buttonThreshold) // Index Trigger
                    currentState.Buttons |= (int) RawButton.LIndexTrigger;

                if (currentState.Thumbstick.y >= buttonThreshold) // Left Thumb Trigger
                    currentState.Buttons |= (int) RawButton.LThumbstickUp;
                if (currentState.Thumbstick.y <= -buttonThreshold)
                    currentState.Buttons |= (int) RawButton.LThumbstickDown;
                if (currentState.Thumbstick.x <= -buttonThreshold)
                    currentState.Buttons |= (int) RawButton.LThumbstickLeft;
                if (currentState.Thumbstick.x >= buttonThreshold)
                    currentState.Buttons |= (int) RawButton.LThumbstickRight;

                return controllerType;
            }

            protected override void ConfigureButtonMap()
            {
                buttonMap.None = RawButton.None;
                buttonMap.One = RawButton.X;
                buttonMap.Two = RawButton.Y;
                buttonMap.Start = RawButton.Start;
                buttonMap.IndexTrigger = RawButton.LIndexTrigger;
                buttonMap.HandTrigger = RawButton.LHandTrigger;
                buttonMap.ThumbSick = RawButton.LThumbstick;
                buttonMap.ThumbUp = RawButton.LThumbstickUp;
                buttonMap.ThumbDown = RawButton.LThumbstickDown;
                buttonMap.ThumbLeft = RawButton.LThumbstickLeft;
                buttonMap.ThumbRight = RawButton.LThumbstickRight;
            }

            protected override void ConfigureTouchMap()
            {
                touchMap.None = RawTouch.None;
                touchMap.One = RawTouch.X;
                touchMap.Two = RawTouch.Y;
                touchMap.IndexTrigger = RawTouch.LIndexTrigger;
                touchMap.ThumbStick = RawTouch.LThumbstick;
            }

            protected override void ConfigureAxis1DMap() { axis1DMap.IndexTrigger = RawAxis1D.LIndexTrigger; }

            protected override void ConfigureAxis2DMap() { axis2DMap.Thumbstick = RawAxis2D.LThumbstick; }

            public override int batteryPercentRemaining =>
                Convert.ToInt32(currentState.BatteryPercentRemaining * 100.0f);

            public override bool isCharging => currentState.isCharging;
        }

        private class YVRControllerRightTouch : YVRControllerBase
        {
            public YVRControllerRightTouch() { controllerType = ControllerType.RightTouch; }

            public override ControllerType UpdateControllerState()
            {
                ControllerType baseControllerType = base.UpdateControllerState();

                if (baseControllerType == ControllerType.None)
                    return baseControllerType;

                if (!inputDevice.isValid)
                {
                    currentState = YVRControllerEmulator.instance.controllerState;
                    return controllerType;
                }

                if (GetBooleanAction(CommonUsages.primaryButton)) currentState.Buttons |= (int) RawButton.A;
                if (GetBooleanAction(CommonUsages.secondaryButton)) currentState.Buttons |= (int) RawButton.B;
                if (GetBooleanAction(CommonUsages.triggerButton)) currentState.Buttons |= (int) RawButton.RIndexTrigger;
                if (GetBooleanAction(CommonUsages.gripButton)) currentState.Buttons |= (int) RawButton.RHandTrigger;
                if (GetBooleanAction(CommonUsages.menuButton)) currentState.Buttons |= (int) RawButton.Reserved;
                if (GetBooleanAction(CommonUsages.primary2DAxisClick))
                    currentState.Buttons |= (int) RawButton.RThumbstick;

                if (GetBooleanAction(CommonUsages.primaryTouch)) currentState.Touches |= (int) RawButton.A;
                if (GetBooleanAction(CommonUsages.secondaryTouch)) currentState.Touches |= (int) RawButton.B;
                if (GetBooleanAction(new InputFeatureUsage<bool>("IndexTouch")))
                    currentState.Touches |= (int) RawButton.RIndexTrigger;
                if (GetBooleanAction(CommonUsages.primary2DAxisTouch))
                    currentState.Touches |= (int) RawButton.RThumbstick;

                currentState.Thumbstick = GetVector2Action(CommonUsages.primary2DAxis);
                currentState.IndexTrigger = GetFloatAction(CommonUsages.trigger);
                currentState.BatteryPercentRemaining = GetFloatAction(CommonUsages.batteryLevel);
                currentState.isCharging = GetBooleanAction(ExtendUsages.batteryCharging);

                if (currentState.IndexTrigger >= buttonThreshold)
                    currentState.Buttons |= (int) RawButton.RIndexTrigger;

                if (currentState.Thumbstick.y >= buttonThreshold) // Right Thumb Trigger
                    currentState.Buttons |= (uint) RawButton.RThumbstickUp;
                if (currentState.Thumbstick.y <= -buttonThreshold)
                    currentState.Buttons |= (uint) RawButton.RThumbstickDown;
                if (currentState.Thumbstick.x <= -buttonThreshold)
                    currentState.Buttons |= (uint) RawButton.RThumbstickLeft;
                if (currentState.Thumbstick.x >= buttonThreshold)
                    currentState.Buttons |= (uint) RawButton.RThumbstickRight;

                return controllerType;
            }

            protected override void ConfigureButtonMap()
            {
                buttonMap.None = RawButton.None;
                buttonMap.One = RawButton.A;
                buttonMap.Two = RawButton.B;
                buttonMap.Start = RawButton.Start;
                buttonMap.IndexTrigger = RawButton.RIndexTrigger;
                buttonMap.HandTrigger = RawButton.RHandTrigger;
                buttonMap.ThumbSick = RawButton.RThumbstick;
                buttonMap.ThumbUp = RawButton.RThumbstickUp;
                buttonMap.ThumbDown = RawButton.RThumbstickDown;
                buttonMap.ThumbLeft = RawButton.RThumbstickLeft;
                buttonMap.ThumbRight = RawButton.RThumbstickRight;
            }

            protected override void ConfigureTouchMap()
            {
                touchMap.None = RawTouch.None;
                touchMap.One = RawTouch.A;
                touchMap.Two = RawTouch.B;
                touchMap.IndexTrigger = RawTouch.RIndexTrigger;
                touchMap.ThumbStick = RawTouch.RThumbstick;
            }

            protected override void ConfigureAxis1DMap() { axis1DMap.IndexTrigger = RawAxis1D.RIndexTrigger; }

            protected override void ConfigureAxis2DMap() { axis2DMap.Thumbstick = RawAxis2D.RThumbstick; }

            protected override InputDevice inputDevice =>
                device == default ? InputDevices.GetDeviceAtXRNode(XRNode.RightHand) : device;

            public override int batteryPercentRemaining =>
                Convert.ToInt32(currentState.BatteryPercentRemaining * 100.0f);

            public override bool isCharging => currentState.isCharging;
        }
    }
}