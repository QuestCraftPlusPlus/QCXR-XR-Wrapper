using HurricaneVR.Framework.Core.Sockets;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    public class HVRSocketableTagDropDown : EditorWindow
    {
        private HVRSocketableTag _tag;
        private HVRSocketableTags _tags;
        private SerializedObject _o;

        public void Setup(SerializedObject o, HVRSocketableTag tag, HVRSocketableTags tags)
        {
            _tag = tag;
            _tags = tags;
            _o = o;
        }

        public Vector2 GetWindowSize(Rect controlRect)
        {
            int h = Mathf.Min(_tags.GetCount(), 32);

            float height = 18 * h + 38;
            return new Vector2(controlRect.width, height);
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            var updated = false;
            foreach (var tag in _tags.GetNames())
            {
                if (string.IsNullOrWhiteSpace(tag))
                    continue;

                var index = _tags.GetIndex(tag);
                var hasBit = _tag[index];

                EditorGUI.BeginChangeCheck();
                EditorGUILayout.BeginHorizontal();

                var toggled = GUILayout.Toggle(hasBit, new GUIContent(tag));

                _tag[index] = toggled;


                EditorGUILayout.EndHorizontal();

                if (EditorGUI.EndChangeCheck())
                {
                    updated = true;
                }
            }

            if (updated)
            {
                EditorUtility.SetDirty(_o.targetObject);
            }
                    

            EditorGUILayout.EndVertical();
        }
    }
}