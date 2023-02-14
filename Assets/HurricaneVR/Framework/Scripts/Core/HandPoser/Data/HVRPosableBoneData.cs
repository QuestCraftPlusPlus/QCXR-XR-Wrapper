using System;
using UnityEngine;

namespace HurricaneVR.Framework.Core.HandPoser.Data
{
    [Serializable]
    public class HVRPosableBoneData
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public HVRPosableBoneData DeepCopy()
        {
            return new HVRPosableBoneData()
            {
                Position = Position,
                Rotation = Rotation
            };
        }
    }
}