using Assets.HurricaneVR.Framework.Shared.Utilities;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.Framework.Weapons.Guns
{
    public class HVRAutoDropMagazine : HVRSocket
    {
        public HVRSocket MagazineSocket;


        protected override void OnHoverEnter(HVRGrabbable grabbable)
        {
            ForceRelease();
            if (MagazineSocket && IsValid(grabbable) && grabbable.HandGrabbers.Count > 0)
            {
                Debug.Log($"drop");
                MagazineSocket.ForceRelease();
            }
        }

        public override bool IsValid(HVRGrabbable grabbable)
        {
            if (!MagazineSocket || !MagazineSocket.IsGrabbing)
                return false;

            return MagazineSocket.IsValid(grabbable);
        }

        public override bool CanGrab(HVRGrabbable grabbable)
        {
            return false;
        }
    }
}