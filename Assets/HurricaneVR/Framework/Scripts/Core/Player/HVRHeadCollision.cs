using System;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Core.Player
{
    public class HVRHeadCollision : MonoBehaviour
    {
        public float FadeSpeed = 5f;
        public float OuterRadius = 0.1f;
        public float DistanceToMostClosePoint;
        public float RadiusOfSphereCast = 0.1f;
        public float EyeRadius = 0.015f;
        public LayerMask LayerMask;
        public bool RequireUserPresence = true;
        public HVRScreenFade ScreenFade;

        private Collider[] _hits;
        private Collider[] _hitsDummy;

        public bool IsColliding { get; private set; }

        void Start()
        {
            _hits = new Collider[20];
            _hitsDummy = new Collider[1];
        }

        void FixedUpdate()
        {

            DistanceToMostClosePoint = 1;

            IsColliding = false;
            var hits = Physics.OverlapSphereNonAlloc(transform.position, OuterRadius, _hits, LayerMask, QueryTriggerInteraction.Ignore);
            if (hits > 0)
            {
                //DistanceToMostClosePoint = 0f;
                if (Physics.OverlapSphereNonAlloc(transform.position, EyeRadius, _hitsDummy, LayerMask, QueryTriggerInteraction.Ignore ) > 0)
                {
                    IsColliding = true;
                    DistanceToMostClosePoint = 0;
                }
                else
                {
                    for (var i = 0; i < hits; i++)
                    {
                        var centerOfHit = _hits[i].bounds.center;
                        var direction = centerOfHit - transform.position;

                        if (Physics.SphereCast(transform.position, RadiusOfSphereCast, direction, out RaycastHit hit, Vector3.Distance(transform.position, centerOfHit), LayerMask))
                        {
                            if (hit.distance < DistanceToMostClosePoint)
                            {
                                DistanceToMostClosePoint = hit.distance;
                            }
                        }
                    }
                }
            }

            var fade = 1 - DistanceToMostClosePoint;
            if (ScreenFade)
            {
                if (fade > .001f)
                {
                    ScreenFade.Fade(fade, FadeSpeed);
                }
                else
                {
                    ScreenFade.UpdateFade(0f);
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, EyeRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, OuterRadius);
        }
#endif
    }
}
