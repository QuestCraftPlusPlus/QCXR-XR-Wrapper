using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.XR.OpenXR.Features;
using System.Runtime.InteropServices;
using System.Text;
 
#if UNITY_EDITOR
[UnityEditor.XR.OpenXR.Features.OpenXRFeature(UiName = "Get System Info",
    BuildTargetGroups = new[] { BuildTargetGroup.Android },
    Company = "Fireproof Studios",
    Desc = "Feature extension for getting system info.",
    DocumentationLink = "https://docs.unity3d.com/Packages/com.unity.xr.openxr@1.8/manual/index.html",
    Version = "0.0.1",
    FeatureId = featureId)]
#endif
public class OpenXRFeatureSystemInfo : OpenXRFeature
{
    /// <summary>
    /// The feature id string. This is used to give the feature a well known id for reference.
    /// </summary>
    public const string featureId = "com.unity.openxr.feature.example.getSystemInfo";
 
    // delegate for xrGetInstanceProcAddr() https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrGetInstanceProcAddr.html
    unsafe private delegate ulong GetInstanceProcAddrDelegate(ulong instance, StringBuilder procName, IntPtr* procAddr);
    // delegate for xrGetSystemProperties() https://registry.khronos.org/OpenXR/specs/1.0/man/html/xrGetSystemProperties.html
    unsafe private delegate ulong GetSystemPropertiesDelegate(ulong instance, ulong systemId, void* properties);
 
    // XrSystemProperties structure https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrSystemProperties.html
    unsafe struct XrSystemProperties
    {
        public ulong type;      // 64 bit
        public void* next;      // 64 bit
        public ulong systemId;  // 64 bit
        public int vendorId;    // 32 bit
        public fixed byte systemName[XR_MAX_SYSTEM_NAME_SIZE];
        public XrSystemGraphicsProperties graphicsProperties;
        public XrSystemTrackingProperties trackingProperties;
    };
 
    // XrSystemGraphicsProperties structure https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrSystemGraphicsProperties.html
    struct XrSystemGraphicsProperties
    {
        public uint maxSwapchainImageHeight;
        public uint maxSwapchainImageWidth;
        public uint maxLayerCount;
    };
 
    // XrSystemTrackingProperties structure https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrSystemTrackingProperties.html
    struct XrSystemTrackingProperties
    {
        public uint orientationTracking;
        public uint positionTracking;
    };
 
    // https://registry.khronos.org/OpenXR/specs/1.0/man/html/XrStructureType.html
    private const int XR_TYPE_SYSTEM_PROPERTIES = 5;
    private const int XR_MAX_SYSTEM_NAME_SIZE = 256;
 
    private static string SystemName = null;
    private static ulong XrInstance = 0;
    private static ulong XrSystem = 0;
 
    protected override bool OnInstanceCreate(ulong xrInstance)
    {
        XrInstance = xrInstance;
        return base.OnInstanceCreate(xrInstance);
    }
 
    protected override void OnSystemChange(ulong xrSystem)
    {
        XrSystem = xrSystem;
        base.OnSystemChange(xrSystem);
    }
 
    public static string GetHeadsetName()
    {
        if (SystemName == null
            && XrInstance != 0
            && XrSystem != 0
            && OpenXRFeature.xrGetInstanceProcAddr != IntPtr.Zero)
        {
            GetInstanceProcAddrDelegate getInstanceProcAddr = Marshal.GetDelegateForFunctionPointer<GetInstanceProcAddrDelegate>(OpenXRFeature.xrGetInstanceProcAddr);
            unsafe
            {
                // call xrGetInstanceProcAddr() to get the address of the xrGetSystemProperties() function
                IntPtr xrGetSystemPropertiesAddr;
                ulong result = getInstanceProcAddr(XrInstance, new StringBuilder("xrGetSystemProperties"), &xrGetSystemPropertiesAddr);
 
                GetSystemPropertiesDelegate getSystemPropertiesAddr = Marshal.GetDelegateForFunctionPointer<GetSystemPropertiesDelegate>(xrGetSystemPropertiesAddr);
                if (xrGetSystemPropertiesAddr != null)
                {
                    // call xrGetSystemProperties()
                    // the properties structure needs to have the type initialised otherwise xrGetSystemProperties() fails
                    XrSystemProperties properties;
                    properties.type = XR_TYPE_SYSTEM_PROPERTIES;
                    result = getSystemPropertiesAddr(XrInstance, XrSystem, &properties);
 
                    // convert the name from an array of bytes to a c# string
                    SystemName = "";
                    for (int charIndex = 0; charIndex < XR_MAX_SYSTEM_NAME_SIZE; charIndex++)
                    {
                        if (properties.systemName[charIndex] == 0)
                        {
                            break;
                        }
                        SystemName += ((char)properties.systemName[charIndex]);
                    }
                }
            }
        }
        return SystemName;
    }
}
 