using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents{
    public class HVRTriggerTouchEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        protected override void CheckInput(HVRController controller)
        {
            if (controller.TriggerTouchState.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.TriggerTouchState.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}