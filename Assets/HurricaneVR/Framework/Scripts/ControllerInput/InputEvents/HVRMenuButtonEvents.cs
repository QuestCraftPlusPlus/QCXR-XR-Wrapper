using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents{
    public class HVRMenuButtonEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        protected override void CheckInput(HVRController controller)
        {
            if (controller.MenuButtonState.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.MenuButtonState.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}