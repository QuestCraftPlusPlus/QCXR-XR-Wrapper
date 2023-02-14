using System;
using System.Collections.Generic;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Utils;
using UnityEngine;

namespace HurricaneVR.Framework.Core.HandPoser
{
    public class HVRPoseZone : MonoBehaviour
    {
        [Tooltip("Decide to ignore non trigger colliders or not on the hand for stable detection of non moving colliders.")]
        public bool UseHandTriggersOnly = true;

        [Tooltip("Uses box overlap or sphere overlap")]
        public bool UseBox = true;

        public Vector3 OverlapBox = new Vector3(.15f, .15f, .15f);
        public float OverlapRadius = .15f;

        [Tooltip("Layer mask of the hand collider for performant detection")]
        public LayerMask HandMask;

        [Tooltip("Poser that defines the pose to animate to, captured on awake from same object if not supplied.")]
        public HVRHandPoser Poser;

        private readonly List<HVRHandGrabber> overlappedHands = new List<HVRHandGrabber>(2);
        private readonly List<HVRHandGrabber> hands = new List<HVRHandGrabber>(2);
        private readonly Collider[] _colliders = new Collider[100];

        private void Awake()
        {
            if (!Poser)
            {
                TryGetComponent(out Poser);
            }
        }

        private void FixedUpdate()
        {
            hands.Clear();
            int count;
            if (UseBox)
            {
                count = Physics.OverlapBoxNonAlloc(transform.position, OverlapBox * .5f, _colliders, transform.rotation, HandMask, QueryTriggerInteraction.Collide);
            }
            else
            {
                count = Physics.OverlapSphereNonAlloc(transform.position, OverlapRadius, _colliders, HandMask, QueryTriggerInteraction.Collide);
            }

            for (int i = 0; i < count; i++)
            {
                var c = _colliders[i];
                var hand = c.FindComponent<HVRHandGrabber>();
                if (!hand) continue;

                if (c.isTrigger)
                {
                    if (!c.TryGetComponent(out HVRHandTrigger _))
                    {
                        //ignore triggers that aren't marked with HVRHandTrigger or a child of the hand model so that other triggers on the hand rigidbody
                        //that are used for other purposes like the distance grabber are aren't considered
                        if (hand.HandAnimator && (!c.transform.IsChildOf(hand.HandAnimator.Hand.transform)) && c.transform != hand.HandAnimator.Hand.transform)
                        {
                            continue;
                        }
                    }
                }
                else if (UseHandTriggersOnly)
                {
                    continue;
                }

                hands.Add(hand);
            }


            foreach (var hand in hands)
            {
                if (!overlappedHands.Contains(hand))
                {
                    hand.SetAnimatorOverridePose(Poser);
                    overlappedHands.Add(hand);
                }
            }

            for (var i = overlappedHands.Count - 1; i >= 0; i--)
            {
                var hand = overlappedHands[i];
                if (!hand || !hands.Contains(hand))
                {
                    hand.SetAnimatorOverridePose(null);
                    overlappedHands.RemoveAt(i);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            if (UseBox)
            {
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawWireCube(Vector3.zero, OverlapBox);
            }
            else
            {
                Gizmos.DrawWireSphere(transform.position, OverlapRadius);
            }
        }
    }
}