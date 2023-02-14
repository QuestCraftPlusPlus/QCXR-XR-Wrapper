using System;
using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;


namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Used to limit rotations beyond the default Physx limit of 177 degrees. Joint is recreated at certain thresholds to allow the wider
    /// range of motion, only use this if you need range of motion beyond 177 degrees.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(HVRRotationTracker))]
    public class HVRRotationLimiter : MonoBehaviour
    {
        public const float PhysxMaxLimit = 177f;

        [Tooltip("Connected Body of the Joint")]
        public Rigidbody ConnectedBody;

        [Tooltip("Minimum angle of rotation")]
        public int MinAngle;

        [Tooltip("Maximum angle of rotation")]
        public int MaxAngle;

        [Tooltip("Distance traveled before the joint is recreated with new limits")]
        public float JointResetThreshold = 90f;


        public Rigidbody Rigidbody { get; private set; }

        public HVRRotationTracker Tracker { get; private set; }

        private ConfigurableJoint _joint;
        private float _angleAtCreation;
        
        [Header("Debugging")]
        public float maxDelta;
        public float minDelta;

        protected virtual void Start()
        {
            Rigidbody = this.GetRigidbody();
            Tracker = GetComponent<HVRRotationTracker>();

            if (MinAngle > 0)
            {
                MinAngle *= -1;
            }

            if (MaxAngle < 0)
            {
                MaxAngle *= -1;
            }

            MinAngle = Mathf.Clamp(MinAngle, MinAngle, 0);
            MaxAngle = Mathf.Clamp(MaxAngle, 0, MaxAngle);
        }

        protected virtual void FixedUpdate()
        {
            var angle = Mathf.Clamp(Tracker.Angle, MinAngle, MaxAngle);
            minDelta = Math.Abs(MinAngle - angle);
            maxDelta = MaxAngle - angle;

            var force = false;
            if (_joint)
            {
                var angleFromJointCreation = angle - _angleAtCreation;
                var angleDelta = Mathf.Abs(angleFromJointCreation);

                if (angleDelta > JointResetThreshold)
                {
                    Destroy(_joint);
                    force = true;
                }
            }

            if (!_joint || force)
            {
                if (minDelta < PhysxMaxLimit || maxDelta < PhysxMaxLimit)
                {
                    _joint = gameObject.AddComponent<ConfigurableJoint>();
                    _joint.axis = Tracker.AxisOfRotation;
                    _joint.secondaryAxis = Tracker.AxisOfRotation.OrthogonalVector();
                    _joint.LimitAngularXMotion();
                    _joint.connectedBody = ConnectedBody;

                    _angleAtCreation = Tracker.Angle;

                    if (minDelta < PhysxMaxLimit)
                    {
                        _joint.SetAngularXHighLimit(minDelta);
                    }
                    else
                    {
                        _joint.SetAngularXHighLimit(PhysxMaxLimit);
                    }

                    if (maxDelta < PhysxMaxLimit)
                    {
                        _joint.SetAngularXLowLimit(-maxDelta);
                    }
                    else
                    {
                        _joint.SetAngularXLowLimit(-PhysxMaxLimit);
                    }
                }
            }
        }
    }
}