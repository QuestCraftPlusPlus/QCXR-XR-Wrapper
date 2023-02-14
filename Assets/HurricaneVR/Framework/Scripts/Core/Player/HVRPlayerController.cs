using System;
using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;
//using UnityEngine.SpatialTracking;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace HurricaneVR.Framework.Core.Player
{
    public class HVRPlayerController : MonoBehaviour
    {
        [Header("Settings")]
        public bool CanJump = false;

        public bool CanSteerWhileJumping = true;
        public bool CanSprint = true;
        public bool CanCrouch = true;
        public PlayerDirectionMode DirectionStyle = PlayerDirectionMode.Camera;

        [Tooltip("If true the player will ignore the first HMD movement on detection. " +
                 "If the HMD is not centered the player would move away from it's placed position to where the HMD is.")]
        public bool InitialHMDAdjustment = true;

        public LayerMask GroundedLayerMask;
        public float GroundedDistance = .02f;

        [Tooltip("Grounded sphere case radius factor multiplied by the capsules radius for ground checking.")]
        public float GroundedRadiusFactor = .5f;

        [Tooltip("Minimum Player Capsule Height.")]
        public float MinHeight = .3f;

        [Header("Locomotion")]
        public bool InstantAcceleration = true;

        [Tooltip("Walking speed in m/s.")]
        public float Acceleration = 15;

        public float Deacceleration = 15f;
        public float MoveSpeed = 1.5f;
        public float SprintAcceleration = 20f;

        [Tooltip("Sprinting speed in m/s.")]
        public float RunSpeed = 3.5f;

        public float Gravity = 9.81f;
        public float MaxFallSpeed = 2f;
        public float JumpVelocity = 5f;

        [Tooltip("Double click timeout for sprinting.")]
        public float DoubleClickThreshold = .25f;


        [Header("Turning")]
        public RotationType RotationType;

        public float SmoothTurnSpeed = 90f;
        public float SmoothTurnThreshold = .1f;
        public float SnapAmount = 45f;

        [Tooltip("Axis threshold to be considered valid for snap turning.")]
        public float SnapThreshold = .75f;

        [Tooltip("If true you can turn the player while aiming the teleporter, false by default")]
        public bool RotateWhileTeleportAiming;

        [Header("Crouching")]
        [Tooltip("Player height must be above this to toggle crouch.")]
        public float CrouchMinHeight = 1.2f;

        [Tooltip("Player height after toggling a crouch via controller.")]
        public float CrouchHeight = 0.7f;

        [Tooltip("Speed at which toggle crouch moves the player up and down.")]
        public float CrouchSpeed = 1.5f;

        [Header("Transforms")]
        public Transform Camera;

        public Transform NeckPivot;
        public Transform Root;
        public Transform FloorOffset;
        public Transform LeftControllerTransform;
        public Transform RightControllerTransform;

        [Header("Components")]
        public HVRCameraRig CameraRig;

        public HVRHandGrabber LeftHand;
        public HVRHandGrabber RightHand;
        public HVRJointHand LeftJointHand;
        public HVRJointHand RightJointHand;
        public HVRScreenFade ScreenFader;

        [Header("Head Collision")]
        public HVRHeadCollision HeadCollision;

        public float HeadCollisionFadeSpeed = 1f;

        [Tooltip("If true, when your head collides it returns your head to the body's position")]
        public bool HeadCollisionPushesBack = true;

        [Tooltip("If true, limits the head distance from the body by MaxLean amount.")]
        public bool LimitHeadDistance = true;

        [Tooltip("If LimitHeadDistance is true, the max distance your head can be from your body.")]
        public float MaxLean = .5f;

        [Tooltip("Screen fades when leaning to far into something.")]
        public bool FadeFromLean = true;


        [Header("Debugging")]
        public bool MouseTurning;

        public float MouseSensitivityX = 1f;


        public Rigidbody RigidBody { get; private set; }
        public CharacterController CharacterController { get; private set; }
        public HVRTeleporter Teleporter { get; private set; }

        public virtual float CameraHeight
        {
            get { return CameraRig.AdjustedCameraHeight; }
        }

        public bool IsCrouching => CameraHeight < CrouchMinHeight;

        public bool IsClimbing => LeftHand && LeftHand.IsClimbing || RightHand && RightHand.IsClimbing;

        public bool Sprinting { get; set; }

        public bool IsGrounded; // { get; set; }

        public Vector3 GroundNormal { get; set; }

        public bool MovementEnabled { get; set; } = true;
        public bool RotationEnabled { get; set; } = true;

        public Transform Neck { get; private set; }

        public HVRPlayerInputs Inputs { get; private set; }

        public Vector3 PreviousPosition { get;  set; }
        
        private Vector3 _previousLeftControllerPosition;
        private Vector3 _previousRightControllerPosition;

        private bool _waitingForCameraMovement;
        private Vector3 _cameraStartingPosition;


        private float _timeSinceLastPress;
        private bool _awaitingSecondClick;

        private bool _crouchInProgress;
        private bool _cameraBelowCrouchHeight;
        private Coroutine _crouchRoutine;
        private float _previousTurnAxis;
        private float _crouchOffset;
        private bool _isCrouchingToggled;
        private bool _isCameraCorrecting;
        private bool _hasTeleporter;
        private Vector3 _previousVelocity;
        private float yVelocity;
        private Vector3 xzVelocity;
        private Quaternion _preTeleportRotation;

        [SerializeField]
        private float _actualVelocity;

        protected virtual void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
            CharacterController = GetComponent<CharacterController>();
            Teleporter = GetComponent<HVRTeleporter>();
            if (Teleporter)
            {
                _hasTeleporter = true;
                Teleporter.BeforeTeleport.AddListener(OnBeforeTeleport);
                Teleporter.PositionUpdate.AddListener(OnTeleported);
            }

            if (NeckPivot)
                Neck = NeckPivot;
            else
                Neck = Camera;

            _cameraStartingPosition = Camera.localPosition;

            Inputs = GetComponent<HVRPlayerInputs>();

            if (!ScreenFader)
            {
                var finder = FindObjectOfType<HVRGlobalFadeFinder>();
                if (finder)
                {
                    ScreenFader = finder.gameObject.GetComponent<HVRScreenFade>();
                }
            }

            PreviousPosition = transform.position;
        }

        private void OnTeleported(Vector3 arg0)
        {
            //prevent teleporting from impulsing the hand calcs in AdjustHandAcceleration
            PreviousPosition = transform.position;
            var rot = transform.rotation * Quaternion.Inverse(_preTeleportRotation);
            _previousVelocity = rot * _previousVelocity;
        }

        private void OnBeforeTeleport(Vector3 arg0)
        {
            _preTeleportRotation = transform.rotation;
        }


        private IEnumerator CorrectCamera()
        {
            _isCameraCorrecting = true;

            var delta = transform.position - Neck.position;
            delta.y = 0f;

            if (!ScreenFader)
            {
                CameraRig.transform.position += delta;
                _isCameraCorrecting = false;
                yield break;
            }

            ScreenFader.Fade(1, HeadCollisionFadeSpeed);

            while (ScreenFader.CurrentFade < .9)
            {
                yield return null;
            }

            delta = transform.position - Neck.position;
            delta.y = 0f;
            CameraRig.transform.position += delta;

            ScreenFader.Fade(0, HeadCollisionFadeSpeed);

            while (ScreenFader.CurrentFade > .1)
            {
                yield return null;
            }

            _isCameraCorrecting = false;
        }


        protected virtual void Start()
        {
            Reset();
        }


        public virtual void Reset()
        {
            _waitingForCameraMovement = InitialHMDAdjustment;
        }

        protected virtual void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Q))
            // {
            //     Teleporter.Teleport(transform.position, Quaternion.AngleAxis(90f, Vector3.up) * transform.forward);
            // }
            CheckCameraCorrection();
            CheckSprinting();
            UpdateHeight();
            CheckCrouching();
            CameraRig.PlayerControllerYOffset = _crouchOffset;
        }

        private void CheckCameraCorrection()
        {
            if (HeadCollisionPushesBack && HeadCollision && HeadCollision.IsColliding && !_isCameraCorrecting)
            {
                StartCoroutine(CorrectCamera());
            }
        }

        protected virtual void FixedUpdate()
        {
            if (_waitingForCameraMovement)
                CheckCameraMovement();

            if (CharacterController.enabled)
            {
               
                HandleMovement(); 

                if (CanRotate())
                {
                    HandleRotation();
                }
            }

            CheckLean();
            CheckGrounded();


            _actualVelocity = ((transform.position - PreviousPosition) / Time.deltaTime).magnitude;

            _previousLeftControllerPosition = LeftControllerTransform.position;
            _previousRightControllerPosition = RightControllerTransform.position;

            PreviousPosition = transform.position;
        }

        protected virtual bool CanRotate()
        {
            if (!RotationEnabled)
                return false;

            if (_hasTeleporter && Teleporter.IsAiming && !RotateWhileTeleportAiming)
            {
                return false;
            }

            return true;
        }

        protected virtual void CheckCameraMovement()
        {
            if (Vector3.Distance(_cameraStartingPosition, Camera.transform.localPosition) < .05f)
            {
                return;
            }

            var delta = Camera.transform.position - CharacterController.transform.position;
            delta.y = 0f;
            CameraRig.transform.position -= delta;
            _waitingForCameraMovement = false;
            PreviousPosition = transform.position;
        }

        protected virtual void CheckGrounded()
        {
            var radius = CharacterController.radius * GroundedRadiusFactor;
            var origin = CharacterController.center - Vector3.up * (.5f * CharacterController.height - radius);
            IsGrounded = Physics.SphereCast(
                transform.TransformPoint(origin) + Vector3.up * CharacterController.contactOffset,
                radius,
                Vector3.down,
                out var hit,
                GroundedDistance + CharacterController.contactOffset,
                GroundedLayerMask, QueryTriggerInteraction.Ignore);

            GroundNormal = hit.normal;
        }

        protected virtual void CheckLean()
        {
            if (_isCameraCorrecting || !LimitHeadDistance)
                return;

            var delta = Neck.transform.position - CharacterController.transform.position;
            delta.y = 0;

            if (delta.sqrMagnitude < .01f || delta.magnitude < MaxLean) return;

            if (FadeFromLean)
            {
                StartCoroutine(CorrectCamera());
                return;
            }

            var allowedPosition = CharacterController.transform.position + delta.normalized * MaxLean;
            var difference = allowedPosition - Neck.transform.position;
            difference.y = 0f;
            CameraRig.transform.position += difference;
        }

        protected virtual void UpdateHeight()
        {
            CharacterController.height = Mathf.Clamp(CameraRig.AdjustedCameraHeight, MinHeight, CameraRig.AdjustedCameraHeight);
            CharacterController.center = new Vector3(0, CharacterController.height * .5f + CharacterController.skinWidth, 0f);
        }

        protected virtual void HandleHMDMovement()
        {
            var originalCameraPosition = CameraRig.transform.position;
            var originalCameraRotation = CameraRig.transform.rotation;

            var delta = Neck.transform.position - CharacterController.transform.position;
            delta.y = 0f;
            if (delta.magnitude > 0.0f && CharacterController.enabled)
            {
                delta = Vector3.ProjectOnPlane(delta, GroundNormal);
                CharacterController.Move(delta);
            }

            transform.rotation = Quaternion.Euler(0.0f, Neck.rotation.eulerAngles.y, 0.0f);

            CameraRig.transform.position = originalCameraPosition;
            var local = CameraRig.transform.localPosition;
            local.y = 0f;
            CameraRig.transform.localPosition = local;
            CameraRig.transform.rotation = originalCameraRotation;
        }

        protected virtual void HandleRotation()
        {
            if (RotationType == RotationType.Smooth)
            {
                HandleSmoothRotation();
            }
            else if (RotationType == RotationType.Snap)
            {
                HandleSnapRotation();
            }

            HandlMouseRotation();

            _previousTurnAxis = GetTurnAxis().x;
        }

        protected virtual void HandlMouseRotation()
        {
            if (MouseTurning)
            {
                if (Inputs.IsMouseDown)
                {
                    var offset = Quaternion.Euler(new Vector3(0, Inputs.MouseAxis.x * MouseSensitivityX, 0));
                    transform.rotation *= offset;

                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }

        protected virtual void HandleSnapRotation()
        {
            var input = GetTurnAxis().x;
            if (Math.Abs(input) < SnapThreshold || Mathf.Abs(_previousTurnAxis) > SnapThreshold)
                return;

            var rotation = Quaternion.Euler(0, Mathf.Sign(input) * SnapAmount, 0);
            transform.rotation *= rotation;
        }

        protected virtual void HandleSmoothRotation()
        {
            var input = GetTurnAxis().x;
            if (Math.Abs(input) < SmoothTurnThreshold)
                return;

            var rotation = input * SmoothTurnSpeed * Time.deltaTime;
            var rotationVector = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + rotation, transform.eulerAngles.z);
            transform.rotation = Quaternion.Euler(rotationVector);
        }


        protected virtual void HandleMovement()
        {
            if (IsClimbing)
            {
                HandleClimbing();
                return;
            }

            if (!_waitingForCameraMovement)
            {
                HandleHMDMovement();
            }

            HandleHorizontalMovement();
            HandleVerticalMovement();
            AdjustHandAcceleration();
        }

        protected virtual void GetMovementDirection(out Vector3 forwards, out Vector3 right)
        {
            var t = transform;

            switch (DirectionStyle)
            {
                case PlayerDirectionMode.Camera:
                    if (Camera)
                        t = Camera;
                    break;
                case PlayerDirectionMode.LeftController:
                    if (LeftControllerTransform)
                        t = LeftControllerTransform;
                    break;
                case PlayerDirectionMode.RightController:
                    if (RightControllerTransform)
                        t = RightControllerTransform;
                    break;
            }

            forwards = t.forward;
            right = t.right;
            forwards.y = 0;
            forwards.Normalize();
            right.y = 0;
            right.Normalize();
        }

        protected virtual void HandleVerticalMovement()
        {
            Vector3 velocity = xzVelocity;

            if (IsGrounded)
            {
                if (Inputs.IsJumpActivated && CanJump && MovementEnabled)
                {
                    yVelocity = JumpVelocity;
                }
                else
                {
                    yVelocity += -Gravity * Time.deltaTime;
                }

                yVelocity = Mathf.Clamp(yVelocity, -Gravity * Time.deltaTime, yVelocity);
            }
            else
            {
                yVelocity += -Gravity * Time.deltaTime;
                yVelocity = Mathf.Clamp(yVelocity, -MaxFallSpeed, yVelocity);
            }

            velocity.y += yVelocity;

            CharacterController.Move(velocity * Time.deltaTime);
        }

        protected virtual void HandleHorizontalMovement()
        {
            var speed = MoveSpeed;
            var runSpeed = RunSpeed;

            if (Sprinting)
                speed = runSpeed;

            var movement = GetMovementAxis();

            if (!MovementEnabled)
            {
                movement = Vector2.zero;
            }

            GetMovementDirection(out var forward, out var right);
            var direction = (forward * movement.y + right * movement.x);

            if (IsGrounded || CanSteerWhileJumping)
            {
                if (IsGrounded)
                {
                    direction = Vector3.ProjectOnPlane(direction, GroundNormal);
                }

                if (InstantAcceleration)
                {
                    xzVelocity = speed * direction;
                }
                else
                {
                    var noMovement = Mathf.Abs(movement.x) < .1f && Mathf.Abs(movement.y) < .1f;
                    if (noMovement)
                    {
                        var dir = xzVelocity.normalized;
                        var deacceleration = Deacceleration * Time.deltaTime;
                        if (deacceleration > xzVelocity.magnitude)
                        {
                            xzVelocity = Vector3.zero;
                        }
                        else
                        {
                            xzVelocity -= dir * deacceleration;
                        }
                    }
                    else
                    {
                        var acceleration = (Sprinting ? SprintAcceleration : Acceleration) * Time.deltaTime;
                        xzVelocity += acceleration * direction;
                        xzVelocity = Vector3.ClampMagnitude(xzVelocity, speed);
                    }
                }
            }
        }

        /// <summary>
        /// Accounts for the instantaneous acceleration of the player, without this the hands would lag behind
        /// </summary>
        protected virtual void AdjustHandAcceleration()
        {
            var v = (transform.position - PreviousPosition) / Time.deltaTime;
            var acceler = (v - _previousVelocity) / Time.deltaTime;
            _previousVelocity = v;
            
            LeftJointHand.RigidBody.AddForce(acceler * LeftJointHand.RigidBody.mass, ForceMode.Force);
            RightJointHand.RigidBody.AddForce(acceler * RightJointHand.RigidBody.mass, ForceMode.Force);

            Rigidbody leftRB = null;
            Rigidbody rightRb = null;

            if (LeftHand.GrabbedTarget && LeftHand.GrabbedTarget.Rigidbody)
            {
                leftRB = LeftHand.GrabbedTarget.Rigidbody;
            }

            if (RightHand.GrabbedTarget && RightHand.GrabbedTarget.Rigidbody)
            {
                rightRb = RightHand.GrabbedTarget.Rigidbody;
            }

            if (leftRB && rightRb && leftRB == rightRb)
            {
                LeftJointHand.RigidBody.AddForce(acceler * (.5f * leftRB.mass), ForceMode.Force);
                RightJointHand.RigidBody.AddForce(acceler * (.5f * rightRb.mass), ForceMode.Force);
            }
            else
            {
                if (leftRB)
                {
                    LeftJointHand.RigidBody.AddForce(acceler * leftRB.mass, ForceMode.Force);
                }

                if (rightRb)
                {
                    RightJointHand.RigidBody.AddForce(acceler * rightRb.mass, ForceMode.Force);
                }
            }
        }

        protected virtual void HandleClimbing()
        {
            var left = Vector3.zero;
            var right = Vector3.zero;

            if (LeftHand && LeftHand.IsClimbing)
            {
                left = (_previousLeftControllerPosition - LeftControllerTransform.position);
            }

            if (RightHand && RightHand.IsClimbing)
            {
                right = (_previousRightControllerPosition - RightControllerTransform.position);
            }

            var max = Mathf.Max(left.magnitude, right.magnitude);
            var move = left + right;

            move = Vector3.ClampMagnitude(move, max);

            CharacterController.Move(move);
        }


        protected virtual Vector2 GetMovementAxis()
        {
            return Inputs.MovementAxis;
        }

        protected virtual Vector2 GetTurnAxis()
        {
            return Inputs.TurnAxis;
        }

        protected virtual void CheckSprinting()
        {
            if (!CanSprint)
                return;

            if (Inputs.SprintRequiresDoubleClick)
            {
                if (_awaitingSecondClick)
                {
                    _timeSinceLastPress += Time.deltaTime;
                }

                if (!Sprinting && Inputs.IsSprintingActivated)
                {
                    if (_timeSinceLastPress < DoubleClickThreshold && _awaitingSecondClick)
                    {
                        Sprinting = true;
                        _awaitingSecondClick = false;
                    }
                    else
                    {
                        _timeSinceLastPress = 0f;
                        _awaitingSecondClick = true;
                    }
                }
            }
            else
            {
                if (Sprinting && Inputs.IsSprintingActivated)
                    Sprinting = false;
                else if (!Sprinting && Inputs.IsSprintingActivated)
                    Sprinting = true;
            }

            if (GetMovementAxis().magnitude < .01f)
            {
                Sprinting = false;
            }
        }

        protected virtual void CheckCrouching()
        {
            if (!CanCrouch)
                return;

            if (!_crouchInProgress && CameraHeight >= CrouchMinHeight)
            {
                if (Inputs.IsCrouchActivated)
                {
                    Crouch();
                }
                else if (_isCrouchingToggled)
                {
                    StopCrouching();
                }
            }
            else if (_isCrouchingToggled && Inputs.IsCrouchActivated)
            {
                StopCrouching();
            }

            if (IsCrouching && _isCrouchingToggled)
            {
                if (_cameraBelowCrouchHeight && CameraHeight > CrouchHeight)
                {
                    StopCrouching();
                }
                else if (CameraHeight < (CrouchHeight - MinHeight) / 2f)
                {
                    _cameraBelowCrouchHeight = true;
                }
            }
        }

        protected virtual void Crouch()
        {
            _isCrouchingToggled = true;
            var target = CrouchHeight - CameraHeight;
            _cameraBelowCrouchHeight = false;
            if (_crouchRoutine != null)
                StopCoroutine(_crouchRoutine);
            _crouchRoutine = StartCoroutine(CrouchRoutine(target, true));
        }

        protected virtual void StopCrouching()
        {
            _isCrouchingToggled = false;
            _cameraBelowCrouchHeight = false;
            if (_crouchRoutine != null)
                StopCoroutine(_crouchRoutine);
            _crouchRoutine = StartCoroutine(CrouchRoutine(0f, false));
        }

        private IEnumerator CrouchRoutine(float target, bool crouching)
        {
            _crouchInProgress = true;

            var total = 0f;

            float delta;
            float min;
            float max;
            float sign;
            if (crouching)
            {
                delta = _crouchOffset - target;
                sign = -1;
                min = target;
                max = 0f;
            }
            else
            {
                delta = 0 - _crouchOffset;
                sign = 1;
                min = _crouchOffset;
                max = 0f;
            }

            while (total < delta)
            {
                _crouchOffset += sign * Time.deltaTime * CrouchSpeed;
                total += Time.deltaTime * CrouchSpeed;

                _crouchOffset = Mathf.Clamp(_crouchOffset, min, max);

                yield return new WaitForEndOfFrame();
            }

            _crouchInProgress = false;
        }

        public virtual void IgnoreCollision(IEnumerable<Collider> colliders)
        {
            foreach (var otherCollider in colliders)
            {
                if (otherCollider && CharacterController)
                    Physics.IgnoreCollision(CharacterController, otherCollider, true);
            }
        }

        /// <summary>
        /// Removes components not necessary on other players rigs
        /// </summary>
        public void RemoveMultiplayerComponents()
        {
            foreach (var t in new[]
                     {
                         typeof(HVRCamera),
                         typeof(Camera),
                         typeof(AudioListener),
                         //typeof(TrackedPoseDriver),
                         typeof(HVRScreenFade),
                         typeof(HVRHeadCollision),
                         typeof(HVRThrowingCenterOfMass),
                         typeof(HVRControllerOffset), typeof(HVRJointHand)
                     })
            {
                foreach (var component in GetComponentsInChildren(t))
                {
                    Destroy(component);
                }
            }
        }

        public void SetTurnType(RotationType rotationType)
        {
            RotationType = rotationType;
        }

        public void SetSnapAmount(float snapAmount)
        {
            SnapAmount = snapAmount;
        }

        public void SetSmoothTurnSpeed(float speed)
        {
            SmoothTurnSpeed = speed;
        }

        public void FaceDirection(Vector3 forward)
        {
            var f = Camera.forward;
            f.y = 0f;
            forward.y = 0f;
            var rotation = Quaternion.AngleAxis(Vector3.SignedAngle(f, forward, Vector3.up), Vector3.up);
            transform.rotation = rotation * transform.rotation;
        }
    }

    public enum PlayerDirectionMode
    {
        Camera,
        LeftController,
        RightController
    }

    public enum RotationType
    {
        Smooth,
        Snap
    }
}