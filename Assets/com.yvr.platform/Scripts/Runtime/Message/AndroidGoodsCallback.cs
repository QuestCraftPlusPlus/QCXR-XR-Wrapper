using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
namespace YVR.Platform
{
    public class AndroidGoodsCallback : AndroidJavaProxy
    {
        public Action<GoodsListResponse> GoodsCallback;
        public AndroidGoodsCallback() : base("com.yvr.loginpay.ResultCallback") {}

        public void onResult(string jsonInfo)
        {
            GoodsListResponse ordersListResponse = JsonConvert.DeserializeObject<GoodsListResponse>(jsonInfo);
            GoodsCallback?.Invoke(ordersListResponse);
        }
    }
}