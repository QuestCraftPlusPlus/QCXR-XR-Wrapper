#if UNITY_EDITOR
using UnityEditor;

namespace HurricaneVR.Framework.Core.Utils
{

    public static class AssetUtils
    {
        public static T CreateOrReplaceAsset<T>(T asset, string path) where T : UnityEngine.Object
        {
            var existingAsset = AssetDatabase.LoadAssetAtPath<T>(path);

            if (existingAsset == null)
            {
                AssetDatabase.CreateAsset(asset, path);
                existingAsset = asset;
            }
            else
            {
                EditorUtility.CopySerialized(asset, existingAsset);
                AssetDatabase.SaveAssets();
            }

            //this changes name to existing asset in case of saved clones.
            AssetDatabase.Refresh();

            return existingAsset;
        }
    }


}
#endif