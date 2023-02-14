using System;
using UnityEngine;

namespace HurricaneVR.Framework.Shared
{
    [CreateAssetMenu(menuName = "HurricaneVR/Input Settings", fileName = "InputSettings")]
    public class HVRInputSettings : ScriptableObject
    {
        [Header("XR Input Settings (Oculus / WMR Plugins only) - SteamVR / OpenXR can ignore.")]
        public InputAxes JoystickAxis;
        public InputAxes TrackPadAxis;
        public HVRXRInputFeatures Primary = HVRXRInputFeatures.PrimaryButton;
        public HVRXRInputFeatures Secondary = HVRXRInputFeatures.SecondaryButton;
        public HVRXRInputFeatures Menu = HVRXRInputFeatures.MenuButton;
        public HVRXRInputFeatures PrimaryTouch = HVRXRInputFeatures.PrimaryTouch;
        public HVRXRInputFeatures SecondaryTouch = HVRXRInputFeatures.SecondaryTouch;
        public HVRXRInputFeatures JoystickButton = HVRXRInputFeatures.Primary2DAxisClick;
        public HVRXRInputFeatures TrackPadButton = HVRXRInputFeatures.Secondary2DAxisClick;

        public HVRXRInputFeatures JoystickTouch = HVRXRInputFeatures.Primary2DAxisTouch;
        public HVRXRInputFeatures TrackPadTouch = HVRXRInputFeatures.Secondary2DAxisTouch;

        [Header("Should Grip / Trigger use Analog checks or button presses from SteamVR / OpenXR")]

        [Tooltip("If true grip clicked will use the grip 0-1 analog value compared to the GripThreshold, otherwise the grip pressed SteamVR / OpenXR binding")]
        public bool GripUseAnalog = true;

        [Tooltip("If true trigger clicked check will use the trigger 0-1 analog value compared to the TriggerThreshold, otherwise the trigger pressed SteamVR / OpenXR binding will be used.")]
        public bool TriggerUseAnalog = true;

        [Header("Grip Analog Activation")]
        
        public float GripThreshold = .7f;

        [Tooltip("If false the Threshold only will be used to determine Trigger activation / deactivation")]
        public bool GripUseReleaseThreshold;

        [Tooltip("If lower than the Threshold, the value must fall below this value to activate. " +
                 "When larger than the Threshold, the value must go above and then back below this value to deactivate.")]
        public float GripReleaseThreshold = .7f;


        [Tooltip("Only used when the release threshold is greater than the threshold, if true the value must drop below the Threshold before it can be considered active again" +
                 "Otherwise going back over the Release Threshold will activate the button.")]
        public bool GripRequireReset;

        [Header("Trigger Analog Activation")]

        public float TriggerThreshold = .7f;

        [Tooltip("If false the Threshold only will be used to determine Trigger activation / deactivation")]
        public bool TriggerUseReleaseThreshold;

        [Tooltip("If lower than the Threshold, the value must fall below this value to activate. " +
                 "When larger than the Threshold, the value must go above and then back below this value to deactivate.")]
        public float TriggerReleaseThreshold = .7f;

        [Tooltip("Only used when the release threshold is greater than the threshold, if true the value must drop below the Threshold before it can be considered active again" +
                 "Otherwise going back over the Release Threshold will activate the button.")]
        public bool TriggerRequireReset;

        [Header("Track Pad Click Thresholds")]

        public float Axis2DUpThreshold = .7f;
        public float Axis2DDownThreshold = .7f;
        public float Axis2DLeftThreshold = .7f;
        public float Axis2DRighThreshold = .7f;
    }

    [Serializable]
    public enum InputAxes
    {
        None,
        Primary2DAxis = 1,
        Secondary2DAxis = 2,
    };


}