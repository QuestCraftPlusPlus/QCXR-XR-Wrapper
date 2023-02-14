using System;
using HurricaneVR.Framework.Core.HandPoser;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using HurricaneVR.Framework.Shared.Utilities;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace HurricaneVR.Editor
{
    [CustomEditor(typeof(HVRPhysicsPoser))]
    public class HVRPhysicsPoserEditor : UnityEditor.Editor
    {
        public HVRPhysicsPoser PhysicsPoser;
        private VisualElement _editorContainer;
        private VisualElement _root;

        private void OnEnable()
        {
            try
            {
                PhysicsPoser = target as HVRPhysicsPoser;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public override VisualElement CreateInspectorGUI()
        {
            _root = new VisualElement();

            var defaultUI = new VisualElement();

            var iterator = serializedObject.GetIterator();
            if (iterator.NextVisible(true))
            {
                do
                {
                    var propertyField = new PropertyField(iterator.Copy()) { name = "PropertyField:" + iterator.propertyPath };

                    if (iterator.propertyPath == "m_Script" && serializedObject.targetObject != null)
                        propertyField.SetEnabled(value: false);

                    defaultUI.Add(propertyField);
                }
                while (iterator.NextVisible(false));
            }

            _root.Add(defaultUI);

            _editorContainer = new VisualElement();
            var editorTree = UnityEngine.Resources.Load<VisualTreeAsset>("HVRPhysicsPoserEditor");
            editorTree.CloneTree(_editorContainer);
            _root.Add(_editorContainer);

            _root.Q<Button>("SetupButton").clickable.clicked += () =>
            {
                if(PhysicsPoser.Validate()) PhysicsPoser.Setup();
                serializedObject.ApplyModifiedProperties();
            };

            _root.Q<Button>("ResetButton").clickable.clicked += () =>
            {
                PhysicsPoser.OpenFingers();
                PhysicsPoser.ResetHand();
            };
            _root.Q<Button>("SimulateButton").clickable.clicked += () =>
            {
                PhysicsPoser.transform.SetLayerRecursive(HVRLayers.Hand);
                PhysicsPoser.TestClose();
            };
            _root.Q<Button>("NextFingerButton").clickable.clicked += () =>
            {
                PhysicsPoser.NextFinger();
            };
            _root.Q<Button>("StepButton").clickable.clicked += () =>
            {
                PhysicsPoser.StepIteration();
            };
            _root.Q<Button>("StepBackButton").clickable.clicked += () =>
            {
                PhysicsPoser.BackStepIteration();
            };

            return _root;
        }
    }
}