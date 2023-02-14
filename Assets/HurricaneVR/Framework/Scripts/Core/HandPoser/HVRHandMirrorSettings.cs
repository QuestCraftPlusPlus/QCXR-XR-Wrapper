using System;
using System.Collections.Generic;
using UnityEngine;

namespace HurricaneVR.Framework.Core.HandPoser
{
    [CreateAssetMenu(menuName = "HurricaneVR/Mirror Settings", fileName = "HandMirrorSettings")]
    public class HVRHandMirrorSettings : ScriptableObject
    {
        public bool UseThumbSetting;
        public bool UseIndexSetting;
        public bool UseMiddleSetting;
        public bool UseRingSetting;
        public bool UsePinkySetting;

        public HVRJointMirrorSetting AllSetting;
        public HVRJointMirrorSetting ThumbSetting;
        public HVRJointMirrorSetting IndexSetting;
        public HVRJointMirrorSetting MiddleSetting;
        public HVRJointMirrorSetting RingSetting;
        public HVRJointMirrorSetting PinkySetting;

        public List<HVRJointMirrorSetting> ThumbSettings;
        public List<HVRJointMirrorSetting> IndexSettings;
        public List<HVRJointMirrorSetting> MiddleSettings;
        public List<HVRJointMirrorSetting> RingSettings;
        public List<HVRJointMirrorSetting> PinkySettings;
    }

    public enum FingerMirrorRotation
    {
        Same, Opposite, Minus180, Plus180, Neg180Minus, P180Minus
    }

    public enum FingerMirrorPosition
    {
        Opposite, Same
    }

    [Serializable]
    public class HVRJointMirrorSetting
    {
        public FingerMirrorRotation XRotation;
        public FingerMirrorRotation YRotation;
        public FingerMirrorRotation ZRotation;

        public FingerMirrorPosition XPosition;
        public FingerMirrorPosition YPosition;
        public FingerMirrorPosition ZPosition;
    }
}