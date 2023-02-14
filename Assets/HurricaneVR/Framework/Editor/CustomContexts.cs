using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Core.Utils;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    public class CustomContexts
    {
        [MenuItem("Assets/HVR/Convert to Strength", false, 1)]
        private static void StrengthConvert(MenuCommand command)
        {
            foreach (var o in Selection.objects)
            {
                var path = AssetDatabase.GetAssetPath(o);
                var js = AssetDatabase.LoadAssetAtPath<HVRJointSettings>(path);
                if (!js)
                    continue;

                var fileName = path.Replace(".asset", "");
                fileName += "_Strength.asset";

                var s = AssetDatabase.LoadAssetAtPath<PDStrength>(fileName);
                if (!s)
                {
                    s = ScriptableObject.CreateInstance<PDStrength>();
                }

                s.Mode = js.ApplyMode;
                s.Spring = js.XDrive.Spring;
                s.Damper = js.XDrive.Damper;
                s.MaxForce =  js.XDrive.MaxForce;
                s.TorqueSpring = js.SlerpDrive.Spring;
                s.TorqueDamper = js.SlerpDrive.Damper;
                s.MaxTorque = js.SlerpDrive.MaxForce;

                AssetUtils.CreateOrReplaceAsset(s, fileName);
            }
        }

        [MenuItem("Assets/HVR/Convert to Strength", true, 1)]
        private static bool StrengthConvertValidation()
        {
            foreach (var o in Selection.objects)
            {
                var path = AssetDatabase.GetAssetPath(o);
                var js = AssetDatabase.LoadAssetAtPath<HVRJointSettings>(path);
                if (!js)
                    return false;
            }
        
            return Selection.objects.Length > 0;
        }
    }
}