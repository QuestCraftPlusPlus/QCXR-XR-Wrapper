using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;
#endif

namespace YVR.Core.XR
{
#if UNITY_INPUT_SYSTEM
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class InputLayoutLoader
    {
        static InputLayoutLoader()
        {
            Debug.Log("InputLayoutLoader");
            RegisterInputLayouts();
        }

        public static void RegisterInputLayouts()
        {
            InputSystem.RegisterLayout<YVRXRHMD>("YVRHMD",
               matches: new InputDeviceMatcher()
                   .WithInterface(XRUtilities.InterfaceMatchAnyVersion)
                   .WithProduct("Headset").WithManufacturer("YVR"));
            InputSystem.RegisterLayout<YVRXRController>("YVRController",
               matches: new InputDeviceMatcher()
                   .WithInterface(XRUtilities.InterfaceMatchAnyVersion)
                   .WithProduct(@"(Controller)$").WithManufacturer("YVR"));
        }
    }
#endif
}