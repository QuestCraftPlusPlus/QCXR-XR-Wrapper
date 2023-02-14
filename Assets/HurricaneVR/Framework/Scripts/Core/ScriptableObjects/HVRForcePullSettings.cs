using HurricaneVR.Framework.Core.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace HurricaneVR.Framework.Core.ScriptableObjects
{
    [CreateAssetMenu(menuName = "HurricaneVR/Force Pull Settings", fileName = "ForcePullSettings")]
    public class HVRForcePullSettings : ScriptableObject
    {
        /// <summary>
        /// Distance to the hand when auto grab will occur for non dynamic posed grabs.
        /// </summary>
        [Tooltip("Distance to the hand when auto grab will occur for non dynamic posed grabs.")]
        [Range(.01f, .3f)]
        public float DistanceThreshold = .1f;

        /// <summary>
        /// Distance to the hand when auto grab will occur for dynamic posed grabs.
        /// </summary>
        [Tooltip("Distance to the hand when auto grab will occur for dynamic posed grabs.")]
        [Range(.1f, .3f)]
        public float DynamicGrabThreshold = .2f;

        /// <summary>
        /// Max linear velocity the object will move toward your hand
        /// </summary>
        [FormerlySerializedAs("Speed")]
        [Tooltip("Max linear velocity the object will move toward your hand")]
        public float MaxSpeed = 10f;

        /// <summary>
        /// Amount of gravity force to counteract when pulling.
        /// </summary>
        [Tooltip("Amount of gravity force to counteract when pulling.")]
        [Range(0.1f, 1f)]
        public float CounterGravityFactor = 1f;

        /// <summary>
        /// ActiveDampRate applies once the object is within this distance to the hand.
        /// </summary>
        [Tooltip("ActiveDampRate applies once the object is within this distance to the hand.")]
        public float DampDistance = .5f;

        /// <summary>
        /// Object smooth lerps to the hand's velocity at this rate within DampDistance to the object.
        /// </summary>
        [FormerlySerializedAs("ActiveDampRate")] [Tooltip("Object smooth lerps to the hand's velocity at this rate within DampDistance to the object")]
        public float DampSpeed = 20f;

        /// <summary>
        /// Maximum acceleration force applied to get the object up to speed.
        /// </summary>
        [Tooltip("Maximum acceleration force applied to get the object up to speed.")]
        public float MaxAccelerationForce = 200f;

        /// <summary>
        /// Velocity is capped at this speed if released before grabbing
        /// </summary>
        [Tooltip("Velocity magnitude cap after releasing this object and not grabbing it")]
        public float MaxMissSpeed = 1f;

        /// <summary>
        /// Angular Velocity is capped at this speed if released before grabbing
        /// </summary>
        [Tooltip("Max Angular Velocity after releasing this object and not grabbing it")]
        public float MaxMissAngularSpeed = 5f;

        /// <summary>
        /// DistanceToHand: once within this range to the hand
        /// TimeSinceStart: after elapsed time since start of pull
        /// PercentTraveled: remaining distance / distance at time of grab
        /// </summary>
        [Header("Pose Rotation Trigger")]
        [Tooltip("What causes the rotation to the pose rotation to start?")]
        public ForcePullRotationTrigger RotationTrigger = ForcePullRotationTrigger.PercentTraveled;

        [Tooltip("'DistanceToHand' Trigger: Once distance from the hand to the object is below this value, the object will rotate into pose orientation.")]
        [DrawIf("RotationTrigger", ForcePullRotationTrigger.DistanceToHand)]
        public float RotateTriggerDistance = 1.5f;

        [Tooltip("'TimeSinceStart' Trigger: Start rotating after pulling for this amount of time if TimeSinceStart mode")]
        [DrawIf("RotationTrigger", ForcePullRotationTrigger.TimeSinceStart)]
        public float RotateTriggerTime = .40f;

        [Tooltip("'PercentTraveled' Trigger: Start rotating after traveling this percentage of initial distance to the hand.")]
        [DrawIf("RotationTrigger", ForcePullRotationTrigger.PercentTraveled)]
        public float RotateTriggerPercent = 30f;


        [Header("Pose Rotation Style")]
        public ForceRotationStyle RotationStyle = ForceRotationStyle.RotateOverRemaining;

        [Tooltip("Rotation Max Velocity is calculated based on Speed over this distance")]
        [DrawIf("RotationStyle", ForceRotationStyle.RotateOverDistance)]
        public float RotateOverDistance = .5f;

        [Header("Pose Rotation PD Controller")]
        public float SlerpSpring = 10000f;
        public float SlerpDamper = 500f;
        public float SlerpMaxForce = 250f;

     
    }

    public enum ForcePullRotationTrigger
    {
        DistanceToHand, //once within this range to the hand
        TimeSinceStart, //after elapsed time since start of pull
        PercentTraveled //remaining distance / distance at time of grab
    }

    public enum ForceRotationStyle
    {
        RotateOverDistance,
        RotateOverRemaining
    }
}