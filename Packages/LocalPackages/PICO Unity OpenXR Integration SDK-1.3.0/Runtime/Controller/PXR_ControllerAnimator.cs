/*******************************************************************************
Copyright © 2015-2022 PICO Technology Co., Ltd.All rights reserved.  

NOTICE：All information contained herein is, and remains the property of 
PICO Technology Co., Ltd. The intellectual and technical concepts 
contained herein are proprietary to PICO Technology Co., Ltd. and may be 
covered by patents, patents in process, and are protected by trade secret or 
copyright law. Dissemination of this information or reproduction of this 
material is strictly forbidden unless prior written permission is obtained from
PICO Technology Co., Ltd. 
*******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Unity.XR.PXR
{
    public class PXR_ControllerAnimator : MonoBehaviour
    {
        private Animator controllerAnimator;
        public Transform primary2DAxisTran;
        public Transform gripTran;
        public Transform triggerTran;
        public bool controllerL=false;
        private Vector2 axis2D = Vector2.zero;
        private bool primaryButton;
        private bool secondaryButton;
        private bool menuButton;
        private float grip;
        private float trigger;
        private Vector3 originalGrip;
        private Vector3 originalTrigger;
        private Vector3 originalJoystick;

        public const string primary = "IsPrimaryDown";
        public const string secondary = "IsSecondaryDown";
        public const string media = "IsMediaDown";
        public const string menu = "IsMenuDown";

        void Start()
        {
            controllerAnimator = GetComponent<Animator>();
            originalGrip = gripTran.localEulerAngles;
            originalJoystick = primary2DAxisTran.localEulerAngles;
            originalTrigger = triggerTran.localEulerAngles;
        }

        void Update()
        {
            InputDevices.GetDeviceAtXRNode(controllerL
                ? XRNode.LeftHand
                : XRNode.RightHand).TryGetFeatureValue(CommonUsages.primary2DAxis, out axis2D);
            InputDevices.GetDeviceAtXRNode(controllerL
                ? XRNode.LeftHand
                : XRNode.RightHand).TryGetFeatureValue(CommonUsages.grip, out grip);
            InputDevices.GetDeviceAtXRNode(controllerL
                ? XRNode.LeftHand
                : XRNode.RightHand).TryGetFeatureValue(CommonUsages.trigger, out trigger);
            InputDevices.GetDeviceAtXRNode(controllerL
                ? XRNode.LeftHand
                : XRNode.RightHand).TryGetFeatureValue(CommonUsages.primaryButton, out primaryButton);
            InputDevices.GetDeviceAtXRNode(controllerL
                ? XRNode.LeftHand
                : XRNode.RightHand).TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButton);
            InputDevices.GetDeviceAtXRNode(controllerL
                ? XRNode.LeftHand
                : XRNode.RightHand).TryGetFeatureValue(CommonUsages.menuButton, out menuButton);

            float x = Mathf.Clamp(axis2D.x * 10f, -10f, 10f);
            float z = Mathf.Clamp(axis2D.y * 10f, -10f, 10f);
            if (primary2DAxisTran != null)
            {
                if (controllerL)
                {
                    primary2DAxisTran.localEulerAngles = new Vector3(-z, 0, x) + originalJoystick;
                }
                else
                {
                    primary2DAxisTran.localEulerAngles = new Vector3(-z, 0, -x) + originalJoystick;
                }
            }

            trigger *= -15;
            if (triggerTran != null)
                triggerTran.localEulerAngles = new Vector3(trigger, 0f, 0f) + originalTrigger;
            grip *= 12;
            if (gripTran != null)
                gripTran.localEulerAngles = new Vector3(0f, grip, 0f) + originalGrip;

            if (controllerAnimator != null)
            {
                controllerAnimator.SetBool(primary, primaryButton);
                controllerAnimator.SetBool(secondary, secondaryButton);


                if (controllerL)
                    controllerAnimator.SetBool(menu, menuButton);
                else 
                    controllerAnimator.SetBool(media, menuButton);
            }
        }
    }
}