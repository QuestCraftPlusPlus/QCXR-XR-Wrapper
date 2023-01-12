using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;

namespace YVR.Core
{
    [ExcludeFromDocs]
    public class YVRDebugHub : MonoBehaviour
    {
        [SerializeField] private bool isHeadLock = true;
        private new Transform transform = null;
        protected virtual IEnumerator Start()
        {
            this.transform = base.transform;

            InitDebugHub();
            yield return null;
            YVRManager.instance.eventsManager.onUpdate += UpdateDebugInfo;
        }

        protected virtual void InitDebugHub() { }

        protected virtual void UpdateDebugInfo()
        {
            if (isHeadLock)
                transform.SetParent(YVRManager.instance.cameraRig.trackingSpace, false);
        }

    }
}