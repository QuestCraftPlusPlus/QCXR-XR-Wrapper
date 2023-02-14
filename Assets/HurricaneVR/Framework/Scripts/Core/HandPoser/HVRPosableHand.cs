using System;
using System.Collections.Generic;
using System.IO;
using HurricaneVR.Framework.Core.HandPoser.Data;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;

using UnityEngine;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HurricaneVR.Framework.Core.HandPoser
{
    public class HVRPosableHand : MonoBehaviour
    {
        [Header("Settings")]
        public bool IsLeft;

        [Tooltip("Used to match up with grab points to allowed objects to have grab points that can be grabbed by different hands.")]
        public int PoserIndex;

        [Header("Mirroring")]
        public MirrorAxis MirrorAxis = MirrorAxis.X;
        public HVRHandMirrorSettings MirrorSettings;
        public HVRHandMirrorer HandMirrorer;

        [Header("Hand Mirror Settings for VRIK")]
        public bool UseMatchRotation;

        [FormerlySerializedAs("Forward")] public HVRAxis Axis1 = HVRAxis.Y;
        [FormerlySerializedAs("Up")] public HVRAxis Axis2 = HVRAxis.Z;

        [FormerlySerializedAs("TargetAxis")] [FormerlySerializedAs("OtherForward")] public HVRAxis TargetAxis1 = HVRAxis.Y;
        [FormerlySerializedAs("OtherUp")] public HVRAxis TargetAxis2 = HVRAxis.Z;

        [Header("Fingers")]

        public HVRPosableFinger Thumb;
        public HVRPosableFinger Index;
        public HVRPosableFinger Middle;
        public HVRPosableFinger Ring;
        public HVRPosableFinger Pinky;

        public HVRHandSide Side => IsLeft ? HVRHandSide.Left : HVRHandSide.Right;

        public bool IsRight => !IsLeft;

        [SerializeField]
        private HVRPosableFinger[] _fingers;

        public HVRPosableFinger[] Fingers
        {
            get
            {
                if (_fingers == null || _fingers.Length == 0)
                {
                    SetupFingerArray();
                }

                return _fingers;
            }
        }

        public void SetupFingerArray()
        {
            var fingers = new List<HVRPosableFinger>();

            if (Thumb != null)
            {
                fingers.Add(Thumb);
            }

            if (Index != null)
            {
                fingers.Add(Index);
            }

            if (Middle != null)
            {
                fingers.Add(Middle);
            }

            if (Ring != null)
            {
                fingers.Add(Ring);
            }

            if (Pinky != null)
            {
                fingers.Add(Pinky);
            }

            _fingers = fingers.ToArray();
        }



        private void Awake()
        {
            SetupFingerArray();

            if (!HandMirrorer)
                TryGetComponent(out HandMirrorer);
        }

        public void Pose(HVRHandPose pose)
        {
            Pose(pose.GetPose(this.IsLeft));
        }

        public void PoseFingers(HVRHandPose pose)
        {
            PoseFingers(pose.GetPose(this.IsLeft));
        }

        public void PoseFingers(HVRHandPoseData pose)
        {
            PoseFinger(Thumb, pose.Thumb);
            PoseFinger(Index, pose.Index);
            PoseFinger(Middle, pose.Middle);
            PoseFinger(Ring, pose.Ring);
            PoseFinger(Pinky, pose.Pinky);
        }

        public void Pose(HVRHandPoseData pose, bool poseHand = true)
        {
            if (poseHand)
            {
                transform.localPosition = pose.Position;
                transform.localRotation = pose.Rotation;
            }

            PoseFinger(Thumb, pose.Thumb);
            PoseFinger(Index, pose.Index);
            PoseFinger(Middle, pose.Middle);
            PoseFinger(Ring, pose.Ring);
            PoseFinger(Pinky, pose.Pinky);
        }

        public void PoseFrom(HVRPosableHand source, bool poseHand = true)
        {
            if (poseHand)
            {
                transform.position = source.transform.position;
                transform.rotation = source.transform.rotation;
            }

            PoseFinger(Thumb, source.Thumb);
            PoseFinger(Index, source.Index);
            PoseFinger(Middle, source.Middle);
            PoseFinger(Ring, source.Ring);
            PoseFinger(Pinky, source.Pinky);
        }

        public static Vector3 GetSignedAxisVectorToDirection(Quaternion r, Vector3 direction)
        {
            direction = direction.normalized;
            Vector3 axis = Vector3.right;

            float dotX = Mathf.Abs(Vector3.Dot(r * Vector3.right, direction));
            float dotY = Mathf.Abs(Vector3.Dot(r * Vector3.up, direction));
            if (dotY > dotX) axis = Vector3.up;
            float dotZ = Mathf.Abs(Vector3.Dot(r * Vector3.forward, direction));
            if (dotZ > dotX && dotZ > dotY) axis = Vector3.forward;

            if (Vector3.Dot(r * axis, direction) < 0f) axis = -axis;

            return axis;
        }

        public void PoseFinger(HVRPosableFinger finger, HVRPosableFingerData data)
        {
            if (finger == null || data == null)
            {
                return;
            }

            if (finger.Bones == null || data.Bones == null)
            {
                return;
            }

            for (int i = 0; i < finger.Bones.Count; i++)
            {
                var bone = finger.Bones[i];
                if (data.Bones.Count - 1 >= i)
                {
                    var boneData = data.Bones[i];
                    bone.Transform.localPosition = boneData.Position;
                    bone.Transform.localRotation = boneData.Rotation;
                }
            }
        }

        public void PoseFinger(HVRPosableFinger finger, HVRPosableFinger target)
        {
            if (finger == null || target == null)
            {
                return;
            }

            if (finger.Bones == null || target.Bones == null)
            {
                return;
            }

            for (int i = 0; i < finger.Bones.Count; i++)
            {
                var bone = finger.Bones[i];
                if (target.Bones.Count - 1 >= i)
                {
                    var boneData = target.Bones[i];
                    bone.Transform.localPosition = boneData.Transform.localPosition;
                    bone.Transform.localRotation = boneData.Transform.localRotation;
                }
            }
        }

        #region Editor


#if UNITY_EDITOR

        [Header("Finger Setup")]

        [InspectorButton("FingerSetup")]
        public string SetupFingers;
        public void FingerSetup()
        {
            _fingers = null;

            if (ThumbRoot)
            {
                Thumb = new HVRPosableFinger();
                Thumb.Root = ThumbRoot;
                Thumb.Tip = ThumbTip;
                FindBonesForFinger(ThumbRoot, ThumbTip, Thumb);
            }

            if (IndexRoot)
            {
                Index = new HVRPosableFinger();
                Index.Root = IndexRoot;
                Index.Tip = IndexTip;
                FindBonesForFinger(IndexRoot, IndexTip, Index);
            }

            if (MiddleRoot)
            {
                Middle = new HVRPosableFinger();
                Middle.Root = MiddleRoot;
                Middle.Tip = MiddleTip;
                FindBonesForFinger(MiddleRoot, MiddleTip, Middle);
            }

            if (RingRoot)
            {
                Ring = new HVRPosableFinger();
                Ring.Root = RingRoot;
                Ring.Tip = RingTip;
                FindBonesForFinger(RingRoot, RingTip, Ring);
            }

            if (PinkyRoot)
            {
                Pinky = new HVRPosableFinger();
                Pinky.Root = PinkyRoot;
                Pinky.Tip = PinkyTip;
                FindBonesForFinger(PinkyRoot, PinkyTip, Pinky);
            }

            EditorUtility.SetDirty(this);
        }

        public Transform ThumbRoot;
        public Transform ThumbTip;

        public Transform IndexRoot;
        public Transform IndexTip;

        public Transform MiddleRoot;
        public Transform MiddleTip;

        public Transform RingRoot;
        public Transform RingTip;

        public Transform PinkyRoot;
        public Transform PinkyTip;

        [Header("Capsule Helpers (Floaty hands only!)")]


        public float CapsuleRadius = .01f;
        public HVRAxis CapsuleDirection;
        public bool CapsuleAddRadius = true;

        [InspectorButton("AddThumbCapsulesPrivate")]
        public string AddThumbCapsules;
        [InspectorButton("AddIndexCapsulesPrivate")]
        public string AddIndexCapsules;
        [InspectorButton("AddMiddleCapsulesPrivate")]
        public string AddMiddleCapsules;
        [InspectorButton("AddRingCapsulesPrivate")]
        public string AddRingCapsules;
        [InspectorButton("AddPinkyCapsulesPrivate")]
        public string AddPinkyCapsules;

        public HVRPosableHand otherhand;

        [InspectorButton("DetectFingerAxesPrivate")]
        public string DetectFingerAxes;

        [InspectorButton("MirrorTest")]
        public string MirrorToOtherHand;

        [InspectorButton("ApplyPoseF")]
        public string ApplyPrimaryPose;

        private void ApplyPoseF()
        {
            var p = gameObject.GetComponent<HVRHandPoser>();
            if (!p) return;
            var pose = p.PrimaryPose.Pose.GetPose(IsLeft);
            Pose(pose);
            EditorUtility.SetDirty(p);
        }

        private void DetectFingerAxesPrivate()
        {
            DetectBoneAxes(otherhand, transform.parent.forward, transform.parent.up);
        }

        private void MirrorTest()
        {
            var m = Mirror(MirrorAxis.X);
            otherhand.Pose(m, false);
        }

        private void AddThumbCapsulesPrivate()
        {
            AddFingerCapsules(Thumb);
        }

        private void AddIndexCapsulesPrivate()
        {
            AddFingerCapsules(Index);
        }

        private void AddMiddleCapsulesPrivate()
        {
            AddFingerCapsules(Middle);
        }

        private void AddRingCapsulesPrivate()
        {
            AddFingerCapsules(Ring);
        }

        private void AddPinkyCapsulesPrivate()
        {
            AddFingerCapsules(Pinky);
        }

        private void AddFingerCapsules(HVRPosableFinger finger)
        {
            if (finger == null || finger.Bones == null)
                return;

            Undo.SetCurrentGroupName("AddFingerCapsules");

            for (var i = 0; i < finger.Bones.Count; i++)
            {
                var bone = finger.Bones[i];
                var colName = "coll_" + bone.Transform.name;
                var col = bone.Transform.Find(colName);
                GameObject go;
                if (col) go = col.gameObject;
                else
                {
                    go = new GameObject("coll_" + bone.Transform.name);
                    go.transform.parent = bone.Transform;
                
                    
                    Undo.RegisterCreatedObjectUndo(go, "AddGO");
                }

                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                go.transform.localScale = Vector3.one;

                var capsule = go.GetComponent<CapsuleCollider>();
                if (!capsule)
                {
                    capsule = go.AddComponent<CapsuleCollider>();
                }

                Transform next;
                if (i < finger.Bones.Count - 1)
                {
                    next = finger.Bones[i + 1].Transform;
                }
                else
                {
                    next = finger.Tip;
                }

                var jointDelta = next.position - bone.Transform.position;
                var length = jointDelta.magnitude;
                if (CapsuleAddRadius && i == finger.Bones.Count - 1) length -= CapsuleRadius;
                go.transform.position += jointDelta.normalized * (length * 0.5f);
                capsule.height = length;
                if (CapsuleAddRadius)
                {
                    capsule.height += CapsuleRadius * 2f;
                }

                capsule.radius = CapsuleRadius;
                capsule.direction = (int)CapsuleDirection;

            }

            Undo.CollapseUndoOperations(Undo.GetCurrentGroup());
        }

#endif

        #endregion



        private void FindBonesForFinger(Transform bone, Transform tip, HVRPosableFinger finger)
        {
            finger.Bones.Add(new HVRPosableBone() { Transform = bone.transform });

            if (tip.parent == bone)
            {
                return;
            }

            if (bone.childCount > 0)
            {
                FindBonesForFinger(bone.GetChild(0), tip, finger);
            }
        }

        public HVRHandPoseData CreateHandPose(Transform transformOverride = null)
        {
            var t = transformOverride ?? transform;

            var data = new HVRHandPoseData
            {
                Position = t.localPosition,
                Rotation = t.localRotation,
            };

            data.Thumb = Thumb?.GetFingerData();
            data.Index = Index?.GetFingerData();
            data.Middle = Middle?.GetFingerData();
            data.Ring = Ring?.GetFingerData();
            data.Pinky = Pinky?.GetFingerData();

            return data;
        }

        public void CopyHandData(HVRHandPoseData data)
        {
            data.Position = transform.localPosition;
            data.Rotation = transform.localRotation;

            for (var i = 0; i < Fingers.Length; i++)
            {
                var finger = Fingers[i];
                for (var j = 0; j < finger.Bones.Count; j++)
                {
                    var bone = finger.Bones[j];

                    data.Fingers[i].Bones[j].Position = bone.Transform.localPosition;
                    data.Fingers[i].Bones[j].Rotation = bone.Transform.localRotation;
                }
            }
        }

        public HVRHandPose CreateFullHandPoseWorld(MirrorAxis axis)
        {
            var hand = CreateHandPose();
            hand.Position = transform.position;
            hand.Rotation = transform.rotation;

            //var handMirror = hand.Mirror(axis, transform);
            var handMirror = Mirror(axis);

            var left = IsLeft ? hand : handMirror;
            var right = IsLeft ? handMirror : hand;

            var handPose = ScriptableObject.CreateInstance<HVRHandPose>();
            handPose.SnappedLeft = Side == HVRHandSide.Left;
            handPose.LeftHand = left;
            handPose.RightHand = right;
            return handPose;
        }

        public HVRHandPose CreateFullHandPose(MirrorAxis axis, Transform transformOverride = null)
        {
            var hand = CreateHandPose(transformOverride);

            //var handMirror = hand.Mirror(axis, transform);
            var handMirror = Mirror(axis);

            var left = IsLeft ? hand : handMirror;
            var right = IsLeft ? handMirror : hand;

            var handPose = ScriptableObject.CreateInstance<HVRHandPose>();
            handPose.SnappedLeft = Side == HVRHandSide.Left;
            handPose.LeftHand = left;
            handPose.RightHand = right;
            return handPose;
        }

        public void Mirror(HVRPosableHand targetHand, MirrorAxis axis)
        {
            MirrorHand(axis, out var pos, out var rot);

            targetHand.transform.localPosition = pos;
            targetHand.transform.localRotation = rot;

            if (HandMirrorer)
                HandMirrorer.MirrorFingers(this, targetHand);
        }

        public void MirrorHand(MirrorAxis axis, out Vector3 position, out Quaternion rotation, Transform transformOverride = null)
        {
            var hand = transformOverride ? transformOverride : transform; //iktarget doesn't have the HVRPosableHand on it

            position = hand.localPosition;
            rotation = hand.localRotation;

            Vector3 direction;

            switch (axis)
            {
                case MirrorAxis.X:
                    position.x *= -1f;
                    direction = Vector3.right;
                    break;
                case MirrorAxis.Y:
                    position.y *= -1;
                    direction = Vector3.up;
                    break;
                case MirrorAxis.Z:
                    position.z *= -1;
                    direction = Vector3.forward;
                    break;
                default:
                    return;
            }

            Vector3 forward;
            Vector3 up;

            if (hand.parent != null)
            {
                forward = hand.parent.InverseTransformDirection(hand.forward);
                up = hand.parent.InverseTransformDirection(hand.up);
            }
            else
            {
                forward = hand.forward;
                up = hand.up;
            }

            var mirror = Vector3.Reflect(forward, direction);
            var upMirror = Vector3.Reflect(up, direction);
            rotation = Quaternion.LookRotation(mirror, upMirror);

            if (UseMatchRotation)
            {
                rotation = MatchRotation(rotation, TargetAxis1.GetVector(), TargetAxis2.GetVector(), Axis1.GetVector(), Axis2.GetVector());
            }
        }

        public HVRHandPoseData Mirror(MirrorAxis axis, Transform transformOverride = null)
        {
            var clone = new HVRHandPoseData();

            MirrorHand(axis, out clone.Position, out clone.Rotation, transformOverride);

            if (HandMirrorer)
            {
                HandMirrorer.MirrorFingers(this, clone);
            }
            else
            {
                MirrorFingers(clone);
            }

            return clone;
        }

        private void MirrorFingers(HVRHandPoseData clone)
        {
            HVRJointMirrorSetting thumbOverride = null;
            HVRJointMirrorSetting indexMirror = null;
            HVRJointMirrorSetting middleMirror = null;
            HVRJointMirrorSetting ringMirror = null;
            HVRJointMirrorSetting pinkyMirror = null;

            if (MirrorSettings)
            {
                thumbOverride = MirrorSettings.UseThumbSetting ? MirrorSettings.ThumbSetting : MirrorSettings.AllSetting;
                indexMirror = MirrorSettings.UseIndexSetting ? MirrorSettings.IndexSetting : MirrorSettings.AllSetting;
                middleMirror = MirrorSettings.UseMiddleSetting ? MirrorSettings.MiddleSetting : MirrorSettings.AllSetting;
                ringMirror = MirrorSettings.UseRingSetting ? MirrorSettings.RingSetting : MirrorSettings.AllSetting;
                pinkyMirror = MirrorSettings.UsePinkySetting ? MirrorSettings.PinkySetting : MirrorSettings.AllSetting;
            }


            if (Thumb != null)
            {
                clone.Thumb = MirrorFinger(Thumb, thumbOverride, MirrorSettings?.ThumbSettings);
            }

            if (Index != null)
            {
                clone.Index = MirrorFinger(Index, indexMirror, MirrorSettings?.IndexSettings);
            }

            if (Middle != null)
            {
                clone.Middle = MirrorFinger(Middle, middleMirror, MirrorSettings?.MiddleSettings);
            }

            if (Ring != null)
            {
                clone.Ring = MirrorFinger(Ring, ringMirror, MirrorSettings?.RingSettings);
            }

            if (Pinky != null)
            {
                clone.Pinky = MirrorFinger(Pinky, pinkyMirror, MirrorSettings?.PinkySettings);
            }
        }


        public void DetectBoneAxes(HVRPosableHand otherHand, Vector3 forward, Vector3 up)
        {
            for (var i = 0; i < Fingers.Length; i++)
            {
                var finger = Fingers[i];
                for (var j = 0; j < finger.Bones.Count; j++)
                {
                    var bone = finger.Bones[j];
                    var targetBone = otherHand.Fingers[i].Bones[j];

                    // Get local orthogonal axes of the right hand pointing forward and up
                    var axis1 = GetSignedAxisVectorToDirection(bone.Transform.rotation, forward);
                    var axis2 = GetSignedAxisVectorToDirection(bone.Transform.rotation, up);

                    var targetAxis1 = GetSignedAxisVectorToDirection(targetBone.Transform.rotation, forward);
                    var targetAxis2 = GetSignedAxisVectorToDirection(targetBone.Transform.rotation, up);

                    bone.Forward = axis1;
                    bone.Up = axis2;
                    bone.OtherForward = targetAxis1;
                    bone.OtherUp = targetAxis2;
                }
            }
        }

        public static Quaternion MatchRotation(Quaternion targetRotation, Vector3 targetforwardAxis, Vector3 targetUpAxis, Vector3 forwardAxis, Vector3 upAxis)
        {
            Quaternion f = Quaternion.LookRotation(forwardAxis, upAxis);
            Quaternion fTarget = Quaternion.LookRotation(targetforwardAxis, targetUpAxis);

            Quaternion d = targetRotation * fTarget;
            return d * Quaternion.Inverse(f);
        }


        private HVRPosableFingerData MirrorFinger(HVRPosableFinger finger, HVRJointMirrorSetting mirrorOverride, List<HVRJointMirrorSetting> settings)
        {
            var fingerData = new HVRPosableFingerData();

            for (var i = 0; i < finger.Bones.Count; i++)
            {
                var bone = finger.Bones[i];
                var boneData = new HVRPosableBoneData();



                HVRJointMirrorSetting mirror = null;


                if (settings != null && i < settings.Count)
                {
                    mirror = settings[i];
                }
                else if (mirrorOverride != null)
                {
                    mirror = mirrorOverride;
                }

                if (mirror != null)
                {
                    var euler = bone.Transform.localRotation.eulerAngles;

                    var xAngle = euler.x;
                    var yAngle = euler.y;
                    var zAngle = euler.z;

                    MirrorRotation(mirror.XRotation, ref xAngle);
                    MirrorRotation(mirror.YRotation, ref yAngle);
                    MirrorRotation(mirror.ZRotation, ref zAngle);

                    boneData.Position = bone.Transform.localPosition;
                    boneData.Rotation = Quaternion.Euler(xAngle, yAngle, zAngle);

                    if (mirror.XPosition == FingerMirrorPosition.Opposite)
                    {
                        boneData.Position.x *= -1f;
                    }

                    if (mirror.YPosition == FingerMirrorPosition.Opposite)
                    {
                        boneData.Position.y *= -1f;
                    }

                    if (mirror.ZPosition == FingerMirrorPosition.Opposite)
                    {
                        boneData.Position.z *= -1f;
                    }

                }
                else
                {
                    boneData.Position = bone.Transform.localPosition * -1;
                    boneData.Rotation = bone.Transform.localRotation;
                }

                fingerData.Bones.Add(boneData);
            }

            return fingerData;
        }

        private void MirrorRotation(FingerMirrorRotation option, ref float angle)
        {
            switch (option)
            {
                case FingerMirrorRotation.Minus180:
                    angle -= 180f;
                    break;
                case FingerMirrorRotation.Plus180:
                    angle += 180f;
                    break;
                case FingerMirrorRotation.Same:
                    break;
                case FingerMirrorRotation.Opposite:
                    angle *= -1f;
                    break;
                case FingerMirrorRotation.Neg180Minus:
                    angle = -180f - angle;
                    break;
                case FingerMirrorRotation.P180Minus:
                    angle = 180 - angle;
                    break;

            }
        }

        public void Serialize(byte[] buffer)
        {
            using (var ms = new MemoryStream(buffer))
            {
                using (var writer = new BinaryWriter(ms))
                {
                    writer.Write(transform.localPosition.x);
                    writer.Write(transform.localPosition.y);
                    writer.Write(transform.localPosition.z);

                    writer.Write(transform.localRotation.x);
                    writer.Write(transform.localRotation.y);
                    writer.Write(transform.localRotation.z);
                    writer.Write(transform.localRotation.w);

                    writer.Write(Fingers.Length);

                    foreach (var finger in Fingers)
                    {
                        writer.Write(finger.Bones.Count);

                        foreach (var bone in finger.Bones)
                        {
                            writer.Write(bone.Transform.localPosition.x);
                            writer.Write(bone.Transform.localPosition.y);
                            writer.Write(bone.Transform.localPosition.z);

                            writer.Write(bone.Transform.localRotation.x);
                            writer.Write(bone.Transform.localRotation.y);
                            writer.Write(bone.Transform.localRotation.z);
                            writer.Write(bone.Transform.localRotation.w);
                        }
                    }
                }
            }
        }

        public void FromByteArray(byte[] bytes, HVRHandSide side)
        {
            using (var ms = new MemoryStream(bytes))
            {
                using (var reader = new BinaryReader(ms))
                {
                    transform.localPosition = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    transform.localRotation = new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                    var fingers = reader.ReadInt32();

                    for (var i = 0; i < fingers; i++)
                    {
                        var finger = Fingers[i];

                        var bones = reader.ReadInt32();

                        for (var j = 0; j < bones; j++)
                        {
                            var bone = finger.Bones[j];
                            bone.Transform.localPosition = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                            bone.Transform.localRotation = new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        }
                    }
                }
            }
        }
    }

    [Serializable]
    public class HVRPosableFinger
    {
        public Transform Root;
        public Transform Tip;
        public List<HVRPosableBone> Bones = new List<HVRPosableBone>();

        public HVRPosableFingerData GetFingerData()
        {
            var finger = new HVRPosableFingerData
            {
                Bones = new List<HVRPosableBoneData>()
            };

            foreach (var bone in Bones)
            {
                finger.Bones.Add(bone.GetBoneData());
            }

            return finger;
        }
    }

    [Serializable]
    public class HVRPosableBone
    {
        public Transform Transform;

        public HVRPosableBoneData GetBoneData()
        {
            var bone = new HVRPosableBoneData();
            bone.Position = Transform.localPosition;
            bone.Rotation = Transform.localRotation;
            return bone;
        }


        public Vector3 Forward;
        public Vector3 Up;

        public Vector3 OtherForward;
        public Vector3 OtherUp;
    }

}