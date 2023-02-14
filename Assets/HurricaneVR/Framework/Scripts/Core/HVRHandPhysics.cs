using System.Collections.Generic;
using System.Linq;
using HurricaneVR.Framework.Core.Utils;
using UnityEngine;

namespace HurricaneVR.Framework.Core
{
    [RequireComponent(typeof(Rigidbody))]
    public class HVRHandPhysics : MonoBehaviour
    {
        public Collider[] HandColliders;// { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        void Awake()
        {
            Rigidbody = gameObject.GetRigidbody();
        }

        public void SetupColliders()
        {
            HandColliders = GetComponentsInChildren<Collider>().Where(e => !e.isTrigger && e.enabled).ToArray();
        }

        public void DisableCollision()
        {
            Rigidbody.detectCollisions = false;
        }

        public void SetAllToTrigger()
        {
            foreach (var handCollider in HandColliders)
            {
                handCollider.isTrigger = true;
            }
        }

        public void ResetToNonTrigger()
        {
            foreach (var handCollider in HandColliders)
            {
                handCollider.isTrigger = false;
            }
        }

        public void EnableCollision()
        {
            Rigidbody.detectCollisions = true;
        }

        public void IgnoreCollision(List<Collider> colliders, bool ignore)
        {
            if (HandColliders == null || colliders == null)
            {
                return;
            }

            foreach (var handCollider in HandColliders)
            {
                foreach (var col in colliders)
                {
                    if (col)
                    {
                        Physics.IgnoreCollision(handCollider, col, ignore);
                    }
                }
            }
        }
    }
}
