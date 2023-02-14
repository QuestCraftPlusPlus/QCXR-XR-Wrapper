using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.HandPoser;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using HurricaneVR.Framework.Shared.Utilities;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using GameObject = UnityEngine.GameObject;


namespace HurricaneVR.Editor
{
    public class HVRManagementWindow : EditorWindow
    {
        private ObjectField grabbableObjectField;
        private ObjectField posableObjectField;
        private ObjectField grabPointsObjectField;
        private UnityEditor.Editor _poserEditor;
        private VisualElement _poserRoot;
        private TextField _recordedPrefixTextBox;
        private VisualElement _root;
        private ScrollView _scrollView;

        [MenuItem("Tools/HurricaneVR/Posing")]
        public static void ShowExample()
        {
            HVRManagementWindow wnd = GetWindow<HVRManagementWindow>();
            wnd.titleContent = new GUIContent("HVR Posing");
        }

        public const string GrabbleName = "VRPoser_Grabbable";

        public GameObject SelectedGrabPoints
        {
            get
            {
                var obj = grabPointsObjectField.value as GameObject;
                return obj;
            }
        }

        public GameObject SelectedPoser
        {
            get
            {
                return posableObjectField.value as GameObject;
            }
        }

        public GameObject SelectedGrabbable
        {
            get
            {
                return grabbableObjectField.value as GameObject;
            }
        }

        private void OnDestroy()
        {

        }

        public void OnEnable()
        {
            _root = rootVisualElement;
            var visualTree = UnityEngine.Resources.Load<VisualTreeAsset>("HVRManagementWindow");
            _root.Add(visualTree.CloneTree());

            _scrollView =  _root.Q<ScrollView>("ScrollView"); 

            SetupObjectFields(_root);
            SetupFocusButtons(_root);
            SetupAddPoser(_root);
            SetupPoseSelected(_root);

            var list = _root.Q<VisualElement>("RecordedPoses");
            list.styleSheets.Add(Resources.Load<StyleSheet>("HVRManagementWindow"));
            _recordedPrefixTextBox = _root.Q<TextField>("RecordedPrefixTextBox");

            SetupButtons(_root);
        }

        public ListView PosesListView { get; set; }
        private List<HVRHandPose> Poses { get; set; }

        private void SetupPosesListView(List<HVRHandPose> poses)
        {
            var posesParent = GameObject.Find("RecordedPosesCloneHolder");
            if (!posesParent)
            {
                posesParent = new GameObject("RecordedPosesCloneHolder");
            }

            foreach (Transform child in posesParent.transform)
            {
                DestroyImmediate(child.gameObject);
            }

            Poses = poses;
            var listview = _root.Q<ScrollView>("RecordedPoses");
            listview.Clear();

            foreach (var pose in poses)
            {
                var row = new VisualElement();
                row.AddToClassList("poserow");

                var field = new ObjectField("Pose:");
                field.objectType = typeof(HVRHandPose);
                field.value = pose;
                row.Add(field);
                field.AddToClassList("poserow-field");

                var clone = Instantiate(HVRSettings.Instance.GetPoserHand(pose.SnappedLeft ? HVRHandSide.Left : HVRHandSide.Right), posesParent.transform, true);

                var posableHand = clone.GetComponent<HVRPosableHand>();
                if (posableHand != null)
                {
                    posableHand.Pose(pose);
                }

                var attach = new Button(() =>
                {
                    if (!SelectedGrabPoints)
                    {
                        EditorUtility.DisplayDialog("Error!", "Please set GrabPoints field.", "Ok!");
                        return;
                    }

                    var grabPoint = new GameObject("GrabPoint");
                    grabPoint.transform.parent = SelectedGrabPoints.transform;
                    grabPoint.transform.localPosition = Vector3.zero;
                    var poser = grabPoint.AddComponent<HVRHandPoser>();
                    var posable = grabPoint.AddComponent<HVRPosableGrabPoint>();
                    posable.HandPoser = poser;
                    grabPoint.transform.localPosition = Vector3.zero;//.position = pose.SnappedLeft ? pose.LeftHand.Position : pose.RightHand.Position;
                    grabPoint.transform.localRotation = Quaternion.identity;

                    var clonedPose = posableHand.CreateFullHandPose(posableHand.MirrorAxis);

                    clonedPose.RightHand.Rotation = Quaternion.Inverse(grabPoint.transform.rotation) * clonedPose.RightHand.Rotation;
                    clonedPose.LeftHand.Rotation = Quaternion.Inverse(grabPoint.transform.rotation) * clonedPose.LeftHand.Rotation;

                    clonedPose.RightHand.Position = Vector3.zero;
                    clonedPose.LeftHand.Position = Vector3.zero;

                    poser.PrimaryPose = new HVRHandPoseBlend();
                    poser.PrimaryPose.Pose = clonedPose;
                    poser.PrimaryPose.SetDefaults();

                    var grabbable = grabPoint.GetComponentInParent<HVRGrabbable>();
                    string name;
                    if (grabbable)
                    {
                        name = grabbable.name + "-" + pose.name;
                    }
                    else
                    {
                        name = DateTime.Now.Ticks.ToString();
                    }

                    poser.PrimaryPose.Pose = HVRSettings.Instance.SavePoseToDefault(clonedPose, name, "");


                    //EditorUtility.SetDirty(pose);
                    //AssetDatabase.SaveAssets();
                    //AssetDatabase.Refresh();

                })
                {
                    text = "Attach"
                };

                var delete = new Button(() =>
                {
                    row.RemoveFromHierarchy();
                    DestroyImmediate(clone);
                })
                { text = "-" };

                var focus = new Button(() =>
                {
                    Selection.activeGameObject = clone;
                    SceneView.FrameLastActiveSceneView();

                })
                { text = "Focus" };

                attach.AddToClassList("poserow-button");
                delete.AddToClassList("poserow-button");
                focus.AddToClassList("poserow-button");

                row.Add(focus);
                row.Add(attach);
                row.Add(delete);

                listview.Insert(listview.childCount, row);


            }

        }


        private void SetupButtons(VisualElement root)
        {
            var button = root.Q<Button>("SetGrabPoints");
            button.clickable.clicked += () =>
            {
                if (Selection.activeGameObject) grabPointsObjectField.value = Selection.activeGameObject;
            };

            button = root.Q<Button>("SetGrabbable");
            button.clickable.clicked += () =>
            {
                if (Selection.activeGameObject) grabbableObjectField.value = Selection.activeGameObject;
            };

            button = root.Q<Button>("ExpandGrabbable");
            button.clickable.clicked += () =>
            {
                if (SelectedGrabbable) SelectedGrabbable.SetExpandedRecursive(true);
            };

            button = root.Q<Button>("CollapseGrabbable");
            button.clickable.clicked += () =>
            {
                if (SelectedGrabbable) SelectedGrabbable.SetExpandedRecursive(false);
            };

            button = root.Q<Button>("ExpandGrabPoints");
            button.clickable.clicked += () =>
            {
                if (SelectedGrabPoints) SelectedGrabPoints.SetExpandedRecursive(true);
            };

            button = root.Q<Button>("CollapseGrabPoints");
            button.clickable.clicked += () =>
            {
                if (SelectedGrabPoints) SelectedGrabPoints.SetExpandedRecursive(false);
            };

            root.Q<Button>("SetupGrabbable").clickable.clicked += () =>
            {
                if (SelectedGrabbable == null || SelectedGrabbable != Selection.activeGameObject)
                {
                    grabbableObjectField.value = Selection.activeGameObject;
                    AddGrabbable(Selection.activeGameObject);
                }
                else
                {
                    AddGrabbable(SelectedGrabbable);
                }
            };

            root.Q<Button>("AddGrabPoints").clickable.clicked += () =>
            {
                AddGrabPoints(SelectedGrabbable ? SelectedGrabbable : Selection.activeGameObject);
            };

            root.Q<Button>("AddGrabPointButton").clickable.clicked += () =>
            {
                if (!SelectedGrabPoints) return;
                AddGrabPoint(SelectedGrabPoints);
                SelectedGrabPoints.SetExpandedRecursive(true);
            };

            root.Q<Button>("SetupGrabPoints").clickable.clicked += () =>
            {
                if (!SelectedGrabbable) return;
                var grabbable = SelectedGrabbable.GetComponent<HVRGrabbable>();
                if (grabbable != null) grabbable.PopulateGrabPoints();
                EditorUtility.SetDirty(grabbable);
            };

            root.Q<Button>("LoadPoses").clickable.clicked += () =>
            {
                var path = EditorUtility.OpenFolderPanel("Load poses", HVRSettings.Instance.GetRuntimePosesDirectory(), "");

                if (string.IsNullOrWhiteSpace(path))
                {
                    return;
                }

                var files = Directory.GetFiles(path);

                var poses = new List<HVRHandPose>();


                foreach (var file in files)
                {
                    try
                    {

                        if (file.EndsWith(".asset"))
                        {
                            var start = file.IndexOf("Assets");
                            var localPath = file.Substring(start).Replace("\\", "/");
                            var pose = AssetDatabase.LoadAssetAtPath<HVRHandPose>(localPath);
                            poses.Add(pose);
                        }


                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }

                SetupPosesListView(poses);

            };


        }

        public Toggle ToggleSteam { get; set; }

        public Toggle ToggleOculus { get; set; }

        private void AddGrabbable(GameObject obj)
        {
            if (!obj) return;

            var grabbable = obj.GetComponent<HVRGrabbable>();
            var physicsGrabbable = obj.GetComponent<HVRPhysicsGrabbable>();
            if (!grabbable && !physicsGrabbable)
            {
                obj.AddComponent<HVRGrabbable>();
            }

            if (AddGrabPoints(obj))
            {
                AddGrabPoint(SelectedGrabPoints);
            }

            obj.SetExpandedRecursive(true);
        }

        private GameObject AddGrabPoint(GameObject obj)
        {
            var grabPoint = new GameObject("GrabPoint");
            grabPoint.transform.parent = obj.transform;
            grabPoint.transform.ResetLocalProps();
            var poser = grabPoint.AddComponent<HVRHandPoser>();
            var posable = grabPoint.AddComponent<HVRPosableGrabPoint>();
            posable.HandPoser = poser;

            posableObjectField.value = grabPoint;

            return grabPoint;
        }

        private bool AddGrabPoints(GameObject obj)
        {
            if (!obj)
                return false;

            var points = obj.GetComponentInChildren<HVRGrabPoints>()?.gameObject;

            if (points)
            {
                if (!EditorUtility.DisplayDialog("Warning!", "GrabPoint component exists in children, add another?", "Yes", "No"))
                {
                    Debug.Log($"This object already has a child VRGrabPoints");
                    grabPointsObjectField.value = points;
                    return false;
                }
            }

            points = new GameObject("GrabPoints");
            var vrGrabPoints = points.AddComponent<HVRGrabPoints>();
            points.transform.parent = obj.transform;
            points.transform.ResetLocalProps();
            //vrGrabPoints.AddRunTimePoses();
            //AddGrabPoint(points);

            grabPointsObjectField.value = points;
            obj.SetExpandedRecursive(true);
            return true;
        }

        private void SetupObjectFields(VisualElement root)
        {
            posableObjectField = root.Q<ObjectField>("VRHandPoser");
            posableObjectField.objectType = typeof(GameObject);

            posableObjectField.RegisterValueChangedCallback(evt =>
            {
                if (evt.newValue)
                {
                    var obj = evt.newValue as GameObject;
                    var poser = obj.GetComponent<HVRHandPoser>();
                    if (poser != null)
                    {
                        SetupPoser(root, poser);
                    }
                    else
                    {
                        CleanupPoserEditor();
                    }
                }
                else
                {
                    CleanupPoserEditor();
                }
            });

            grabbableObjectField = root.Q<ObjectField>("Grabbable");
            grabbableObjectField.objectType = typeof(GameObject);

            grabbableObjectField.RegisterValueChangedCallback(evt =>
            {
            });

            if (posableObjectField.value)
            {
                var obj = posableObjectField.value as GameObject;

                var poser = obj.GetComponent<HVRHandPoser>();
                if (poser != null)
                {
                    SetupPoser(root, poser);
                }
            }

            grabPointsObjectField = root.Q<ObjectField>("GrabPointsObject");
            grabPointsObjectField.objectType = typeof(GameObject);
        }

        private void SetupPoser(VisualElement root, HVRHandPoser poser)
        {
            CleanupPoserEditor();

            _poserEditor = UnityEditor.Editor.CreateEditor(poser);
            _poserRoot = _poserEditor.CreateInspectorGUI();
            //root.Add(_poserRoot);
            _scrollView.Add(_poserRoot);
            _poserRoot.Unbind();
            _poserRoot.Bind(_poserEditor.serializedObject);
        }

        private void CleanupPoserEditor()
        {
            if (_poserRoot != null)
            {
                _poserRoot.Clear();
                _poserRoot.RemoveFromHierarchy();
            }

            if (_poserEditor != null)
            {
                DestroyImmediate(_poserEditor);
            }
        }

        private void SetupAddPoser(VisualElement root)
        {
            var button = root.Q<ToolbarButton>("AddPoser");
            button.clickable.clicked += () =>
            {
                if (!SelectedPoser) return;

                var poser = SelectedPoser.GetComponent<HVRHandPoser>();
                if (poser == null)
                {
                    poser = SelectedPoser.AddComponent<HVRHandPoser>();
                    SetupPoser(root, poser);
                }

            };
        }

        private void SetupFocusButtons(VisualElement root)
        {
            root.Q<Button>("FocusPoser").clickable.clicked += () =>
            {
                if (SelectedPoser) Selection.activeGameObject = SelectedPoser;
            };

            root.Q<Button>("FocusGrabbable").clickable.clicked += () =>
            {
                if (SelectedGrabbable) Selection.activeGameObject = SelectedGrabbable;
            };

            root.Q<Button>("FocusGrabPoints").clickable.clicked += () =>
            {
                if (SelectedGrabPoints) Selection.activeGameObject = SelectedGrabPoints;
            };
        }

        private void SetupPoseSelected(VisualElement root)
        {
            var button = root.Q<Button>("SetPoser");
            button.clickable.clicked += () =>
            {
                if (Selection.activeGameObject != null) posableObjectField.value = Selection.activeGameObject;
            };
        }

        private void OnDisable()
        {
            CleanupPoserEditor();
        }
    }
}