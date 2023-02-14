using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Utils
{
    public static class PhysicsExtensions
    {
        public static IEnumerator IgnoreCollisionForSeconds(this Collider[] colliders, List<Collider> otherColliders, float seconds)
        {
            for (var i = 0; i < colliders.Length; i++)
            {
                var collider = colliders[i];
                if (!collider)
                    continue;

                for (var j = 0; j < otherColliders.Count; j++)
                {
                    var otherCollider = otherColliders[j];
                    if (!otherCollider)
                        continue;
                    Physics.IgnoreCollision(collider, otherCollider, true);
                }
            }

            yield return new WaitForSeconds(seconds);

            for (var i = 0; i < colliders.Length; i++)
            {
                var collider = colliders[i];
                if (!collider)
                    continue;

                for (var j = 0; j < otherColliders.Count; j++)
                {
                    var otherCollider = otherColliders[j];
                    if (!otherCollider)
                        continue;
                    Physics.IgnoreCollision(collider, otherCollider, false);
                }
            }
        }
    }
}