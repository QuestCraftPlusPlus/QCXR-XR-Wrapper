using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace YVR.Core.XR
{
    [StructLayout(LayoutKind.Sequential)]
    public struct YVRXRUserDefinedSettings
    {
        [MarshalAs(UnmanagedType.U1)]
        public bool use16BitDepthBuffer;

        [MarshalAs(UnmanagedType.U1)]
        public bool useMonoscopic;

        [MarshalAs(UnmanagedType.U1)]
        public bool useLinearColorSpace;

        [MarshalAs(UnmanagedType.U1)]
        public bool useAndroidSession;
        [MarshalAs(UnmanagedType.U1)]
        public bool UseVRWidget;

        public ushort stereoRenderingMode;
        public float eyeRenderScale;
    }
}