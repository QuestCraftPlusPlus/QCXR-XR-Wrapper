using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Internal;

namespace YVR.Core
{
    public partial class YVRInputModule : PointerInputModule
    {
        [ExcludeFromDocs]
        public class YVRInputDataLHandTrackingProvider : YVRInputDataBaseProvider
        {
            private Transform _rayTransform = null;

            public override Transform rayTransform
            {
                get
                {
                    if (_rayTransform == null)
                        _rayTransform = YVRHandManager.instance.leftHand.pointerPose;
                    return _rayTransform;
                }
            }

            public override Vector2 scrollData
            {
                get => Vector2.zero;
            }

            public override bool pressed { get => YVRHandManager.instance.GetPinchDown(HandType.HandLeft, XRHandFingerID.Index); }

            public override bool released { get => YVRHandManager.instance.GetPinchUp(HandType.HandLeft, XRHandFingerID.Index); }

            public override bool longPressed { get => YVRHandManager.instance.GetPinch(HandType.HandLeft, XRHandFingerID.Index); }

            public override bool thumbStickUp => false;
            public override bool thumbStickDown => false;
        }

        [ExcludeFromDocs]
        public class YVRInputDataRHandTrackingProvider : YVRInputDataBaseProvider
        {
            private Transform _rayTransform = null;

            public override Transform rayTransform
            {
                get
                {
                    if (_rayTransform == null)
                        _rayTransform = YVRHandManager.instance.rightHand.pointerPose;
                    return _rayTransform;
                }
            }

            public override Vector2 scrollData
            {
                get => Vector2.zero;
            }

            public override bool pressed { get => YVRHandManager.instance.GetPinchDown(HandType.HandRight, XRHandFingerID.Index); }

            public override bool released { get => YVRHandManager.instance.GetPinchUp(HandType.HandRight, XRHandFingerID.Index); }

            public override bool longPressed { get => YVRHandManager.instance.GetPinch(HandType.HandRight, XRHandFingerID.Index); }

            public override bool thumbStickUp => false;
            public override bool thumbStickDown => false;
        }
    }
}