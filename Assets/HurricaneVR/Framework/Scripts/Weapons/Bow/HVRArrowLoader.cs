using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.Framework.Weapons.Bow
{
    [RequireComponent(typeof(HVRPhysicsBow))]
    public class HVRArrowLoader : MonoBehaviour
    {
        public HVRGrabbable NockGrabbable;
        public GameObject ArrowPrefab;

        public HVRPhysicsBow bow { get; private set; }

        public void Start()
        {
            bow = GetComponent<HVRPhysicsBow>();

            if (NockGrabbable)
            {
                NockGrabbable.HandGrabbed.AddListener(OnStringGrabbed);
            }
        }

        private void OnStringGrabbed(HVRHandGrabber hand, HVRGrabbable arg1)
        {
            if (!bow.ArrowNocked && ArrowPrefab)
            {
                var go = Instantiate(ArrowPrefab);
                var arrow = go.GetComponent<HVRArrow>();
                bow.NockArrow(arrow);
                hand.DisableHandCollision(arrow.Grabbable);
            }
        }
    }
}