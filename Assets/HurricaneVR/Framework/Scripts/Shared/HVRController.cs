using System.Collections.Generic;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Shared.Utilities;
using UnityEngine;
using UnityEngine.XR;

namespace HurricaneVR.Framework.Shared
{


    public abstract class HVRController : MonoBehaviour
    {
        public HVRHandSide Side { get; set; }

        public HVRButtonState GripButtonState;
        public HVRButtonState TriggerButtonState;
        public HVRButtonState PrimaryButtonState;
        public HVRButtonState SecondaryButtonState;
        public HVRButtonState MenuButtonState;
        public HVRButtonState PrimaryTouchButtonState;
        public HVRButtonState SecondaryTouchButtonState;
        public HVRButtonState JoystickButtonState;
        public HVRButtonState TrackpadButtonState;

        public HVRButtonState JoystickTouchState;
        public HVRButtonState TrackPadTouchState;
        public HVRButtonState TriggerTouchState;
        public HVRButtonState ThumbTouchState;

        public HVRButtonState TriggerNearTouchState;
        public HVRButtonState ThumbNearTouchState;

        public HVRButtonState TrackPadUp;
        public HVRButtonState TrackPadLeft;
        public HVRButtonState TrackPadRight;
        public HVRButtonState TrackPadDown;

        public Vector2 JoystickAxis;
        public Vector2 TrackpadAxis;

        public bool PrimaryButton;
        public bool SecondaryButton;
        public bool JoystickClicked;
        public bool TrackPadClicked;
        public bool MenuButton;
        public bool PrimaryTouch;
        public bool SecondaryTouch;

        public float Grip;
        public float GripForce;
        public float Trigger;

        public bool ThumbTouch;
        public bool TriggerTouch;

        public bool ThumbNearTouch;
        public bool TriggerNearTouch;

        /// <summary>
        /// Supplied by SteamVR, OpenXR, or OVR input bindings
        /// </summary>
        public bool GripButton;

        /// <summary>
        /// Suppplied by SteamVR, OpenXR, or OVR input bindings
        /// </summary>
        public bool TriggerButton;

        public bool JoystickTouch;
        public bool TrackPadTouch;

        public float[] FingerCurls;
        public float ThumbCurl;
        public float IndexCurl;
        public float MiddleCurl;
        public float RingCurl;
        public float PinkyCurl;

        public Vector3 Velocity;
        public Vector3 AngularVelocity;

        public bool IsActive { get; set; }

        public XRNode XRNode;

        private InputDevice _device;
        public InputDevice Device
        {
            get
            {
                if (_device.isValid)
                    return _device;
                _device = InputDevices.GetDeviceAtXRNode(XRNode);
                return _device;
            }
        }

        public Vector2 ThumbstickDeadZone { get; set; }
        public HVRInputSettings InputMap { get; set; }
        public HVRFingerSettings FingerSettings { get; set; }

        public bool Knuckles => ControllerType == HVRControllerType.Knuckles;
        public bool WMR => ControllerType == HVRControllerType.WMR;

        public bool Vive => ControllerType == HVRControllerType.Vive;

        private static readonly Dictionary<HVRButtons, HVRButtonState> _leftButtonStates = new Dictionary<HVRButtons, HVRButtonState>();
        private static readonly Dictionary<HVRButtons, HVRButtonState> _rightButtonStates = new Dictionary<HVRButtons, HVRButtonState>();

        public static float[] LeftFingerCurls = new float[5];
        public static float[] RightFingerCurls = new float[5];

        public HVRControllerType ControllerType { get; set; }

        public float AngularVelocityMagnitude;
        public float VelocityMagnitude;

        public readonly CircularBuffer<float> RecentVelocities = new CircularBuffer<float>(200);


        protected virtual void Awake()
        {
            FingerCurls = new float[5];
        }

        protected virtual void Start()
        {
            ResetTrackedVelocities();
        }

        private void Update()
        {
            UpdateInput();

            CorrectDeadzone();

            CheckButtonState(HVRButtons.Grip, ref GripButtonState);
            CheckButtonState(HVRButtons.Trigger, ref TriggerButtonState);
            CheckButtonState(HVRButtons.JoystickButton, ref JoystickButtonState);
            CheckButtonState(HVRButtons.TrackPadButton, ref TrackpadButtonState);
            CheckButtonState(HVRButtons.Primary, ref PrimaryButtonState);
            CheckButtonState(HVRButtons.Secondary, ref SecondaryButtonState);
            CheckButtonState(HVRButtons.Menu, ref MenuButtonState);
            CheckButtonState(HVRButtons.PrimaryTouch, ref PrimaryTouchButtonState);
            CheckButtonState(HVRButtons.SecondaryTouch, ref SecondaryTouchButtonState);
            CheckButtonState(HVRButtons.JoystickTouch, ref JoystickTouchState);
            CheckButtonState(HVRButtons.TrackPadTouch, ref TrackPadTouchState);
            CheckButtonState(HVRButtons.TriggerTouch, ref TriggerTouchState);
            CheckButtonState(HVRButtons.ThumbTouch, ref ThumbTouchState);
            CheckButtonState(HVRButtons.TriggerNearTouch, ref TriggerNearTouchState);
            CheckButtonState(HVRButtons.ThumbNearTouch, ref ThumbNearTouchState);

            CheckButtonState(HVRButtons.TrackPadUp, ref TrackPadUp);
            CheckButtonState(HVRButtons.TrackPadLeft, ref TrackPadLeft);
            CheckButtonState(HVRButtons.TrackPadRight, ref TrackPadRight);
            CheckButtonState(HVRButtons.TrackPadDown, ref TrackPadDown);



            RecentVelocities.Enqueue(Velocity.magnitude);

            Device.TryGetFeatureValue(CommonUsages.deviceVelocity, out Velocity);
            Device.TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out AngularVelocity);

            AngularVelocityMagnitude = AngularVelocity.magnitude;
            VelocityMagnitude = Velocity.magnitude;

            UpdateFingerCurls();

            var curls = LeftFingerCurls;
            if (XRNode == XRNode.RightHand)
            {
                curls = RightFingerCurls;
            }

            for (int i = 0; i < 5; i++)
            {
                curls[i] = FingerCurls[i];
            }

            AfterInputUpdate();
        }

        protected virtual void AfterInputUpdate()
        {

        }

        protected virtual void UpdateFingerCurls()
        {
            if (FingerSettings)
            {
                FingerSettings.Evaluate(
                    FingerCurls,
                    Grip,
                    Trigger,
                    TriggerTouchState.Active,
                    PrimaryTouchButtonState.Active,
                    SecondaryTouchButtonState.Active,
                    TrackPadTouchState.Active,
                    JoystickTouchState.Active,
                    Knuckles,
                    HVRInputManager.Instance.IsOpenXR);
            }
        }

        protected void ResetTrackedVelocities()
        {
            for (var i = 0; i < 200; i++)
            {
                RecentVelocities.Enqueue(0f);
            }
        }

        public float GetAverageVelocity(float seconds)
        {
            var frames = seconds / Time.fixedDeltaTime;
            var sum = 0f;
            for (var i = 0; i < frames; i++)
            {
                sum += RecentVelocities[i];
            }

            if (frames == 0f) return 0f;
            return sum / frames;
        }

        private void CorrectDeadzone()
        {
            if (Mathf.Abs(JoystickAxis.x) < ThumbstickDeadZone.x) JoystickAxis.x = 0f;
            if (Mathf.Abs(JoystickAxis.y) < ThumbstickDeadZone.y) JoystickAxis.y = 0f;
        }

        protected abstract void UpdateInput();

        protected virtual void CheckButtonState(HVRButtons button, ref HVRButtonState buttonState)
        {
            ResetButton(ref buttonState);

            switch (button)
            {
                case HVRButtons.Grip:
                    buttonState.Value = Grip;
                    SetButtonState(button, ref buttonState, GetIsGripPressed());
                    break;
                case HVRButtons.Trigger:
                    buttonState.Value = Trigger;
                    SetButtonState(button, ref buttonState, GetIsTriggerPressed());
                    break;
                case HVRButtons.Primary:
                    SetButtonState(button, ref buttonState, PrimaryButton);
                    break;
                case HVRButtons.PrimaryTouch:
                    SetButtonState(button, ref buttonState, PrimaryTouch);
                    break;
                case HVRButtons.Secondary:
                    SetButtonState(button, ref buttonState, SecondaryButton);
                    break;
                case HVRButtons.SecondaryTouch:
                    SetButtonState(button, ref buttonState, SecondaryTouch);
                    break;
                case HVRButtons.Menu:
                    SetButtonState(button, ref buttonState, MenuButton);
                    break;
                case HVRButtons.JoystickButton:
                    SetButtonState(button, ref buttonState, JoystickClicked);
                    break;
                case HVRButtons.TrackPadButton:
                    SetButtonState(button, ref buttonState, TrackPadClicked);
                    break;
                case HVRButtons.JoystickTouch:
                    SetButtonState(button, ref buttonState, JoystickTouch);
                    break;
                case HVRButtons.TrackPadTouch:
                    SetButtonState(button, ref buttonState, TrackPadTouch);
                    break;
                case HVRButtons.TriggerTouch:
                    SetButtonState(button, ref buttonState, TriggerTouch);
                    break;
                case HVRButtons.ThumbTouch:
                    SetButtonState(button, ref buttonState, ThumbTouch);
                    break;
                case HVRButtons.TriggerNearTouch:
                    SetButtonState(button, ref buttonState, TriggerNearTouch);
                    break;
                case HVRButtons.ThumbNearTouch:
                    SetButtonState(button, ref buttonState, ThumbNearTouch);
                    break;
                case HVRButtons.TrackPadLeft:
                    SetButtonState(button, ref buttonState, TrackPadClicked && TrackpadAxis.x <= -InputMap.Axis2DLeftThreshold);
                    break;
                case HVRButtons.TrackPadRight:
                    SetButtonState(button, ref buttonState, TrackPadClicked && TrackpadAxis.x >= InputMap.Axis2DRighThreshold);
                    break;
                case HVRButtons.TrackPadUp:
                    SetButtonState(button, ref buttonState, TrackPadClicked && TrackpadAxis.y >= InputMap.Axis2DUpThreshold);
                    break;
                case HVRButtons.TrackPadDown:
                    SetButtonState(button, ref buttonState, TrackPadClicked && TrackpadAxis.y <= -InputMap.Axis2DDownThreshold);
                    break;
            }
        }

        private bool _triggerLowerReset;
        private bool _triggerUpperReset;

        private bool _gripLowerReset;
        private bool _gripUpperReset;

        protected virtual bool GetIsTriggerPressed()
        {
            if (InputMap.TriggerUseAnalog)
            {
                if (InputMap.TriggerUseReleaseThreshold)
                {
                    if (InputMap.TriggerThreshold > InputMap.TriggerReleaseThreshold)
                    {
                        if (TriggerButtonState.Active)
                            return Trigger >= InputMap.TriggerReleaseThreshold;
                        return Trigger >= InputMap.TriggerThreshold;
                    }


                    if (Trigger < InputMap.TriggerThreshold)
                        _triggerLowerReset = true;

                    if (TriggerButtonState.Active)
                    {
                        if (Trigger > InputMap.TriggerReleaseThreshold)
                            _triggerUpperReset = true;

                        if (Trigger < InputMap.TriggerReleaseThreshold && _triggerUpperReset)
                        {
                            _triggerUpperReset = false;
                            return false;
                        }

                        if (Trigger < InputMap.TriggerThreshold)
                            return false;

                        return true;
                    }

                    if (Trigger > InputMap.TriggerReleaseThreshold && !_triggerUpperReset && !InputMap.TriggerRequireReset)
                        return true;

                    if (Trigger > InputMap.TriggerThreshold && _triggerLowerReset)
                    {
                        _triggerLowerReset = false;
                        return true;
                    }

                    return false;
                }

                return Trigger >= InputMap.TriggerThreshold;
            }

            return TriggerButton;
        }

        protected virtual bool GetIsGripPressed()
        {
            if (InputMap.GripUseAnalog)
            {
                if (InputMap.GripUseReleaseThreshold)
                {
                    if (InputMap.GripThreshold > InputMap.GripReleaseThreshold)
                    {
                        if (GripButtonState.Active)
                            return Grip >= InputMap.GripReleaseThreshold;
                        return Grip >= InputMap.GripThreshold;
                    }


                    if (Grip < InputMap.GripThreshold)
                        _gripLowerReset = true;

                    if (GripButtonState.Active)
                    {
                        if (Grip > InputMap.GripReleaseThreshold)
                            _gripUpperReset = true;

                        if (Grip < InputMap.GripReleaseThreshold && _gripUpperReset)
                        {
                            _gripUpperReset = false;
                            return false;
                        }

                        if (Grip < InputMap.GripThreshold)
                            return false;

                        return true;
                    }

                    if (Grip > InputMap.GripReleaseThreshold && !_gripUpperReset && !InputMap.GripRequireReset)
                        return true;

                    if (Grip > InputMap.GripThreshold && _gripLowerReset)
                    {
                        _gripLowerReset = false;
                        return true;
                    }

                    return false;
                }

                return Grip >= InputMap.GripThreshold;
            }

            return GripButton;
        }


        protected void SetButtonState(HVRButtons button, ref HVRButtonState buttonState, bool pressed)
        {
            if (pressed)
            {
                if (!buttonState.Active)
                {
                    buttonState.JustActivated = true;
                    buttonState.Active = true;
                }
            }
            else
            {
                if (buttonState.Active)
                {
                    buttonState.Active = false;
                    buttonState.JustDeactivated = true;
                }
            }

            SetButtonState(Side, button, buttonState);
        }

        protected void ResetButton(ref HVRButtonState buttonState)
        {
            buttonState.JustDeactivated = false;
            buttonState.JustActivated = false;
            buttonState.Value = 0f;
        }

        public static void SetButtonState(HVRHandSide side, HVRButtons button, HVRButtonState state)
        {
            var map = side == HVRHandSide.Right ? _rightButtonStates : _leftButtonStates;
            map[button] = state;
        }

        public static HVRButtonState GetButtonState(HVRHandSide side, HVRButtons button)
        {
            var map = side == HVRHandSide.Right ? _rightButtonStates : _leftButtonStates;
            map.TryGetValue(button, out var state);
            return state;
        }

        public virtual void Vibrate(HapticData haptics)
        {
            if (HVRSettings.Instance.DisableHaptics) return;

            if (haptics != null && haptics.Valid)
            {
                Vibrate(haptics.Amplitude, haptics.Duration, haptics.Frequency);
            }
        }

        /// <summary>
        ///   <para>Sends a haptic impulse to the controller.</para>
        /// </summary>
        /// <param name="amplitude">The normalized (0.0 to 1.0) amplitude value of the haptic impulse to play on the device.</param>
        /// <param name="duration">The duration in seconds that the haptic impulse will play. Only supported on Oculus.</param>
        public virtual void Vibrate(float amplitude, float duration = 1f, float frequency = 1f)
        {
            if (HVRSettings.Instance.DisableHaptics) return;

            if (Device.isValid && Device.TryGetHapticCapabilities(out var hapticCapabilities) && hapticCapabilities.supportsImpulse)
            {
                amplitude = Mathf.Clamp(amplitude, 0f, 1f);
                Device.SendHapticImpulse(0, amplitude, duration);
            }
        }
    }

    public enum InputSDK
    {
        XRInput,
        Oculus,
        SteamVR,
        InputSystem,
        None
    }

    public enum HVRControllerType
    {
        None,
        Oculus,
        WMR,
        Vive,
        Knuckles,
        Cosmos,
        ReverbG2
    }
}