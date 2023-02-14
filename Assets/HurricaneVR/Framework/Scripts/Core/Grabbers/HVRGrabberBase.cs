using System;
using System.Collections.Generic;
using HurricaneVR.Framework.Core.Bags;
using HurricaneVR.Framework.Shared;
using UnityEngine;


namespace HurricaneVR.Framework.Core.Grabbers
{
    public abstract class HVRGrabberBase : MonoBehaviour
    {
        public VRGrabberEvent BeforeGrabbed = new VRGrabberEvent();
        public VRGrabberEvent Grabbed = new VRGrabberEvent();
        public VRGrabberEvent Released = new VRGrabberEvent();
        public VRGrabberEvent BeforeHoverEnter = new VRGrabberEvent();
        public VRGrabberEvent HoverEnter = new VRGrabberEvent();
        public VRGrabberEvent HoverExit = new VRGrabberEvent();


        [Header("Grabbable Bags")]
#pragma warning disable CS0649 
        [SerializeField] private HVRGrabbableBag _grabBag;
#pragma warning restore CS0649
        public List<HVRGrabbableBag> GrabBags = new List<HVRGrabbableBag>();

        public virtual Quaternion ControllerRotation { get; set; } = Quaternion.identity;

        public bool AllowHovering { get; set; }
        public virtual bool AllowGrabbing { get; set; }
        public bool IsGrabbing { get; private set; }
        public bool IsHovering { get; private set; }

        public HVRGrabbable HoverTarget
        {
            get => _hoveredTarget;
            internal set
            {
                _hoveredTarget = value;
                IsHovering = value;
            }
        }

        public HVRGrabbable HeldObject => GrabbedTarget;

        public HVRGrabbable GrabbedTarget
        {
            get => _grabbedTarget;
            protected set
            {
                _grabbedTarget = value;
                IsGrabbing = value;
            }
        }

        public virtual bool IsGrabActivated { get; protected set; }

        public virtual bool IsHoldActive { get; protected set; }

        public virtual bool IsHandGrabber => false;
        public virtual bool IsSocket => false;

        public virtual bool AllowSwap => false;

        public Rigidbody Rigidbody { get; protected set; }

        protected Transform _grabPoint;
        public virtual Transform GrabPoint
        {
            get => _grabPoint;
            set
            {
                _grabPoint = value;
            }
        }


        public virtual Vector3 JointAnchorWorldPosition { get; }

        public virtual bool IsMine { get; set; } = true;
        public virtual bool PerformUpdate { get; set; } = true;

        public bool PullingGrabbable { get; set; }

        private HVRGrabbable _grabbedTarget;
        private HVRGrabbable _hoveredTarget;
        private Ray _lineOfSightRay;

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {
            HVRManager.Instance?.UnregisterGrabber(this);
        }

        protected virtual void OnDestroy()
        {
            HVRManager.Instance?.UnregisterGrabber(this);
        }

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        protected virtual void Start()
        {
            HVRManager.Instance?.RegisterGrabber(this);
         
            AllowGrabbing = true;
            AllowHovering = true;

            if (_grabBag)
            {
                if (!GrabBags.Contains(_grabBag))
                {
                    GrabBags.Add(_grabBag);
                }
            }

            foreach (var bag in GrabBags)
            {
                bag.Grabber = this;
            }
        }

        protected virtual void Update()
        {
            if (PerformUpdate)
            {
                CheckUnHover();
                CheckRelease();
                CheckHover();
                CheckGrab();
            }
        }

        protected virtual void FixedUpdate()
        {

        }


        protected virtual void CheckRelease()
        {
            if (IsGrabbing)
            {
                if (!IsHoldActive)
                    ReleaseGrabbable(this, GrabbedTarget);
            }
        }

        public virtual void ForceRelease()
        {
            //Debug.Log("Force Releasing.");
            if (IsGrabbing)
            {
                ReleaseGrabbable(this, GrabbedTarget);
            }
            else
            {
                //Debug.Log("Nothing to force release.");
            }
        }

        /// <summary>
        /// Returns true if this object should be released from it's existing grabber prior to be grabbed by another.
        /// </summary>
        protected virtual bool CheckSwapReleaseRequired(HVRGrabbable grabbable)
        {
            return grabbable.IsBeingForcedGrabbed || grabbable.PrimaryGrabber && grabbable.PrimaryGrabber.AllowSwap;
        }

        /// <summary>
        /// Will check if this object should be released from it's primary grabber and release it if so.
        /// </summary>
        protected virtual void CheckSwapRelease(HVRGrabbable grabbable)
        {
            if (CheckSwapReleaseRequired(grabbable))
                SwapRelease(grabbable);
        }

        /// <summary>
        /// Releases the grabbable from it's current grabber, this is prior to being grabbed by another grabber.
        /// </summary>
        protected virtual void SwapRelease(HVRGrabbable grabbable)
        {
            grabbable.PrimaryGrabber.ForceRelease();
        }

        /// <summary>
        /// Executes the release sequence on the provided grabber and grabbable.
        /// </summary>
        /// <param name="raiseEvents">If true the Released Unity events on the grabber and grabbable will execute.</param>
        public static void ReleaseGrabbable(HVRGrabberBase grabber, HVRGrabbable grabbable, bool raiseEvents = true, bool isHandSwap = false)
        {
            grabber.OnReleased(grabbable);
            grabbable.InternalOnReleased(grabber);

            if (raiseEvents)
            {
                grabbable.Released.Invoke(grabber, grabbable);
                if (grabber.IsHandGrabber)
                {
                    grabbable.HandReleased.Invoke(grabber as HVRHandGrabber, grabbable);
                    if (grabbable.Grabbers.Count == 0 && !isHandSwap)
                    {
                        grabbable.HandFullReleased.Invoke(grabber as HVRHandGrabber, grabbable);
                    }
                }

                if (grabber.IsSocket)
                {
                    grabbable.UnSocketed.Invoke(grabber as HVRSocket, grabbable);
                }
            }
        }

        public virtual bool IsValidGrabbable(HVRGrabbable grabbable)
        {
            for (var i = 0; i < GrabBags.Count; i++)
            {
                var bag = GrabBags[i];
                if (bag.ValidGrabbables.Contains(grabbable))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the closest grabbable in the grabbable bag.
        /// </summary>
        public virtual HVRGrabbable GetClosestGrabbable()
        {
            for (var i = 0; i < GrabBags.Count; i++)
            {
                var bag = GrabBags[i];
                if (bag.ClosestGrabbable)
                    return bag.ClosestGrabbable;
            }

            return null;
        }

        /// <summary>
        /// Returns the closest grabbable in the grabbable bag that satisfies canGrab delegate.
        /// </summary>
        public virtual HVRGrabbable GetClosestGrabbable(Predicate<HVRGrabbable> canGrab)
        {
            for (var i = 0; i < GrabBags.Count; i++)
            {
                var bag = GrabBags[i];
                if (bag.ClosestGrabbable && canGrab(bag.ClosestGrabbable))
                    return bag.ClosestGrabbable;
            }

            return null;
        }

        protected virtual void CheckGrab()
        {
            if (!IsGrabActivated || !AllowGrabbing || IsGrabbing)
            {
                return;
            }

            for (var g = 0; g < GrabBags.Count; g++)
            {
                var grabBag = GrabBags[g];
                for (var i = 0; i < grabBag.ValidGrabbables.Count; i++)
                {
                    var grabbable = grabBag.ValidGrabbables[i];
                    if (TryGrab(grabbable))
                        break;
                }
            }
        }


        public virtual bool TryGrab(HVRGrabbable grabbable, bool force = false)
        {
            if (force || CanGrab(grabbable))
            {
                GrabGrabbable(this, grabbable);
                return true;
            }

            return false;
        }

        public virtual bool TryGrabNoEvents(HVRGrabbable grabbable, bool force = false)
        {
            if (force || CanGrab(grabbable))
            {
                GrabGrabbable(this, grabbable, false);
                return true;
            }

            return false;
        }



        protected virtual void GrabGrabbable(HVRGrabberBase grabber, HVRGrabbable grabbable, bool raiseEvents = true)
        {
            CheckSwapRelease(grabbable);

            if (raiseEvents)
            {
                grabber.BeforeGrabbed.Invoke(grabber, grabbable);
            }

            grabbable.InternalOnBeforeGrabbed(grabber);
            var args = new HVRGrabArgs(grabbable);
            args.RaiseEvents = raiseEvents;


            grabber.OnBeforeGrabbed(args);

            if (args.Cancel)
            {
                grabbable.InternalOnGrabCanceled(grabber);
                return;
            }

            grabber.GrabbedTarget = grabbable;
            grabber.OnGrabbed(args);

            if (args.Cancel)
            {
                grabber.GrabbedTarget = null;
                grabbable.InternalOnGrabCanceled(grabber);
            }
            else
            {
                grabbable.InternalOnGrabbed(grabber);
                grabber.InternalOnAfterGrabbed(grabbable);
            }
        }

        internal virtual void InternalOnGrabbed(HVRGrabArgs args)
        {
            OnGrabbed(args);
        }

        protected virtual void OnBeforeGrabbed(HVRGrabArgs args)
        {
            
        }

        protected virtual void OnGrabbed(HVRGrabArgs args)
        {
            args.Grabbable.Destroyed.AddListener(OnGrabbableDestroyed);
        }

        internal virtual void InternalOnAfterGrabbed(HVRGrabbable grabbable)
        {
            OnAfterGrabbed(grabbable);
        }

        protected virtual void OnAfterGrabbed(HVRGrabbable grabbable)
        {

        }

        protected virtual void CheckUnHover()
        {
            if (!HoverTarget)
                return;

            var closestValid = ClosestValidHover();

            if (!CanHover(HoverTarget) || closestValid != HoverTarget)
            {
                UnhoverGrabbable(this, HoverTarget);
            }
        }

        protected HVRGrabbable ClosestValidHover(bool triggerOnly)
        {
            for (var g = 0; g < GrabBags.Count; g++)
            {
                var grabBag = GrabBags[g];
                for (var i = 0; i < grabBag.ValidGrabbables.Count; i++)
                {
                    var grabbable = grabBag.ValidGrabbables[i];

                    if (triggerOnly)
                    {
                        if (grabbable.IsSocketed && grabbable.Socket.GrabDetectionType == HVRGrabDetection.Grabbable)
                        {
                            if (grabbable.Socket.GrabControl != HVRGrabControls.TriggerOnly)
                                continue;
                        }
                        else if (grabbable.GrabControl != HVRGrabControls.TriggerOnly)
                            continue;
                    }
                    else
                    {
                        if (grabbable.IsSocketed && grabbable.Socket.GrabDetectionType == HVRGrabDetection.Grabbable)
                        {
                            if (grabbable.Socket.GrabControl == HVRGrabControls.TriggerOnly)
                                continue;
                        }
                        else if (grabbable.GrabControl == HVRGrabControls.TriggerOnly)
                            continue;
                    }

                    if (CanHover(grabbable))
                    {
                        return grabbable;
                    }
                }
            }

            return null;
        }

        protected HVRGrabbable ClosestValidHover()
        {
            for (var g = 0; g < GrabBags.Count; g++)
            {
                var grabBag = GrabBags[g];
                for (var i = 0; i < grabBag.ValidGrabbables.Count; i++)
                {
                    var grabbable = grabBag.ValidGrabbables[i];

                    if (CanHover(grabbable))
                    {
                        return grabbable;
                    }
                }
            }

            return null;
        }


        protected virtual bool CheckHover()
        {
            if (IsHovering || !AllowHovering)
            {
                if (IsHovering && !HoverTarget)
                {
                    HoverTarget = null;
                }
                else
                {
                    return true;
                }
            }

            var closestValid = ClosestValidHover();
            if (!closestValid )
                return false;

            HoverGrabbable(this, closestValid);
            return true;
        }

        protected internal virtual void OnBeforeHover(HVRGrabbable grabbable)
        {

        }

        protected internal virtual void OnAfterHover(HVRGrabbable grabbable)
        {

        }

        protected void HoverGrabbable(HVRGrabberBase grabber, HVRGrabbable grabbable)
        {
            if (grabber.IsHovering)
                return;

            OnBeforeHover(grabbable);
            grabber.BeforeHoverEnter.Invoke(grabber, grabbable);

            grabber.HoverTarget = grabbable;

            grabbable.InternalOnHoverEnter(grabber);
            grabber.OnHoverEnter(grabbable);
            grabbable.HoverEnter.Invoke(grabber, grabbable);
            grabber.HoverEnter.Invoke(grabber, grabbable);
        }

        protected void UnhoverGrabbable(HVRGrabberBase grabber, HVRGrabbable grabbable)
        {
            try
            {
                if (grabbable && grabber)
                {
                    grabbable.InternalOnHoverExit(grabber);
                    grabbable.HoverExit.Invoke(grabber, grabbable);
                }

                if (grabber.HoverTarget)
                {
                    grabber.OnHoverExit(grabbable);
                    grabber.HoverExit.Invoke(grabber, grabbable);
                }
            }
            finally
            {
                if (grabber)
                    grabber.HoverTarget = null;

                OnAfterHover(grabbable);
            }
        }

        public virtual bool CanGrab(HVRGrabbable grabbable)
        {
            if (grabbable.MasterGrabbable && grabbable.MasterGrabbable.IsSocketed)
                return false;
            return AllowGrabbing && !IsGrabbing;
        }

        public virtual bool CanHover(HVRGrabbable grabbable)
        {
            return AllowHovering;
        }



        protected virtual void OnReleased(HVRGrabbable grabbable)
        {
            //Debug.Log($"released {gameObject.name}");
            GrabbedTarget = null;
            grabbable.Destroyed.RemoveListener(OnGrabbableDestroyed);
        }

        protected virtual void OnGrabbableDestroyed(HVRGrabbable grabbable)
        {
            grabbable.BeingDestroyed = true;

            if (IsGrabbing)
            {
                //Debug.Log($"destroyed grab {gameObject.name}");
                GrabbedTarget = null;
                OnReleased(grabbable);
            }
        }

        private void OnHoverGrabbableDestroyed(HVRGrabbable grabbable)
        {
            if (IsHovering)
            {
                //Debug.Log($"destroyed hover {gameObject.name}");
                HoverTarget = null;
                OnHoverExit(grabbable);
                HoverExit.Invoke(this, grabbable);
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected virtual void OnHoverEnter(HVRGrabbable grabbable)
        {
            if(HVRSettings.Instance.VerboseGrabbableEvents)
                Debug.Log($"OnHoverEnter {gameObject.name} -> {grabbable.name}");
            grabbable.Destroyed.AddListener(OnHoverGrabbableDestroyed);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected virtual void OnHoverExit(HVRGrabbable grabbable)
        {
            if(HVRSettings.Instance.VerboseGrabbableEvents)
                Debug.Log($"OnHoverExit {gameObject.name}-> {grabbable.name}");
            grabbable.Destroyed.RemoveListener(OnHoverGrabbableDestroyed);
        }


        public bool CheckForLineOfSight(Vector3 rayOrigin, HVRGrabbable grabbable, LayerMask RaycastLayermask, float rayMaxDistance = .75f, bool useClosestPoint = true)
        {
            if (CheckLineOfSight(rayOrigin, grabbable, RaycastLayermask, rayMaxDistance, grabbable.Colliders, QueryTriggerInteraction.Ignore, useClosestPoint))
                return true;

            if (CheckLineOfSight(rayOrigin, grabbable, RaycastLayermask, rayMaxDistance, grabbable.Triggers, QueryTriggerInteraction.Collide, useClosestPoint))
                return true;

            return false;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private bool CheckLineOfSight(Vector3 rayOrigin, HVRGrabbable grabbable, LayerMask RaycastLayermask, float rayMaxDistance, List<Collider> colliders, QueryTriggerInteraction queryTrigger, bool useClosestPoint = true)
        {
            _lineOfSightRay.origin = rayOrigin;

            for (var i = 0; i < colliders.Count; i++)
            {
                var grabbableCollider = colliders[i];

                if (!grabbableCollider)
                    continue;

                if (!useClosestPoint || grabbable.HasConcaveColliders && grabbableCollider is MeshCollider meshCollider && !meshCollider.convex ||
                    grabbable.HasWheelCollider && grabbableCollider is WheelCollider)
                {
                    _lineOfSightRay.direction = grabbableCollider.bounds.center - _lineOfSightRay.origin;
                }
                else
                {
                    var closestPoint = grabbableCollider.ClosestPoint(rayOrigin);
                    if (closestPoint == rayOrigin && grabbableCollider.bounds.Contains(rayOrigin))
                    {
                        if (HVRSettings.Instance.VerboseGrabbableEvents)
                        {
                            Debug.Log($"Line of sight origin inside collider.");
                        }
                        return true;
                    }
                    _lineOfSightRay.direction = closestPoint - _lineOfSightRay.origin;
                }

                if (Physics.Raycast(_lineOfSightRay, out var hit, rayMaxDistance, RaycastLayermask, queryTrigger))
                {
                    if (Equals(grabbableCollider, hit.collider))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    public class HVRGrabArgs
    {
        public HVRGrabArgs(HVRGrabbable grabbable)
        {
            Grabbable = grabbable;
        }

        public bool Cancel;
        public HVRGrabbable Grabbable;
        public bool RaiseEvents = true;
    }

}