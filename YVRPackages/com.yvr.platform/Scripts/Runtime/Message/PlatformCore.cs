using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate functions of account Entitlement
    /// </summary>
    public static class PlatformCore
    {
        /// <summary>
        /// Get User Entitled state
        /// </summary>
        /// <returns></returns>
        public static YVRRequest<Entitlement> GetViewerEntitled()
        {
            if (!YVRPlatform.IsInitialized)
                return null;

            return new YVRRequest<Entitlement>(YVRPlatform.YVR_Permission_GetViewerEntitled());
        }
    }
}
