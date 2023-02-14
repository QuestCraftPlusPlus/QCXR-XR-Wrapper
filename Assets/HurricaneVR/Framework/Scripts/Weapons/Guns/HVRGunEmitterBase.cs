using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared.Utilities;
using UnityEngine;

namespace HurricaneVR.Framework.Weapons.Guns
{
    public abstract class HVRGunEmitterBase : MonoBehaviour
    {
        public float LaunchRadius = .05f;

        public float MinVelocity = 4f;
        public float MaxVelocity = 6f;

        public Vector3 MinAngularVelocity = new Vector3(0f, 8f, 0f);
        public Vector3 MaxAngularVelocity = new Vector3(0f, 10f, 0f);

        public HVRGunBase Gun;
        
        public GameObject Prefab;

        protected virtual void Awake()
        {

        }

        public virtual void Emit()
        {

        }

        public virtual void Launch(Rigidbody rb)
        {
            if (!rb)
                return;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.transform.position = transform.position;
            rb.transform.rotation = transform.rotation;
            

            var xy = Random.insideUnitCircle * LaunchRadius;
            var launchDirection = transform.right + new Vector3(0f, xy.x, xy.y);

            rb.velocity = launchDirection * Random.Range(MinVelocity, MaxVelocity);
            rb.AddRelativeTorque(
                Random.Range(MinAngularVelocity.x, MaxAngularVelocity.x),
                Random.Range(MinAngularVelocity.y, MaxAngularVelocity.y),
                Random.Range(MinAngularVelocity.z, MaxAngularVelocity.z),
                ForceMode.VelocityChange
            );
        }
    }
}