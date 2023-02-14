using System;
using System.Linq;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Core.Player
{
    public class HVRTeleporter : MonoBehaviour
    {
        [Header("Transforms / Components")]
        public Transform Camera;

        public Transform TeleportLineSourceLeft;
        public Transform TeleportLineSourceRight;
        public HVRHandGrabber LeftHand;
        public HVRHandGrabber RightHand;
        public HVRPlayerController Player;

        [Header("Teleport Disable Checks")]
        [Tooltip("Should player rotation disable teleport aiming?")]
        public bool PlayerRotateCheck = true;

        [Tooltip("If the player is not grounded, should teleport be disabled.")]
        public bool PlayerGroundedCheck = true;

        [Tooltip("If the player is climbing, should teleport be disabled.")]
        public bool PlayerClimbingCheck = true;

        [Tooltip("If turning how long of a timeout to wait before allowing joystick teleporting to prevent accidental teleporting when turning with the same joystick")]
        public float RotationTeleportThreshold = .3f;


        [Header("Visuals")]
        public Color ValidColor = new Color(2, 212, 186);

        public Color InvalidColor = new Color(221, 37, 37);
        public LineRenderer LineRenderer;
        public LineRenderer DownRenderer;
        public HVRTeleportMarkerBase TeleportMarker;
        public float DownLineMinLength = .2f;
        public float DownLineMaxLength = .2f;
        public bool DisableMarkerWhenInvalid;


        [Header("Teleport Curve")]
        public HVRTeleportCurve CurveType;

        [Range(90f, 179f)]
        public float MaxAngle = 179f;

        public int LineSegments = 20;

        [Tooltip("The hit point is backed from the point of collision by this distance to provide a visual buffer")]
        public float CollisionBuffer = .05f;

        [Tooltip("Teleport curve layer mask")]
        public LayerMask LayerMask = 1 << 0;

        [Tooltip("Trigger setting for ray cast")]
        public QueryTriggerInteraction LayerMaskTriggerInteraction = QueryTriggerInteraction.Ignore;

        [Header("Ballistic Settings")]
        public float Speed = 7.5f;

        public float TimeStep = .01f;

        [Header("Bezier Settings")]
        public float BezierAngleOffset = .3f;

        public AnimationCurve BezierAngleOffsetCurve;
        public float TeleportDistance = 5f;


        [Header("Downward Ray cast")]
        [Tooltip("Max length of the downwards ray cast")]
        public float DownRayLength = 5f;

        [Tooltip("Layers to ignore along the downwards ray cast")]
        public LayerMask DownRayIgnoreMask = HVRConstants.PlayerHandGrabbableMask;


        [Header("Dash Teleport")]
        public bool Dash;

        public float DashSpeed = 15f;

        [Header("Origin Line Of Sight")]
        [Tooltip("If true line of sight from the camera to the origin of the teleport line is required")]
        public bool RequireOriginLineOfSight;

        [Tooltip("Layers ignored when checking line of sight to the teleport origin ")]
        public LayerMask OriginIgnoreLayerMask = HVRConstants.PlayerHandGrabbableMask;


        [Header("Destination Line Of Sight")]
        [Tooltip("If true line of sight from the camera to the teleport destination is required")]
        public bool RequireDestinationLineOfSight;

        [Tooltip("How high above the destination the line of sight check will be performed")]
        public float DestinationHeight = 1f;

        [Tooltip("How many ray casts above the teleport point will be used to determine destination line of sight")]
        public int DestinationIntervals = 15;

        public LayerMask DestinationIgnoreLayerMask;


        [Header("Destination Validation")]
        [Tooltip("If true the fall distance from the bottom of the capsule cannot exceed MaxDropDistance")]
        public bool CheckDropDistance;

        [Tooltip("Max fall distance that is calculated from the bottom of the provided Capsule Collider or CharacterController")]
        public float MaxDropDistance = 3f;


        public bool CheckJumpDistance;
        public float MaxJumpDistance = 1.5f;

        [Tooltip("Ignored layers when checking if the player fits in the target destination")]
        public LayerMask PlayerFitIgnoreLayerMask = HVRConstants.PlayerHandGrabbableMask;

        [Tooltip("Layers of the ray hit object that the player can stand on")]
        public LayerMask TeleportableLayers = ~(HVRConstants.DefaultHandMask | HVRConstants.DefaultPlayerMask);

        [Tooltip("Max angle allowed to teleport onto")]
        [Range(0f, 90f)]
        public float MaxSurfaceAngle = 45f;

        [Header("Capsule Collider Override")]
        public CapsuleCollider Capsule;


        public TeleportPositionUpdate BeforeDashTeleport = new TeleportPositionUpdate();
        public TeleportPositionUpdate AfterDashTeleport = new TeleportPositionUpdate();
        public TeleportPositionUpdate BeforeTeleport = new TeleportPositionUpdate();
        public UnityEvent AfterTeleport = new UnityEvent();
        public TeleportPositionUpdate PositionUpdate = new TeleportPositionUpdate();


        [Header("Debugging")]
        public Vector3 DownHitNormal;

        public float SurfaceAngle;
        public bool IsDropDistanceValid;
        public bool IsSurfaceAngleValid;
        public bool IsDestinationInLineOfSight;
        public bool IsOnlyDropDistanceInvalid;
        public bool IsOriginInLineOfSight;
        public bool IsJumpDistanceValid;
        public bool DoesPlayerFit;
        public TeleportState TeleportState = TeleportState.None;


        /// <summary>
        /// The last collider hit by the forward raycast
        /// </summary>
        protected Collider HitCollider { get; set; }

        /// <summary>
        /// The last collider hit by the downward raycast
        /// </summary>
        protected Collider DownHitCollider { get; set; }

        protected Vector3[] LineRendererPoints { get; set; }

        public virtual HVRHandSide TeleportHand => PlayerInputs.TeleportHandSide;

        /// <summary>
        /// The world position of the valid teleport destination
        /// </summary>
        public Vector3 TeleportDestination { get; protected set; }

        public Color Color => IsTeleportValid ? ValidColor : InvalidColor;

        public HVRPlayerInputs PlayerInputs { get; private set; }

        /// <summary>
        /// World position where the teleport line collided
        /// </summary>
        public Vector3 HitPosition { get; protected set; }

        /// <summary>
        /// The last valid raycast origin
        /// </summary>
        public Vector3 LastValidPoint => LineRendererPoints[LastValidIndex];

        /// <summary>
        /// The last raycast origin
        /// </summary>
        public Vector3 LastPoint => LineRendererPoints[LastIndex];

        /// <summary>
        /// Last world position hit by the downward raycast
        /// </summary>
        public Vector3 LastDownwardPoint { get; set; }

        /// <summary>
        /// Last valid world position hit by the downward raycast
        /// </summary>
        public Vector3 LastValidDownwardPoint { get; set; }


        /// <summary>
        /// Last index of the curve array that produced a valid destination for the player to land
        /// </summary>
        public int LastValidIndex { get; protected set; }

        /// <summary>
        /// Last index of the curve array before the ray cast hit something.
        /// </summary>
        public int LastIndex { get; protected set; }

        public virtual Transform TeleportLineSource => TeleportHand == HVRHandSide.Left ? TeleportLineSourceLeft : TeleportLineSourceRight;

        /// <summary>
        /// Did the forward raycast find a valid teleportable location
        /// </summary>
        public bool IsRaycastValid { get; set; }

        /// <summary>
        /// Normal of the plane hit by the valid raycast.
        /// </summary>
        public Vector3 SurfaceNormal { get; protected set; }

        public Vector3 Origin => TeleportLineSource.position;
        public Vector3 Forward => TeleportLineSource.forward;

        public CharacterController CharacterController { get; private set; }
        public bool CanTeleport { get; protected set; }
        public bool IsTeleporting { get; protected set; }
        public Vector3 CapsuleBottom => Capsule ? Capsule.GetBottom() : CharacterController.GetBottom();

        protected virtual bool LeftHandPrevents { get; set; }

        protected virtual bool RightHandPrevents { get; set; }

        protected virtual bool HandPrevents => LeftHandPrevents || RightHandPrevents;

        public bool IsTeleportPreviouslyValid { get; protected set; }

        public bool IsTeleportValid { get; protected set; }

        public bool IsAiming { get; protected set; }

        protected bool PreviousAiming { get; set; }

        private readonly Collider[] _dummy = new Collider[1];
        protected HVRInvalidTeleport _dummyInvalid;

        protected float _timeSinceLastRotation;
        protected Quaternion _previousPlayerRotation;
        private Vector3 _leftVel, _rightVel, _leftHeldVel, _rightHeldVel;


        protected virtual void Awake()
        {
            CharacterController = GetComponent<CharacterController>();
            CanTeleport = true;
            if (!Camera)
            {
                Camera = GetComponentInChildren<HVRCamera>()?.transform;
            }

            PlayerInputs = GetComponent<HVRPlayerInputs>();
            LineRendererPoints = new Vector3[LineSegments];

            if (TeleportMarker)
            {
                TeleportMarker.Teleporter = this;
            }

            if (!Player)
            {
                Player = GetComponentInChildren<HVRPlayerController>();
            }
        }


        protected virtual void Start()
        {
            if (!LeftHand)
            {
                LeftHand = transform.root.GetComponentsInChildren<HVRHandGrabber>().FirstOrDefault(e => e.HandSide == HVRHandSide.Left);
            }

            if (!RightHand)
            {
                RightHand = transform.root.GetComponentsInChildren<HVRHandGrabber>().FirstOrDefault(e => e.HandSide == HVRHandSide.Right);
            }

            if (LeftHand)
            {
                LeftHand.Grabbed.AddListener(LeftHandGrabbed);
                LeftHand.Released.AddListener(LeftHandReleased);
            }

            if (RightHand)
            {
                RightHand.Grabbed.AddListener(RightHandGrabbed);
                RightHand.Released.AddListener(RightHandReleased);
            }
        }


        public virtual void Enable()
        {
            CanTeleport = true;
        }

        public virtual void Disable()
        {
            IsAiming = false;
            CanTeleport = false;
            IsTeleportValid = false;
            ToggleGraphics(false);
        }

        protected virtual void Update()
        {
            CheckTeleport();
            EnabledCheck();
            if (IsAiming)
            {
                IsTeleportPreviouslyValid = IsTeleportValid;
                BeforeRaycast();
                Raycast();
                AfterRaycast();
                if (HandPrevents)
                {
                    IsTeleportValid = false;
                }

                SurfaceAngle = Vector3.Angle(Vector3.up, SurfaceNormal);
                CheckValidTeleportChanged(IsTeleportPreviouslyValid);
                HandleValidStatus(IsTeleportValid);
                UpdateTeleportMarker(IsTeleportValid);
            }

            CheckPlayerRotation();

            PreviousAiming = IsAiming;
        }

        protected virtual void CheckPlayerRotation()
        {
            if (Player && Quaternion.Angle(Player.transform.rotation, _previousPlayerRotation) > 1f)
            {
                _timeSinceLastRotation = 0f;
            }
            else
            {
                _timeSinceLastRotation += Time.deltaTime;
            }

            _previousPlayerRotation = Player.transform.rotation;
        }

        protected virtual void EnabledCheck()
        {
            if (PlayerGroundedCheck && Player && !Player.IsGrounded)
            {
                Disable();
                return;
            }

            if (PlayerRotateCheck && Player && _timeSinceLastRotation < RotationTeleportThreshold && !IsAiming)
            {
                Disable();
                return;
            }

            if (PlayerClimbingCheck && Player && Player.IsClimbing)
            {
                Disable();
                return;
            }

            Enable();
        }

        protected virtual void FixedUpdate()
        {
            if (TeleportState == TeleportState.Dashing)
            {
                UpdateDashTeleport();
            }
            else if (TeleportState == TeleportState.Teleporting)
            {
                UpdateTeleport();
                OnAfterTeleport();
            }
        }

        protected virtual void CheckTeleport()
        {
            if (!CheckCanTeleport())
            {
                return;
            }

            if (!IsAiming && IsTeleportActivated())
            {
                OnTeleportActivated();
            }
            else if (IsTeleportDeactivated())
            {
                OnTeleportDeactivated();
            }
        }

        protected virtual void BeforeRaycast()
        {
            IsTeleportValid = false;
            LastValidIndex = 0;
            LastIndex = 0;
            HitCollider = null;
            DownHitCollider = null;
            IsOnlyDropDistanceInvalid = false;
        }

        protected virtual void Raycast()
        {
            CalculateCurve(LineRendererPoints);

            HitPosition = LineRendererPoints[LineRendererPoints.Length - 1];

            IsRaycastValid = false;

            for (var i = 0; i < LineRendererPoints.Length - 1; i++)
            {
                var origin = LineRendererPoints[i];
                var target = LineRendererPoints[i + 1];

                Vector3 destination;

                if (ProjectForwardRay(origin, target, out var forwardHit))
                {
                    var direction = forwardHit.point - origin;
                    HitPosition = forwardHit.point - direction.normalized * CollisionBuffer;
                    HitCollider = forwardHit.collider;

                    destination = forwardHit.point; // + VerticalBuffer;

                    if (CheckValidDestination(HitCollider.gameObject, destination, forwardHit.normal))
                    {
                        IsTeleportValid = true;
                        LastValidIndex = i;
                        TeleportDestination = destination;
                        IsRaycastValid = true;
                        SurfaceNormal = forwardHit.normal;
                    }

                    break;
                }

                LastIndex = i;

                if (!ProjectDownwardRay(LineRendererPoints[i], out var downwardHit, DownRayLength))
                {
                    continue;
                }


                DownHitCollider = downwardHit.collider;
                LastDownwardPoint = downwardHit.point;
                DownHitNormal = downwardHit.normal;

                destination = downwardHit.point; // + VerticalBuffer;

                if (!CheckValidDestination(downwardHit.collider.gameObject, destination, downwardHit.normal))
                {
                    continue;
                }

                LastValidDownwardPoint = LastDownwardPoint;
                IsTeleportValid = true;
                TeleportDestination = destination;
                LastValidIndex = i;
                SurfaceNormal = downwardHit.normal;
            }

            AfterRaycast();
        }


        protected virtual void AfterRaycast()
        {
            var downOrigin = LastValidPoint;
            var downTarget = TeleportDestination;
            var lastValidIndex = LastValidIndex;

            var fallDistanceValid = CheckFallDistance(TeleportDestination);
            var originValid = CheckOriginLineOfSight();
            if (IsTeleportValid && (!fallDistanceValid || !originValid))
            {
                IsOnlyDropDistanceInvalid = true;
                IsTeleportValid = false;
                downOrigin = LastPoint;
                downTarget = LastDownwardPoint;
                lastValidIndex = LastIndex;
            }
            else
            {
                LineRendererPoints[LastValidIndex + 1] = HitPosition;
            }

            UpdateLineRenderer(HitPosition, lastValidIndex, IsRaycastValid);
            UpdateMarkerPosition();
            UpdateDownRenderer(downOrigin, downTarget, IsRaycastValid);
        }

        protected virtual void CheckValidTeleportChanged(bool previousValid)
        {
            if (previousValid != IsTeleportValid || !PreviousAiming && IsAiming)
            {
                OnValidTeleportChanged(IsTeleportValid);
            }
        }

        protected virtual void HandleValidStatus(bool valid)
        {
        }

        protected virtual void OnTeleportActivated()
        {
            ToggleGraphics(true);
            IsAiming = true;
        }

        protected virtual void OnTeleportDeactivated()
        {
            if (IsAiming)
            {
                if (IsTeleportValid)
                {
                    OnValidTeleport();
                    IsTeleportValid = false;
                }
            }

            ToggleGraphics(false);
            IsAiming = false;
        }

        protected virtual void ToggleGraphics(bool toggle)
        {
            if (TeleportMarker)
            {
                if (toggle)
                {
                    TeleportMarker.Activate();
                }
                else
                {
                    TeleportMarker.Deactivate();
                }
            }

            LineRenderer.enabled = toggle;
            if (DownRenderer)
            {
                DownRenderer.enabled = toggle;
            }
        }


        protected virtual bool CheckCanTeleport()
        {
            return CanTeleport && !IsTeleporting;
        }

        /// <summary>
        /// Determine if the origin of the teleport is in line of sight of the player's hmd.
        /// </summary>
        protected virtual bool CheckOriginLineOfSight()
        {
            IsOriginInLineOfSight = true;
            if (!RequireOriginLineOfSight || !Camera)
                return true;

            var direction = (Origin - Camera.transform.position);
            if (Physics.Raycast(Camera.transform.position, direction, direction.magnitude, ~OriginIgnoreLayerMask, QueryTriggerInteraction.Ignore))
            {
                IsOriginInLineOfSight = false;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determine if the player is allowed to teleport to a destination.
        /// </summary>
        /// <param name="hitObject">The object hit by the raycast</param>
        /// <param name="destination">The position of the final ray cast hit</param>
        /// <param name="surfaceNormal">The normal of the ray cast hit, useful for ground angle validation</param>
        /// <returns></returns>
        protected virtual bool CheckValidDestination(GameObject hitObject, Vector3 destination, Vector3 surfaceNormal)
        {
            if (!CheckDestinationAllowed(hitObject, destination))
                return false;

            if (!CheckDestinationLineOfSight(destination))
                return false;

            if (!CheckPlayerFits(destination))
                return false;

            if (!CheckSurfaceAngle(surfaceNormal))
                return false;

            if (!CheckVerticalDistance(destination))
                return false;

            return true;
        }

        /// <summary>
        /// Determine if the player is allowed to teleport to a destination lower than the current position.
        /// </summary>
        protected virtual bool CheckFallDistance(Vector3 destination)
        {
            if (CapsuleBottom.y > destination.y && CapsuleBottom.y - destination.y > MaxDropDistance && CheckDropDistance)
            {
                IsDropDistanceValid = false;
                return false;
            }

            IsDropDistanceValid = true;
            return true;
        }

        /// <summary>
        /// Determine if the destination is in line of sight, by default the Camera is the origin of the ray cast.
        /// </summary>
        protected virtual bool CheckDestinationLineOfSight(Vector3 destination)
        {
            if (!RequireDestinationLineOfSight || !Camera)
            {
                IsDestinationInLineOfSight = true;
                return true;
            }

            for (int i = 0; i < DestinationIntervals; i++)
            {
                var offset = (float)i / DestinationIntervals * DestinationHeight;
                var direction = destination + new Vector3(0f, offset, 0f) - Camera.position;
                if (!Physics.Raycast(Camera.transform.position, direction, direction.magnitude, ~DestinationIgnoreLayerMask, QueryTriggerInteraction.Ignore))
                {
                    IsDestinationInLineOfSight = true;
                    return true;
                }
            }

            IsDestinationInLineOfSight = false;
            return false;
        }


        /// <summary>
        /// Determines if the player is allowed to land at the provided destination
        /// </summary>
        /// <param name="hitObject">The gameobject that the final ray cast hit.</param>
        protected virtual bool CheckDestinationAllowed(GameObject hitObject, Vector3 destination)
        {
            if (hitObject.TryGetComponent(out _dummyInvalid))
                return false;

            return (TeleportableLayers & (1 << hitObject.layer)) != 0;
        }


        /// <summary>
        /// Determines if the player is allowed to land at the provided destination
        /// </summary>
        protected virtual bool CheckPlayerFits(Vector3 destination)
        {
            DoesPlayerFit = CheckCapsuleFits(destination);
            return DoesPlayerFit;
        }

        /// <summary>
        /// Determines if the player is allowed to land at the provided destination using a capsule overlap check.
        /// </summary>
        protected virtual bool CheckCapsuleFits(Vector3 destination)
        {
            GetCapsuleData(out var height, out var radius, out var offset);
            var p1 = destination + Vector3.up * (height - radius + offset);
            var p2 = destination + Vector3.up * (radius + offset);

            var fits = Physics.OverlapCapsuleNonAlloc(p1, p2, radius, _dummy, ~0 & ~PlayerFitIgnoreLayerMask, QueryTriggerInteraction.Ignore) == 0;

            //if (!fits)
            //{
            //    Debug.DrawLine(destination, destination + Vector3.right * .3f, Color.magenta);
            //    Debug.DrawLine(p1, p2, Color.red);
            //    Debug.DrawLine(p1, p1 + Vector3.up * radius, Color.blue);
            //    Debug.DrawLine(p2, p2 - Vector3.up * radius, Color.cyan);
            //}

            return fits;
        }

        /// <summary>
        /// Determine if the player is allowed to reach a destination above the player's current position.
        /// </summary>
        protected virtual bool CheckVerticalDistance(Vector3 destination)
        {
            IsJumpDistanceValid = true;
            if (!CheckJumpDistance)
                return true;

            if (CapsuleBottom.y > destination.y)
                return true;

            if (destination.y - CapsuleBottom.y > MaxJumpDistance)
            {
                IsJumpDistanceValid = false;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determine if the surface angle is too steep using the raycast hit normal.
        /// </summary>
        protected virtual bool CheckSurfaceAngle(Vector3 normal)
        {
            var angle = Vector3.Angle(Vector3.up, normal);
            IsSurfaceAngleValid = angle <= MaxSurfaceAngle;
            return IsSurfaceAngleValid;
        }

        /// <summary>
        /// Gathers the height, radius, and collider width offset that will be used for the overlap fits check
        /// </summary>
        /// <param name="height">Height of the capsule</param>
        /// <param name="radius">Radius of the capsule</param>
        /// <param name="offset">Y offset (buffer) above the destination point to start the overlap check</param>
        protected virtual void GetCapsuleData(out float height, out float radius, out float offset)
        {
            if (Capsule)
            {
                height = Capsule.height;
                radius = Capsule.radius;
                offset = Capsule.contactOffset;
            }
            else
            {
                height = CharacterController.height;
                radius = CharacterController.radius;
                offset = CharacterController.skinWidth;
            }
        }

        protected virtual bool ProjectForwardRay(Vector3 origin, Vector3 target, out RaycastHit hit)
        {
            var layerMask = GetCollisionMask();
            var direction = target - origin;
            return Physics.Raycast(origin, direction, out hit, direction.magnitude, layerMask, LayerMaskTriggerInteraction);
        }

        protected virtual bool ProjectDownwardRay(Vector3 origin, out RaycastHit hit, float length)
        {
            return Physics.Raycast(origin, Vector3.down, out hit, length, ~0 & ~DownRayIgnoreMask, QueryTriggerInteraction.Ignore);
        }

        protected virtual LayerMask GetCollisionMask()
        {
            return LayerMask;
        }

        protected virtual Vector3 GetHorizontalPoint(Vector3 direction)
        {
            var forward = Vector3.RotateTowards(Vector3.down, Forward, Mathf.Deg2Rad * 90f, 0f);
            var angle = Vector3.Angle(forward, direction);
            var length = TeleportDistance / Mathf.Cos(angle * Mathf.Deg2Rad);

            var layerMask = GetCollisionMask();
            var ray = new Ray(Origin, direction);

            if (Physics.Raycast(ray, out var hit, length, layerMask, LayerMaskTriggerInteraction))
            {
                length = hit.distance;
            }

            var point = ray.GetPoint(length); // - CollisionBuffer);

            return point;
        }

        protected virtual Vector3 GetTeleportDirection(out float angle)
        {
            angle = Vector3.Angle(Vector3.down, Forward);

            if (angle > MaxAngle)
            {
                angle = MaxAngle;
                return Vector3.RotateTowards(Vector3.down, Forward, Mathf.Deg2Rad * MaxAngle, 0f);
            }

            return Forward;
        }

        protected virtual bool IsTeleportDeactivated()
        {
            return PlayerInputs.IsTeleportDeactivated;
        }

        protected virtual bool IsTeleportActivated()
        {
            return PlayerInputs.IsTeleportActivated;
        }


        protected virtual void OnValidTeleport()
        {
            if (Dash)
            {
                TeleportState = TeleportState.Dashing;
                OnBeforeDashTeleport();
            }
            else
            {
                TeleportState = TeleportState.Teleporting;
            }

            OnBeforeTeleport();
        }

        protected virtual void UpdateMarkerPosition()
        {
            if (!TeleportMarker)
                return;

            if (IsTeleportValid)
            {
                TeleportMarker.transform.position = TeleportDestination;
            }
            else
            {
                if (DownHitCollider)
                {
                    TeleportMarker.transform.position = LastDownwardPoint;
                }
                else
                {
                    TeleportMarker.transform.position = LastPoint;
                }
            }
        }

        protected virtual void UpdateLineRenderer(Vector3 hitPoint, int lastValidIndex, bool lineValid)
        {
            if (LineRenderer)
            {
                var count = lastValidIndex + 1;
                LineRenderer.positionCount = count;
                LineRenderer.SetPositions(LineRendererPoints);
            }
        }

        protected virtual void UpdateDownRenderer(Vector3 origin, Vector3 targetPoint, bool forwardLineValid)
        {
            if (DownRenderer)
            {
                var height = origin.y - targetPoint.y;
                DownRenderer.enabled = !forwardLineValid && targetPoint.y < origin.y && height > DownLineMinLength;

                if (DownRenderer.enabled)
                {
                    var endPoint = origin + Vector3.down * Mathf.Clamp(height, height, DownLineMaxLength);


                    DownRenderer.SetPosition(0, origin);
                    DownRenderer.SetPosition(1, endPoint);
                }
            }
        }


        protected virtual void UpdateTeleportMarker(bool isTeleportValid)
        {
            if (TeleportMarker)
            {
                var target = transform.position + 20f * Forward;
                target.y = TeleportMarker.transform.position.y;
                TeleportMarker.transform.LookAt(target);

                TeleportMarker.UpdateState(isTeleportValid);

                if (DisableMarkerWhenInvalid)
                {
                    if (isTeleportValid)
                    {
                        TeleportMarker.Activate();
                    }
                    else
                    {
                        TeleportMarker.Deactivate();
                    }
                }
            }
        }

        protected virtual void CalculateCurve(Vector3[] points)
        {
            if (CurveType == HVRTeleportCurve.Ballistic)
            {
                GenerateBallisticCurve(points);
            }
            else
            {
                GenerateBezierCurve(points);
            }
        }

        protected virtual void GenerateBezierCurve(Vector3[] points)
        {
            var direction = GetTeleportDirection(out var angle);
            var p2 = GetHorizontalPoint(direction);

            p2.y = Mathf.Clamp(p2.y, p2.y, Origin.y);

            var distance = Vector3.Distance(Origin, p2);
            var p1 = Origin + direction * (distance * .7f);
            var offsetFactor = angle / MaxAngle;
            var offset = BezierAngleOffsetCurve.Evaluate(offsetFactor) * BezierAngleOffset;
            p1.y += offset;

            HitPosition = p2;

            GenerateBezierCurve(Origin, p1, p2, points);
        }

        protected virtual void GenerateBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3[] points)
        {
            var resolution = 1f / points.Length;

            for (var i = 1; i <= Mathf.FloorToInt(1f / resolution); i++)
            {
                var t = i * resolution;
                var point = HVRUtilities.QuadraticBezier(p0, p1, p2, t);
                points[i - 1] = point;
            }

            points[points.Length - 1] = p2;
        }

        protected virtual void GenerateBallisticCurve(Vector3[] points)
        {
            var direction = GetTeleportDirection(out _);

            points[0] = Origin;

            var velocity = direction * Speed;

            for (var i = 1; i < LineSegments; i++)
            {
                velocity += Physics.gravity * TimeStep;
                points[i] = points[i - 1] + velocity * TimeStep;
            }
        }

        public virtual void OnValidTeleportChanged(bool isTeleportValid)
        {
            if (LineRenderer)
            {
                LineRenderer.material.SetColor("_TintColor", Color);
            }

            if (DownRenderer)
            {
                DownRenderer.material.SetColor("_TintColor", Color);
            }
        }


        protected virtual void UpdatePlayerPosition(Vector3 position)
        {
            CharacterController.transform.position = position;
        }

        protected virtual void OnBeforeDashTeleport()
        {
            var leftGrabbable = LeftHand.GrabbedTarget;
            var rightGrabbable = RightHand.GrabbedTarget;

            _leftVel = LeftHand.Rigidbody.velocity;
            _rightVel = RightHand.Rigidbody.velocity;

            _leftHeldVel = Vector3.zero;
            _rightHeldVel = Vector3.zero;

            var lrb = leftGrabbable ? leftGrabbable.Rigidbody : null;
            var rrb = rightGrabbable ? rightGrabbable.Rigidbody : null;

            if (lrb) _leftHeldVel = lrb.velocity;
            if (rrb) _rightHeldVel = rrb.velocity;
            
            //execute last, hand will disable itself in it's event handler
            BeforeDashTeleport.Invoke(FeetPosition);
        }

        protected virtual void OnAfterDashTeleport()
        {
            //execute first, hand will enable itself in it's event handler
            AfterDashTeleport.Invoke(FeetPosition);
            
            
            //restore velocity from before dash
            var leftGrabbable = LeftHand.GrabbedTarget;
            var rightGrabbable = RightHand.GrabbedTarget;
            
            var lrb = leftGrabbable ? leftGrabbable.Rigidbody : null;
            var rrb = rightGrabbable ? rightGrabbable.Rigidbody : null;

            LeftHand.Rigidbody.velocity = _leftVel;
            RightHand.Rigidbody.velocity = _rightVel;
            if (lrb) lrb.velocity = _leftHeldVel;
            if (rrb && rrb != lrb) rrb.velocity = _rightHeldVel;
        }

        protected virtual void OnBeforeTeleport()
        {
            IsTeleporting = true;
            BeforeTeleport.Invoke(FeetPosition);
            if (CharacterController)
                CharacterController.enabled = false;
        }

        protected virtual void OnAfterTeleport()
        {
            TeleportState = TeleportState.None;
            IsTeleporting = false;
            AfterTeleport.Invoke();
            if (CharacterController)
                CharacterController.enabled = true;

            if (LeftHand) LeftHand.BreakDistanceCooldown();
            if (RightHand) RightHand.BreakDistanceCooldown();
        }

        /// <summary>
        /// Instantly teleports the player to the desired position
        /// </summary>
        public virtual bool Teleport(Vector3 position)
        {
            if (IsTeleporting)
                return false;

            Teleport(position, Vector3.zero, false);
            return true;
        }

        /// <summary>
        /// Optionally rotate the player to the desired direction, and then instantly teleports the player to the desired position
        /// </summary>
        public virtual void Teleport(Vector3 position, Vector3 direction, bool faceDirection = true)
        {
            if (IsTeleporting)
                return;

            if (!Player) return;

            var leftRot = Quaternion.identity;
            var leftPos = Vector3.zero;
            var rightRot = Quaternion.identity;
            var rightPos = Vector3.zero;
            var leftGrabRot = Quaternion.identity;
            var leftGrabPos = Vector3.zero;
            var rightGrabRot = Quaternion.identity;
            var rightGrabPos = Vector3.zero;

            //snapshot relative values and then reapply after rotating the player

            var leftGrabbable = LeftHand.GrabbedTarget;
            var rightGrabbable = RightHand.GrabbedTarget;

            if (LeftHand)
            {
                LeftHand.GetRelativesTo(Player, out leftPos, out leftRot);
                if (leftGrabbable)
                {
                    leftGrabbable.MainTransform.GetRelativesTo(LeftHand, out leftGrabPos, out leftGrabRot);
                }
            }


            if (RightHand)
            {
                RightHand.GetRelativesTo(Player, out rightPos, out rightRot);
                if (rightGrabbable)
                {
                    rightGrabbable.MainTransform.GetRelativesTo(RightHand, out rightGrabPos, out rightGrabRot);
                }
            }

            var lv = LeftHand.Rigidbody.velocity;
            var rv = RightHand.Rigidbody.velocity;

            var leftObjV = Vector3.zero;
            var rightObjV = Vector3.zero;

            var lrb = leftGrabbable ? leftGrabbable.Rigidbody : null;
            var rrb = rightGrabbable ? rightGrabbable.Rigidbody : null;

            if (lrb) leftObjV = lrb.velocity;
            if (rrb) rightObjV = rrb.velocity;

            BeforeTeleport.Invoke(FeetPosition);

            var before = Player.transform.rotation;
            if (faceDirection)
                Player.FaceDirection(direction);
            var rot = Player.transform.rotation * Quaternion.Inverse(before);

            lv = rot * lv;
            rv = rot * rv;

            leftObjV = rot * leftObjV;
            rightObjV = rot * rightObjV;

            if (LeftHand)
            {
                LeftHand.transform.SetPositionAndRotation(Player.transform.TransformPoint(leftPos), Player.transform.rotation * leftRot);
                if (leftGrabbable)
                {
                    leftGrabbable.MainTransform.SetPositionAndRotation(LeftHand.transform.TransformPoint(leftGrabPos), LeftHand.transform.rotation * leftGrabRot);
                }
            }

            if (RightHand)
            {
                RightHand.transform.SetPositionAndRotation(Player.transform.TransformPoint(rightPos), Player.transform.rotation * rightRot);
                if (rightGrabbable)
                {
                    rightGrabbable.MainTransform.SetPositionAndRotation(RightHand.transform.TransformPoint(rightGrabPos), RightHand.transform.rotation * rightGrabRot);
                }
            }


            UpdatePlayerPosition(position);
            PositionUpdate.Invoke(FeetPosition);
            AfterTeleport.Invoke();

            LeftHand.Rigidbody.velocity = lv;
            RightHand.Rigidbody.velocity = rv;
            if (lrb) lrb.velocity = leftObjV;
            if (rrb && rrb != lrb) rrb.velocity = rightObjV;
        }

        protected virtual Vector3 FeetPosition => CharacterController.transform.position;

        protected virtual void UpdateDashTeleport()
        {
            if (Vector3.Distance(FeetPosition, TeleportDestination) > .01)
            {
                UpdatePlayerPosition(Vector3.MoveTowards(FeetPosition, TeleportDestination, DashSpeed * Time.deltaTime));
                PositionUpdate.Invoke(FeetPosition);
            }
            else
            {
                UpdatePlayerPosition(TeleportDestination);
                PositionUpdate.Invoke(FeetPosition);
                OnAfterTeleport();
                OnAfterDashTeleport();
            }
        }

        protected virtual void UpdateTeleport()
        {
            UpdatePlayerPosition(TeleportDestination);
            PositionUpdate.Invoke(FeetPosition);
        }

        protected virtual void RightHandReleased(HVRGrabberBase arg0, HVRGrabbable grabbable)
        {
            RightHandPrevents = false;
        }

        protected virtual void RightHandGrabbed(HVRGrabberBase arg0, HVRGrabbable grabbable)
        {
            if (grabbable.TryGetComponent<HVRTeleportOptions>(out var o))
            {
                if (o.BeforeTeleportOption == BeforeTeleportOptions.PreventsTeleport)
                {
                    RightHandPrevents = true;
                }
            }
        }

        protected virtual void LeftHandReleased(HVRGrabberBase arg0, HVRGrabbable grabbable)
        {
            LeftHandPrevents = false;
        }

        protected virtual void LeftHandGrabbed(HVRGrabberBase arg0, HVRGrabbable grabbable)
        {
            if (grabbable.TryGetComponent<HVRTeleportOptions>(out var o))
            {
                if (o.BeforeTeleportOption == BeforeTeleportOptions.PreventsTeleport)
                {
                    LeftHandPrevents = true;
                }
            }
        }
    }

    public enum HVRTeleportCurve
    {
        Ballistic,
        Bezier
    }

    public enum TeleportState
    {
        None,
        Teleporting,
        Dashing
    }

    [Serializable]
    public class TeleportPositionUpdate : UnityEvent<Vector3>
    {
    }
}