using System;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Shared;
using HurricaneVR.Framework.Weapons.Guns;
using UnityEngine;

namespace HurricaneVR.Framework.Weapons
{
    
    public class HVRAmmoReleaseAction : HVRInputAction
    {
        public HVRGunBase Gun { get; private set; }


        protected override void Awake()
        {
            base.Awake();
            Gun = GetComponent<HVRGunBase>();
        }


        protected override void CheckInput(HVRController controller)
        {
            var release = false;


            if (controller.ControllerType == HVRControllerType.WMR)
            {
                release = controller.Side == HVRHandSide.Right ? controller.TrackPadLeft.JustActivated : controller.TrackPadRight.JustActivated;
            }
            else if (controller.ControllerType == HVRControllerType.Vive)
            {
                release = HVRInputManager.Instance.RightController.TrackPadDown.JustActivated;
            }
            else
            {
                release = controller.PrimaryButtonState.JustActivated;
            }

            if (release && Gun)
            {
                Gun.ReleaseAmmo();
            }
        }
    }
}