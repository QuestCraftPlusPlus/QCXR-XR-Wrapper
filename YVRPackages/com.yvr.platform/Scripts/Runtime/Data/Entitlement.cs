using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate properties of Entitlement
    /// </summary>
    public class Entitlement
    {
        /// <summary>
        /// Is user entitled to this app
        /// </summary>
        public readonly bool isEntitled;

        public Entitlement(AndroidJavaObject obj)
        {
            isEntitled = YVRPlatform.YVR_Permission_IsViewerEntitled(obj);
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(string.Format("isEntitled:【{0}】,\n\r", isEntitled));
            return str.ToString();
        }
    }
}