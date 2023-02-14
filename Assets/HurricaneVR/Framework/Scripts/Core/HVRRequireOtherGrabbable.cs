using UnityEngine;

namespace HurricaneVR.Framework.Core
{
    public class HVRRequireOtherGrabbable : MonoBehaviour
    {
        [Tooltip("This grabbable must be held otherwise this grabbable cannot be grabbed.")]
        public HVRGrabbable Grabbable;

        [Tooltip("If the required grabbable is dropped, should we release?")]
        public bool DropIfReleased;

        [Tooltip("If the required grabbable is dropped, let's try and grab it.")]
        public bool GrabRequiredIfReleased;
    }
}