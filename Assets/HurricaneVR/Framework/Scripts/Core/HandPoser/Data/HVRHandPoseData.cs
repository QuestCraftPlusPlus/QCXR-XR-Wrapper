using System;
using System.Collections.Generic;
using System.IO;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Core.HandPoser.Data
{
    [Serializable]
    public class HVRHandPoseData
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public HVRPosableFingerData Thumb;
        public HVRPosableFingerData Index;
        public HVRPosableFingerData Middle;
        public HVRPosableFingerData Ring;
        public HVRPosableFingerData Pinky;

        private HVRPosableFingerData[] _fingers;

        public HVRPosableFingerData[] Fingers
        {
            get
            {
                if (_fingers == null || _fingers.Length == 0)
                {
                    var fingers = new List<HVRPosableFingerData>();

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

                return _fingers;
            }
        }

        public HVRHandPoseData()
        {
        }

        public HVRHandPoseData DeepCopy()
        {
            var copy = new HVRHandPoseData();

            copy.Position = Position;
            copy.Rotation = Rotation;
            copy.Thumb = Thumb?.DeepCopy();
            copy.Index = Index?.DeepCopy();
            copy.Middle = Middle?.DeepCopy();
            copy.Ring = Ring?.DeepCopy();
            copy.Pinky = Pinky?.DeepCopy();

            return copy;
        }

        public void CopyTo(HVRHandPoseData data)
        {
            data.Position = Position;
            data.Rotation = Rotation;

            for (var i = 0; i < Fingers.Length; i++)
            {
                var finger = Fingers[i];

                for (var j = 0; j < finger.Bones.Count; j++)
                {
                    var bone = finger.Bones[j];

                    data.Fingers[i].Bones[j].Position = bone.Position;
                    data.Fingers[i].Bones[j].Rotation = bone.Rotation;
                }
            }
        }

        public byte[] Serialize()
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms))
                {
                    writer.Write(Position.x);
                    writer.Write(Position.y);
                    writer.Write(Position.z);

                    writer.Write(Rotation.x);
                    writer.Write(Rotation.y);
                    writer.Write(Rotation.z);
                    writer.Write(Rotation.w);

                    writer.Write(Fingers.Length);

                    foreach (var finger in Fingers)
                    {
                        writer.Write(finger.Bones.Count);

                        foreach (var bone in finger.Bones)
                        {
                            writer.Write(bone.Position.x);
                            writer.Write(bone.Position.y);
                            writer.Write(bone.Position.z);

                            writer.Write(bone.Rotation.x);
                            writer.Write(bone.Rotation.y);
                            writer.Write(bone.Rotation.z);
                            writer.Write(bone.Rotation.w);
                        }
                    }

                    return ms.ToArray();
                }
            }
        }

        public static HVRHandPoseData FromByteArray(byte[] bytes, HVRHandSide side)
        {
            var pose = new HVRHandPoseData();

            using (var ms = new MemoryStream(bytes))
            {
                using (var reader = new BinaryReader(ms))
                {
                    pose.Position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    pose.Rotation = new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                    var fingers = reader.ReadInt32();

                    for (var i = 0; i < fingers; i++)
                    {
                        var finger = new HVRPosableFingerData();
                        finger.Bones = new List<HVRPosableBoneData>();

                        switch (i)
                        {
                            case 0:
                                pose.Thumb = finger;
                                break;
                            case 1:
                                pose.Index = finger;
                                break;
                            case 2:
                                pose.Middle = finger;
                                break;
                            case 3:
                                pose.Ring = finger;
                                break;
                            case 4:
                                pose.Pinky = finger;
                                break;
                        }

                        var bones = reader.ReadInt32();
                        for (var j = 0; j < bones; j++)
                        {
                            var bone = new HVRPosableBoneData();
                            finger.Bones.Add(bone);
                            bone.Position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                            bone.Rotation = new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        }
                    }
                }
            }

            return pose;
        }
    }
}