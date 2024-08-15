using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.XR.OpenXR.Features;
#endif

namespace Zappar.OpenXR.GenericAndroidLoader
{
#if UNITY_EDITOR
    [OpenXRFeature(UiName ="Generic Android Loader",
        Desc ="Generic openxr loader",
        Version ="1.0.0",
        Company = "Zappar",
        DocumentationLink = "",
        BuildTargetGroups = new[] { BuildTargetGroup.Android},
        CustomRuntimeLoaderBuildTargets = new [] {BuildTarget.Android},
        FeatureId = featureId
        )]
#endif
    public class GenericFeature : OpenXRFeature
    {
        /// <summary>
        /// Well known feature id string for reference.
        /// </summary>
        public const string featureId = "com.zappar.openxr.generic.loader";

        public bool EnableTracker = false;
    }
}