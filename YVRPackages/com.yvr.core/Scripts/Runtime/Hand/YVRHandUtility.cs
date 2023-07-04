using System;
using UnityEngine;

namespace YVR.Core
{
    public static class YVRHandUtility
    {

        /// <summary>
        /// Get the front joint ID of the finger
        /// </summary>
        /// <param name="fingerId">The ID of the finger</param>
        /// <returns>The front joint ID of the finger</returns>
        public static HandJoint GetFrontJointID(this XRHandFingerID fingerId)
        {
            switch (fingerId)
            {
                case XRHandFingerID.Thumb:
                    return HandJoint.JointThumbMetacarpal;

                case XRHandFingerID.Index:
                    return HandJoint.JointIndexMetacarpal;

                case XRHandFingerID.Middle:
                    return HandJoint.JointMiddleMetacarpal;

                case XRHandFingerID.Ring:
                    return HandJoint.JointRingMetacarpal;

                case XRHandFingerID.Little:
                    return HandJoint.JointLittleMetacarpal;
            }

            throw new ArgumentException(nameof(fingerId) + " must be a valid value!");
        }

        /// <summary>
        /// Get the back joint ID of the finger
        /// </summary>
        /// <param name="fingerId">The ID of the finger</param>
        /// <returns>The back joint ID of the finger</returns>
        public static HandJoint GetBackJointID(this XRHandFingerID fingerId)
        {
            switch (fingerId)
            {
                case XRHandFingerID.Thumb:
                    return HandJoint.JointThumbTip;

                case XRHandFingerID.Index:
                    return HandJoint.JointIndexTip;

                case XRHandFingerID.Middle:
                    return HandJoint.JointMiddleTip;

                case XRHandFingerID.Ring:
                    return HandJoint.JointRingTip;

                case XRHandFingerID.Little:
                    return HandJoint.JointLittleTip;
            }

            throw new ArgumentException(nameof(fingerId) + " must be a valid value!");
        }

        /// <summary>
        /// Converts a Vector3 with flipped Z-axis to a normal Vector3.
        /// </summary>
        /// <param name="v">The Vector3 with flipped Z-axis.</param>
        /// <returns>A normal Vector3.</returns>
        public static Vector3 FromFlippedZVector3f(this Vector3 v)
        {
            return new Vector3() { x = v.x, y = v.y, z = -v.z };
        }

        /// <summary>
        /// Flips the Z-axis of a Quaternion.
        /// </summary>
        /// <param name="q">The Quaternion to flip.</param>
        /// <returns>The flipped Quaternion.</returns>
        public static Quaternion FromFlippedZQuatf(this Quaternion q)
        {
            return new Quaternion() { x = -q.x, y = -q.y, z = q.z, w = q.w };
        }
    }
}