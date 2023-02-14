using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Runs callback automatically
    /// </summary>
    public class YVRCallbackRunner : MonoBehaviour
    {
        /// <summary>
        /// Decide whether this object destroy when scene changes
        /// </summary>
        public bool dontDestroyOnLoad = true;

        private void Awake()
        {
            YVRCallbackRunner[] existingRunner = GameObject.FindObjectsOfType<YVRCallbackRunner>();

            if (existingRunner.Length > 1)
            {
                Debug.LogWarning("[YVRPlatform] There are more than one instance of YVRCallbackRunner in scene");
            }

            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Update()
        {
            YVRCallback.RunCallbacks();
        }

        private void OnApplicationQuit()
        {
            YVRCallback.OnApplicationQuit();
        }
    }
}