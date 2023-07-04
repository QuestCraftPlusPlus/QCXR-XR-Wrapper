using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.Management;
using UnityEngine;

namespace YVR.Core.XR
{
    public class YVRXRBuildProcess : XRBuildHelper<YVRXRSettings>
    {
        public override string BuildSettingsKey
        {
            get { return "YVR.Core.XR.YVRXRSettings"; }
        }
    }
}