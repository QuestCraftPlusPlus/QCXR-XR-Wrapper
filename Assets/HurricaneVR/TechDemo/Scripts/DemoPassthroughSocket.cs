using HurricaneVR.Framework.Core.Grabbers;

namespace HurricaneVR.TechDemo.Scripts
{
    public class DemoPassthroughSocket : HVRSocket
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
            AllowGrabbing = false;
            AllowHovering = false;
            args.Cancel = true;
            Grabbed.Invoke(this, args.Grabbable);
            ForceRelease();
            PlaySocketedSFX(args.Grabbable.Socketable);
        }
    }
}