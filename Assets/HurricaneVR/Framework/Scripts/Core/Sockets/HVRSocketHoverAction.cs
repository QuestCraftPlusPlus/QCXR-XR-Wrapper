using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Sockets
{
    public abstract class HVRSocketHoverAction : MonoBehaviour
    {
        public Transform Target;

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }

        public abstract void OnHoverEnter(HVRSocket socket, HVRGrabbable grabbable, bool isValid);

        public abstract void OnHoverExit(HVRSocket socket, HVRGrabbable grabbable, bool isValid);
    }
}