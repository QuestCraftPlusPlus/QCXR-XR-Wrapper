using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate functions of account request
    /// </summary>
    public static class Account
    {
        /// <summary>
        /// Get logged in user
        /// </summary>
        /// <returns></returns>
        public static YVRRequest<AccountData> GetLoggedInUser()
        {
            if (!YVRPlatform.IsInitialized)
                return null;

            return new YVRRequest<AccountData>(YVRPlatform.YVR_Account_GetLoggedInUser());
        }
    }
}