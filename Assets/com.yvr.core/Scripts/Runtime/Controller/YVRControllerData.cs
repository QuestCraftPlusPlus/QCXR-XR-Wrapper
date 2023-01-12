using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

//QUESTION: How to reduce the convert times?

namespace YVR.Core
{
    /// <summary>
    /// Flags identifying controller type
    /// </summary>
    public enum ControllerType
    {
        /// <summary>
        /// Not a controller
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Left Controller
        /// </summary>
        LeftTouch = 0x00000001,

        /// <summary>
        /// Right Controller
        /// </summary>
        RightTouch = 0x0000002,

        /// <summary>
        /// Both left and right controllers
        /// </summary>
        Touch = LeftTouch | RightTouch,

        /// <summary>
        /// The active controller
        /// </summary>
        Active = unchecked((int)0x80000000),

        /// <summary>
        /// All controllers, currently equals to @YVR.Core.ControllerType.Touch
        /// </summary>
        All = ~None
    }

    /// <summary>
    /// Enums for input data provider
    /// </summary>
    public enum DataProviderSource
    {
        /// <summary>
        /// HMD data provider
        /// </summary>
        HMD = 0,
        /// <summary>
        /// Left controller data provider
        /// </summary>
        LeftController = 1,
        /// <summary>
        /// Right controller data provider
        /// </summary>
        RightController = 2,
        /// <summary>
        /// Customized data provider
        /// </summary>
        Custom = 3,
        /// <summary>
        /// None data provider
        /// </summary>
        None = -1,
    }

    public static partial class YVRInput
    {
        #region Button

        /// <summary>
        /// Virtual button mapping for controllers
        /// </summary>
        public enum VirtualButton : uint
        {
            /// <summary>
            /// Not a button
            /// </summary>
            None = 0,
            /// <summary>
            /// One Button
            /// </summary>
            One = 1 << 0,
            /// <summary>
            /// Two Button
            /// </summary>
            Two = 1 << 1,
            /// <summary>
            /// Start Button
            /// </summary>
            Start = 1 << 2,
            /// <summary>
            /// Index Trigger Button
            /// </summary>
            IndexTrigger = 1 << 3,
            /// <summary>
            /// Hand Trigger Button
            /// </summary>
            HandTrigger = 1 << 4,
            /// <summary>
            /// Thumb Stick Button
            /// </summary>
            ThumbStick = 1 << 5,
            /// <summary>
            /// Thumb Up Button
            /// </summary>
            ThumbUp = 1 << 6,
            /// <summary>
            /// Thumb Down Button
            /// </summary>
            ThumbDown = 1 << 7,
            /// <summary>
            /// Thumb Left Button
            /// </summary>
            ThumbLeft = 1 << 8,
            /// <summary>
            /// Thumb Right Button
            /// </summary>
            ThumbRight = 1 << 9,
            /// <summary>
            /// Any Button
            /// </summary>
            Any = ~None
        }

        /// <summary>
        /// Raw button mapping for controller
        /// </summary>
        public enum RawButton : uint
        {
            /// <summary>
            /// Not a Button
            /// </summary>
            None = 0,
            /// <summary>
            /// A Button on right controller
            /// </summary>
            A = 1 << 0,
            /// <summary>
            /// B Button on right controller
            /// </summary>
            B = 1 << 1,
            /// <summary>
            /// X Button on left controller
            /// </summary>
            X = 1 << 2,
            /// <summary>
            /// Y Button on left controller
            /// </summary>
            Y = 1 << 3,
            /// <summary>
            /// Start Button on left controller
            /// </summary>
            Start = 1 << 4,
            /// <summary>
            /// Reserved key flag
            /// </summary>
            Reserved = 1 << 5,
            /// <summary>
            /// Index Trigger Button on left controller
            /// </summary>
            LIndexTrigger = 1 << 6,
            /// <summary>
            /// Index Trigger Button on right controller
            /// </summary>
            RIndexTrigger = 1 << 7,
            /// <summary>
            /// Index Hand Button on left controller
            /// </summary>
            LHandTrigger = 1 << 8,
            /// <summary>
            /// Index Hand Button on right controller
            /// </summary>
            RHandTrigger = 1 << 9,
            /// <summary>
            /// Thumb Stick Button on left controller (Pressed or not)
            /// </summary>
            LThumbstick = 1 << 10,
            /// <summary>
            /// Thumb Stick Button on right controller (Pressed or not)
            /// </summary>
            RThumbstick = 1 << 11,
            /// <summary>
            /// Thumb Stick Up Button on left controller
            /// </summary>
            LThumbstickUp = 1 << 12,
            /// <summary>
            /// Thumb Stick Down Button on left controller
            /// </summary>
            LThumbstickDown = 1 << 13,
            /// <summary>
            /// Thumb Stick Left Button on left controller
            /// </summary>
            LThumbstickLeft = 1 << 14,
            /// <summary>
            /// Thumb Stick Right Button on left controller
            /// </summary>
            LThumbstickRight = 1 << 15,
            /// <summary>
            /// Thumb Stick Up Button on right controller
            /// </summary>
            RThumbstickUp = 1 << 16,
            /// <summary>
            /// Thumb Stick Down Button on right controller
            /// </summary>
            RThumbstickDown = 1 << 17,
            /// <summary>
            /// Thumb Stick Left Button on right controller
            /// </summary>
            RThumbstickLeft = 1 << 18,
            /// <summary>
            /// Thumb Stick Right Button on right controller
            /// </summary>
            RThumbstickRight = 1 << 19,
            /// <summary>
            /// All buttons on both controllers
            /// </summary>
            All = ~None
        }

        private class VirtualButtonMap
        {
            // Match to Virtual Button
            public RawButton None = RawButton.None;
            public RawButton One = RawButton.None;
            public RawButton Two = RawButton.None;
            public RawButton Start = RawButton.None;
            public RawButton IndexTrigger = RawButton.None;
            public RawButton HandTrigger = RawButton.None;
            public RawButton ThumbSick = RawButton.None;
            public RawButton ThumbUp = RawButton.None;
            public RawButton ThumbDown = RawButton.None;
            public RawButton ThumbLeft = RawButton.None;
            public RawButton ThumbRight = RawButton.None;

            public RawButton ToRawMask(VirtualButton virtualMask)
            {
                RawButton rawMask = 0;

                if ((virtualMask & VirtualButton.One) != 0)
                    rawMask |= One;
                if ((virtualMask & VirtualButton.Two) != 0)
                    rawMask |= Two;
                if ((virtualMask & VirtualButton.Start) != 0)
                    rawMask |= Start;
                if ((virtualMask & VirtualButton.IndexTrigger) != 0)
                    rawMask |= IndexTrigger;
                if ((virtualMask & VirtualButton.HandTrigger) != 0)
                    rawMask |= HandTrigger;
                if ((virtualMask & VirtualButton.ThumbStick) != 0)
                    rawMask |= ThumbSick;
                if ((virtualMask & VirtualButton.ThumbUp) != 0)
                    rawMask |= ThumbUp;
                if ((virtualMask & VirtualButton.ThumbDown) != 0)
                    rawMask |= ThumbDown;
                if ((virtualMask & VirtualButton.ThumbLeft) != 0)
                    rawMask |= ThumbLeft;
                if ((virtualMask & VirtualButton.ThumbRight) != 0)
                    rawMask |= ThumbRight;

                return rawMask;
            }
        }
        #endregion

        #region Touch
        /// <summary>
        /// Virtual touch mapping for controllers
        /// </summary>
        public enum VirtualTouch : uint
        {
            /// <summary>
            /// Not a touch
            /// </summary>
            None = VirtualButton.None,
            /// <summary>
            /// One Touch
            /// </summary>
            One = VirtualButton.One,
            /// <summary>
            /// Two Touch
            /// </summary>
            Two = VirtualButton.Two,
            /// <summary>
            /// Index Trigger Touch
            /// </summary>
            IndexTrigger = VirtualButton.IndexTrigger,
            /// <summary>
            /// Thumb Stick Touch
            /// </summary>
            Thumbstick = VirtualButton.ThumbStick,
            /// <summary>
            /// Any Touch
            /// </summary>
            Any = ~None
        }

        /// <summary>
        /// Raw touch mapping for controllers
        /// </summary>
        public enum RawTouch : uint
        {
            /// <summary>
            /// Not a touch
            /// </summary>
            None = 0,
            /// <summary>
            /// A Touch on right controller
            /// </summary>
            A = RawButton.A,
            /// <summary>
            /// B Touch on right controller
            /// </summary>
            B = RawButton.B,
            /// <summary>
            /// X Touch on left controller
            /// </summary>
            X = RawButton.X,
            /// <summary>
            /// Y Touch on right controller
            /// </summary>
            Y = RawButton.Y,
            /// <summary>
            /// Index Trigger Touch on left controller
            /// </summary>
            LIndexTrigger = RawButton.LIndexTrigger,
            /// <summary>
            /// Index Trigger Touch on right controller
            /// </summary>
            RIndexTrigger = RawButton.RIndexTrigger,
            /// <summary>
            /// Thumb Stick Touch on left controller
            /// </summary>
            LThumbstick = RawButton.LThumbstick,
            /// <summary>
            /// Thumb Stick Touch on right controller
            /// </summary>
            RThumbstick = RawButton.RThumbstick,
            /// <summary>
            /// All touches on both controllers
            /// </summary>
            All = ~None
        }

        private class VirtualTouchMap
        {
            public RawTouch None = RawTouch.None;
            public RawTouch One = RawTouch.None;
            public RawTouch Two = RawTouch.None;
            public RawTouch IndexTrigger = RawTouch.None;
            public RawTouch ThumbStick = RawTouch.None;

            public RawTouch ToRawMask(VirtualTouch virtualMask)
            {
                RawTouch rawMask = 0;

                if ((virtualMask & VirtualTouch.One) != 0)
                    rawMask |= One;
                if ((virtualMask & VirtualTouch.Two) != 0)
                    rawMask |= Two;
                if ((virtualMask & VirtualTouch.IndexTrigger) != 0)
                    rawMask |= IndexTrigger;
                if ((virtualMask & VirtualTouch.Thumbstick) != 0)
                    rawMask |= ThumbStick;

                return rawMask;
            }
        }
        #endregion

        #region Axis1D
        /// <summary>
        /// Virtual 1D Axis mapping for controller
        /// </summary>
        public enum VirtualAxis1D
        {
            /// <summary>
            /// Not a 1D Axis
            /// </summary>
            None = 0,
            /// <summary>
            /// Index Trigger 1D Axis
            /// </summary>
            IndexTrigger = 1 << 0,
            /// <summary>
            /// Any 1D Axis
            /// </summary>
            Any = ~None
        }

        /// <summary>
        /// Raw 1D Axis mapping for controller
        /// </summary>
        public enum RawAxis1D
        {
            /// <summary>
            /// Not a 1D Axis
            /// </summary>
            None = 0,
            /// <summary>
            /// Index Trigger 1D Axis on left controller
            /// </summary>
            LIndexTrigger = 1 << 0,
            /// <summary>
            /// Index Trigger 1D Axis on right controller
            /// </summary>
            RIndexTrigger = 1 << 1,
        }

        private class VirtualAxis1DMap
        {
            public RawAxis1D None = RawAxis1D.None;
            public RawAxis1D IndexTrigger = RawAxis1D.None;

            public RawAxis1D ToRawMask(VirtualAxis1D virtualMask)
            {
                RawAxis1D rawMask = 0;

                if ((virtualMask & VirtualAxis1D.IndexTrigger) != 0)
                    rawMask |= IndexTrigger;

                return rawMask;
            }
        }
        #endregion

        #region Axis2D
        /// <summary>
        /// Virtual 2D Axis mapping for controllers
        /// </summary>
        public enum VirtualAxis2D
        {
            /// <summary>
            /// Not a virtual 2D Axis
            /// </summary>
            None = 0,
            /// <summary>
            /// Thumb Stick 2D Axis
            /// </summary>
            Thumbstick = 1 << 1,
        }

        /// <summary>
        /// Raw 2D Axis mapping for controllers
        /// </summary>
        public enum RawAxis2D
        {
            /// <summary>
            /// Not a 2D Axis
            /// </summary>
            None = 0,
            /// <summary>
            /// Thumb Stick 2D Axis on left controller
            /// </summary>
            LThumbstick = 1 << 0,
            /// <summary>
            /// Thumb Stick 2D Axis on right controller
            /// </summary>
            RThumbstick = 1 << 1,
        }

        private class VirtualAxis2DMap
        {
            public RawAxis2D Thumbstick = RawAxis2D.None;
            public RawAxis2D ToRawMask(VirtualAxis2D virtualMask)
            {
                RawAxis2D rawMask = 0;

                if ((virtualMask & VirtualAxis2D.Thumbstick) != 0)
                    rawMask |= Thumbstick;

                return rawMask;
            }
        }
        #endregion
    }
}