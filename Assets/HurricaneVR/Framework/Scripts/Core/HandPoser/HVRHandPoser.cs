using System;
using System.Collections.Generic;
using UnityEngine;

namespace HurricaneVR.Framework.Core.HandPoser
{
    [Flags]
    public enum HVRHandPoseMask
    {
        None = 0, Hand = 1, Thumb = 2, Index = 4, Middle = 8, Ring = 16, Pinky = 32
    }

    [HelpURL("https://cloudwalker2020.github.io/HurricaneVR-Docs/manual/hand_posing.html")]
    public class HVRHandPoser : MonoBehaviour
    {
        #region EditorState 

        [SerializeField]
        protected GameObject LeftHandPreview;

        [SerializeField]
        protected GameObject RightHandPreview;

        [SerializeField]
        protected GameObject BodyPreview;

        [SerializeField]
        protected bool PreviewLeft;

        [SerializeField]
        protected bool PreviewRight;

        [SerializeField]
        protected bool LeftAutoPose;

        [SerializeField]
        protected bool RightAutoPose;

        [SerializeField]
        protected int SelectionIndex;

        [SerializeField]
        public List<string> PoseNames = new List<string>();

        #endregion


        public HVRHandPoseBlend PrimaryPose;
        public List<HVRHandPoseBlend> Blends = new List<HVRHandPoseBlend>();

        public MirrorAxis MirrorAxis = MirrorAxis.X;

        private void Awake()
        {
            if (LeftHandPreview)
            {
                Destroy(LeftHandPreview);
                Debug.Log($"Left hand preview was still active. Destroyed.");
            }

            PreviewLeft = false;
            PreviewRight = false;

            if (RightHandPreview)
            {
                Destroy(RightHandPreview);
                Debug.Log($"Right hand preview was still active. Destroyed.");
            }

            if (BodyPreview)
            {
                Destroy(BodyPreview);
                Debug.Log($"Full Body Preview was still active. Destroyed.");
            }
        }
    }

    [Serializable]
    public enum MirrorAxis
    {
        X, Y, Z
    }
}
