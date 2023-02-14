using System.Collections;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;

namespace HurricaneVR.Framework.Weapons
{
    public class HVRShotgunAmmoSocket : HVRSocket
    {
        public HVRShotgunMagazine ShotgunMagazine;

        public override bool CanHover(HVRGrabbable grabbable)
        {
            if (!ShotgunMagazine.CanAddBullet())
                return false;

            return base.CanHover(grabbable);
        }

        public override bool CanGrab(HVRGrabbable grabbable)
        {
            if (!ShotgunMagazine.CanAddBullet())
                return false;

            return base.CanGrab(grabbable);
        }

        protected override void OnGrabbed(HVRGrabArgs args)
        {
            base.OnGrabbed(args);
            if (ShotgunMagazine.TryAddBullet())
            {
                StartCoroutine(DropAndDestroy(args.Grabbable));
            }
            else
            {
                StartCoroutine(Drop(args.Grabbable));
            }
        }

        private IEnumerator DropAndDestroy(HVRGrabbable grabbable)
        {
            yield return null;
            grabbable.ForceRelease();
            Destroy(grabbable.gameObject);
        }

        private IEnumerator Drop(HVRGrabbable grabbable)
        {
            yield return null;
            grabbable.ForceRelease();
        }
    }
}