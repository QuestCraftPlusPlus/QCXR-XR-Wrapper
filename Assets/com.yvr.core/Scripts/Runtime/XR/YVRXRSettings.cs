using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

namespace YVR.Core.XR
{
    [System.Serializable]
    [XRConfigurationData("YVR", "YVR.Core.XR.YVRXRSettings")]
    public class YVRXRSettings : ScriptableObject
    {
        public enum StereoRenderingMode { MultiPass = 0, Multiview = 2 }

        [SerializeField, Tooltip("Use 16-bit depth buffer to save bandwidth")]
        public bool use16BitDepthBuffer = false;

        [SerializeField, Tooltip("Use the same poses for left/right eyes")]
        public bool useMonoscopic = false;

        [SerializeField, Tooltip("Set the Stereo Rendering Method")]
        public StereoRenderingMode stereoRenderingMode = StereoRenderingMode.Multiview;

        [SerializeField, Range(0.1f, 2), Tooltip("Set the Eye Resolution ")]
        public float eyeResolutionScale = 1.0f;

        public static YVRXRSettings xrSettings { get; private set; }

        public ushort GetStereoRenderingMode()
        {
            return (ushort) stereoRenderingMode;
        }

        public void Awake()
        {
            xrSettings = this;
        }
    }
}