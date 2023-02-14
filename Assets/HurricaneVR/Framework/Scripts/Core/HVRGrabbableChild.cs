using UnityEngine;

namespace HurricaneVR.Framework.Core
{
    public class HVRGrabbableChild : MonoBehaviour
    {
        public HVRGrabbable ParentGrabbable;

        private void Start()
        {
            if (!ParentGrabbable) ParentGrabbable = GetComponentInParent<HVRGrabbable>();
        }
    }
}
