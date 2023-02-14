using HurricaneVR.Framework.Core.Stabbing;
using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    public class DemoFullStabConfetti : MonoBehaviour
    {
        public HVRStabbable Stabbable;
        public ParticleSystem Confetti;
        public bool OnFullStab;

        private Vector3 point;
        private Vector3 normal;

        public void Start()
        {
            Stabbable = GetComponentInParent<HVRStabbable>();
            TryGetComponent(out Confetti);

            if (Stabbable)
            {
                Stabbable.Stabbed.AddListener(Stabbed);
                Stabbable.UnStabbed.AddListener(Unstabbed);
                Stabbable.FullStabbed.AddListener(FullStabbed);
            }
        }

        private void FullStabbed(HVRStabber arg0, HVRStabbable arg1)
        {
            if (OnFullStab)
                PopConfetti(normal);
        }

        private void PopConfetti(Vector3 dir)
        {
            Confetti.transform.position = Stabbable.transform.TransformPoint(point);
            Confetti.transform.rotation = Quaternion.FromToRotation(Confetti.transform.up, dir) * Confetti.transform.rotation;
            Confetti.Stop();
            Confetti.Play();
        }

        private void Unstabbed(HVRStabber arg0, HVRStabbable arg1)
        {
        }

        private void Stabbed(StabArgs stabArgs)
        {
            normal = stabArgs.Normal;
            point = Stabbable.transform.InverseTransformPoint(stabArgs.Point);
            if (!OnFullStab)
                PopConfetti(normal);
        }
    }
}