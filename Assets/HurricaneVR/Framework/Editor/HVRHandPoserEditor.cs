using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.HandPoser;
using HurricaneVR.Framework.Core.HandPoser.Data;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using HurricaneVR.Framework.Shared.Utilities;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

#if UNITY_2021_2_OR_NEWER
using UnityEditor.SceneManagement;
#else
using UnityEditor.Experimental.SceneManagement;
#endif

namespace HurricaneVR.Editor
{
    [CustomEditor(typeof(HVRHandPoser))]
    public class HVRHandPoserEditor : UnityEditor.Editor
    {
        private SerializedProperty SP_LeftHandPreview;
        private SerializedProperty SP_RightHandPreview;
        private SerializedProperty SP_BodyPreview;
        private SerializedProperty SP_PreviewLeft;
        private SerializedProperty SP_PreviewRight;
        private SerializedProperty SP_LeftAutoPose;
        private SerializedProperty SP_RightAutoPose;

        private SerializedProperty SP_SelectionIndex;
        private SerializedProperty SP_PoseNames;


        private SerializedProperty SP_PrimaryPose;
        private SerializedProperty SP_Blends;

        private VisualElement _root;
        private VisualTreeAsset _tree;
        private HVRHandPoser Poser;

        private IntegerField _selectionIndexField;
        private string _leftInstanceId;
        private string _rightInstanceId;
        private string _bodyId;
        private HVRPhysicsPoser _leftPhysicsPoser;
        private HVRPhysicsPoser _rightPhysicsPoser;

        public ObjectField SelectedPoseField { get; set; }

        public ListView PosesListView { get; set; }
        public Toggle PreviewRightToggle { get; set; }

        public Toggle PreviewLeftToggle { get; set; }

        public bool FullBody => HVRSettings.Instance.InverseKinematics;

        protected int SelectedIndex
        {
            get => SP_SelectionIndex?.intValue ?? 0;
            set
            {
                if (SP_SelectionIndex != null) SP_SelectionIndex.intValue = value;
                serializedObject.ApplyModifiedProperties();
            }
        }

        public bool PreviewLeft => SP_PreviewLeft.boolValue;
        public bool PreviewRight => SP_PreviewRight.boolValue;

        protected GameObject BodyPreview
        {
            get
            {
                if (SP_BodyPreview == null || SP_BodyPreview.objectReferenceValue == null) return null;
                return SP_BodyPreview.objectReferenceValue as GameObject;
            }
            set
            {
                if (SP_BodyPreview != null) SP_BodyPreview.objectReferenceValue = value;
            }
        }

        protected GameObject LeftHandPreview
        {
            get
            {
                if (SP_LeftHandPreview == null || SP_LeftHandPreview.objectReferenceValue == null) return null;
                return SP_LeftHandPreview.objectReferenceValue as GameObject;
            }
            set
            {
                if (SP_LeftHandPreview != null) SP_LeftHandPreview.objectReferenceValue = value;
            }
        }

        protected GameObject RightHandPreview
        {
            get
            {
                if (SP_RightHandPreview == null || SP_RightHandPreview.objectReferenceValue == null) return null;
                return SP_RightHandPreview.objectReferenceValue as GameObject;
            }
            set
            {
                if (SP_RightHandPreview != null) SP_RightHandPreview.objectReferenceValue = value;
            }
        }

        private HVRHandPoseBlend PrimaryPose
        {
            get { return Poser.PrimaryPose; }
            set
            {
                Poser.PrimaryPose = value;
                serializedObject.ApplyModifiedProperties();
            }
        }

        public HVRHandPose SelectedPose
        {
            get { return SelectedBlendPose?.Pose; }
            set
            {
                if (SelectedBlendPose == null) return;
                SelectedBlendPose.Pose = value;
            }
        }

        public HVRHandPoseBlend SelectedBlendPose
        {
            get
            {
                if (CurrentPoseIndex <= PrimaryIndex) return Poser.PrimaryPose;
                return Poser.Blends[BlendIndex];
            }
        }

        public SerializedProperty SerializedSelectedPose
        {
            get
            {
                if (CurrentPoseIndex <= PrimaryIndex) return SP_PrimaryPose;
                return SP_Blends.GetArrayElementAtIndex(BlendIndex);
            }
        }

        public int PrimaryIndex => 0;

        public int CurrentPoseIndex
        {
            get => PosesListView?.selectedIndex ?? 0;
        }

        public int BlendIndex
        {
            get => CurrentPoseIndex - 1 - PrimaryIndex;
        }

        private static readonly Dictionary<string, PreviewState> _map = new Dictionary<string, PreviewState>();
        private static readonly List<string> cleanup = new List<string>();

        private class PreviewState
        {
            public Transform Target;
            public Transform SelectedBone;
            public int SelectedFinger;
            public HVRPosableHand Hand;

            public bool DrawRotation;


            public bool FullBody;
            public Transform Body;
            public bool IsLeft;
        }

        static HVRHandPoserEditor()
        {
            SceneView.duringSceneGui -= OnSceneGUI2;
            SceneView.duringSceneGui += OnSceneGUI2;
        }


        private static void OnSceneGUI2(SceneView view)
        {
            cleanup.Clear();

            foreach (var kvp in _map)
            {
                var state = kvp.Value;
                if (state.Target)
                {
                    if (!state.Hand)
                    {
                        if (state.FullBody)
                        {
                            if (state.Body) state.Hand = state.Body.GetComponentsInChildren<HVRPosableHand>().FirstOrDefault(e => e == state.IsLeft);
                            else
                            {
                                cleanup.Add(kvp.Key);
                                state.Target = null;
                            }
                        }
                        else
                        {
                            state.Target.TryGetComponent(out state.Hand);
                        }
                    }

                    DrawHandHandles(state.Target, ref state.DrawRotation);
                    SetupBoneButtons(state.Hand, ref state.SelectedBone, ref state.SelectedFinger);
                    DrawRotationHandle(state.SelectedBone);
                }
                else
                {
                    if (!Application.isPlaying) cleanup.Add(kvp.Key);
                }
            }

            foreach (var c in cleanup) _map.Remove(c);
        }

        private void GetHands(ref HVRPosableHand leftHand, ref HVRPosableHand rightHand)
        {
            leftHand = null;
            rightHand = null;

            if (FullBody)
            {
                if (BodyPreview)
                {
                    leftHand = BodyPreview.GetComponentsInChildren<HVRPosableHand>().FirstOrDefault(e => e.IsLeft);
                    rightHand = BodyPreview.GetComponentsInChildren<HVRPosableHand>().FirstOrDefault(e => e.IsRight);
                }
            }
            else
            {
                if (LeftHandPreview)
                {
                    leftHand = LeftHandPreview.GetComponent<HVRPosableHand>();
                }

                if (RightHandPreview)
                {
                    rightHand = RightHandPreview.GetComponent<HVRPosableHand>();
                }
            }
        }

        private void GetPhysicsPosers()
        {
            if (FullBody)
            {
                if (BodyPreview)
                {
                    if (!_leftPhysicsPoser)
                    {
                        _leftPhysicsPoser = BodyPreview.GetComponentsInChildren<HVRPhysicsPoser>().FirstOrDefault(e => e.Hand && e.Hand.IsLeft);
                    }

                    if (!_rightPhysicsPoser)
                    {
                        _rightPhysicsPoser = BodyPreview.GetComponentsInChildren<HVRPhysicsPoser>().FirstOrDefault(e => e.Hand && e.Hand.IsRight);
                    }
                }
                else
                {
                    _leftPhysicsPoser = null;
                    _rightPhysicsPoser = null;
                }
            }
            else
            {
                if (LeftHandPreview)
                {
                    if (!_leftPhysicsPoser)
                    {
                        _leftPhysicsPoser = LeftHandPreview.GetComponent<HVRPhysicsPoser>();
                    }
                }
                else
                {
                    _leftPhysicsPoser = null;
                }

                if (RightHandPreview)
                {
                    _rightPhysicsPoser = RightHandPreview.GetComponent<HVRPhysicsPoser>();
                }
                else
                {
                    _rightPhysicsPoser = null;
                }
            }

            if (_leftPhysicsPoser)
            {
                _leftPhysicsPoser.transform.SetLayerRecursive(HVRLayers.Hand);
                _leftPhysicsPoser.CurrentMask = ~LayerMask.GetMask("Hand");
                if (_leftPhysicsPoser.Validate()) _leftPhysicsPoser.Setup();
            }

            if (_rightPhysicsPoser)
            {
                _rightPhysicsPoser.transform.SetLayerRecursive(HVRLayers.Hand);
                _rightPhysicsPoser.CurrentMask = ~LayerMask.GetMask("Hand");
                if (_rightPhysicsPoser.Validate()) _rightPhysicsPoser.Setup();
            }
        }

        private static void DrawHandHandles(Transform handleTarget, ref bool drawRotation)
        {
            if (!handleTarget)
                return;


            Handles.color = Color.yellow;
            var capMethod = (Handles.CapFunction)Handles.CubeHandleCap;
            if (drawRotation)
            {
                capMethod = Handles.SphereHandleCap;
            }

            if (Handles.Button(handleTarget.position + new Vector3(0, 0.075f, 0.0f), Quaternion.identity, .01f, .01f, capMethod))
            {
                drawRotation = !drawRotation;
            }

            var offset = HVRSettings.Instance.HandPoseHandleOffset;

            if (!drawRotation)
            {
                var previous = handleTarget.position + offset;
                var newPosition = Handles.PositionHandle(previous, Quaternion.identity);
                if (newPosition != previous)
                {
                    Undo.RecordObject(handleTarget, "Hand Moved");
                    handleTarget.position = newPosition - offset;
                }
            }
            else
            {
                var newRotation = Handles.RotationHandle(handleTarget.rotation, handleTarget.position + offset);
                if (newRotation != handleTarget.rotation)
                {
                    Undo.RecordObject(handleTarget, "hand Rotated");
                    handleTarget.rotation = newRotation;
                }
            }
        }

        private static void DrawRotationHandle(Transform bone)
        {
            if (bone)
            {
                var newRotation = Handles.RotationHandle(bone.rotation, bone.position);
                if (newRotation != bone.rotation)
                {
                    Undo.RecordObject(bone, "Bone Rotated");
                    bone.rotation = newRotation;
                }
            }
        }

        private static void SetupBoneButtons(HVRPosableHand hand, ref Transform selectedBone, ref int selectedFinger)
        {
            if (hand)
            {
                for (var i = 0; i < hand.Fingers.Length; i++)
                {
                    Color color = Color.blue;
                    switch (i)
                    {
                        case 0:
                            color = Color.blue;
                            break;
                        case 1:
                            color = Color.green;
                            break;
                        case 2:
                            color = Color.red;
                            break;
                        case 3:
                            color = Color.cyan;
                            break;
                        case 4:
                            color = Color.magenta;
                            break;
                    }

                    color.a = .5f;
                    Handles.color = color;

                    var finger = hand.Fingers[i];
                    for (var j = 0; j < finger.Bones.Count; j++)
                    {
                        if (HVRSettings.Instance.PoserShowsOneFinger && i != selectedFinger && j > 0)
                        {
                            break;
                        }

                        var bone = finger.Bones[j];
                        if (Handles.Button(bone.Transform.position, bone.Transform.rotation, .005f, .001f, Handles.SphereHandleCap))
                        {
                            if (j == 0)
                            {
                                selectedFinger = i;
                            }

                            selectedBone = bone.Transform;
                        }
                    }
                }
            }
            else
            {
                selectedBone = null;
            }
        }

        private bool _inPrefabMode;
        private bool _active;
        
        private void OnEnable()
        {
            Poser = target as HVRHandPoser;

            _leftInstanceId = "LeftPreview_" + target.GetInstanceID();
            _rightInstanceId = "RightPreview_" + target.GetInstanceID();
            _bodyId = "Body_" + target.GetInstanceID();

            SP_LeftHandPreview = serializedObject.FindProperty("LeftHandPreview");
            SP_RightHandPreview = serializedObject.FindProperty("RightHandPreview");
            SP_BodyPreview = serializedObject.FindProperty("BodyPreview");

            SP_PrimaryPose = serializedObject.FindProperty("PrimaryPose");

            SP_Blends = serializedObject.FindProperty("Blends");

            SP_PreviewLeft = serializedObject.FindProperty("PreviewLeft");
            SP_PreviewRight = serializedObject.FindProperty("PreviewRight");

            SP_LeftAutoPose = serializedObject.FindProperty("LeftAutoPose");
            SP_RightAutoPose = serializedObject.FindProperty("RightAutoPose");

            SP_SelectionIndex = serializedObject.FindProperty("SelectionIndex");
            SP_PoseNames = serializedObject.FindProperty("PoseNames");

            _root = new VisualElement();
            _tree = UnityEngine.Resources.Load<VisualTreeAsset>("HVRHandPoserEditor");

            var stage = PrefabStageUtility.GetPrefabStage(Poser.gameObject);
            _inPrefabMode = stage != null;

            _active = true;
            var s = _root.schedule.Execute(EditorUpdate);
            s.Every(1000);
            s.Until(() => !_active);
        }

        private void EditorUpdate()
        {
            CheckRigidBody();
        }

        private void OnDisable()
        {
            _active = false;
        }

        private void CreatePoseIfNeeded()
        {
            if (PrimaryPose.Pose == null && HVRSettings.Instance.OpenHandPose)
            {
                _root.schedule.Execute(() =>
                {
                    PrimaryPose.SetDefaults();
                    serializedObject.ApplyModifiedProperties();
                });
                var poseProperty = SP_PrimaryPose.FindPropertyRelative("Pose");
                var clone = poseProperty.objectReferenceValue = HVRSettings.Instance.OpenHandPose.DeepCopy();
                clone.name = "Unsaved!";
                serializedObject.ApplyModifiedProperties();
            }
        }

        public override VisualElement CreateInspectorGUI()
        {
            _root.Clear();
            _tree.CloneTree(_root);

            CreatePoseIfNeeded();

            blendEditorRoot = _root.Q<BindableElement>("BlendEditorRoot");
            blendEditorRoot.Q<ObjectField>("Pose").objectType = typeof(HVRHandPose);

            _root.Add(blendEditorRoot);

            SetupAddButton();
            SetupDeleteButton();
            SetupSelectedPose(blendEditorRoot);
            SetupPosesListView();
            SetupNewButton();
            SetupSaveAsButton();
            SetupSaveButton();
            SetupMirrorButtons();
            SetupHandButtons();
            SetupAutoPoseButtons();

            //can't remember why I would add this field on the ui...
            //_selectionIndexField = new IntegerField("SelectedIndex");
            //_selectionIndexField.bindingPath = "SelectionIndex";
            //_selectionIndexField.RegisterValueChangedCallback(evt =>
            //{
            //    if (PosesListView.selectedIndex != evt.newValue) PosesListView.selectedIndex = evt.newValue;
            //});
            //_root.Add(_selectionIndexField);

            if (_inPrefabMode)
            {
                _root.Q("MirrorAxis").style.display = DisplayStyle.None;
                _root.Q("boxPreview").style.display = DisplayStyle.None;
            }
            else
            {
                _root.Q("warning").style.display = DisplayStyle.None;

                PreviewLeftToggle = _root.Q<Toggle>("PreviewLeft");
                PreviewLeftToggle.BindProperty(SP_PreviewLeft);
                PreviewRightToggle = _root.Q<Toggle>("PreviewRight");
                PreviewRightToggle.BindProperty(SP_PreviewRight);

                PreviewLeftToggle.RegisterValueChangedCallback(OnPreviewLeftChanged);
                PreviewRightToggle.RegisterValueChangedCallback(OnPreviewRightChanged);

                ToggleLeftAutoPose = _root.Q<Toggle>("LeftAutoPose");
         
                ToggleLeftAutoPose.BindProperty(SP_LeftAutoPose);
                ToggleRightAutoPose = _root.Q<Toggle>("RightAutoPose");
                ToggleRightAutoPose.BindProperty(SP_RightAutoPose);

                ToggleLeftAutoPose.RegisterValueChangedCallback(OnLeftAutoPoseChanged);
                ToggleRightAutoPose.RegisterValueChangedCallback(OnRightAutoPoseChanged);

                if (SelectedIndex >= PosesListView.itemsSource.Count + PrimaryIndex)
                {
                    Debug.Log($"Stored SelectedIndex is higher than pose count.");
                    SelectedIndex = PosesListView.itemsSource.Count - PrimaryIndex - 1;
                    serializedObject.ApplyModifiedProperties();
                }

                PosesListView.selectedIndex = SelectedIndex;


                GetPhysicsPosers();

                if (FullBody)
                {
                    var body = GameObject.Find(_bodyId);
                    if (body)
                    {
                        SP_BodyPreview.objectReferenceValue = body;
                    }

                    UpdateBodyPreview(SelectedPose != null ? SelectedPose.LeftHand : null, SelectedPose != null ? SelectedPose.RightHand : null, PreviewLeft, PreviewRight);
                }
                else
                {
                    SP_PreviewLeft.boolValue = SP_LeftHandPreview.objectReferenceValue != null;
                    SP_PreviewRight.boolValue = SP_RightHandPreview.objectReferenceValue != null;

                    if (!SP_PreviewLeft.boolValue)
                    {
                        FindPreviewHand(true, out var left);
                        if (left)
                        {
                            SP_PreviewLeft.boolValue = true;
                            SP_LeftHandPreview.objectReferenceValue = left;
                        }
                    }

                    if (!SP_PreviewRight.boolValue)
                    {
                        FindPreviewHand(false, out var right);
                        if (right)
                        {
                            SP_PreviewRight.boolValue = true;
                            SP_RightHandPreview.objectReferenceValue = right;
                        }
                    }

                    UpdatePreview(false, SP_PreviewRight.boolValue, SelectedPose != null ? SelectedPose.LeftHand : null);
                    UpdatePreview(true, SP_PreviewLeft.boolValue, SelectedPose != null ? SelectedPose.RightHand : null);
                }

                if (_leftPhysicsPoser)
                {
                    SP_LeftAutoPose.boolValue = _leftPhysicsPoser.LiveUpdate;
                }
                else
                {
                    SP_LeftAutoPose.boolValue = false;
                }

                if (_rightPhysicsPoser)
                {
                    SP_RightAutoPose.boolValue = _rightPhysicsPoser.LiveUpdate;
                }
                else
                {
                    SP_RightAutoPose.boolValue = false;
                }
            }


            serializedObject.ApplyModifiedProperties();

            return _root;
        }

        private void SetupAutoPoseButtons()
        {
            _root.Q<Button>("ButtonOpenLeft").clickable.clicked += () =>
            {
                GetPhysicsPosers();
                if (_leftPhysicsPoser)
                {
                    _leftPhysicsPoser.OpenFingers();
                    _leftPhysicsPoser.ResetHand();
                }
            };

            _root.Q<Button>("ButtonCloseLeft").clickable.clicked += () =>
            {
                GetPhysicsPosers();
                if (_leftPhysicsPoser)
                {
                    _leftPhysicsPoser.TestClose();
                }
            };

            _root.Q<Button>("ButtonOpenRight").clickable.clicked += () =>
            {
                GetPhysicsPosers();

                if (_rightPhysicsPoser)
                {
                    _rightPhysicsPoser.OpenFingers();
                    _rightPhysicsPoser.ResetHand();
                }
            };

            _root.Q<Button>("ButtonCloseRight").clickable.clicked += () =>
            {
                GetPhysicsPosers();
                if (_rightPhysicsPoser)
                {
                    _rightPhysicsPoser.TestClose();
                }
            };
        }


        public Toggle ToggleRightAutoPose { get; set; }

        public Toggle ToggleLeftAutoPose { get; set; }

        private void OnRightAutoPoseChanged(ChangeEvent<bool> evt)
        {
            GetPhysicsPosers();
            if (_rightPhysicsPoser)
            {
                if (evt.newValue && !_rightPhysicsPoser.Validate())
                {
                    return;
                }

                _rightPhysicsPoser.LiveUpdate = evt.newValue;
            }
        }

        private void OnLeftAutoPoseChanged(ChangeEvent<bool> evt)
        {
            GetPhysicsPosers();
            if (_leftPhysicsPoser)
            {
                if (evt.newValue && !_leftPhysicsPoser.Validate())
                {
                    return;
                }

                _leftPhysicsPoser.LiveUpdate = evt.newValue;
            }
        }

        private void OnPreviewRightChanged(ChangeEvent<bool> evt)
        {
            CreatePoseIfNeeded();

            if (FullBody)
            {
                UpdateBodyPreview(SelectedPose != null ? SelectedPose.LeftHand : null, SelectedPose != null ? SelectedPose.RightHand : null, PreviewLeft, evt.newValue);
                if (BodyPreview) RightPosableHand = BodyPreview.GetComponentsInChildren<HVRPosableHand>().FirstOrDefault(e => !e.IsLeft);
                else RightPosableHand = null;
            }
            else
            {
                //binding multiple editors to this object, they're bouncing off each other.
                if (evt.newValue && RightHandPreview)
                {
                    return;
                }

                if (!evt.newValue && RightHandPreview)
                {
                    DestroyImmediate(RightHandPreview);
                    RightHandPreview = null;
                    serializedObject.ApplyModifiedProperties();
                }
                else
                {
                    UpdatePreview(false, evt.newValue, SelectedPose != null ? SelectedPose.RightHand : null);
                }

                if (RightHandPreview)
                    RightPosableHand = RightHandPreview.GetComponent<HVRPosableHand>();
                else
                {
                    RightPosableHand = null;
                }
            }

            GetPhysicsPosers();
            if (_rightPhysicsPoser)
            {
                _rightPhysicsPoser.LiveUpdate = SP_RightAutoPose.boolValue;
            }
        }

        private void OnPreviewLeftChanged(ChangeEvent<bool> evt)
        {
            CreatePoseIfNeeded();

            if (FullBody)
            {
                UpdateBodyPreview(SelectedPose != null ? SelectedPose.LeftHand : null, SelectedPose != null ? SelectedPose.RightHand : null, evt.newValue, PreviewRight);
                if (BodyPreview) LeftPosableHand = BodyPreview.GetComponentsInChildren<HVRPosableHand>().FirstOrDefault(e => e.IsLeft);
                else LeftPosableHand = null;
            }
            else
            {
                //binding multiple editors to this object, they're bouncing off each other.
                if (evt.newValue && LeftHandPreview)
                {
                    return;
                }

                if (!evt.newValue && LeftHandPreview)
                {
                    DestroyImmediate(LeftHandPreview);
                    LeftHandPreview = null;
                    serializedObject.ApplyModifiedProperties();
                }
                else
                {
                    UpdatePreview(true, evt.newValue, SelectedPose != null ? SelectedPose.LeftHand : null);
                }

                if (LeftHandPreview)
                    LeftPosableHand = LeftHandPreview.GetComponent<HVRPosableHand>();
                else
                {
                    LeftPosableHand = null;
                }
            }

            GetPhysicsPosers();
            if (_leftPhysicsPoser)
            {
                _leftPhysicsPoser.LiveUpdate = SP_LeftAutoPose.boolValue;
            }
        }

        private void CheckRigidBody()
        {
            var g = Poser.GetComponentInParent<HVRGrabbable>();
            Rigidbody rb = null;
            if (g)
            {
                rb = g.gameObject.GetComponent<Rigidbody>();
            }

            _root.Q("lblAutoPoseWarning").style.display = rb ? DisplayStyle.Flex : DisplayStyle.None;
        }

        private void UpdateBodyPreview(HVRHandPoseData leftpose, HVRHandPoseData rightpose, bool previewLeft, bool previewRight, bool poseChanged = false)
        {
            if (!previewRight && !previewLeft)
            {
                if (BodyPreview)
                {
                    DestroyImmediate(BodyPreview);
                    BodyPreview = null;
                    serializedObject.ApplyModifiedProperties();
                    SetupIKTarget(previewLeft, "LeftIKTarget", out var dummy);
                    SetupIKTarget(previewRight, "RightIKTarget", out var dummy2);
                }

                return;
            }

            if (!BodyPreview)
            {
                var body = GameObject.Find(_bodyId);
                if (body)
                {
                    SP_BodyPreview.objectReferenceValue = body;
                }
                else
                {
                    BodyPreview = Instantiate(HVRSettings.Instance.FullBody, null, false);
                    BodyPreview.name = _bodyId;
                    BodyPreview.transform.position = Poser.transform.position;
                    BodyPreview.transform.position -= new Vector3(0, 0, .4f);
                    BodyPreview.transform.LookAt(Poser.transform.position, Vector3.up);
                    BodyPreview.transform.position -= new Vector3(0, 1.5f, 0);

                    if (!BodyPreview.TryGetComponent(out HVRIKTargets targets))
                    {
                        targets = BodyPreview.AddComponent<HVRIKTargets>();
                    }

                    targets.IsPoser = true;
                    targets.enabled = true;
                }
            }

            var ikTargets = BodyPreview.GetComponent<HVRIKTargets>();
            var hands = BodyPreview.GetComponentsInChildren<HVRPosableHand>();
            var leftHand = hands.FirstOrDefault(h => h.IsLeft);
            var rightHand = hands.FirstOrDefault(h => h.IsRight);

            _leftIKTarget = SetupIKTarget(previewLeft, "LeftIKTarget", out var leftExists);
            _rightIKTarget = SetupIKTarget(previewRight, "RightIKTarget", out var rightExists);

            if (_leftIKTarget)
            {
                if (!_map.TryGetValue(_leftInstanceId, out var state))
                {
                    state = new PreviewState();
                }

                _map[_leftInstanceId] = state;
                state.Body = BodyPreview.transform;
                state.Hand = leftHand;
                state.FullBody = true;
                state.IsLeft = true;
                state.Target = _leftIKTarget;

                ikTargets.LeftTarget = _leftIKTarget;

                if (leftHand && leftpose != null && (!leftExists || poseChanged))
                {
                    leftHand.Pose(leftpose, false);
                    _leftIKTarget.localPosition = leftpose.Position;
                    _leftIKTarget.localRotation = leftpose.Rotation;
                }
            }
            else
            {
                ikTargets.LeftTarget = BodyPreview.transform;
            }

            if (_rightIKTarget)
            {
                ikTargets.RightTarget = _rightIKTarget;

                if (!_map.TryGetValue(_rightInstanceId, out var state))
                {
                    state = new PreviewState() { Target = _rightIKTarget };
                }

                _map[_rightInstanceId] = state;
                state.Body = BodyPreview.transform;
                state.Hand = rightHand;
                state.FullBody = true;
                state.IsLeft = false;
                state.Target = _rightIKTarget;

                if (rightHand && rightpose != null && (!rightExists || poseChanged))
                {
                    rightHand.Pose(rightpose, false);
                    _rightIKTarget.localPosition = rightpose.Position;
                    _rightIKTarget.localRotation = rightpose.Rotation;
                }
            }
            else
            {
                ikTargets.RightTarget = BodyPreview.transform;
            }


            SceneView.RepaintAll();

            serializedObject.ApplyModifiedProperties();
        }

        private Transform SetupIKTarget(bool preview, string targetName, out bool exists)
        {
            var ikTarget = Poser.transform.Find(targetName);
            exists = ikTarget;
            if (preview && !ikTarget)
            {
                var go = new GameObject(targetName);
                go.transform.parent = Poser.transform;
                ikTarget = go.transform;
                ikTarget.ResetLocalProps();
            }
            else if (!preview && ikTarget)
            {
                DestroyImmediate(ikTarget.gameObject);
                return null;
            }

            return ikTarget;
        }

        private void UpdatePreview(bool isLeft, bool preview, HVRHandPoseData pose, bool poseChange = false)
        {
            var previewHandProperty = isLeft ? SP_LeftHandPreview : SP_RightHandPreview;
            var handPrefab = isLeft ? HVRSettings.Instance.LeftHand : HVRSettings.Instance.RightHand;

            if (!preview || !handPrefab)
            {
                return;
            }

            if (previewHandProperty.objectReferenceValue != null && !poseChange)
            {
                return;
            }

            var previewName = FindPreviewHand(isLeft, out var existing);

            //binding multiple editors to this object, they're bouncing off each other.
            //safety net
            if (existing)
            {
                if (isLeft && SP_LeftHandPreview.objectReferenceValue != existing)
                {
                    SP_LeftHandPreview.objectReferenceValue = existing;
                    serializedObject.ApplyModifiedProperties();
                }
                else if (!isLeft && SP_RightHandPreview.objectReferenceValue != existing)
                {
                    SP_RightHandPreview.objectReferenceValue = existing;
                    serializedObject.ApplyModifiedProperties();
                }

                if (!poseChange)
                    return;
            }

            var previewObj = previewHandProperty.objectReferenceValue as GameObject;
            if (!previewObj)
            {
                previewObj = Instantiate(handPrefab, Poser.transform, false);
                previewObj.name = previewName;
                previewHandProperty.objectReferenceValue = previewObj;

                if (!_map.TryGetValue(previewName, out var state))
                {
                    state = new PreviewState();
                }

                _map[previewName] = state;
                state.FullBody = false;
                state.Body = null;
                state.Target = previewObj.transform;
            }

            var hand = previewObj.GetComponent<HVRPosableHand>();
            serializedObject.ApplyModifiedProperties();
            if (hand != null && pose != null)
            {
                hand.Pose(pose);
                SceneView.RepaintAll();
            }
            else if (hand == null)
            {
                Debug.Log($"Preview hand is missing VRPosableHand");
            }
        }

        private void OnSelectedPoseChanged(ChangeEvent<Object> evt)
        {
            if (_inPrefabMode)
                return;

            if (evt.newValue != null)
            {
                var newPose = evt.newValue as HVRHandPose;
                if (FullBody)
                {
                    UpdateBodyPreview(newPose.LeftHand, newPose.RightHand, PreviewLeft, PreviewRight, true);
                }
                else
                {
                    UpdatePreview(true, PreviewLeftToggle.value, newPose.LeftHand, true);
                    UpdatePreview(false, PreviewRightToggle.value, newPose.RightHand, true);
                }
            }
            else
            {
                PreviewLeftToggle.SetValueWithoutNotify(false);
                PreviewRightToggle.SetValueWithoutNotify(false);

                if (LeftHandPreview)
                    DestroyImmediate(LeftHandPreview);
                if (RightHandPreview)
                    DestroyImmediate(RightHandPreview);
                if (BodyPreview)
                    DestroyImmediate(BodyPreview);
            }

            _root.schedule.Execute(PopulatePoses);
        }

        private void BindBlendContainer()
        {
            //for some reason binding to a custom serialized class doesn't update the binding paths

            var previousPath = blendEditorRoot.bindingPath;

            blendEditorRoot.Unbind();
            blendEditorRoot.bindingPath = SerializedSelectedPose.propertyPath;

            var newPath = SerializedSelectedPose.propertyPath;

            if (previousPath != null && previousPath != newPath)
            {
                //Debug.Log($"{previousPath} to {newPath}");
                FixPath(previousPath, newPath, blendEditorRoot);
            }

            blendEditorRoot.Bind(SerializedSelectedPose.serializedObject);
        }

        private void FixPath(string previous, string newPath, VisualElement element)
        {
            var bindable = element as IBindable;
            if (bindable != null)
            {
                if (bindable.bindingPath != null && bindable.bindingPath.StartsWith(previous))
                {
                    bindable.bindingPath = bindable.bindingPath.Replace(previous, newPath);
                }
            }

            VisualElement.Hierarchy hierarchy = element.hierarchy;
            int childCount = hierarchy.childCount;
            for (int index = 0; index < childCount; ++index)
            {
                hierarchy = element.hierarchy;
                FixPath(previous, newPath, hierarchy[index]);
            }
        }


        private void SetupDeleteButton()
        {
            var deleteButton = _root.Q<Button>("DeleteBlendPose");

            deleteButton.clickable.clicked += () =>
            {
                if (PosesListView.selectedIndex == PrimaryIndex)
                {
                    Debug.Log($"Cannot remove primary pose.");
                    return;
                }

                if (PosesListView.selectedIndex <= 0) return;
                //deleting an item with a reference doesn't actually delete it, it sets it to null
                //if (SP_Blends.GetArrayElementAtIndex(PosesListView.selectedIndex - 1).objectReferenceValue != null)
                //{
                //    SP_Blends.DeleteArrayElementAtIndex(PosesListView.selectedIndex - 1);
                //}

                var isLast = SP_Blends.arraySize == PosesListView.selectedIndex;
                var index = PosesListView.selectedIndex - 1;
                var nextIndex = PosesListView.selectedIndex - 1;

                SP_Blends.DeleteArrayElementAtIndex(index);
                serializedObject.ApplyModifiedProperties();

                PopulatePoses();

                if (isLast)
                {
                    //PosesListView.SetSelection(nextIndex);
                    PosesListView.selectedIndex = nextIndex;
                }
            };
        }


        private void SetupNewButton()
        {
            var button = _root.Q<Button>("NewPose");

            button.clickable.clicked += () => { SaveNew(); };
        }

        private void SaveNew()
        {
            var folder = HVRSettings.Instance.PosesDirectory;
            string path;

            if (string.IsNullOrWhiteSpace(folder))
            {
                path = EditorUtility.SaveFilePanelInProject("Save New Pose", "pose", "asset", "Message");
            }
            else
            {
                path = EditorUtility.SaveFilePanelInProject("Save New Pose", "pose", "asset", "Message", folder);
            }

            if (!string.IsNullOrEmpty(path))
            {
                HVRHandPose pose;

                HVRPosableHand leftHand = null;
                HVRPosableHand rightHand = null;

                GetHands(ref leftHand, ref rightHand);

                var doLeft = leftHand && PreviewLeft;
                var doRight = rightHand && PreviewRight;

                if (doLeft && !doRight)
                {
                    pose = leftHand.CreateFullHandPose(Poser.MirrorAxis, FullBody ? _leftIKTarget : null);
                }
                else if (doRight && !doLeft)
                {
                    pose = rightHand.CreateFullHandPose(Poser.MirrorAxis, FullBody ? _rightIKTarget : null);
                }
                else if (doRight && doLeft)
                {
                    pose = CreateInstance<HVRHandPose>();
                    pose.LeftHand = leftHand.CreateHandPose(FullBody ? _leftIKTarget : null);
                    pose.RightHand = rightHand.CreateHandPose(FullBody ? _rightIKTarget : null);
                }
                else
                {
                    EditorUtility.DisplayDialog("Error!", "Preview hands required.", "Ok!");
                    return;
                }

                SerializedSelectedPose.FindPropertyRelative("Pose").objectReferenceValue = AssetUtils.CreateOrReplaceAsset(pose, path);

                SelectedPose = pose;
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void SetupSaveAsButton()
        {
            var button = _root.Q<Button>("SaveAsPose");

            button.clickable.clicked += () =>
            {
                if (!SelectedPose)
                {
                    SaveNew();
                    return;
                }

                var folder = HVRSettings.Instance.PosesDirectory;
                string path;

                if (string.IsNullOrWhiteSpace(folder))
                {
                    path = EditorUtility.SaveFilePanelInProject("Save New Pose", "pose", "asset", "Message");
                }
                else
                {
                    path = EditorUtility.SaveFilePanelInProject("Save New Pose", "pose", "asset", "Message", folder);
                }

                if (!string.IsNullOrEmpty(path))
                {
                    var left = SelectedPose.LeftHand;
                    var right = SelectedPose.RightHand;

                    HVRPosableHand leftHand = null;
                    HVRPosableHand rightHand = null;

                    GetHands(ref leftHand, ref rightHand);

                    if (leftHand && PreviewLeft)
                    {
                        left = leftHand.CreateHandPose(FullBody ? _leftIKTarget : null);
                    }

                    if (rightHand && PreviewRight)
                    {
                        right = rightHand.CreateHandPose(FullBody ? _rightIKTarget : null);
                    }

                    var clone = Instantiate(SelectedPose);
                    clone.LeftHand = left;
                    clone.RightHand = right;

                    SerializedSelectedPose.FindPropertyRelative("Pose").objectReferenceValue = AssetUtils.CreateOrReplaceAsset(clone, path);

                    serializedObject.ApplyModifiedProperties();
                    //PopulatePoses();
                }
            };
        }

        private void SetupSaveButton()
        {
            var button = _root.Q<Button>("SavePose");

            button.clickable.clicked += () =>
            {
                if (!SelectedPose)
                {
                    SaveNew();
                    return;
                }

                HVRPosableHand leftHand = null;
                HVRPosableHand rightHand = null;

                GetHands(ref leftHand, ref rightHand);

                if (leftHand && PreviewLeft)
                {
                    var pose = leftHand.CreateHandPose(FullBody ? _leftIKTarget : null);
                    SelectedPose.LeftHand = pose;
                }

                if (rightHand && PreviewRight)
                {
                    var pose = rightHand.CreateHandPose(FullBody ? _rightIKTarget : null);
                    SelectedPose.RightHand = pose;
                }

                EditorUtility.SetDirty(SelectedPose);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            };
        }

        private void SetupAddButton()
        {
            var addNewButton = _root.Q<Button>("AddBlendPose");

            addNewButton.clickable.clicked += () =>
            {
                var i = SP_Blends.arraySize;
                SP_Blends.InsertArrayElementAtIndex(i);
                var test = SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("Pose");
                var clone = test.objectReferenceValue = PrimaryPose.Pose.DeepCopy();
                clone.name = "Unsaved!";

                SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("AnimationParameter").stringValue = HVRHandPoseBlend.DefaultParameter;
                SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("Weight").floatValue = 1f;
                SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("Speed").floatValue = 16f;

                SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("Type").enumValueIndex = (int)BlendType.Immediate;

                SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("ThumbType").enumValueIndex = (int)HVRSettings.Instance.ThumbCurlType;
                SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("IndexType").enumValueIndex = (int)HVRSettings.Instance.IndexCurlType;
                SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("MiddleType").enumValueIndex = (int)HVRSettings.Instance.MiddleCurlType;
                SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("RingType").enumValueIndex = (int)HVRSettings.Instance.RingCurlType;
                SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("PinkyType").enumValueIndex = (int)HVRSettings.Instance.PinkyCurlType;

                SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("ThumbStart").floatValue = HVRSettings.Instance.ThumbStart;
                SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("IndexStart").floatValue = HVRSettings.Instance.IndexStart;
                SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("MiddleStart").floatValue = HVRSettings.Instance.MiddleStart;
                SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("RingStart").floatValue = HVRSettings.Instance.RingStart;
                SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("PinkyStart").floatValue = HVRSettings.Instance.PinkyStart;

                serializedObject.ApplyModifiedProperties();
                PopulatePoses();
            };
        }

        private void SetupMirrorButtons()
        {
            var mirrorRight = _root.Q<Button>("ButtonMirrorRight");

            mirrorRight.clickable.clicked += () =>
            {
                if (FullBody)
                {
                    var hands = BodyPreview.GetComponentsInChildren<HVRPosableHand>();
                    var leftHand = hands.FirstOrDefault(h => h.IsLeft);
                    var rightHand = hands.FirstOrDefault(h => h.IsRight);

                    if (!leftHand || !rightHand || !_leftIKTarget || !_rightIKTarget)
                        return;

                    var right = leftHand.Mirror(Poser.MirrorAxis, _leftIKTarget);

                    Undo.RegisterFullObjectHierarchyUndo(rightHand.gameObject, "Mirror left to right");
                    rightHand.Pose(right, false);
                    if (HVRSettings.Instance.IKHandMirroring)
                    {
                        Undo.RegisterFullObjectHierarchyUndo(_rightIKTarget, "Mirror left to right");
                        _rightIKTarget.localRotation = right.Rotation;
                        _rightIKTarget.localPosition = right.Position;
                    }
                }
                else
                {
                    if (LeftHandPreview && RightHandPreview)
                    {
                        var leftHand = LeftHandPreview.GetComponent<HVRPosableHand>();
                        var rightHand = RightHandPreview.GetComponent<HVRPosableHand>();
                        var right = leftHand.Mirror(Poser.MirrorAxis);

                        Undo.RegisterFullObjectHierarchyUndo(RightHandPreview, "Mirror left to right");
                        rightHand.Pose(right);
                    }
                }
            };

            var mirrorLeft = _root.Q<Button>("ButtonMirrorLeft");

            mirrorLeft.clickable.clicked += () =>
            {
                if (FullBody)
                {
                    var hands = BodyPreview.GetComponentsInChildren<HVRPosableHand>();
                    var leftHand = hands.FirstOrDefault(h => h.IsLeft);
                    var rightHand = hands.FirstOrDefault(h => h.IsRight);

                    if (!leftHand || !rightHand || !_leftIKTarget || !_rightIKTarget)
                        return;

                    var pose = rightHand.Mirror(Poser.MirrorAxis, _rightIKTarget);

                    Undo.RegisterFullObjectHierarchyUndo(leftHand.gameObject, "Mirror right to left");
                    leftHand.Pose(pose, false);
                    if (HVRSettings.Instance.IKHandMirroring)
                    {
                        Undo.RegisterFullObjectHierarchyUndo(_leftIKTarget, "Mirror right to left");
                        _leftIKTarget.localPosition = pose.Position;
                        _leftIKTarget.localRotation = pose.Rotation;
                    }
                }
                else
                {
                    if (LeftHandPreview && RightHandPreview)
                    {
                        var leftHand = LeftHandPreview.GetComponent<HVRPosableHand>();
                        var rightHand = RightHandPreview.GetComponent<HVRPosableHand>();
                        var left = rightHand.Mirror(Poser.MirrorAxis);

                        Undo.RegisterFullObjectHierarchyUndo(LeftHandPreview, "Mirror right to left");
                        leftHand.Pose(left);
                    }
                }
            };
        }

        private void SetupHandButtons()
        {
            var focusLeft = _root.Q<Button>("ButtonFocusLeft");

            focusLeft.clickable.clicked += () =>
            {
                if (LeftHandPreview) Selection.activeGameObject = LeftHandPreview;
                else if (_leftIKTarget) Selection.activeGameObject = _leftIKTarget.gameObject;
            };

            var focusRight = _root.Q<Button>("ButtonFocusRight");

            focusRight.clickable.clicked += () =>
            {
                if (RightHandPreview) Selection.activeGameObject = RightHandPreview;
                else if (_rightIKTarget) Selection.activeGameObject = _rightIKTarget.gameObject;
            };

            var leftExpand = _root.Q<Button>("LeftExpand");

            leftExpand.clickable.clicked += () =>
            {
                if (LeftHandPreview) LeftHandPreview.SetExpandedRecursive(true);
                else if (LeftPosableHand) LeftPosableHand.gameObject.SetExpandedRecursive(true);
            };

            var leftCollapse = _root.Q<Button>("LeftCollapse");

            leftCollapse.clickable.clicked += () =>
            {
                if (LeftHandPreview) LeftHandPreview.SetExpandedRecursive(false);
                else if (LeftPosableHand) LeftPosableHand.gameObject.SetExpandedRecursive(false);
            };

            var rightExpand = _root.Q<Button>("RightExpand");

            rightExpand.clickable.clicked += () =>
            {
                if (RightHandPreview) RightHandPreview.SetExpandedRecursive(true);
                else if (RightPosableHand) RightPosableHand.gameObject.SetExpandedRecursive(true);
            };

            var rightCollapse = _root.Q<Button>("RightCollapse");

            rightCollapse.clickable.clicked += () =>
            {
                if (RightHandPreview) RightHandPreview.SetExpandedRecursive(false);
                else if (RightPosableHand) RightPosableHand.gameObject.SetExpandedRecursive(false);
            };
        }

        private void SetupSelectedPose(VisualElement container)
        {
            SelectedPoseField = container.Q<ObjectField>("Pose");
            SelectedPoseField.objectType = typeof(HVRHandPose);
            SelectedPoseField.bindingPath = "Pose";
            //SelectedPoseField.RegisterValueChangedCallback(OnSelectedPoseChanged);
            ////unity decide to fk with everything in 2020, hack to handle selected pose field from firing it's change event on enable that didn't happen in 2019..
            var schedule = container.schedule.Execute(() => { SelectedPoseField.RegisterValueChangedCallback(OnSelectedPoseChanged); });
            schedule.StartingIn(1000);
        }

        private void SetupPosesListView()
        {
            PosesListView = _root.Q<ListView>("Poses");
            PosesListView.itemsSource = Poser.PoseNames;
            PosesListView.makeItem = MakePoseListItem;
            PosesListView.bindItem = BindItem;
            PosesListView.selectionType = SelectionType.Single;

#if UNITY_2021_2_OR_NEWER
            PosesListView.fixedItemHeight = (int)EditorGUIUtility.singleLineHeight;
#else
            PosesListView.itemHeight = (int)EditorGUIUtility.singleLineHeight;
#endif

            PosesListView.style.height = EditorGUIUtility.singleLineHeight * 5;
            PopulatePoses();


#if UNITY_2021_1_OR_NEWER
            PosesListView.onSelectionChange += OnPoseSelectionChanged;
#else

            PosesListView.onSelectionChanged += OnPoseListIndexChanged;

#endif
        }

        private BindableElement blendEditorRoot;

        public HVRPosableHand LeftPosableHand;
        public HVRPosableHand RightPosableHand;
        private Transform _leftIKTarget;
        private Transform _rightIKTarget;

        private string FindPreviewHand(bool isLeft, out GameObject existing)
        {
            var previewName = isLeft ? _leftInstanceId : _rightInstanceId;
            existing = GameObject.Find(previewName);
            return previewName;
        }

        private void OnPoseSelectionChanged(IEnumerable<object> obj)
        {
            if (PosesListView.selectedIndex >= Poser.PoseNames.Count)
            {
                PosesListView.selectedIndex = Poser.PoseNames.Count - 1;
                return;
            }

            if (SelectedBlendPose != null && string.IsNullOrWhiteSpace(SelectedBlendPose.AnimationParameter))
            {
                SelectedBlendPose.AnimationParameter = "None";
            }

            SelectedIndex = PosesListView.selectedIndex;

            var poseName = SelectedPose == null ? "None" : SelectedPose.name;
            blendEditorRoot.Q<Label>("lblSelectedPose").text = $"Selected Pose: {poseName}";

            BindBlendContainer();
        }

        private void OnPoseListIndexChanged(List<object> p)
        {
            OnPoseSelectionChanged(p);
        }

        private void BindItem(VisualElement visual, int index)
        {
            var label = visual as Label;
            if (index == PrimaryIndex)
            {
                label.AddToClassList("primarypose");
            }

            if (index < Poser.PoseNames.Count)
                label.text = Poser.PoseNames[index];
        }

        private VisualElement MakePoseListItem()
        {
            return new Label();
        }

        public void PopulatePoses()
        {
            SP_PoseNames.ClearArray();

            var primaryName = PrimaryPose == null || PrimaryPose.Pose == null ? "Primary Not Set" : "Primary: " + PrimaryPose.Pose.name;
            SP_PoseNames.InsertArrayElementAtIndex(0);
            SP_PoseNames.GetArrayElementAtIndex(0).stringValue = primaryName;

            for (var i = 0; i < SP_Blends.arraySize; i++)
            {
                string poseName;
                var blendablePose = Poser.Blends[i];
                if (blendablePose == null || blendablePose.Pose == null)
                {
                    poseName = "Not Set";
                }
                else
                {
                    poseName = blendablePose.Pose.name;
                }

                SP_PoseNames.InsertArrayElementAtIndex(i + 1);
                SP_PoseNames.GetArrayElementAtIndex(i + 1).stringValue = poseName;
            }

            serializedObject.ApplyModifiedProperties();


#if UNITY_2021_2_OR_NEWER
            PosesListView?.Rebuild();
#else
            PosesListView?.Refresh();
#endif
        }
    }

    //[Serializable]
    //public class State
    //{
    //    //  [SerializeField]
    //    protected GameObject LeftHandPreview;

    //    // [SerializeField]
    //    protected GameObject RightHandPreview;

    //    // [SerializeField]
    //    protected GameObject BodyPreview;

    //    //  [SerializeField]
    //    protected bool PreviewLeft;

    //    //  [SerializeField]
    //    protected bool PreviewRight;

    //    //  [SerializeField]
    //    protected bool LeftAutoPose;

    //    //  [SerializeField]
    //    protected bool RightAutoPose;

    //    // [SerializeField]
    //    protected int SelectionIndex;

    //    // [SerializeField]
    //    public List<string> PoseNames = new List<string>();
    //}
}