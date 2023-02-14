using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Shared;
using HurricaneVR.Framework.Shared.Utilities;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    public class HVREditorManager
    {
        private const string HurricaneVRUploader = "HurricaneVRUploader";
        public const string Version = "2.9.1e";

        [InitializeOnLoadMethod]
        private static void Hook()
        {
            EditorApplication.delayCall += CheckUpdate;
        }


        private static void CheckUpdate()
        {
            if (Application.productName != HurricaneVRUploader)
            {
                var kickit = HVRSettings.Instance;

                if (HVREditorPreferences.UpdateDisplayedVersion != Version)
                {
                    HVREditorPreferences.UpdateDisplayedVersion = Version;
                    HVRSetupWindow.ShowWindow();
                }
            }
        }
    }
}