using System;
using HurricaneVR.Framework.Core;
using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    [RequireComponent(typeof(MeshRenderer))]
    public class HVRRingHighlight : HVRScaleHighlight
    {
        public Transform Camera;

        [Header("Settings")]
        
        [Tooltip("Ring must be within this distance from the camera to be displayed")]
        public float Distance = 5f;
        

        [Header("Line of Sight Settings")]
        
        [Tooltip("Use ray cast to the camera collider to determine if we should show")]
        public bool RequireLineOfSight = true;
        
        [Tooltip("Layer mask for checking line of sight, include the layer of the camera(default is Player)")]
        public LayerMask LayerMask;
        
        [Tooltip("Check line of sight only if distance greater than this")]
        public float LineOfSightThreshold = 1.5f;
        
        [Tooltip("Timeout to check line of sight")]
        public float Delay = 1f;

        public MeshRenderer Ring { get; private set; }

        private bool _hasCamera;

        protected override void Start()
        {
            if (!Camera && HVRManager.Instance)
            {
                Camera = HVRManager.Instance.Camera;
            }

            _hasCamera = Camera;

            Ring = GetComponent<MeshRenderer>();
        }

        protected override void Update()
        {
            base.Update();

            if (!_hasCamera)
                return;

            transform.LookAt(Camera);

            if (!RequireLineOfSight)
            {
                var distance = Vector3.Distance(transform.position, Camera.position);
                if (distance > Distance)
                {
                    Ring.enabled = false;
                }
                else
                {
                    Ring.enabled = true;
                }
            }
        }

        private void FixedUpdate()
        {
            if (!RequireLineOfSight || !_hasCamera)
                return;

            var distance = Vector3.Distance(transform.position, Camera.position);
            if (distance > Distance)
            {
                Ring.enabled = false;
                return;
            }

            if (distance < LineOfSightThreshold)
            {
                Ring.enabled = true;
                return;
            }

            if (Physics.Raycast(transform.position, (Camera.position - transform.position).normalized, out var hit, Distance, LayerMask, QueryTriggerInteraction.Ignore))
            {
                Ring.enabled = hit.collider.gameObject == Camera.transform.gameObject;
            }
            else
            {
                Ring.enabled = false;
            }
        }

        public override void Enable()
        {
            base.Enable();
            Ring.enabled = true;
        }

        public override void Disable()
        {
            base.Disable();
            Ring.enabled = false;
        }
    }
}