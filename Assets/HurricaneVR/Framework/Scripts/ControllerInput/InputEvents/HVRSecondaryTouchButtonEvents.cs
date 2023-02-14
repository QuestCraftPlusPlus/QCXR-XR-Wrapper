using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents{
    public class HVRSecondaryTouchButtonEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        protected override void CheckInput(HVRController controller)
        {
            if (controller.SecondaryTouchButtonState.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.SecondaryTouchButtonState.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}