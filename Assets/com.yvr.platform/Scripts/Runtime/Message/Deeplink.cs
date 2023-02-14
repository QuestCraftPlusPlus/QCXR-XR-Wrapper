using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate functions of deeplink request
    /// </summary>
    public static class Deeplink
    {
        /// <summary>
        /// Does app opened with deeplink param
        /// </summary>
        /// <returns></returns>
        public static bool IsDeeplinkLaunch()
        {
            return YVRPlatform.YVR_Deeplink_IsDeeplinkLaunch();
        }

        /// <summary>
        /// Get deeplink room id
        /// </summary>
        /// <returns></returns>
        public static string GetDeeplinkRoomId()
        {
            return YVRPlatform.YVR_Deeplink_GetDeeplinkRoomId();
        }

        /// <summary>
        /// Get deeplink api name
        /// </summary>
        /// <returns></returns>
        public static string GetDeeplinkApiName()
        {
            return YVRPlatform.YVR_Deeplink_GetDeeplinkApiName();
        }
    }
}