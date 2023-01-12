using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Core
{
    public static partial class YVRInput
    {
        private class YVRControllerLeftTouch : YVRControllerBase
        {
            public YVRControllerLeftTouch()
            {
                controllerType = ControllerType.LeftTouch;
            }

            public override ControllerType Update()
            {
                ControllerType baseControllerType = base.Update();

                if (baseControllerType == ControllerType.None)
                    return baseControllerType;

                if (currentState.IndexTrigger >= BUTTON_THRESHHOLD) // Index Trigger
                    currentState.Buttons |= (int) RawButton.LIndexTrigger;

                if (currentState.Thumbstick.y >= BUTTON_THRESHHOLD) // Left Thumb Trigger
                    currentState.Buttons |= (int) RawButton.LThumbstickUp;
                if (currentState.Thumbstick.y <= -BUTTON_THRESHHOLD)
                    currentState.Buttons |= (int) RawButton.LThumbstickDown;
                if (currentState.Thumbstick.x <= -BUTTON_THRESHHOLD)
                    currentState.Buttons |= (int) RawButton.LThumbstickLeft;
                if (currentState.Thumbstick.x >= BUTTON_THRESHHOLD)
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

            protected override void ConfigureAxis1DMap()
            {
                axis1DMap.IndexTrigger = RawAxis1D.LIndexTrigger;
            }

            protected override void ConfigureAxis2DMap()
            {
                axis2DMap.Thumbstick = RawAxis2D.LThumbstick;
            }

            public override int batteryPercentRemaining => Convert.ToInt32(currentState.BatteryPercentRemaining);

            public override int isCharging => Convert.ToInt32(currentState.isCharging);
        }

        private class YVRControllerRightTouch : YVRControllerBase
        {
            public YVRControllerRightTouch()
            {
                controllerType = ControllerType.RightTouch;
            }

            public override ControllerType Update()
            {
                ControllerType baseControllerType = base.Update();

                if (baseControllerType == ControllerType.None)
                    return baseControllerType;

                if (currentState.IndexTrigger >= BUTTON_THRESHHOLD)
                    currentState.Buttons |= (int) RawButton.RIndexTrigger;

                if (currentState.Thumbstick.y >= BUTTON_THRESHHOLD) // Right Thumb Trigger
                    currentState.Buttons |= (uint) RawButton.RThumbstickUp;
                if (currentState.Thumbstick.y <= -BUTTON_THRESHHOLD)
                    currentState.Buttons |= (uint) RawButton.RThumbstickDown;
                if (currentState.Thumbstick.x <= -BUTTON_THRESHHOLD)
                    currentState.Buttons |= (uint) RawButton.RThumbstickLeft;
                if (currentState.Thumbstick.x >= BUTTON_THRESHHOLD)
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

            protected override void ConfigureAxis1DMap()
            {
                axis1DMap.IndexTrigger = RawAxis1D.RIndexTrigger;
            }

            protected override void ConfigureAxis2DMap()
            {
                axis2DMap.Thumbstick = RawAxis2D.RThumbstick;
            }

            public override int batteryPercentRemaining => Convert.ToInt32(currentState.BatteryPercentRemaining);

            public override int isCharging => Convert.ToInt32(currentState.isCharging);
        }
    }
}