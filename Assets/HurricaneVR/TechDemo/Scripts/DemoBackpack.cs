using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HurricaneVR.Framework.Core;
using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    public class DemoBackpack : MonoBehaviour
    {
        [Tooltip("Used to ignore collision with grabbable colliders.")]
        public List<Collider> Colliders = new List<Collider>();

        void Start()
        {
            if (Colliders == null || Colliders.Count == 0)
            {
                Colliders = GetComponentsInChildren<Collider>().Where(e=>!e.isTrigger).ToList();
            }
            if (Colliders.Count > 0)
                StartCoroutine(IgnoreColliders());
        }

        public IEnumerator IgnoreColliders()
        {
            yield return null;

            //this will only ignore grabbables at the start of the game.
            //you would need to ignore the collision yourself if you instantiate grabbables after.

            var watch = Stopwatch.StartNew();
            var grabbables = FindObjectsOfType<HVRGrabbable>();

            foreach (var grabbable in grabbables)
            {
                IgnoreCollision(grabbable);
            }
            watch.Stop();
            //Debug.Log($"Backpack colliders ignore took : {watch.ElapsedMilliseconds} ms.");
        }

        public void IgnoreCollision(HVRGrabbable grabbable)
        {
            for (var i = 0; i < grabbable.Colliders.Count; i++)
            {
                var c = grabbable.Colliders[i];
                if (!c)
                    continue;

                for (var j = 0; j < Colliders.Count; j++)
                {
                    var ourCollider = Colliders[j];
                    if (ourCollider.isTrigger)
                        continue;
                    Physics.IgnoreCollision(c, ourCollider);
                }
            }
        }
    }
}
