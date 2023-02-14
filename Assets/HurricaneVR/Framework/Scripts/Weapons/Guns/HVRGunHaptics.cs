using System;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Weapons.Guns
{
    [CreateAssetMenu(menuName = "HurricaneVR/GunHaptics", fileName = "GunHaptics")]
    public class HVRGunHaptics : ScriptableObject
    {
        public void Reset()
        {
            Fire = new HapticData(.20f, .70f, 150f);
            DryFire = new HapticData(.04f, .5f, 50f);
            TriggerSqueezed = new HapticData(.1f, .01f, 60f);
            TriggeredReleased = new HapticData(.025f, .2f, 45f);

            CockingHandleEject = new HapticData(.05f, 200f, .22f);
            CockingHandleChamberedRound = new HapticData(.05f, 200f, .22f);
            CockingHandleReleased = new HapticData(.05f, 200f, .22f);
            AmmoSocketed = new HapticData(.05f, 200f, .22f);
            AmmoSocketReleased = new HapticData(.05f, 200f, .22f);
        }

        public HapticData DryFire;
        public HapticData Fire;
        public HapticData TriggeredReleased;
        public HapticData TriggerSqueezed;

        public HapticData CockingHandleEject;
        public HapticData CockingHandleChamberedRound;
        public HapticData CockingHandleReleased;

        public HapticData AmmoSocketed;
        public HapticData AmmoSocketReleased;

        public float TriggerSqueezeStop = .7f;
        public float TriggerSqueezeStart = .1f;
    }
}