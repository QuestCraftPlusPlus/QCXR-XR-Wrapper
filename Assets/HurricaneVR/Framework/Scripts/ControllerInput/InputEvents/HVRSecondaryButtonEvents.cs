using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents{
    public class HVRSecondaryButtonEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        protected override void CheckInput(HVRController controller)
        {
            if (controller.SecondaryButtonState.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.SecondaryButtonState.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}