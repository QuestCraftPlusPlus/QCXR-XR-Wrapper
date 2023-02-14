//using System;
//using System.Collections.Generic;
//using UnityEngine;

//namespace HurricaneVR.Framework.Shared.HandPoser.Data
//{
//    [CreateAssetMenu(menuName = "HurricaneVR/RuntimePoses", fileName = "RuntimePoses")]
//    public class HVRRuntimePosesData : ScriptableObject
//    {
//        public List<VRRunTimePose> PosableGrabPoints = new List<VRRunTimePose>();

//        public void AddPose(VRRunTimePose pose)
//        {
//            PosableGrabPoints.Add(pose);
//        }

//#if UNITY_EDITOR
//        public void Save()
//        {
//            UnityEditor.AssetDatabase.Refresh();
//            UnityEditor.EditorUtility.SetDirty(this);
//            UnityEditor.AssetDatabase.SaveAssets();
//        }

//#endif

//    }

//    [Serializable]
//    public class VRRunTimePose
//    {
//        public HVRHandPoseData Left;
//        public HVRHandPoseData Right;
//        public Vector3 Position;
//        public Quaternion Rotation;
//    }
//}