#if HVR_STEAMVR
using System;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Shared;
using Valve.VR;

namespace HurricaneVR.Framework.SteamVR
{
    public class HVRSteamVRController : HVRController
    {
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void UpdateInput()
        {
            SteamVR_Action_Skeleton skeleton;

            if (Side == HVRHandSide.Left)
            {
                JoystickAxis = SteamVR_Actions.hVR_LeftPrimaryAxis.axis;
                JoystickClicked = SteamVR_Actions.hVR_LeftPrimaryAxisClick.state;
                TrackpadAxis = SteamVR_Actions.hVR_LeftSecondaryAxis.axis;
                TrackPadClicked = SteamVR_Actions.hVR_LeftSecondaryAxisClick.state;

                Grip = SteamVR_Actions.hVR_LeftGrip.axis;
                GripForce = SteamVR_Actions.hVR_LeftGripForce.axis;
                Trigger = SteamVR_Actions.hVR_LeftTrigger.axis;

                PrimaryButton = SteamVR_Actions.hVR_LeftPrimary.state;
                SecondaryButton = SteamVR_Actions.hVR_LeftSecondary.state;

                PrimaryTouch = SteamVR_Actions.hVR_LeftPrimaryTouch.state;
                SecondaryTouch = SteamVR_Actions.hVR_LeftSecondaryTouch.state;

                TriggerNearTouch = SteamVR_Actions.hVR_LeftTriggerNearTouch.state;
                ThumbNearTouch = SteamVR_Actions.hVR_LeftThumbNearTouch.state;

                TriggerTouch = SteamVR_Actions.hVR_LeftTriggerTouch.state;
                ThumbTouch = SteamVR_Actions.hVR_LeftThumbTouch.state;
                JoystickTouch = SteamVR_Actions.hVR_LeftPrimaryAxisTouch.state;
                TrackPadTouch = SteamVR_Actions.hVR_LeftSecondaryAxisTouch.state;

                MenuButton = SteamVR_Actions.hVR_LeftMenu.state;


                GripButton = SteamVR_Actions.hVR_LeftGripButton.state;
                TriggerButton = SteamVR_Actions.hVR_LeftTriggerButton.state;

                skeleton = SteamVR_Actions.hVR_left_skeleton;
            }
            else
            {
                JoystickAxis = SteamVR_Actions.hVR_RightPrimaryAxis.axis;
                JoystickClicked = SteamVR_Actions.hVR_RightPrimaryAxisClick.state;
                TrackpadAxis = SteamVR_Actions.hVR_RightSecondaryAxis.axis;
                TrackPadClicked = SteamVR_Actions.hVR_RightSecondaryAxisClick.state;

                Grip = SteamVR_Actions.hVR_RightGrip.axis;
                GripForce = SteamVR_Actions.hVR_RightGripForce.axis;
                Trigger = SteamVR_Actions.hVR_RightTrigger.axis;

                PrimaryButton = SteamVR_Actions.hVR_RightPrimary.state;
                SecondaryButton = SteamVR_Actions.hVR_RightSecondary.state;

                PrimaryTouch = SteamVR_Actions.hVR_RightPrimaryTouch.state;
                SecondaryTouch = SteamVR_Actions.hVR_RightSecondaryTouch.state;

                TriggerNearTouch = SteamVR_Actions.hVR_RightTriggerNearTouch.state;
                ThumbNearTouch = SteamVR_Actions.hVR_RightThumbNearTouch.state;

                TriggerTouch = SteamVR_Actions.hVR_RightTriggerTouch.state;
                ThumbTouch = SteamVR_Actions.hVR_RightThumbTouch.state;
                JoystickTouch = SteamVR_Actions.hVR_RightPrimaryAxisTouch.state;
                TrackPadTouch = SteamVR_Actions.hVR_RightSecondaryAxisTouch.state;

                MenuButton = SteamVR_Actions.hVR_RightMenu.state;


                GripButton = SteamVR_Actions.hVR_RightGripButton.state;
                TriggerButton = SteamVR_Actions.hVR_RightTriggerButton.state;

                skeleton = SteamVR_Actions.hVR_right_skeleton;
            }

            ThumbCurl = skeleton.thumbCurl;
            IndexCurl = skeleton.indexCurl;
            MiddleCurl = skeleton.middleCurl;
            RingCurl = skeleton.ringCurl;
            PinkyCurl = skeleton.pinkyCurl;
        }

        protected override void UpdateFingerCurls()
        {
            FingerCurls[0] = ThumbCurl;
            FingerCurls[1] = IndexCurl;
            FingerCurls[2] = MiddleCurl;
            FingerCurls[3] = RingCurl;
            FingerCurls[4] = PinkyCurl;

            if (FingerSettings)
            {
                base.UpdateFingerCurls();

                if (ControllerType == HVRControllerType.Knuckles)
                {
                    if (!FingerSettings.KnucklesOverrideThumb)
                    {
                        FingerCurls[0] = ThumbCurl;
                    }

                    if (!FingerSettings.KnucklesOverrideIndex)
                    {
                        FingerCurls[1] = IndexCurl;
                    }
                }
                else
                {
                    if (!FingerSettings.OverrideThumb)
                    {
                        FingerCurls[0] = ThumbCurl;
                    }

                    if (!FingerSettings.OverrideIndex)
                    {
                        FingerCurls[1] = IndexCurl;
                    }

                    if (!FingerSettings.OverrideTriggerGrab)
                    {
                        FingerCurls[2] = MiddleCurl;
                        FingerCurls[3] = RingCurl;
                        FingerCurls[4] = PinkyCurl;
                    }
                }
            }
        }

       

        public override void Vibrate(float amplitude, float duration = 1, float frequency = 1)
        {
            if (HVRSettings.Instance.DisableHaptics) return;
            SteamVR_Action_Vibration haptics = (Side == HVRHandSide.Left) ? SteamVR_Actions.hVR_Haptics : SteamVR_Actions.hVR_RightHaptics;
            haptics.Execute(0, duration, frequency, amplitude, Side == HVRHandSide.Left ? SteamVR_Input_Sources.LeftHand : SteamVR_Input_Sources.RightHand);

        }
    }
}
#endif