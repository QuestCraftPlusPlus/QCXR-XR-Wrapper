using System;
using System.Collections.Generic;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Bags
{
    public class HVRTriggerGrabbableBag : HVRGrabbableBag
    {
        private readonly Dictionary<HVRGrabbable, HashSet<Collider>> _map = new Dictionary<HVRGrabbable, HashSet<Collider>>();

        [Tooltip("If true it will use Collider.ClosestPoint method to determine the closest grabbable.")]
        public bool UseColliderDistance = true;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();

            if (Math.Abs(MaxDistanceAllowed) < .001)
            {
                //Debug.Log($"{gameObject.name}: MaxDistanceAllowed too low, setting to 1.5f");
                MaxDistanceAllowed = 1.5f;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent<HVRGrabbable>(out var grabbable);
            other.TryGetComponent<HVRGrabbableChild>(out var childGrabbable);

            if (!grabbable)
            {
                if (childGrabbable && childGrabbable.ParentGrabbable)
                {
                    grabbable = childGrabbable.ParentGrabbable;
                }
            }

            if (HVRSettings.Instance.UseAttachedRigidBody && !grabbable && other.attachedRigidbody)
            {
                other.attachedRigidbody.TryGetComponent<HVRGrabbable>(out grabbable);
            }

            if (HVRSettings.Instance.ComponentInParentFallback && !grabbable)
            {
                grabbable = other.GetComponentInParent<HVRGrabbable>();
            }

            if (grabbable)
            {
                if (grabbable.FilterGrabColliders && !grabbable.GrabCollidersSet.Contains(other))
                    return;

                if (!_map.TryGetValue(grabbable, out var colliders))
                {
                    colliders = new HashSet<Collider>();
                    _map[grabbable] = colliders;
                }

                if (colliders.Count == 0)
                {
                    AddGrabbable(grabbable);
                }

                colliders.Add(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {

            other.TryGetComponent<HVRGrabbable>(out var grabbable);
            other.TryGetComponent<HVRGrabbableChild>(out var childGrabbable);
            if (!grabbable && childGrabbable && childGrabbable.ParentGrabbable)
            {
                grabbable = childGrabbable.ParentGrabbable;
            }

            if (HVRSettings.Instance.UseAttachedRigidBody && !grabbable && other.attachedRigidbody)
            {
                other.attachedRigidbody.TryGetComponent<HVRGrabbable>(out grabbable);
            }

            if (HVRSettings.Instance.ComponentInParentFallback && !grabbable)
            {
                grabbable = other.GetComponentInParent<HVRGrabbable>();
            }

            if (grabbable)
            {
                if (_map.TryGetValue(grabbable, out var colliders))
                {
                    colliders.Remove(other);
                }

                if (colliders == null || colliders.Count == 0)
                {
                    RemoveGrabbable(grabbable);
                }
            }
        }

        public override float DistanceToGrabbable(HVRGrabbable grabbable)
        {
            if (UseColliderDistance && _map.TryGetValue(grabbable, out var colliders) && grabbable.UseColliderClosestPoint)
            {
                var distance = float.MaxValue;
                var anchor = DistanceSource ? DistanceSource.position : Grabber.transform.position;

                foreach (var grabbableCollider in colliders)
                {
                    if (!grabbableCollider)
                        continue;

                    Vector3 point;
                    if (grabbable.HasConcaveColliders && grabbableCollider is MeshCollider meshCollider && !meshCollider.convex)
                    {
                        if (!grabbableCollider.Raycast(new Ray(anchor, grabbableCollider.bounds.center - anchor), out var hit, Vector3.Distance(grabbableCollider.bounds.center, anchor)))
                        {
                            continue;
                        }

                        point = hit.point;
                    }
                    else
                    {
                        point = grabbableCollider.ClosestPoint(anchor);
                    }

                    var current = Vector3.Distance(point, anchor);
                    if (current < distance)
                    {
                        distance = current;
                    }
                }

                return distance;
            }

            return base.DistanceToGrabbable(grabbable);
        }

    }
}