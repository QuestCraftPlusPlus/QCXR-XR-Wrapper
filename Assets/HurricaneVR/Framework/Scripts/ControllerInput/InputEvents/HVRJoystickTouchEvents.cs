using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents{
    public class HVRJoystickTouchEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        protected override void CheckInput(HVRController controller)
        {
            if (controller.JoystickTouchState.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.JoystickTouchState.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}