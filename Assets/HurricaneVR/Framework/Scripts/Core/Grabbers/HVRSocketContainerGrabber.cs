using System.Collections;
using System.Linq;
using HurricaneVR.Framework.Core.Bags;
using HurricaneVR.Framework.Core.Sockets;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Grabbers
{

    /// <summary>
    /// Grabber that uses a HVRSocketContainer as a target of the grab. Main uses are over the shoulder or chest collection type inventories
    /// </summary>
    public class HVRSocketContainerGrabber : HVRGrabberBase
    {
        public HVRSocketContainer SocketContainer;

        [Tooltip("If true item's must be placed with a hand grabber.")]
        public bool GrabbleMustBeHeld = true;

        public override bool IsGrabActivated => !GrabbleMustBeHeld && SocketContainer.HasAvailableSocket();
        public override bool AllowSwap => true;

        protected override void Start()
        {
            base.Start();

            if (GrabBags.Count == 0)
            {
                var bag = gameObject.AddComponent<HVRTriggerGrabbableBag>();
                GrabBags.Add(bag);
                bag.Grabber = this;
            }
        }

        public override bool CanHover(HVRGrabbable grabbable)
        {
            if (!SocketContainer)
                return false;
            if (grabbable.IsBeingForcedGrabbed)
                return false;
            if (grabbable.GrabberCount != 1)
                return false;
            var handGrabber = grabbable.PrimaryGrabber as HVRHandGrabber;
            if (handGrabber == null)
                return false;

            if (!SocketContainer.HasAvailableSocket(grabbable))
                return false;
            
            if (grabbable.IsStabbing || grabbable.IsStabbed)
                return false;

            return base.CanHover(grabbable);
        }

        public override bool CanGrab(HVRGrabbable grabbable)
        {
            if (!SocketContainer)
                return false;
            if (grabbable.IsBeingForcedGrabbed)
                return false;
            if (grabbable.IsBeingHeld)
                return false;
            if (!SocketContainer.HasAvailableSocket(grabbable))
                return false;
            if (grabbable.IsStabbing || grabbable.IsStabbed)
                return false;
            return base.CanGrab(grabbable);
        }

        protected override void OnHoverEnter(HVRGrabbable grabbable)
        {
            if (GrabbleMustBeHeld)
                grabbable.Released.AddListener(OnHoverGrabbableReleased);
            base.OnHoverEnter(grabbable);
        }

        protected override void OnHoverExit(HVRGrabbable grabbable)
        {
            if (GrabbleMustBeHeld)
                grabbable.Released.RemoveListener(OnHoverGrabbableReleased);
            base.OnHoverExit(grabbable);
        }

        private void OnHoverGrabbableReleased(HVRGrabberBase grabber, HVRGrabbable grabbable)
        {
            UnhoverGrabbable(grabber, grabbable);
            //something else might have grabbed this, like a another one of these grabbers
            StartCoroutine(TryGrabGrabbable(grabbable));
        }

        private IEnumerator TryGrabGrabbable(HVRGrabbable grabbable)
        {
            yield return null;

            if (!grabbable.IsBeingHeld)
                SocketContainer.TryAddGrabbable(grabbable);
        }

        protected override void OnGrabbed(HVRGrabArgs args)
        {
            //socket cannot grab something if it's being held, so we force release it first so it's CanGrab will not return false
            ForceRelease();
            SocketContainer.TryAddGrabbable(args.Grabbable);
        }
    }
}
