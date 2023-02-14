using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents{
    public class HVRPrimaryButtonEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        protected override void CheckInput(HVRController controller)
        {
            if (controller.PrimaryButtonState.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.PrimaryButtonState.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}