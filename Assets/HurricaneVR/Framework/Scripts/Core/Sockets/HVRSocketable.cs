using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Sockets
{
    [RequireComponent(typeof(HVRGrabbable))]
    public class HVRSocketable : MonoBehaviour
    {
        public HVRGrabbable Grabbable { get; private set; }

        [Tooltip("Stores relative position and rotation information for any socket type.")]
        public List<SocketPose> Poses = new List<SocketPose>();

        [Tooltip("If your grabbable model is not at 1,1,1 scale. ")]
        public Vector3 CounterScale = Vector3.one;

        [Header("Scaling")] [Tooltip("If true the the renderer bounds at the time of socketing will be used to scale the object in a socket that has scaling enabled.")]
        public bool UseRendererBounds = true;

        [Tooltip("If greater than 0 and UseRendererBounds is disabled, the size used when computing socket scale when socketing into a socket with scale enabled.")]
        public float ScaleSize = 0f;

        [Tooltip("Socket scale factor applied after socketing.")]
        public float SocketScale = 1f;

        [Tooltip("Override renderer bounds when socket is scaling")]
        public BoxCollider ScaleOverride;

        [Header("SFX")] public AudioClip SocketedClip;
        public AudioClip UnsocketedClip;

        [Tooltip("If populated this object cannot be socketed if any of these objects are held.")]
        public HVRGrabbable[] LinkedGrabbables;

        
        [Header("Deprecated")]
        public Transform SocketOrientation;
        
        public bool AnyLinkedGrabbablesHeld
        {
            get
            {
                if (LinkedGrabbables == null || LinkedGrabbables.Length == 0)
                    return false;

                if (LinkedGrabbables[0].IsBeingHeld)
                    return true;

                for (int i = 1; i < LinkedGrabbables.Length; i++)
                {
                    if (LinkedGrabbables[i].IsBeingHeld)
                        return true;
                }

                return false;
            }
        }

        private Dictionary<int, SocketPose> _poseCache = new Dictionary<int, SocketPose>();

        protected virtual void Awake()
        {
            Grabbable = GetComponent<HVRGrabbable>();

            InitializePoses();
        }

        public void InitializePoses()
        {
            foreach (var pose in Poses)
            {
                pose.Init();
            }

            _poseCache = Poses.Where(e => !string.IsNullOrWhiteSpace(e.SocketTag))
                .GroupBy(e=>e.Hash)
                .ToDictionary(e => e.Key, e => e.First());
        }

        public virtual float GetSocketScaleSize(HVRSocket socket)
        {
            Vector3 size;

            if (ScaleOverride)
            {
                size = ScaleOverride.size;
            }
            else
            {
                if (!UseRendererBounds && ScaleSize > 0f)
                {
                    return ScaleSize;
                }

                //making sure the AABB is aligned the same every time before pulling the renderer bounds

                var rot = transform.rotation;

                transform.rotation = Quaternion.identity;

                #if UNITY_EDITOR
                if (!Grabbable)
                {
                    Grabbable = GetComponent<HVRGrabbable>();
                }
                #endif
                
                size = Grabbable.ModelBounds.size;

                transform.rotation = rot;
            }

            var axis = size.x;

            if (size.y > axis) axis = size.y;
            if (size.z > axis) axis = size.z;

            return axis;
        }

        public virtual Vector3 GetPositionOffset(HVRSocket socket)
        {
            if (socket.PoseHash.HasValue && _poseCache.TryGetValue(socket.PoseHash.Value, out var pose))
            {
                return pose.Position;
            }

            return Vector3.zero;
        }

        public virtual Quaternion GetRotationOffset(HVRSocket socket)
        {
            if (socket.PoseHash.HasValue && _poseCache.TryGetValue(socket.PoseHash.Value, out var pose))
            {
                return pose.Rotation;
            }

            return Quaternion.identity;
        }
    }

    [Serializable]
    public class SocketPose
    {
        public string SocketTag;
        public Vector3 Position;
        public Vector3 EulerAngles;

        private Quaternion? _rotation;

        public Quaternion Rotation
        {
            get
            {
                if (!_rotation.HasValue)
                    _rotation = Quaternion.Euler(EulerAngles);
                return _rotation.Value;
            }
            
        }

        public int Hash { get; private set; }

        public void Init()
        {
            if (SocketTag != null)
                Hash = Animator.StringToHash(SocketTag);
        }
    }
}