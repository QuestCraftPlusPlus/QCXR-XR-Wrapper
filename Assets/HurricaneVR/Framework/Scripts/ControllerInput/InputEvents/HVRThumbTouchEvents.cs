using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents{
    public class HVRThumbTouchEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        protected override void CheckInput(HVRController controller)
        {
            if (controller.ThumbTouchState.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.ThumbTouchState.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}