using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents
{
    public class HVRTriggerButtonEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        public float Value;

        protected override void CheckInput(HVRController controller)
        {
            Value = controller.Trigger;

            if (controller.TriggerButtonState.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.TriggerButtonState.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}