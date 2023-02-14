using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    public class AndroidUserInfoCallback : AndroidJavaProxy
    {
        public Action<string> UserInfoCallback;
        public AndroidUserInfoCallback() : base("com.yvr.loginpay.ResultCallback") { }

        public void onResult(string jsonUserInfo)
        {
            UserInfoCallback?.Invoke(jsonUserInfo);
        }
    }
}