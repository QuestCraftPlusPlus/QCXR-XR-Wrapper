using HurricaneVR.Framework.Core.Utils;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    [CustomPropertyDrawer(typeof(EmbeddedAttribute), true)]
    public class EmbeddedAssetDrawer : PropertyDrawer
    {
        private EmbeddeAssetEditor _editor;

        public EmbeddeAssetEditor Editor
        {
            get
            {
                if (_editor == null)
                    _editor = new EmbeddeAssetEditor();
                return _editor;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var a = attribute as EmbeddedAttribute;

            Editor.DrawEditorCombo(position, fieldInfo.FieldType, label, property, $"Save {property.displayName}.", $"{property.displayName.Replace(" ", "_")}", "asset", string.Empty);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded || property.objectReferenceValue == null)
            {
                return EditorGUIUtility.singleLineHeight;
            }
            
            float height = base.GetPropertyHeight(property, label) + EditorGUIUtility.singleLineHeight;
            var so = new SerializedObject(property.objectReferenceValue);
            var prop = so.GetIterator();
            prop.NextVisible(true);
            while (prop.NextVisible(true))
            {
                if (prop.name == "m_Script")
                    continue;
                height += EditorGUIUtility.singleLineHeight;
            }
            return height * 1.2f;
        }
    }
}