using System;
using System.Collections.Generic;

namespace HurricaneVR.Framework.Core.HandPoser.Data
{
    [Serializable]
    public class HVRPosableFingerData
    {
        public List<HVRPosableBoneData> Bones = new List<HVRPosableBoneData>();

        public HVRPosableFingerData DeepCopy()
        {
            var finger = new HVRPosableFingerData();
            foreach (var bone in Bones)
            {
                finger.Bones.Add(bone.DeepCopy());
            }

            return finger;
        }
    }
}