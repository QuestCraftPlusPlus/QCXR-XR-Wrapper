using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Internal;

namespace YVR.Core
{
    public partial class YVRInputModule : PointerInputModule
    {
        [ExcludeFromDocs]
        public class YVRInputDataLControllerProvider : YVRInputDataBaseProvider
        {
            private Transform _rayTransform = null;
            public override Transform rayTransform
            {
                get
                {
                    if (_rayTransform == null)
                        _rayTransform = YVRManager.instance.controllerRig.leftControllerAnchor;
                    return _rayTransform;
                }
            }

            public override Vector2 scrollData { get => YVRInput.Get(YVRInput.RawAxis2D.LThumbstick); }

            public override bool pressed { get => YVRInput.GetDown(YVRInput.RawButton.LIndexTrigger); }
            public override bool released { get => YVRInput.GetUp(YVRInput.RawButton.LIndexTrigger); }
            public override bool longPressed { get => YVRInput.Get(YVRInput.RawButton.LIndexTrigger); }
            public override bool thumbStickUp { get => YVRInput.Get(YVRInput.RawButton.LThumbstickUp); }
            public override bool thumbStickDown { get => YVRInput.Get(YVRInput.RawButton.LThumbstickDown); }
        }
        [ExcludeFromDocs]
        public class YVRInputDataRControllerProvider : YVRInputDataBaseProvider
        {
            private Transform _rayTransform = null;
            public override Transform rayTransform
            {
                get
                {
                    if (_rayTransform == null)
                        _rayTransform = YVRManager.instance.controllerRig.rightControllerAnchor;
                    return _rayTransform;
                }
            }

            public override Vector2 scrollData { get => YVRInput.Get(YVRInput.RawAxis2D.RThumbstick); }

            public override bool pressed { get => YVRInput.GetDown(YVRInput.RawButton.RIndexTrigger); }
            public override bool released { get => YVRInput.GetUp(YVRInput.RawButton.RIndexTrigger); }
            public override bool longPressed { get => YVRInput.Get(YVRInput.RawButton.RIndexTrigger); }
            public override bool thumbStickUp { get => YVRInput.Get(YVRInput.RawButton.RThumbstickUp); }
            public override bool thumbStickDown { get => YVRInput.Get(YVRInput.RawButton.RThumbstickDown); }
        }
    }
}
