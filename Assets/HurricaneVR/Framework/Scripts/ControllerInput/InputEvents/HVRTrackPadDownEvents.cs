using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents{
    public class HVRTrackPadDownEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        protected override void CheckInput(HVRController controller)
        {
            if (controller.TrackPadDown.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.TrackPadDown.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}