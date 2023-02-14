using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Helper component to parent this transform to the assigned transform on game start
    /// </summary>
    public class HVRParentOnStart : MonoBehaviour
    {
        public Transform Parent;
        public bool WorldPositionStays = true;

        private void Start()
        {
            if (Parent)
            {
                transform.SetParent(Parent, WorldPositionStays);
            }
        }
    }
}