using Newtonsoft.Json;
using System;
using UnityEngine;

namespace YVR.Platform
{
    public class AndroidPayCallback : AndroidJavaProxy
    {
        public Action<PayResponse> PayResoutCallback;
        public AndroidPayCallback() : base("com.yvr.loginpay.ResultCallback") {}

        public void onResult(string jsonInfo)
        {
            PayResponse ordersListResponse = JsonConvert.DeserializeObject<PayResponse>(jsonInfo);
            PayResoutCallback?.Invoke(ordersListResponse);
        }
    }
}