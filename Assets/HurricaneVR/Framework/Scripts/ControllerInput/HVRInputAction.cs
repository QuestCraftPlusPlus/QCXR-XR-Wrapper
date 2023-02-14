using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.ControllerInput
{
    [RequireComponent(typeof(HVRGrabbable))]
    public abstract class HVRInputAction : MonoBehaviour
    {
        public HVRGrabbable Grabbable { get; private set; }

        protected virtual void Awake()
        {
            Grabbable = GetComponent<HVRGrabbable>();
        }

        public void Update()
        {
            for (var i = 0; i < Grabbable.HandGrabbers.Count; i++)
            {
                var hand = Grabbable.HandGrabbers[i];
                var controller = HVRInputManager.Instance.GetController(hand.HandSide);
                CheckInput(controller);
            }
        }

        protected abstract void CheckInput(HVRController controller);
    }
}