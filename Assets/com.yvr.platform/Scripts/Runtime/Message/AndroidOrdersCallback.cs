using Newtonsoft.Json;
using System;
using UnityEngine;

namespace YVR.Platform
{
    public class AndroidOrdersCallback : AndroidJavaProxy
    {
        public Action<OrdersListResponse> OrdersCallback;
        public AndroidOrdersCallback() : base("com.yvr.loginpay.ResultCallback") { }
        public void onResult(string jsonInfo)
        {
            OrdersListResponse ordersListResponse = JsonConvert.DeserializeObject<OrdersListResponse>(jsonInfo);
            OrdersCallback?.Invoke(ordersListResponse);
        }
    }
}