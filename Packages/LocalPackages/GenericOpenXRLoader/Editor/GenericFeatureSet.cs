using UnityEditor;
using UnityEditor.XR.OpenXR.Features;

namespace Zappar.OpenXR.GenericAndroidLoader.Editor
{
    [OpenXRFeatureSet(
        FeatureIds = new[]
        {
            GenericFeature.featureId,
        },
        UiName = "Generic OpenXR Support",
        Description = "For targetting generic OpenXR compatible runtime on android",
        FeatureSetId = "com.zappar.openxr.generic.featureset.loader",
        SupportedBuildTargets = new BuildTargetGroup[] { BuildTargetGroup.Android }
        )]
    sealed class GenericFeatureSet
    {
    }
}