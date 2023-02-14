using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents{
    public class HVRTrackpadButtonEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        protected override void CheckInput(HVRController controller)
        {
            if (controller.TrackpadButtonState.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.TrackpadButtonState.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}