using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Core
{
    /// <summary>
    /// Encapsulate all rendering related operation and data
    /// </summary>
    public partial class YVRCameraRenderer
    {
        /// <summary>
        /// The camera of center eye
        /// </summary>
        public Camera centerEyeCamera { get; private set; }

        /// <summary>
        /// The camera of left eye
        /// </summary>
        public Camera leftEyeCamera { get; private set; }

        /// <summary>
        /// The camera of right eye
        /// </summary>
        public Camera rightEyeCamera { get; private set; }

        /// <summary>
        /// Gets the current measured latency values.
        /// </summary>
        public LatencyData latency
        {
            get { return YVRPlugin.Instance.GetLatencyData(); }
        }

        /// <summary>
        /// Gets the resolution and field of view for the given eye.
        /// </summary>
        public EyeRenderDescription GetEyeRenderDesc(EyeSide eye)
        {
            return eyeRenderDescriptions[eye == EyeSide.Left ? 0 : 1];
        }

        /// <summary>
        /// Get or Set the current display frequency.
        /// </summary>
        public float displayFrequency
        {
            get => YVRPlugin.Instance.GetDisplayFrequency();
            set { YVRPlugin.Instance.SetDisplayFrequency(value); }
        }

        /// <summary>
        /// Gets the list of available display frequencies supported by this hardware.
        /// </summary>
        public float[] displayFrequenciesAvailable
        {
            get { return YVRPlugin.Instance.GetDisplayFrequenciesAvailable(); }
        }

        /// <summary>
        /// Whether the application is displayed in focus.
        /// </summary>
        public bool isFocusing => YVRPlugin.Instance.IsFocusing();

        /// <summary>
        /// Whether the app is currently visible.
        /// </summary>
        public bool isVisible => YVRPlugin.Instance.IsVisible();

        private EyeRenderDescription[] eyeRenderDescriptions = new EyeRenderDescription[2];

        /// <summary>
        /// Initialize function
        /// </summary>
        public void Initialize(YVRManager yvrManager)
        {
            YVRCameraRig cameraRig = yvrManager.cameraRig;

            centerEyeCamera = cameraRig.centerEyeAnchor.AutoAddingGetComponent<Camera>();
            leftEyeCamera = cameraRig.leftEyeAnchor.AutoAddingGetComponent<Camera>();
            rightEyeCamera = cameraRig.rightEyeAnchor.AutoAddingGetComponent<Camera>();

            centerEyeCamera.tag = "MainCamera"; // mark left Camera as the Main Camera

            ConfigureEyeDescription(EyeSide.Left);
            ConfigureEyeDescription(EyeSide.Right);
        }

        private void ConfigureEyeDescription(EyeSide eye)
        {
            int index = eye == EyeSide.Left ? 0 : 1;
            YVRPlugin.Instance.GetEyeResolution(ref eyeRenderDescriptions[index].resolution);
            YVRPlugin.Instance.GetEyeFov((int)eye, ref eyeRenderDescriptions[index].fullFov);

            float maxFovX = Mathf.Max(eyeRenderDescriptions[index].fullFov.LeftFov, eyeRenderDescriptions[index].fullFov.RightFov);
            float maxFovY = Mathf.Max(eyeRenderDescriptions[index].fullFov.UpFov, eyeRenderDescriptions[index].fullFov.DownFov);
            eyeRenderDescriptions[index].fov.x = 2 * maxFovX;
            eyeRenderDescriptions[index].fov.y = 2 * maxFovY;
        }
    }
}