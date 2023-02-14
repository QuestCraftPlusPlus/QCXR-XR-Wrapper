using HurricaneVR.Framework.Shared;
using UnityEngine.Events;
namespace HurricaneVR.Framework.ControllerInput.InputEvents{
    public class HVRTrackPadTouchEvents : HVRInputAction
    {
        public UnityEvent Activated = new UnityEvent();
        public UnityEvent Deactivated = new UnityEvent();

        protected override void CheckInput(HVRController controller)
        {
            if (controller.TrackPadTouchState.JustActivated)
            {
                Activated.Invoke();
            }
            else if (controller.TrackPadTouchState.JustDeactivated)
            {
                Deactivated.Invoke();
            }
        }
    }
}