using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents
{
    public class HVRGripButtonEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        public float Value;

        protected override void CheckInput(HVRController controller)
        {
            Value = controller.Grip;

            if (controller.GripButtonState.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.GripButtonState.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}