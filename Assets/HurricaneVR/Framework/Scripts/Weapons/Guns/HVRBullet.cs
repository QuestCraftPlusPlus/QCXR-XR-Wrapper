using HurricaneVR.Framework.Components;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Weapons.Guns
{
    public class HVRBullet : MonoBehaviour
    {
        public float Speed;
        public float Gravity;
        public float TimeToLive;
        public float Elapsed;
        public LayerMask LayerMask;

        public bool Hit;

        public HVRGunBase Gun { get; set; }

        //public UnityEvent

        public void Reset()
        {
            Elapsed = 0f;
            Hit = false;
        }

        public void Update()
        {
            //d = vt;
          
            Elapsed += Time.deltaTime;
        }
    }
}