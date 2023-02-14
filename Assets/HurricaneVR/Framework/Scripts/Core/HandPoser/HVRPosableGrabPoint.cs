using System.Collections.Generic;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.Serialization;

namespace HurricaneVR.Framework.Core.HandPoser
{
    public class HVRPosableGrabPoint : MonoBehaviour
    {
        public int PoserIndex;

        [Header("Settings")]
        public bool IsJointAnchor;

        public bool IsForceGrabbable = true;

        [Tooltip("If true only one hand can grab this grabpoint")]
        public bool OneHandOnly;

        [Range(0f, 360f)]
        public float AllowedAngleDifference = 360f;

        public bool CheckDistance;
        public float MaxDistance = .3f;

        [Tooltip("Can the Left hand grab this")]
        public bool LeftHand = true;

        [Tooltip("Can the right hand grab this")]
        public bool RightHand = true;

        [Tooltip("Grab Points in the same group will have pose rotation considered")]
        public int Group = -1;

        [Header("Controller Tracking Offsets")]
        [FormerlySerializedAs("jointOffset")]
        public Vector3 HandRotationOffset;
        public Vector3 HandPositionOffset;

        [Header("Transforms")]

        [Tooltip("If populated, the grab point highlight indicator will be placed at this transform's position instead of this transform's position")]
        public Transform GrabIndicatorPosition;
        public Transform VisualGrabPoint;

        public HVRHandPoser HandPoser;

        [Tooltip("Auto populated from the first parent if not supplied")]
        public HVRGrabbable Grabbable;


        [Header("Line Grab")]
        public bool IsLineGrab;

        [DrawIf("IsLineGrab", true)]
        public Transform LineStart;

        [DrawIf("IsLineGrab", true)]
        public Transform LineEnd;

        [DrawIf("IsLineGrab", true)]
        public bool CanLineFlip = true;

        [DrawIf("IsLineGrab", true)]
        public float LooseDamper = 100;

        [DrawIf("IsLineGrab", true)]
        public float LooseAngularDamper = 1;

        [DrawIf("IsLineGrab", true)]
        public bool LineCanReposition = true;

        [DrawIf("IsLineGrab", true)]
        public bool LineInitialCanReposition = true;

        [DrawIf("IsLineGrab", true)]
        public bool LineCanRotate = true;

        [DrawIf("IsLineGrab", true)]
        public bool LineFreeRotation;

        [DrawIf("IsLineGrab", true)]
        public bool LineInitialCanRotate = true;

        public Vector3 WorldLine => LineEnd.position - LineStart.position;
        public Vector3 WorldLineMiddle => (LineEnd.position + LineStart.position) / 2f;

        public Quaternion LeftPoseOffset { get; private set; }
        public Quaternion RightPoseOffset { get; private set; }

        public Vector3 LeftPosePositionOffset { get; private set; }

        public Vector3 RightPosePositionOffset { get; private set; }

        public List<HVRPosableGrabPoint> Others = new List<HVRPosableGrabPoint>();

        protected virtual void Start()
        {
            if (!HandPoser)
                TryGetComponent(out HandPoser);

            LeftPoseOffset = Quaternion.identity;
            RightPoseOffset = Quaternion.identity;

            if (!Grabbable)
                Grabbable = GetComponentInParent<HVRGrabbable>();

            if (HandPoser)
            {
                if (HandPoser && HandPoser.PrimaryPose != null && HandPoser.PrimaryPose.Pose && HandPoser.PrimaryPose.Pose.RightHand != null)
                {
                    RightPoseOffset = Quaternion.Euler(HandPoser.PrimaryPose.Pose.RightHand.Rotation.eulerAngles);
                    RightPosePositionOffset = HandPoser.PrimaryPose.Pose.RightHand.Position;
                }
                else if (RightHand)
                {
                    Debug.LogWarning($"Right Hand pose missing! {(Grabbable != null ? Grabbable.name : null)}.{this.name}");
                }

                if (HandPoser && HandPoser.PrimaryPose != null && HandPoser.PrimaryPose.Pose && HandPoser.PrimaryPose.Pose.LeftHand != null && LeftHand)
                {
                    LeftPoseOffset = Quaternion.Euler(HandPoser.PrimaryPose.Pose.LeftHand.Rotation.eulerAngles);
                    LeftPosePositionOffset = HandPoser.PrimaryPose.Pose.LeftHand.Position;
                }
                else if (LeftHand)
                {
                    Debug.LogWarning($"Left Hand pose missing! {(Grabbable != null ? Grabbable.name : null)}.{this.name}");
                }
            }
        }

        public void AddGroupedGrabPoint(HVRPosableGrabPoint p)
        {
            if (p != this && !Others.Contains(p))
            {
                Others.Add(p);
            }
        }

        public Vector3 GetPoseWorldPosition(HVRHandSide side)
        {
            if (side == HVRHandSide.Left)
                return transform.TransformPoint(LeftPosePositionOffset);
            return transform.TransformPoint(RightPosePositionOffset);
        }

        public Vector3 GetPosePositionOffset(HVRHandSide side)
        {
            if (side == HVRHandSide.Left)
                return LeftPosePositionOffset;
            return RightPosePositionOffset;
        }

        public Quaternion GetPoseRotationOffset(HVRHandSide side)
        {
            if (side == HVRHandSide.Left)
                return LeftPoseOffset;
            return RightPoseOffset;
        }

        public Quaternion GetPoseWorldRotation(HVRHandSide side)
        {
            if (side == HVRHandSide.Left)
                return transform.rotation * LeftPoseOffset;
            return transform.rotation * RightPoseOffset;
        }

        public Quaternion GetGrabbableRelativeRotation(HVRHandSide side)
        {
            return Quaternion.Inverse(Grabbable.transform.rotation) * GetPoseWorldRotation(side);
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            // Show Grip Points
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 0.015f);
        }

#endif
    }
}
