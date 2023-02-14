using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.Serialization;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Simple component to help setup a joint constrained on one axis. With bonus rotation friction option when held.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class HVRPhysicsDial : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("Local axis of rotation")]
        public HVRAxis Axis;

        [Tooltip("Rigidbody to connect the joint to")]
        public Rigidbody ConnectedBody;

        [Tooltip("If true the angular velocity will be zero'd out on release.")]
        public bool StopOnRelease = true;

        [Tooltip("Defaults to true to prevent rotation when not held")]
        public bool DisableGravity = true;

        [Header("Joint Limits")]
        public bool LimitRotation;

        [Tooltip("Minimum Angle about the axis of rotation")]
        public float MinAngle;

        [Tooltip("Maximum rotation about the axis of rotation")]
        public float MaxAngle;

        [Header("Joint Settings")]
        [Tooltip("Angular Damper when the dial is grabbed")]
        public float GrabbedDamper = 3;

        [Tooltip("Angular Damper when the dial is not grabbed")]
        public float Damper = 3;

        [Tooltip("Optional spring value of the joint to return to starting rotation if desired")]
        public float Spring;

        [Header("Editor")]
        [SerializeField]
        protected Quaternion JointStartRotation;

        [Header("Debugging Tools")]
        public float TargetAngularVelocity = 0f;

        public Rigidbody Rigidbody { get; private set; }

        public HVRGrabbable Grabbable { get; private set; }

        public ConfigurableJoint Joint { get; set; }

        protected virtual void Awake()
        {
            Rigidbody = this.GetRigidbody();

            Rigidbody.useGravity = !DisableGravity;

            Grabbable = GetComponent<HVRGrabbable>();
            if (Grabbable)
            {
                Grabbable.HandGrabbed.AddListener(OnDialGrabbed);
                Grabbable.HandReleased.AddListener(OnDialReleased);
            }

            FixAngle(ref MinAngle, ref MaxAngle);

            SetupJoint();
            AfterJointCreated(Joint);
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            if (TargetAngularVelocity > 0f || TargetAngularVelocity < 0f) Joint.targetAngularVelocity = new Vector3(TargetAngularVelocity, 0f, 0f);
        }

        protected virtual void OnDialReleased(HVRHandGrabber arg0, HVRGrabbable arg1)
        {
            Joint.SetAngularXDrive(Spring, Damper, 10000f);
            if (StopOnRelease)
            {
                Rigidbody.angularVelocity = Vector3.zero;
            }
        }

        protected virtual void OnDialGrabbed(HVRHandGrabber arg0, HVRGrabbable arg1)
        {
            Joint.SetAngularXDrive(0f, GrabbedDamper, 10000f);
        }

        protected virtual void SetupJoint()
        {
            var currentRotation = transform.localRotation;
            transform.localRotation = JointStartRotation;
            Joint = gameObject.AddComponent<ConfigurableJoint>();
            Joint.connectedBody = ConnectedBody;
            Joint.LockLinearMotion();
            Joint.LockAngularYMotion();
            Joint.LockAngularZMotion();
            Joint.anchor = Vector3.zero;
            Joint.axis = Axis.GetVector();

            if (LimitRotation)
            {
                ResetLimits();
            }
            else
            {
                Joint.angularXMotion = ConfigurableJointMotion.Free;
            }

            Joint.secondaryAxis = Joint.axis.OrthogonalVector();
            Joint.SetAngularXDrive(Spring, Damper, 10000f);
            Joint.projectionAngle = 1f;
            Joint.projectionDistance = .01f;
            Joint.projectionMode = JointProjectionMode.PositionAndRotation;
            transform.localRotation = currentRotation;
        }

        protected virtual void AfterJointCreated(ConfigurableJoint joint)
        {

        }

        /// <summary>
        /// Sets the rotation limits on the Joint's main rotation axis
        /// </summary>
        public void SetLimits(float minAngle, float maxAngle)
        {
            FixAngle(ref minAngle, ref maxAngle);

            Joint.LimitAngularXMotion();
            Joint.SetAngularXHighLimit(minAngle);
            Joint.SetAngularXLowLimit(maxAngle);
        }

        /// <summary>
        /// Resets the limit's to the -MinAngle and MaxAngle values of this component.
        /// </summary>
        public void ResetLimits()
        {
            Joint.LimitAngularXMotion();
            Joint.SetAngularXHighLimit(-MinAngle);
            Joint.SetAngularXLowLimit(-MaxAngle);
        }

        /// <summary>
        /// Frees the axis of rotation
        /// </summary>
        public void RemoveLimits()
        {
            Joint.angularXMotion = ConfigurableJointMotion.Free;
        }

        /// <summary>
        /// Sanity check on the angles for the joint high and low settings
        /// </summary>
        private void FixAngle(ref float min, ref float max)
        {
            if (min > 0)
            {
                min *= -1;
            }

            if (max < 0)
            {
                max *= -1;
            }

            min = Mathf.Clamp(min, min, 0);
            max = Mathf.Clamp(max, 0, max);
        }
    }
}