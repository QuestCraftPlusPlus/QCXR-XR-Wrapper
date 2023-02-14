using System;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Publishes the destroyed event so others may know, used by the hand grabber to know if the held object is destroyed
    /// </summary>
    public class HVRDestroyListener : MonoBehaviour
    {
        public HVRDestroyedEvent Destroyed = new HVRDestroyedEvent();

        private void OnDestroy()
        {
            Destroyed.Invoke(this);
            Destroyed.RemoveAllListeners();
        }
    }

    [Serializable]
    public class HVRDestroyedEvent : UnityEvent<HVRDestroyListener>
    {

    }
}
