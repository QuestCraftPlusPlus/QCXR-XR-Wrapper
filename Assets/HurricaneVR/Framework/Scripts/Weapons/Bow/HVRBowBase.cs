using System.Collections;
using Assets.HurricaneVR.Framework.Shared.Utilities;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Weapons.Bow
{
    [RequireComponent(typeof(HVRGrabbable))]
    [RequireComponent(typeof(Rigidbody))]
    public class HVRBowBase : MonoBehaviour
    {
        [Header("Bow String")]
        public HVRGrabbable NockGrabbable;

        public float StringLimit = .5f;
        public float StringDropLimit = .6f;

        [Header("Settings")]
        public HVRBowLimitStyle StringLimitStyle = HVRBowLimitStyle.Limit;

        public float ShootThreshold = .2f;
        public float Speed = 50f;
        public AnimationCurve SpeedCurve;
        public bool ReverseArrowsRests;


        [Header("Transforms")]
        [Tooltip("Arrow Rest When the bow is held with the left hand.")]
        public Transform LeftRest;

        [Tooltip("Arrow Rest When the bow is held with the right hand.")]
        public Transform RightRest;

        [Tooltip("Transform for forward vector, uses this transform if not provided.")]
        public Transform ForwardMarker;

        [Header("Haptics")]
        public bool StringHaptics = true;

        public bool BowHandHaptics = true;

        [Tooltip("Number of haptic ticks by percent traveled.")]
        [Range(.02f, 1f)]
        public float HapticStep = .01f;

        [Tooltip("Vibration strength when pulling the bow.")]
        [Range(0f, 1f)]
        public float HapticsMinAmplitude = .1f;

        [Tooltip("Vibration strength when pulling the bow.")]
        [Range(0f, 1f)]
        public float HapticsMaxAmplitude = .1f;

        [Tooltip("Vibration frequency when pulling the bow.")]
        public float HapticsDuration = .01f;

        [Tooltip("Vibration frequency when pulling the bow.")]
        public float HapticsFrequency = 1f;

        [Header("SFX")]
        public AudioClip StringClip;

        public float StringMinPitch = 1f;
        public float StringMaxPitch = 1.25f;

        public AudioClip[] ReleasedSFX;


        public Transform Rest { get; protected set; }
        public float Tension { get; protected set; }

        public Vector3 Forward => transform.InverseTransformDirection(ForwardMarker.forward);
        public Vector3 WorldForward => ForwardMarker.forward;

        public bool ArrowNocked => Arrow;

        protected HVRArrow Arrow { get; set; }

        public HVRNockingPoint NockSocket { get; private set; }
        public HVRGrabbable Grabbable { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        protected HVRHandGrabber NockHand;

        protected HVRHandGrabber BowHand;

        private Vector3 _nockStart;
        private Vector3 _nockDir;
        private float _nockDistance;
        private bool _previousHeld;
        private float _previousHapticDistance;
        private float _shootSpeed;
        private float _previousArrowSleep;

        protected virtual void Start()
        {
            if (!ForwardMarker)
            {
                ForwardMarker = this.transform;
            }

            Rest = LeftRest;

            NockSocket = GetComponentInChildren<HVRNockingPoint>();

            Grabbable = GetComponent<HVRGrabbable>();
            Rigidbody = GetComponent<Rigidbody>();

            NockSocket.BeforeHoverEnter.AddListener(BeforeNockHovered);
            NockSocket.Grabbed.AddListener(OnArrowSocketed);

            Grabbable.Grabbed.AddListener(OnGrabbed);
            Grabbable.Released.AddListener(OnReleased);
            Grabbable.HandGrabbed.AddListener(OnHandGrabbed);
            Grabbable.HandReleased.AddListener(OnHandReleased);
            Grabbable.Socketed.AddListener(OnBowSocketed);
            Grabbable.UnSocketed.AddListener(OnBowUnsocketed);

            NockGrabbable.HandGrabbed.AddListener(OnStringGrabbed);
            NockGrabbable.HandReleased.AddListener(OnStringReleased);

            NockSocket.ParentGrabbable = Grabbable;

            _nockStart = transform.InverseTransformPoint(NockGrabbable.transform.position);


            if (SpeedCurve == null)
            {
                SpeedCurve = new AnimationCurve();
            }

            if (SpeedCurve.keys.Length == 0)
            {
                SpeedCurve.AddKey(0f, 1f);
                SpeedCurve.AddKey(1f, 1f);
            }
        }


        private void Update()
        {
            UpdateBow();
        }

        protected virtual void UpdateBow()
        {
        }

        private void FixedUpdate()
        {
            BeforeFixedUpdateBow();
            FixedUpdateBow();
            AfterFixedUpdateBow();
        }

        protected virtual void BeforeFixedUpdateBow()
        {
        }

        protected virtual void AfterFixedUpdateBow()
        {
        }

        protected virtual void FixedUpdateBow()
        {
            var nockPosition = transform.InverseTransformPoint(NockGrabbable.transform.position);

            _nockDir = nockPosition - _nockStart;
            _nockDistance = _nockDir.magnitude;
            Tension = _nockDistance / StringLimit;
            Tension = Mathf.Clamp(Tension, 0f, 1f);

            UpdateHaptics(_nockDistance);

            CheckDropArrow();
            CheckArrowRelease();

            _previousHeld = NockGrabbable.IsBeingHeld;
        }

        public void NockArrow(HVRArrow arrow)
        {
            if (Arrow)
                return;

            OnArrowNocked(arrow);
        }

        protected virtual void CheckArrowRelease()
        {
            var shootArrow = false;

            if (StringLimitStyle == HVRBowLimitStyle.ShootArrow && _nockDistance > StringLimit)
            {
                NockGrabbable.ForceRelease();
                shootArrow = Arrow;
            }

            if (!Arrow)
                return;

            if (!shootArrow && _previousHeld && !NockGrabbable.IsBeingHeld && _nockDistance > ShootThreshold)
            {
                shootArrow = true;
            }

            if (shootArrow)
            {
                OnArrowShot();
            }
        }

        protected virtual void OnArrowShot()
        {
            OnArrowRemoved(Arrow);

            _shootSpeed = SpeedCurve.Evaluate(Tension) * Speed;

            PlayReleasedSFX();

            ShootArrow(Arrow.transform.forward);
        }

        protected virtual void ShootArrow(Vector3 direction)
        {
            Arrow.Rigidbody.sleepThreshold = _previousArrowSleep;
            Arrow.Grabbable.CanBeGrabbed = true;
            Arrow.Rigidbody.velocity = direction * _shootSpeed;
            Arrow.Rigidbody.angularVelocity = Vector3.zero;
            Arrow.Flying = true;
            Arrow = null;
        }

        protected virtual void CheckDropArrow()
        {
            if (StringLimitStyle != HVRBowLimitStyle.DropArrow || !Arrow)
                return;

            if (_nockDistance > StringDropLimit)
            {
                NockGrabbable.ForceRelease();
                OnArrowDropped();
            }
        }

        protected virtual void UpdateHaptics(float nockDistance)
        {
            if (nockDistance > _previousHapticDistance + HapticStep ||
                nockDistance < _previousHapticDistance - HapticStep)
            {
                var amplitude = nockDistance.Remap(0, StringLimit, HapticsMinAmplitude, HapticsMaxAmplitude);

                if (StringHaptics && NockGrabbable.HandGrabbers.Count > 0)
                    NockGrabbable.HandGrabbers[0].Controller.Vibrate(amplitude, HapticsDuration, HapticsFrequency);

                if (BowHandHaptics && Grabbable.HandGrabbers.Count > 0)
                    Grabbable.HandGrabbers[0].Controller.Vibrate(amplitude, HapticsDuration, HapticsFrequency);

                PlayStringSFX(nockDistance);


                _previousHapticDistance = nockDistance;
            }
        }

        protected virtual void PlayStringSFX(float nockDistance)
        {
            var pitch = nockDistance.Remap(0, StringLimit, StringMinPitch, StringMaxPitch);
            if (SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(StringClip, NockGrabbable.transform.position, pitch, 1f);
        }

        protected virtual void PlayReleasedSFX()
        {
            if (SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(ReleasedSFX.GetRandom(), NockGrabbable.transform.position);
        }

        protected virtual void OnGrabbed(HVRGrabberBase arg0, HVRGrabbable arg1)
        {
        }

        protected virtual void OnReleased(HVRGrabberBase arg0, HVRGrabbable arg1)
        {
        }

        private void BeforeNockHovered(HVRGrabberBase grabber, HVRGrabbable grabbable)
        {
            NockHand = grabbable.PrimaryGrabber as HVRHandGrabber;
        }

        protected virtual void OnArrowSocketed(HVRGrabberBase arg0, HVRGrabbable grabbable)
        {
            var arrow = grabbable.transform.GetComponent<HVRArrow>();
            if (!arrow)
            {
                NockHand = null;
                Debug.LogWarning($"{grabbable.name} missing HVRArrow component.");
                return;
            }


            OnArrowNocked(arrow);
        }

        protected virtual void OnArrowNocked(HVRArrow arrow)
        {
            _previousArrowSleep = arrow.Rigidbody.sleepThreshold;

            arrow.transform.rotation = Quaternion.LookRotation(WorldForward, NockSocket.transform.up);
            arrow.transform.position = NockSocket.transform.position;


            var grabbable = arrow.Grabbable;
            //grabbable.ForceRelease();
            grabbable.CanBeGrabbed = false;
            grabbable.Rigidbody.sleepThreshold = 0f;
            grabbable.Grabbed.AddListener(OnNockedArrowGrabbed);

            UpdateBowHandCollision(BowHand, grabbable, false);

            NockSocket.AllowGrabbing = false;
            Arrow = arrow;

            Grabbable.IgnoreCollision(grabbable);

            if (NockHand)
            {
                NockHand.DisableHandCollision(Arrow.Grabbable);
                NockHand.TryGrab(NockGrabbable, true);
                NockHand = null;
            }

            arrow.EnableForwardGrabbable();
        }

        protected virtual void OnNockedArrowGrabbed(HVRGrabberBase arg0, HVRGrabbable arg1)
        {
            OnArrowDropped();
        }

        protected virtual void OnHandGrabbed(HVRHandGrabber hand, HVRGrabbable bow)
        {
            BowHand = hand;

            if (Arrow)
            {
                UpdateBowHandCollision(hand, Arrow.Grabbable, false);
            }

            if (hand.HandSide == HVRHandSide.Left && !ReverseArrowsRests)
            {
                Rest = LeftRest;
            }
            else
            {
                Rest = RightRest;
            }
        }

        protected virtual void OnHandReleased(HVRHandGrabber arg0, HVRGrabbable arg1)
        {
            if (Arrow)
            {
                StartCoroutine(EnableBowHandCollisionRoutine(arg0, Arrow.Grabbable));
            }

            BowHand = null;
        }

        protected virtual void OnStringReleased(HVRHandGrabber arg0, HVRGrabbable arg1)
        {
        }

        protected virtual void OnStringGrabbed(HVRHandGrabber hand, HVRGrabbable nock)
        {
        }

        protected virtual void OnBowSocketed(HVRSocket arg0, HVRGrabbable arg1)
        {
            if (Arrow)
            {
                OnArrowDropped();
            }
        }

        protected virtual void OnBowUnsocketed(HVRSocket arg0, HVRGrabbable arg1)
        {
        }

        protected virtual void OnArrowDropped()
        {
            OnArrowRemoved(Arrow);

            Arrow.Rigidbody.sleepThreshold = _previousArrowSleep;
            Arrow.Grabbable.CanBeGrabbed = true;

            Arrow = null;
        }

        protected virtual void OnArrowRemoved(HVRArrow arrow)
        {
            this.ExecuteAfterSeconds(() => NockSocket.AllowGrabbing = true, .25f);

            StartCoroutine(EnableBowHandCollisionRoutine(BowHand, arrow.Grabbable));

            arrow.Grabbable.Grabbed.RemoveListener(OnNockedArrowGrabbed);

            arrow.DisableForwardGrabbable();
        }

        protected void UpdateBowHandCollision(HVRHandGrabber hand, HVRGrabbable arrow, bool enable)
        {
            if (hand && arrow)
            {
                hand.UpdateCollision(arrow, enable);
            }
        }

        protected IEnumerator EnableBowHandCollisionRoutine(HVRHandGrabber hand, HVRGrabbable arrow)
        {
            if (!hand || !arrow)
                yield break;
            yield return new WaitForSeconds(1f);
            if (BowHand && BowHand == hand || !arrow)
                yield break;
            UpdateBowHandCollision(hand, arrow, true);
            Grabbable.IgnoreCollision(arrow, false);
        }

        public void OnDrawGizmosSelected()
        {
            if (NockGrabbable && Rest)
            {
                var forward = Rest.transform.position - NockGrabbable.transform.position;
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(NockGrabbable.transform.position - forward.normalized * ShootThreshold, .02f);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(NockGrabbable.transform.position - forward.normalized * StringLimit, .02f);
            }
        }
    }

    public enum HVRBowLimitStyle
    {
        Limit,
        ShootArrow,
        DropArrow
    }

    public class HVRBowEvent : UnityEvent<HVRPhysicsBow>
    {
    }
}