using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents{
    public class HVRPrimaryTouchButtonEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        protected override void CheckInput(HVRController controller)
        {
            if (controller.PrimaryTouchButtonState.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.PrimaryTouchButtonState.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}