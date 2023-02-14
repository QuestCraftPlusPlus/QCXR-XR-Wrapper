using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace HurricaneVR.Framework.Weapons.Guns
{
    public class HVRGunBase : HVRDamageProvider
    {

        public HVRGrabbable Grabbable { get; private set; }

        [Header("Settings")]
        public float TriggerPullThreshold = .7f;
        public float TriggerResetThreshold = .6f;

        [Tooltip("Cooldown before the next shot")]
        public float Cooldown = .0666667f;

        [Tooltip("Physics layers for the ray cast")]
        public LayerMask HitLayerMask;
        public float MuzzleFlashTime = .2f;

        [Tooltip("Flexible bullet range per gun type")]
        public float BarrelRange = 10;

        [Tooltip("Does this gun require ammo inserted to shoot")]
        public bool RequiresAmmo = true;

        [Tooltip("Is chambering required to shoot")]
        public bool RequiresChamberedBullet = true;
        public GunFireType FireType = GunFireType.Single;

        [Tooltip("Speed of the bullet prefab")]
        public float BulletSpeed = 40f;

        [Tooltip("How fast to kick the magazine out of the gun")]
        public float AmmoEjectVelocity = 1f;

        [Tooltip("How long until we destroy the muzzle smoke object")]
        public float MuzzleSmokeTime = 1.5f;

        [Tooltip("Should the gun automatically chamber the next round after firing")]
        public bool ChambersAfterFiring = true;

        [Tooltip("Should the gun automatically eject a casing after firing")]
        public bool EjectCasingAfterFiring = true;

        [Tooltip("Should the gun automatically push the bolt back after out of ammo")]
        public bool BoltPushedBackAfterEmpty = true;

        [Tooltip("If true will use damage, force, range, from the ammo")]
        public bool UseAmmoProperties;

        [Tooltip("If not using ammo properties, range of the bullet")]
        public float NoAmmoRange = 40f;

        [Tooltip("If true adds force on hit to everything")]
        public bool AddForceOnHit = true;

        [Header("Haptics")]
        public HVRGunHaptics Haptics;
        public List<HVRGrabbable> HapticGrabbables = new List<HVRGrabbable>();

        [Header("Objects")]

        [Tooltip("Muzzle flash object")]
        public GameObject MuzzleFlashObject;

        [Tooltip("Muzzle smoke object")]
        public GameObject MuzzleSmoke;

        public GameObject ChamberedRound;
        public GameObject ChamberedCasing;

        [Header("Required Transforms")]

        [Tooltip("Optional Direction to eject Ammo - use the z axis")]
        public Transform AmmoEjectDirection; //forward

        [Tooltip("Where the bullet should come from, z forward direction")]
        public Transform BulletOrigin;

        [Header("Components")]
        public HVRGunEmitterBase BulletEmitter;
        public HVRGunEmitterBase CasingEmitter;
        public HVRCockingHandle CockingHandle;
        public HVRGunBolt Bolt;


        [Tooltip("If this grabbable is held, the StabilizedRecoilForce is used when shooting.")]
        public HVRGrabbable StabilizerGrabbable;

        [Tooltip("Recoil settings component")]
        public HVRRecoil RecoilComponent;

        [Tooltip("Socket for taking in ammo / magazines")]
        public HVRSocket AmmoSocket;

        [Tooltip("Component that handls gun sfx")]
        public HVRGunSounds GunSounds;

        [Header("Animation")]
        public HVRTriggerAnimator TriggerAnimator;
        public float CyclingTime;
        public bool AnimateGun = true;
        public Animator Animator;
        public string FireParameter = "Fire";

        public int MaxPooledBullets = 100;

        [Header("Projectile")]
        public bool SlowMotionBulletOnly;
        public GameObject BulletPrefab;
        public float BulletLife = 5f;

        private readonly List<HVRBulletTracker> _objects = new List<HVRBulletTracker>();
        private HVRGunPart[] _animatableGunParts;
        private Coroutine _animationRoutine;

        public UnityEvent Fired = new UnityEvent();
        public HVRGunHitEvent Hit = new HVRGunHitEvent();

        protected float TimeOfLastShot { get; set; }

        public bool IsBulletChambered { get; set; }

        public HVRAmmo Ammo { get; set; }

        public HVRGrabbable AmmoGrabbable { get; set; }

        public float BulletRange
        {
            get
            {
                if (UseAmmoProperties && Ammo)
                    return Ammo.MaxRange + BarrelRange;
                return BarrelRange + NoAmmoRange;
            }
        }

        public HVRDamageProvider DamageProvider
        {
            get
            {
                if (UseAmmoProperties && Ammo)
                    return Ammo;
                return this;
            }
        }

        public bool OutOfAmmo
        {
            get
            {
                if (RequiresChamberedBullet && IsBulletChambered)
                    return false;

                if (!RequiresAmmo)
                    return false;

                if (!Ammo || Ammo.IsEmpty)
                    return true;

                return false;
            }
        }

        public bool IsFiring { get; protected set; }

        protected int RoundsFired { get; set; }

        protected virtual void Awake()
        {
            Grabbable = GetComponent<HVRGrabbable>();

            Grabbable.HandReleased.AddListener(OnHandReleased);
            Grabbable.HandGrabbed.AddListener(OnHandGrabbed);

            if (StabilizerGrabbable)
            {
                StabilizerGrabbable.Grabbed.AddListener(OnStabilizerGrabbed);
                StabilizerGrabbable.Released.AddListener(OnStabilizerReleased);
            }

            if (AmmoSocket)
            {
                AmmoSocket.Grabbed.AddListener(OnAmmoSocketed);
                AmmoSocket.Released.AddListener(OnAmmoSocketReleased);
                AmmoSocket.ParentDisablesGrab = true;
                AmmoSocket.ParentGrabbable = Grabbable;
            }

            if (!RecoilComponent)
            {
                RecoilComponent = GetComponent<HVRRecoil>();
            }

            if (!GunSounds)
            {
                GunSounds = GetComponent<HVRGunSounds>();
            }

            if (!Animator)
            {
                Animator = GetComponent<Animator>();
            }

            if (CockingHandle)
            {
                CockingHandle.Released.AddListener(OnCockingHandleReleased);
                CockingHandle.EjectReached.AddListener(OnCockingHandleEjected);
                CockingHandle.ChamberRound.AddListener(OnCockingHandleChambered);
            }

            SetupPooledBullets();

            _animatableGunParts = GetComponentsInChildren<HVRGunPart>();

            if (BulletEmitter)
            {
                BulletEmitter.Gun = this;
            }

            if (CasingEmitter)
            {
                CasingEmitter.Gun = this;
            }
        }




        protected virtual void SetupPooledBullets()
        {
            for (int i = 0; i < MaxPooledBullets; i++)
            {
                _objects.Add(new HVRBulletTracker());

                if (BulletPrefab)
                {
                    _objects[i].Bullet = Instantiate(BulletPrefab);
                }
                else
                {
                    _objects[i].Bullet = new GameObject("Bullet");
                }

                _objects[i].Bullet.SetActive(false);
                _objects[i].Bullet.hideFlags = HideFlags.HideInHierarchy;
                _objects[i].Renderers = _objects[i].Bullet.GetComponentsInChildren<Renderer>();
            }
        }


        protected virtual void Update()
        {
            CheckTriggerHaptics();
            CheckTriggerPull();
            UpdateTrackedBullets();
            UpdateTriggerAnimation();
            UpdateShooting();
        }

        protected virtual void CheckTriggerHaptics()
        {
            if (Grabbable.HandGrabbers.Count == 0 || !Grabbable.HandGrabbers[0].IsMine || !Haptics) return;

            var controller = Grabbable.HandGrabbers[0].Controller;

            if (controller.Trigger > Haptics.TriggerSqueezeStart && controller.Trigger < Haptics.TriggerSqueezeStop)
            {
                PlayHaptics(Grabbable, Haptics.TriggerSqueezed);
            }
        }

        public bool IsTriggerReset;// { get; set; }
        public bool IsTriggerPulled; //{ get; set; }

        protected virtual void CheckTriggerPull()
        {
            if (!Grabbable.IsHandGrabbed)
                return;

            var controller = Grabbable.HandGrabbers[0].Controller;

            if (controller.Trigger <= TriggerResetThreshold)
            {
                IsTriggerReset = true;
            }

            if (controller.Trigger > TriggerPullThreshold && IsTriggerReset)
            {
                TriggerPulled();
                IsTriggerReset = false;
                IsTriggerPulled = true;
            }
            else if (controller.Trigger < TriggerPullThreshold && IsTriggerPulled)
            {
                IsTriggerPulled = false;
                TriggerReleased();
            }
        }

        protected virtual void UpdateShooting()
        {
            if (IsFiring)
            {
                if (!CanFire())
                {
                    IsFiring = false;
                    return;
                }

                if (Time.time - TimeOfLastShot > Cooldown)
                {
                    Shoot();
                }
            }
        }

        protected virtual void OnCockingHandleChambered()
        {
            TryChamberRound();
            CockingHandleChamberedHaptics();
        }

        protected virtual void OnCockingHandleEjected()
        {
            if (GunSounds)
                GunSounds.PlaySlideBack();

            EjectBullet();
            CockingHandleEjectHaptics();
        }

        protected virtual void OnCockingHandleReleased()
        {
            if (GunSounds)
                GunSounds.PlaySlideForward();

            if (Bolt)
            {
                Bolt.IsPushedBack = false;
            }

            CockingHandleReleasedHaptics();
        }

        protected virtual void EnableChamberedRound()
        {
            if (!IsBulletChambered)
                return;

            if (ChamberedRound)
            {
                ChamberedRound.SetActive(true);
            }
        }

        protected virtual void EnableChamberedCasing()
        {
            if (ChamberedCasing)
            {
                ChamberedCasing.SetActive(true);
            }
        }

        protected virtual void DisableChamberedCasing()
        {
            if (ChamberedCasing)
            {
                ChamberedCasing.SetActive(false);
            }
        }

        protected virtual void DisableChamberedRound()
        {
            if (ChamberedRound)
            {
                ChamberedRound.SetActive(false);
            }
        }

        protected virtual void OnHandReleased(HVRHandGrabber arg0, HVRGrabbable arg1)
        {
            if (RecoilComponent)
            {
                RecoilComponent.HandRigidBody = null;
            }

            TriggerReleased();
        }

        protected virtual void OnHandGrabbed(HVRHandGrabber hand, HVRGrabbable arg1)
        {
            if (RecoilComponent)
            {
                RecoilComponent.HandRigidBody = hand.Rigidbody;
            }

            if (AmmoGrabbable)
            {
                hand.DisableHandCollision(AmmoGrabbable);
            }
        }

        private void OnStabilizerReleased(HVRGrabberBase grabber, HVRGrabbable arg1)
        {
            Grabbable.ForceTwoHandSettings = false;

            if (RecoilComponent)
            {
                RecoilComponent.TwoHanded = false;
            }
        }

        private void OnStabilizerGrabbed(HVRGrabberBase grabber, HVRGrabbable arg1)
        {
            if (Grabbable.PrimaryGrabber && Grabbable.PrimaryGrabber.IsHandGrabber)
            {
                Grabbable.ForceTwoHandSettings = true;

                if (RecoilComponent)
                {
                    RecoilComponent.TwoHanded = true;
                }
            }
        }



        protected virtual void OnAmmoSocketed(HVRGrabberBase grabber, HVRGrabbable grabbable)
        {
            var ammo = grabbable.GetComponent<HVRAmmo>();
            if (!ammo)
            {
                Debug.Log($"{grabbable.name} is missing the ammo component.");
                return;
            }

            Ammo = ammo;
            AmmoGrabbable = grabbable;

            for (var i = 0; i < Grabbable.Colliders.Count; i++)
            {
                var ourCollider = Grabbable.Colliders[i];
                for (var j = 0; j < grabbable.Colliders.Count; j++)
                {
                    var ammoCollider = grabbable.Colliders[j];
                    Physics.IgnoreCollision(ourCollider, ammoCollider, true);
                }
            }

            if (Grabbable.HandGrabbers.Count > 0)
            {
                Grabbable.HandGrabbers[0].HandPhysics.IgnoreCollision(grabbable.Colliders, true);
            }

            AmmoSocketedHaptics();
        }

        protected virtual void AmmoSocketedHaptics()
        {
            if (Haptics) PlayHaptics(Grabbable, Haptics.AmmoSocketed);
        }

        protected virtual void OnAmmoSocketReleased(HVRGrabberBase arg0, HVRGrabbable arg1)
        {
            if (Ammo && !Ammo.HasAmmo && Ammo.DestroyIfEmpty)
            {
                Ammo.StartDestroy();
            }

            if (Ammo && AmmoGrabbable)
            {
                AfterAmmoReleased(AmmoGrabbable, Ammo);
            }
            Ammo = null;
            AmmoGrabbable = null;
            AmmoSocketReleasedHaptics();
        }

        protected virtual void AmmoSocketReleasedHaptics()
        {
            if (Haptics) PlayHaptics(Grabbable, Haptics.AmmoSocketReleased);
        }

        public virtual void ReleaseAmmo()
        {
            if (!AmmoSocket || !AmmoSocket.IsGrabbing)
            {
                return;
            }

            var releasedAmmo = Ammo;
            Ammo = null;
            AmmoGrabbable = null;
            var ammoGrabbable = AmmoSocket.GrabbedTarget;
            AmmoSocket.ForceRelease();

            if (ammoGrabbable)
            {
                EjectAmmo(ammoGrabbable);
                AfterAmmoReleased(ammoGrabbable, releasedAmmo);
            }
        }

        protected virtual void EjectAmmo(HVRGrabbable ammoGrabbable)
        {
            var direction = -AmmoSocket.transform.up;
            if (AmmoEjectDirection)
            {
                direction = AmmoEjectDirection.forward;
            }

            if (ammoGrabbable.Rigidbody)
            {
                ammoGrabbable.Rigidbody.velocity = direction.normalized * AmmoEjectVelocity;
            }
        }

        protected virtual void AfterAmmoReleased(HVRGrabbable ammoGrabbable, HVRAmmo releasedAmmo)
        {
            var hand = Grabbable.PrimaryGrabber as HVRHandGrabber;
            if (hand)
            {
                hand.HandPhysics.IgnoreCollision(ammoGrabbable.Colliders, true);
            }

            if (!releasedAmmo.HasAmmo && releasedAmmo.DestroyIfEmpty)
            {
                releasedAmmo.StartDestroy();
            }
            else
            {
                StartCoroutine(RenablePhysics(ammoGrabbable, hand));
            }
        }

        public virtual void TriggerPulled()
        {
            if (!CanFire())
            {
                PlayDryFire();
                DryFireHaptics();
                return;
            }

            if (FireType == GunFireType.Single)
            {
                if (Time.time - TimeOfLastShot < Cooldown)
                    return;

                TimeOfLastShot = Time.time;

                Shoot();
                return;
            }

            if (IsFiring && FireType == GunFireType.ThreeRoundBurst)
                return;

            IsFiring = true;
            RoundsFired = 0;
        }

        public virtual void TriggerReleased()
        {
            if (FireType == GunFireType.Automatic)
            {
                IsFiring = false;
            }

            TriggerReleasedHaptics();
        }

        public virtual void TriggerReleasedHaptics()
        {
            if (!Haptics) return;
            PlayHaptics(Grabbable, Haptics.TriggeredReleased);
        }

        protected virtual void PlayDryFire()
        {
            if (GunSounds)
            {
                GunSounds.PlayOutOfAmmo();
            }
        }

        protected virtual void DryFireHaptics()
        {
            if (!Haptics) return;
            PlayHaptics(Grabbable, Haptics.DryFire);
        }

        private void UpdateTrackedBullets()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                var tracker = _objects[i];
                var bullet = tracker.Bullet;
                if (bullet.activeSelf)
                {
                    var speed = _objects[i].Speed;
                    var distance = speed * Time.deltaTime;

                    if (Physics.Raycast(bullet.transform.position, tracker.Direction, out var hit, distance, HitLayerMask, QueryTriggerInteraction.Ignore))
                    {
                        OnHit(hit, tracker.Direction);
                        bullet.SetActive(false);
                        _objects[i].SetRenderersActive(false);
                    }
                    else
                    {
                        tracker.Elapsed += Time.deltaTime;
                        tracker.DistanceTraveled += distance;
                        if (tracker.Elapsed > tracker.TimeToLive || tracker.DistanceTraveled > tracker.Range)
                        {
                            bullet.SetActive(false);
                        }
                        else
                        {
                            bullet.transform.position += .95f * distance * tracker.Direction;
                        }
                    }
                }
            }
        }

        public virtual void UpdateTriggerAnimation()
        {
            if (!TriggerAnimator)
                return;

            if (Grabbable.HandGrabbers.Count == 1)
            {
                var controller = Grabbable.HandGrabbers[0].Controller;
                TriggerAnimator.Animate(controller.Trigger);
            }
            else
            {
                TriggerAnimator.Animate(0f);
            }
        }



        private IEnumerator RenablePhysics(HVRGrabbable grabbable, HVRHandGrabber hand)
        {
            yield return new WaitForSeconds(1);

            for (var i = 0; i < Grabbable.Colliders.Count; i++)
            {
                var ourCollider = Grabbable.Colliders[i];
                for (var j = 0; j < grabbable.Colliders.Count; j++)
                {
                    var ammoCollider = grabbable.Colliders[j];
                    Physics.IgnoreCollision(ourCollider, ammoCollider, false);
                }
            }

            if (hand)
            {
                hand.HandPhysics.IgnoreCollision(grabbable.Colliders, false);
            }
        }

        protected virtual void Recoil()
        {
            if (RecoilComponent)
            {
                RecoilComponent.Recoil();
                return;
            }
        }

        protected virtual bool CanFire()
        {
            if (RequiresChamberedBullet)
            {
                return IsBulletChambered;
            }

            return !RequiresAmmo || Ammo && Ammo.HasAmmo;
        }

        protected virtual void PlaySFX()
        {
            if (GunSounds)
            {
                GunSounds.PlayGunFire();
            }
        }

        protected virtual void Shoot()
        {
            OnShoot();
            AfterFired();
            TimeOfLastShot = Time.time;
            RoundsFired++;

            if (FireType == GunFireType.ThreeRoundBurst && RoundsFired == 3)
            {
                IsFiring = false;
            }
        }

        protected virtual void OnShoot()
        {
            PlaySFX();
            Recoil();
            Smoke();
            MuzzleFlash();
            OnFire(BulletOrigin.forward);
            IsBulletChambered = false;
            DisableChamberedRound();
            EnableChamberedCasing();
            TryReload();
            if (AnimateGun && !OutOfAmmo)
            {
                Animate();
            }

            if (OutOfAmmo && _animationRoutine != null)
            {
                StopCoroutine(_animationRoutine);
            }

            PlayAnimator();
            Fired.Invoke();
        }

        protected virtual void Animate()
        {
            if (_animationRoutine != null)
                StopCoroutine(_animationRoutine);

            _animationRoutine = StartCoroutine(AnimationRoutine());
        }

        protected IEnumerator AnimationRoutine()
        {
            var elapsed = 0f;

            var time = CyclingTime * .5f;
            while (elapsed < time)
            {
                var percent = elapsed / time;

                foreach (var part in _animatableGunParts)
                {
                    part.Animate(percent, CycleDirection.Backward);
                }

                yield return null;
                elapsed += Time.deltaTime;
            }

            EjectCasing();

            elapsed = 0f;

            var roundenabled = false;
            while (elapsed < time)
            {
                var percent = 1 - elapsed / time;

                foreach (var part in _animatableGunParts)
                {
                    part.Animate(percent, CycleDirection.Forward);
                }

                if (percent > .5f && !roundenabled)
                {
                    roundenabled = true;
                    EnableChamberedRound();
                }

                yield return null;
                elapsed += Time.deltaTime;
            }

            if (!roundenabled)
            {
                EnableChamberedRound();
            }

            foreach (var part in _animatableGunParts)
            {
                part.Animate(0f, CycleDirection.Forward);
            }

            _animationRoutine = null;
        }

        protected virtual void TryReload()
        {
            if (OutOfAmmo)
            {
                OnOutOfAmmo();
            }
            else if (ChambersAfterFiring)
            {
                TryChamberRound();
            }
            else if (!RequiresChamberedBullet && RequiresAmmo && Ammo && Ammo.HasAmmo)
            {
                Ammo.RemoveBullet();
            }
        }

        public virtual void TryChamberRound()
        {
            if (IsBulletChambered)
            {
                return;
            }

            if (Ammo && Ammo.HasAmmo)
            {
                Ammo.RemoveBullet();
                IsBulletChambered = true;
            }

            if (IsBulletChambered)
            {
                EnableChamberedRound();
            }
        }

        protected virtual void OnOutOfAmmo()
        {
            if (Animator)
            {
                Animator.enabled = false;
            }

            if (CockingHandle)
            {
                CockingHandle.PushBack();
            }

            if (EjectCasingAfterFiring)
            {
                EjectCasing();
            }
            if (Bolt && BoltPushedBackAfterEmpty)
            {
                Bolt.PushBack();
            }
        }

        protected virtual void PlayAnimator()
        {
            if (!OutOfAmmo && Animator)
            {
                Animator.enabled = true;
                Animator.SetTrigger(FireParameter);
            }
        }

        protected virtual void OnFire(Vector3 direction)
        {
            FireBullet(direction);
            FireHaptics();
        }

        protected virtual void FireHaptics()
        {
            if (!Haptics) return;
            PlayHapticsAllHands(Haptics.Fire);
        }

        protected virtual void CockingHandleEjectHaptics()
        {
            if (!Haptics) return;
            PlayHaptics(CockingHandle.Grabbable, Haptics.CockingHandleEject);
            PlayHaptics(Grabbable, Haptics.CockingHandleEject);
        }

        protected virtual void CockingHandleReleasedHaptics()
        {
            if (!Haptics) return;
            PlayHaptics(CockingHandle.Grabbable, Haptics.CockingHandleReleased);
            PlayHaptics(Grabbable, Haptics.CockingHandleReleased);
        }

        protected virtual void CockingHandleChamberedHaptics()
        {
            if (!Haptics) return;
            PlayHaptics(CockingHandle.Grabbable, Haptics.CockingHandleChamberedRound);
            PlayHaptics(Grabbable, Haptics.CockingHandleChamberedRound);
        }

        protected virtual void PlayHapticsAllHands(HapticData haptic)
        {
            PlayHaptics(Grabbable, haptic);

            foreach (var other in HapticGrabbables)
            {
                PlayHaptics(other, haptic);
            }
        }

        protected virtual void PlayHaptics(HVRGrabbable grabbable, HapticData data)
        {
            if (grabbable.HandGrabbers.Count == 0 || !grabbable.HandGrabbers[0].IsMine || data == null) return;

            var controller = grabbable.HandGrabbers[0].Controller;
            controller.Vibrate(data);
        }

        protected virtual void FireBullet(Vector3 direction)
        {
            var bullet = GetFreeBullet();
            bullet.Reset();
            //bullet.Bullet = Instantiate(BulletPrefab);
            //bullet.Bullet.hideFlags = HideFlags.HideInHierarchy;
            bullet.Range = BulletRange;
            bullet.Direction = direction;
            bullet.TimeToLive = BulletLife;
            bullet.Speed = BulletSpeed;
            bullet.Bullet.transform.position = BulletOrigin.position;
            bullet.Bullet.SetActive(true);
            bullet.Bullet.transform.rotation = Quaternion.FromToRotation(bullet.Bullet.transform.forward, direction) *
                                               bullet.Bullet.transform.rotation;
            bullet.SetRenderersActive(!SlowMotionBulletOnly || HVRTimeManager.Instance.IsTimeSlowed);

        }

        protected virtual void AfterFired()
        {

        }

        protected virtual void MuzzleFlash()
        {
            if (MuzzleFlashObject)
            {
                StartCoroutine(MuzzleFlashRoutine());
            }
        }

        private IEnumerator MuzzleFlashRoutine()
        {
            MuzzleFlashObject.SetActive(false);/// ADDED to cancel longer fx like smoke to allow flame fx to fire again.
            MuzzleFlashObject.SetActive(true);
            var elapsed = 0f;

            while (elapsed < MuzzleFlashTime)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            MuzzleFlashObject.SetActive(false);
        }

        protected virtual void Smoke()
        {
            if (MuzzleSmoke)
            {
                var muzzleSmoke = Instantiate(MuzzleSmoke, MuzzleSmoke.transform.position, MuzzleSmoke.transform.rotation);
                muzzleSmoke.SetActive(true);
                Destroy(muzzleSmoke, MuzzleSmokeTime);
            }
        }

        private HVRBulletTracker GetFreeBullet()
        {
            HVRBulletTracker tracker = _objects[0];

            var time = -1f;

            for (var i = 0; i < _objects.Count; i++)
            {
                if (!_objects[i].Bullet.activeSelf)
                {
                    return _objects[i];
                }

                if (_objects[i].Elapsed > time)
                {
                    tracker = _objects[i];
                    time = _objects[i].Elapsed;
                }
            }

            return tracker;
        }

        //call from animation
        public void DisableFireAnimator()
        {
            Animator.enabled = false;
        }


        protected virtual void OnHit(RaycastHit hit, Vector3 direction)
        {
            var damageHandler = hit.collider.GetComponent<HVRDamageHandlerBase>();
            if (damageHandler)
            {
                damageHandler.HandleDamageProvider(DamageProvider, hit.point, direction);
                damageHandler.HandleRayCastHit(DamageProvider, hit);
            }


            if (AddForceOnHit && hit.collider.attachedRigidbody)
            {
                hit.collider.attachedRigidbody.AddForceAtPosition(direction.normalized * Force, hit.point, ForceMode.Impulse);
            }

            Hit.Invoke(new GunHitArgs(this, direction, hit));
        }

        public virtual void EjectBullet()
        {
            if (IsBulletChambered)
            {
                IsBulletChambered = false;
                DisableChamberedRound();

                if (OutOfAmmo)
                {
                    if (CockingHandle)
                    {
                        CockingHandle.PushBack();
                    }
                }

                if (BulletEmitter)
                {
                    BulletEmitter.Emit();
                }
            }
        }

        public virtual void EjectCasing()
        {
            if (CasingEmitter)
            {
                CasingEmitter.Emit();
            }

            DisableChamberedCasing();
        }

        public virtual void IgnoreCollision(Collider[] colliders, float time)
        {
            StartCoroutine(IgnoreCollisionRoutine(colliders, time));
        }

        protected virtual IEnumerator IgnoreCollisionRoutine(Collider[] colliders, float time)
        {
            for (var i = 0; i < colliders.Length; i++)
            {
                var c = colliders[i];
                if (c && c.enabled)
                {
                    for (var j = 0; j < Grabbable.Colliders.Count; j++)
                    {
                        var ourCollider = Grabbable.Colliders[j];
                        if (ourCollider && ourCollider.enabled)
                        {
                            Physics.IgnoreCollision(c, ourCollider, true);
                        }
                    }
                }
            }

            yield return new WaitForSeconds(time);

            foreach (var c in colliders)
            {
                if (c && c.enabled)
                {
                    for (var i = 0; i < Grabbable.Colliders.Count; i++)
                    {
                        var ourCollider = Grabbable.Colliders[i];
                        if (ourCollider && ourCollider.enabled)
                        {
                            Physics.IgnoreCollision(c, ourCollider, false);
                        }
                    }
                }
            }

        }

        private class HVRBulletTracker
        {
            public GameObject Bullet;
            public float Elapsed;
            public float TimeToLive;
            public float Speed;
            public Vector3 Direction;
            public float DistanceTraveled;
            public float Range;
            public Renderer[] Renderers;

            public void Reset()
            {
                Elapsed = 0f;
                DistanceTraveled = 0f;
            }

            public void SetRenderersActive(bool active)
            {
                if (Renderers != null)
                {
                    foreach (var r in Renderers)
                    {
                        r.enabled = active;
                    }
                }
            }
        }
    }
    
    [Serializable]
    public class HVRGunHitEvent : UnityEvent<GunHitArgs>
    {

    }

    public struct GunHitArgs
    {
        public HVRGunBase Gun;
        public Vector3 HitPoint;
        public Vector3 Normal;
        public Vector3 Direction;
        public Collider Collider;
        public Rigidbody Rb;
        public float Distance;

        public GunHitArgs(HVRGunBase gun, Vector3 direction, RaycastHit hit)
        {
            Gun = gun;
            HitPoint = hit.point;
            Normal = hit.normal;
            Direction = direction;
            Collider = hit.collider;
            Rb = hit.collider.attachedRigidbody;
            Distance = hit.distance;
        }
    }
    
    public enum GunFireType
    {
        Single,
        ThreeRoundBurst,
        Automatic
    }
}