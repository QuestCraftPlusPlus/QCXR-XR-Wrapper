using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate properties of account data
    /// </summary>
    public class AccountData
    {
        /// <summary>
        /// User's account ID
        /// </summary>
        public readonly int accountID;
        /// <summary>
        /// User's nickname
        /// </summary>
        public readonly string nickname;
        /// <summary>
        /// User's avatar url
        /// </summary>
        public readonly string avatar;
        /// <summary>
        /// User's gender, 1:man, 2:women, 3:unknow
        /// </summary>
        public readonly int gender;

        public AccountData(AndroidJavaObject obj)
        {
            this.accountID = YVRPlatform.YVR_Account_GetAccountID(obj);
            this.nickname = YVRPlatform.YVR_Account_GetUserName(obj);
            this.avatar = YVRPlatform.YVR_Account_GetUserIcon(obj);
            this.gender = YVRPlatform.YVR_Account_GetUserSex(obj);
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(string.Format("accountID:【{0}】,\n\r", accountID));
            str.Append(string.Format("userName:【{0}】,\n\r", nickname ?? "null"));
            str.Append(string.Format("userIcon:【{0}】,\n\r", avatar ?? "null"));
            str.Append(string.Format("userSex:【{0}】,\n\r", gender));
            return str.ToString();
        }
    }
}
