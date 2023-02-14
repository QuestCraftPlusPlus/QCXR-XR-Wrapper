using System;
using System.Linq;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Core.HandPoser
{
    public class HVRGrabPointSwapper : HVRInputAction
    {
        public HVRPosableGrabPoint[] GrabPoints;

        [Tooltip("Rotation direction when moving to this grab point index.")]
        public HVRAxis[] RotateAxis;

        [Tooltip("Time it takes to get to the next grab point.")]
        public float SwapTime = .2f;

        [Tooltip("Used when other hands with poser index > 0.")]
        public PoserGrabPoints[] OtherHands;

        protected override void Awake()
        {
            base.Awake();
            if (GrabPoints != null)
            {
                GrabPoints = GrabPoints.Where(e => e).ToArray();
            }

            //Grabbable.ParentHandModel = false;
        }

        protected override void CheckInput(HVRController controller)
        {
            var activated = GetActivated(controller);

            if (activated)
            {
                Swap();
            }
        }

        protected virtual bool GetActivated(HVRController controller)
        {
            var activated = false;

            if (controller.ControllerType == HVRControllerType.WMR)
            {
                activated = controller.Side == HVRHandSide.Right ? controller.TrackPadLeft.JustActivated : controller.TrackPadRight.JustActivated;
            }
            else if (controller.ControllerType == HVRControllerType.Vive)
            {
                activated = HVRInputManager.Instance.RightController.TrackPadDown.JustActivated;
            }
            else
            {
                activated = controller.PrimaryButtonState.JustActivated;
            }

            return activated;
        }

        public virtual void Swap()
        {
            var hand = Grabbable.HandGrabbers[0];
            var points = GrabPoints;

            if (hand.PoserIndex > 0)
            {
                if (OtherHands == null || hand.PoserIndex - 1 >= OtherHands.Length)
                    return;

                points = OtherHands[hand.PoserIndex - 1].GrabPoints;
            }

            if (points == null || points.Length == 0)
                return;

            var index = Array.IndexOf(points, hand.PosableGrabPoint);
            if (index < 0)
                return;

            var nextIndex = index + 1;
            if (index == points.Length - 1)
                nextIndex = 0;

            var current = hand.PosableGrabPoint;
            var next = points[nextIndex];
            var axis = HVRAxis.X;
            if (RotateAxis != null && RotateAxis.Length > 0 && nextIndex < RotateAxis.Length)
                axis = RotateAxis[nextIndex];
            hand.ChangeGrabPoint(next, SwapTime, axis);
            OnGrabPointSwapped(current, next);
        }

        protected virtual void OnGrabPointSwapped(HVRPosableGrabPoint previous, HVRPosableGrabPoint next)
        {

        }
    }

    [Serializable]
    public class PoserGrabPoints
    {
        public HVRPosableGrabPoint[] GrabPoints;
    }
}