using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;

namespace YVR.Core
{
    /// <summary>
    /// Implements all controllers input state related functions
    /// </summary>
    public static partial class YVRInput
    {
        private static YVRControllerBase[] s_ControllersList = null;

        private static ControllerType activeControllerType { get; set; }
        private static ControllerType connectedControllersType { get; set; }
        private static ControllerType clickedControllerType { get; set; }
        private static ControllerType touchedControllerType { get; set; }

        // The index defined in native is different from ones defined here
        // In native, 0 is left controller, 1 is right controller
        private static Dictionary<ControllerType, uint> s_ControllerType2IndexMap = new Dictionary<ControllerType, uint>()
        {
            {ControllerType.LeftTouch, 0},
            {ControllerType.RightTouch, 1}
        };

        /// <summary>
        /// Is controller connected or not
        /// </summary>
        /// <param name="controller"> Target controller </param>
        /// <returns> true when the controller is connected</returns>
        public static bool IsControllerConnected(ControllerType controller)
        {
            if (controller == ControllerType.None) return false;
            if (controller == ControllerType.All) controller = ControllerType.Touch;
            return (connectedControllersType & controller) == controller;
        }

        /// <summary>
        /// Get the active controller in current frame.
        /// </summary>
        /// <remarks>If any button of one controller is pressed or touched during the frame,
        /// then this controller will be marked as active.
        /// <para/>If either left or right controller are marked as active during the frame,
        /// and both controllers are connect, then the Controller will be marked as active
        /// </remarks>
        /// <returns> The @YVR.Core.ControllerType of active controller</returns>
        public static ControllerType GetActiveController() => activeControllerType;

        /// <summary>
        /// Set the existing active controller flag to None and start recording it again.
        /// </summary>
        public static void ClearActiveController() => activeControllerType = ControllerType.None;

        /// <summary>
        /// Get last clicked controller
        /// </summary>
        /// <returns></returns>
        public static ControllerType GetClickedController() => clickedControllerType;

        /// <summary>
        /// Set the existing clicked controller flag to None and start recording it again.
        /// </summary>
        public static void ClearClickedController() => clickedControllerType = ControllerType.None;

        /// <summary>
        /// Get last touched controller
        /// </summary>
        /// <returns></returns>
        public static ControllerType GetTouchedController() => touchedControllerType;

        /// <summary>
        /// Set the existing touched controller flag to None and start recording it again.
        /// </summary>
        public static void ClearTouchedController() => touchedControllerType = ControllerType.None;

        static YVRInput()
        {
            s_ControllersList = new YVRControllerBase[]
            {
                new YVRControllerLeftTouch(),
                new YVRControllerRightTouch()
            };
        }

        [ExcludeFromDocs]
        public static void Update()
        {
            connectedControllersType = ControllerType.None;

            foreach (YVRControllerBase controller in s_ControllersList)
            {
                connectedControllersType |= controller.UpdateControllerState();
                if ((connectedControllersType & controller.controllerType) == controller.controllerType // target controller is connected
                    && (Get(RawButton.All, controller.controllerType) || Get(RawTouch.All, controller.controllerType)))
                {
                    activeControllerType = controller.controllerType;
                }
            }

            // If either Touch is active and both controller are connected, set both controller to active
            if (((activeControllerType == ControllerType.LeftTouch) || (activeControllerType == ControllerType.RightTouch))
                && (connectedControllersType & ControllerType.Touch) == ControllerType.Touch)
                activeControllerType = ControllerType.Touch;

            // Update for clickedControllerType
            if ((connectedControllersType & ControllerType.Touch) != ControllerType.Touch)
                clickedControllerType = connectedControllersType;
            else if (Get(RawButton.All, ControllerType.LeftTouch))
                clickedControllerType = ControllerType.LeftTouch;
            else if (Get(RawButton.All, ControllerType.RightTouch))
                clickedControllerType = ControllerType.RightTouch;

            // Update for touchedControllerType
            if ((connectedControllersType & ControllerType.Touch) != ControllerType.Touch)
                touchedControllerType = connectedControllersType;
            else if (Get(RawTouch.All, ControllerType.LeftTouch))
                touchedControllerType = ControllerType.LeftTouch;
            else if (Get(RawTouch.All, ControllerType.RightTouch))
                touchedControllerType = ControllerType.RightTouch;
        }


        /// <summary>
        /// Get the remaining battery of the controller in percent
        /// </summary>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> Remaining battery in percent </returns>
        public static int GetControllerBatteryPercentRemaining(ControllerType controllerMask) =>
            GetResolvedControllerData(controllerMask, (controller) => controller.batteryPercentRemaining);

        public static bool GetControllerIsCharging(ControllerType controllerMask)
        {
            return IsControllerConnected(controllerMask) &&
                   GetResolvedControllerData(controllerMask, (controller) => controller.isCharging);
        }

        /// <summary>
        /// Make specific controller vibrate at the set frequency and amplitude
        /// </summary>
        /// <param name="frequency"> Vibration frequency </param>
        /// <param name="amplitude"> Vibration amplitude </param>
        /// <param name="controllerMask"> Target controller</param>
        public static void SetControllerVibration(float frequency, float amplitude, ControllerType controllerMask = ControllerType.Active)
        {
            SetResolvedControllerHandle(controllerMask, (controller) => { controller.SetControllerVibration(frequency, amplitude); });
        }

        #region Get

        /// <summary>
        /// Whether the button is holds down
        /// </summary>
        /// <param name="rawMask"> Target button </param>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> true if user holds down the <paramref name = "rawMask"/> button </returns>
        public static bool Get(RawButton rawMask, ControllerType controllerMask = ControllerType.Active)
        {
            return GetResolvedButton(VirtualButton.None, rawMask, controllerMask);
        }

        /// <summary>
        /// Whether the button is holds down
        /// </summary>
        /// <param name="virtualMask">Target button</param>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> true if user holds down the <paramref name="virtualMask"/> button </returns>
        public static bool Get(VirtualButton virtualMask, ControllerType controllerMask = ControllerType.Active)
        {
            return GetResolvedButton(virtualMask, RawButton.None, controllerMask);
        }

        private static bool GetResolvedButton(VirtualButton virtualMask, RawButton rawMask, ControllerType controllerMask)
        {
            controllerMask = GetResolvedController(controllerMask);

            return GetResolvedControllerData(controllerMask, (controller) =>
            {
                RawButton rawButtonMask = rawMask | controller.ResolveToRawMask(virtualMask);
                return ((RawButton) controller.currentState.Buttons & rawButtonMask) != 0;
            });
        }

        /// <summary>
        /// Whether the button is being touched
        /// </summary>
        /// <param name="rawMask"> Target button </param>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> true if user is touching the <paramref name="rawMask"/> button </returns>
        public static bool Get(RawTouch rawMask, ControllerType controllerMask = ControllerType.Active)
        {
            return GetResolvedTouch(VirtualTouch.None, rawMask, controllerMask);
        }

        /// <summary>
        /// Whether the button is being touched
        /// </summary>
        /// <param name="virtualMask">  Target button </param>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> true if user is touching the <paramref name="virtualMask"/> button </returns>
        public static bool Get(VirtualTouch virtualMask, ControllerType controllerMask = ControllerType.Active)
        {
            return GetResolvedTouch(virtualMask, RawTouch.None, controllerMask);
        }

        private static bool GetResolvedTouch(VirtualTouch virtualMask, RawTouch rawMask, ControllerType controllerMask)
        {
            controllerMask = GetResolvedController(controllerMask);

            return GetResolvedControllerData(controllerMask, (controller) =>
            {
                RawTouch rawTouchMask = rawMask | controller.ResolveToRawMask(virtualMask);
                return ((RawTouch) controller.currentState.Touches & rawTouchMask) != 0;
            });
        }

        /// <summary>
        /// Get the trigger position
        /// </summary>
        /// <param name="rawMask"> Target trigger </param>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> position of the <paramref name="rawMask"/> trigger, values is in -1 ~ 1  </returns>
        public static float Get(RawAxis1D rawMask, ControllerType controllerMask = ControllerType.Active)
        {
            return GetResolveAxis1D(VirtualAxis1D.None, rawMask, controllerMask);
        }

        /// <summary>
        /// Get the trigger position
        /// </summary>
        /// <param name="virtualMask"> Target trigger </param>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> position of the <paramref name="rawMask"/> trigger, values is in -1 ~ 1 </returns>
        public static float Get(VirtualAxis1D virtualMask, ControllerType controllerMask = ControllerType.Active)
        {
            return GetResolveAxis1D(virtualMask, RawAxis1D.None, controllerMask);
        }

        private static float GetResolveAxis1D(VirtualAxis1D virtualMask, RawAxis1D rawMask, ControllerType controllerMask)
        {
            controllerMask = GetResolvedController(controllerMask);

            return GetResolvedControllerData(controllerMask, (controller) =>
            {
                RawAxis1D rawAxis1DMask = rawMask | controller.ResolveToRawMask(virtualMask);

                if ((rawAxis1DMask == RawAxis1D.None) ||
                    (rawAxis1DMask == RawAxis1D.LIndexTrigger && controller.controllerType != ControllerType.LeftTouch) ||
                    (rawAxis1DMask == RawAxis1D.RIndexTrigger && controller.controllerType != ControllerType.RightTouch))
                    return 0.0f;

                unsafe
                {
                    fixed (YVRPlugin.ControllerState* stateData = &controller.currentState)
                    {
                        // The 1D Axis data step is 4 bytes and begins from the 8th byte
                        return *((float*) ((byte*) stateData + 8));
                    }
                }
            });
        }

        /// <summary>
        /// Get the thumbstick position
        /// </summary>
        /// <param name="rawMask"> Target thumbstick </param>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> position of <paramref name="rawMask"/> thumbstick, both x/y axis value is in -1 ~ 1 </returns>
        public static Vector2 Get(RawAxis2D rawMask, ControllerType controllerMask = ControllerType.Active)
        {
            return GetResolveAxis2D(VirtualAxis2D.None, rawMask, controllerMask);
        }

        /// <summary>
        /// Get the thumbstick position
        /// </summary>
        /// <param name="virtualMask"> Target thumbstick </param>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> position of <paramref name="virtualMask"/> thumbstick, both x/y axis value is in -1 ~ 1 </returns>
        public static Vector2 Get(VirtualAxis2D virtualMask, ControllerType controllerMask = ControllerType.Active)
        {
            return GetResolveAxis2D(virtualMask, RawAxis2D.None, controllerMask);
        }

        private static Vector2 GetResolveAxis2D(VirtualAxis2D virtualMask, RawAxis2D rawMask, ControllerType controllerMask)
        {
            controllerMask = GetResolvedController(controllerMask);

            return GetResolvedControllerData(controllerMask, (controller) =>
            {
                RawAxis2D rawAxis2DMask = rawMask | controller.ResolveToRawMask(virtualMask); // Right

                if ((rawAxis2DMask == RawAxis2D.None) ||
                    (rawAxis2DMask == RawAxis2D.LThumbstick && controller.controllerType != ControllerType.LeftTouch) ||
                    (rawAxis2DMask == RawAxis2D.RThumbstick && controller.controllerType != ControllerType.RightTouch))
                    return Vector2.zero;

                unsafe
                {
                    fixed (YVRPlugin.ControllerState* stateData = &controller.currentState)
                    {
                        // The 2D Axis data step is 8 bytes and begin from the 12th byte
                        return *((Vector2*) ((byte*) stateData + 12));
                    }
                }
            });
        }

        #endregion

        #region GetDown

        /// <summary>
        /// Whether the button is pressed down in this frame
        /// </summary>
        /// <param name="rawMask"> Target button </param>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> true if the <paramref name="rawMask"/> button is pressed down in this frame </returns>
        public static bool GetDown(RawButton rawMask, ControllerType controllerMask = ControllerType.Active)
        {
            return GetResolvedButtonDown(VirtualButton.None, rawMask, controllerMask);
        }

        /// <summary>
        /// Whether the button is pressed down in this frame
        /// </summary>
        /// <param name="virtualMask"> Target button </param>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> true if the <paramref name="virtualMask"/> button is pressed down in this frame </returns>
        public static bool GetDown(VirtualButton virtualMask, ControllerType controllerMask = ControllerType.Active)
        {
            return GetResolvedButtonDown(virtualMask, RawButton.None, controllerMask);
        }

        private static bool GetResolvedButtonDown(VirtualButton virtualMask, RawButton rawMask, ControllerType controllerMask)
        {
            controllerMask = GetResolvedController(controllerMask);

            return GetResolvedControllerData(controllerMask, (controller) =>
            {
                RawButton rawButtonMask = rawMask | controller.ResolveToRawMask(virtualMask);

                if (((RawButton) controller.previousState.Buttons & rawButtonMask) != 0) // Previous frame this key is pressed
                    return false;

                if ((((RawButton) controller.currentState.Buttons & rawButtonMask) != 0)
                    && (((RawButton) controller.previousState.Buttons & rawButtonMask) == 0))
                    return true;

                return false;
            });
        }

        /// <summary>
        /// Whether the button is touched down in this frame
        /// </summary>
        /// <param name="rawMask"> Target button </param>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> true if the <paramref name="rawMask"/> button is touched down in this frame </returns>
        public static bool GetDown(RawTouch rawMask, ControllerType controllerMask = ControllerType.Active)
        {
            return GetResolvedTouchDown(VirtualTouch.None, rawMask, controllerMask);
        }

        /// <summary>
        /// Whether the button is touched down in this frame
        /// </summary>
        /// <param name="virtualMask"> Target button </param>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> true if the <paramref name="virtualMask"/> button is touched down in this frame </returns>
        public static bool GetDown(VirtualTouch virtualMask, ControllerType controllerMask = ControllerType.Active)
        {
            return GetResolvedTouchDown(virtualMask, RawTouch.None, controllerMask);
        }

        private static bool GetResolvedTouchDown(VirtualTouch virtualMask, RawTouch rawMask, ControllerType controllerMask)
        {
            controllerMask = GetResolvedController(controllerMask);

            return GetResolvedControllerData(controllerMask, (controller) =>
            {
                RawTouch rawTouchMask = rawMask | controller.ResolveToRawMask(virtualMask);
                if (((RawTouch) controller.previousState.Touches & rawTouchMask) != 0) // Previous frame this key is pressed
                    return false;

                if ((((RawTouch) controller.currentState.Touches & rawTouchMask) != 0)
                    && (((RawTouch) controller.previousState.Touches & rawTouchMask) == 0))
                    return true;

                return false;
            });
        }

        #endregion

        #region GetUp

        /// <summary>
        /// Whether the button is pressed up in this frame
        /// </summary>
        /// <param name="rawMask"> Target button </param>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> true if the <paramref name="rawMask"/> button is pressed up in this frame </returns>
        public static bool GetUp(RawButton rawMask, ControllerType controllerMask = ControllerType.Active)
        {
            return GetResolvedButtonUp(VirtualButton.None, rawMask, controllerMask);
        }

        /// <summary>
        /// Whether the button is pressed up in this frame
        /// </summary>
        /// <param name="virtualMask"> Target button </param>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> true if the <paramref name="virtualMask"/> button is pressed up in this frame </returns>
        public static bool GetUp(VirtualButton virtualMask, ControllerType controllerMask = ControllerType.Active)
        {
            return GetResolvedButtonUp(virtualMask, RawButton.None, controllerMask);
        }

        private static bool GetResolvedButtonUp(VirtualButton virtualMask, RawButton rawMask, ControllerType controllerMask)
        {
            controllerMask = GetResolvedController(controllerMask);

            return GetResolvedControllerData(controllerMask, (controller) =>
            {
                RawButton rawButtonMask = rawMask | controller.ResolveToRawMask(virtualMask);
                if (((RawButton) controller.currentState.Buttons & rawButtonMask) != 0) // current frame this key is pressed
                    return false;

                if ((((RawButton) controller.currentState.Buttons & rawButtonMask) == 0)
                    && (((RawButton) controller.previousState.Buttons & rawButtonMask) != 0))
                    return true;

                return false;
            });
        }

        /// <summary>
        /// Whether the button is touched up in this frame
        /// </summary>
        /// <param name="rawMask"> Target button </param>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> true if the <paramref name="rawMask"/> button is touched up in this frame </returns>
        public static bool GetUp(RawTouch rawMask, ControllerType controllerMask = ControllerType.Active)
        {
            return GetResolvedTouchUp(VirtualTouch.None, rawMask, controllerMask);
        }

        /// <summary>
        /// Whether the button is touched up in this frame
        /// </summary>
        /// <param name="virtualMask"> Target button </param>
        /// <param name="controllerMask"> Target controller </param>
        /// <returns> true if the <paramref name="virtualMask"/> button is touched up in this frame </returns>
        public static bool GetUp(VirtualTouch virtualMask, ControllerType controllerMask = ControllerType.Active)
        {
            return GetResolvedTouchUp(virtualMask, RawTouch.None, controllerMask);
        }

        private static bool GetResolvedTouchUp(VirtualTouch virtualMask, RawTouch rawMask, ControllerType controllerMask)
        {
            controllerMask = GetResolvedController(controllerMask);

            return GetResolvedControllerData(controllerMask, (controller) =>
            {
                RawTouch rawTouchMask = rawMask | controller.ResolveToRawMask(virtualMask);
                if (((RawTouch) controller.currentState.Touches & rawTouchMask) != 0) // current frame this key is pressed
                    return false;

                if ((((RawTouch) controller.currentState.Touches & rawTouchMask) == 0)
                    && (((RawTouch) controller.previousState.Touches & rawTouchMask) != 0))
                    return true;

                return false;
            });
        }

        #endregion

        /// <summary>
        /// Get the primary controller set in the global system
        /// </summary>
        /// <returns></returns>
        public static ControllerType GetPrimaryController()
        {
            uint controllerMask = 0;
            YVRPlugin.Instance.GetPrimaryController(ref controllerMask);
            ControllerType controllerType = s_ControllerType2IndexMap.First(kv => kv.Value == controllerMask).Key;
            return controllerType;
        }

        /// <summary>
        /// Set the primary controller in global system
        /// </summary>
        /// <param name="controllerType">Primary controller</param>
        public static void SetPrimaryController(ControllerType controllerType)
        {
            YVRPlugin.Instance.SetPrimaryController(s_ControllerType2IndexMap[controllerType]);
        }

        private static bool GetResolvedControllerData(ControllerType controllerMask, System.Func<YVRControllerBase, bool> handle)
        {
            foreach (YVRControllerBase controller in s_ControllersList)
            {
                if (ShouldResolveThisController(controller.controllerType, controllerMask) && handle(controller))
                    return true;
            }

            return false;
        }

        private static int GetResolvedControllerData(ControllerType controllerMask, System.Func<YVRControllerBase, int> handle)
        {
            if (controllerMask == ControllerType.All)
                return 0;

            if ((controllerMask & ControllerType.Active) != 0) // targetController contains activeController
                controllerMask = activeControllerType;

            if ((controllerMask & connectedControllersType) != controllerMask ||
                (controllerMask == ControllerType.Touch && connectedControllersType != ControllerType.Touch))
                return 0;

            int maxValue = int.MinValue;

            foreach (YVRControllerBase controller in s_ControllersList)
            {
                if (ShouldResolveThisController(controller.controllerType, controllerMask))
                    maxValue = Mathf.Max(maxValue, handle(controller));
            }

            return maxValue == int.MinValue ? 0 : maxValue;
        }

        private static float GetResolvedControllerData(ControllerType controllerMask, System.Func<YVRControllerBase, float> handle)
        {
            float maxValue = float.MinValue;

            foreach (YVRControllerBase controller in s_ControllersList)
            {
                if (ShouldResolveThisController(controller.controllerType, controllerMask))
                    maxValue = Mathf.Max(maxValue, handle(controller));
            }

            return maxValue == float.MinValue ? 0 : maxValue;
        }

        private static Vector2 GetResolvedControllerData(ControllerType controllerMask, System.Func<YVRControllerBase, Vector2> handle)
        {
            Vector2 maxValue = Vector2.zero;

            foreach (YVRControllerBase controller in s_ControllersList)
            {
                if (ShouldResolveThisController(controller.controllerType, controllerMask))
                    maxValue = maxValue.GetMax(handle(controller));
            }

            return maxValue;
        }

        private static ControllerType GetResolvedController(ControllerType controllerMask)
        {
            return controllerMask == ControllerType.Active ? activeControllerType : controllerMask;
        }

        private static void SetResolvedControllerHandle(ControllerType controllerMask, System.Action<YVRControllerBase> handle)
        {
            if ((controllerMask & ControllerType.Active) != 0) // targetController contains activeController
                controllerMask |= activeControllerType;

            foreach (YVRControllerBase controller in s_ControllersList)
            {
                if (ShouldResolveThisController(controller.controllerType, controllerMask))
                    handle(controller);
            }
        }

        private static bool ShouldResolveThisController(ControllerType thisController, ControllerType targetController)
        {
            return (thisController & targetController) != 0; // targetController contains thisController
        }
    }
}