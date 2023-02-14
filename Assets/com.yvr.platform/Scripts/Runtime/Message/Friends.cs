using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate functions of friends request
    /// </summary>
    public static class Friends
    {
        /// <summary>
        /// Get friends list
        /// </summary>
        /// <returns></returns>
        public static YVRRequest<FriendsList> GetFriends()
        {
            if (!YVRPlatform.IsInitialized)
                return null;

            return new YVRRequest<FriendsList>(YVRPlatform.YVR_Friends_GetYvrFriends());
        }

        /// <summary>
        /// Get friends infomation
        /// </summary>
        /// <returns></returns>
        public static YVRRequest<FriendInfo> GetFriendInfomation(int accountID)
        {
            if (!YVRPlatform.IsInitialized)
                return null;

            return new YVRRequest<FriendInfo>(YVRPlatform.YVR_Friends_GetYvrFriendInfo(accountID));
        }
    }
}