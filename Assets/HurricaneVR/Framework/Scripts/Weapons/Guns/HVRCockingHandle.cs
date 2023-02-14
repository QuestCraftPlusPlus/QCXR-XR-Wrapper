using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Weapons.Guns
{
    public class HVRCockingHandle : HVRGunPart
    {

        [Header("Settings")]

        [Tooltip("Forward speed of the slide when released")]
        public float ForwardSpeed = 10f;

        [Tooltip("Faux difficulty for pulling back the handle")]
        public float Difficulty = .05f;

        [Tooltip("If the handle was popped open due to out of ammo, it will release immediately upon being grabbed")]
        public bool ImmediateReleaseWhenOpen = true;

        [Tooltip("If true, locks back even if the handle isn't reciprocating")]
        public bool LockBackOverride;

        public HVRCockingHandleType Type;

        [Header("Lock Options")]
        public bool LocksForward;

        public bool TriggerUnlocks;
        public float TriggerThreshold = .7f;

        [Tooltip("Hand must move this fast to unlock the handle")]
        public float AccelerationThreshold = 2f;

        [Header("Components")]

        public HVRGrabbable Grabbable;

        [Tooltip("Bolt that moves with the charging handle, pump")]
        public HVRGunBolt Bolt;


        [Header("Editor Positions")]

        [Tooltip("Position to reach to eject the chambered round")]
        public Vector3 EjectPosition;

        [Tooltip("Position to reach that will chamber a round.")]
        public Vector3 ChamberRoundPosition;

        public List<HVRGunPart> AnimatedParts = new List<HVRGunPart>();

        [Header("Slide Events")]
        public UnityEvent Released = new UnityEvent();
        public UnityEvent EjectReached = new UnityEvent();
        public UnityEvent ChamberRound = new UnityEvent();


        private Transform _grabbedPositionTracker;


        /// <summary>
        /// The offset of the cocking handle when it's grabbed
        /// </summary>
        protected float GrabOffset { get; set; }

        public bool EmptyOpen { get; protected set; }

        public Vector3 ForwardPositionWorld => transform.parent.TransformPoint(ForwardPosition);
        public Vector3 MaxPositionWorld => transform.parent.TransformPoint(BackwardPosition);

        public Vector3 BackDirectionWorld => (MaxPositionWorld - ForwardPositionWorld).normalized;

        private float _chamberedRequiredDistance;
        private bool _chamberDistanceReached;

        private float _maximumDistance;

        private float _ejectDistance;
        private bool _ejectDistanceReached;

        private float _lockDistance;
        private bool _lockDistanceReached;

        private Vector3 _previousHandPosition;
        private Vector3 _previousVelocity;
        private Vector3 _handAcceleration;

        private bool _locked = true;

        private Coroutine _forwardRoutine;

        public virtual bool LocksBack
        {
            get
            {
                if (Type == HVRCockingHandleType.Reciprocating)
                {
                    return true;
                }

                return LockBackOverride;
            }
        }

        protected virtual void Awake()
        {
            if (!Grabbable)
            {
                Grabbable = GetComponent<HVRGrabbable>();
            }

            if (Grabbable)
            {
                Grabbable.HandGrabbed.AddListener(OnGrabbed);
                Grabbable.HandReleased.AddListener(OnReleased);
            }
            else
            {
                Debug.LogError($"{name} is missing it's grabbable component.");
            }



            var go = new GameObject("GrabTracker");
            go.transform.parent = transform.parent;
            _grabbedPositionTracker = go.transform;

            _chamberedRequiredDistance = Vector3.Distance(ChamberRoundPosition, ForwardPosition);
            _maximumDistance = Vector3.Distance(BackwardPosition, ForwardPosition);
            _ejectDistance = Vector3.Distance(EjectPosition, ForwardPosition);
            _lockDistance = _maximumDistance * .05f;
        }

        public void Enable()
        {
            Grabbable.CanBeGrabbed = true;
        }

        public void Disable()
        {
            Grabbable.CanBeGrabbed = false;
        }

        protected virtual void Update()
        {
            if (Grabbable.HandGrabbers.Count > 0)
            {
                UpdateHandTracking();

                if (_locked)
                {
                    if (CheckUnlock())
                    {
                        Unlock();
                    }
                }

                if (_locked && LocksForward)
                {
                    return;
                }

                var pullDirection = (Grabbable.HandGrabbers[0].TrackedController.position - _grabbedPositionTracker.position);
                var backDirection = (MaxPositionWorld - ForwardPositionWorld).normalized * 10;
                var amount = Vector3.Dot(pullDirection, backDirection) * Difficulty;

                if (amount + GrabOffset > 0)
                {
                    UpdatePosition(amount);
                }
                else
                {
                    transform.localPosition = ForwardPosition;
                }
                
                var distance = Vector3.Distance(transform.position, ForwardPositionWorld);

                CheckEject(distance);
                CheckChamberDistance(distance);
                ClampPullBack(distance, BackDirectionWorld);
                MoveBolt();
                AnimateParts();
                CheckLock(distance);
            }
        }

        protected virtual void CheckLock(float distance)
        {
            if (!LocksForward)
                return;

            if (distance > _lockDistance && !_lockDistanceReached)
            {
                _lockDistanceReached = true;
            }
            else if (distance < _lockDistance && _lockDistanceReached)
            {
                Close();
                _lockDistanceReached = false;
            }
        }

        private void UpdatePosition(float amount)
        {
            transform.position = ForwardPositionWorld + BackDirectionWorld * (amount + GrabOffset);
        }


        public void Lock()
        {
            _locked = true;
            //Debug.Log($"lock");
        }

        public void Unlock()
        {
            _locked = false;
            //Debug.Log($"unlock");
        }

        protected virtual bool CheckUnlock()
        {
            if (!LocksForward)
                return true;

            if (TriggerUnlocks)
            {
                if (Grabbable.HandGrabbers.Count > 0)
                {
                    if (Grabbable.HandGrabbers[0].Controller.Trigger > TriggerThreshold)
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (_handAcceleration.magnitude > AccelerationThreshold)
                {
                    return true;
                }
            }


            return false;
        }

        protected virtual void CheckEject(float distance)
        {
            if (distance > _ejectDistance && !_ejectDistanceReached)
            {
                EjectReached.Invoke();
                _ejectDistanceReached = true;
            }
            else if (distance < _ejectDistance && _ejectDistanceReached)
            {
                _ejectDistanceReached = false;
            }
        }

        protected virtual void CheckChamberDistance(float distance)
        {
            if (distance > _chamberedRequiredDistance)
            {
                if (!_chamberDistanceReached)
                {
                    _chamberDistanceReached = true;
                }
            }
            else if (distance < _chamberedRequiredDistance && _chamberDistanceReached)
            {
                _chamberDistanceReached = false;
                ChamberRound.Invoke();
            }
        }

        protected virtual void ClampPullBack(float distance, Vector3 backDirection)
        {
            if (distance > _maximumDistance)
            {
                transform.localPosition = BackwardPosition;
            }
        }

        protected virtual void UpdateHandTracking()
        {
            if (!Grabbable.IsBeingHeld)
                return;

            if (Grabbable.PrimaryGrabber.IsHandGrabber)
            {
                var hand = Grabbable.HandGrabbers[0];
                var velocity = hand.TrackedController.position - _previousHandPosition;
                _handAcceleration = (velocity - _previousVelocity) / Time.deltaTime;
                _previousVelocity = velocity;
                _previousHandPosition = hand.TrackedController.position;
            }
        }

        protected virtual void MoveBolt()
        {
            if (Bolt)
            {
                if (_ejectDistance == 0f)
                {
                    return;
                }

                var percent = Vector3.Distance(transform.localPosition, ForwardPosition) / _maximumDistance;

                if (percent > .90)
                {
                    Bolt.IsPushedBack = false;
                }

                if (Bolt.IsPushedBack && percent < .90)
                    return;
                Bolt.Move(percent);
            }
        }

        protected virtual void AnimateParts()
        {
            var percent = Vector3.Distance(transform.localPosition, ForwardPosition) / _maximumDistance;

            foreach (var part in AnimatedParts)
            {
                part.Animate(percent, CycleDirection.Backward);
            }
        }

        public void Move(float percent)
        {
            transform.localPosition = Vector3.Lerp(ForwardPosition, BackwardPosition, percent);
        }

        protected virtual void OnGrabbed(HVRHandGrabber grabber, HVRGrabbable slide)
        {
            GrabOffset = 0f;
            
            if (ImmediateReleaseWhenOpen && EmptyOpen && LocksBack)
            {
                if (Bolt)
                {
                    Bolt.IsPushedBack = false;
                }
                Grabbable.ForceRelease();
            }

            _previousHandPosition = grabber.TrackedController.position;
            _previousVelocity = Vector3.zero;
            if (Type == HVRCockingHandleType.Pump)
            {
                _grabbedPositionTracker.position = grabber.GrabPoint.position;
            }
            else
            {
                _grabbedPositionTracker.localPosition = transform.parent.InverseTransformPoint(_previousHandPosition);
                GrabOffset = Vector3.Distance(ForwardPositionWorld, transform.position);
            }
        }

        
        
        protected virtual void OnReleased(HVRHandGrabber grabber, HVRGrabbable slide)
        {
            EmptyOpen = false;
            if (Type != HVRCockingHandleType.Pump)
            {
                Close();
            }
        }

        public void Close()
        {
            if (_forwardRoutine != null)
                return;
            StartCoroutine(ForwardRoutine());
        }

        protected virtual IEnumerator ForwardRoutine()
        {
            Grabbable.CanBeGrabbed = false;

            while (true)
            {
                var distance = Vector3.Distance(transform.localPosition, ForwardPosition);
                var travel = ForwardSpeed * Time.deltaTime;

                if (distance < travel)
                {
                    transform.localPosition = ForwardPosition;
                    MoveBolt();
                    AnimateParts();
                    break;
                }

                transform.localPosition = Vector3.MoveTowards(transform.localPosition, ForwardPosition, travel);
                AnimateParts();
                MoveBolt();

                yield return null;
            }

            _forwardRoutine = null;
            Grabbable.CanBeGrabbed = true;

            if (_chamberDistanceReached)
            {
                ChamberRound.Invoke();
            }

            Released.Invoke();
            _chamberDistanceReached = false;
            _ejectDistanceReached = false;
            EmptyOpen = false;

            if (LocksForward)
            {
                Lock();
            }
        }

        public virtual void PushBack()
        {
            if (LocksBack)
            {
                transform.localPosition = EjectPosition;
                EmptyOpen = true;
                _chamberDistanceReached = true;
                if (Bolt)
                {
                    Bolt.Move(1f);
                }
            }
        }

        public override void Animate(float percent, CycleDirection direction)
        {
            if (Type == HVRCockingHandleType.Reciprocating)
            {
                Move(percent);
            }
        }

        //public void OnDrawGizmosSelected()
        //{
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawCube(ForwardPositionWorld, new Vector3(.025f, .025f, .025f));
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawCube(MaxPositionWorld, new Vector3(.025f, .025f, .025f));
        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawCube(transform.parent.TransformPoint(EjectPosition), new Vector3(.025f, .025f, .025f));
        //    Gizmos.color = Color.cyan;
        //    Gizmos.DrawCube(transform.parent.TransformPoint(ChamberRoundPosition), new Vector3(.025f, .025f, .025f));
        //}
    }

    public enum HVRCockingHandleType
    {
        Reciprocating, NonReciprocating, Pump
    }
}