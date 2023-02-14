using System.Collections.Generic;

namespace HurricaneVR.Framework.Core.Sockets
{
    public class HVRGrabbableSocketFilter : HVRSocketFilter
    {
        public List<HVRGrabbable> ValidGrabbables = new List<HVRGrabbable>();

        public override bool IsValid(HVRSocketable socketable)
        {
            return ValidGrabbables.Contains(socketable.Grabbable);
        }
    }
}