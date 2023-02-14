using HurricaneVR.Framework.Core.Sockets;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    [CustomPropertyDrawer(typeof(HVRSocketableTag))]
    public class HVRSocketableTagDrawer : PropertyDrawer
    {
        private HVRSocketableTags _tags;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var tagsProperty = property.FindPropertyRelative("Tags");

            position.height = 16;
            tagsProperty.objectReferenceValue = EditorGUI.ObjectField(position, tagsProperty.objectReferenceValue, typeof(HVRSocketableTags), false);

            position.y += 18;
            position.height = 16;

            var tag = fieldInfo.GetValue(property.serializedObject.targetObject) as HVRSocketableTag;
            var tags = tagsProperty.objectReferenceValue as HVRSocketableTags;
            
            if (tags != null)
            {
                if (GUI.Button(position, tag.DisplayName, EditorStyles.layerMaskField))
                {
                    var pop = ScriptableObject.CreateInstance<HVRSocketableTagDropDown>();
                    pop.Setup(property.serializedObject, tag, tags);

                    var popPosition = position;
                    popPosition.position = GUIUtility.GUIToScreenPoint(position.position);
                    
                    pop.ShowAsDropDown(popPosition, pop.GetWindowSize(position));
                }
            }

            if (tags != _tags)
            {
                OnTagsChanged(tags);
            }

            EditorGUI.EndProperty();
        }

        private void OnTagsChanged(HVRSocketableTags tags)
        {
            _tags = tags;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 36;
        }
    }
}