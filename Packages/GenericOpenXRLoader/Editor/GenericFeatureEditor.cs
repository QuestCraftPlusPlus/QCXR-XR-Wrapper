using UnityEditor;

namespace Zappar.OpenXR.GenericAndroidLoader.Editor
{
    [CustomEditor(typeof(GenericFeature))]
    public class GenericFeatureEditor : UnityEditor.Editor
    {
        private SerializedProperty enableTracker;

        private void OnEnable()
        {
            enableTracker = serializedObject.FindProperty(nameof(GenericFeature.EnableTracker));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(enableTracker);

            serializedObject.ApplyModifiedProperties();
        }
    }
}