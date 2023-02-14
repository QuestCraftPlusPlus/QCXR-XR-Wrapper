using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    public abstract class HVRGrabbableHoverBase : MonoBehaviour
    {
        public HVRHoverPosition HoverPosition = HVRHoverPosition.GrabPoint;
        public bool LookAtCamera = true;
        
        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }

        public abstract void Hover();

        public abstract void Unhover();

        public abstract void Enable();
        public abstract void Disable();
    }

    public enum HVRHoverPosition
    {
        GrabPoint,
        Self,
        Transform
    }
}