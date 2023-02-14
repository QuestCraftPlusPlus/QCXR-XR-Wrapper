using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents{
    public class HVRTrackPadLeftEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        protected override void CheckInput(HVRController controller)
        {
            if (controller.TrackPadLeft.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.TrackPadLeft.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}