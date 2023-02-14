using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    public abstract class HVRDamageHandlerBase : MonoBehaviour
    {
        public virtual void TakeDamage(float damage)
        {
        }

        public virtual void HandleDamageProvider(HVRDamageProvider damageProvider, Vector3 hitPoint, Vector3 direction)
        {
            TakeDamage(damageProvider.Damage);
        }

        public virtual void HandleRayCastHit(HVRDamageProvider damageProvider, RaycastHit hit)
        {

        }
    }
}