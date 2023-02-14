using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Components.Creators
{
    public class HVRPhysicsLeverCreator : MonoBehaviour
    {
        public Transform Anchor;
        public Transform Lever;
        public HVRAxis Axis;
        public float Mass = 5f;
        public float MinAngle = 90f;
        public float MaxAngle = 90f;
        public float GrabbedDamper = 3f;
        public float Damper = 10f;
    }
}