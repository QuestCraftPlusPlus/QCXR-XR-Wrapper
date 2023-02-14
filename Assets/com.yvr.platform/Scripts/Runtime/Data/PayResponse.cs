using Unity.Plastic.Newtonsoft.Json;

namespace YVR.Platform
{
    public class PayResponse
    {
        public int errCode;
        public string errMsg;
        [JsonProperty("data")]
        public PayContent payContent;
    }

    public class PayContent
    {
        [JsonProperty("content")]
        public PayResult payResult;
    }

    public class PayResult
    {
        public string cpOrderNo;
        public string orderNo;

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(string.Format("cpOrderNo:{0},\n", cpOrderNo));
            str.Append(string.Format("orderNo:{0},\n", orderNo));
            return str.ToString();
        }
    }
}