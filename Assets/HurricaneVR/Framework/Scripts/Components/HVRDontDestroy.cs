using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    public class HVRDontDestroy : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
