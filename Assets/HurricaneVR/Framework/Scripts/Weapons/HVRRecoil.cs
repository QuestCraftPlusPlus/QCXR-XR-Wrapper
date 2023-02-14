using System;
using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace HurricaneVR.Framework.Weapons
{
    public class HVRRecoil : MonoBehaviour
    {
        public HVRRecoilSettings Settings;

        public UpRecoilType UpRecoilType;

        public Transform UpRecoil;
        public Transform BackRecoil;

        public Vector3 CurrentForce;

        [Tooltip("If the gun is rotated set to true to reverse the x torque direction")]
        public bool TorqueAxisReversed;

        private float _timeSinceLastRecoil;
        private float _recoveryTimer;
        private bool _recoil;


        public Rigidbody Rigidbody => UpRecoilType == Weapons.UpRecoilType.TorqueHand ? HandRigidBody : GunRigidBody;

        public Rigidbody HandRigidBody { get; set; }

        public Rigidbody GunRigidBody { get; set; }

        public bool TwoHanded { get; set; }

        public float SideToSide
        {
            get
            {
                if (!Settings || !Settings.RandomSideToSideRecoil)
                {
                    return 0f;
                }

                if (TwoHanded)
                    return Random.Range(Settings.TwoHandSideToSideMin, Settings.TwoHandSideToSideMax);

                return Random.Range(Settings.SideToSideMin, Settings.SideToSideMax);
            }
        }

        private void Start()
        {
            GunRigidBody = gameObject.GetRigidbody();
        }

        private void FixedUpdate()
        {
            _timeSinceLastRecoil += Time.fixedDeltaTime;

            if (!Rigidbody || !Settings)
                return;

            ApplyRecoil();
            CheckRecovery();
            ApplyConstantForce();


            _recoil = false;
        }

        private void ApplyRecoil()
        {
            if (_recoil)
            {
                var upForce = Settings.UpForce;
                var backForce = Settings.BackwardsForce;

                if (TwoHanded && Settings.UseTwoHandRecoilForce)
                {
                    upForce = Settings.TwoHandUpForce;
                    backForce = Settings.TwoHandBackwardsForce;
                }

                if (Settings.ImpulseForce)
                {
                    ApplyImpulseRecoil(upForce, backForce);
                }
                else
                {
                    CurrentForce.y += upForce;
                    CurrentForce.z += backForce;
                }

                if (Settings.RandomSideToSideRecoil)
                {
                    //Rigidbody.AddForceAtPosition(UpRecoil.right * SideToSide, UpRecoil.position, ForceMode.Impulse);
                    Rigidbody.AddTorque(transform.up * SideToSide, ForceMode.Impulse);
                    CurrentForce.x += SideToSide / Time.fixedDeltaTime;
                }
            }

            if (Settings.LimitRecoilForce)
            {
                var maxForce = Settings.MaxUpForce;
                if (Settings.UseTwoHandMaxUpforce && TwoHanded)
                {
                    maxForce = Settings.TwoHandMaxUpForce;
                }

                var maxSideForce = Settings.MaxSideForce;
                if (Settings.UseTwoHandMaxSideForce && TwoHanded)
                {
                    maxSideForce = Settings.TwoHandMaxSideForce;
                }

                CurrentForce.x = Mathf.Clamp(CurrentForce.x, -maxSideForce, maxSideForce);
                CurrentForce.y = Mathf.Clamp(CurrentForce.y, 0, maxForce);
                CurrentForce.z = Mathf.Clamp(CurrentForce.z, 0, Settings.MaxBackForce);
            }
        }

        private void CheckRecovery()
        {
            var delay = TwoHanded ? Settings.TwoHandedRecoveryDelay : Settings.RecoveryDelay;

            if (_timeSinceLastRecoil > delay)
            {
                var recoveryTime = TwoHanded ? Settings.TwoHandedRecoveryTime : Settings.RecoveryTime;
                _recoveryTimer += Time.fixedDeltaTime;
                var percentRecovered = Mathf.Clamp(_recoveryTimer / recoveryTime, 0, 1);

                CurrentForce *= 1 - percentRecovered;
            }
        }

        private void ApplyConstantForce()
        {
            if (UpRecoilType == UpRecoilType.UpRecoil)
            {
                if (UpRecoil)
                {
                    Rigidbody.AddForceAtPosition(UpRecoil.up * CurrentForce.y, UpRecoil.position, ForceMode.Force);
                }
            }
            else
            {
                Rigidbody.AddTorque(transform.right * (CurrentForce.y * (TorqueAxisReversed ? -1 : 1)), ForceMode.Force);
            }

            Rigidbody.AddTorque(transform.up * CurrentForce.x, ForceMode.Force);

            if (BackRecoil)
            {
                Rigidbody.AddForceAtPosition(BackRecoil.forward * CurrentForce.z, BackRecoil.position, ForceMode.Force);
            }
        }

        private void ApplyImpulseRecoil(float upForce, float backForce)
        {
            if (UpRecoilType == Weapons.UpRecoilType.UpRecoil)
            {
                if (UpRecoil)
                {
                    Rigidbody.AddForceAtPosition(UpRecoil.up * upForce, UpRecoil.position, ForceMode.Impulse);
                }
            }
            else
            {
                Rigidbody.AddTorque(transform.right * (upForce *( TorqueAxisReversed ? -1 : 1)), ForceMode.Impulse);
            }


            if (BackRecoil)
            {
                Rigidbody.AddForceAtPosition(BackRecoil.forward * backForce, BackRecoil.position, ForceMode.Impulse);
            }

            CurrentForce.y += upForce / Time.fixedDeltaTime;
            CurrentForce.z += backForce / Time.fixedDeltaTime;
        }

        public void Recoil()
        {
            _recoil = true;
            _timeSinceLastRecoil = 0f;
            _recoveryTimer = 0f;
        }
    }

    public enum UpRecoilType
    {
        UpRecoil,
        TorqueHand,
        TorqueGun
    }
}