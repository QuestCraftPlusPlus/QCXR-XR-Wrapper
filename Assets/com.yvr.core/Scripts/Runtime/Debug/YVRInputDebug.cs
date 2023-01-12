using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.UI;

namespace YVR.Core
{
    [ExcludeFromDocs]
    public class YVRInputDebug : YVRDebugHub
    {
        public Text stateInfo = null;
        public Text logInfo = null;
        public ScrollRect debugInfoScrollRect = null;

        private float leftVibrationFrequency = 0.5f;
        private float leftVibrationAmplitude = 0.5f;
        private float rightVibrationFrequency = 0.5f;
        private float rightVibrationAmplitude = 0.5f;

        private bool leftControllerShouldVibrate = false;
        private bool rightControllerShouldVibrate = false;

        protected override void UpdateDebugInfo()
        {
            base.UpdateDebugInfo();
            DebugRawLeftController();
            UpdateControllerValibration();
        }

        private void DebugRawLeftController()
        {
            stateInfo.text = "Active Controller is " + YVRInput.GetActiveController() + "\n" +
                             "Left Vibrate Frequency is " + leftVibrationFrequency + " , Amplitude is " + leftVibrationAmplitude + "\n" +
                             "Right Vibrate Frequency is " + rightVibrationFrequency + " , Amplitude is " + rightVibrationAmplitude + "\n" +
                             "Left Controller should vibrate " + leftControllerShouldVibrate + "\n" +
                             "Right Controller should vibrate " + rightControllerShouldVibrate + "\n" +
                             "\n" +
                             "Controller connected -- Left: " + YVRInput.IsControllerConnected(ControllerType.LeftTouch) + "  ,Right: " + YVRInput.IsControllerConnected(ControllerType.RightTouch) + "\n" +
                             "Controller position Tracked -- left " + YVRControllerRig.GetPositionTracked(ControllerType.LeftTouch) + " , Right: " + YVRControllerRig.GetPositionTracked(ControllerType.RightTouch) + "\n" +
                             "Controller orientation Tracked -- left " + YVRControllerRig.GetOrientationTracked(ControllerType.LeftTouch) + " , Right: " + YVRControllerRig.GetOrientationTracked(ControllerType.RightTouch) + "\n" +
                             "Controller battery is " + YVRInput.GetControllerBatteryPercentRemaining(ControllerType.LeftTouch) + " ,Right: " + YVRInput.GetControllerBatteryPercentRemaining(ControllerType.RightTouch) + "\n" +
                             "Controller is charging " + YVRInput.GetControllerIsCharging(ControllerType.LeftTouch).ToString() + " ,Right: " + YVRInput.GetControllerIsCharging(ControllerType.RightTouch).ToString() + "\n" +
                             "Raw Value Index Trigger: " + YVRInput.Get(YVRInput.RawAxis1D.LIndexTrigger) + " ,Right: " + YVRInput.Get(YVRInput.RawAxis1D.RIndexTrigger) + "\n" +
                             "Raw Value Thumbstick: " + YVRInput.Get(YVRInput.RawAxis2D.LThumbstick) + ",Right: " + YVRInput.Get(YVRInput.RawAxis2D.RThumbstick) + "\n" +
                             "Raw Button Left Thumbstick Up: " + YVRInput.Get(YVRInput.RawButton.LThumbstickUp) + " ,Right: " + YVRInput.Get(YVRInput.RawButton.RThumbstickUp) + "\n" +
                             "Raw Button Left Thumbstick Down: " + YVRInput.Get(YVRInput.RawButton.LThumbstickDown) + " ,Right: " + YVRInput.Get(YVRInput.RawButton.RThumbstickDown) + "\n" +
                             "Raw Button Left Thumbstick Left: " + YVRInput.Get(YVRInput.RawButton.LThumbstickLeft) + " ,Right: " + YVRInput.Get(YVRInput.RawButton.RThumbstickLeft) + "\n" +
                             "Raw Button Left Thumbstick Right: " + YVRInput.Get(YVRInput.RawButton.LThumbstickRight) + " ,Right: " + YVRInput.Get(YVRInput.RawButton.RThumbstickRight) + "\n" +
                             "Clicked Controller is " + YVRInput.GetClickedController().ToString() + "\n" +
                             "Touched Controller is " + YVRInput.GetTouchedController().ToString();


            // Debug Info for Left YVRInput.RawButton
            if (YVRInput.GetDown(YVRInput.RawButton.X)) UpdateDebugInfoScroll("Raw Button X Click Down");
            if (YVRInput.GetUp(YVRInput.RawButton.X)) UpdateDebugInfoScroll("Raw Button X Click Up");
            if (YVRInput.GetDown(YVRInput.RawButton.Y)) UpdateDebugInfoScroll("Raw Button Y Click Down");
            if (YVRInput.GetUp(YVRInput.RawButton.Y)) UpdateDebugInfoScroll("Raw Button Y Click Up");
            if (YVRInput.GetDown(YVRInput.RawButton.A)) UpdateDebugInfoScroll("Raw Button A Click Down");
            if (YVRInput.GetUp(YVRInput.RawButton.A)) UpdateDebugInfoScroll("Raw Button A Click Up");
            if (YVRInput.GetDown(YVRInput.RawButton.B)) UpdateDebugInfoScroll("Raw Button B Click Down");
            if (YVRInput.GetUp(YVRInput.RawButton.B)) UpdateDebugInfoScroll("Raw Button B Click Up");
            if (YVRInput.GetDown(YVRInput.RawButton.Start)) UpdateDebugInfoScroll("Raw Button Start Click Down");
            if (YVRInput.GetUp(YVRInput.RawButton.Start)) UpdateDebugInfoScroll("Raw Button Start Click Up");
            if (YVRInput.GetDown(YVRInput.RawButton.Reserved)) UpdateDebugInfoScroll("Raw Button Home Click Down");
            if (YVRInput.GetUp(YVRInput.RawButton.Reserved)) UpdateDebugInfoScroll("Raw Button Home Click Up");

            // Debug Info for Left YVRInput.RawTouch
            if (YVRInput.GetDown(YVRInput.RawTouch.X)) UpdateDebugInfoScroll("Raw Touch X Touch Down");
            if (YVRInput.GetUp(YVRInput.RawTouch.X)) UpdateDebugInfoScroll("Raw Touch X Touch Up");
            if (YVRInput.GetDown(YVRInput.RawTouch.Y)) UpdateDebugInfoScroll("Raw Touch Y Touch Down");
            if (YVRInput.GetUp(YVRInput.RawTouch.Y)) UpdateDebugInfoScroll("Raw Touch Y Touch Up");
            if (YVRInput.GetDown(YVRInput.RawTouch.A)) UpdateDebugInfoScroll("Raw Touch A Touch Down");
            if (YVRInput.GetUp(YVRInput.RawTouch.A)) UpdateDebugInfoScroll("Raw Touch A Touch Up");
            if (YVRInput.GetDown(YVRInput.RawTouch.B)) UpdateDebugInfoScroll("Raw Touch B Touch Down");
            if (YVRInput.GetUp(YVRInput.RawTouch.B)) UpdateDebugInfoScroll("Raw Touch B Touch Up");

            if (YVRInput.GetDown(YVRInput.RawTouch.LIndexTrigger)) UpdateDebugInfoScroll("Raw Touch Left Index Trigger Down");
            if (YVRInput.GetUp(YVRInput.RawTouch.LIndexTrigger)) UpdateDebugInfoScroll("Raw Touch Left Index Trigger Up");
            if (YVRInput.GetDown(YVRInput.RawTouch.RIndexTrigger)) UpdateDebugInfoScroll("Raw Touch Right Index Trigger Down");
            if (YVRInput.GetUp(YVRInput.RawTouch.RIndexTrigger)) UpdateDebugInfoScroll("Raw Touch Right Index Trigger up");

            if (YVRInput.GetDown(YVRInput.RawTouch.LThumbstick)) UpdateDebugInfoScroll("Raw Touch Left Thumbstick Down");
            if (YVRInput.GetUp(YVRInput.RawTouch.LThumbstick)) UpdateDebugInfoScroll("Raw Touch Left Thumbstick Up");
            if (YVRInput.GetDown(YVRInput.RawTouch.RThumbstick)) UpdateDebugInfoScroll("Raw Touch Right Thumbstick Down");
            if (YVRInput.GetUp(YVRInput.RawTouch.RThumbstick)) UpdateDebugInfoScroll("Raw Touch Right Thumbstick Up");

            // Debug Info for Left Index Trigger
            if (YVRInput.GetDown(YVRInput.RawButton.LIndexTrigger)) UpdateDebugInfoScroll("Raw Button Left Index Trigger Down");
            if (YVRInput.GetUp(YVRInput.RawButton.LIndexTrigger)) UpdateDebugInfoScroll("Raw Button Left Index Trigger Up");
            if (YVRInput.GetDown(YVRInput.RawButton.RIndexTrigger)) UpdateDebugInfoScroll("Raw Button Right Index Trigger Down");
            if (YVRInput.GetUp(YVRInput.RawButton.RIndexTrigger)) UpdateDebugInfoScroll("Raw Button Right Index Trigger Up");

            // Debug Info for Left Index Trigger
            if (YVRInput.GetDown(YVRInput.RawButton.LHandTrigger)) UpdateDebugInfoScroll("Raw Button Left Hand Trigger Down");
            if (YVRInput.GetUp(YVRInput.RawButton.LHandTrigger)) UpdateDebugInfoScroll("Raw Button Left Hand Trigger Up");
            if (YVRInput.GetDown(YVRInput.RawButton.RHandTrigger)) UpdateDebugInfoScroll("Raw Button Right Hand Trigger Down");
            if (YVRInput.GetUp(YVRInput.RawButton.RHandTrigger)) UpdateDebugInfoScroll("Raw Button Right Hand Trigger Up");

            // Debug Info for Left ThumbStick
            if (YVRInput.GetDown(YVRInput.RawButton.LThumbstick)) UpdateDebugInfoScroll("Raw Button Left Thumbstick Down");
            if (YVRInput.GetUp(YVRInput.RawButton.LThumbstick)) UpdateDebugInfoScroll("Raw Button Left Thumbstick Up");
            if (YVRInput.GetDown(YVRInput.RawTouch.LThumbstick)) UpdateDebugInfoScroll("Raw Touch Left Thumbstick Down");
            if (YVRInput.GetUp(YVRInput.RawTouch.LThumbstick)) UpdateDebugInfoScroll("Raw Touch Left Thumbstick Up");
            if (YVRInput.GetDown(YVRInput.RawButton.LThumbstickUp)) UpdateDebugInfoScroll("Raw Button Left ThumbstickUp Down");
            if (YVRInput.GetUp(YVRInput.RawButton.LThumbstickUp)) UpdateDebugInfoScroll("Raw Button Left ThumbstickUp Up");
            if (YVRInput.GetDown(YVRInput.RawButton.LThumbstickDown)) UpdateDebugInfoScroll("Raw Button Left ThumbstickDown Down");
            if (YVRInput.GetUp(YVRInput.RawButton.LThumbstickDown)) UpdateDebugInfoScroll("Raw Button Left ThumbstickDown Up");
            if (YVRInput.GetDown(YVRInput.RawButton.LThumbstickLeft)) UpdateDebugInfoScroll("Raw Button Left ThumbstickLeft Down");
            if (YVRInput.GetUp(YVRInput.RawButton.LThumbstickLeft)) UpdateDebugInfoScroll("Raw Button Left ThumbstickLeft Up");
            if (YVRInput.GetDown(YVRInput.RawButton.LThumbstickRight)) UpdateDebugInfoScroll("Raw Button Left ThumbstickRight Down");
            if (YVRInput.GetUp(YVRInput.RawButton.LThumbstickRight)) UpdateDebugInfoScroll("Raw Button Left ThumbstickRight Up");

            // Debug Info for Right ThumbStick
            if (YVRInput.GetDown(YVRInput.RawButton.RThumbstick)) UpdateDebugInfoScroll("Raw Button Right Thumbstick Down");
            if (YVRInput.GetUp(YVRInput.RawButton.RThumbstick)) UpdateDebugInfoScroll("Raw Button Right Thumbstick Up");
            if (YVRInput.GetDown(YVRInput.RawButton.RThumbstickUp)) UpdateDebugInfoScroll("Raw Button Right ThumbstickUp Down");
            if (YVRInput.GetUp(YVRInput.RawButton.RThumbstickUp)) UpdateDebugInfoScroll("Raw Button Right ThumbstickUp Up");
            if (YVRInput.GetDown(YVRInput.RawButton.RThumbstickDown)) UpdateDebugInfoScroll("Raw Button Right ThumbstickDown Down");
            if (YVRInput.GetUp(YVRInput.RawButton.RThumbstickDown)) UpdateDebugInfoScroll("Raw Button Right ThumbstickDown Up");
            if (YVRInput.GetDown(YVRInput.RawButton.RThumbstickRight)) UpdateDebugInfoScroll("Raw Button Right ThumbstickLeft Down");
            if (YVRInput.GetUp(YVRInput.RawButton.RThumbstickRight)) UpdateDebugInfoScroll("Raw Button Right ThumbstickLeft Up");
            if (YVRInput.GetDown(YVRInput.RawButton.RThumbstickRight)) UpdateDebugInfoScroll("Raw Button Right ThumbstickRight Down");
            if (YVRInput.GetUp(YVRInput.RawButton.RThumbstickRight)) UpdateDebugInfoScroll("Raw Button Right ThumbstickRight Up");
        }

        private void UpdateControllerValibration()
        {
            if (YVRInput.Get(YVRInput.RawButton.X))
            {
                leftVibrationAmplitude += YVRInput.Get(YVRInput.RawAxis2D.LThumbstick).x * 0.05f;
                leftVibrationFrequency += YVRInput.Get(YVRInput.RawAxis2D.LThumbstick).y * 0.05f;
                if (YVRInput.GetDown(YVRInput.RawButton.LIndexTrigger))
                {
                    leftControllerShouldVibrate = true;
                    YVRInput.SetControllerVibration(leftVibrationFrequency, leftVibrationAmplitude, ControllerType.LeftTouch);
                }

                if (YVRInput.GetUp(YVRInput.RawButton.LIndexTrigger))
                {
                    leftControllerShouldVibrate = false;
                    YVRInput.SetControllerVibration(0, 0, ControllerType.LeftTouch);
                }
            }

            if (YVRInput.Get(YVRInput.RawButton.A))
            {
                rightVibrationAmplitude += YVRInput.Get(YVRInput.RawAxis2D.RThumbstick).x * 0.05f;
                rightVibrationFrequency += YVRInput.Get(YVRInput.RawAxis2D.RThumbstick).y * 0.05f;

                if (YVRInput.GetDown(YVRInput.RawButton.RIndexTrigger))
                {
                    rightControllerShouldVibrate = true;
                    YVRInput.SetControllerVibration(rightVibrationFrequency, rightVibrationAmplitude, ControllerType.RightTouch);
                }

                if (YVRInput.GetUp(YVRInput.RawButton.RIndexTrigger))
                {
                    rightControllerShouldVibrate = false;
                    YVRInput.SetControllerVibration(0, 0, ControllerType.RightTouch);
                }
            }
        }

        public void AddVibrateFrequency(float step)
        {
            leftVibrationFrequency += step;
        }

        public void MinusVibrateFrequency(float step)
        {
            leftVibrationFrequency -= step;
        }

        public void AddVibrateAmplitude(float step)
        {
            leftVibrationAmplitude += step;
        }

        public void MinusVibrateAmplitude(float step)
        {
            leftVibrationAmplitude -= step;
        }

        public void LeftControllerStartVibrating()
        {
            leftControllerShouldVibrate = true;
            YVRInput.SetControllerVibration(leftVibrationFrequency, leftVibrationAmplitude, ControllerType.LeftTouch);
        }

        public void LeftControllerStopVibrating()
        {
            leftControllerShouldVibrate = false;
            YVRInput.SetControllerVibration(0, 0, ControllerType.LeftTouch);
        }

        public void RightControllerStartVibrating()
        {
            rightControllerShouldVibrate = true;
            YVRInput.SetControllerVibration(leftVibrationFrequency, leftVibrationAmplitude, ControllerType.RightTouch);
        }

        public void RightControllerStopVibrating()
        {
            rightControllerShouldVibrate = false;
            YVRInput.SetControllerVibration(0, 0, ControllerType.RightTouch);
        }

        private void UpdateDebugInfoScroll(string appendDebugInfo)
        {
            logInfo.text += appendDebugInfo + "\n";

            // Unity use one quad to render one character, when the text is too long, the vertices number will over 65000 which is the limit on vertices number
            if (logInfo.text.Length > 10000)
                logInfo.text = logInfo.text.Substring(9000);

            // Making scroll rect always focuses at the bottom of the scroller view
            debugInfoScrollRect.verticalNormalizedPosition = 0.0f;
        }
    }
}