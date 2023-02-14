using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents{
    public class HVRTrackPadUpEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        protected override void CheckInput(HVRController controller)
        {
            if (controller.TrackPadUp.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.TrackPadUp.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}