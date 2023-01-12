using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    public class OrdersListResponse
    {
        [JsonProperty("data")]
        public OrdersList orderList;
        public int errCode;
        public string errMsg;
    }

    public class OrdersList
    {
        [JsonProperty("content")]
        public List<Order> content { get; set; }
    }

    public class Order
    {
        public string nick;
        public string cpOrderNo;
        public string orderNo;
        public string payTime;
        public float totalPrice;
        public float price;
        public string appName;
        public long appId;
        public long actId;
        public string name;
        public int count;
        public string sku;
        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(string.Format("nick:{0},\n", nick));
            str.Append(string.Format("cpOrderNo:{0},\n", cpOrderNo));
            str.Append(string.Format("orderNo:{0},\n", orderNo));
            str.Append(string.Format("payTime:{0},\n", payTime));
            str.Append(string.Format("totalPrice:{0},\n", totalPrice));
            str.Append(string.Format("price:{0},\n", price));
            str.Append(string.Format("appName:{0},\n", appName));
            str.Append(string.Format("appId:{0},\n", appId));
            str.Append(string.Format("actId:{0},\n", actId));
            str.Append(string.Format("name:{0},\n", name));
            str.Append(string.Format("count:{0},\n", count));
            str.Append(string.Format("sku:{0},\n", sku));
            return str.ToString();
        }
    }
}
