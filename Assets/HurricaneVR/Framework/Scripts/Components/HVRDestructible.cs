using HurricaneVR.Framework.Core;
using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Helper component that spawns a prefab game object when the Destroy function is called.
    /// If the spawned game object has a rigidbodies then they will have force added to them based on the
    /// fields provided.
    /// </summary>
    public class HVRDestructible : MonoBehaviour
    {
        public GameObject DestroyedVersion;

        public float ExplosionRadius = .1f;
        public float ExplosionPower = 1;
        public float ExplosionUpwardsPower = 1;

        public bool RemoveDebris = true;
        public float RemoveDebrisTimerUpper = 10f;
        public float RemoveDebrisTimerLower = 5f;

        public bool IgnorePlayerCollision = true;

        public bool Destroyed { get; protected set; }

        public virtual void Destroy()
        {
            if (Destroyed) return;

            if (DestroyedVersion)
            {
                var destroyed = Instantiate(DestroyedVersion, transform.position, transform.rotation);

                foreach (var rigidBody in destroyed.GetComponentsInChildren<Rigidbody>())
                {
                    var v = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    rigidBody.AddForce(v * ExplosionPower, ForceMode.VelocityChange);

                    if (RemoveDebris)
                    {
                        var delay = Random.Range(RemoveDebrisTimerLower, RemoveDebrisTimerUpper);
                        if (delay < .1f)
                        {
                            delay = 3f;
                        }

                        var timer = rigidBody.gameObject.AddComponent<HVRDestroyTimer>();
                        timer.StartTimer(delay);
                    }

                    if (IgnorePlayerCollision)
                    {
                        var colliders = rigidBody.gameObject.GetComponentsInChildren<Collider>();
                        HVRManager.Instance?.IgnorePlayerCollision(colliders);
                    }

                    rigidBody.transform.parent = null;
                }

                if (RemoveDebris)
                {
                    var timer = destroyed.gameObject.AddComponent<HVRDestroyTimer>();
                    var delay = RemoveDebrisTimerUpper;
                    if (delay <= .1f)
                        delay = 3f;
                    timer.StartTimer(delay);
                }
            }

            Destroyed = true;
            Destroy(gameObject);
        }
    }
}