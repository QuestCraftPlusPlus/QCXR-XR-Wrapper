using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.XR.Management.Metadata;
using UnityEngine.XR;


namespace YVR.Core.XR
{
    public class YVRXRMetaData : IXRPackage
    {
        public bool PopulateNewSettingsInstance(ScriptableObject obj)
        {
            var settings = obj as YVRXRSettings;

            if (settings != null)
            {
                settings.stereoRenderingMode = YVRXRSettings.StereoRenderingMode.MultiPass;
                settings.eyeResolutionScale = 1.0f;
                settings.use16BitDepthBuffer = false;
                settings.useMonoscopic = false;
                return true;
            }

            return false;
        }

        private class YVRXRPackageMetadata : IXRPackageMetadata
        {
            public string packageName => "YVR";
            public string packageId => "com.yvr.core";
            public string settingsType => typeof(YVRXRSettings).FullName;

            public List<IXRLoaderMetadata> loaderMetadata => new List<IXRLoaderMetadata>()
            {
                new YVRLoaderMetaData()
            };
        }

        private static IXRPackageMetadata s_metadata = new YVRXRPackageMetadata();
        public IXRPackageMetadata metadata => s_metadata;

        private class YVRLoaderMetaData : IXRLoaderMetadata
        {
            public string loaderName => "YVR";
            public string loaderType => typeof(YVRXRLoader).FullName;

            public List<BuildTargetGroup> supportedBuildTargets => new List<BuildTargetGroup>()
            {
                BuildTargetGroup.Android,
                BuildTargetGroup.Standalone
            };
        }
    }
}