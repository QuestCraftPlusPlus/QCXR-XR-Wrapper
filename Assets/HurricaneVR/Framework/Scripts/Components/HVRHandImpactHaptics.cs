using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Player;
using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    public class HVRHandImpactHaptics : HVRImpactHapticsBase
    {
        public HVRHandGrabber Hand;

        [Tooltip("If true and the hand is holding something, haptics will not play.")]
        public bool HandGrabbingPrevents = true;

        [Tooltip("If true the max force for the haptic amplitude calc will use the hand's default max force.")]
        public bool UseHandMaxStrength = true;

        private HVRJointHand _hand;

        public override float MaxForce
        {
            get
            {
                if (UseHandMaxStrength)
                {
                    if ( _hand && _hand.JointSettings)
                        return _hand.JointSettings.XDrive.MaxForce;
                }

                return base.MaxForce;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            if (!Hand) TryGetComponent(out Hand);

            TryGetComponent(out _hand);
        }

        protected override void Vibrate(float duration, float amplitude, float frequency)
        {
            if (HandGrabbingPrevents && Hand.IsGrabbing) return;
            Hand.Controller.Vibrate(amplitude, duration, frequency);
        }
    }
}