using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Unity.XR.CoreUtils;
using Unity.XR.PXR;
using UnityEditor;
using UnityEngine;
#if AR_FOUNDATION
using UnityEngine.XR.ARSubsystems;
#endif
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor.XR.OpenXR.Features;
#endif


namespace Unity.XR.OpenXR.Features.PICOSupport
{
#if UNITY_EDITOR
    [OpenXRFeature(UiName = "PICO Spatial Anchor",
        Hidden = false,
        BuildTargetGroups = new[] { UnityEditor.BuildTargetGroup.Android },
        Company = "PICO",
        OpenxrExtensionStrings = extensionString,
        Version = "1.0.0",
        FeatureId = featureId)]
#endif
    public class PICOSpatialAnchor: OpenXRFeature
    {
        public const string featureId = "com.pico.openxr.feature.spatialanchor";
        public const string extensionString = "XR_PICO_spatial_anchor XR_PICO_spatial_sensing XR_EXT_future";
        
        public static bool isEnable => OpenXRRuntime.IsExtensionEnabled("XR_PICO_spatial_anchor");

        protected override void OnSessionCreate(ulong xrSession)
        {
            base.OnSessionCreate(xrSession);
            PXR_Plugin.MixedReality.UPxr_CreateSpatialAnchorSenseDataProvider();
        }
        protected override void OnSessionExiting(ulong xrSession)
        {
            PXR_MixedReality.GetSenseDataProviderState(PxrSenseDataProviderType.SpatialAnchor, out var providerState);
            if (providerState == PxrSenseDataProviderState.Running)
            {
                PXR_MixedReality.StopSenseDataProvider(PxrSenseDataProviderType.SpatialAnchor);
            }

            PXR_Plugin.MixedReality.UPxr_DestroySenseDataProvider(
                PXR_Plugin.MixedReality.UPxr_GetSenseDataProviderHandle(PxrSenseDataProviderType.SpatialAnchor));

            base.OnSessionExiting(xrSession);
        }
        
#if AR_FOUNDATION
        public  bool isAnchorSubsystem=true;
        static List<XRAnchorSubsystemDescriptor> anchorSubsystemDescriptors = new List<XRAnchorSubsystemDescriptor>();
        protected override void OnSubsystemCreate()
        {
            base.OnSubsystemCreate();
            if (isAnchorSubsystem)
            {
                CreateSubsystem<XRAnchorSubsystemDescriptor, XRAnchorSubsystem>(
                    anchorSubsystemDescriptors,
                    PICOAnchorSubsystem.k_SubsystemId);
            }

        }
        protected override void OnSubsystemStart()
        {
            if (isAnchorSubsystem)
            {
                StartSubsystem<XRAnchorSubsystem>();
            }
        }
        protected override void OnSubsystemStop()
        {
            if (isAnchorSubsystem)
            {
                StopSubsystem<XRAnchorSubsystem>();
            }
        }
        protected override void OnSubsystemDestroy()
        {
            if (isAnchorSubsystem)
            {
                DestroySubsystem<XRAnchorSubsystem>();
            }
        }
#endif
    }
}