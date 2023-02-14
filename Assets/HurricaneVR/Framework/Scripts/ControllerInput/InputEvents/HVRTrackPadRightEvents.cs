using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents{
    public class HVRTrackPadRightEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        protected override void CheckInput(HVRController controller)
        {
            if (controller.TrackPadRight.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.TrackPadRight.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}