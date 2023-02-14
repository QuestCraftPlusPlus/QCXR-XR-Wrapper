using HurricaneVR.Framework.Core.HandPoser.Data;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Core.HandPoser
{
    public class HVRHandPose : ScriptableObject
    {
        public bool SnappedLeft;

        public HVRHandPoseData LeftHand;
        public HVRHandPoseData RightHand;

        public HVRHandPoseData GetPose(bool isLeft)
        {
            return isLeft ? LeftHand : RightHand;
        }

        public HVRHandPoseData GetPose(HVRHandSide side)
        {
            return side == HVRHandSide.Left ? LeftHand : RightHand;
        }

        public HVRHandPose DeepCopy()
        {
            var copy = ScriptableObject.CreateInstance<HVRHandPose>();
            copy.LeftHand = LeftHand.DeepCopy();
            copy.RightHand = RightHand.DeepCopy();
            return copy;
        }
    }
}