using System;
using System.Runtime.InteropServices.WindowsRuntime;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.Serialization;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Stores Unity SDK and Device offset combinations to handle the differences between SDK's and the position and rotation values they report.
    /// </summary>
    [CreateAssetMenu(menuName = "HurricaneVR/Controller Offsets", fileName = "ControllerOffset")]
    public class HVRControllerOffsets : ScriptableObject
    {
        [Header("Unity XR")]
        public HVRDevicePoseOffset Oculus;
        public HVRDevicePoseOffset WMR;

        [Header("SteamVR")]
        public HVRDevicePoseOffset OculusSteamVR;
        public HVRDevicePoseOffset WMRSteamVR;
        public HVRDevicePoseOffset ReverbG2SteamVR;
        public HVRDevicePoseOffset CosmosSteamVR;
        public HVRDevicePoseOffset ViveSteamVR;
        public HVRDevicePoseOffset KnucklesSteamVR;

        [Header("OpenXR")]
        public HVRDevicePoseOffset OculusOpenXR;
        public HVRDevicePoseOffset WMROpenXR;
        public HVRDevicePoseOffset ReverbG2OpenXR;
        public HVRDevicePoseOffset ViveOpenXR;
        public HVRDevicePoseOffset CosmosOpenXR;
        public HVRDevicePoseOffset KnucklesOpenXR;

        public HVRDevicePoseOffset GetDeviceOffset(HVRHandSide side)
        {
            if (side == HVRHandSide.Left)
                return GetDeviceOffset(HVRInputManager.Instance.LeftController.ControllerType);
            return GetDeviceOffset(HVRInputManager.Instance.RightController.ControllerType);
        }

        public HVRDevicePoseOffset GetDeviceOffset(HVRControllerType type)
        {
            var steamVR = HVRInputManager.Instance.IsSteamVR;
            var openXr = HVRInputManager.Instance.IsOpenXR;

            switch (type)
            {

                case HVRControllerType.Oculus:
                    if (steamVR)
                    {
                        return OculusSteamVR;
                    }
                    else if (openXr)
                    {
                        return OculusOpenXR;
                    }
                    else
                    {
                        return Oculus;
                    }
                case HVRControllerType.WMR:
                    if (steamVR)
                    {
                        return WMRSteamVR;
                    }
                    else if (openXr)
                    {
                        return WMROpenXR;
                    }
                    else
                    {
                        return WMR;
                    }
                case HVRControllerType.Vive:
                    if (steamVR)
                    {
                        return ViveSteamVR;
                    }
                    else if (openXr)
                    {
                        return ViveOpenXR;
                    }
                    break;
                case HVRControllerType.Knuckles:
                    if (steamVR)
                    {
                        return KnucklesSteamVR;
                    }
                    else if (openXr)
                    {
                        return KnucklesOpenXR;
                    }
                    break;
                case HVRControllerType.Cosmos:
                    if (steamVR)
                    {
                        return CosmosSteamVR;
                    }
                    else if (openXr)
                    {
                        return CosmosOpenXR;
                    }
                    break;
                case HVRControllerType.ReverbG2:
                    if (steamVR)
                    {
                        return ReverbG2SteamVR;
                    }
                    else if (openXr)
                    {
                        return ReverbG2OpenXR;
                    }
                    break;
            }

            return null;
        }
    }

    [Serializable]
    public class HVRDevicePoseOffset
    {
        public Vector3 Position;
        [FormerlySerializedAs("_rotation")] [SerializeField]
        public Vector3 Rotation;
    }
}