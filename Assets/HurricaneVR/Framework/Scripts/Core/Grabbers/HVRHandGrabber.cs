using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.HurricaneVR.Framework.Shared.Utilities;
using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Core.Bags;
using HurricaneVR.Framework.Core.HandPoser;
using HurricaneVR.Framework.Core.HandPoser.Data;
using HurricaneVR.Framework.Core.Player;
using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using HurricaneVR.Framework.Shared.Utilities;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace HurricaneVR.Framework.Core.Grabbers
{
    public class HVRHandGrabber : HVRGrabberBase
    {
        internal const int TrackedVelocityCount = 10;


        [Tooltip("HVRSocketBag used for placing and removing from sockets")]
        public HVRSocketBag SocketBag;

        [Header("HandSettings")]
        [Tooltip("Set to true if the HandModel is an IK target")]
        public bool InverseKinematics;

        [Tooltip("If true the default hand layer will be applied to this object on start")]
        public bool ApplyHandLayer = true;

        [Header("Grab Settings")]
        [Tooltip("If true the hand will move to the grabbable instead of pulling the grabbable to the hand")]
        public bool HandGrabs;

        [Tooltip("Hand move speed when HandGrabs = true")]
        public float HandGrabSpeed = 5f;

        [Tooltip("When dynamic grabbing the palm faces closest point on the collider surface before closing the fingers.")]
        public bool DynamicGrabPalmAdjust;

        [Tooltip("If in a networked game, can someone take this an object from your hand?")]
        public bool AllowMultiplayerSwap;


        [Tooltip("Hold down or Toggle grabbing")]
        public HVRGrabTrigger GrabTrigger = HVRGrabTrigger.Active;

        [Tooltip("Left or right hand.")]
        public HVRHandSide HandSide;

        public HVRHandPoser GrabPoser;
        public HVRHandPoser HoverPoser;

        [Tooltip("If true the hand model will be cloned for collision use, and colliders removed off the original hand. This will prevent" +
                 "unwanted center of mass and inertia tensor recalculations on grabbable objects due to hand model parenting.")]
        public bool CloneHandModel;

        [Tooltip("Ignores hand model parenting distance check.")]
        public bool IgnoreParentingDistance;

        [Tooltip("Ignores hand model parenting angle check.")]
        public bool IgnoreParentingAngle;

        [Tooltip("Angle to meet before hand model parents to the grabbable.")]
        public float ParentingMaxAngleDelta = 10f;

        [Tooltip("Distance to meet before hand model parents to the grabbable")]
        public float ParentingMaxDistance = .01f;

        [Tooltip("Settings used to pull and rotate the object into position")]
        public HVRJointSettings PullingSettings;

        [Tooltip("Layer mask to determine line of sight to the grabbable.")]
        public LayerMask RaycastLayermask;

        [Tooltip("How quickly does the push out box collider go from zero to full size.")]
        public float PushoutTime = .1f;

        [Header("Pull Settings")]
        [Tooltip("Lerps between grabbable starting position and final hand posed position over this amount of time")]
        public float PullLerpTime = .06f;

        [Tooltip("If the grabbable still isn't in pose orientation after the timeout, the hand will retrieve the object if the pose rotation delta is greater than this.")]
        public float MoveThreshold = 10f;

        [Header("Components")]
        [Tooltip("The hand animator component, loads from children on startup if not supplied.")]
        public HVRHandAnimator HandAnimator;

        [Tooltip("Component that holds collider information about the hands. Auto populated from children if not set.")]
        public HVRHandPhysics HandPhysics;

        public HVRPlayerInputs Inputs;
        public HVRPhysicsPoser PhysicsPoser;
        public HVRForceGrabber ForceGrabber;

        public HVRControllerOffset ControllerOffset;
        public HVRTeleportCollisonHandler CollisionHandler;

        [Tooltip("Used to push objects away from the hand, or unstuck the hand if desired by calling StartPushing.")]
        public BoxCollider Pusher;

        [Header("Grab Indicators")]
        public HVRGrabbableHoverBase GrabIndicator;

        public HVRGrabbableHoverBase TriggerGrabIndicator;
        public HVRGrabbableHoverBase DynamicPoseIndicator;
        public DynamicPoseGrabIndicator DynamicPoseIndicatorMode = DynamicPoseGrabIndicator.Palm;

        [Tooltip("Default hand pose to fall back to.")]
        public HVRHandPoser FallbackPoser;

        [Header("Required Transforms")]
        [Tooltip("Object holding the hand model.")]
        public Transform HandModel;

        [Tooltip("Configurable joints are anchored here")]
        public Transform JointAnchor;

        [Tooltip("Used to shoot ray casts at the grabbable to check if there is line of sight before grabbing.")]
        public Transform RaycastOrigin;

        [Tooltip("The transform that is handling device tracking.")]
        public Transform TrackedController;

        [Tooltip("Sphere collider that checks when collisions should be re-enabled between a released grabbable and this hand.")]
        public Transform OverlapSizer;

        [Header("Throw Settings")]
        [Tooltip("Factor to apply to the linear velocity of the throw.")]
        public float ReleasedVelocityFactor;

        [Tooltip("Factor to apply to the angular to linear calculation.")]
        public float ReleasedAngularConversionFactor = 1.0f;

        [Tooltip("Hand angular velocity must exceed this to add linear velocity based on angular velocity.")]
        public float ReleasedAngularThreshold = 1f;

        [Tooltip("Number of frames to average velocity for throwing.")]
        public int ThrowLookback = 5;

        [Tooltip("Number of frames to skip while averaging velocity.")]
        public int ThrowLookbackStart;


        [Tooltip("If true throwing takes only the top peak velocities for throwing.")]
        public bool TakePeakVelocities;

        [DrawIf("TakePeakVelocities", true)]
        public int CountPeakVelocities = 3;

        [Tooltip("Uses the center of mass that should match with current controller type you are using.")]
        public HVRThrowingCenterOfMass ThrowingCenterOfMass;

        [Tooltip("Invoked when the hand and object are too far apart")]
        public VRHandGrabberEvent BreakDistanceReached = new VRHandGrabberEvent();

        [Header("Debugging")]
        [Tooltip("If enabled displays vectors involved in throwing calculation.")]
        public bool DrawCenterOfMass;

        public bool GrabToggleActive;

        [SerializeField]
        private HVRGrabbable _triggerHoverTarget;

        public HVRSocket HoveredSocket;

        [SerializeField]
        private HVRGrabbable _hoverTarget;


        private bool _dynamicIndicatorEnabled;
        private bool _grabIndicatorEnabled;
        private HVRGrabbableHoverBase _grabIndicator;
        private HVRGrabbableHoverBase _triggerIndicator;

        public override bool IsHandGrabber => true;

        public bool IsLeftHand => HandSide == HVRHandSide.Left;
        public bool IsRightHand => HandSide == HVRHandSide.Right;
        
        public HVRHandStrengthHandler StrengthHandler { get; set; }

        public Transform HandModelParent { get; private set; }
        public Vector3 HandModelPosition { get; private set; }
        public Quaternion HandModelRotation { get; private set; }
        public Vector3 HandModelScale { get; private set; }

        public HVRRigidBodyOverrides RigidOverrides { get; private set; }

        public Dictionary<HVRGrabbable, Coroutine> OverlappingGrabbables = new Dictionary<HVRGrabbable, Coroutine>();

        public GameObject TempGrabPoint { get; internal set; }

        public HVRController Controller => HandSide == HVRHandSide.Left ? HVRInputManager.Instance.LeftController : HVRInputManager.Instance.RightController;

        public bool IsLineGrab { get; private set; }

        public bool IsInitialLineGrab => IsLineGrab && !_primaryGrabPointGrab && PosableGrabPoint.LineInitialCanReposition;


        public HVRGrabbable TriggerHoverTarget
        {
            get { return _triggerHoverTarget; }
            set
            {
                _triggerHoverTarget = value;
                IsTriggerHovering = value;
            }
        }

        public bool IsTriggerHovering { get; private set; }

        public HVRTrackedController HVRTrackedController { get; private set; }

        public override Transform GrabPoint
        {
            get => base.GrabPoint;
            set
            {
                if (!value)
                {
                    PosableGrabPoint = null;
                }
                else if (GrabPoint != value)
                {
                    PosableGrabPoint = value.GetComponent<HVRPosableGrabPoint>();
                }

                base.GrabPoint = value;
            }
        }


        public HVRPosableGrabPoint PosableGrabPoint { get; private set; }

        private Transform _triggerGrabPoint;

        public Transform TriggerGrabPoint
        {
            get => _triggerGrabPoint;
            set
            {
                if (!value)
                {
                    TriggerPosableGrabPoint = null;
                }
                else if (GrabPoint != value)
                {
                    TriggerPosableGrabPoint = value.GetComponent<HVRPosableGrabPoint>();
                }

                _triggerGrabPoint = value;
            }
        }


        public HVRPosableGrabPoint TriggerPosableGrabPoint { get; private set; }

        /// <summary>
        /// When a grab is initiated, this should be set to the hand models rotation relative to the grabbable object transform
        /// </summary>

        public Quaternion PoseLocalRotation { get; set; }

        /// <summary>
        /// World Pose Rotation of the currently active grab point 
        /// </summary>
        public Quaternion PoseWorldRotation
        {
            get { return GrabbedTarget.transform.rotation * PoseLocalRotation; }
        }

        public Vector3 PoseWorldPosition
        {
            get
            {
                if (PosableGrabPoint)
                {
                    return PosableGrabPoint.transform.TransformPoint(PosableGrabPoint.GetPosePositionOffset(HandSide));
                }

                if (IsPhysicsPose)
                {
                    return GrabbedTarget.transform.TransformPoint(PhysicsHandPosition);
                }

                return GrabPoint.position;
            }
        }


        internal Quaternion PhysicsHandRotation
        {
            get { return PoseLocalRotation; }
            set { PoseLocalRotation = value; }
        }

        internal Vector3 PhysicsHandPosition { get; set; }
        internal byte[] PhysicsPoseBytes { get; private set; }

        public override Quaternion ControllerRotation => TrackedController.rotation;

        public Transform Palm => PhysicsPoser.Palm;

        public bool IsClimbing { get; private set; }

        public bool IsPhysicsPose { get; set; }

        public Vector3 BaseLineGrabAnchor => GrabbedTarget.transform.InverseTransformPoint(PosableGrabPoint.WorldLineMiddle);

        public Vector3 LineGrabAnchor => BaseLineGrabAnchor + _lineOffset;

        public Vector3 GrabAnchorLocal { get; private set; }

        public Vector3 GrabAnchorWorld => GrabbedTarget.Rigidbody ? GrabbedTarget.Rigidbody.transform.TransformPoint(GrabAnchorLocal + _lineOffset) : GrabbedTarget.transform.TransformPoint(GrabAnchorLocal);

        public override Vector3 JointAnchorWorldPosition => JointAnchor.position;

        public Vector3 HandAnchorWorld => transform.TransformPoint(HandAnchorLocal);

        public Vector3 HandAnchorLocal { get; private set; }

        public bool IsHoveringSocket => HoveredSocket;

        public int PoserIndex => _posableHand ? _posableHand.PoserIndex : 0;

        public Vector3 CachedWorldPosition => transform.TransformPoint(HandModelPosition);

        public Vector3 HandWorldPosition => HandModel.position;

        public Quaternion CachedWorldRotation => transform.rotation * HandModelRotation;
        public Quaternion HandWorldRotation => HandModel.rotation;

        public readonly CircularBuffer<Vector3> RecentVelocities = new CircularBuffer<Vector3>(TrackedVelocityCount);
        public readonly CircularBuffer<Vector3> RecentAngularVelocities = new CircularBuffer<Vector3>(TrackedVelocityCount);

        public bool CanActivate { get; private set; }

        public bool CanRelease { get; set; } = true;

        /// <summary>
        /// ignores the next overlap check and enabling of collision with the released grabbable. Useful if grabbing something
        /// requires collision to remain disabled with the object that is next released.
        /// </summary>
        public bool IgnoreNextCollisionCheck { get; set; }

        protected Vector3 LineGrabHandVector => transform.rotation * HandModelRotation * _lineGrabHandRelativeDirection;

        protected Vector3 LineGrabVector => PosableGrabPoint.WorldLine.normalized * (_flippedLinePose ? -1f : 1f);

        #region Private

        private SphereCollider _overlapCollider;
        private readonly Collider[] _overlapColliders = new Collider[1000];
        private bool _hasPosed;
        private bool _handMoving;
        private Quaternion _previousRotation = Quaternion.identity;
        private float _pullingTimer;
        private Transform _collisionTransform;
        private HVRHandAnimator _collisionAnimator;
        public ConfigurableJoint Joint { get; protected set; }
        private Transform _fakeHand;
        private Transform _fakeHandAnchor;
        private bool _isForceAutoGrab;
        private Vector3 _lineOffset;
        private bool _tightlyHeld;
        private bool _flippedLinePose;
        private Quaternion _startRotation;
        private bool _primaryGrabPointGrab;
        private bool _socketGrab;
        private HVRPosableHand _posableHand;
        private HVRPosableHand _collisionHand;
        private HVRHandPoseData _physicsPose;
        private HVRHandPoseData _savedPose;
        private Vector3 _lineGrabHandRelativeDirection;
        private WaitForFixedUpdate _wffu;
        private bool _moveGrab;
        protected bool IsGripGrabActivated;
        protected bool IsTriggerGrabActivated;
        protected bool IsGripGrabActive;
        protected bool IsTriggerGrabActive;

        private bool _checkingSwap;
        private bool _checkingEnableCollision = true;
        private bool _forceFullyGrabbed;

        private Vector3 _pusherSize;
        private bool _pushing;
        private GameObject _anchor;
        private Rigidbody _forceRB;

        private bool _swappingGrabPoint;
        private bool _finalJointCreated;

        #endregion


        protected override void Awake()
        {
            base.Awake();

            if (TrackedController)
                HVRTrackedController = TrackedController.GetComponent<HVRTrackedController>();

            RigidOverrides = GetComponent<HVRRigidBodyOverrides>();

            _wffu = new WaitForFixedUpdate();
            BreakDistanceCooldown();
            SetupPusher();

            CheckPullAnchor();
        }


        protected override void Start()
        {
            base.Start();

            if (ApplyHandLayer)
            {
                transform.SetLayerRecursive(HVRLayers.Hand);
            }

            if (!CollisionHandler && transform.root)
            {
                CollisionHandler = transform.root.GetComponentInChildren<HVRTeleportCollisonHandler>();
            }

            //created in awake of the hand components if not exist, snapshot in start
            StrengthHandler = GetComponent<HVRHandStrengthHandler>();

            if (!Inputs && transform.root)
            {
                Inputs = transform.root.GetComponentInChildren<HVRPlayerInputs>();
            }

            if (!ForceGrabber)
            {
                ForceGrabber = GetComponentInChildren<HVRForceGrabber>();
            }

            if (!HandAnimator)
            {
                if (HandModel)
                {
                    HandAnimator = HandModel.GetComponentInChildren<HVRHandAnimator>();
                }
                else
                {
                    HandAnimator = GetComponentInChildren<HVRHandAnimator>();
                }
            }

            if (!PhysicsPoser)
            {
                if (HandModel)
                {
                    PhysicsPoser = HandModel.GetComponentInChildren<HVRPhysicsPoser>();
                }
                else
                {
                    PhysicsPoser = GetComponentInChildren<HVRPhysicsPoser>();
                }
            }

            if (!HandPhysics)
            {
                HandPhysics = GetComponentInChildren<HVRHandPhysics>();
            }

            _posableHand = PhysicsPoser.Hand;

            if (HandModel)
            {
                HandModelParent = HandModel.parent;
                HandModelPosition = HandModel.localPosition;
                HandModelRotation = HandModel.localRotation;
                HandModelScale = HandModel.localScale;

                if (InverseKinematics && CloneHandModel)
                {
                    CloneHandModel = false;
                }

                //no need to clone hand model if the hand doesn't have colliders on it
                if (CloneHandModel)
                {
                    if (HandModel.GetComponentsInChildren<Collider>().Length == 0)
                        CloneHandModel = false;
                }

                if (CloneHandModel)
                {
                    var handClone = Instantiate(HandModel.gameObject);

                    foreach (var t in handClone.GetComponentsInChildren<HVRCloneDelete>())
                    {
                        Destroy(t.gameObject);
                    }

                    ////cloning the hand model and leaving only hand posing components and colliders

                    foreach (var component in handClone.GetComponentsInChildren<Component>())
                    {
                        if (component is Collider || component is HVRPosableHand || component is HVRHandAnimator || component is Transform ||
                            component is HVRHandPoser) continue;
                        Destroy(component);
                    }

                    //removing colliders from the original hand model
                    foreach (var col in HandModel.GetComponentsInChildren<Collider>())
                    {
                        if (!col.isTrigger) Destroy(col);
                    }

                    _collisionTransform = handClone.transform;
                    ResetHandTransform(_collisionTransform);
                    _collisionAnimator = _collisionTransform.GetComponentInChildren<HVRHandAnimator>();
                    _collisionHand = _collisionTransform.GetComponent<HVRPosableHand>();
                }

                ResetRigidBodyProperties();

                var go = new GameObject("FakeHand");
                go.transform.parent = transform;
                go.transform.localPosition = HandModelPosition;
                go.transform.localRotation = HandModelRotation;
                _fakeHand = go.transform;

                go = new GameObject("FakeHandJointAnchor");
                go.transform.parent = _fakeHand;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                _fakeHandAnchor = go.transform;
            }

            this.ExecuteNextUpdate(() => HandPhysics.SetupColliders());

            if (OverlapSizer)
            {
                _overlapCollider = OverlapSizer.GetComponent<SphereCollider>();
            }

            if (!SocketBag)
                SocketBag = GetComponentInChildren<HVRSocketBag>();

            if (!ThrowingCenterOfMass)
                ThrowingCenterOfMass = GetComponentInChildren<HVRThrowingCenterOfMass>();

            ResetTrackedVelocities();

            if (!ControllerOffset)
            {
                if (TrackedController)
                {
                    ControllerOffset = TrackedController.GetComponentInChildren<HVRControllerOffset>();
                }
            }
        }

        protected override void Update()
        {
            //if (Controller.PrimaryButtonState.JustActivated)
            //{
            //    if(!Rigidbody.detectCollisions) StartPushing();
            //    Rigidbody.detectCollisions = !Rigidbody.detectCollisions;
            //}


            if (PerformUpdate)
            {
                CheckCanActivate();
                CheckActivateGrabbable();

                CheckBreakDistance();
                TrackVelocities();

                UpdateGrabInputs();
                CheckGrabControlSwap();
                CheckUntoggleGrab();
                IsHoldActive = UpdateHolding();
                CheckSocketUnhover();
                CheckSocketHover();
                CheckUnHover();
                CheckTriggerUnHover();
                CheckRelease();
                CheckHover();
                CheckTriggerHover();
                UpdateGrabIndicator();
                UpdateTriggerGrabIndicator();
                CheckGrab();
            }

            UpdatePose();
            CheckPoseHand();

            _previousRotation = transform.rotation;
            _hoverTarget = HoverTarget;
        }

        protected override void FixedUpdate()
        {
            if (PullingGrabbable) UpdatePullGrabbable();
            UpdateLineGrab();
            UpdatePostMoveGrab();
            UpdatePushing();
        }

        protected virtual void SetupPusher()
        {
            if (!Pusher)
            {
                var bounds = Rigidbody.GetColliderBounds();
                var go = new GameObject("Pusher");
                Pusher = go.AddComponent<BoxCollider>();
                Pusher.size = bounds.size;
                go.transform.parent = transform;
                go.transform.position = bounds.center;
            }

            _pusherSize = Pusher.size;
            Pusher.enabled = false;
        }


        /// <summary>
        /// Enables the push collider, disables existing hand colliders, and then grows the pusher collider to box size over 'PushoutTime',
        /// once complete it's disabled and the hand colliders are enabled again.
        /// </summary>
        public virtual void StartPushing()
        {
            _pushing = true;
            Pusher.size = Vector3.zero;
            HandPhysics.SetAllToTrigger();
            Pusher.enabled = true;
            Pusher.isTrigger = false;
        }

        protected virtual void UpdatePushing()
        {
            if (_pushing)
            {
                Pusher.size = Vector3.MoveTowards(Pusher.size, _pusherSize, 1f / PushoutTime * Time.deltaTime);

                if (Vector3.Distance(Pusher.size, _pusherSize) < .01f)
                {
                    Pusher.size = _pusherSize;
                    _pushing = false;
                    Pusher.enabled = false;
                    HandPhysics.ResetToNonTrigger();
                }
            }
        }

        private void UpdatePostMoveGrab()
        {
            if (_checkingEnableCollision)
            {
                if (Vector3.Distance(transform.position, TrackedController.transform.position) < .05f)
                {
                    Rigidbody.detectCollisions = true;
                    _checkingEnableCollision = false;
                    StartPushing();
                }
            }
        }

        private void CheckGrabControlSwap()
        {
            if (!_checkingSwap)
                return;

            if (_grabbableControl == _currentGrabControl)
            {
                _checkingSwap = false;
                return;
            }

            //checking for socket to grabbable grab button changes
            if (_grabbableControl == HVRGrabControls.GripOnly && _currentGrabControl == HVRGrabControls.TriggerOnly)
            {
                if (IsGripGrabActive && !IsTriggerGrabActive)
                {
                    _currentGrabControl = HVRGrabControls.GripOnly;
                    _checkingSwap = false;
                }
            }
            else if (_grabbableControl == HVRGrabControls.GripOnly && _currentGrabControl == HVRGrabControls.GripOrTrigger)
            {
                if (IsGripGrabActive && !IsTriggerGrabActive)
                {
                    _currentGrabControl = HVRGrabControls.GripOnly;
                    _checkingSwap = false;
                }
            }
            else if (_grabbableControl == HVRGrabControls.TriggerOnly && _currentGrabControl == HVRGrabControls.GripOnly)
            {
                if (IsTriggerGrabActive && !IsGripGrabActive)
                {
                    _currentGrabControl = HVRGrabControls.TriggerOnly;
                    _checkingSwap = false;
                }
            }
            else if (_grabbableControl == HVRGrabControls.TriggerOnly && _currentGrabControl == HVRGrabControls.GripOrTrigger)
            {
                if (IsTriggerGrabActive && !IsGripGrabActive)
                {
                    _currentGrabControl = HVRGrabControls.TriggerOnly;
                    _checkingSwap = false;
                }
            }
            else if (_grabbableControl == HVRGrabControls.GripOrTrigger && _currentGrabControl == HVRGrabControls.TriggerOnly)
            {
                if (IsGripGrabActive && !IsTriggerGrabActive || (GrabToggleActive && !IsTriggerGrabActive && !IsGripGrabActive))
                {
                    _currentGrabControl = HVRGrabControls.GripOrTrigger;
                    _checkingSwap = false;
                }
            }
            else if (_grabbableControl == HVRGrabControls.GripOrTrigger && _currentGrabControl == HVRGrabControls.GripOnly)
            {
                if (IsTriggerGrabActive && !IsGripGrabActive || (GrabToggleActive && !IsTriggerGrabActive && !IsGripGrabActive))
                {
                    _currentGrabControl = HVRGrabControls.GripOrTrigger;
                    _checkingSwap = false;
                }
            }
        }

        protected virtual void CheckActivateGrabbable()
        {
            if (IsGrabbing && CanActivate)
            {
                if (Controller.TriggerButtonState.JustActivated)
                {
                    GrabbedTarget.InternalOnActivate(this);
                }
                else if (Controller.TriggerButtonState.JustDeactivated)
                {
                    GrabbedTarget.InternalOnDeactivate(this);
                }
            }
        }

        private void UpdatePose()
        {
            if (!IsLineGrab && IsGrabbing && GrabbedTarget.Stationary && !GrabbedTarget.ParentHandModel && _hasPosed)
            {
                HandModel.rotation = PoseWorldRotation;
                HandModel.position = PoseWorldPosition;
            }
        }

        protected void ResetTrackedVelocities()
        {
            for (var i = 0; i < TrackedVelocityCount; i++)
            {
                RecentVelocities.Enqueue(Vector3.zero);
                RecentAngularVelocities.Enqueue(Vector3.zero);
            }
        }

        private void DetermineGrabPoint(HVRGrabbable grabbable)
        {
            if (IsGrabbing)
                return;

            GrabPoint = GetGrabPoint(grabbable);
        }

        internal Transform GetGrabPoint(HVRGrabbable grabbable, GrabpointFilter grabType = GrabpointFilter.Normal)
        {
            return grabbable.GetGrabPointTransform(this, grabType);
        }

        private void CheckCanActivate()
        {
            if (!CanActivate && !IsTriggerGrabActive)
            {
                CanActivate = true;
            }
        }

        protected override void CheckUnHover()
        {
            if (!HoverTarget)
                return;

            var closestValid = ClosestValidHover(false);

            if (!CanHover(HoverTarget) || closestValid != HoverTarget)
            {
                UnhoverGrabbable(this, HoverTarget);
            }
        }

        protected override bool CheckHover()
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

            var closestValid = ClosestValidHover(false);
            if (!closestValid)
                return false;

            HoverGrabbable(this, closestValid);
            return true;
        }

        protected virtual void CheckTriggerUnHover()
        {
            if (!TriggerHoverTarget)
                return;

            var closestValid = ClosestValidHover(true);

            if (!CanHover(TriggerHoverTarget) || closestValid != TriggerHoverTarget)
            {
                OnTriggerHoverExit(this, TriggerHoverTarget);
            }
        }


        protected virtual bool CheckTriggerHover()
        {
            if (IsTriggerHovering || !AllowHovering)
            {
                if (IsTriggerHovering && !TriggerHoverTarget)
                {
                    TriggerHoverTarget = null;
                }
                else
                {
                    return true;
                }
            }

            var closestValid = ClosestValidHover(true);
            if (!closestValid)
                return false;

            OnTriggerHoverEnter(this, closestValid);
            return true;
        }


        private void CheckUntoggleGrab()
        {
            if (GrabToggleActive && !_checkingSwap)
            {
                if (_currentGrabControl == HVRGrabControls.GripOrTrigger)
                {
                    if (!IsLineGrab && (IsGripGrabActivated || (IsTriggerGrabActivated && Inputs.CanTriggerGrab)))
                    {
                        GrabToggleActive = false;
                    }
                    else if (IsLineGrab && IsGripGrabActivated && !IsTriggerGrabActive)
                    {
                        //if line grab and trigger is pressed - don't allow untoggle
                        GrabToggleActive = false;
                    }
                }
                else if (_currentGrabControl == HVRGrabControls.TriggerOnly && IsTriggerGrabActivated)
                {
                    GrabToggleActive = false;
                }
                else if (_currentGrabControl == HVRGrabControls.GripOnly && IsGripGrabActivated)
                {
                    GrabToggleActive = false;
                }

                if (!GrabToggleActive)
                {
                    IsGripGrabActivated = false;
                    IsTriggerGrabActivated = false;
                }
            }
        }

        private HVRGrabControls _currentGrabControl;
        private HVRGrabControls _grabbableControl;

        protected virtual bool UpdateHolding()
        {
            if (!IsGrabbing)
                return false;

            if (!CanRelease)
                return true;

            var grabTrigger = GrabTrigger;

            if (GrabbedTarget.OverrideGrabTrigger)
            {
                grabTrigger = GrabbedTarget.GrabTrigger;
            }

            switch (grabTrigger)
            {
                case HVRGrabTrigger.Active:
                {
                    if (GrabToggleActive)
                    {
                        return true;
                    }

                    if (IsLineGrab)
                    {
                        return IsGripGrabActive || IsTriggerGrabActive;
                    }

                    var grabActive = false;
                    switch (_currentGrabControl)
                    {
                        case HVRGrabControls.GripOrTrigger:
                            grabActive = IsGripGrabActive || (IsTriggerGrabActive && Inputs.CanTriggerGrab);
                            break;
                        case HVRGrabControls.GripOnly:
                            grabActive = IsGripGrabActive;
                            break;
                        case HVRGrabControls.TriggerOnly:
                            grabActive = IsTriggerGrabActive;
                            break;
                    }

                    return grabActive;
                }
                case HVRGrabTrigger.Toggle:
                {
                    return GrabToggleActive;
                }
                case HVRGrabTrigger.ManualRelease:
                    return true;
            }

            return false;
        }

        protected override void CheckGrab()
        {
            if (!AllowGrabbing || IsGrabbing || GrabbedTarget)
            {
                return;
            }

            if (CheckSocketGrab())
            {
                return;
            }

            if (HoverTarget)
            {
                var grabControl = HoverTarget.GrabControl;
                if (HoverTarget.IsSocketed)
                    grabControl = HoverTarget.Socket.GrabControl;

                if (GrabActivated(grabControl) && TryGrab(HoverTarget))
                {
                    _currentGrabControl = grabControl;
                    return;
                }
            }

            if (TriggerHoverTarget)
            {
                var grabControl = TriggerHoverTarget.GrabControl;
                if (TriggerHoverTarget.IsSocketed)
                    grabControl = TriggerHoverTarget.Socket.GrabControl;
                if (GrabActivated(grabControl) && TryGrab(TriggerHoverTarget))
                {
                    _currentGrabControl = grabControl;
                    return;
                }
            }
        }

        protected virtual bool CheckSocketGrab()
        {
            if (HoveredSocket && CanGrabFromSocket(HoveredSocket) && GrabActivated(HoveredSocket.GrabControl))
            {
                _primaryGrabPointGrab = true;
                _socketGrab = true;
                GrabPoint = null;

                var gp = HoveredSocket.GrabbedTarget.GetGrabPointTransform(this, GrabpointFilter.Socket);
                if (!gp) //in case any socket grab point is invalid, deleted, inactive
                    gp = HoveredSocket.GrabbedTarget.GetGrabPointTransform(this, GrabpointFilter.Normal);

                GrabPoint = gp;

                if (HoveredSocket.InstantHandPose && PosableGrabPoint)
                {
                    OrientGrabbable(HoveredSocket.GrabbedTarget, PosableGrabPoint);
                    HoveredSocket.GrabbedTarget.Rigidbody.rotation = HoveredSocket.GrabbedTarget.transform.rotation;
                    HoveredSocket.GrabbedTarget.Rigidbody.position = HoveredSocket.GrabbedTarget.transform.position;
                }

                if (TryGrab(HoveredSocket.GrabbedTarget, true))
                {
                    _currentGrabControl = HoveredSocket.GrabControl;

                    HoveredSocket.OnHandGrabberExited();

                    if (HoveredSocket.InstantHandPose && PosableGrabPoint)
                    {
                        //OrientGrabbable(GrabbedTarget, PosableGrabPoint);
                        //GrabbedTarget.Rigidbody.rotation = GrabbedTarget.transform.rotation;
                        //GrabbedTarget.Rigidbody.position = GrabbedTarget.transform.position;

                        var grabbable = GrabbedTarget;

                        if (CollisionHandler)
                        {
                            this.ExecuteAfterFixedUpdate(() =>
                            {
                                if (grabbable == GrabbedTarget) CollisionHandler.Sweep(this);
                            });
                        }
                    }

                    HoveredSocket = null;
                    //Debug.Log($"grabbed from socket directly");

                    return true;
                }
            }

            return false;
        }


        private void UpdateGrabInputs()
        {
            IsTriggerGrabActivated = Inputs.GetTriggerGrabState(HandSide).JustActivated;
            IsGripGrabActivated = Inputs.GetGrabActivated(HandSide);

            IsTriggerGrabActive = Inputs.GetTriggerGrabState(HandSide).Active;
            IsGripGrabActive = Inputs.GetGripHoldActive(HandSide);
        }

        private bool GrabActivated(HVRGrabControls grabControl)
        {
            switch (grabControl)
            {
                case HVRGrabControls.GripOrTrigger:
                    return IsGripGrabActivated || (IsTriggerGrabActivated && Inputs.CanTriggerGrab);
                case HVRGrabControls.GripOnly:
                    return IsGripGrabActivated;
                case HVRGrabControls.TriggerOnly:
                    return IsTriggerGrabActivated;
            }

            return false;
        }


        protected virtual void UpdateGrabIndicator()
        {
            if (!IsHovering || !_grabIndicator || !HoverTarget.ShowGrabIndicator)
                return;

            if (_grabIndicator.LookAtCamera && HVRManager.Instance.Camera)
            {
                _grabIndicator.transform.LookAt(HVRManager.Instance.Camera);
            }

            if (_grabIndicator.HoverPosition == HVRHoverPosition.Self)
                return;

            if (_grabIndicator.HoverPosition == HVRHoverPosition.GrabPoint)
            {
                DetermineGrabPoint(HoverTarget);

                if (PosableGrabPoint)
                {
                    EnableGrabIndicator();
                    DisableDynamicIndicator();
                    _grabIndicator.transform.position = GetGrabIndicatorPosition(HoverTarget, PosableGrabPoint);
                    return;
                }

                var isDynamic = HoverTarget.PoseType == PoseType.PhysicPoser || HoverTarget.PhysicsPoserFallback;

                if (isDynamic)
                {
                    if (DynamicPoseIndicatorMode == DynamicPoseGrabIndicator.None)
                    {
                        DisableGrabIndicator();
                        DisableDynamicIndicator();
                        return;
                    }

                    var indicator = _grabIndicator;

                    if (DynamicPoseIndicator)
                    {
                        if (!_dynamicIndicatorEnabled)
                        {
                            EnableDynamicIndicator();
                            DisableGrabIndicator();
                        }

                        indicator = DynamicPoseIndicator;

                        if (DynamicPoseIndicator.LookAtCamera && HVRManager.Instance.Camera)
                        {
                            DynamicPoseIndicator.transform.LookAt(HVRManager.Instance.Camera);
                        }
                    }

                    if (DynamicPoseIndicatorMode == DynamicPoseGrabIndicator.Palm)
                    {
                        indicator.transform.position = FindClosestPoint(HoverTarget, out var inside);
                        return;
                    }

                    if (DynamicPoseIndicatorMode == DynamicPoseGrabIndicator.Transform)
                    {
                        indicator.transform.position = HoverTarget.transform.position;
                    }

                    return;
                }
            }

            DisableDynamicIndicator();
            EnableGrabIndicator();
            _grabIndicator.transform.position = HoverTarget.transform.position;
        }

        protected virtual void UpdateTriggerGrabIndicator()
        {
            if (!IsTriggerHovering || !_triggerIndicator || IsGrabbing || TriggerHoverTarget == HoverTarget || !TriggerHoverTarget.ShowTriggerGrabIndicator)
                return;

            if (_triggerIndicator.LookAtCamera && HVRManager.Instance.Camera)
            {
                _triggerIndicator.transform.LookAt(HVRManager.Instance.Camera);
            }

            if (_triggerIndicator.HoverPosition == HVRHoverPosition.Self)
                return;

            if (_triggerIndicator.HoverPosition == HVRHoverPosition.GrabPoint)
                TriggerGrabPoint = GetGrabPoint(TriggerHoverTarget, GrabpointFilter.Normal);

            if (TriggerPosableGrabPoint && _triggerIndicator.HoverPosition == HVRHoverPosition.GrabPoint)
            {
                _triggerIndicator.transform.position = GetGrabIndicatorPosition(TriggerHoverTarget, TriggerPosableGrabPoint);
            }
            else
            {
                _triggerIndicator.transform.position = TriggerHoverTarget.transform.position;
            }
        }

        internal Vector3 GetGrabIndicatorPosition(HVRGrabbable grabbable, Transform grabPoint, bool useGrabPoint = false)
        {
            var posableGrabPoint = grabPoint.GetComponent<HVRPosableGrabPoint>();
            if (posableGrabPoint)
            {
                return GetGrabIndicatorPosition(grabbable, posableGrabPoint, useGrabPoint);
            }

            return grabPoint.position;
        }

        internal Vector3 GetGrabIndicatorPosition(HVRGrabbable grabbable, HVRPosableGrabPoint grabPoint, bool useGrabPoint = false)
        {
            if (grabPoint.IsLineGrab && !useGrabPoint && grabPoint.LineInitialCanReposition)
            {
                return grabbable.transform.TransformPoint(GetLocalLineGrabPoint(grabbable, transform.TransformPoint(GetLineGrabHandAnchor(grabPoint)), grabPoint));
            }

            if (grabPoint.GrabIndicatorPosition)
                return grabPoint.GrabIndicatorPosition.position;

            return grabPoint.transform.position;
        }

        protected override void OnHoverEnter(HVRGrabbable grabbable)
        {
            base.OnHoverEnter(grabbable);

            GrabPoint = GetGrabPoint(grabbable, GrabpointFilter.Normal);

            OnHoverHaptics();

            if (grabbable.ShowGrabIndicator)
            {
                if (grabbable.GrabIndicator)
                {
                    _grabIndicator = grabbable.GrabIndicator;
                }
                else
                {
                    _grabIndicator = GrabIndicator;
                }

                EnableGrabIndicator();
            }

            if (HoverPoser)
            {
                SetAnimatorPose(HoverPoser);
            }
        }

        protected virtual void OnHoverHaptics()
        {
            if (IsMine && HVRInputManager.Instance.GrabHaptics)
            {
                Controller.Vibrate(HVRInputManager.Instance.GrabHaptics.HandHover);
            }
        }


        protected override void OnHoverExit(HVRGrabbable grabbable)
        {
            base.OnHoverExit(grabbable);

            DisableGrabIndicator();
            DisableDynamicIndicator();

            if (!IsGrabbing)
            {
                ResetAnimator();
            }
        }

        private void EnableGrabIndicator()
        {
            if (_grabIndicatorEnabled) return;
            if (_grabIndicator)
            {
                _grabIndicatorEnabled = true;
                _grabIndicator.Enable();
                _grabIndicator.Hover();
            }
        }

        private void DisableGrabIndicator()
        {
            if (!_grabIndicatorEnabled) return;
            if (_grabIndicator)
            {
                _grabIndicatorEnabled = false;
                _grabIndicator.Unhover();
                _grabIndicator.Disable();
            }
        }

        private void EnableDynamicIndicator()
        {
            if (_dynamicIndicatorEnabled) return;
            if (DynamicPoseIndicator)
            {
                _dynamicIndicatorEnabled = true;
                DynamicPoseIndicator.Enable();
                DynamicPoseIndicator.Hover();
            }
        }

        private void DisableDynamicIndicator()
        {
            if (!_dynamicIndicatorEnabled) return;
            if (DynamicPoseIndicator)
            {
                _dynamicIndicatorEnabled = false;
                DynamicPoseIndicator.Unhover();
                DynamicPoseIndicator.Disable();
            }
        }


        protected virtual void OnTriggerHoverEnter(HVRHandGrabber grabber, HVRGrabbable grabbable)
        {
            TriggerHoverTarget = grabbable;
            TriggerGrabPoint = GetGrabPoint(grabbable, GrabpointFilter.Normal);
            if (grabbable.ShowTriggerGrabIndicator)
            {
                if (grabbable.GrabIndicator)
                {
                    _triggerIndicator = grabbable.GrabIndicator;
                }
                else
                {
                    _triggerIndicator = TriggerGrabIndicator;
                }

                if (_triggerIndicator)
                {
                    _triggerIndicator.Enable();
                    _triggerIndicator.Hover();
                }
            }
        }

        protected virtual void OnTriggerHoverExit(HVRHandGrabber grabber, HVRGrabbable grabbable)
        {
            TriggerHoverTarget = null;

            if (_triggerIndicator)
            {
                _triggerIndicator.Unhover();
                _triggerIndicator.Disable();
            }
        }

        private void TrackVelocities()
        {
            var deltaRotation = transform.rotation * Quaternion.Inverse(_previousRotation);
            deltaRotation.ToAngleAxis(out var angle, out var axis);
            angle *= Mathf.Deg2Rad;
            var angularVelocity = axis * (angle * (1.0f / Time.fixedDeltaTime));

            RecentVelocities.Enqueue(Rigidbody.velocity);
            RecentAngularVelocities.Enqueue(angularVelocity);
        }

        protected virtual void CheckSocketUnhover()
        {
            if (!HoveredSocket)
                return;

            var closest = ClosestValidSocket();

            if (IsGrabbing || IsForceGrabbing || !CanGrabFromSocket(HoveredSocket) || closest != HoveredSocket)
            {
                HoveredSocket.OnHandGrabberExited();
                HoveredSocket = null;

                if (HVRSettings.Instance.VerboseHandGrabberEvents)
                    Debug.Log($"socket exited");
            }
        }

        protected virtual bool CanGrabFromSocket(HVRSocket socket)
        {
            if (!socket || !socket.GrabbedTarget)
            {
                return false;
            }

            if (!socket.GrabbedTarget.CanHandGrab(this))
            {
                return false;
            }

            if (!socket.CanGrabbableBeRemoved(this))
            {
                return false;
            }

            return socket.GrabDetectionType == HVRGrabDetection.Socket;
        }

        protected virtual void CheckSocketHover()
        {
            if (IsGrabbing || IsHoveringSocket || !SocketBag || IsForceGrabbing)
                return;

            var closest = ClosestValidSocket();
            if (closest)
            {
                HoveredSocket = closest;
                HoveredSocket.OnHandGrabberEntered();
            }
        }

        protected virtual HVRSocket ClosestValidSocket()
        {
            for (var i = 0; i < SocketBag.ValidSockets.Count; i++)
            {
                var socket = SocketBag.ValidSockets[i];

                if (!CanGrabFromSocket(socket))
                    continue;

                return socket;
            }

            return null;
        }


        private void CheckPullAnchor()
        {
            if (!_anchor)
            {
                _anchor = new GameObject($"{HandSide} PullAnchor");
                _forceRB = _anchor.AddComponent<Rigidbody>();
                _forceRB.isKinematic = true;
            }
        }

        protected virtual void StartPull()
        {
            var grabbable = GrabbedTarget;
            _startPos = GrabbedTarget.transform.position;
            _startRot = GrabbedTarget.transform.rotation;
            CheckPullAnchor();
            _anchor.transform.position = _startPos;
            _anchor.transform.rotation = _startRot;
            PullingGrabbable = true;
            _pullingTimer = 0f;

            PullJoint = _anchor.AddComponent<ConfigurableJoint>();


            HVRJointSettings pullSettings = null;

            if (grabbable.PullingSettingsOverride)
            {
                pullSettings = grabbable.PullingSettingsOverride;
            }
            else if (PullingSettings)
            {
                pullSettings = PullingSettings;
            }

            PullJoint.autoConfigureConnectedAnchor = false;
            PullJoint.rotationDriveMode = RotationDriveMode.Slerp;
            PullJoint.connectedBody = grabbable.Rigidbody;
            PullJoint.connectedAnchor = Vector3.zero;
            PullJoint.anchor = Vector3.zero;

            pullSettings.ApplySettings(PullJoint);

            _distanceComplete = false;
            if (_isForceAutoGrab) _distanceComplete = true;
        }

        protected virtual void CleanupPull()
        {
            if (_posJoint) Destroy(_posJoint);
            if (_rotJoint) Destroy(_rotJoint);
            if (PullJoint) Destroy(PullJoint);
            PullingGrabbable = false;
        }

        private bool _distanceComplete;
        private ConfigurableJoint _posJoint;
        private ConfigurableJoint _rotJoint;

        protected virtual void UpdatePullGrabbable()
        {
            var grabbable = GrabbedTarget;

            _pullingTimer += Time.fixedDeltaTime;


            var deltaRot = CachedWorldRotation * Quaternion.Inverse(PoseWorldRotation);
            var offset = deltaRot * (grabbable.transform.position - PoseWorldPosition);

            if (IsInitialLineGrab)
            {
                offset = deltaRot * (grabbable.transform.position - (PoseWorldPosition + (grabbable.transform.TransformPoint(BaseLineGrabAnchor + _lineOffset) - GrabPoint.position)));
            }

            var targetPos = offset + CachedWorldPosition;

            var angleDelta = Quaternion.Angle(PoseWorldRotation, CachedWorldRotation);

            bool angleComplete;
            if (_pullingTimer <= PullLerpTime)
            {
                _anchor.transform.SetPositionAndRotation(
                    Vector3.Lerp(_startPos, targetPos, _pullingTimer / PullLerpTime),
                    Quaternion.Lerp(_startRot, deltaRot * grabbable.transform.rotation, _pullingTimer / PullLerpTime));
                angleComplete = angleDelta < 15f;
            }
            else
            {
                _anchor.transform.SetPositionAndRotation(offset + CachedWorldPosition, deltaRot * grabbable.transform.rotation);
                angleComplete = angleDelta < grabbable.FinalJointMaxAngle;
            }


            if (!_distanceComplete)
            {
                _distanceComplete = Vector3.Distance(HandAnchorWorld, GrabAnchorWorld) < .07f;
            }

            var done = false;


            try
            {
                var timesUp = _pullingTimer > PullLerpTime && _pullingTimer > grabbable.FinalJointTimeout;

                //another hand grabbed while pulling, finish off with a move grab
                if (grabbable.GrabberCount > 1 || timesUp && angleDelta > MoveThreshold)
                {
                    done = true;
                    StartCoroutine(MoveGrab());
                    return;
                }

                if (angleComplete && _distanceComplete || timesUp)
                {
                    grabbable.transform.rotation = deltaRot * grabbable.transform.rotation;
                    angleDelta = Quaternion.Angle(PoseWorldRotation, CachedWorldRotation);

                    if (HVRSettings.Instance.VerboseHandGrabberEvents)
                        Debug.Log($"{HandSide} joint created, elapsed {_pullingTimer:f3}, angleDelta: {angleDelta}, pos delta {Vector3.Distance(CachedWorldPosition, PoseWorldPosition)}");

                    SetupConfigurableJoint(grabbable);
                    done = true;
                }
                else if (_distanceComplete && !_posJoint)
                {
                    _posJoint = grabbable.Rigidbody.gameObject.AddComponent<ConfigurableJoint>();
                    _posJoint.LockLinearMotion();
                    _posJoint.connectedBody = Rigidbody;
                    _posJoint.autoConfigureConnectedAnchor = false;
                    _posJoint.anchor = GrabAnchorLocal;
                    if (IsLineGrab)
                    {
                        _posJoint.anchor = BaseLineGrabAnchor + _lineOffset;
                    }

                    _posJoint.connectedAnchor = HandAnchorLocal;
                    PullJoint.SetLinearDrive(0f, 0f, 0f);
                }
                else if (angleComplete && !_rotJoint)
                {
                    grabbable.transform.rotation = deltaRot * grabbable.transform.rotation;
                    _rotJoint = grabbable.Rigidbody.gameObject.AddComponent<ConfigurableJoint>();
                    _rotJoint.LockAllAngularMotion();
                    _rotJoint.connectedBody = Rigidbody;
                    _rotJoint.autoConfigureConnectedAnchor = false;
                    _rotJoint.anchor = GrabAnchorLocal;
                    if (IsLineGrab)
                    {
                        _posJoint.anchor = BaseLineGrabAnchor + _lineOffset;
                    }

                    _rotJoint.connectedAnchor = HandAnchorLocal;
                    PullJoint.SetSlerpDrive(0f, 0f, 0f);
                }
            }
            finally
            {
                if (done)
                {
                    CleanupPull();
                }
            }
        }


        private float _breakDistanceNext;

        /// <summary>
        /// Breakdistance check ignored for the next 'timeout' amount of time. Useful if you're teleporting the player around and need to
        /// ignore break check temporarily while the move resolves.
        /// </summary>
        public void BreakDistanceCooldown(float timeout = .25f)
        {
            _breakDistanceNext = Time.time + timeout;
        }

        protected virtual void CheckBreakDistance()
        {
            if (_handMoving || PullingGrabbable || !GrabbedTarget || Time.time < _breakDistanceNext || (GrabbedTarget.IsJointGrab && !_finalJointCreated) || !CheckBreakDistanceReached(GrabbedTarget))
                return;

            //Debug.Break();
            if (HVRSettings.Instance.VerboseHandGrabberEvents)
            {
                Debug.Log($"{name} break distance reached on {GrabbedTarget.name}.");
            }

            BreakDistanceReached.Invoke(this, GrabbedTarget);
            ForceRelease();
        }

        protected virtual bool CheckBreakDistanceReached(HVRGrabbable grabbable)
        {
            if (grabbable.BreakDistanceSource == BreakDistanceSource.Hand)
                return Vector3.Distance(GrabAnchorWorld, JointAnchorWorldPosition) > grabbable.BreakDistance;
            if (grabbable.BreakDistanceSource == BreakDistanceSource.Controller)
                return Vector3.Distance(GrabAnchorWorld, TrackedController.position) > grabbable.BreakDistance;
            return false;
        }

        private void CheckPoseHand()
        {
            if (!IsGrabbing || _hasPosed || !GrabbedTarget || _handMoving)
                return;

            var angleDelta = 0f;
            if (!IgnoreParentingAngle)
            {
                angleDelta = Quaternion.Angle(PoseWorldRotation, CachedWorldRotation);
            }

            var distance = 0f;
            if (!IgnoreParentingDistance && Joint)
            {
                distance = Vector3.Distance(HandAnchorWorld, GrabAnchorWorld);
            }

            if ((IgnoreParentingAngle || angleDelta <= ParentingMaxAngleDelta) &&
                (IgnoreParentingDistance || distance <= ParentingMaxDistance) ||
                GrabbedTarget.PoseImmediately ||
                GrabbedTarget.GrabberCount > 1)
            {
                PoseHand(GrabbedTarget.ParentHandModel);
            }
        }

        private void PoseHand(bool parent)
        {
            _hasPosed = true;

            if (IsPhysicsPose)
            {
                SetAnimatorPose(null);
                HandAnimator.StartDynamicPose(_physicsPose);
                if (CloneHandModel && _collisionAnimator) _collisionAnimator.StartDynamicPose(_physicsPose);

                if (parent)
                {
                    ParentHandModel(GrabPoint.transform);

                    //todo lerp so there isn't a snap
                    HandModel.transform.localPosition = PhysicsHandPosition;
                    HandModel.transform.localRotation = PhysicsHandRotation;
                }
                else
                {
                    ResetHandTransform(HandModel);
                }

                return;
            }

            var poser = PosableGrabPoint ? PosableGrabPoint.HandPoser : FallbackPoser;

            if (parent)
            {
                ParentHandModel(GrabPoint);
            }

            if (CloneHandModel && _collisionAnimator) _collisionAnimator.SetHeldPoser(poser);
            if (HandAnimator) HandAnimator.SetHeldPoser(poser);
        }

        private void ParentHandModel(Transform parent)
        {
            if (!parent)
                return;

            var worldRotation = parent.rotation;
            var worldPosition = parent.position;

            var posableGrabPoint = parent.GetComponent<HVRPosableGrabPoint>();
            if (posableGrabPoint && posableGrabPoint.VisualGrabPoint)
            {
                parent = posableGrabPoint.VisualGrabPoint;
                parent.rotation = worldRotation;
                parent.position = worldPosition;
            }

            HandModel.parent = parent;

            if (PosableGrabPoint)
            {
                var pose = PosableGrabPoint.HandPoser.PrimaryPose.Pose.GetPose(HandSide);
                HandModel.localRotation = pose.Rotation;
                HandModel.localPosition = pose.Position;
            }

            _hasPosed = true;

            var listener = parent.gameObject.EnsureComponent<HVRDestroyListener>();
            listener.Destroyed.AddListener(OnGrabPointDestroyed);
        }

        public void SetAnimatorPose(HVRHandPoser poser)
        {
            if (CloneHandModel && _collisionAnimator) _collisionAnimator.SetCurrentPoser(poser);
            if (HandAnimator) HandAnimator.SetCurrentPoser(poser);
        }

        public void SetAnimatorOverridePose(HVRHandPoser poser)
        {
            if (CloneHandModel && _collisionAnimator) _collisionAnimator.SetOverridePoser(poser);
            if (HandAnimator) HandAnimator.SetOverridePoser(poser);
        }

        public void ResetAnimator()
        {
            if (HandAnimator)
            {
                if (GrabPoser && HandAnimator.CurrentPoser == GrabPoser || HoverPoser && HandAnimator.CurrentPoser == HoverPoser)
                {
                    HandAnimator.ResetToDefault();
                }
            }

            ResetCloneAnimator();
        }

        public void ResetCloneAnimator()
        {
            if (CloneHandModel && _collisionAnimator) _collisionAnimator.ResetToDefault();
        }

        private void OnGrabPointDestroyed(HVRDestroyListener listener)
        {
            if (HandModel && HandModel.parent == listener.transform)
            {
                ResetHandModel();
            }
        }

        public override bool CanHover(HVRGrabbable grabbable)
        {
            if (IsForceGrabbing || (IsGripGrabActive && (!HoverTarget || HoverTarget != grabbable)))
                return false;

            return CanGrab(grabbable);
        }

        private bool IsForceGrabbing => ForceGrabber && (ForceGrabber.IsForceGrabbing || ForceGrabber.IsAiming);

        public override bool CanGrab(HVRGrabbable grabbable)
        {
            if (!base.CanGrab(grabbable))
                return false;

            if (!grabbable.CanHandGrab(this))
                return false;
            
            if ((!AllowMultiplayerSwap && !grabbable.AllowMultiplayerSwap) && grabbable.HoldType != HVRHoldType.ManyHands && grabbable.AnyGrabberNotMine())
            {
                return false;
            }

            if (grabbable.PrimaryGrabber && !grabbable.PrimaryGrabber.AllowSwap)
            {
                if (grabbable.HoldType == HVRHoldType.TwoHanded && grabbable.GrabberCount > 1)
                    return false;

                if (grabbable.HoldType == HVRHoldType.OneHand && !_isForceAutoGrab && grabbable.GrabberCount > 0)
                    return false;
            }

            if (GrabbedTarget && GrabbedTarget != grabbable)
                return false;

            if (grabbable.IsSocketed && grabbable.Socket.GrabDetectionType == HVRGrabDetection.Socket)
                return false;

            if (grabbable.RequireLineOfSight && !grabbable.IsSocketed && !grabbable.IsBeingForcedGrabbed &&
                !grabbable.IsStabbed && !grabbable.IsStabbing && !CheckLineOfSight(grabbable))
                return false;

            if (grabbable.RequiresGrabbable)
            {
                if (!grabbable.RequiredGrabbable.PrimaryGrabber || !grabbable.RequiredGrabbable.PrimaryGrabber.IsHandGrabber)
                    return false;
            }

            return true;
        }

        protected override void CheckSwapRelease(HVRGrabbable grabbable)
        {
            if (grabbable.HoldType == HVRHoldType.Swap && grabbable.PrimaryGrabber && grabbable.PrimaryGrabber.IsHandGrabber)
            {
                ReleaseGrabbable(grabbable.PrimaryGrabber, grabbable, true, true);
                return;
            }

            base.CheckSwapRelease(grabbable);
        }

        protected virtual bool CheckLineOfSight(HVRGrabbable grabbable)
        {
            if (grabbable.HasConcaveColliders)
                return true;
            return CheckForLineOfSight(RaycastOrigin.position, grabbable, RaycastLayermask);
        }

        protected override void OnBeforeGrabbed(HVRGrabArgs args)
        {
            if (HVRSettings.Instance.VerboseHandGrabberEvents)
            {
                Debug.Log($"{name}:OnBeforeGrabbed");
            }

            if (args.Grabbable.PoseType == PoseType.HandPoser)
            {
                if (args.Grabbable == TriggerHoverTarget)
                    GrabPoint = TriggerGrabPoint;

                if (PosableGrabPoint && PosableGrabPoint.Grabbable && args.Grabbable != PosableGrabPoint.Grabbable)
                    GrabPoint = null;

                if (!GrabPoint)
                {
                    if (_socketGrab)
                    {
                    }
                    else
                    {
                        GrabPoint = args.Grabbable.GetGrabPointTransform(this, GrabpointFilter.Normal);
                    }
                }
            }

            base.OnBeforeGrabbed(args);
        }

        protected override void OnGrabbed(HVRGrabArgs args)
        {
            base.OnGrabbed(args);

            if (HVRSettings.Instance.VerboseHandGrabberEvents)
            {
                Debug.Log($"{name}:OnGrabbed");
            }

            if (HandAnimator)
            {
                if (GrabPoser)
                {
                    SetAnimatorPose(GrabPoser);
                }
                else
                {
                    ResetAnimator();
                }
            }

            var grabbable = args.Grabbable;
            _grabbableControl = grabbable.GrabControl;
            _checkingSwap = true;
            _moveGrab = false;
            _finalJointCreated = false;

            GrabToggleActive = GrabTrigger == HVRGrabTrigger.Toggle || grabbable.OverrideGrabTrigger && grabbable.GrabTrigger == HVRGrabTrigger.Toggle;

            CanActivate = false;

            var grabTransform = grabbable.transform;

            if (grabbable.Rigidbody) grabTransform = grabbable.Rigidbody.transform;

            if (OverlappingGrabbables.TryGetValue(grabbable, out var routine))
            {
                if (routine != null) StopCoroutine(routine);
                OverlappingGrabbables.Remove(grabbable);
            }

            if (grabbable.DisableHandCollision)
            {
                Rigidbody.detectCollisions = false;
            }

            DisableHandCollision(grabbable);

            if (UseDynamicGrab())
            {
                DynamicGrab();
            }

            if (!GrabPoint || args.Grabbable.PoseType == PoseType.Offset)
            {
                PoseLocalRotation = Quaternion.Inverse(grabTransform.rotation) * CachedWorldRotation;
                OffsetGrab(grabbable);
                return;
            }

            if (!IsPhysicsPose)
            {
                IsLineGrab = PosableGrabPoint && PosableGrabPoint.IsLineGrab;

                if (IsLineGrab)
                {
                    SetupLineGrab(grabbable);
                }

                if (IsLineGrab && !_primaryGrabPointGrab)
                {
                    Quaternion handRotation;

                    if (PosableGrabPoint.LineInitialCanRotate)
                    {
                        handRotation = Quaternion.FromToRotation(LineGrabHandVector, LineGrabVector) * transform.rotation * HandModel.localRotation;
                    }
                    else if (_flippedLinePose)
                    {
                        var poseRot = PosableGrabPoint.GetPoseWorldRotation(HandSide);
                        var delta = poseRot * Quaternion.Inverse(CachedWorldRotation);

                        Quaternion rotation;

                        if (IsV1Closest(LineGrabHandVector, transform.forward, transform.up))
                        {
                            var up = delta * transform.up;
                            rotation = Quaternion.LookRotation(LineGrabVector, up);
                        }
                        else
                        {
                            var forward = delta * transform.forward;
                            rotation = Quaternion.LookRotation(forward, LineGrabVector);
                        }

                        handRotation = rotation * HandModel.localRotation;
                    }
                    else
                    {
                        //just use the base pose rotation if rotation and flipping isn't allowed or no need to flip due to the hand's relative orientation
                        handRotation = PosableGrabPoint.GetPoseWorldRotation(HandSide);
                    }

                    PoseLocalRotation = Quaternion.Inverse(grabTransform.rotation) * handRotation;
                }
                else if (PosableGrabPoint)
                {
                    PoseLocalRotation = PosableGrabPoint.GetGrabbableRelativeRotation(HandSide);
                }
            }

            var isStatic = grabbable.Stationary || (grabbable.IsJointGrab && (!grabbable.Rigidbody || grabbable.Rigidbody.isKinematic));

            var linkedHeld = grabbable.MasterGrabbable && grabbable.MasterGrabbable.IsHandGrabbed || grabbable.AnyLinkedHandHeld();

            var preventsMoveGrab = _isForceAutoGrab || grabbable.GrabBehaviour == GrabBehaviour.PullToHand;
            if (!preventsMoveGrab && (grabbable.GrabBehaviour == GrabBehaviour.HandRetrieves || HandGrabs || grabbable.GrabberCount > 1 || grabbable.IsStabbing || isStatic || linkedHeld))
            {
                StartCoroutine(MoveGrab());
            }
            else
            {
                GrabPointGrab(grabbable);
            }

            if (PosableGrabPoint && ControllerOffset)
            {
                ControllerOffset.SetGrabPointOffsets(PosableGrabPoint.HandPositionOffset, PosableGrabPoint.HandRotationOffset);
            }
        }


        public static bool IsV1Closest(Vector3 v, Vector3 v1, Vector3 v2)
        {
            var vNorm = v.normalized;

            var v1Dot = Vector3.Dot(vNorm, v1.normalized);
            var v2Dot = Vector3.Dot(vNorm, v2.normalized);

            return Mathf.Abs(v1Dot) > Mathf.Abs(v2Dot);
        }

        private void OffsetGrab(HVRGrabbable grabbable)
        {
            TempGrabPoint = new GameObject(name + " OffsetGrabPoint");
            TempGrabPoint.transform.parent = GrabbedTarget.transform;
            TempGrabPoint.transform.position = Vector3.zero;
            TempGrabPoint.transform.localRotation = Quaternion.identity;
            GrabPoint = TempGrabPoint.transform;

            if (grabbable.ParentHandModel)
            {
                ParentHandModel(GrabPoint);
            }

            SetAnimatorPose(FallbackPoser);

            Grab(grabbable);
        }

        private void SetupLineGrab(HVRGrabbable grabbable)
        {
            _lineGrabHandRelativeDirection = GetLineGrabRelativeDirection();
            _flippedLinePose = false;
            _lineOffset = Vector3.zero;

            var mid = grabbable.transform.InverseTransformPoint(PosableGrabPoint.WorldLineMiddle);
            var point = IsInitialLineGrab ? transform.TransformPoint(GetLineGrabHandAnchor(PosableGrabPoint)) : GrabPoint.position;
            _lineOffset = GetLocalLineGrabPoint(grabbable, point, PosableGrabPoint) - mid;

            if (PosableGrabPoint.CanLineFlip)
            {
                _flippedLinePose = Vector3.Dot(PosableGrabPoint.WorldLine, LineGrabHandVector) < 0;
            }
        }

        private Vector3 GetLineGrabRelativeDirection()
        {
            //calculate the relative vector of the line grab line to the stored pose information
            _fakeHand.parent = GrabPoint;
            _fakeHand.localPosition = PosableGrabPoint.GetPosePositionOffset(HandSide);
            _fakeHand.localRotation = PosableGrabPoint.GetPoseRotationOffset(HandSide);

            var relativeVector = _fakeHand.InverseTransformDirection(PosableGrabPoint.WorldLine);

            _fakeHand.parent = transform;

            return relativeVector;
        }


        private Vector3 GetLocalLineGrabPoint(HVRGrabbable grabbable, Vector3 point, HVRPosableGrabPoint grabPoint)
        {
            var start = grabbable.transform.InverseTransformPoint(grabPoint.LineStart.position);
            var end = grabbable.transform.InverseTransformPoint(grabPoint.LineEnd.position);
            var testPoint = grabbable.transform.InverseTransformPoint(point);
            return HVRUtilities.FindNearestPointOnLine(start, end, testPoint);
        }

        protected virtual Vector3 FindClosestPoint(HVRGrabbable grabbable, out bool inside)
        {
            var closest = Palm.transform.position;
            var distance = float.PositiveInfinity;
            inside = false;

            if (grabbable.Colliders == null || grabbable.Colliders.Count == 0)
            {
                return closest;
            }

            for (var i = 0; i < grabbable.Colliders.Count; i++)
            {
                var gc = grabbable.Colliders[i];
                if (!gc || !gc.enabled || !gc.gameObject.activeInHierarchy || gc.isTrigger)
                    continue;

                var anchor = Palm.transform.position;
                Vector3 point;
                if (grabbable.HasConcaveColliders && gc is MeshCollider meshCollider && !meshCollider.convex ||
                    grabbable.HasWheelCollider && gc is WheelCollider wheelCollider)
                {
                    if (!gc.Raycast(new Ray(anchor, Palm.transform.forward), out var hit, .3f))
                    {
                        continue;
                    }

                    point = hit.point;
                }
                else
                {
                    point = gc.ClosestPoint(anchor);
                }

                if (point == Palm.transform.position || Vector3.Distance(Palm.transform.position, point) < .00001f)
                {
                    inside = true;
                    //palm is inside the collider or your collider is infinitely small or poorly formed and should be replaced
                    return point;
                }

                var d = Vector3.Distance(point, Palm.transform.position);
                if (d < distance)
                {
                    closest = point;
                    distance = d;
                }
            }

            return closest;
        }

        private bool UseDynamicGrab()
        {
            if (GrabbedTarget.PoseType == PoseType.Offset)
                return false;

            if (GrabbedTarget.Colliders.Count == 0)
            {
                return false;
            }

            return GrabbedTarget.PoseType == PoseType.PhysicPoser || ((!GrabPoint || GrabPoint == GrabbedTarget.transform) && GrabbedTarget.PhysicsPoserFallback);
        }

        private IEnumerator MoveGrab()
        {
            _moveGrab = true;
            //var clone = Instantiate(HandModel, GrabbedTarget.transform, true);
            //clone.position = PoseWorldPosition;
            //clone.rotation = PoseWorldRotation;
            _handMoving = true;
            var target = PoseWorldPosition;
            var offset = -HandModel.localPosition;
            var start = transform.position;

            if (IsLineGrab)
            {
                GrabAnchorLocal = GetGrabbableAnchor(GrabbedTarget, PosableGrabPoint);
                target = GrabAnchorWorld;
                offset = -GetLineGrabHandAnchor(PosableGrabPoint);
            }


            var time = (target + transform.TransformDirection(offset) - transform.position).magnitude / HandGrabSpeed;
            var elapsed = 0f;

            Rigidbody.detectCollisions = false;
            var startRot = HandModel.rotation;

            while (elapsed < time && GrabbedTarget)
            {
                if (IsLineGrab)
                {
                    target = GrabAnchorWorld;
                }
                else
                {
                    target = PoseWorldPosition;
                }


                elapsed += Time.fixedDeltaTime;

                transform.position = Vector3.Lerp(start, target + transform.TransformDirection(offset), elapsed / time);
                transform.rotation = Quaternion.Lerp(startRot, PoseWorldRotation, elapsed / time) * Quaternion.Inverse(HandModelRotation);

                yield return _wffu;
            }

            _handMoving = false;


            if (!GrabbedTarget)
                yield break;

            if (!GrabbedTarget.DisableHandCollision)
            {
                _checkingEnableCollision = true;
            }


            var deltaRot = CachedWorldRotation * Quaternion.Inverse(PoseWorldRotation);
            transform.rotation = Quaternion.Inverse(deltaRot) * transform.rotation;
            transform.position = target + transform.TransformDirection(offset);
            //var angleDelta = Quaternion.Angle(PoseWorldRotation, HandWorldRotation);
            //Debug.Log($"after movegrab {angleDelta}");

            GrabPointGrab(GrabbedTarget);
        }

        private void GrabPointGrab(HVRGrabbable grabbable)
        {
            Grab(grabbable);
            if (grabbable.PoseImmediately)
                PoseHand(GrabbedTarget.ParentHandModel);
        }

        public virtual void NetworkGrab(HVRGrabbable grabbable)
        {
            CommonGrab(grabbable);
        }

        protected virtual void Grab(HVRGrabbable grabbable)
        {
            CommonGrab(grabbable);
            Grabbed.Invoke(this, grabbable);
        }

        private void CommonGrab(HVRGrabbable grabbable)
        {
            SetupGrab(grabbable);
            IsClimbing = grabbable.GetComponent<HVRClimbable>();
            if (grabbable.HandGrabbedClip)
                if (SFXPlayer.Instance)
                    SFXPlayer.Instance.PlaySFX(grabbable.HandGrabbedClip, transform.position);
        }

        public void SetupGrab(HVRGrabbable grabbable)
        {
            if (grabbable.IsJointGrab)
            {
                SetJointAnchors(grabbable);

                bool final;
                if (!grabbable.Rigidbody)
                {
                    final = true;
                }
                else
                {
                    //determine if a pull to hand joint should be enabled, or should the final strong joint be enabled
                    final = grabbable.PoseType == PoseType.Offset || grabbable.Stationary ||
                            grabbable.Rigidbody.isKinematic ||
                            _moveGrab || _forceFullyGrabbed
                            || _socketGrab && HoveredSocket.InstantHandPose;
                    _moveGrab = false;
                }

                if (final)
                {
                    SetupConfigurableJoint(grabbable);
                }
                else //needs pulling and rotating into position
                {
                    StartPull();
                }
            }

            if (GrabPoint)
            {
                grabbable.HeldGrabPoints.Add(GrabPoint);
            }
        }

        internal Vector3 GetGrabbableAnchor(HVRGrabbable grabbable, HVRPosableGrabPoint posableGrabPoint)
        {
            var grabTransform = grabbable.transform;

            if (grabbable.Rigidbody) grabTransform = grabbable.Rigidbody.transform;

            if (IsLineGrab)
            {
                return grabTransform.InverseTransformPoint(PosableGrabPoint.WorldLineMiddle);
            }

            var positionOffset = HandModelPosition;
            var rotationOffset = HandModelRotation;

            if (posableGrabPoint)
            {
                if (posableGrabPoint.IsJointAnchor)
                    return posableGrabPoint.transform.localPosition;
                positionOffset = posableGrabPoint.GetPosePositionOffset(HandSide);
                rotationOffset = posableGrabPoint.GetPoseRotationOffset(HandSide);
            }
            else if (IsPhysicsPose)
            {
                positionOffset = PhysicsHandPosition;
                rotationOffset = PhysicsHandRotation;
            }
            else
            {
                return grabTransform.InverseTransformPoint(JointAnchorWorldPosition);
            }

            _fakeHand.localPosition = HandModelPosition;
            _fakeHand.localRotation = HandModelRotation;

            if (IsPhysicsPose)
            {
                _fakeHandAnchor.position = Palm.position;
            }
            else
            {
                _fakeHandAnchor.position = JointAnchorWorldPosition;
            }

            _fakeHand.parent = GrabPoint;
            _fakeHand.localPosition = positionOffset;
            _fakeHand.localRotation = rotationOffset;

            var anchor = grabTransform.InverseTransformPoint(_fakeHandAnchor.position);

            _fakeHand.parent = transform;


            return anchor;
        }

        internal Vector3 GetAnchorInGrabbableSpace(HVRGrabbable grabbable, HVRPosableGrabPoint posableGrabPoint)
        {
            var grabTransform = grabbable.transform;

            if (grabbable.Rigidbody) grabTransform = grabbable.Rigidbody.transform;

            if (posableGrabPoint.IsJointAnchor)
                return posableGrabPoint.transform.localPosition;

            var positionOffset = posableGrabPoint.GetPosePositionOffset(HandSide);
            var rotationOffset = posableGrabPoint.GetPoseRotationOffset(HandSide);

            _fakeHand.localPosition = HandModelPosition;
            _fakeHand.localRotation = HandModelRotation;
            _fakeHandAnchor.position = JointAnchorWorldPosition;

            _fakeHand.parent = posableGrabPoint.transform;
            _fakeHand.localPosition = positionOffset;
            _fakeHand.localRotation = rotationOffset;

            var anchor = grabTransform.InverseTransformPoint(_fakeHandAnchor.position);

            _fakeHand.parent = transform;

            return anchor;
        }

        private Vector3 GetHandAnchor()
        {
            if (IsLineGrab)
            {
                return GetLineGrabHandAnchor(PosableGrabPoint);
            }

            if (PosableGrabPoint && PosableGrabPoint.IsJointAnchor)
            {
                var p = Quaternion.Inverse(PosableGrabPoint.GetPoseRotationOffset(HandSide)) * -PosableGrabPoint.GetPosePositionOffset(HandSide);
                p = transform.InverseTransformPoint(HandModel.TransformPoint(p));
                return p;
            }

            if (IsPhysicsPose)
            {
                return Rigidbody.transform.InverseTransformPoint(Palm.position);
            }

            return JointAnchor.localPosition;
        }

        private Vector3 GetLineGrabHandAnchor(HVRPosableGrabPoint grabPoint)
        {
            return Quaternion.Inverse(grabPoint.GetPoseRotationOffset(HandSide) * Quaternion.Inverse(HandModelRotation)) * -grabPoint.GetPosePositionOffset(HandSide) + HandModelPosition;
        }


        public Quaternion JointRotation
        {
            get { return Quaternion.Inverse(GrabbedTarget.transform.rotation) * CachedWorldRotation * Quaternion.Inverse(PoseLocalRotation); }
        }


        private void SetupConfigurableJoint(HVRGrabbable grabbable)
        {
            var axis = Vector3.right;
            var secondaryAxis = Vector3.up;

            if (IsLineGrab)
            {
                axis = grabbable.Rigidbody.transform.InverseTransformDirection(PosableGrabPoint.WorldLine).normalized;
                secondaryAxis = axis.OrthogonalVector();
            }

            if (Joint)
            {
                Destroy(Joint);
            }

            var grabTransform = grabbable.transform;

            var noRB = false;
            GameObject owner;
            if (grabbable.Rigidbody)
            {
                owner = grabbable.Rigidbody.gameObject;
                grabTransform = grabbable.Rigidbody.transform;
            }
            else
            {
                owner = gameObject;
                noRB = true;
            }

            var startRot = grabTransform.localRotation;

            Joint = owner.AddComponent<ConfigurableJoint>();
            Joint.autoConfigureConnectedAnchor = false;
            Joint.configuredInWorldSpace = false;

            if (noRB)
            {
                Joint.anchor = HandAnchorLocal;
                Joint.connectedAnchor = transform.TransformPoint(HandAnchorLocal);
                Joint.connectedBody = null;
            }
            else
            {
                Joint.anchor = GrabAnchorLocal;
                Joint.connectedAnchor = HandAnchorLocal;
                Joint.connectedBody = Rigidbody;
            }

            Joint.axis = axis;
            Joint.secondaryAxis = secondaryAxis;
            Joint.swapBodies = false;

            if (IsLineGrab)
            {
                Joint.anchor = BaseLineGrabAnchor + _lineOffset;
                _startRotation = Quaternion.Inverse(Quaternion.Inverse(grabTransform.rotation) * transform.rotation);
                Joint.SetTargetRotationLocal(Quaternion.Inverse(Quaternion.Inverse(grabTransform.rotation) * transform.rotation), _startRotation);
            }
            else
            {
                Joint.SetTargetRotationLocal(startRot * JointRotation, startRot);
            }

            grabbable.AddJoint(Joint, this);

            HVRJointSettings settings;
            if (grabbable.JointOverride)
            {
                settings = grabbable.JointOverride;
            }
            else if (IsLineGrab)
            {
                settings = HVRSettings.Instance.LineGrabSettings;
            }
            else if (HVRSettings.Instance.DefaultJointSettings)
            {
                settings = HVRSettings.Instance.DefaultJointSettings;
            }
            else
            {
                Debug.LogError("HVRGrabbable:JointOverride or HVRSettings:DefaultJointSettings must be populated.");
                return;
            }

            settings.ApplySettings(Joint);

            if (grabbable.TrackingType == HVRGrabTracking.FixedJoint)
            {
                Joint.xMotion = ConfigurableJointMotion.Locked;
                Joint.yMotion = ConfigurableJointMotion.Locked;
                Joint.zMotion = ConfigurableJointMotion.Locked;
                Joint.angularXMotion = ConfigurableJointMotion.Locked;
                Joint.angularYMotion = ConfigurableJointMotion.Locked;
                Joint.angularZMotion = ConfigurableJointMotion.Locked;
            }

            if (IsLineGrab)
            {
                _tightlyHeld = Inputs.GetGripHoldActive(HandSide);

                if (!_tightlyHeld || PosableGrabPoint.LineFreeRotation)
                {
                    SetupLooseLineGrab();
                }
            }

            OnHandAttached();
            BreakDistanceCooldown(2f);
            _finalJointCreated = true;
        }

        public void SetJointAnchors(HVRGrabbable grabbable)
        {
            GrabAnchorLocal = GetGrabbableAnchor(grabbable, PosableGrabPoint);
            HandAnchorLocal = GetHandAnchor();
        }

        protected virtual void OnHandAttached()
        {
            OnGrabbedHaptics();
        }

        protected virtual void OnGrabbedHaptics()
        {
            if (IsMine && HVRInputManager.Instance.GrabHaptics)
            {
                Controller.Vibrate(HVRInputManager.Instance.GrabHaptics.HandGrab);
            }
        }

        private void UpdateLineGrab()
        {
            if (!IsLineGrab || PullingGrabbable || !_finalJointCreated || !Joint)
                return;

            bool tighten;
            bool loosen;

            if (HVRSettings.Instance.LineGrabTriggerLoose)
            {
                tighten = !IsTriggerGrabActive;
                loosen = IsTriggerGrabActive;
            }
            else
            {
                tighten = GrabTrigger == HVRGrabTrigger.Active && (IsGripGrabActive || GrabToggleActive || !CanRelease) ||
                          (GrabTrigger == HVRGrabTrigger.Toggle || GrabToggleActive || !CanRelease) && !IsTriggerGrabActive;

                loosen = GrabTrigger == HVRGrabTrigger.Active && !IsGripGrabActive && !GrabToggleActive && CanRelease ||
                         (GrabTrigger == HVRGrabTrigger.Toggle || GrabToggleActive || !CanRelease) && IsTriggerGrabActive;
            }

            if (PosableGrabPoint.LineCanReposition || PosableGrabPoint.LineCanRotate)
            {
                if (!_tightlyHeld && tighten)
                {
                    _tightlyHeld = true;

                    if (!PosableGrabPoint.LineFreeRotation)
                    {
                        var settings = GrabbedTarget.JointOverride ? GrabbedTarget.JointOverride : HVRSettings.Instance.LineGrabSettings;
                        settings.ApplySettings(Joint);
                        Joint.SetTargetRotationLocal(Quaternion.Inverse(Quaternion.Inverse(GrabbedTarget.transform.rotation) * transform.rotation), _startRotation);
                    }

                    var mid = GrabbedTarget.transform.InverseTransformPoint(PosableGrabPoint.WorldLineMiddle);
                    _lineOffset = GetLocalLineGrabPoint(GrabbedTarget, transform.TransformPoint(HandAnchorLocal), PosableGrabPoint) - mid;
                    Joint.anchor = BaseLineGrabAnchor + _lineOffset;
                }
                else if (_tightlyHeld && loosen)
                {
                    _tightlyHeld = false;
                    SetupLooseLineGrab();
                }
            }
        }

        private void SetupLooseLineGrab()
        {
            if (PosableGrabPoint.LineCanReposition)
            {
                Joint.xMotion = ConfigurableJointMotion.Limited;
                var limit = Joint.linearLimit;
                limit.limit = PosableGrabPoint.WorldLine.magnitude / 2f;
                Joint.linearLimit = limit;
                Joint.anchor = BaseLineGrabAnchor;

                var xDrive = Joint.xDrive;
                xDrive.positionSpring = 0;
                xDrive.positionDamper = PosableGrabPoint.LooseDamper;
                xDrive.maximumForce = 100000f;
                Joint.xDrive = xDrive;
            }

            if (PosableGrabPoint.LineCanRotate || PosableGrabPoint.LineFreeRotation)
            {
                Joint.angularXMotion = ConfigurableJointMotion.Free;


                var xDrive = Joint.angularXDrive;
                xDrive.positionSpring = 0;
                xDrive.positionDamper = PosableGrabPoint.LooseAngularDamper;
                xDrive.maximumForce = 100000f;
                Joint.angularXDrive = xDrive;
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void OnReleased(HVRGrabbable grabbable)
        {
            if (HVRSettings.Instance.VerboseHandGrabberEvents)
            {
                Debug.Log($"{name}:OnReleased");
            }

            base.OnReleased(grabbable);

            if (ControllerOffset)
            {
                ControllerOffset.ResetGrabPointOffsets();
            }

            if (Rigidbody.detectCollisions == false)
            {
                Rigidbody.detectCollisions = true;
                StartPushing();
            }


            _checkingEnableCollision = false;
            _primaryGrabPointGrab = false;
            _socketGrab = false;
            _lineOffset = Vector3.zero;
            _currentGrabControl = HVRGrabControls.GripOrTrigger;
            _grabbableControl = HVRGrabControls.GripOrTrigger;
            IsLineGrab = false;

            TriggerGrabPoint = null;

            if (HandModel && HandModel.parent && HandModel.parent.TryGetComponent(out HVRDestroyListener listener))
            {
                listener.Destroyed.RemoveListener(OnGrabPointDestroyed);
            }

            _hasPosed = false;
            ResetHandTransform(HandModel);
            ResetHandTransform(_collisionTransform);
            HandAnimator.OnHeldObjectReleased();
            if (_collisionAnimator) _collisionAnimator.OnHeldObjectReleased();

            IsPhysicsPose = false;
            _physicsPose = null;

            if (ApplyHandLayer) HandModel.SetLayerRecursive(HVRLayers.Hand);
            if (TempGrabPoint) Destroy(TempGrabPoint.gameObject);
            CleanupPull();

            IsClimbing = false;

            if (!grabbable.BeingDestroyed)
            {
                var timeout = grabbable.OverlapTimeout;

                if (grabbable.Rigidbody && !grabbable.Rigidbody.isKinematic)
                {
                    var throwVelocity = ComputeThrowVelocity(grabbable, out var angularVelocity, true);
                    grabbable.Rigidbody.velocity = throwVelocity;
                    grabbable.Rigidbody.angularVelocity = angularVelocity;
                    //prevent clipping on throw
                    if (timeout < .2f && grabbable.Rigidbody.velocity.magnitude > 2f) timeout = .2f;
                }

                if (!IgnoreNextCollisionCheck)
                {
                    if (grabbable.Rigidbody && !grabbable.Rigidbody.isKinematic && (grabbable.RequireOverlapClearance || timeout > 0f))
                    {
                        var routine = StartCoroutine(CheckReleasedOverlap(grabbable, timeout));
                        OverlappingGrabbables[grabbable] = routine;
                    }
                    else
                    {
                        EnableHandCollision(grabbable);
                    }
                }

                grabbable.HeldGrabPoints.Remove(GrabPoint);
            }

            IgnoreNextCollisionCheck = false;

            GrabToggleActive = false;
            GrabPoint = null;
            Released.Invoke(this, grabbable);

            OnReleasedHaptics();
        }

        protected virtual void OnReleasedHaptics()
        {
            if (IsMine && HVRInputManager.Instance.GrabHaptics)
            {
                Controller.Vibrate(HVRInputManager.Instance.GrabHaptics.HandRelease);
            }
        }

        public Vector3 GetAverageVelocity(int frames, int start)
        {
            if (start + frames > TrackedVelocityCount)
                frames = TrackedVelocityCount - start;
            return GetAverageVelocity(frames, start, RecentVelocities, TakePeakVelocities, CountPeakVelocities);
        }

        public Vector3 GetAverageAngularVelocity(int frames, int start)
        {
            if (start + frames > TrackedVelocityCount)
                frames = TrackedVelocityCount - start;
            return GetAverageVelocity(frames, start, RecentAngularVelocities);
        }

        private static readonly List<Vector3> _peakVelocities = new List<Vector3>(10);
        private static readonly IComparer<Vector3> _velocityComparer = new VelocityComparer();


        internal static Vector3 GetAverageVelocity(int frames, int start, CircularBuffer<Vector3> recentVelocities, bool takePeak = false, int nPeak = 3)
        {
            if (frames == 0)
                return Vector3.zero;

            var sum = Vector3.zero;
            for (var i = start; i < start + frames; i++)
            {
                sum += recentVelocities[i];
            }


            var average = sum / frames;

            sum = Vector3.zero;

            _peakVelocities.Clear();

            for (var i = start; i < start + frames; i++)
            {
                //removing any vectors not going in the direction of the average vector
                var dot = Vector3.Dot(average.normalized, recentVelocities[i].normalized);
                if (dot < .2)
                {
                    //Debug.Log($"Filtered {average},{recentVelocities[i]},{dot}");
                    continue;
                }

                if (takePeak)
                {
                    _peakVelocities.Add(recentVelocities[i]);
                }
                else
                {
                    sum += recentVelocities[i];
                }
            }

            if (!takePeak)
            {
                return sum / frames;
            }

            if (nPeak == 0)
                return Vector3.zero;

            sum = Vector3.zero;
            SortHelper.Sort(_peakVelocities, 0, _peakVelocities.Count, _velocityComparer);

            for (int i = _peakVelocities.Count - 1, j = 0; j < nPeak; j++, i--)
            {
                if (i < 0 || i >= _peakVelocities.Count)
                    break;
                sum += _peakVelocities[i];
            }

            return sum / nPeak;
        }


        public Vector3 ComputeThrowVelocity(HVRGrabbable grabbable, out Vector3 angularVelocity, bool isThrowing = false)
        {
            if (!grabbable.Rigidbody)
            {
                angularVelocity = Vector3.zero;
                return Vector3.zero;
            }

            var grabbableVelocity = grabbable.GetAverageVelocity(ThrowLookback, ThrowLookbackStart, TakePeakVelocities, CountPeakVelocities);
            var grabbableAngular = grabbable.GetAverageAngularVelocity(ThrowLookback, ThrowLookbackStart);

            var handVelocity = GetAverageVelocity(ThrowLookback, ThrowLookbackStart);
            var handAngularVelocity = GetAverageAngularVelocity(ThrowLookback, ThrowLookbackStart);

            var throwVelocity = ReleasedVelocityFactor * handVelocity + grabbableVelocity * grabbable.ReleasedVelocityFactor;

            //Debug.Log($"{handAngularVelocity.magnitude}");

            if (handAngularVelocity.magnitude > ReleasedAngularThreshold)
            {
                //compute linear velocity from wrist rotation
                var grabbableCom = GrabPoint ? GrabPoint.position : grabbable.Rigidbody.worldCenterOfMass;

                Vector3 centerOfMass;
                if (ThrowingCenterOfMass && ThrowingCenterOfMass.CenterOfMass)
                {
                    centerOfMass = ThrowingCenterOfMass.CenterOfMass.position;
                }
                else
                {
                    centerOfMass = Rigidbody.worldCenterOfMass;
                }

                var cross = Vector3.Cross(handAngularVelocity, grabbableCom - centerOfMass) * (grabbable.ReleasedAngularConversionFactor * ReleasedAngularConversionFactor);
                throwVelocity += cross;
            }

            angularVelocity = grabbableAngular * grabbable.ReleasedAngularFactor;

            return throwVelocity;
        }


        private IEnumerator CheckReleasedOverlap(HVRGrabbable grabbable, float timeout)
        {
            if (!OverlapSizer || !_overlapCollider)
            {
                yield break;
            }

            yield return _wffu;

            var elapsed = 0f;

            while (OverlappingGrabbables.ContainsKey(grabbable))
            {
                var count = Physics.OverlapSphereNonAlloc(OverlapSizer.transform.position, _overlapCollider.radius, _overlapColliders, ~0, QueryTriggerInteraction.Ignore);
                if (count == 0)
                    break;

                var match = false;
                for (int i = 0; i < count; i++)
                {
                    if (grabbable.IsIgnoreCollider(_overlapColliders[i]))
                    {
                        match = true;
                        break;
                    }
                }

                if (!match)
                    break;

                yield return _wffu;
                elapsed += Time.fixedDeltaTime;

                if (!grabbable.RequireOverlapClearance && elapsed > timeout)
                {
                    StartPushing();
                    break;
                }
            }

            EnableHandCollision(grabbable);

            OverlappingGrabbables.Remove(grabbable);
        }

        public void UpdateCollision(HVRGrabbable grabbable, bool enable)
        {
            if (enable)
            {
                EnableHandCollision(grabbable);
            }
            else
            {
                DisableHandCollision(grabbable);
            }
        }

        public void EnableHandCollision(HVRGrabbable grabbable)
        {
            HandPhysics.IgnoreCollision(grabbable.Colliders, false);
            HandPhysics.IgnoreCollision(grabbable.AdditionalIgnoreColliders, false);
        }

        public void DisableHandCollision(HVRGrabbable grabbable)
        {
            HandPhysics.IgnoreCollision(grabbable.Colliders, true);
            HandPhysics.IgnoreCollision(grabbable.AdditionalIgnoreColliders, true);
        }

        private readonly Dictionary<Transform, int> _layerCache = new Dictionary<Transform, int>();
        private readonly List<Transform> _layerKeys = new List<Transform>(20);
        private ConfigurableJoint PullJoint;
        private Vector3 _startPos;
        private Quaternion _startRot;

        private void DynamicGrab()
        {
            var layerMask = LayerMask.GetMask(HVRLayers.DynamicPose.ToString());
            var layer = LayerMask.NameToLayer(HVRLayers.DynamicPose.ToString());

            for (var i = 0; i < GrabbedTarget.Colliders.Count; i++)
            {
                var col = GrabbedTarget.Colliders[i];
                if (_layerCache.ContainsKey(col.transform)) continue;
                _layerKeys.Add(col.transform);
                _layerCache[col.transform] = col.gameObject.layer;
                col.gameObject.layer = layer;
            }

            try
            {
                if (_savedPose == null) _savedPose = PhysicsPoser.Hand.CreateHandPose();
                else PhysicsPoser.Hand.CopyHandData(_savedPose);

                //todo cache and reuse
                TempGrabPoint = new GameObject(name + " GrabPoint");
                TempGrabPoint.transform.parent = GrabbedTarget.transform;
                TempGrabPoint.transform.ResetLocalProps(true);
                var point = FindClosestPoint(GrabbedTarget, out var inside);
                GrabPoint = TempGrabPoint.transform;

                var count = 0;

                while (inside && count < 5)
                {
                    PhysicsPoser.transform.position -= Palm.forward * .1f;
                    point = FindClosestPoint(GrabbedTarget, out inside);
                    count++;
                }

                if (!inside && DynamicGrabPalmAdjust || _isForceAutoGrab)
                {
                    var delta = point - PhysicsPoser.Palm.position;
                    var palmDelta = Quaternion.FromToRotation(PhysicsPoser.Palm.forward, delta.normalized);
                    PhysicsPoser.transform.rotation = palmDelta * PhysicsPoser.Hand.transform.rotation;
                }

                PhysicsPoser.OpenFingers();

                var offset = PhysicsPoser.transform.position - Palm.position;
                PhysicsPoser.transform.position = point + offset;

                PhysicsPoser.SimulateClose(layerMask);
                if (_physicsPose == null) _physicsPose = PhysicsPoser.Hand.CreateHandPose();
                else PhysicsPoser.Hand.CopyHandData(_physicsPose);

                PhysicsHandRotation = Quaternion.Inverse(GrabbedTarget.transform.rotation) * PhysicsPoser.transform.rotation;
                PhysicsHandPosition = GrabbedTarget.transform.InverseTransformPoint(PhysicsPoser.transform.position);

                _physicsPose.Position = PhysicsHandPosition;
                _physicsPose.Rotation = PhysicsHandRotation;

                //Debug.Log($"{PhysicsHandRotation.eulerAngles}");
                //Debug.Log($"{PhysicsHandPosition}");
                PhysicsPoser.Hand.Pose(_savedPose, true);

#if HVR_PUN
                PhysicsPoseBytes = _physicsPose.Serialize();
#endif
                IsPhysicsPose = true;
            }
            finally
            {
                for (var i = 0; i < _layerKeys.Count; i++)
                {
                    var key = _layerKeys[i];
                    key.gameObject.layer = _layerCache[key];
                }

                _layerCache.Clear();
                _layerKeys.Clear();
            }
        }

        public bool TryTransferDistanceGrab(HVRGrabbable grabbable, HVRPosableGrabPoint grabPoint)
        {
            if (GrabTrigger == HVRGrabTrigger.Active && !Inputs.GetForceGrabActive(HandSide))
            {
                return false;
            }

            grabbable.Rigidbody.velocity = Vector3.zero;
            grabbable.Rigidbody.angularVelocity = Vector3.zero;

            if (grabPoint) GrabPoint = grabPoint.transform;
            else GrabPoint = null;


            _isForceAutoGrab = true;
            _primaryGrabPointGrab = true;

            try
            {
                //if (grabPoint) OrientGrabbable(grabbable, grabPoint, true, false);
                if (TryGrab(grabbable))
                {
                    if (Inputs.ForceGrabActivation == HVRForceGrabActivation.Grip)
                    {
                        _currentGrabControl = HVRGrabControls.GripOnly;
                    }
                    else if (Inputs.ForceGrabActivation == HVRForceGrabActivation.Trigger)
                    {
                        _currentGrabControl = HVRGrabControls.TriggerOnly;
                    }
                    else
                    {
                        if (IsGripGrabActive)
                            _currentGrabControl = HVRGrabControls.GripOnly;
                        else
                            _currentGrabControl = HVRGrabControls.TriggerOnly;
                    }

                    CheckGrabControlSwap();

                    return true;
                }
            }
            finally
            {
                _isForceAutoGrab = false;
            }

            return false;
        }


        private void ResetHandModel()
        {
            _hasPosed = false;

            if (!HandModel)
                return;

            ResetHandTransform(HandModel);
            if (HandAnimator) HandAnimator.ResetToDefault();
            if (_collisionTransform)
            {
                ResetHandTransform(_collisionTransform);
                if (_collisionAnimator) _collisionAnimator.ResetToDefault();
            }
        }

        private void ResetHandTransform(Transform hand)
        {
            if (!hand) return;
            hand.parent = HandModelParent;
            hand.localPosition = HandModelPosition;
            hand.localRotation = HandModelRotation;
            hand.localScale = HandModelScale;
        }

        private void ResetRigidBodyProperties()
        {
            this.ExecuteNextUpdate(() =>
            {
                Rigidbody.ResetCenterOfMass();
                Rigidbody.ResetInertiaTensor();

                if (RigidOverrides)
                {
                    RigidOverrides.ApplyOverrides();
                }
            });
        }

        internal byte[] GetPoseData()
        {
            return _posableHand.CreateHandPose().Serialize();
        }

        internal void PoseHand(byte[] data)
        {
            if (CloneHandModel && _collisionHand)
            {
                _collisionHand.Pose(HVRHandPoseData.FromByteArray(data, HandSide), GrabbedTarget.ParentHandModel);
            }

            _posableHand.Pose(HVRHandPoseData.FromByteArray(data, HandSide), GrabbedTarget.ParentHandModel);
        }

        public void ChangeGrabPoint(HVRPosableGrabPoint grabPoint, float time, HVRAxis axis)
        {
            if (!GrabbedTarget || _swappingGrabPoint || GrabbedTarget.IsStabbing)
                return;

            StartCoroutine(SwapGrabPoint(grabPoint, time, axis));
        }


        protected virtual IEnumerator SwapGrabPoint(HVRPosableGrabPoint grabPoint, float time, HVRAxis axis)
        {
            var grabbable = GrabbedTarget;
            _swappingGrabPoint = true;

            try
            {
                PoseLocalRotation = grabPoint.GetGrabbableRelativeRotation(HandSide);

                var startRot = Quaternion.Inverse(HandModel.rotation) * GrabbedTarget.transform.rotation;
                var targetRot = Quaternion.Inverse(grabPoint.GetPoseWorldRotation(HandSide)) * GrabbedTarget.transform.rotation;

                var startPos = HandModel.transform.InverseTransformPoint(GrabbedTarget.transform.position);
                var targetPos = Quaternion.Inverse(grabPoint.GetPoseWorldRotation(HandSide)) * (GrabbedTarget.transform.position - grabPoint.GetPoseWorldPosition(HandSide));

                GrabPoint = grabPoint.transform;
                GrabbedTarget.RemoveJoint(this);

                if (HandAnimator)
                {
                    ResetHandModel();
                    HandAnimator.IgnoreCurls = true;
                    HandAnimator.ZeroFingerCurls();
                }

                CanRelease = false;

                if (GrabbedTarget.Rigidbody)
                {
                    GrabbedTarget.Rigidbody.detectCollisions = false;
                }


                if (time > 0f)
                {
                    var elapsed = 0f;
                    var vAxis = axis.GetVector();
                    var va = vAxis.OrthogonalVector();
                    var v1 = GrabbedTarget.transform.rotation * va;
                    var v2 = (HandModel.rotation * targetRot) * va;

                    var angle = Vector3.Angle(v1, v2);
                    var sign = Mathf.Sign(Vector3.Dot(GrabbedTarget.transform.rotation * vAxis, Vector3.Cross(v1, v2)));
                    angle = (angle * sign + 360) % 360;

                    while (GrabbedTarget && elapsed < time)
                    {
                        GrabbedTarget.transform.rotation = HandModel.rotation * startRot * Quaternion.AngleAxis(angle * elapsed / time, axis.GetVector());
                        GrabbedTarget.transform.position = HandModel.transform.TransformPoint(Vector3.Lerp(startPos, targetPos, elapsed / time));
                        if (GrabbedTarget.Rigidbody)
                            GrabbedTarget.Rigidbody.velocity = GrabbedTarget.Rigidbody.angularVelocity = Vector3.zero;
                        elapsed += Time.deltaTime;
                        yield return null;
                    }
                }

                if (!GrabbedTarget || grabbable != GrabbedTarget)
                {
                    yield break;
                }

                PoseHand(GrabbedTarget.ParentHandModel);

                GrabbedTarget.transform.rotation = HandModel.rotation * targetRot;
                GrabbedTarget.transform.position = HandModel.transform.TransformPoint(targetPos);

                SetJointAnchors(GrabbedTarget);
                SetupConfigurableJoint(GrabbedTarget);
            }
            finally
            {
                _swappingGrabPoint = false;
                CanRelease = true;

                if (grabbable && grabbable.Rigidbody)
                {
                    grabbable.Rigidbody.detectCollisions = true;
                }

                if (HandAnimator)
                {
                    HandAnimator.IgnoreCurls = false;
                }
            }
        }


        /// <summary>
        /// Will grab the provided object using the provided grab point, if the grab point isn't provided then the first valid one on the object will be used.
        /// If there are no grab points that are allowed to be grabbed by this hand you shouldn't use this method.
        /// If a grab point is found it will use the saved pose information to orient the object in the hand.
        /// If the CollisionHandler field on this hand is populated, it will do a post teleport sweep to try and prevent overlapping collisions due to the object being teleported.
        /// If grabTrigger is set to toggle or manual release, it will temporarily override the hand grabber / grabbables GrabTrigger while held.
        /// If you provide 'Active'then either the hand or the grabbable need their GrabTrigger set appropriately otherwise the object will just drop on the next frame.
        /// </summary>
        public virtual void Grab(HVRGrabbable grabbable, HVRGrabTrigger grabTrigger, HVRPosableGrabPoint grabPoint = null)
        {
            try
            {
                if (grabbable.IsBeingHeld)
                    grabbable.ForceRelease();

                if (!grabPoint)
                    grabPoint = grabbable.GetGrabPoint(this, GrabpointFilter.Normal);

                if (!grabPoint)
                    return;

                GrabPoint = grabPoint.transform;

                _forceFullyGrabbed = true;

                OrientGrabbable(grabbable, grabPoint);
                GrabGrabbable(this, grabbable);

                if (grabTrigger == HVRGrabTrigger.Toggle)
                    GrabToggleActive = true;
                else if (grabTrigger == HVRGrabTrigger.ManualRelease)
                    CanRelease = false;


                if (CollisionHandler)
                    this.ExecuteNextUpdate(() => CollisionHandler.Sweep(this));
            }
            finally
            {
                _forceFullyGrabbed = false;
            }
        }

        /// <summary>
        /// Immediately puts the grabbable object into pose position relative to the hand
        /// </summary>
        public void OrientGrabbable(HVRGrabbable grabbable, HVRPosableGrabPoint grabPoint, bool position = true, bool rotation = true)
        {
            var deltaRot = CachedWorldRotation * Quaternion.Inverse(grabPoint.GetPoseWorldRotation(HandSide));
            if (rotation) grabbable.MainTransform.rotation = deltaRot * grabbable.MainTransform.rotation;
            if (position) grabbable.MainTransform.position += (HandModel.position - grabPoint.GetPoseWorldPosition(HandSide));
        }

        public override void ForceRelease()
        {
            base.ForceRelease();
            CanRelease = true;
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (DrawCenterOfMass && Rigidbody)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(Rigidbody.worldCenterOfMass, .03f);
            }

            //if (_configurableJoint)
            //{
            //    Gizmos.color = Color.cyan;
            //    Gizmos.DrawWireSphere(_configurableJoint.transform.TransformPoint(_configurableJoint.anchor), .02f);
            //    Gizmos.color = Color.blue;
            //    Gizmos.DrawCube(transform.TransformPoint(_configurableJoint.connectedAnchor), new Vector3(.02f, .02f, .02f));
            //}

            //if (PosableGrabPoint && (IsHovering || IsGrabbing))
            //{
            //    Gizmos.color = Color.red;
            //    Gizmos.DrawCube(PoseWorldPosition, new Vector3(.02f, .02f, .02f));
            //    //Debug.DrawLine(PoseWorldPosition, GrabPoint.position, Color.green);

            //    Gizmos.color = Color.blue;

            //    var grabbable = HoverTarget ?? GrabbedTarget;

            //    var p = Quaternion.Inverse(PosableGrabPoint.GetPoseRotationOffset(HandSide) * Quaternion.Inverse(HandModelRotation)) * -(PosableGrabPoint.GetPosePositionOffset(HandSide));

            //    Gizmos.DrawCube(transform.TransformPoint(p), new Vector3(.02f, .02f, .02f));
            //}
        }

#endif
    }

    internal class VelocityComparer : IComparer<Vector3>
    {
        public int Compare(Vector3 x, Vector3 y)
        {
            return x.magnitude.CompareTo(y.magnitude);
        }
    }

    public enum DynamicPoseGrabIndicator
    {
        Transform,
        Palm,
        None
    }
}