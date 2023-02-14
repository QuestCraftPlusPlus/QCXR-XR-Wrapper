using System;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Core.HandPoser
{
    [Serializable]
    public class HVRHandPoseBlend
    {
        public const string DefaultParameter = "";

        public HVRHandPose Pose;
        [Range(0, 1)] public float Weight = 1f;
        public HVRHandPoseMask Mask = HVRHandPoseMask.None;
        
        public BlendType Type = BlendType.Immediate;
        
        /// <summary>
        /// Primary Pose : The speed the hand will move toward the blended target pose.
        /// Secondary Pose : The speed Bool and Immediate type blends will move toward the defined secondary pose.
        /// </summary>
        public float Speed = 16;
        public string AnimationParameter;
        public bool ButtonParameter;
        public HVRButtons Button;
        public bool Disabled;

        public HVRFingerType ThumbType = HVRFingerType.Static;
        public HVRFingerType IndexType = HVRFingerType.Static;
        public HVRFingerType MiddleType = HVRFingerType.Static;
        public HVRFingerType RingType = HVRFingerType.Static;
        public HVRFingerType PinkyType = HVRFingerType.Static;

        public float ThumbStart = .75f;
        public float IndexStart = 1f;
        public float MiddleStart;
        public float RingStart;
        public float PinkyStart;

        [NonSerialized]
        public float Value;

        //[NonSerialized]
        //public float ElapsedTime;

        public HVRHandPoseBlend()
        {
            if (AnimationParameter == null || string.IsNullOrWhiteSpace(AnimationParameter))
            {
                AnimationParameter = DefaultParameter;
            }
        }

        public void SetDefaults()
        {
            Speed = 16f;
            AnimationParameter = DefaultParameter;
            Weight = 1f;
            Mask = HVRHandPoseMask.None;
            Type = BlendType.Immediate;
            ButtonParameter = false;

            ThumbType = HVRSettings.Instance.ThumbCurlType;
            IndexType = HVRSettings.Instance.IndexCurlType;
            MiddleType = HVRSettings.Instance.MiddleCurlType;
            RingType = HVRSettings.Instance.RingCurlType;
            PinkyType = HVRSettings.Instance.PinkyCurlType;

            ThumbStart = HVRSettings.Instance.ThumbStart;
            IndexStart = HVRSettings.Instance.IndexStart;
            MiddleStart = HVRSettings.Instance.MiddleStart;
            RingStart = HVRSettings.Instance.RingStart;
            PinkyStart = HVRSettings.Instance.PinkyStart;
        }

        public HVRFingerType GetFingerType(int index)
        {
            switch (index)
            {
                case 0:
                    return ThumbType;
                case 1:
                    return IndexType;
                case 2:
                    return MiddleType;
                case 3:
                    return RingType;
                case 4:
                    return PinkyType;
                default:
                    return HVRFingerType.Static;
            }
        }

        public float GetFingerStart(int index)
        {
            switch (index)
            {
                case 0:
                    return ThumbStart;
                case 1:
                    return IndexStart;
                case 2:
                    return MiddleStart;
                case 3:
                    return RingStart;
                case 4:
                    return PinkyStart;
                default:
                    return 0f;
            }
        }
    }

    public enum BlendType
    {
        Immediate, Manual, FloatParameter, BooleanParameter
    }

    public enum HVRFingerType
    {
        Static,
        Close
    }
}