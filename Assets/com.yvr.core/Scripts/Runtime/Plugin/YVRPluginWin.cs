using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.Internal;

namespace YVR.Core
{
    [ExcludeFromDocs]
    public partial class YVRPluginWin : YVRPlugin
    {

        [DllImport("yvrplugin")]
        private static extern void YVRGetControllerState(uint controllerMask, ref ControllerState state);

        [DllImport("yvrplugin")]
        private static extern bool YVRGetControllerConnected(uint controllerMask, int frameCount);

        public static YVRPluginWin Create()
        {
            return new YVRPluginWin();
        }

        public override void SetVSyncCount(YVRQualityManager.VSyncCount vSyncCount)
        {
            QualitySettings.vSyncCount = (int)vSyncCount;
        }

        public override void GetEyeResolution(ref Vector2 resolution)
        {
            resolution.x = Screen.width;
            resolution.y = Screen.height;
        }

        public override void GetEyeFov(int eyeSide, ref YVRCameraRenderer.EyeFov eyeFov)
        {
            eyeFov.UpFov = eyeFov.DownFov = eyeFov.LeftFov = eyeFov.RightFov = 45;
        }

        public override ControllerState GetControllerState(uint controllerMask)
        {
            ControllerState state = new ControllerState();
            YVRGetControllerState(controllerMask, ref state);
            return state;
        }

        public override bool GetControllerConnected(uint controllerMask)
        {
            return YVRGetControllerConnected(controllerMask, Time.frameCount);
        }
    }
}