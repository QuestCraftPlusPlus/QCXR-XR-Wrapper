using System.Collections.Generic;
using System.Linq;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Sockets
{
    /// <summary>
    /// Groups sockets together for inventory like querying
    /// </summary>
    public class HVRSocketContainer : MonoBehaviour
    {
        [Tooltip("Adds all sockets found on or below this object.")]
        public bool AutoPopulate = true;

        [Tooltip("Sockets in this container")]
        public List<HVRSocket> Sockets = new List<HVRSocket>();

        void Start()
        {
            if (AutoPopulate)
                Sockets = GetComponentsInChildren<HVRSocket>().ToList();
        }

        /// <summary>
        /// Returns true if any socket in the container is not currently holding an object
        /// </summary>
        public virtual bool HasAvailableSocket()
        {
            for (var i = 0; i < Sockets.Count; i++)
            {
                var e = Sockets[i];
                if (!e.IsGrabbing) return true;
            }

            return false;
        }

        /// <summary>
        /// Returns true if any socket in the container is free and is able to socket this grabbable based on the socket filter applied
        /// </summary>
        /// <param name="grabbable"></param>
        /// <returns></returns>
        public virtual bool HasAvailableSocket(HVRGrabbable grabbable)
        {
            return TryFindAvailableSocket(grabbable, out var socket);
        }

        /// <summary>
        /// Returns true if any socket in the container is free and able to socket the provided grabbable based on the filter applied.
        /// </summary>
        /// <param name="grabbable">Grabbable to check the filter for</param>
        /// <param name="socket">Resulting free socket, null if none found</param>
        /// <returns></returns>
        public virtual bool TryFindAvailableSocket(HVRGrabbable grabbable, out HVRSocket socket)
        {
            socket = Sockets.FirstOrDefault(e => !e.IsGrabbing && e.IsValid(grabbable));
            if (socket == null)
                return false;
            return true;
        }

        /// <summary>
        /// Sockets the grabbable to the first found free socket if any are available, returns true if it succeeded
        /// </summary>
        /// <param name="grabbable"></param>
        /// <returns></returns>
        public virtual bool TryAddGrabbable(HVRGrabbable grabbable)
        {
            if (TryFindAvailableSocket(grabbable, out var socket))
                return socket.TryGrab(grabbable);
            return false;
        }
    }
}
