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
        public class YVRInputDataHMDProvider : YVRInputDataBaseProvider
        {
            private Coroutine autoClickCoroutine = null;

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

            private bool wasPressing = false;
            private bool isPressing = false;
            public override bool pressed => !wasPressing && isPressing;
            public override bool released => wasPressing && !isPressing;
            public override bool longPressed => false;
            public override bool thumbStickUp => false;
            public override bool thumbStickDown => false;

            private GameObject lastHitGO = null;
            public override void onRaycastHit(GameObject go)
            {
                base.onRaycastHit(go);

                if (go == null && autoClickCoroutine != null)
                {
                    YVRManager.instance.StopCoroutine(autoClickCoroutine);
                }

                if (go != lastHitGO && go != null)
                {
                    if (autoClickCoroutine != null) YVRManager.instance.StopCoroutine(autoClickCoroutine);
                    autoClickCoroutine = YVRManager.instance.StartCoroutine(AutoClick());
                }

                lastHitGO = go;
            }

            private IEnumerator AutoClick(float clickInterval = 2.0f, float pressDuration = 0.5f)
            {
                // To set isPresenting for one frame every clickInterval
                while (true)
                {
                    yield return new WaitForSecondsRealtime(clickInterval);
                    isPressing = true;

                    yield return new WaitForSecondsRealtime(pressDuration);
                    wasPressing = true;
                    isPressing = false;

                    yield return null;
                    wasPressing = false;
                    isPressing = false;
                }
            }
        }
    }
}