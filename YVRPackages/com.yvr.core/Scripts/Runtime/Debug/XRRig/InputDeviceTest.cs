using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
namespace YVR.Core
{
    public class InputDeviceTest : MonoBehaviour
    {
        public Text inputText;
        public ScrollRect debugInfoScrollRect;

        private InputDevice controllerL;
        private InputDevice controllerR;
        private Dictionary<string, bool> buttonAction = new Dictionary<string, bool>();
        private Dictionary<string, Vector2> buttonVect2Value = new Dictionary<string, Vector2>();
        private Dictionary<string, float> buttonValue = new Dictionary<string, float>();
        private float leftAmplitudeValue = 0.1f;
        private float rightAmplitudeValue = 0.1f;

        void Start()
        {
            controllerL = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            controllerR = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }

        void Update()
        {
            ButtonListener();
        }

        private void ButtonListener()
        {
            //RightControllerState
            GetButtonAction(CommonUsages.primaryButton, XRNode.RightHand);
            GetButtonAction(CommonUsages.secondaryButton, XRNode.RightHand);
            GetButtonAction(CommonUsages.triggerButton, XRNode.RightHand);
            GetButtonAction(CommonUsages.gripButton, XRNode.RightHand);
            GetButtonAction(CommonUsages.menuButton, XRNode.RightHand);
            GetButtonAction(CommonUsages.primary2DAxisClick, XRNode.RightHand);
            GetButtonAction(CommonUsages.primaryTouch, XRNode.RightHand);
            GetButtonAction(CommonUsages.secondaryTouch, XRNode.RightHand);
            GetButtonAction(CommonUsages.primary2DAxisTouch, XRNode.RightHand);
            GetButtonVect2Value(CommonUsages.primary2DAxis, XRNode.RightHand);
            GetButtonValue(CommonUsages.grip, XRNode.RightHand);
            GetButtonValue(CommonUsages.trigger,XRNode.RightHand);
            GetButtonValue(CommonUsages.batteryLevel, XRNode.RightHand);

            //LeftControllerState
            GetButtonAction(CommonUsages.primaryButton, XRNode.LeftHand);
            GetButtonAction(CommonUsages.secondaryButton, XRNode.LeftHand);
            GetButtonAction(CommonUsages.triggerButton, XRNode.LeftHand);
            GetButtonAction(CommonUsages.gripButton, XRNode.LeftHand);
            GetButtonAction(CommonUsages.menuButton, XRNode.LeftHand);
            GetButtonAction(CommonUsages.primary2DAxisClick, XRNode.LeftHand);
            GetButtonAction(CommonUsages.primaryTouch, XRNode.LeftHand);
            GetButtonAction(CommonUsages.secondaryTouch, XRNode.LeftHand);
            GetButtonAction(CommonUsages.primary2DAxisTouch, XRNode.LeftHand);
            GetButtonVect2Value(CommonUsages.primary2DAxis, XRNode.LeftHand);
            GetButtonValue(CommonUsages.grip, XRNode.LeftHand);
            GetButtonValue(CommonUsages.trigger, XRNode.LeftHand);
            GetButtonValue(CommonUsages.batteryLevel, XRNode.LeftHand);
        }

        public void LeftControllerAddAmplitude()
        {
            AddAmplitudeValue(ref leftAmplitudeValue);
            SendLeftHapticImpulse();
        }

        public void LeftControllerSubAmplitude()
        {
            SubAmplitudeValue(ref leftAmplitudeValue);
            SendLeftHapticImpulse();
        }

        public void RightControllerAddAmplitude()
        {
            AddAmplitudeValue(ref rightAmplitudeValue);
            SendRightHapticImpulse();
        }

        public void RightControllerSubAmplitude()
        {
            SubAmplitudeValue(ref rightAmplitudeValue);
            SendRightHapticImpulse();
        }

        private void AddAmplitudeValue(ref float value)
        {
            value += 0.05f;
            value = value > 1 ? 1 : value;
        }

        private void SubAmplitudeValue(ref float value)
        {
            value -= 0.05f;
            value = value < 0 ? 0 : value;
        }

        public void SendRightHapticImpulse()
        {
            SendHapticImpulse(controllerR,rightAmplitudeValue);
        }

        public void SendLeftHapticImpulse()
        {
            SendHapticImpulse(controllerL,leftAmplitudeValue);
        }

        public void ClearText()
        {
            inputText.text = "";
        }

        public void StopLeftControllerHaptic()
        {
            controllerL.StopHaptics();
        }

        public void StopRightControllerHaptic()
        {
            controllerR.StopHaptics();
        }

        private void SendHapticImpulse(InputDevice inputDevice,float value )
        {
            HapticCapabilities capabilities;
            if (inputDevice.TryGetHapticCapabilities(out capabilities))
            {

                if (capabilities.supportsImpulse)
                {
                    inputDevice.SendHapticImpulse(0, value);
                }
            }
        }

        private bool GetButtonAction(InputFeatureUsage<bool> usage, XRNode xRNode)
        {
            bool result = false;
            string key = xRNode.ToString() + usage.name;
            switch (xRNode)
            {
                case XRNode.LeftHand:
                    controllerL.TryGetFeatureValue(usage, out result);
                    break;
                case XRNode.RightHand:
                    controllerR.TryGetFeatureValue(usage, out result);
                    break;
                default:
                    break;
            }
            if (buttonAction.ContainsKey(key))
            {
                if (buttonAction[key] != result)
                {
                    SetInputTextString(string.Format("button name = {0},value = {1}", key, result));
                    buttonAction[key] = result;
                }
            }
            else
            {
                buttonAction[key] = result;
            }
            return result;
        }

        private Vector2 GetButtonVect2Value(InputFeatureUsage<Vector2> usage, XRNode xRNode)
        {
            Vector2 buttonVa = Vector2.zero;
            string key = xRNode.ToString() + usage.name;
            switch (xRNode)
            {
                case XRNode.LeftHand:
                    controllerL.TryGetFeatureValue(usage, out buttonVa);
                    break;
                case XRNode.RightHand:
                    controllerR.TryGetFeatureValue(usage, out buttonVa);
                    break;
                default:
                    break;
            }
            if (buttonVect2Value.ContainsKey(key))
            {
                if (buttonVect2Value[key].x != buttonVa.x || buttonVect2Value[key].y != buttonVa.y)
                {
                    SetInputTextString(string.Format("button value name = {0},value.x = {1}, value.y ={2}", key, buttonVa.x, buttonVa.y));
                    buttonVect2Value[key] = buttonVa;
                }
            }
            else
            {
                buttonVect2Value[key] = buttonVa;
            }
            return buttonVa;
        }

        private float GetButtonValue(InputFeatureUsage<float> usage, XRNode xRNode)
        {
            float buttonVa = 0;
            string key = xRNode.ToString() + usage.name;
            switch (xRNode)
            {
                case XRNode.LeftHand:
                    controllerL.TryGetFeatureValue(usage, out buttonVa);
                    break;
                case XRNode.RightHand:
                    controllerR.TryGetFeatureValue(usage, out buttonVa);
                    break;
                default:
                    break;
            }
            if (buttonValue.ContainsKey(key))
            {
                if (buttonValue[key] != buttonVa)
                {
                    SetInputTextString(string.Format("button value name = {0},value = {1}", key, buttonVa));
                    buttonValue[key] = buttonVa;
                }
            }
            else
            {
                buttonValue[key] = buttonVa;
            }
            return buttonVa;
        }

        private void SetInputTextString(string str)
        {
            inputText.text += str + "\n";
            if (inputText.text.Length > 10000)
            {
                inputText.text = inputText.text.Substring(9000);
            }

            debugInfoScrollRect.verticalNormalizedPosition = 0.0f;
        }
    }
}
