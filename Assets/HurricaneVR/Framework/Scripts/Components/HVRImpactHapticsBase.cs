using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    public abstract class HVRImpactHapticsBase : MonoBehaviour
    {
        public HVRImpactHaptics Data;

        [Header("Debug")]
        public float Force;

        private float _lastHaptic;
        private Rigidbody Rb;

        public virtual float MaxForce => Data.MaxForce;
        
        protected virtual void Awake()
        {
            TryGetComponent(out Rb);

            if (!Data)
            {
                Data = ScriptableObject.CreateInstance<HVRImpactHaptics>();
                Data.Reset();
            }
        }

        protected virtual void OnCollisionEnter(Collision other)
        {
            if (!Rb) TryGetComponent(out Rb);

            if (!Rb || Rb.velocity.sqrMagnitude < Data.SqrMagThreshold)
                return;

            if (Time.realtimeSinceStartup - _lastHaptic < Data.Timeout)
                return;

            float impulse;

            if (other.impulse.sqrMagnitude > 0f)
            {
                impulse = other.impulse.magnitude;
            }
            else
            {
                impulse = other.relativeVelocity.magnitude * Rb.mass;
            }

            Force = impulse / Time.fixedDeltaTime;

            var amp = Data.AmpCurve.Evaluate(Force / MaxForce);

            Vibrate(Data.Duration, amp, Data.Frequency);

            _lastHaptic = Time.realtimeSinceStartup;
        }

        protected virtual void Vibrate(float duration, float amplitude, float frequency)
        {

        }
    }
}
