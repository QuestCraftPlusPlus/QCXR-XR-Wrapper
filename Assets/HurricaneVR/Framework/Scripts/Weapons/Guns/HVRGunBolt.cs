using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Weapons.Guns
{
    public class HVRGunBolt : HVRGunPart
    {
        public bool IsPushedBack { get; set; }
        
        private void Awake()
        {
        }

        public void Move(float percent)
        {
            if (float.IsNaN(percent))
                return;
            transform.localPosition = Vector3.Lerp(ForwardPosition, BackwardPosition, percent);
        }

        public virtual void PushBack()
        {
            transform.localPosition = BackwardPosition;
            IsPushedBack = true;
        }
        public override void Animate(float percent, CycleDirection direction)
        {
            Move(percent);
        }
    }
}