using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Utils;
using UnityEngine;

namespace HurricaneVR.Framework.Weapons.Bow
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(HVRGrabbable))]
    public class HVRArrow : MonoBehaviour
    {
        public Transform Notch;
        public Vector3 NotchPointLocal { get; private set; } = Vector3.zero;

        public Rigidbody Rigidbody { get; set; }

        public HVRGrabbable Grabbable { get; private set; }
        public HVRArrowPassthrough ForwardGrabbable { get; private set; }

        public bool Flying { get; set; }

        protected virtual void Awake()
        {
            if (Notch)
            {
                NotchPointLocal = Notch.localPosition;
            }

            Rigidbody = this.GetRigidbody();
            Grabbable = GetComponent<HVRGrabbable>();
            Grabbable.Grabbed.AddListener(OnGrabbed);

            ForwardGrabbable = GetComponentInChildren<HVRArrowPassthrough>();
        }

        public void EnableForwardGrabbable()
        {
            if(ForwardGrabbable)
                ForwardGrabbable.enabled = true;
        }

        public void DisableForwardGrabbable()
        {
            if (ForwardGrabbable)
                ForwardGrabbable.enabled = false;
        }

        private void OnGrabbed(HVRGrabberBase arg0, HVRGrabbable arg1)
        {
            Flying = false;
        }

        public void Update()
        {
            if (Flying)
            {
                transform.forward = Vector3.Slerp(transform.forward, Rigidbody.velocity.normalized, Time.deltaTime);
            }
        }

        public virtual void FixedUpdate()
        {
            if (Rigidbody.velocity.sqrMagnitude < 1f)
            {
                Flying = false;
            }
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            Flying = false;
        }
    }
}