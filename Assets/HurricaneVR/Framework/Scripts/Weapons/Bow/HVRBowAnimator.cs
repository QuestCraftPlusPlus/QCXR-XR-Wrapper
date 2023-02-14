using UnityEngine;

namespace HurricaneVR.Framework.Weapons.Bow
{
    [RequireComponent(typeof(HVRPhysicsBow))]
    public class HVRBowAnimator : MonoBehaviour
    {
        public Animator Animator;
        public HVRPhysicsBow Bow { get; private set; }

        public void Start()
        {
            Bow = GetComponent<HVRPhysicsBow>();
        }

        public void Update()
        {
            if (Animator)
            {

                Animator.enabled = true;
                Animator.Play(0, 0, Bow.Tension);
            }
        }
    }
}