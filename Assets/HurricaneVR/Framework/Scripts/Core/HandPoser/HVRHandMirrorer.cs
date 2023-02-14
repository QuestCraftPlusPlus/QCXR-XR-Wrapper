using System;
using HurricaneVR.Framework.Core.HandPoser.Data;
using UnityEngine;

namespace HurricaneVR.Framework.Core.HandPoser
{
    public class HVRHandMirrorer : MonoBehaviour
    {

        public virtual void MirrorFingers(HVRPosableHand hand, HVRHandPoseData clone)
        {
            if (hand.Thumb != null)
            {
                clone.Thumb = MirrorFingerData(hand.Thumb);
            }

            if (hand.Index != null)
            {
                clone.Index = MirrorFingerData(hand.Index);
            }

            if (hand.Middle != null)
            {
                clone.Middle = MirrorFingerData(hand.Middle);
            }

            if (hand.Ring != null)
            {
                clone.Ring = MirrorFingerData(hand.Ring);
            }

            if (hand.Pinky != null)
            {
                clone.Pinky = MirrorFingerData(hand.Pinky);
            }

        }

        public virtual void MirrorFingers(HVRPosableHand source, HVRPosableHand target)
        {
            MirrorFinger(source.Thumb, target.Thumb);
            MirrorFinger(source.Index, target.Index);
            MirrorFinger(source.Middle, target.Middle);
            MirrorFinger(source.Ring, target.Ring);
            MirrorFinger(source.Pinky, target.Pinky);
        }

        public virtual HVRPosableFingerData MirrorFingerData(HVRPosableFinger finger)
        {
            var fingerData = new HVRPosableFingerData();

            for (var i = 0; i < finger.Bones.Count; i++)
            {
                var boneData = new HVRPosableBoneData();

                var bone = finger.Bones[i];

                var rot = MirrorBone(bone, out var pos);
                boneData.Position = pos;
                boneData.Rotation = rot;

                fingerData.Bones.Add(boneData);
            }

            return fingerData;
        }

        public virtual void MirrorFinger(HVRPosableFinger source, HVRPosableFinger target)
        {
            if (source == null || target == null || source.Bones.Count != target.Bones.Count)
                return;

            for (var i = 0; i < source.Bones.Count; i++)
            {
                var sourceBone = source.Bones[i];
                target.Bones[i].Transform.localRotation = MirrorBone(sourceBone, out var pos);
                target.Bones[i].Transform.localPosition = pos;
            }
        }

        protected virtual Quaternion MirrorBone(HVRPosableBone bone, out Vector3 position)
        {
            var cross = Vector3.Cross(bone.Forward, bone.Up);
            var otherCross = Vector3.Cross(bone.OtherForward, bone.OtherUp);


            var forwardAngle = Vector3.Scale(bone.Transform.localEulerAngles, bone.Forward).magnitude;
            var upAngle = Vector3.Scale(bone.Transform.localEulerAngles, bone.Up).magnitude;
            var rightAngle = Vector3.Scale(bone.Transform.localEulerAngles, cross).magnitude;

            position = bone.Transform.localPosition;

            var fp = Vector3.Scale(position, ABS(bone.Forward));
            var up = Vector3.Scale(position, ABS(bone.Up));
            var rp = Vector3.Scale(position, ABS(cross));

            if (Vector3.Dot(bone.Forward, bone.OtherForward) > 0)
            {
                forwardAngle *= -1f;
            }
            else
            {
                fp *= -1f;
            }

            if (Vector3.Dot(bone.Up, bone.OtherUp) > 0)
            {
                upAngle *= -1f;
            }
            else
            {
                up *= -1f;
            }

            if (Vector3.Dot(cross, otherCross) < 0)
            {
                rightAngle *= -1f;
            }
            else
            {
                rp *= -1f;
            }


            var final = forwardAngle * ABS(bone.OtherForward) + upAngle * ABS(bone.OtherUp) + rightAngle * ABS(otherCross);
            position = Vector3.Scale(fp, ABS(bone.OtherForward)) +
                       Vector3.Scale(up, ABS(bone.OtherUp)) +
                       Vector3.Scale(rp, ABS(otherCross));

            var rot = Quaternion.Euler(final);
            return rot;
        }

        public Vector3 ABS(Vector3 v)
        {
            return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }
    }
}