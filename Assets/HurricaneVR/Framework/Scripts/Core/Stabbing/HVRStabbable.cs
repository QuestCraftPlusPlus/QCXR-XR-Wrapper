using System.Collections.Generic;
using System.Linq;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Stabbing
{
    public class HVRStabbable : MonoBehaviour
    {
        public HVRStabbableSettings Settings;
        
        [Tooltip("Collision of these colliders are ignored with the stabber.")]
        public List<Collider> Ignorecolliders = new List<Collider>();

        public HVRStabEvent Stabbed = new HVRStabEvent();
        public HVRStabEvents UnStabbed = new HVRStabEvents();
        public HVRStabEvents FullStabbed = new HVRStabEvents();

        public bool IsStabbed => Stabbers.Count > 0;
        public List<HVRStabber> Stabbers;

        public Vector3 Velocity { get; private set; }
        private Vector3 _previousPosition;

        protected virtual void Awake()
        {
            if (!Settings)
            {
                Settings = ScriptableObject.CreateInstance<HVRStabbableSettings>();
            }

            Settings.CheckCurve();
            Stabbers = new List<HVRStabber>();

            if (Ignorecolliders == null) Ignorecolliders = new List<Collider>();
            if (Ignorecolliders.Count == 0)
            {
                RefreshColliders();
            }
        }
        /// <summary>
        /// Refreshes ignore colliders, if rigidbody present it will get those colliders, if not it will get all children colliders
        /// </summary>
        public virtual void RefreshColliders()
        {
            Ignorecolliders.Clear();

            if (TryGetComponent(out Rigidbody rb))
            {
                Ignorecolliders.AddRange(rb.GetColliders());
            }
            else
            {
                Ignorecolliders.AddRange(GetComponentsInChildren<Collider>().Where(e => !e.isTrigger));
            }
        }

        //public void Update()
        //{
        //    //DrawBounds();
        //}

        private void DrawBounds()
        {
            var bounds = transform.GetColliderBounds();
            bounds.DrawBounds();
        }



        public void FixedUpdate()
        {
            Cleanup();
            Velocity = (transform.position - _previousPosition) / Time.deltaTime;
            _previousPosition = transform.position;
        }

        private void Cleanup()
        {
            var cleanup = false;
            foreach (var stabber in Stabbers)
            {
                if (!stabber || !stabber.gameObject.activeInHierarchy || !stabber.enabled)
                {
                    cleanup = true;
                    break;
                }
            }

            if (cleanup)
            {
                Stabbers.RemoveAll(e => e == null || !e.gameObject.activeInHierarchy || !e.enabled);
            }
        }


        public virtual void OnStabberEnter(HVRStabber stabber, Collision collision, ContactPoint contactPoint)
        {
            Stabbers.Add(stabber);
        }

        public virtual void OnStabberExit(HVRStabber stabber)
        {
            Stabbers.Remove(stabber);
            UnStabbed.Invoke(stabber, this);
        }

        public virtual void OnFullStabReached(HVRStabber stabber)
        {
            FullStabbed.Invoke(stabber, this);
        }

    }
}