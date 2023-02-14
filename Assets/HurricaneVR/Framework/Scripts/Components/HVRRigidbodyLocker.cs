using System;
using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Helps constrain loose joints on the desired axis.
    /// </summary>
    public class HVRRigidbodyLocker : MonoBehaviour
    {
        public LockOptions Locks;

        private Vector3 _position;
        private Vector3 _rotation;

        public void Start()
        {
            _position = transform.localPosition;
            _rotation = transform.localEulerAngles;
        }

        public void Update()
        {
            Lock();
        }

        private void Lock()
        {
            var position = transform.localPosition;
            var rotation = transform.localEulerAngles;

            if ((Locks & LockOptions.PosX) == LockOptions.PosX)
            {
                position.x = _position.x;
            }

            if ((Locks & LockOptions.PosY) == LockOptions.PosY)
            {
                position.y = _position.y;
            }

            if ((Locks & LockOptions.PosZ) == LockOptions.PosZ)
            {
                position.z = _position.z;
            }

            if ((Locks & LockOptions.RotX) == LockOptions.RotX)
            {
                rotation.x = _rotation.x;
            }

            if ((Locks & LockOptions.RotY) == LockOptions.RotY)
            {
                rotation.y = _rotation.y;
            }

            if ((Locks & LockOptions.RotZ) == LockOptions.RotZ)
            {
                rotation.z = _rotation.z;
            }

            transform.localPosition = position;
            transform.localEulerAngles = rotation;
        }

        public void LateUpdate()
        {
            Lock();
        }
    }

    [Flags]
    public enum LockOptions
    {
        None = 0,
        PosX = 1,
        PosY = 2,
        PosZ = 4,
        RotX = 8,
        RotY = 16,
        RotZ = 32
    }
}