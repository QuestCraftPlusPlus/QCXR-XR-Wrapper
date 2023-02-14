using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace HurricaneVR.Framework.Core.ScriptableObjects
{
    [CreateAssetMenu(menuName = "HurricaneVR/Hand Strength", fileName = "HandStrength")]
    public class PDStrength : ScriptableObject
    {
        public JointApply Mode = JointApply.PositionAndRotation;

        //[Header("Force")]
        public float Spring = 3000f;
        public float Damper = 300f;
        public float MaxForce = 300f;

        //[Header("Torque")]
        public float TorqueSpring = 500f;
        public float TorqueDamper = 50f;
        public float MaxTorque = 75f;
    }
}