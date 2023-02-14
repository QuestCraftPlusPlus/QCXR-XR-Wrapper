using System.IO;
using UnityEngine;

namespace YVR.Core.XR
{
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    public sealed class YVRPlatformSetting : ScriptableObject
    {
        [SerializeField]
        public string appID;

        private static YVRPlatformSetting instance;
        public static YVRPlatformSetting Instance
        {
            get
            {
                if (instance == null)
                {

                    instance = Resources.Load<YVRPlatformSetting>("YVRPlatformSetting");
#if UNITY_EDITOR
                    string path = Application.dataPath + "/XR/Resources";
                    if (!Directory.Exists(path))
                    {
                        UnityEditor.AssetDatabase.CreateFolder("Assets/XR", "Resources");
                    }

                    if (instance == null)
                    {
                        instance = CreateInstance<YVRPlatformSetting>();
                        UnityEditor.AssetDatabase.CreateAsset(instance, "Assets/XR/Resources/YVRPlatformSetting.asset");
                    }
#endif
                }

                return instance;
            }
        }
    }
}


