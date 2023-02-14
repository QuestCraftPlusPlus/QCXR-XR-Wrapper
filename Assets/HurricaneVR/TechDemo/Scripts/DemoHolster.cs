using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    public class DemoHolster : HVRSocket
    {
        public override Vector3 GetTargetPosition(HVRGrabbable grabbable)
        {
            if (grabbable.TryGetComponent(out DemoHolsterOrientation orientation))
            {
                var offSet = -orientation.Orientation.localPosition;
                var delta = Quaternion.Inverse(orientation.Orientation.localRotation);
                offSet = delta * offSet;

                offSet.x *= grabbable.transform.localScale.x;
                offSet.y *= grabbable.transform.localScale.y;
                offSet.z *= grabbable.transform.localScale.z;

                return offSet;
            }

            return base.GetTargetPosition(grabbable);
        }

        public override Quaternion GetTargetRotation(HVRGrabbable grabbable)
        {
            if (grabbable.TryGetComponent(out DemoHolsterOrientation orientation))
            {
                return Quaternion.Inverse(orientation.Orientation.localRotation);
            }
            
            return base.GetTargetRotation(grabbable);
        }
    }
}