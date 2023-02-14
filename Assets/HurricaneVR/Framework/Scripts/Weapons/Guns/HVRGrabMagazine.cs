using System.Collections;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.Framework.Weapons.Guns
{

    public class HVRGrabMagazine : HVRHandGrabEvent
    {
        public float GrabDelay = .05f;
        public HVRSocket MagSocket;

        protected WaitForSeconds Timeout;

        protected override void Awake()
        {
            base.Awake();
            Timeout = new WaitForSeconds(GrabDelay);
        }

        protected override bool CheckEnableGrab()
        {
            return MagSocket && MagSocket.IsGrabbing;
        }

        protected override void OnHandGrabbed(HVRHandGrabber hand, HVRGrabbable arg1)
        {
            base.OnHandGrabbed(hand, arg1);
            if (MagSocket && MagSocket.GrabbedTarget)
            {
                var ammo = MagSocket.GrabbedTarget;
                MagSocket.ForceRelease();
                StartCoroutine(GrabRoutine(ammo, hand));
            }
        }

        protected IEnumerator GrabRoutine(HVRGrabbable ammo, HVRHandGrabber hand)
        {
            //yield return Timeout;
            yield return new WaitForSeconds(GrabDelay);
            if (ammo && !ammo.IsBeingHeld && !hand.GrabbedTarget)
            {
                hand.TryGrab(ammo, true);
            }
        }
    }
}