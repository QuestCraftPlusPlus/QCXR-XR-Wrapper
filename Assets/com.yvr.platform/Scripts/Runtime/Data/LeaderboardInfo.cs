using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate properties of Leaderboard info
    /// </summary>
    public class LeaderboardInfo
    {
        /// <summary>
        /// The current page number
        /// </summary>
        public readonly int currentPageIndex;

        /// <summary>
        /// Maximum number of page
        /// </summary>
        public readonly int pageSize;

        /// <summary>
        /// Total number of pages
        /// </summary>
        public readonly int totalPages;

        /// <summary>
        /// Total number of learderboard
        /// </summary>
        public readonly int totalCount;

        /// <summary>
        /// The number of pages currently
        /// </summary>
        public readonly int currentPageSize;

        private readonly AndroidJavaObject usingRankList;

        /// <summary>
        /// The leaderboard entry info for the current page
        /// </summary>
        public readonly List<LeaderboardEntryInfo> rankList;

        public LeaderboardInfo(AndroidJavaObject obj)
        {
            this.currentPageIndex = YVRPlatform.YVR_Leaderboard_GetCurrentPageIndex(obj);
            this.pageSize = YVRPlatform.YVR_Leaderboard_GetPageSize(obj);
            this.totalPages = YVRPlatform.YVR_Leaderboard_GetTotalPages(obj);
            this.totalCount = YVRPlatform.YVR_Leaderboard_GetTotalCount(obj);
            this.usingRankList = YVRPlatform.YVR_Leaderboard_GetRankList(obj);
            this.currentPageSize = YVRPlatform.YVR_Leaderboard_GetRankListSize(usingRankList);

            rankList = new List<LeaderboardEntryInfo>();
            for (int i = 0; i < currentPageSize; i++)
            {
                rankList.Add(new LeaderboardEntryInfo(YVRPlatform.YVR_Leaderboard_GetRankItem(usingRankList, i)));
            }
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append($"currentPageIndex:{currentPageIndex}\n");
            str.Append($"pageSize:{pageSize}\n");
            str.Append($"totalPages:{totalPages}\n");
            str.Append($"totalCount:{totalCount}\n");
            str.Append($"currentPageSize:{currentPageSize}\n");
            if (rankList != null)
            {
                for (int i = 0; i < rankList.Count; i++)
                {
                    str.Append($"rankList:{rankList[i]}\n");
                }
            }

            return str.ToString();
        }
    }
}