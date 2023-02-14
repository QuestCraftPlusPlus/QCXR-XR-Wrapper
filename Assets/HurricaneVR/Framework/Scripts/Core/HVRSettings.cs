using System;
using System.IO;
using HurricaneVR.Framework.Core.HandPoser;
using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Core.Sockets;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HurricaneVR.Framework.Core
{
    public class HVRSettings : ScriptableObject
    {
        private const string HurricaneVRUploader = "HurricaneVRUploader";

        private static HVRSettings _instance;

        public static HVRSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<HVRSettings>(HandPoserSettings);

                    //Asset doesn't exist, create it
                    if (_instance == null)
                    {
                        _instance = HVRSettings.CreateInstance<HVRSettings>();

#if UNITY_EDITOR
                        _instance.Setup(_instance);
                        if (Application.productName != HurricaneVRUploader)
                        {
                            _instance.AddAssetToResource(_instance, HandPoserSettingsFileName);
                        }
#endif
                    }
                }
#if UNITY_EDITOR
                if (!_instance.LineGrabSettings)
                {
                    _instance.LineGrabSettings = FindJointSettings("HVR_LineGrabSettings");
                }
#endif

                return _instance;
            }
        }

        [Header("Directories")]

        public string LocalEditorRootDirectory;
        public string LocalRootDirectory;
        public string LocalResourcesDirectory;
        public string LocalRuntimePosesDirectory;
        public string LocalPosesDirectory;



        public const string HandPoserSettings = "HVRSettings";
        public const string HandPoserSettingsFileName = HandPoserSettings + ".asset";

        public const string DefaultOpenHand = "OculusCustomHandOpen";

        public const string DefaultLeftHand = "HVR_left_hand";
        public const string DefaultRightHand = "HVR_right_hand";

        public const string RuntimePoses = "RuntimePoses";

        [Header("Misc Settings")]
        public bool AutoApplyGrabbableLayer = true;

        [Tooltip("Tag Filter / Socketables will populate with this scriptable object if assigned.")]
        public HVRSocketableTags DefaultSocketableTags;

        [Header("Pose Settings")]

        public GameObject LeftHand;
        public GameObject RightHand;
        public bool InverseKinematics;
        public bool IKHandMirroring = true;
        public GameObject FullBody;
        public Vector3 HandPoseHandleOffset = new Vector3(0f, .08f, .0f);
        public HVRHandPose OpenHandPose;
        public bool PoserShowsOneFinger;

        [Header("Hand Poser Finger Curl Defaults")]
        public HVRFingerType ThumbCurlType = HVRFingerType.Static;
        public HVRFingerType IndexCurlType = HVRFingerType.Static;
        public HVRFingerType MiddleCurlType = HVRFingerType.Static;
        public HVRFingerType RingCurlType = HVRFingerType.Static;
        public HVRFingerType PinkyCurlType = HVRFingerType.Static;

        [Range(0f, 1f)] public float ThumbStart = 1f;
        [Range(0f, 1f)] public float IndexStart = 1f;
        [Range(0f, 1f)] public float MiddleStart = 1f;
        [Range(0f, 1f)] public float RingStart = 1f;
        [Range(0f, 1f)] public float PinkyStart = 1f;

        [Header("Grab Detection")]
        public bool UseAttachedRigidBody;
        public bool ComponentInParentFallback;

        [Header("Grab Settings")]
        [Tooltip("If true then holding trigger loosens the line grab without releasing grip")]
        public bool LineGrabTriggerLoose;

        [Header("Joint Setting Defaults")]
        [Tooltip("Default joint settings when grabbing an object.")]
        public HVRJointSettings DefaultJointSettings;

        public HVRJointSettings LineGrabSettings;


        [Header("Debugging")]
        public bool VerboseGrabbableEvents;
        public bool VerboseHandGrabberEvents;
        public bool DisableHaptics;

        public GameObject GetPoserHand(HVRHandSide side)
        {
            if (side == HVRHandSide.Left) return LeftHand;
            return RightHand;
        }
#if UNITY_EDITOR

        public string PosesDirectory => GetPosesDirectory();

        private void Setup(HVRSettings settings)
        {
            try
            {
                TryFindRoot();
                TryFindResources();

                if (!DefaultJointSettings)
                {
                    DefaultJointSettings = FindJointSettings("HVR_GrabbableSettings");
                }

                LineGrabSettings = FindJointSettings("HVR_LineGrabSettings");

            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            TryCreateRuntimePoseFolder();

            SetupDefaultHands(settings);
        }

        private static void SetupDefaultHands(HVRSettings settings)
        {
            try
            {
                if (settings.LeftHand == null)
                {
                    settings.LeftHand = FindPrefab(DefaultLeftHand);
                }

                if (settings.RightHand == null)
                {
                    settings.RightHand = FindPrefab(DefaultRightHand);
                }

                //if (settings.OpenHandPose == null)
                //{
                //    settings.OpenHandPose = FindAsset<HVRHandPose>(DefaultOpenHand);
                //}
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void TryFindRoot()
        {
            try
            {
                var frameworkDir = GetRootFrameworkDirectory();

                System.IO.DirectoryInfo assetsDirectoryInfo = new DirectoryInfo(Application.dataPath);
                LocalRootDirectory = frameworkDir.Substring(assetsDirectoryInfo.Parent.FullName.Length + 1);
                LocalEditorRootDirectory = LocalRootDirectory + "Editor" + Path.DirectorySeparatorChar;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public string GetRootFrameworkDirectory()
        {
            var root = ScriptableObject.CreateInstance<HVRRootFinder>();
            var rootScript = UnityEditor.MonoScript.FromScriptableObject(root);
            var rootPath = UnityEditor.AssetDatabase.GetAssetPath(rootScript);
            var rootFileInfo = new FileInfo(rootPath);

            return rootFileInfo.Directory.FullName.Replace($"HurricaneVR{Path.DirectorySeparatorChar}Framework{Path.DirectorySeparatorChar}Scripts",
                $"HurricaneVR{Path.DirectorySeparatorChar}Framework{Path.DirectorySeparatorChar}");
        }

        private string GetResourcesDirectory()
        {
            var root = GetRootFrameworkDirectory();
            var path = Path.Combine(root, "Resources");
            return path;
        }


        private string GetDefaultRuntimePosesDirectory()
        {
            return Path.Combine(GetResourcesDirectory(), RuntimePoses);
        }

        public string GetRuntimePosesDirectory()
        {
            if (string.IsNullOrWhiteSpace(LocalRuntimePosesDirectory))
            {
                Debug.LogWarning($"LocalRunTimePosesDirectory is blank.");
                return null;
            }

            if (LocalRuntimePosesDirectory.StartsWith("Assets\\") || LocalRuntimePosesDirectory.StartsWith("Assets/"))
            {
                return Application.dataPath + LocalRuntimePosesDirectory.Substring(6);
            }

            Debug.LogWarning($"LocalRunTimePosesDirectory is blank.");

            return GetDefaultRuntimePosesDirectory();
        }

        public string GetPosesDirectory()
        {
            if (string.IsNullOrWhiteSpace(LocalPosesDirectory))
            {
                Debug.LogWarning($"LocalPosesDirectory is blank.");
                return null;
            }


            if (LocalPosesDirectory.StartsWith("Assets\\") || LocalPosesDirectory.StartsWith("Assets/"))
            {
                return Application.dataPath + LocalPosesDirectory.Substring(6);
            }

            Debug.LogWarning($"LocalPosesDirectory is blank.");

            return null;
        }

        private void TryFindResources()
        {
            try
            {
                var path = GetResourcesDirectory();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                LocalResourcesDirectory = LocalRootDirectory + "Resources" + Path.DirectorySeparatorChar;
                //Debug.Log($"HVRSettings.ResourcesPath={ResourcesDirectory}");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
      

        private void TryCreateRuntimePoseFolder()
        {
            try
            {
                if (string.IsNullOrEmpty(LocalResourcesDirectory)) return;

                if (string.IsNullOrEmpty(LocalRuntimePosesDirectory))
                {
                    LocalRuntimePosesDirectory = LocalResourcesDirectory + "RuntimePoses" + Path.DirectorySeparatorChar;
                }

                Directory.CreateDirectory(GetDefaultRuntimePosesDirectory());
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        [InspectorButton("ShowPosesFolderChooser")]
        public string ChosePosesDirectory = "Choose Pose Directory";

        [InspectorButton("ShowRuntimePosesFolderChooser", 300)]
        public string ChoseRunTimePosesDirectory = "Choose RunTime Poses Directory";

        public void ShowPosesFolderChooser()
        {
            var PosesDirectory = EditorUtility.OpenFolderPanel("Choose Pose Directory", null, null);
            LocalPosesDirectory = PosesDirectory.Substring(Application.dataPath.IndexOf("Assets"));
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void ShowRuntimePosesFolderChooser()
        {
            var RuntimePosesDirectory = EditorUtility.OpenFolderPanel("Choose Pose Directory", null, null);
            LocalRuntimePosesDirectory = RuntimePosesDirectory.Substring(Application.dataPath.IndexOf("Assets"));
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
      

        [InspectorButton("ReloadGlobalsMethod")]
        public string ReloadGlobals = "Reload Globals";
        public void ReloadGlobalsMethod()
        {
            if (Instance)
            {
                Setup(Instance);
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }


        public HVRHandPose SaveRunTimePose(HVRHandPose pose, string fileName, string directory = null)
        {
            try
            {
                var RuntimePosesDirectory = GetRuntimePosesDirectory();
                if (RuntimePosesDirectory == null)
                {
                    return null;
                }

                if (!string.IsNullOrWhiteSpace(directory))
                {
                    var folder = Path.Combine(RuntimePosesDirectory, directory);
                    Directory.CreateDirectory(folder);
                }

                if (!fileName.EndsWith(".asset"))
                {
                    fileName += ".asset";
                }

                string path;
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    path = Path.Combine(Path.Combine(LocalRuntimePosesDirectory, directory), fileName);
                }
                else
                {
                    path = Path.Combine(LocalRuntimePosesDirectory, fileName);
                }

                pose = AssetUtils.CreateOrReplaceAsset(pose, path);
                Debug.Log($"Saved {fileName} to {LocalRuntimePosesDirectory}");
                return pose;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return null;
        }

        public HVRHandPose SavePoseToDefault(HVRHandPose pose, string fileName, string directory = null)
        {
            try
            {
                var PosesDirectory = GetPosesDirectory();

                if (string.IsNullOrWhiteSpace(PosesDirectory))
                {
                    //Debug.Log($"Setup PosesDirectory and LocalPosesDirectory.");
                    return SaveRunTimePose(pose, fileName, null);
                }

                if (!string.IsNullOrWhiteSpace(directory))
                {
                    var folder = Path.Combine(PosesDirectory, directory);
                    Directory.CreateDirectory(folder);
                }

                if (!fileName.EndsWith(".asset"))
                {
                    fileName += ".asset";
                }

                string path;
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    path = Path.Combine(Path.Combine(LocalPosesDirectory, directory), fileName);
                }
                else
                {
                    path = Path.Combine(LocalPosesDirectory, fileName);
                }

                return AssetUtils.CreateOrReplaceAsset(pose, path);
                //Debug.Log($"Saved {fileName} to {LocalPosesDirectory}");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return null;
        }

        public void AddAssetToResource<T>(T asset, string name) where T : Object
        {
            try
            {
                if (!name.EndsWith(".asset"))
                {
                    name += ".asset";
                }

                var path = Path.Combine(LocalResourcesDirectory, name);
                AssetUtils.CreateOrReplaceAsset<T>(asset, path);

                //Debug.Log($"Saved {name} to {LocalResourcesDirectory}");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static TAssetType FindAsset<TAssetType>(string name) where TAssetType : UnityEngine.Object
        {

            string[] defaultPaths = UnityEditor.AssetDatabase.FindAssets(name);
            if (defaultPaths != null && defaultPaths.Length > 0)
            {
                string defaultGUID = defaultPaths[0];
                string defaultPath = UnityEditor.AssetDatabase.GUIDToAssetPath(defaultGUID);
                var defaultAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TAssetType>(defaultPath);

                if (defaultAsset == null)
                    Debug.LogError($"Unable to find asset of {name}. Found path: " + defaultPath);

                return defaultAsset;
            }

            return null;
        }

        public static HVRJointSettings FindJointSettings(string name)
        {
            return FindAsset<HVRJointSettings>($"t:hvrjointsettings {name}");
        }

        public static GameObject FindPrefab(string name)
        {
            return FindAsset<GameObject>(string.Format("t:Prefab {0}", name));
        }
#endif
        public void OnValidate()
        {

        }
    }
}
