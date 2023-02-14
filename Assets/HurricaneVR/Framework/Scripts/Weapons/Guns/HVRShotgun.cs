using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.Framework.Weapons.Guns
{
    public class HVRShotgun : HVRGunBase
    {
        [Header("Shotgun Settings")]
        public int NumberOfPellets = 5;
        public float ShotRadius = 0.05f;

        protected override void Awake()
        {
            base.Awake();
            Ammo = GetComponent<HVRShotgunMagazine>();
        }

        protected override void OnFire(Vector3 direction)
        {
            for (int i = 0; i < NumberOfPellets; i++)
            {
                var xy = Random.insideUnitCircle * ShotRadius;
                var newDirection = direction + transform.TransformDirection(xy);
                FireBullet(newDirection);
            }
        }

        protected override void OnCockingHandleEjected()
        {
            base.OnCockingHandleEjected();
            if (ChamberedCasing && ChamberedCasing.activeSelf)
            {
                EjectCasing();
            }
        }

        protected override void OnAmmoSocketed(HVRGrabberBase grabber, HVRGrabbable grabbable)
        {
            AmmoSocketedHaptics();
        }

        protected override void OnAmmoSocketReleased(HVRGrabberBase arg0, HVRGrabbable arg1)
        {
            
        }
    }
}