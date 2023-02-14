using System.Collections;
using Assets.HurricaneVR.Framework.Shared.Utilities;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace HurricaneVR.Framework.Core.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class HVRJointHand : MonoBehaviour
    {
        /// <summary>
        /// Default hand strength settings.
        /// </summary>
        [Header("Settings")]

        [Embedded]
        [Tooltip("Default hand strength settings.")]
        public PDStrength Strength;
        
        [Tooltip("Physx constraint iterations")]
        public int SolverIterations = 10;

        [Tooltip("Physx constraint iterations")]
        public int SolverVelocityIterations = 10;
        
    

   

        /// <summary>
        /// The speed at which the hand returns to the controller when the MaxDistanceBehaviour is not set to HandSweep
        /// </summary>
        [Tooltip("Speed at which the hand returns to the controller when the max limit is breached, does not work in HandSweep mode.")]
        public float ReturnSpeed = 5f;


        /// <summary>
        /// The max distance the hand is allowed to stray from it's target.
        /// </summary>
        [FormerlySerializedAs("MaxDistance")]
        [Tooltip("Max distance between the hand and the controller target.")]
        public float MaxTargetDistance = .8f;

        /// <summary>
        /// Defines the action taken after max distance from the controller is exceeeded.
        /// </summary>
        [Tooltip("Behaviour when arm length is exceeded, or distance from the hand to the controller is exceeded.")]
        public MaxDistanceBehaviour MaxDistanceBehaviour = MaxDistanceBehaviour.HandSweep;

        /// <summary>
        /// Optional anchor point to test distance to the hand, could the shoulder of an arm or full body avatar. Works in tandem with the ArmLength field to limit the distance of the hand.
        /// </summary>
        [Header("Arm Limit")]
        [Tooltip("Optional 'Shoulder', if populated the hand and it's object will snap back based on the MaxDistanceBehaviour assigned.")]
        public Transform Anchor;

        /// <summary>
        /// Max distance allowed from the Anchor point if supplied.
        /// </summary>
        [Tooltip("Max allowed length from the Anchor to the hand.")]
        public float ArmLength = .75f;

        /// <summary>
        /// This hand will chase after the supplied transform, usually set to the offset child of the tracked device transform.
        /// </summary>
        [Header("Components")]
        [Tooltip("Target transform for position and rotation tracking")]
        public Transform Target;

        [Header("Deprecated")]
        [Tooltip("Default hand strength settings.")]
        public HVRJointSettings JointSettings;
        
        /// <summary>
        /// Kinematic rigidbody that will hold the joint for stability.
        /// </summary>
        public Rigidbody ParentRigidBody;

        /// <summary>
        /// This component manages the strength overrides that come from grabbable override fields.
        /// </summary>
        public HVRHandStrengthHandler StrengthHandler;

        /// <summary>
        /// Referenced to keep track of a recent teleport to prevent the Max Distance behaviour from executing after a teleport.
        /// </summary>
        public HVRTeleporter Teleporter;

        /// <summary>
        /// Invoked once the max distance is reached from the controller target.
        /// </summary>
        [Header("Events")]
        public UnityEvent MaxDistanceReached = new UnityEvent();

        /// <summary>
        /// If the max distance behaviour in effect is not a HandSweep, this will invoke after the hand has returned to the controller.
        /// </summary>
        public UnityEvent ReturnedToController = new UnityEvent();

        [Header("Debug")]
        public bool IsReturningToController;


        public Rigidbody RigidBody { get; private set; }

        public ConfigurableJoint Joint { get; set; }

        public HVRHandGrabber Grabber { get; private set; }


        public Vector3 PreviousControllerPosition { get; set; }
        private Quaternion _previousRotation;
        private float _teleportTimer = 0f;

        protected virtual void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
            //this joint needs to be created before any offsets are applied to the controller target
            //due to how joints snapshot their initial rotations on creation
            SetupJoint();

            RigidBody.maxAngularVelocity = 150f;
            RigidBody.solverIterations = SolverIterations;
            RigidBody.solverVelocityIterations = SolverVelocityIterations;

            Grabber = GetComponent<HVRHandGrabber>();

            if (!Teleporter && transform.root)
            {
                Teleporter = transform.root.GetComponentInChildren<HVRTeleporter>();
            }

            if (Teleporter)
            {
                Teleporter.PositionUpdate.AddListener(OnPlayerTeleported);
                Teleporter.BeforeDashTeleport.AddListener(OnBeforeDashTeleport);
                Teleporter.AfterDashTeleport.AddListener(OnAfterDashTeleport);
            }

            if (!Strength)
                Debug.LogError("Strength field is empty, must be populated with PDStrength scriptable object.");

            if (!StrengthHandler)
            {
                if (!TryGetComponent(out StrengthHandler))
                {
                    StrengthHandler = gameObject.AddComponent<HVRHandStrengthHandler>();
                }
            }

            if (ReturnSpeed < .1f)
                ReturnSpeed = 5f;

            StrengthHandler.Joint = Joint;
            if(JointSettings) StrengthHandler.Initialize(JointSettings);
            else StrengthHandler.Initialize(Strength);
        }

        private void OnAfterDashTeleport(Vector3 arg0)
        {
            Enable();
        }

        private void OnBeforeDashTeleport(Vector3 arg0)
        {
            Disable();
        }

        protected virtual void Start()
        {
            PreviousControllerPosition = Target.position;
            _previousRotation = Target.rotation;
            //fixing the issue where the hand goes to world 0,0,0 at start
            StartCoroutine(StopHandsRoutine());
        }

        protected virtual IEnumerator StopHandsRoutine()
        {
            var count = 0;
            while (count < 100)
            {
                yield return new WaitForFixedUpdate();
                RigidBody.velocity = Vector3.zero;
                RigidBody.angularVelocity = Vector3.zero;
                transform.position = Target.position;
                count++;
            }
        }

        public void Disable()
        {
            RigidBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            RigidBody.isKinematic = true;
        }

        public void Enable()
        {
            RigidBody.isKinematic = false;
            RigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }

        protected virtual void OnEnable()
        {
            Joint.targetRotation = Quaternion.Inverse(Quaternion.Inverse(ParentRigidBody.transform.rotation) * transform.rotation);
        }

        public virtual void SetupJoint()
        {
            //Debug.Log($"{name} joint created.");
            //this joint needs to be created before any offsets are applied to the controller target
            //due to how joints snapshot their initial rotations on creation
            Joint = ParentRigidBody.transform.gameObject.AddComponent<ConfigurableJoint>();
            Joint.autoConfigureConnectedAnchor = false;
            Joint.connectedBody = RigidBody;
            Joint.connectedAnchor = Vector3.zero;
            Joint.anchor = ParentRigidBody.transform.InverseTransformPoint(Target.position);
            Joint.enableCollision = false;
            Joint.enablePreprocessing = false;
            Joint.rotationDriveMode = RotationDriveMode.Slerp;
        }

        protected virtual void FixedUpdate()
        {
            UpdateTargetVelocity();
            UpdateDistanceCheck();

            if (Anchor)
            {
                if (Vector3.Distance(Anchor.position, Target.position) > ArmLength)
                {
                    var localTargetPosition = ParentRigidBody.transform.InverseTransformPoint(Target.position);
                    var localAnchor = ParentRigidBody.transform.InverseTransformPoint(Anchor.position);
                    var dir = localTargetPosition - localAnchor;
                    dir = Vector3.ClampMagnitude(dir, ArmLength);
                    var point = localAnchor + dir;

                    Joint.targetPosition = point;
                }
                else
                {
                    Joint.targetPosition = Vector3.zero;
                }
            }
        }


        public virtual void OnPlayerTeleported(Vector3 position)
        {
            _teleportTimer = Time.time;
            SaveTargetState(); //updating state after teleport
        }

        protected virtual void UpdateDistanceCheck()
        {
            if (Time.time - _teleportTimer < .5f)
                return;

            if (!IsReturningToController)
            {
                var reached = false;
                if (Anchor)
                    reached = Vector3.Distance(Anchor.position, transform.position) > ArmLength;
                if (!reached)
                    reached = Vector3.Distance(transform.position, Target.position) > MaxTargetDistance;

                if (reached)
                {
                    OnMaxDistanceReached();
                }
            }
            else if (IsReturningToController)
            {
                transform.position = Vector3.MoveTowards(transform.position, Target.position, ReturnSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, Target.position) < .05f)
                {
                    IsReturningToController = false;
                    OnReturned();
                }
            }
        }

        protected virtual void OnReturned()
        {
            RigidBody.detectCollisions = true;
            //sweep in case controller is inside something
            if (Grabber.CollisionHandler && MaxDistanceBehaviour != MaxDistanceBehaviour.HandSweep)
                Grabber.CollisionHandler.Sweep(Grabber, Target.position);
            ReturnedToController.Invoke();
        }

        protected MaxDistanceBehaviour GetBehaviour()
        {
            if (Grabber.GrabbedTarget && Grabber.GrabbedTarget.OverrideMaxDistanceBehaviour)
                return Grabber.GrabbedTarget.MaxDistanceBehaviour;
            return MaxDistanceBehaviour;
        }

        protected virtual void OnMaxDistanceReached()
        {
            var behaviour = GetBehaviour();

            if (behaviour == MaxDistanceBehaviour.GrabbablePrevents && Grabber.GrabbedTarget)
                return;

            if (behaviour == MaxDistanceBehaviour.GrabbableDrops && Grabber.GrabbedTarget)
                Grabber.ForceRelease();

            if (behaviour == MaxDistanceBehaviour.HandSweep && Grabber.CollisionHandler)
            {
                this.ExecuteAfterFixedUpdate(() => Grabber.CollisionHandler.Sweep(Grabber, Target.position));
                return;
            }

            IsReturningToController = true;
            MaxDistanceReached.Invoke();
            RigidBody.detectCollisions = false;
        }

        public virtual void UpdateTargetVelocity()
        {
            if (!(Vector3.SqrMagnitude(Target.position - PreviousControllerPosition) > 1f))
            {
                var worldVelocity = (Target.position - PreviousControllerPosition) / Time.fixedDeltaTime;
                Joint.targetVelocity = ParentRigidBody.transform.InverseTransformDirection(worldVelocity);
            }

            var angularVelocity = Target.rotation.AngularVelocity(_previousRotation);
            Joint.targetAngularVelocity = Quaternion.Inverse(ParentRigidBody.transform.rotation) * angularVelocity;

            SaveTargetState();
        }

        /// <summary>
        /// Saves the controller position and rotation, call this if you manually move the player outside of the framework teleporter code.
        /// </summary>
        public void SaveTargetState()
        {
            PreviousControllerPosition = Target.position;
            _previousRotation = Target.rotation;
        }

        private void OnDrawGizmos()
        {
            if (RigidBody)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(RigidBody.worldCenterOfMass, .017f);
            }
        }
    }

    public enum MaxDistanceBehaviour
    {
        GrabbablePrevents,
        GrabbableDrops,
        HandSweep
    }
}