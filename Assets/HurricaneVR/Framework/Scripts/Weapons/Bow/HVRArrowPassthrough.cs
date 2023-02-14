using Assets.HurricaneVR.Framework.Shared.Utilities;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;

namespace HurricaneVR.Framework.Weapons.Bow
{
    public class HVRArrowPassthrough : HVRGrabbable
    {
        public HVRGrabbable Arrow;

        protected override void Start()
        {
            base.Start();

            if (!Arrow && transform.parent)
            {
                Arrow = transform.parent.GetComponentInParent<HVRGrabbable>();
            }
        }

        protected override void OnGrabbed(HVRGrabberBase grabber)
        {
            grabber.ForceRelease();
            if (Arrow)
            {
                grabber.AllowGrabbing = false;
                this.ExecuteNextUpdate(() =>
                {
                    grabber.TryGrab(Arrow, true);
                    grabber.AllowGrabbing = true;
                });
            }
        }
    }
}