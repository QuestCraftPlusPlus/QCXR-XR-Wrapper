using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    public static class LoginPay
    {
        /// <summary>
        /// Initialize the login pay sdk
        /// </summary>
        /// <param name="appID"></param>
        public static void Initialize(long appID)
        {
            if (!IsYVRUserLogin(appID))
            {
                Debug.LogWarning("User is not logged in!");
            }
        }

        /// <summary>
        /// Enable login pay log
        /// </summary>
        public static void LoginPaySetDebug()
        {
            YVRPlatform.YVR_LoginPay_SetDebug();
        }

        /// <summary>
        /// Whether the account is logged in.
        /// Only the login status can make the order payment; Before placing an order, call this interface to determine the login status;
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static bool IsYVRUserLogin(long appId)
        {
            return YVRPlatform.YVR_LoginPay_IsYvrUserLogin(appId);
        }

        /// <summary>
        /// Get information about your account
        /// </summary>
        public static void GetLogInUserInfo(long appId,Action<string> userInfoCallback)
        {
            YVRPlatform.YVR_LoginPay_ReqLogInUserInfo(appId,userInfoCallback);
        }

        /// <summary>
        /// Get a list of available items for purchase
        /// </summary>
        public static void GetGoodsList(Action<GoodsListResponse> goodsListCallback)
        {
            YVRPlatform.YVR_LoginPay_ReqGoodsList(goodsListCallback);
        }

        /// <summary>
        /// Place an order to pay
        /// </summary>
        /// <param name="sku">googs sku</param>
        /// <param name="count">purchase quantity</param>
        /// <param name="cpOrderNo">order number</param>
        /// <param name="payCallback"></param>
        public static void Pay(string sku,int count,string cpOrderNo,Action<PayResponse> payCallback)
        {
            YVRPlatform.YVR_LoginPay_Pay(sku,count,cpOrderNo,payCallback);
        }

        /// <summary>
        /// Get user order records
        /// </summary>
        /// <param name="orderNo">order number. If you pass empty, it means that all orders of the user are queried</param>
        /// <param name="ordersCallback"></param>
        public static void GetOrders(string orderNo,Action<OrdersListResponse> ordersCallback)
        {
            YVRPlatform.YVR_LoginPay_GetOrders(orderNo, ordersCallback);
        }
    }
}