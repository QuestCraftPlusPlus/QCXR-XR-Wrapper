using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace HurricaneVR.Editor
{
    //this file merely exists because unity packages  appear to add only and this trash is no good at all

    //[CustomEditor(typeof(Object), true, isFallback = true)]
    //public class DefaultEditor : UnityEditor.Editor
    //{
    //    public override VisualElement CreateInspectorGUI()
    //    {
    //        var container = new VisualElement();

    //        var iterator = serializedObject.GetIterator();
    //        if (iterator.NextVisible(true))
    //        {
    //            do
    //            {
    //                var propertyField = new PropertyField(iterator.Copy()) { name = "PropertyField:" + iterator.propertyPath };

    //                if (iterator.propertyPath == "m_Script" && serializedObject.targetObject != null)
    //                    propertyField.SetEnabled(value: false);

    //                container.Add(propertyField);
    //            }
    //            while (iterator.NextVisible(false));
    //        }

    //        return container;
    //    }
    //}
}