using System;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

namespace HurricaneVR.Editor
{
    /// <summary>
    /// Helper for drawing embedded asset editors
    /// </summary>
    public class EmbeddeAssetEditor
    {
        /// <summary>
        /// Free the resources in OnDisable()
        /// </summary>
        public void OnDisable()
        {
            DestroyEditor();
        }

        private readonly GUIContent m_CreateButtonGUIContent = new GUIContent("Create Asset", "Create new asset.");

        UnityEditor.Editor m_Editor = null;
        const int kIndentOffset = 3;
        private Type _type;

        public void DrawEditorCombo(Rect position, Type type, GUIContent label, SerializedProperty property, string title, string defaultName, string extension, string message)
        {
            _type = type;
            DrawEditorCombo(position, property, title, defaultName, extension, message, false);
        }

        /// <summary>
        /// Call this from OnInspectorGUI.  Will draw the asset reference field, and
        /// the embedded editor, or a Create Asset button, if no asset is set.
        /// </summary>
        private void DrawEditorCombo(Rect position, SerializedProperty property,
            string title, string defaultName, string extension, string message,
            bool indent)
        {
            UpdateEditor(property);
            if (!m_Editor)
            {
                AssetFieldWithCreateButton(position, property, title, defaultName, extension, message);
                return;
            }

            var rect = position;
            var propRect = rect;
            propRect.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(propRect, property);

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
                UpdateEditor(property);
            }

            if (m_Editor)
            {
                Rect foldoutRect = new Rect(rect.x - kIndentOffset, rect.y, rect.width + kIndentOffset, EditorGUIUtility.singleLineHeight);
                property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, GUIContent.none, true);

                bool canEditAsset = AssetDatabase.IsOpenForEdit(m_Editor.target, StatusQueryOptions.UseCachedIfPossible);
                GUI.enabled = canEditAsset;
                if (property.isExpanded)
                {
                    position.y += EditorGUIUtility.singleLineHeight;
                    var box = position;
                    box.height *= .9f;
                    EditorGUI.HelpBox(box, "CTRL+S to persist asset changes.", MessageType.None);
                    position.y += EditorGUIUtility.singleLineHeight;
                    EditorGUI.BeginChangeCheck();
                    var so = new SerializedObject(property.objectReferenceValue);
                    var prop = so.GetIterator();

                    while (prop.NextVisible(true))
                    {
                        if (prop.name == "m_Script")
                            continue;
                        position.height = EditorGUIUtility.singleLineHeight;
                        EditorGUI.PropertyField(position, prop);
                        position.y += EditorGUIUtility.singleLineHeight;
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        so.ApplyModifiedProperties();
                    }
                }

                GUI.enabled = true;
                if (m_Editor.target != null)
                {
                    if (!canEditAsset && GUILayout.Button("Check out"))
                    {
                        Task task = Provider.Checkout(AssetDatabase.GetAssetPath(m_Editor.target), CheckoutMode.Asset);
                        task.Wait();
                    }
                }
            }
        }

        void AssetFieldWithCreateButton(Rect position, SerializedProperty property,
            string title, string defaultName, string extension, string message)
        {
            EditorGUI.BeginChangeCheck();

            float hSpace = 5;
            float buttonWidth = GUI.skin.button.CalcSize(m_CreateButtonGUIContent).x;

            var r = position;
            r.width -= buttonWidth + hSpace;
            EditorGUI.PropertyField(r, property);
            r.x += r.width + hSpace;
            r.width = buttonWidth;
            if (GUI.Button(r, m_CreateButtonGUIContent))
            {
                string newAssetPath = EditorUtility.SaveFilePanelInProject(
                    title, defaultName, extension, message);
                if (!string.IsNullOrEmpty(newAssetPath))
                {
                    var asset = CreateAt(_type, newAssetPath);
                    property.objectReferenceValue = asset;
                    property.serializedObject.ApplyModifiedProperties();
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
                UpdateEditor(property);
            }
        }

        void DestroyEditor()
        {
            if (m_Editor != null)
            {
                UnityEngine.Object.DestroyImmediate(m_Editor);
                m_Editor = null;
            }
        }

        void UpdateEditor(SerializedProperty property)
        {
            property.serializedObject.ApplyModifiedProperties();

            var target = property.objectReferenceValue;
            if (m_Editor && m_Editor.target != target)
            {
                DestroyEditor();
            }

            if (target != null)
            {
                if (!m_Editor)
                {
                    m_Editor = UnityEditor.Editor.CreateEditor(target);
                }
            }
        }

        public static ScriptableObject CreateAt(Type assetType, string assetPath)
        {
            ScriptableObject asset = ScriptableObject.CreateInstance(assetType);
            if (!asset)
            {
                Debug.LogError("Failed to create instance of " + assetType.Name + " at " + assetPath);
                return null;
            }

            AssetDatabase.CreateAsset(asset, assetPath);
            return asset;
        }
    }
}