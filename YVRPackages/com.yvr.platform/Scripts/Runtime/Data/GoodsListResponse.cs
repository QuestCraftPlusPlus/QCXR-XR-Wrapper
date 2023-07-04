using Newtonsoft.Json;
using System.Collections.Generic;

namespace YVR.Platform
{
    public class GoodsListResponse
    {
        [JsonProperty("data")]
        public GoodsList goodsList;
        public int errCode;
        public string errMsg;
    }

    public class GoodsList
    {
        [JsonProperty("content")]
        public List<Goods> content { get; set; }
    }

    public class Goods
    {
        public long createTime;
        public float price;
        public long appId;
        public string name;
        public long updateTime;
        public int id;
        public string sku;
        public int type;
        public int status;

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(string.Format("createTime:{0},\n", createTime));
            str.Append(string.Format("price:{0},\n", price));
            str.Append(string.Format("name:{0},\n", name));
            str.Append(string.Format("appId:{0},\n", appId));
            str.Append(string.Format("updateTime:{0},\n", updateTime));
            str.Append(string.Format("id:{0},\n", id));
            str.Append(string.Format("sku:{0},\n", sku));
            str.Append(string.Format("type:{0},\n", type));
            str.Append(string.Format("status:{0},\n", status));
            return str.ToString();
        }
    }
}
