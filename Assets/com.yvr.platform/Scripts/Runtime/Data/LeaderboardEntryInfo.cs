using System;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Leaderboard entry info
    /// </summary>
    public class LeaderboardEntryInfo
    {
        /// <summary>
        /// The leaderboard id
        /// </summary>
        public readonly long leaderboardApiId;

        /// <summary>
        /// User id
        /// </summary>
        public readonly long id;

        /// <summary>
        /// User name
        /// </summary>
        public readonly string name;

        /// <summary>
        /// User icon
        /// </summary>
        public readonly string icon;

        /// <summary>
        /// User score
        /// </summary>
        public readonly double score;

        /// <summary>
        /// User extra data
        /// </summary>
        public readonly sbyte[] extraData;

        /// <summary>
        /// Extra data length
        /// </summary>
        public readonly long extraDataLength;

        /// <summary>
        /// LeaderboardEntryInfo sort
        /// </summary>
        public readonly long sort;

        /// <summary>
        /// LeaderboardEntryInfo add time
        /// </summary>
        public readonly string addTime;

        /// <summary>
        /// LeaderboardEntryInfo update time
        /// </summary>
        public readonly string updateTime;

        /// <summary>
        ///  Update policy
        /// </summary>
        public readonly LeaderboardUpdatePolicy updatePolicy;

        public LeaderboardEntryInfo(AndroidJavaObject obj)
        {
            this.leaderboardApiId = YVRPlatform.YVR_Leaderboard_GetAppIdByRank(obj);
            this.id = YVRPlatform.YVR_Leaderboard_GetUserIDByRank(obj);
            this.name = YVRPlatform.YVR_Leaderboard_GetUserNameByRank(obj);
            this.icon = YVRPlatform.YVR_Leaderboard_GetUserIconByRank(obj);
            this.score = YVRPlatform.YVR_Leaderboard_GetScoreByRank(obj);
            this.extraData = YVRPlatform.YVR_Leaderboard_GetExtraDataByRank(obj);
            this.extraDataLength = YVRPlatform.YVR_Leaderboard_GetExtraLengthByRank(obj);
            this.sort = YVRPlatform.YVR_Leaderboard_GetSortByRank(obj);
            this.addTime = YVRPlatform.YVR_Leaderboard_GetAddTimeByRank(obj);
            this.updateTime = YVRPlatform.YVR_Leaderboard_GetUpdateTimeByRank(obj);
            this.updatePolicy = (LeaderboardUpdatePolicy)YVRPlatform.YVR_Leaderboard_GetUpdatePolicyByRank(obj);
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append($"leaderboardApiId:{leaderboardApiId}\n");
            str.Append($"id:{id}\n");
            str.Append($"name:{name}\n");
            str.Append($"icon:{icon}\n");
            str.Append($"score:{score}\n");
            if (extraDataLength > 0 && extraData?.Length > 0)
                str.Append($"extraData:{System.Text.Encoding.UTF8.GetString(Array.ConvertAll(extraData, (a) => (byte)a))}\n");
            else
                str.Append($"extraData: null\n");

            str.Append($"extraDataLength:{extraDataLength}\n");
            str.Append($"sort:{sort}\n");
            str.Append($"addTime:{addTime}\n");
            str.Append($"updateTime:{updateTime}\n");
            str.Append($"updatePolicy:{updatePolicy}\n");
            return str.ToString();
        }
    }
}