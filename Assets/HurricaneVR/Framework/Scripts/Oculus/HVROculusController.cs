#if HVR_OCULUS
using System.Collections;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.XR;

namespace HurricaneVR.Framework.Oculus
{
    public class HVROculusController : HVRController
    {
        public OVRInput.Controller ControllerMask => Side == HVRHandSide.Left ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;
        public bool OVRHaptics { get; set; }

        public static void UpdateOVRInput()
        {
            OVRInput.Update();
        }

        protected override void UpdateInput()
        {
            JoystickAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, ControllerMask);
            Grip = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, ControllerMask);
            Trigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, ControllerMask);

            PrimaryButton = OVRInput.Get(OVRInput.Button.One, ControllerMask);
            SecondaryButton = OVRInput.Get(OVRInput.Button.Two, ControllerMask);

            PrimaryTouch = OVRInput.Get(OVRInput.Touch.One, ControllerMask);
            SecondaryTouch = OVRInput.Get(OVRInput.Touch.Two, ControllerMask);

            TriggerNearTouch = OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, ControllerMask);
            ThumbNearTouch = OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, ControllerMask);

            TriggerTouch = OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, ControllerMask);
            ThumbTouch = OVRInput.Get(OVRInput.Touch.PrimaryThumbRest, ControllerMask);
            JoystickTouch = OVRInput.Get(OVRInput.Touch.PrimaryThumbstick, ControllerMask);

            MenuButton = OVRInput.Get(OVRInput.Button.Start, ControllerMask);
            JoystickClicked = OVRInput.Get(OVRInput.Button.PrimaryThumbstick, ControllerMask);

            GripButton = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, ControllerMask);
            TriggerButton = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, ControllerMask);
        }

        protected override void CheckButtonState(HVRButtons button, ref HVRButtonState buttonState)
        {
            ResetButton(ref buttonState);

            switch (button)
            {
                case HVRButtons.Grip:
                    buttonState.Value = Grip;
                    if (InputMap.GripUseAnalog)
                        SetButtonState(button, ref buttonState, Grip >= InputMap.GripThreshold);
                    else
                        SetButtonState(button, ref buttonState, GripButton);

                    break;
                case HVRButtons.Trigger:
                    buttonState.Value = Trigger;
                    if (InputMap.TriggerUseAnalog)
                        SetButtonState(button, ref buttonState, Trigger >= InputMap.TriggerThreshold);
                    else
                        SetButtonState(button, ref buttonState, TriggerButton);
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
                    break;
                case HVRButtons.JoystickTouch:
                    SetButtonState(button, ref buttonState, JoystickTouch);
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
            }
        }

        private Coroutine _vibrateRoutine;

        public override void Vibrate(float amplitude, float duration = 1, float frequency = 1)
        {
            if (HVRSettings.Instance.DisableHaptics) return;
            if (OVRHaptics)
            {
                if (_vibrateRoutine != null)
                    StopCoroutine(_vibrateRoutine);
                _vibrateRoutine = StartCoroutine(VibrateRoutine(amplitude, duration, frequency));
            }
            else
            {
                base.Vibrate(amplitude, duration, frequency);
            }
        }

        private IEnumerator VibrateRoutine(float amplitude, float duration = 1, float frequency = 1)
        {
            OVRInput.SetControllerVibration(frequency, amplitude, ControllerMask);
            yield return new WaitForSeconds(duration);
            OVRInput.SetControllerVibration(0, 0, ControllerMask);
        }
    }
}
#endif
