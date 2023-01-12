using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


#if USE_URP
[CustomEditor(typeof(OverdrawRendererData))]
public class OverDrawRendererDataEditor : UnityEditor.Rendering.Universal.ScriptableRendererDataEditor
{
    public override void OnInspectorGUI()
    {

        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_overdrawOpaqueMaterial"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_overdrawTransparentMaterial"));
        serializedObject.ApplyModifiedProperties();
        base.OnInspectorGUI();
    }
}
#endif