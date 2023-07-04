using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Rendering;
using UnityEngine.XR;
using UnityEngine.XR.Management;
#if UNITY_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;
#endif

namespace YVR.Core.XR
{
    public class YVRXRLoader : XRLoaderHelper
    {
        [DllImport("yvrplugin")]
        private static extern void YVRSetXRUserDefinedSettings(ref YVRXRUserDefinedSettings userDefinedSettings);

        [DllImport("yvrplugin")]
        private static extern int YVRSetDevelopmentBuildMode(bool isDevelopmentBuild);

        private static List<XRDisplaySubsystemDescriptor> displaySubsystemDescriptors
            = new List<XRDisplaySubsystemDescriptor>();

        private static List<XRInputSubsystemDescriptor> inputSubsystemDescriptors
            = new List<XRInputSubsystemDescriptor>();

        public override bool Initialize()
        {
#if UNITY_INPUT_SYSTEM
            InputLayoutLoader.RegisterInputLayouts();
#endif

            YVRXRSettings settings = null;
#if UNITY_EDITOR
            UnityEditor.EditorBuildSettings.TryGetConfigObject<YVRXRSettings>("YVR.Core.XR.YVRXRSettings", out settings);
#elif UNITY_ANDROID
            settings = YVRXRSettings.xrSettings;
            YVREventTracking.instance.UploadSDKInfo();
#endif
            YVRXRUserDefinedSettings userDefinedSettings = new YVRXRUserDefinedSettings();

            userDefinedSettings.stereoRenderingMode = settings.GetStereoRenderingMode();
            userDefinedSettings.eyeRenderScale = settings.eyeResolutionScale;
            userDefinedSettings.use16BitDepthBuffer = settings.use16BitDepthBuffer;
            userDefinedSettings.useMonoscopic = settings.useMonoscopic;
            userDefinedSettings.useLinearColorSpace = QualitySettings.activeColorSpace == ColorSpace.Linear;
            userDefinedSettings.useAndroidSession = settings.useAndroidSession;
            userDefinedSettings.UseVRWidget = settings.useVRWidget;
            YVRSetXRUserDefinedSettings(ref userDefinedSettings);

#if DEVELOPMENT_BUILD
            YVRSetDevelopmentBuildMode(true);
#else
            YVRSetDevelopmentBuildMode(false);
#endif

            CreateSubsystem<XRDisplaySubsystemDescriptor, XRDisplaySubsystem>(displaySubsystemDescriptors, "Display");
            CreateSubsystem<XRInputSubsystemDescriptor, XRInputSubsystem>(inputSubsystemDescriptors, "Tracking");
            return true;
        }

        public override bool Start()
        {
            StartSubsystem<XRDisplaySubsystem>();
            StartSubsystem<XRInputSubsystem>();
            return true;
        }

        public override bool Stop()
        {
            StopSubsystem<XRDisplaySubsystem>();
            StopSubsystem<XRInputSubsystem>();
            return true;
        }

        public override bool Deinitialize()
        {
            DestroySubsystem<XRDisplaySubsystem>();
            DestroySubsystem<XRInputSubsystem>();
            return true;
        }
    }
}