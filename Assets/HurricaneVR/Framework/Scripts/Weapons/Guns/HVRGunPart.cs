using UnityEngine;

namespace HurricaneVR.Framework.Weapons.Guns
{
    public class HVRGunPart : MonoBehaviour
    {
        [Header("Gun Part Positions (Base Class)")]
        public Vector3 ForwardPosition;
        public Vector3 BackwardPosition;

        public virtual void Animate(float percent, CycleDirection direction)
        {
            if (float.IsNaN(percent))
                return;
            transform.localPosition = Vector3.Lerp(ForwardPosition, BackwardPosition, percent);
        }
    }

    public enum CycleDirection
    {
        Backward,
        Forward
    }
}