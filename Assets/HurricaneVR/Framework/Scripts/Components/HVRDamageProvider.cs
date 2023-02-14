using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    public class HVRDamageProvider : MonoBehaviour
    {
        public float Damage = 25f;
        public float Force;

        [Tooltip("Player transform for ai frameworks like emerald ai")]
        public Transform Player;

        protected virtual void Start()
        {
        
        }

    }
}
