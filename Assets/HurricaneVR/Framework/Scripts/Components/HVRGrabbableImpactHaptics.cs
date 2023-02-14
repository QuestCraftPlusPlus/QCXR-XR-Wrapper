using HurricaneVR.Framework.Core;

namespace HurricaneVR.Framework.Components
{

    public class HVRGrabbableImpactHaptics : HVRImpactHapticsBase
    {
        public HVRGrabbable Grabbable;

        protected override void Awake()
        {
            base.Awake();

            if (!Grabbable) TryGetComponent(out Grabbable);
        }

        protected override void Vibrate(float duration, float amplitude, float frequency)
        {
            for (var i = 0; i < Grabbable.HandGrabbers.Count; i++)
            {
                var h = Grabbable.HandGrabbers[i];
                if (!h.IsMine) break;

                h.Controller.Vibrate(amplitude, Data.Duration, Data.Frequency);
            }
        }
    }
}