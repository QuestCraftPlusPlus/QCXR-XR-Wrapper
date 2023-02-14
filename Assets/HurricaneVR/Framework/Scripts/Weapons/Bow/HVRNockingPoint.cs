using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.Framework.Weapons.Bow
{
    public class HVRNockingPoint : HVRSocket
    {
        protected override void Start()
        {
            base.Start();

            ScaleGrabbable = false;
            GrabbableMustBeHeld = true;
            GrabsFromHand = true;
            CanRemoveGrabbable = false;
            ParentDisablesGrab = true;
        }

        protected override void OnGrabbed(HVRGrabArgs args)
        {
            //Debug.Log($"nocked");
            args.Cancel = true;
            Grabbed.Invoke(this, args.Grabbable);
            //ForceRelease();
        }

        protected override void OnReleased(HVRGrabbable grabbable)
        {
        }

        protected override void OnHoverEnter(HVRGrabbable grabbable)
        {
            if (grabbable.LeftHandGrabber) grabbable.LeftHandGrabber.IgnoreNextCollisionCheck = true;
            if (grabbable.RightHandGrabber) grabbable.RightHandGrabber.IgnoreNextCollisionCheck = true;
            base.OnHoverEnter(grabbable);
        }
    }
}