using System;
using HurricaneVR.Framework.ControllerInput;
using UnityEngine;

namespace HurricaneVR.Framework.Shared
{
    //https://www.cloudwalkingames.com/en/scriptable-objects/fingersettings
    [CreateAssetMenu(menuName = "HurricaneVR/Finger Settings", fileName = "FingerSettings")]
    public class HVRFingerSettings : ScriptableObject
    {
        [Header("Non Knuckles SteamVR Finger Curl Overrides. Enable to use below weights when using SteamVR.")]
        public bool OverrideThumb = true;
        public bool OverrideIndex = true;
        public bool OverrideTriggerGrab = true; //trigger click controllers bend last 3 fingers on trigger pull

        [Header("Knuckles Overrides")]

        [Tooltip("Knuckles default thumb behaviour is mostly bent thumb on all capacitive buttons, and even joystick movement. Enable to override with the below touch weights.")]
        public bool KnucklesOverrideThumb = true;

        [Tooltip("Knuckles Default trigger touch pulls to .83, trigger completes the finger curl to .83 to 1. Enable to override with the below touch weights")]
        public bool KnucklesOverrideIndex;

        [Tooltip("Since OpenXR doesn't support finger tracking yet, you might want to set this to true until they implement it")]
        public bool KnucklesOverrideGripFingers;

        [Header("Per Button Per Finger Weights")]

        public HVRTouchWeight JoystickTouchWeight;
        public HVRTouchWeight TrackpadTouchWeight;
        public HVRTouchWeight PrimaryTouchWeight;
        public HVRTouchWeight SecondaryTouchWeight;
        public HVRTouchWeight TriggerTouchWeight;
        public HVRTouchWeight GripWeight;
        public HVRTouchWeight TriggerWeight;

        public void Reset()
        {
            OverrideThumb = true;
            OverrideIndex = true;

            GripWeight = new HVRTouchWeight(0f, 0f, 1f, 1f, 1f);
            TriggerWeight = new HVRTouchWeight(0f, 1f, 0f, 0f, 0f);
            PrimaryTouchWeight = SecondaryTouchWeight = TrackpadTouchWeight = new HVRTouchWeight(1f, 0f, 0f, 0f, 0f);
            JoystickTouchWeight = new HVRTouchWeight(0f, 0f, 0f, 0f, 0f);
            TriggerTouchWeight = new HVRTouchWeight(0f, .25f, 0f, 0f, 0f);
        }

        public float ThumbTotal => JoystickTouchWeight.Thumb + TrackpadTouchWeight.Thumb + PrimaryTouchWeight.Thumb + SecondaryTouchWeight.Thumb +
                                   TriggerTouchWeight.Thumb + GripWeight.Thumb + TriggerWeight.Thumb;

        public float IndexTotal => JoystickTouchWeight.Index + TrackpadTouchWeight.Index + PrimaryTouchWeight.Index + SecondaryTouchWeight.Index +
                                   TriggerTouchWeight.Index + GripWeight.Index + TriggerWeight.Index;

        public float MiddleTotal => JoystickTouchWeight.Middle + TrackpadTouchWeight.Middle + PrimaryTouchWeight.Middle + SecondaryTouchWeight.Middle +
                                   TriggerTouchWeight.Middle + GripWeight.Middle + TriggerWeight.Middle;

        public float RingTotal => JoystickTouchWeight.Ring + TrackpadTouchWeight.Ring + PrimaryTouchWeight.Ring + SecondaryTouchWeight.Ring +
                                   TriggerTouchWeight.Ring + GripWeight.Ring + TriggerWeight.Ring;
        public float PinkyTotal => JoystickTouchWeight.Pinky + TrackpadTouchWeight.Pinky + PrimaryTouchWeight.Pinky + SecondaryTouchWeight.Pinky +
                                   TriggerTouchWeight.Pinky + GripWeight.Pinky + TriggerWeight.Pinky;

        public void Evaluate(float[] curls, float grip, float trigger, bool triggerTouch, bool primaryTouch, bool secondaryTouch, bool trackpadTouch, bool joystickTouch, bool knuckles, bool isOpenXR)
        {
            var joystick = joystickTouch ? 1f : 0f;
            var trackPad = trackpadTouch ? 1f : 0f;
            var primary = primaryTouch ? 1f : 0f;
            var secondary = secondaryTouch ? 1f : 0f;
            var triggerT = triggerTouch ? 1f : 0f;

            curls[0] = JoystickTouchWeight.Thumb * joystick +
                          TrackpadTouchWeight.Thumb * trackPad +
                          PrimaryTouchWeight.Thumb * primary +
                          SecondaryTouchWeight.Thumb * secondary +
                          TriggerTouchWeight.Thumb * triggerT +
                          GripWeight.Thumb * grip +
                          TriggerWeight.Thumb * trigger;

            curls[1] = JoystickTouchWeight.Index * joystick +
                          TrackpadTouchWeight.Index * trackPad +
                          PrimaryTouchWeight.Index * primary +
                          SecondaryTouchWeight.Index * secondary +
                          TriggerTouchWeight.Index * triggerT +
                          GripWeight.Index * grip +
                          TriggerWeight.Index * trigger;

            //until openxr has index finger curls supplied (if ever..)
            if (!knuckles || (KnucklesOverrideGripFingers || isOpenXR))
            {
                curls[2] = JoystickTouchWeight.Middle * joystick +
                           TrackpadTouchWeight.Middle * trackPad +
                           PrimaryTouchWeight.Middle * primary +
                           SecondaryTouchWeight.Middle * secondary +
                           TriggerTouchWeight.Middle * triggerT +
                           GripWeight.Middle * grip +
                           TriggerWeight.Middle * trigger;

                curls[3] = JoystickTouchWeight.Ring * joystick +
                           TrackpadTouchWeight.Ring * trackPad +
                           PrimaryTouchWeight.Ring * primary +
                           SecondaryTouchWeight.Ring * secondary +
                           TriggerTouchWeight.Ring * triggerT +
                           GripWeight.Ring * grip +
                           TriggerWeight.Ring * trigger;

                curls[4] = JoystickTouchWeight.Pinky * joystick +
                           TrackpadTouchWeight.Pinky * trackPad +
                           PrimaryTouchWeight.Pinky * primary +
                           SecondaryTouchWeight.Pinky * secondary +
                           TriggerTouchWeight.Pinky * triggerT +
                           GripWeight.Pinky * grip +
                           TriggerWeight.Pinky * trigger;
            }

            //if (ThumbTotal > 0) curls[0] /= ThumbTotal;
            //if (IndexTotal > 0) curls[1] /= IndexTotal;
            //if (MiddleTotal > 0) curls[2] /= MiddleTotal;
            //if (RingTotal > 0) curls[3] /= RingTotal;
            //if (PinkyTotal > 0) curls[4] /= PinkyTotal;


            curls[0] = Mathf.Clamp01(curls[0]);
            curls[1] = Mathf.Clamp01(curls[1]);
            curls[2] = Mathf.Clamp01(curls[2]); 
            curls[3] = Mathf.Clamp01(curls[3]);
            curls[4] = Mathf.Clamp01(curls[4]);
        }
    }

    [Serializable]
    public struct HVRTouchWeight
    {
        [Range(0, 1)]
        public float Thumb;
        [Range(0, 1)]
        public float Index;
        [Range(0, 1)]
        public float Middle;
        [Range(0, 1)]
        public float Ring;
        [Range(0, 1)]
        public float Pinky;

        public HVRTouchWeight(float t, float i, float m, float r, float p)
        {
            Thumb = t;
            Index = i;
            Middle = m;
            Ring = r;
            Pinky = p;
        }
    }
}