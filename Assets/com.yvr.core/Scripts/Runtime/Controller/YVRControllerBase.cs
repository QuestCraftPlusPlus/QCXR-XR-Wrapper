using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Core
{
    public static partial class YVRInput
    {
        private abstract class YVRControllerBase
        {
            protected static readonly float BUTTON_THRESHHOLD = 0.5f;

            public ControllerType controllerType = ControllerType.None;

            public YVRPlugin.ControllerState previousState = new YVRPlugin.ControllerState();
            public YVRPlugin.ControllerState currentState = new YVRPlugin.ControllerState();

            protected VirtualButtonMap buttonMap = new VirtualButtonMap();
            protected VirtualTouchMap touchMap = new VirtualTouchMap();
            protected VirtualAxis1DMap axis1DMap = new VirtualAxis1DMap();
            protected VirtualAxis2DMap axis2DMap = new VirtualAxis2DMap();

            protected abstract void ConfigureButtonMap();
            protected abstract void ConfigureTouchMap();
            protected abstract void ConfigureAxis1DMap();
            protected abstract void ConfigureAxis2DMap();

            public YVRControllerBase()
            {
                ConfigureButtonMap();
                ConfigureTouchMap();
                ConfigureAxis1DMap();
                ConfigureAxis2DMap();
            }

            public virtual ControllerType Update() // return this Controller
            {
                previousState = currentState;

                if (!controllerType2IndexMap.ContainsKey(controllerType))
                    return ControllerType.None;

                if (!YVRPlugin.Instance.GetControllerConnected(controllerType2IndexMap[controllerType]))
                    return ControllerType.None;

                currentState = YVRPlugin.Instance.GetControllerState(controllerType2IndexMap[controllerType]);

                return controllerType;
            }

            public RawButton ResolveToRawMask(VirtualButton virtualMask) { return buttonMap.ToRawMask(virtualMask); }

            public RawTouch ResolveToRawMask(VirtualTouch virtualMask) { return touchMap.ToRawMask(virtualMask); }

            public RawAxis1D ResolveToRawMask(VirtualAxis1D virtualMask) { return axis1DMap.ToRawMask(virtualMask); }

            public RawAxis2D ResolveToRawMask(VirtualAxis2D virtualMask) { return axis2DMap.ToRawMask(virtualMask); }

            public virtual void SetControllerVibration(float frequency, float amplitude)
            {
                YVRPlugin.Instance.SetControllerVibration(controllerType2IndexMap[controllerType], frequency, amplitude);
            }

            public virtual int batteryPercentRemaining => 0;

            public virtual int isCharging => 0;
        }
    }
}