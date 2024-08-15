using System;
using System.Xml;
using UnityEditor.Build.Reporting;
using UnityEditor.XR.OpenXR.Features;

namespace Zappar.OpenXR.GenericAndroidLoader.Editor
{
    internal class ModifyAndroidManfest : OpenXRFeatureBuildHooks
    {
        public override int callbackOrder => 1;

        public override Type featureType => typeof(GenericFeature);

        protected override void OnPostGenerateGradleAndroidProjectExt(string path)
        {
            //throw new NotImplementedException();
        }

        protected override void OnPostprocessBuildExt(BuildReport report)
        {
            //throw new NotImplementedException();
        }

        protected override void OnPreprocessBuildExt(BuildReport report)
        {
            //throw new NotImplementedException();
        }
    }

}