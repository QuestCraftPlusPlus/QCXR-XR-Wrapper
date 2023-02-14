using UnityEngine;
using UnityEngine.XR;

namespace YVR.Core
{
    public static partial class YVRInput
    {
        private abstract class YVRControllerBase
        {
            protected static readonly float buttonThreshold = 0.5f;

            public ControllerType controllerType = ControllerType.None;

            public YVRPlugin.ControllerState previousState = new YVRPlugin.ControllerState();
            public YVRPlugin.ControllerState currentState = new YVRPlugin.ControllerState();

            protected VirtualButtonMap buttonMap = new VirtualButtonMap();
            protected VirtualTouchMap touchMap = new VirtualTouchMap();
            protected VirtualAxis1DMap axis1DMap = new VirtualAxis1DMap();
            protected VirtualAxis2DMap axis2DMap = new VirtualAxis2DMap();
            protected InputDevice device = default;

            protected abstract void ConfigureButtonMap();
            protected abstract void ConfigureTouchMap();
            protected abstract void ConfigureAxis1DMap();
            protected abstract void ConfigureAxis2DMap();
            protected abstract InputDevice inputDevice { get; }

            public YVRControllerBase()
            {
                ConfigureButtonMap();
                ConfigureTouchMap();
                ConfigureAxis1DMap();
                ConfigureAxis2DMap();
            }

            public virtual ControllerType UpdateControllerState() // return this Controller
            {
                previousState = currentState;

                if (!s_ControllerType2IndexMap.ContainsKey(controllerType))
                    return ControllerType.None;

                if (!Application.isEditor && (inputDevice == default || !inputDevice.isValid))
                    return ControllerType.None;

                currentState.Clear();

                return controllerType;
            }

            public RawButton ResolveToRawMask(VirtualButton virtualMask) { return buttonMap.ToRawMask(virtualMask); }

            public RawTouch ResolveToRawMask(VirtualTouch virtualMask) { return touchMap.ToRawMask(virtualMask); }

            public RawAxis1D ResolveToRawMask(VirtualAxis1D virtualMask) { return axis1DMap.ToRawMask(virtualMask); }

            public RawAxis2D ResolveToRawMask(VirtualAxis2D virtualMask) { return axis2DMap.ToRawMask(virtualMask); }

            public virtual void SetControllerVibration(float frequency, float amplitude)
            {
                YVRPlugin.Instance.SetControllerVibration(s_ControllerType2IndexMap[controllerType], frequency,
                    amplitude);
            }

            public virtual int batteryPercentRemaining => 0;

            public virtual bool isCharging => false;

            protected bool GetBooleanAction(in InputFeatureUsage<bool> usage)
            {
                inputDevice.TryGetFeatureValue(usage, out bool result);
                return result;
            }

            protected Vector2 GetVector2Action(in InputFeatureUsage<Vector2> usage)
            {
                inputDevice.TryGetFeatureValue(usage, out Vector2 result);
                return result;
            }

            protected float GetFloatAction(in InputFeatureUsage<float> usage)
            {
                inputDevice.TryGetFeatureValue(usage, out float result);
                return result;
            }
        }
    }
}