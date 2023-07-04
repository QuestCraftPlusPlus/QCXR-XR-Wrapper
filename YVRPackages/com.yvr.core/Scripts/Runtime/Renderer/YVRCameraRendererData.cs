using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Core
{
    public partial class YVRCameraRenderer
    {
        /// <summary>
        /// Contains full fov information per eye in degree
        /// Under Symmetric Fov mode, UpFov == DownFov and LeftFov == RightFov.
        /// </summary>
        public struct EyeFov
        {
            /// <summary>
            /// the fov of left screen
            /// </summary>
            public float LeftFov;
            /// <summary>
            ///  the fov of right screen
            /// </summary>
            public float RightFov;
            /// <summary>
            /// the fov of up screen
            /// </summary>
            public float UpFov;
            /// <summary>
            /// the fov of down screen
            /// </summary>
            public float DownFov;
        }

        /// <summary>
        /// Specifies the size and field-of-view for one eye texture.
        /// </summary>
        public struct EyeRenderDescription
        {
            /// <summary>
            /// The horizontal and vertical size of the texture.
            /// </summary>
            public Vector2 resolution;

            /// <summary>
            /// The angle of the horizontal and vertical field of view in degrees.
            /// For Symmetric FOV interface compatibility
            /// Note this includes the fov angle from both sides
            /// </summary>
            public Vector2 fov;

            /// <summary>
            /// The full information of field of view in degrees.
            /// </summary>
            public EyeFov fullFov;
        }

        /// <summary>
        /// Latency information for a single frame of rendering.
        /// </summary>
        public struct LatencyData
        {
            /// <summary>
            /// The time spent rendering both eyes in seconds.
            /// </summary>
            public float render;

            /// <summary>
            /// The time spent performing TimeWarp in seconds.
            /// </summary>
            public float timeWarp;

            /// <summary>
            /// The time between the end of TimeWarp and scan-out in seconds.
            /// </summary>
            public float postPresent;

            /// <summary>
            /// The flag for render error
            /// </summary>
            public float renderError;

            /// <summary>
            /// The flag for time warp error
            /// </summary>
            public float timeWarpError;
        }
    }
}