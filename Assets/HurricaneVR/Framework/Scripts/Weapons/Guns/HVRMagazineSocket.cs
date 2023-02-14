using System.Collections;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.Framework.Weapons.Guns
{
    public class HVRMagazineSocket : HVRSocket
    {
        [Header("Magazine Socket Fields")]
        public bool Animate = true;
        public float AnimationTime = .2f;
        public float MagazineTravel = .1f;
        public bool AnimateEject;
        public float EjectTime = .2f;

        public Transform MagazineAxis;

        protected Vector3 MagazineDirection => MagazineAxis ? MagazineAxis.forward : -transform.up;

        protected override void OnGrabbableParented(HVRGrabbable grabbable)
        {
            if (Animate)
            {
                StartCoroutine(LoadAnimationRoutine(grabbable));
            }
            else
            {
                base.OnGrabbableParented(grabbable);
            }
        }

        protected virtual IEnumerator LoadAnimationRoutine(HVRGrabbable grabbable)
        {
            CanRemoveGrabbable = false;

            var elapsed = 0f;
            var targetPosition = GetTargetPosition(grabbable);
            var targetRotation = GetTargetRotation(grabbable);
            var direction = targetPosition - grabbable.transform.localPosition;
            var speed = direction.magnitude / AnimationTime;

            grabbable.transform.position = transform.position + MagazineDirection * MagazineTravel;
            grabbable.transform.localRotation = targetRotation;

            while (elapsed < AnimationTime)
            {
                grabbable.transform.localPosition = Vector3.MoveTowards(grabbable.transform.localPosition, targetPosition, speed * Time.deltaTime);

                elapsed += Time.deltaTime;
                yield return null;
            }

            grabbable.transform.localPosition = targetPosition;

            CanRemoveGrabbable = true;
        }

        protected virtual IEnumerator EjectAnimationRoutine(HVRGrabbable grabbable)
        {
            grabbable.CanBeGrabbed = false;
            grabbable.Rigidbody.useGravity = false;
            var elapsed = 0f;
            var direction = MagazineDirection;
            var speed = MagazineTravel / EjectTime;
            var target = grabbable.transform.position + direction.normalized * MagazineTravel;

            while (elapsed < EjectTime && grabbable)
            {
                grabbable.transform.position = Vector3.MoveTowards(grabbable.transform.position, target, speed * Time.deltaTime);
                grabbable.Rigidbody.velocity = Vector3.zero;
                grabbable.Rigidbody.angularVelocity = Vector3.zero;
                elapsed += Time.deltaTime;
                yield return null;
            }

            //magazine might be destroyed if it's empty
            if (grabbable)
            {
                grabbable.Rigidbody.useGravity = true;
                grabbable.CanBeGrabbed = true;
            }
        }

        protected override void OnReleased(HVRGrabbable grabbable)
        {
            base.OnReleased(grabbable);
            if (AnimateEject && gameObject.activeInHierarchy)
            {
                StartCoroutine(EjectAnimationRoutine(grabbable));
            }
        }
    }
}