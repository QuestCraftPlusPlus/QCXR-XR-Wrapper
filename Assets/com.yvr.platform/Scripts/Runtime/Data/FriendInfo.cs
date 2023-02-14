using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate properties of friend info
    /// </summary>
    public class FriendInfo
    {
        /// <summary>
        /// Friend's account ID
        /// </summary>
        public readonly int accountID;
        /// <summary>
        /// Friend's nickname
        /// </summary>
        public readonly string nickname;
        /// <summary>
        /// Friend's avatar url
        /// </summary>
        public readonly string avatar;
        /// <summary>
        /// Friend's gender, 1:man, 2:women, 3:unknow
        /// </summary>
        public readonly int gender;
        /// <summary>
        /// Friend's age
        /// </summary>
        public readonly int age;
        /// <summary>
        /// Friend's online state, 1:online, 2:offline
        /// </summary>
        public readonly int onlineState;

        public FriendInfo(AndroidJavaObject obj)
        {
            this.accountID = YVRPlatform.YVR_Friends_GetActIdOfUser(obj);
            this.nickname = YVRPlatform.YVR_Friends_GetNickOfUser(obj);
            this.avatar = YVRPlatform.YVR_Friends_GetIconOfUser(obj);
            this.gender = YVRPlatform.YVR_Friends_GetSexOfUser(obj);
            this.age = YVRPlatform.YVR_Friends_GetAgeOfUser(obj);
            this.onlineState = YVRPlatform.YVR_Friends_GetOnlineOfUser(obj);
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(string.Format("accountID:¡¾{0}¡¿,\n\r", accountID));
            str.Append(string.Format("nickname:¡¾{0}¡¿,\n\r", nickname ?? "null"));
            str.Append(string.Format("avatar:¡¾{0}¡¿,\n\r", avatar ?? "null"));
            str.Append(string.Format("gender:¡¾{0}¡¿,\n\r", gender));
            str.Append(string.Format("age:¡¾{0}¡¿,\n\r", age));
            str.Append(string.Format("onlineState:¡¾{0}¡¿,\n\r", onlineState));
            return str.ToString();
        }
    }
}