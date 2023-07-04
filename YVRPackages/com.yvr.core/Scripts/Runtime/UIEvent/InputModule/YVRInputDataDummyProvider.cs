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
        public class YVRInputDataDummyProvider : YVRInputDataBaseProvider
        {
            private Transform _rayTransform = null;
            public override Transform rayTransform
            {
                get
                {
                    if (_rayTransform == null)
                        _rayTransform = YVRManager.instance.cameraRig.centerEyeAnchor;
                    return _rayTransform;
                }
            }

            public override Vector2 scrollData { get => Vector2.zero; }
            public override bool pressed => false;
            public override bool released => false;
            public override bool longPressed => false;
            public override bool thumbStickUp => false;
            public override bool thumbStickDown => false;
        }
    }
}