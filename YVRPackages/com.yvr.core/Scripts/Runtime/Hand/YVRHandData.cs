using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace YVR.Core
{
    public enum HandType
    {
        None = 0,
        HandLeft = 1,
        HandRight = 2
    }

    public enum ActiveInputDevice : ulong
    {
        ControllerActive = 1,
        HandTrackingActive = 2,
    }

    [Flags]
    public enum HandStatus : ulong
    {
        HandTracked = (1 << 0), // if this is set the hand pose and bone rotations data is usable
        InputStateValid = (1 << 1), // if this is set the pointer pose and pinch data is usable
        IndexPinching = (1 << 2),
        MiddlePinching = (1 << 3),
        RingPinching = (1 << 4),
        PinkPinching = (1 << 5),
        SystemGestureInProgress = (1 << 6), // if this is set the hand is currently processing a system gesture
        DominantHand = (1 << 7), // if this is set the hand is currently the dominant hand
        MenuPressed = (1 << 8) // if this is set the hand performed a menu press
    }

    public enum XRHandFingerID
    {
        /// <summary>
        /// Represents the thumb.
        /// </summary>
        Thumb,

        /// <summary>
        /// Represents the index finger.
        /// </summary>
        Index,

        /// <summary>
        /// Represents the middle finger.
        /// </summary>
        Middle,

        /// <summary>
        /// Represents the ring finger.
        /// </summary>
        Ring,

        /// <summary>
        /// Represents the little finger.
        /// </summary>
        Little
    }

    public enum HandJoint
    {
        JointPalm = 0,
        JointWrist = 1,
        JointThumbMetacarpal = 2,
        JointThumbProximal = 3,
        JointThumbDistal = 4,
        JointThumbTip = 5,
        JointIndexMetacarpal = 6,
        JointIndexProximal = 7,
        JointIndexIntermediate = 8,
        JointIndexDistal = 9,
        JointIndexTip = 10,
        JointMiddleMetacarpal = 11,
        JointMiddleProximal = 12,
        JointMiddleIntermediate = 13,
        JointMiddleDistal = 14,
        JointMiddleTip = 15,
        JointRingMetacarpal = 16,
        JointRingProximal = 17,
        JointRingIntermediate = 18,
        JointRingDistal = 19,
        JointRingTip = 20,
        JointLittleMetacarpal = 21,
        JointLittleProximal = 22,
        JointLittleIntermediate = 23,
        JointLittleDistal = 24,
        JointLittleTip = 25,
        JointMax = 26
    }

    public struct Posef
    {
        public Quaternion orientation;
        public Vector3 position;

        public override string ToString()
        {
            return string.Format("Orientation :{0}, {1}, {2}, {3}  Position: {4}, {5}, {6}",
                                 orientation.x, orientation.y, orientation.z, orientation.w,
                                 position.x, position.y, position.z);
        }

        public void ToJointPosef(HandType hand)
        {
            Quaternion rot = orientation;

            if (hand == HandType.HandLeft)
            {
                rot = new Quaternion(-rot.x, -rot.y, rot.z, rot.w) * Quaternion.AngleAxis(90f, Vector3.up);
                rot *= Quaternion.AngleAxis(180, Vector3.forward);
                rot *= Quaternion.AngleAxis(180, Vector3.up);
            }
            else
            {
                rot = new Quaternion(-rot.x, -rot.y, rot.z, rot.w) * Quaternion.AngleAxis(-90f, Vector3.up);
            }

            position.z = -position.z;
            orientation = rot;
        }
    }

    public struct HandJointLocation
    {
        public ulong locationFlags;
        public Posef pose;
        public float radius;
    }

    public struct HandJointVelocity
    {
        public ulong velocityFlags;
        public Vector3 linearVelocity;
        public Vector3 angularVelocity;
    }

    public struct HandJointLocations
    {
        public uint isActive;
        public uint jointCount;
        public float handScale;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int) HandJoint.JointMax)]
        public HandJointLocation[] jointLocations;

        public HandTrackingAimState aimState;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int) HandJoint.JointMax)]
        public HandJointVelocity[] jointVelocities;
    }

    public struct HandTrackingAimState
    {
        public HandStatus status;
        public Posef aimPose;
        public float pinchStrengthIndex;
        public float pinchStrengthMiddle;
        public float pinchStrengthRing;
        public float pinchStrengthLittle;
    };
}