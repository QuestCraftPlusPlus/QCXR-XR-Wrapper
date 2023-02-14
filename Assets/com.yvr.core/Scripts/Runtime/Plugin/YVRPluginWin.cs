using UnityEngine;
using UnityEngine.Internal;

namespace YVR.Core
{
    [ExcludeFromDocs]
    public class YVRPluginWin : YVRPlugin
    {
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
    }
}