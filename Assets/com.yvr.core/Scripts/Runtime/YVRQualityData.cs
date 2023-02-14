using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace YVR.Core
{
    public partial class YVRQualityManager
    {
        /// <summary>
        /// VSync count every frame.
        /// </summary>
        public enum VSyncCount
        {
            /// <summary>
            /// VSync every frame
            /// </summary>
            k1 = 1,
            /// <summary>
            /// VSync every two frame,FPS reduced to half.
            /// </summary>
            k2 = 2,
        };

        /// <summary>
        /// The level of fixed foveated rendering
        /// </summary>
        public enum FixedFoveatedRenderingLevel
        {
            /// <summary>
            /// Disable fixed foveated rendering.
            /// </summary>
            Off = 0,
            /// <summary>
            ///  The lowest level of fixed foveated rendering
            /// </summary>
            Low = 1,
            /// <summary>
            ///  The Medium level of fixed foveated rendering
            /// </summary>
            Medium = 2,
            /// <summary>
            /// The High level of fixed foveated rendering
            /// </summary>
            High = 3,
            /// <summary>
            /// The TopHigh level of fixed foveated rendering
            /// </summary>
            TopHigh = 4
        }
    }
}