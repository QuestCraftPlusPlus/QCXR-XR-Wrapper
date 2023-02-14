#if HVR_OCULUS
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Oculus
{
    public class HVROculusHandAnimator : MonoBehaviour
    {
        public UnityEvent HandTrackingEnabled = new UnityEvent();
        public UnityEvent HandTrackingDisabled = new UnityEvent();

        public OVRSkeleton Skeleton;
        
        public OVRSkeleton.BoneId HandBoneId = OVRSkeleton.BoneId.Hand_Start;
        public Vector3 HandRotationOffset;
        public Vector3 HandPositionOffset;
        public Transform OffsetTarget;

        public List<HVROculusBone> Bones = new List<HVROculusBone>();
        private HVROculusBone[] _boneMap;

        private Vector3 _targetOriginalPosition;
        private Quaternion _targetOriginalRotation;
        private bool _previousIsHandTracking;

        private void Start()
        {
            _boneMap = new HVROculusBone[(int)OVRSkeleton.BoneId.Max];
            foreach (var bone in Bones)
            {
                var index = (int)bone.OVRSkeletonBoneId;
                if (_boneMap[index] != null)
                {
                    Debug.LogWarning($"{bone.OVRSkeletonBoneId} has already been mapped!");
                }

                _boneMap[index] = bone;
            }

            if (OffsetTarget)
            {
                _targetOriginalPosition = OffsetTarget.localPosition;
                _targetOriginalRotation = OffsetTarget.localRotation;
            }
        }

        private void LateUpdate()
        {
            if (Skeleton && Skeleton.IsInitialized && Skeleton.IsDataValid)
            {
                if (!_previousIsHandTracking)
                {
                    HandTrackingEnabled.Invoke();
                    _previousIsHandTracking = true;
                }

                if (OffsetTarget)
                {
                    OffsetTarget.localRotation = Quaternion.Euler(HandRotationOffset);
                    OffsetTarget.localPosition = HandPositionOffset;
                }

                for (int i = 0; i < Skeleton.Bones.Count; i++)
                {
                    var ovrBone = Skeleton.Bones[i];
                    var index = (int)ovrBone.Id;
                    var bone = _boneMap[index];

                    //unity seems to fill my null array slots with empty objects?
                    if (bone != null && bone.Transform)
                    {
                        bone.Transform.localRotation = bone.RotationOffset * ovrBone.Transform.localRotation;
                    }
                }
            }
            else
            {
                if (_previousIsHandTracking)
                {
                    HandTrackingDisabled.Invoke();
                    _previousIsHandTracking = false;
                }

                if (OffsetTarget)
                {
                    OffsetTarget.localRotation = _targetOriginalRotation;
                    OffsetTarget.localPosition = _targetOriginalPosition;
                }
            }
        }
    }

    [Serializable]
    public class HVROculusBone
    {
        public OVRSkeleton.BoneId OVRSkeletonBoneId;
        public Transform Transform;
        public Vector3 RotationOffsetEuler;
        public Quaternion RotationOffset => Quaternion.Euler(RotationOffsetEuler);
    }
}
#endif