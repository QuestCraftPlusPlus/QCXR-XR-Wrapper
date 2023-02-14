using UnityEngine;

namespace HurricaneVR.Framework.Core.HandPoser
{
    public class HVRIKTargets : MonoBehaviour
    {
        public Transform LeftTarget;
        public Transform RightTarget;

        public bool IsPoser { get; set; }
    }
}