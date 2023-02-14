using System;
using HurricaneVR.Framework.Core;
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
    //[CustomEditor(typeof(HVRPosableHand))]
    //public class HVRPosableHandEditor : UnityEditor.Editor
    //{
    //    private SerializedProperty SP_IsLeft;
    //    private SerializedProperty SP_HandPreview;
    //    private SerializedProperty SP_DoPreview;
    //    private SerializedProperty SP_SelectedPose;

    //    private PropertyField PropertyField_IsLeft;

    //    private VisualElement HandContainer;

    //    public HVRPosableHand ThisHand;
    //    private VisualElement _editorContainer;
    //    private VisualElement _root;

    //    public ObjectField SelectedPoseField { get; set; }

    //    public bool IsLeft => SP_IsLeft.boolValue;

    //    protected GameObject PreviewInstance
    //    {
    //        get
    //        {
    //            if (SP_HandPreview == null || SP_HandPreview.objectReferenceValue == null) return null;
    //            return SP_HandPreview.objectReferenceValue as GameObject;
    //        }
    //        set
    //        {
    //            if (SP_HandPreview != null) SP_HandPreview.objectReferenceValue = value;
    //        }
    //    }

    //    public HVRHandPose SelectedPose
    //    {
    //        get
    //        {
    //            return SP_SelectedPose.objectReferenceValue as HVRHandPose;
    //        }
    //        set
    //        {
    //            if (SP_SelectedPose == null) return;
    //            SP_SelectedPose.objectReferenceValue = value;
    //        }
    //    }

    //    private void OnEnable()
    //    {
    //        try
    //        {
    //            SP_IsLeft = serializedObject.FindProperty("IsLeft");
    //            SP_HandPreview = serializedObject.FindProperty("Preview");
    //            SP_DoPreview = serializedObject.FindProperty("DoPreview");
    //            SP_SelectedPose = serializedObject.FindProperty("SelectedPose");
    //            ThisHand = target as HVRPosableHand;
    //        }
    //        catch (Exception e)
    //        {
    //            Debug.LogException(e);
    //        }
    //    }

    //    public bool FullBody => HVRSettings.Instance.InverseKinematics;

    //    public override VisualElement CreateInspectorGUI()
    //    {
    //        _root = new VisualElement();
    //        var defaultUI = new VisualElement();

    //        var iterator = serializedObject.GetIterator();
    //        if (iterator.NextVisible(true))
    //        {
    //            do
    //            {
    //                var propertyField = new PropertyField(iterator.Copy()) { name = "PropertyField:" + iterator.propertyPath };

    //                if (iterator.propertyPath == "m_Script" && serializedObject.targetObject != null)
    //                    propertyField.SetEnabled(value: false);

    //                defaultUI.Add(propertyField);
    //            }
    //            while (iterator.NextVisible(false));
    //        }

    //        var folder = new Foldout();
    //        folder.Add(defaultUI);
    //        folder.text = "Default Inspector";
    //        _root.Add(folder);
    //        _editorContainer = new VisualElement();
    //        var editorTree = UnityEngine.Resources.Load<VisualTreeAsset>("HVRHandPoseEditor");
    //        editorTree.CloneTree(_editorContainer);
    //        _editorContainer.Q<ObjectField>("SelectedPose").objectType = typeof(HVRHandPose);
    //        _root.Add(_editorContainer);

    //        PropertyField_IsLeft = _root.Q<PropertyField>("PropertyField:IsLeft");
    //        PropertyField_IsLeft.RegisterCallback(new EventCallback<ChangeEvent<bool>>(OnIsLeftChanged));


    //        HandContainer = _editorContainer.Q<VisualElement>("HandContainer");

    //        PreviewToggle = HandContainer.Q<Toggle>("Preview");
    //        PreviewToggle.BindProperty(SP_DoPreview);
    //        PreviewToggle.RegisterValueChangedCallback(OnPreviewChanged);

    //        UpdatePreview(SP_DoPreview.boolValue, true);

    //        SetupSelectedPose();
    //        SetupNewButton();
    //        SetupSaveAsButton();
    //        SetupSaveButton();
    //        SetupPreviewButton();

    //        return _root;
    //    }

    //    private void SetupNewButton()
    //    {
    //        var button = _root.Q<Button>("NewPose");

    //        button.clickable.clicked += () =>
    //        {
    //            var folder = HVRSettings.Instance.PosesDirectory;
    //            string path;

    //            if (string.IsNullOrWhiteSpace(folder))
    //            {
    //                path = EditorUtility.SaveFilePanelInProject("Save New Pose", "pose", "asset", "Message");
    //            }
    //            else
    //            {
    //                path = EditorUtility.SaveFilePanelInProject("Save New Pose", "pose", "asset", "Message", folder);
    //            }

    //            if (!string.IsNullOrEmpty(path))
    //            {
    //                var pose = ThisHand.CreateFullHandPose();
    //                AssetUtils.CreateOrReplaceAsset(pose, path);

    //                SP_SelectedPose.objectReferenceValue = pose;
    //                serializedObject.ApplyModifiedProperties();
    //            }
    //        };
    //    }

    //    private void SetupSaveAsButton()
    //    {
    //        var button = _root.Q<Button>("SaveAsPose");

    //        button.clickable.clicked += () =>
    //        {
    //            var folder = HVRSettings.Instance.PosesDirectory;
    //            string path;

    //            if (string.IsNullOrWhiteSpace(folder))
    //            {
    //                path = EditorUtility.SaveFilePanelInProject("Save New Pose", "pose", "asset", "Message");
    //            }
    //            else
    //            {
    //                path = EditorUtility.SaveFilePanelInProject("Save New Pose", "pose", "asset", "Message", folder);
    //            }

    //            if (!string.IsNullOrEmpty(path))
    //            {
    //                var pose = ThisHand.CreateFullHandPose();
    //                SelectedPose = AssetUtils.CreateOrReplaceAsset(pose, path);

    //                serializedObject.ApplyModifiedProperties();
    //            }
    //        };
    //    }

    //    private void SetupPreviewButton()
    //    {
    //        var button = _root.Q<Button>("Preview");

    //        button.clickable.clicked += () =>
    //        {
    //            if (!SelectedPose)
    //                return;

    //            Undo.RegisterFullObjectHierarchyUndo(ThisHand.gameObject, "PreviewPose");
    //            ThisHand.Pose(SelectedPose.GetPose(IsLeft));
    //            UpdatePreview(SP_DoPreview.boolValue, false);
    //        };
    //    }

    //    private void SetupSaveButton()
    //    {
    //        var button = _root.Q<Button>("SavePose");

    //        button.clickable.clicked += () =>
    //        {
    //            if (IsLeft)
    //            {
    //                SelectedPose.LeftHand = ThisHand.CreateHandPose();
    //                SelectedPose.RightHand = ThisHand.Mirror(ThisHand.MirrorAxis);
    //            }
    //            else
    //            {
    //                SelectedPose.RightHand = ThisHand.CreateHandPose();
    //                SelectedPose.LeftHand = ThisHand.Mirror(ThisHand.MirrorAxis);
    //            }

    //            EditorUtility.SetDirty(SelectedPose);
    //            AssetDatabase.SaveAssets();
    //            AssetDatabase.Refresh();
    //        };
    //    }

    //    private void SetupSelectedPose()
    //    {
    //        SelectedPoseField = _editorContainer.Q<ObjectField>("SelectedPose");
    //        SelectedPoseField.objectType = typeof(HVRHandPose);
    //        SelectedPoseField.RegisterValueChangedCallback(OnSelectedPoseChanged);
    //    }

    //    private void TrySetToDefault()
    //    {
    //        if (!ThisHand || !HVRSettings.Instance.OpenHandPose)
    //            return;

    //        ThisHand.Pose(HVRSettings.Instance.OpenHandPose);
    //    }

    //    private void OnSelectedPoseChanged(ChangeEvent<UnityEngine.Object> evt)
    //    {
    //        //if (Application.isPlaying)
    //        //    return;

    //        //if (evt.newValue == null)
    //        //{
    //        //    PreviewToggle.SetValueWithoutNotify(false);
    //        //    if (PreviewInstance) DestroyImmediate(PreviewInstance);

    //        //    _root.schedule.Execute(() => { TrySetToDefault(); });
    //        //}
    //        //else
    //        //{
    //        //    var newPose = evt.newValue as HVRHandPose;
    //        //    ThisHand.Pose(newPose.GetPose(IsLeft));
    //        //    UpdatePreview(SP_DoPreview.boolValue, false);
    //        //}
    //    }

    //    private void OnPreviewChanged(ChangeEvent<bool> evt)
    //    {
    //        UpdatePreview(evt.newValue, false);
    //    }

    //    public Toggle PreviewToggle { get; set; }

    //    private void OnIsLeftChanged(ChangeEvent<bool> evt)
    //    {
    //    }

    //    private void UpdatePreview(bool preview, bool isRetained)
    //    {
    //        if (PreviewInstance && !isRetained)
    //        {
    //            DestroyImmediate(PreviewInstance);
    //            serializedObject.ApplyModifiedProperties();
    //        }

    //        if (!preview)
    //        {
    //            return;
    //        }

    //        if (isRetained && SP_HandPreview.objectReferenceValue != null) return;

    //        var source = IsLeft ? HVRSettings.Instance.RightHand : HVRSettings.Instance.LeftHand;

    //        var previewName = $"Mirror [{ThisHand.gameObject.name}]";

    //        var existing = GameObject.Find(previewName);

    //        if (existing)
    //        {
    //            PreviewInstance = existing;
    //        }
    //        else
    //        {
    //            PreviewInstance = Instantiate(source, ThisHand.transform.parent, false);
    //            PreviewInstance.name = previewName;
    //        }


    //        var hand = PreviewInstance.GetComponent<HVRPosableHand>();
    //        hand.DoPreview = false;
    //        hand.IsLeft = !ThisHand.IsLeft;
    //        serializedObject.ApplyModifiedProperties();
    //        if (hand != null)
    //        {
    //            var mirror = ThisHand.Mirror(MirrorAxis.X);
    //            var snapShot = ThisHand.CreateHandPose();
    //            //var mirror = snapShot.Mirror(ThisHand.MirrorAxis, ThisHand.transform);
    //            //hand.FingerSetup();
    //            hand.Pose(mirror);
    //            SceneView.RepaintAll();
    //        }
    //        else
    //        {
    //            Debug.Log($"Preview hand is missing VRPosableHand");
    //        }
    //    }

    //    private void OnDisable()
    //    {

    //    }
    //}
}