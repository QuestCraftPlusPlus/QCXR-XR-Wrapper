using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents{
    public class HVRJoystickButtonEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        protected override void CheckInput(HVRController controller)
        {
            if (controller.JoystickButtonState.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.JoystickButtonState.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}