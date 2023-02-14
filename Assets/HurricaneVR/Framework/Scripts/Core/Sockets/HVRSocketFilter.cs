using UnityEngine;

namespace HurricaneVR.Framework.Core.Sockets
{
    /// <summary>
    /// Base class for Socket Filters that will determine if a HVRSocketable is allowed.
    /// </summary>
    public abstract class HVRSocketFilter : MonoBehaviour
    {
        /// <summary>
        /// Return true if the HVRSocketable is allowed in the socket this filter is attached to.
        /// </summary>
        public abstract bool IsValid(HVRSocketable socketable);

        protected virtual void Awake()
        {
            
        }
    }
}