using UnityEngine;
using System;
using System.Collections.Generic;

namespace YVR.Platform
{
    /// <summary>
    /// Base class of yvrmessage class
    /// </summary>
    public class YVRMessage
    {
        /// <summary>
        /// Message type
        /// </summary>
        public string type { protected set; get; }

        /// <summary>
        /// Request ID of this message
        /// </summary>
        public int requestID { protected set; get; }

        /// <summary>
        /// Error data
        /// </summary>
        protected YVRError error;

        /// <summary>
        /// Whether this message is error
        /// </summary>
        public bool isError { get { return error != null; } }

        /// <summary>
        /// Excute callback function when handle message
        /// </summary>
        /// <param name="message"></param>
        public delegate void Callback(YVRMessage message);

        internal YVRMessage(AndroidJavaObject obj)
        {
            this.requestID = YVRPlatform.YVR_Message_GetRequestID(obj);
            this.type = YVRPlatform.YVR_Message_GetRequestType(obj);
            bool isError = YVRPlatform.YVR_Message_IsError(obj);

            if (isError)
            {
                this.error = new YVRError(
                    YVRPlatform.YVR_Error_GetErrorCode(obj),
                    YVRPlatform.YVR_Error_GetErrorMessage(obj));

                Debug.LogErrorFormat("[YVRPlatform] Message is Error. \nErrorCode : {0} \nErrorMessage : {1}", error.errorCode, error.errorMsg);
            }
        }

        internal static YVRMessage PopMessage()
        {
            AndroidJavaObject obj = YVRPlatform.YVR_PopMessage();

            if (obj == null)
                return null;

            YVRMessage msg = ParseMessageObj(obj);

            YVRPlatform.YVR_Message_FreeRequest(msg.requestID);

            return msg;
        }

        internal static YVRMessage ParseMessageObj(AndroidJavaObject obj)
        {
            if (obj == null)
            {
                return null;
            }

            string msgType = YVRPlatform.YVR_Message_GetRequestType(obj);
            YVRMessage msg = null;

            Debug.Log("message type: " + msgType);

            switch (msgType)
            {
                case "reqUserInfo":
                    msg = new YVRMessage_Account(obj);
                    break;
                case "addAchieve":
                    msg = new YVRMessage(obj);
                    break;
                case "queryAchieveDefinition":
                    msg = new YVRMessage_AchievementDefinitionList(obj);
                    break;
                case "queryAchieveProgress":
                    msg = new YVRMessage_AchievementProgressList(obj);
                    break;
                case "reqIsUserEntitled":
                    msg = new YVRMessage_Entitlement(obj);
                    break;
                case "getFriends":
                    msg = new YVRMessage_FriendsList(obj);
                    break;
                case "getUserInfo":
                    msg = new YVRMessage_FriendInfo(obj);
                    break;
                case "reqLeaderboard":
                    msg = new YVRMessage_LeaderboardData(obj);
                    break;
                case "addItem":
                    msg = new YVRMessage(obj);
                    break;
                default:
                    msg = new YVRMessage(obj);
                    break;
            }

            return msg;
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(string.Format("Type:【{0}】,\n\r", type ?? "null"));
            str.Append(string.Format("RequestID:【{0}】,\n\r", requestID));
            str.Append(string.Format("Error:【{0}】,\n\r", error != null ? error.ToString() : "null"));
            return str.ToString();
        }
    }

    /// <summary>
    /// Generics class of yvrmessage class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class YVRMessage<T> : YVRMessage
    {
        /// <summary>
        /// Excute callback function when handle message
        /// </summary>
        /// <param name="msg"></param>
        public new delegate void Callback(YVRMessage<T> msg);

        /// <summary>
        /// Final data of message
        /// </summary>
        public T data { protected set; get; }

        internal YVRMessage(AndroidJavaObject obj) : base(obj)
        {
            if (!isError)
            {
                this.data = GetDataFromMessage(obj);
            }
        }

        protected abstract T GetDataFromMessage(AndroidJavaObject obj);

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(string.Format("Type:【{0}】,\n\r", type ?? "null"));
            str.Append(string.Format("RequestID:【{0}】,\n\r", requestID));
            str.Append(string.Format("Error:【{0}】,\n\r", error != null ? error.ToString() : "null"));
            str.Append(string.Format("data:【{0}】,\n\r", data != null ? data.ToString() : "null"));
            return str.ToString();
        }
    }

    /// <summary>
    /// Generics class of yvrmessage class with the data type : AccountData
    /// </summary>
    public class YVRMessage_Account : YVRMessage<AccountData>
    {
        internal YVRMessage_Account(AndroidJavaObject obj) : base(obj) { }
        protected override AccountData GetDataFromMessage(AndroidJavaObject obj)
        {
            return new AccountData(obj);
        }
    }

    /// <summary>
    /// Generics class of yvrmessage class with the data type : AchievementDefinitionList
    /// </summary>
    public class YVRMessage_AchievementDefinitionList : YVRMessage<AchievementDefinitionList>
    {
        internal YVRMessage_AchievementDefinitionList(AndroidJavaObject obj) : base(obj) { }
        protected override AchievementDefinitionList GetDataFromMessage(AndroidJavaObject obj)
        {
            return new AchievementDefinitionList(obj);
        }
    }

    /// <summary>
    /// Generics class of yvrmessage class with the data type : AchievementProgressList
    /// </summary>
    public class YVRMessage_AchievementProgressList : YVRMessage<AchievementProgressList>
    {
        internal YVRMessage_AchievementProgressList(AndroidJavaObject obj) : base(obj) { }
        protected override AchievementProgressList GetDataFromMessage(AndroidJavaObject obj)
        {
            return new AchievementProgressList(obj);
        }
    }

    /// <summary>
    /// Generics class of yvrmessage class with the data type : Entitlement
    /// </summary>
    public class YVRMessage_Entitlement : YVRMessage<Entitlement>
    {
        internal YVRMessage_Entitlement(AndroidJavaObject obj) : base(obj) { }
        protected override Entitlement GetDataFromMessage(AndroidJavaObject obj)
        {
            return new Entitlement(obj);
        }
    }

    /// <summary>
    /// Generics class of yvrmessage class with the data type : FriendsList
    /// </summary>
    public class YVRMessage_FriendsList : YVRMessage<FriendsList>
    {
        internal YVRMessage_FriendsList(AndroidJavaObject obj) : base(obj) { }
        protected override FriendsList GetDataFromMessage(AndroidJavaObject obj)
        {
            return new FriendsList(obj);
        }
    }

    /// <summary>
    /// Generics class of yvrmessage class with the data type : FriendInfo
    /// </summary>
    public class YVRMessage_FriendInfo : YVRMessage<FriendInfo>
    {
        internal YVRMessage_FriendInfo(AndroidJavaObject obj) : base(obj) { }
        protected override FriendInfo GetDataFromMessage(AndroidJavaObject obj)
        {
            return new FriendInfo(obj);
        }
    }

    public class YVRMessage_LeaderboardData : YVRMessage<LeaderboardInfo>
    {
        internal YVRMessage_LeaderboardData(AndroidJavaObject obj) : base(obj) { }

        protected override LeaderboardInfo GetDataFromMessage(AndroidJavaObject obj)
        {
            return new LeaderboardInfo(obj);
        }
    }
}