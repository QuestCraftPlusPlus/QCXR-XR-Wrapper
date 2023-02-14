using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HurricaneVR.Editor;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.HandPoser;
using HurricaneVR.Framework.Core.Player;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HurricaneVR
{
    public class HVRHandSetup : EditorWindow
    {
        public Transform Left;
        public Transform Right;
        public Transform RigRoot;

        public Transform LeftFingerParent;
        public Transform RightFingerParent;

        public List<FingerSetting> settings = new List<FingerSetting>();

        [MenuItem("Tools/HurricaneVR/Custom Hand Model Helper")]
        public static void ShowWindow()
        {
            var wnd = GetWindow<HVRHandSetup>();
            wnd.titleContent = new GUIContent("Hand Setup");
        }

        private void OnEnable()
        {
            _mirrorText = Mirroring ? "Stop Mirroring" : "Start Mirroring";
            SceneView.duringSceneGui += OnSceneGui;

            settings.Add(new FingerSetting() { BoneCount = 3, Finger = Finger.Index, Index = 0 });
            settings.Add(new FingerSetting() { BoneCount = 3, Finger = Finger.Middle, Index = 1 });
            settings.Add(new FingerSetting() { BoneCount = 3, Finger = Finger.Pinky, Index = 2 });
            settings.Add(new FingerSetting() { BoneCount = 3, Finger = Finger.Ring, Index = 3 });
            settings.Add(new FingerSetting() { BoneCount = 3, Finger = Finger.Thumb, Index = 4 });
        }

        private void OnDisable()
        {
            UnsubSceneGui();
        }

        private bool _mirroring => Mirroring;
        private string _mirrorText = "Mirror";

        private GUIStyle helpBoxStyle;

        public bool HandsSetup;
        public bool MirrorDetected;
        public bool RigSetup;
        public bool PosesAssigned;
        public bool ColliderSetup;

        public bool Mirroring;

        public HVRPosableHand RightHand;
        public HVRPosableHand LeftHand;

        public HVRPhysicsPoser LeftPhysics;
        public HVRPhysicsPoser RightPhysics;

        public HVRHandPoser LeftPoser;
        public HVRHandPoser RightPoser;

        public HVRHandPose OpenPose;
        public HVRHandPose ClosedPose;
        public HVRHandPose RelaxedPose;

        private Vector2 scroll;

        public void OnGUI()
        {
            scroll = EditorGUILayout.BeginScrollView(scroll);

            try
            {
                if (helpBoxStyle == null)
                {
                    helpBoxStyle = GUI.skin.GetStyle("HelpBox");
                    helpBoxStyle = new GUIStyle(helpBoxStyle);
                    helpBoxStyle.fontSize = 13;
                }

                var currLeft = Left;
                var currRight = Right;


                EditorGUILayout.TextArea("1. Create an empty game object and place the hand model's as children of the new object. Make the palms face each other with the finger's facing along the forward (blue) vector of the parent empty.\r\n\r\n" +
                                         "2. Assign the roots of each hand model.\r\n\r\n" +
                                         "3. Assign the root of the XR Rig the hand's will be used with.", helpBoxStyle);
                EditorGUILayout.Space();
                HVREditorExtensions.ObjectField("Left Hand Model", ref Left);
                HVREditorExtensions.ObjectField("Right Hand Model", ref Right);
                HVREditorExtensions.ObjectField("XR Rig Root", ref RigRoot);



                if ((!Left || (currLeft && currLeft != Left)) ||
                    (!Right || (currRight && currRight != Right)))
                {
                    RightHand = LeftHand = null;
                    LeftPoser = RightPoser = null;
                    LeftPhysics = RightPhysics = null;
                    HandsSetup = MirrorDetected = RigSetup = PosesAssigned = false;
                }

                if (!Left || !Right)
                    return;

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Progress Tracking", EditorStyles.boldLabel);

                HVREditorExtensions.Toggle("Hand Setup", ref HandsSetup);
                HVREditorExtensions.Toggle("Mirror Detected", ref MirrorDetected);
                HVREditorExtensions.Toggle("Rig Setup", ref RigSetup);
                HVREditorExtensions.Toggle("Poses Assigned", ref PosesAssigned);
                //HVREditorExtensions.Toggle("Collider Setup", ref ColliderSetup);

                Mirror();

                if (!LeftHand && Left) LeftHand = Left.transform.GetComponentsInChildren<HVRPosableHand>().FirstOrDefault(e => e.IsLeft);
                if (!RightHand && Right) RightHand = Right.transform.GetComponentsInChildren<HVRPosableHand>().FirstOrDefault(e => !e.IsLeft);

                if (!HandsSetup) HandSetup();
                if (!MirrorDetected) DetectMirror();
                if (!RigSetup) SetupRig();
                if (!PosesAssigned) PoseValidation();
                FinalSetup();


            }
            finally
            {
                EditorGUILayout.EndScrollView();
            }

        }

        private void PoseValidation()
        {
            if (!LeftHand || !RightHand || !RigSetup || !RigRoot) return;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Pose Setup", EditorStyles.boldLabel);

            if (!LeftPhysics) LeftHand.TryGetComponent(out LeftPhysics);
            if (!RightPhysics) RightHand.TryGetComponent(out RightPhysics);

            if (!LeftPoser) LeftHand.TryGetComponent(out LeftPoser);
            if (!RightPoser) RightHand.TryGetComponent(out RightPoser);

            var error = new StringBuilder();

            if (!LeftPhysics) error.AppendLine("Left HVRPhysicsPoser missing.");
            else
            {
                if (LeftPhysics.OpenPose == null) error.AppendLine("Left HVRPhysicsPoser missing OpenPose.");
                if (LeftPhysics.ClosedPose == null) error.AppendLine("Left HVRPhysicsPoser missing ClosedPose.");
            }
            if (!RightPhysics) error.AppendLine("Right HVRPhysicsPoser missing.");
            else
            {
                if (RightPhysics.OpenPose == null) error.AppendLine("Right HVRPhysicsPoser missing OpenPose.");
                if (RightPhysics.ClosedPose == null) error.AppendLine("Right HVRPhysicsPoser missing ClosedPose.");
            }

            if (!LeftPoser) error.AppendLine("Left HVRHandPoser missing.");
            else
            {
                if (LeftPoser.PrimaryPose.Pose == null) error.AppendLine("Left HVRHandPoser Primary Pose not assigned.");
            }
            if (!RightPoser) error.AppendLine("Right HVRHandPoser missing.");
            else if (RightPoser.PrimaryPose.Pose == null) error.AppendLine("Right HVRHandPoser Primary Pose not assigned.");


            EditorGUILayout.TextArea("1. Pressing Auto Setup will auto create prefabs of your hands in the root Assets Folder and automate Step 2 for you. Either do this by manually or let the automation do it for you.\r\n\r\n" +
                                     "2. Locate HVRSettings in your project folder:\r\n" +
                                     "  - Assign the newly created hand prefabs to the 'Left Hand' and 'Right Hand' fields.\r\n" +
                                     "  - Toggle off 'Inverse Kinematics' if it's enabled.\r\n" +
                                     "  - Clear out the 'Open Hand Pose' field.\r\n\r\n" +
                                     "3. Press Create Hand Poser and begin creating the required poses.\r\n" +
                                     "  - RelaxedPose: for when your hands are not holding something.\r\n" +
                                     "  - ClosedPose: for the physics poser, and for the resting pose to blend into using finger curl information.\r\n" +
                                     "  - OpenPose: a flat 'Open Hand Pose' pose for the physics poser.\r\n\r\n" +
                                     "4. Assign the poses below and press 'Setup Poses'", helpBoxStyle);

            RelaxedPose = EditorGUILayout.ObjectField("Relaxed Pose:", RelaxedPose, typeof(HVRHandPose), true) as HVRHandPose;
            OpenPose = EditorGUILayout.ObjectField("Open Pose:", OpenPose, typeof(HVRHandPose), true) as HVRHandPose;
            ClosedPose = EditorGUILayout.ObjectField("Closed Pose:", ClosedPose, typeof(HVRHandPose), true) as HVRHandPose;

            if (GUILayout.Button("Auto Setup Prefab / HVRSettings"))
            {
                Left.localPosition = Vector3.zero;
                Right.localPosition = Vector3.zero;

                var leftPrefab = PrefabUtility.SaveAsPrefabAssetAndConnect(Left.gameObject, $"Assets/{Left.name}.prefab", InteractionMode.UserAction);
                var rightPrefab = PrefabUtility.SaveAsPrefabAssetAndConnect(Right.gameObject, $"Assets/{Right.name}.prefab", InteractionMode.UserAction);

                var so = new SerializedObject(HVRSettings.Instance);
                var left = so.FindProperty("LeftHand");
                var right = so.FindProperty("RightHand");
                so.FindProperty("OpenHandPose").objectReferenceValue = null;
                so.FindProperty("InverseKinematics").boolValue = false;

                left.objectReferenceValue = leftPrefab;
                right.objectReferenceValue = rightPrefab;

                so.ApplyModifiedProperties();
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            if (GUILayout.Button("Create Hand Poser"))
            {
                var poser = GameObject.Find("PoserSetup");
                if (!poser) poser = new GameObject("PoserSetup");
                var p = poser.UndoEnsureComponent<HVRHandPoser>();
                poser.transform.position += Vector3.up * 1.7f;
                Selection.activeGameObject = poser;
            }

            if (GUILayout.Button("Setup Poses"))
            {
                SetupPhysicsPoserPoses(LeftPhysics);
                SetupPhysicsPoserPoses(RightPhysics);

                SetupHandPoser(LeftPoser);
                SetupHandPoser(RightPoser);

                var leftHand = RigRoot.GetComponentsInChildren<HVRHandGrabber>().FirstOrDefault(e => e.HandSide == HVRHandSide.Left);
                var rightHand = RigRoot.GetComponentsInChildren<HVRHandGrabber>().FirstOrDefault(e => e.HandSide == HVRHandSide.Right);

                SetupFallback(leftHand);
                SetupFallback(rightHand);

                PosesAssigned = true;

                EditorApplication.delayCall += () =>
                {
                    var leftPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(LeftPhysics.gameObject);
                    var rightPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(RightPhysics.gameObject);
                    Debug.Log($"Applying pose overrides to {leftPath} and {rightPath}.");
                    PrefabUtility.ApplyObjectOverride(LeftPhysics, leftPath, InteractionMode.AutomatedAction);
                    PrefabUtility.ApplyObjectOverride(RightPhysics, rightPath, InteractionMode.AutomatedAction);
                    PrefabUtility.ApplyObjectOverride(LeftPoser, leftPath, InteractionMode.AutomatedAction);
                    PrefabUtility.ApplyObjectOverride(RightPoser, rightPath, InteractionMode.AutomatedAction);

                    //if (leftHand.FallbackPoser)
                    //{
                    //    var so = new SerializedObject(leftHand.FallbackPoser);
                    //    var blend = so.FindProperty("PrimaryPose");
                    //    var pose = blend.FindPropertyRelative("Pose");

                    //    PrefabUtility.ApplyPropertyOverride(pose, leftPath, InteractionMode.AutomatedAction);
                    //}

                    //if (rightHand.FallbackPoser)
                    //{
                    //    var so = new SerializedObject(rightHand.FallbackPoser);
                    //    var blend = so.FindProperty("PrimaryPose");
                    //    var pose = blend.FindPropertyRelative("Pose");

                    //    PrefabUtility.ApplyPropertyOverride(pose, rightPath, InteractionMode.AutomatedAction);
                    //}
                };
            }


            if (error.Length > 0)
            {
                EditorGUILayout.HelpBox(error.ToString(), MessageType.Error);
            }
        }

        private void SetupFallback(HVRHandGrabber hand)
        {
            if (hand && hand.FallbackPoser)
            {
                var so = new SerializedObject(hand.FallbackPoser);
                var blend = so.FindProperty("PrimaryPose");
                var pose = blend.FindPropertyRelative("Pose");
                pose.objectReferenceValue = ClosedPose;
                so.ApplyModifiedProperties();
                EditorUtility.SetDirty(hand);
            }
        }

        private void SetupHandPoser(HVRHandPoser poser)
        {
            var so = new SerializedObject(poser);
            var blend = so.FindProperty("PrimaryPose");
            var pose = blend.FindPropertyRelative("Pose");
            pose.objectReferenceValue = RelaxedPose;

            so.ApplyModifiedProperties();

            var SP_Blends = so.FindProperty("Blends");

            var i = SP_Blends.arraySize;
            if (i == 0) SP_Blends.InsertArrayElementAtIndex(i);

            i = 0;

            var test = SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("Pose");

            test.objectReferenceValue = ClosedPose;

            SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("AnimationParameter").stringValue = HVRHandPoseBlend.DefaultParameter;
            SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("Weight").floatValue = 1f;
            SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("Speed").floatValue = 16f;

            SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("Mask").enumValueIndex = (int)HVRHandPoseMask.None;
            SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("Type").enumValueIndex = (int)BlendType.Immediate;

            SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("ThumbType").enumValueIndex = (int)HVRFingerType.Close;
            SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("IndexType").enumValueIndex = (int)HVRFingerType.Close;
            SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("MiddleType").enumValueIndex = (int)HVRFingerType.Close;
            SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("RingType").enumValueIndex = (int)HVRFingerType.Close;
            SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("PinkyType").enumValueIndex = (int)HVRFingerType.Close;

            SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("ThumbStart").floatValue = 0f;
            SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("IndexStart").floatValue = 0f;
            SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("MiddleStart").floatValue = 0f;
            SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("RingStart").floatValue = 0f;
            SP_Blends.GetArrayElementAtIndex(i).FindPropertyRelative("PinkyStart").floatValue = 0f;

            so.ApplyModifiedProperties();
        }

        private void SetupPhysicsPoserPoses(HVRPhysicsPoser pp)
        {
            var so = new SerializedObject(pp);
            var open = so.FindProperty("OpenPose");
            var closed = so.FindProperty("ClosedPose");

            open.objectReferenceValue = OpenPose;
            closed.objectReferenceValue = ClosedPose;

            so.ApplyModifiedProperties();
        }

        private void FinalSetup()
        {
            if (!LeftHand || !RightHand || !RigRoot || !RigSetup || !PosesAssigned) return;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Final Setup", EditorStyles.boldLabel);

            EditorGUILayout.TextArea("Orient the new hands to match the existing hand model or orient in play mode and copy and paste the transform component out into editor mode." +
                                     "\r\n\r\nLocate 'JointAnchor' on each physics hand and move it to the center of the palm" +
                                     "\r\n\r\nAt this point you should add colliders to your hands, if you want per bone capsules, you can use the helper buttons on the HVRPosableHand component inspectors. " +
                                     "\r\n\r\nIf you're ok with a primitive box shape, you can press 'Add Collider Controller' which will add a component to the root that will dynamically manage the hand collider. " +
                                     "\r\n\r\nA Box collider will be added to the hand rigidbody relative to the hand model, make sure you're happy with the hand orientation first.\r\n" +
                                     "\r\nYou can always adjust the box collider shape and rotation manually after it's created.\r\n" +
                                     "\r\nAssign an animation curve on the collider controller, which will be used in conjunction with finger curl information to shrink the box to fit a fist pose. The default exponential curve provided by unity seems to work fine.\r\n",
                helpBoxStyle);



            if (GUILayout.Button("Add Collider Controller"))
            {
                var cc = RigRoot.EnsureComponent<HVRHandColliderController>();
                cc.LeftPosableHand = LeftHand;
                cc.RightPosableHand = RightHand;

                var leftHand = RigRoot.GetComponentsInChildren<HVRHandGrabber>().FirstOrDefault(e => e.HandSide == HVRHandSide.Left);
                var rightHand = RigRoot.GetComponentsInChildren<HVRHandGrabber>().FirstOrDefault(e => e.HandSide == HVRHandSide.Right);

                if (leftHand)
                    cc.LeftParent = Add(leftHand.transform, "Colliders");
                if (rightHand)
                    cc.RightParent = Add(rightHand.transform, "Colliders");

                cc.AddHandBoxesF();
                ColliderSetup = true;
            }
        }

        private void SetupRig()
        {
            if (!LeftHand || !RightHand || !HandsSetup || !RigRoot) return;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Rig Setup", EditorStyles.boldLabel);


            EditorGUILayout.TextArea("Press 'Setup Rig References', this will update required component references for you.\r\n"

                , helpBoxStyle);

            if (GUILayout.Button("Setup Rig References"))
            {
                SetupRigRefs();
                RigSetup = true;
            }
        }

        private GameObject _clone;

        private void Mirror()
        {
            if (!LeftHand || !RightHand || !HandsSetup || !MirrorDetected || RigSetup) return;


            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Mirror Testing", EditorStyles.boldLabel);

            if (LeftHand.transform.parent != RightHand.transform.parent)
            {
                EditorGUILayout.HelpBox("To test mirroring, both hands must be the child of the same empty game object.", MessageType.Warning);
                return;
            }

            if (GUILayout.Button(_mirrorText))
            {
                if (!Mirroring)
                {
                    var clone = Object.Instantiate(LeftHand.transform.parent);
                    clone.transform.parent = null;
                    clone.transform.position += Vector3.right * .5f;

                    _leftMirrorHand = clone.GetComponentsInChildren<HVRPosableHand>().FirstOrDefault(e => e.IsLeft);
                    _rightMirrorHand = clone.GetComponentsInChildren<HVRPosableHand>().FirstOrDefault(e => !e.IsLeft);

                    _clone = clone.gameObject;

                    Selection.activeGameObject = _rightMirrorHand.gameObject;
                    _rightMirrorHand.gameObject.SetExpandedRecursive(true);
                }
                else
                {
                    if (_clone)
                    {
                        DestroyImmediate(_clone);
                        _leftMirrorHand = null;
                        _rightMirrorHand = null;
                    }
                }

                Mirroring = !Mirroring;
                _mirrorText = Mirroring ? "Stop Mirroring" : "Start Mirroring";
            }
        }

        private void UnsubSceneGui()
        {
            SceneView.duringSceneGui -= OnSceneGui;
        }

        private bool _badMirror;
        private string _badMirrorText;

        private void DetectMirror()
        {
            if (!LeftHand || !RightHand || !HandsSetup) return;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Mirror Detect", EditorStyles.boldLabel);

            EditorGUILayout.TextArea("Orient the hands into a mirrored pose with the fingers facing along the " +
                                     "forward (blue) axis of the parent object and the palms facing each other.\r\n\r\n" +
                                     "The thumb bones may need to be bent a bit to align it's forward vector with the parent's forward vector if the detection fails.\r\n\r\n" +
                                     "Once Done Press Detect Mirror", helpBoxStyle);

            if (GUILayout.Button("Detect Mirror"))
            {
                DetectBoneAxes(LeftHand, RightHand, LeftHand.transform.parent.forward, LeftHand.transform.parent.up);
                DetectBoneAxes(RightHand, LeftHand, LeftHand.transform.parent.forward, LeftHand.transform.parent.up);

                _badMirrorText = "";
                _badMirror = false;

                ValidateMirrorSettings(LeftHand);
                ValidateMirrorSettings(RightHand);

                EditorUtility.SetDirty(LeftHand);
                EditorUtility.SetDirty(RightHand);

                MirrorDetected = !_badMirror;
            }

            if (_badMirror)
            {
                EditorGUILayout.TextArea($"Unable to detect all forward and up axes of the bones.\r\n\r\n" +
                                         $"Bend the offending fingers so that each bone's forward and up axes align more closely with the parent object's forward and up axes.\r\n\r\n" +
                                         $"{_badMirrorText}", helpBoxStyle);
            }
        }

        public void DetectBoneAxes(HVRPosableHand hand, HVRPosableHand otherHand, Vector3 forward, Vector3 up)
        {
            var so = new SerializedObject(hand);
            var fingers = so.FindProperty("_fingers");

            for (var i = 0; i < fingers.arraySize; i++)
            {
                var spFinger = fingers.GetArrayElementAtIndex(i);
                var spBones = spFinger.FindPropertyRelative("Bones");

                for (var j = 0; j < spBones.arraySize; j++)
                {
                    var spBone = spBones.GetArrayElementAtIndex(j);
                    var bone = hand.Fingers[i].Bones[j];
                    var targetBone = otherHand.Fingers[i].Bones[j];

                    // Get local orthogonal axes of the right hand pointing forward and up
                    var axis1 = HVRPosableHand.GetSignedAxisVectorToDirection(bone.Transform.rotation, forward);
                    var axis2 = HVRPosableHand.GetSignedAxisVectorToDirection(bone.Transform.rotation, up);

                    var targetAxis1 = HVRPosableHand.GetSignedAxisVectorToDirection(targetBone.Transform.rotation, forward);
                    var targetAxis2 = HVRPosableHand.GetSignedAxisVectorToDirection(targetBone.Transform.rotation, up);

                    spBone.FindPropertyRelative(nameof(HVRPosableBone.Forward)).vector3Value = axis1;
                    spBone.FindPropertyRelative(nameof(HVRPosableBone.Up)).vector3Value = axis2;
                    spBone.FindPropertyRelative(nameof(HVRPosableBone.OtherForward)).vector3Value = targetAxis1;
                    spBone.FindPropertyRelative(nameof(HVRPosableBone.OtherUp)).vector3Value = targetAxis2;

                }
            }

            so.ApplyModifiedProperties();
        }

        private void ValidateMirrorSettings(HVRPosableHand hand)
        {
            for (var i = 0; i < hand.Fingers.Length; i++)
            {
                var finger = hand.Fingers[i];
                for (var j = 0; j < finger.Bones.Count; j++)
                {
                    var bone = finger.Bones[j];
                    if (!Mathf.Approximately(0f, Vector3.Dot(bone.Forward, bone.Up)))
                    {
                        _badMirror = true;
                        _badMirrorText += $"{bone.Transform.name.PadRight(25)}: Forward ({bone.Forward.LogFormatF0()}) Up: ({bone.Up.LogFormatF0()}).\r\n";
                    }

                    if (!Mathf.Approximately(0f, Vector3.Dot(bone.OtherForward, bone.OtherUp)))
                    {
                        _badMirror = true;
                        _badMirrorText += $"{bone.Transform.name.PadRight(25)}: OtherForward ({bone.OtherForward.LogFormatF0()}) OtherUp: ({bone.OtherUp.LogFormatF0()}).\r\n";
                    }
                }
            }
        }

        private void HandSetup()
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Hand Setup", EditorStyles.boldLabel);



            EditorGUILayout.TextArea(
                "1. The 'Parent' field is the transform that is the common parent of each finger's first bone." +
                "\r\n\r\n2. Match the order of the finger list with the order on the hand model." +
                " The default order matches most hand rigs. If your hand has less than 5 fingers, set those slots to 'None'\r\n" +
                "\r\n3. Update the bone count for each finger.\r\n" +
                "\r\n4. Some hand rigs may have an uneven hierarchy, update the Root Offset field with the # of bones between the parent and first bone.\r\n" +
                "\r\n5. Press Setup and verify each HVRPosableHand fingers have the proper Root, Tip, and Bone counts assigned. Move the tip transforms to the center of the finger pad.\r\n" +
                "\r\n6. Move the 'Palm' transforms that were added to the hands to the center of the palm (barely touching the surface) with the forward (blue) axis facing out of the palm.", helpBoxStyle);

            //int i = 0;
            //bool up = false;
            //bool down = false;

            HVREditorExtensions.ObjectField("Left Finger Parent", ref LeftFingerParent);
            HVREditorExtensions.ObjectField("Right Finger Parent", ref RightFingerParent);

            foreach (var s in settings)
            {
                EditorGUILayout.BeginHorizontal();

                HVREditorExtensions.EnumField("Finger", ref s.Finger);
                HVREditorExtensions.IntField("Bones", ref s.BoneCount);
                HVREditorExtensions.IntField("Root Offset", ref s.BoneOffset);

                //if (GUILayout.Button("^"))
                //{
                //    i = s.Index;
                //    up = true;
                //}

                //if (GUILayout.Button("V"))
                //{
                //    i = s.Index;
                //    down = true;
                //}

                EditorGUILayout.EndHorizontal();
            }

            //if (up && i > 0)
            //{
            //    var temp = settings[i];
            //    settings.RemoveAt(i);
            //    settings.Insert(i - 1, temp);
            //}

            //if (down && i < settings.Count - 1)
            //{
            //    var temp = settings[i];
            //    settings.RemoveAt(i);
            //    settings.Insert(i + 1, temp);
            //}

            //for (var k = 0; k < settings.Count; k++)
            //{
            //    settings[k].Index = k;
            //}

            if (GUILayout.Button("Setup"))
            {
                var left = SetupHand(true, Left, LeftFingerParent);
                var right = SetupHand(false, Right, RightFingerParent);

                left.SetupFingerArray();
                right.SetupFingerArray();

                EditorUtility.SetDirty(left);
                EditorUtility.SetDirty(right);

                HandsSetup = true;
            }
        }

        private HVRPosableHand _leftMirrorHand, _rightMirrorHand;

        private void OnSceneGui(SceneView obj)
        {
            if (_mirroring && _leftMirrorHand && _rightMirrorHand)
            {
                _rightMirrorHand.Mirror(_leftMirrorHand, MirrorAxis.X);
            }
        }


        private void SetupRigRefs()
        {
            var leftHand = RigRoot.GetComponentsInChildren<HVRHandGrabber>().FirstOrDefault(e => e.HandSide == HVRHandSide.Left);
            var rightHand = RigRoot.GetComponentsInChildren<HVRHandGrabber>().FirstOrDefault(e => e.HandSide == HVRHandSide.Right);

            Left.parent = leftHand.transform;
            Left.transform.localPosition = Vector3.zero;
            Right.parent = rightHand.transform;
            Right.transform.localPosition = Vector3.zero;

            SetupHandGrabber(leftHand, Left);
            SetupHandGrabber(rightHand, Right);
        }


        private void SetupHandGrabber(HVRHandGrabber hand, Transform handRoot)
        {
            var so = new SerializedObject(hand);
            var hover = so.FindProperty("HoverPoser");
            hover.objectReferenceValue = null;
            var grabPoser = so.FindProperty("GrabPoser");
            grabPoser.objectReferenceValue = null;


            so.FindProperty("HandAnimator").objectReferenceValue = handRoot.gameObject.GetComponent<HVRHandAnimator>();
            so.FindProperty("PhysicsPoser").objectReferenceValue = handRoot.gameObject.GetComponent<HVRPhysicsPoser>();
            so.FindProperty("InverseKinematics").boolValue = false;
            so.FindProperty("HandModel").objectReferenceValue = handRoot;
            so.ApplyModifiedProperties();


            var fg = hand.gameObject.GetComponentInChildren<HVRForceGrabber>();
            if (fg)
            {
                so = new SerializedObject(fg);
                hover = so.FindProperty("HoverPoser");
                hover.objectReferenceValue = null;
                grabPoser = so.FindProperty("GrabPoser");
                grabPoser.objectReferenceValue = null;
                EditorUtility.SetDirty(fg);
                so.ApplyModifiedProperties();
            }

            if (hand.FallbackPoser)
            {
                so = new SerializedObject(hand.FallbackPoser);
                var blend = so.FindProperty("PrimaryPose");
                var pose = blend.FindPropertyRelative("Pose");
                pose.objectReferenceValue = null;
                so.ApplyModifiedProperties();
            }

            EditorUtility.SetDirty(hand);
        }

        HVRPosableHand SetupHand(bool left, Transform t, Transform fingerParent)
        {
            var go = t.gameObject;

            var hand = go.UndoEnsureComponent<HVRPosableHand>();
            var animator = go.UndoEnsureComponent<HVRHandAnimator>();
            var physicsPoser = go.UndoEnsureComponent<HVRPhysicsPoser>();
            var poser = go.UndoEnsureComponent<HVRHandPoser>();
            var hm = go.UndoEnsureComponent<HVRHandMirrorer>();

            var palm = t.Find("Palm");
            if (!palm)
            {
                var p = new GameObject("Palm");
                p.transform.parent = t;
                p.transform.ResetLocalProps();
                palm = p.transform;
            }

            hand.IsLeft = left;
            hand.HandMirrorer = hm;


            animator.DefaultPoseHand = false;
            animator.PhysicsPoser = physicsPoser;
            animator.Hand = hand;
            animator.DefaultPoser = poser;
            physicsPoser.Hand = hand;
            physicsPoser.Palm = palm;

            int i = 0;
            foreach (var s in settings)
            {
                HVRPosableFinger finger;

                switch (s.Finger)
                {
                    case Finger.Thumb:
                        finger = hand.Thumb = new HVRPosableFinger();
                        break;
                    case Finger.Index:
                        finger = hand.Index = new HVRPosableFinger();
                        break;
                    case Finger.Middle:
                        finger = hand.Middle = new HVRPosableFinger();
                        break;
                    case Finger.Ring:
                        finger = hand.Ring = new HVRPosableFinger();
                        break;
                    case Finger.Pinky:
                        finger = hand.Pinky = new HVRPosableFinger();
                        break;
                    default:
                        i++;
                        continue;
                }


                var node = fingerParent.GetChild(i);

                for (var k = 0; k < s.BoneOffset; k++)
                    node = node.GetChild(0);

                for (var j = 0; j < s.BoneCount; j++)
                {

                    var bone = new HVRPosableBone();
                    bone.Transform = node;
                    finger.Bones.Add(bone);
                    if (j < s.BoneCount - 1) node = node.GetChild(0);
                }


                if (finger.Bones.Count > 0)
                {
                    var last = finger.Bones.Last();
                    var tipName = s.Finger.ToString() + " Tip";
                    var existing = last.Transform.Find(tipName);

                    if (!existing)
                    {
                        var tip = new GameObject(tipName);
                        tip.transform.parent = last.Transform;
                        tip.transform.ResetLocalProps();
                        finger.Tip = tip.transform;

                        Undo.RegisterCreatedObjectUndo(tip, $"Add {tipName} to {last.Transform.name}");
                    }
                    else
                    {
                        finger.Tip = existing;
                    }


                    finger.Root = finger.Bones[0].Transform;
                }

                i++;
            }


            if (hand.Index != null)
            {
                hand.IndexRoot = hand.Index.Bones.FirstOrDefault()?.Transform;
                hand.IndexTip = hand.Index?.Tip;
            }

            if (hand.Thumb != null)
            {
                hand.ThumbRoot = hand.Thumb.Bones.FirstOrDefault()?.Transform;
                hand.ThumbTip = hand.Thumb?.Tip;
            }

            if (hand.Middle != null)
            {
                hand.MiddleRoot = hand.Middle.Bones.FirstOrDefault()?.Transform;
                hand.MiddleTip = hand.Middle?.Tip;
            }

            if (hand.Ring != null)
            {
                hand.RingRoot = hand.Ring.Bones.FirstOrDefault()?.Transform;
                hand.RingTip = hand.Ring?.Tip;
            }

            if (hand.Pinky != null)
            {
                hand.PinkyRoot = hand.Pinky.Bones.FirstOrDefault()?.Transform;
                hand.PinkyTip = hand.Pinky?.Tip;
            }

            EditorUtility.SetDirty(hand);
            EditorUtility.SetDirty(animator);
            EditorUtility.SetDirty(physicsPoser);
            EditorUtility.SetDirty(poser);

            return hand;
        }

        private Transform Add(Transform parent, string name)
        {
            var existing = parent.Find(name);

            if (!existing)
            {
                var tip = new GameObject(name);
                tip.transform.parent = parent;
                tip.transform.ResetLocalProps();
                existing = tip.transform;
                Undo.RegisterCreatedObjectUndo(tip, $"Add {name} to {parent.name}");
                Debug.Log($"{tip.name} added to {parent.name}");
            }

            return existing;
        }

    }



    public class FingerSetting
    {
        public Finger Finger;
        public int BoneCount = 3;
        public int BoneOffset = 0;
        public int Index;
    }

    public enum Finger
    {
        None, Thumb, Index, Middle, Ring, Pinky
    }
}