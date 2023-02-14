namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate functions of LeaderBoard request
    /// </summary>
    public static class Leaderboard
    {

        /// <summary>
        /// Get leaderboard info by page
        /// </summary>
        /// <param name="leaderboardByPage">leaderboard request info</param>
        /// <returns></returns>
        public static YVRRequest<LeaderboardInfo> GetLeaderboardInfoByPage(LeaderboardByPage leaderboardByPage)
        {
            if (!YVRPlatform.IsInitialized)
                return null;

            return new YVRRequest<LeaderboardInfo>(YVRPlatform.YVR_Leaderboard_GetList(leaderboardByPage.current,
                0, LeaderboardDataDirection.backward.ToString(), leaderboardByPage.size, leaderboardByPage.leaderboardApiName,
                leaderboardByPage.pageType.ToString(), LeaderboardSortType.none.ToString()));
        }

        /// <summary>
        /// Get leaderboard info by leaderboardEntry
        /// </summary>
        /// <param name="leaderboardByRank"></param>
        /// <returns></returns>
        public static YVRRequest<LeaderboardInfo> GetLeaderboardInfoByRank(LeaderboardByRank leaderboardByRank)
        {
            if (!YVRPlatform.IsInitialized)
                return null;

            return new YVRRequest<LeaderboardInfo>(YVRPlatform.YVR_Leaderboard_GetList(0, leaderboardByRank.currentStart,
                leaderboardByRank.dataDirection == LeaderboardDataDirection.none ? "" : leaderboardByRank.dataDirection.ToString(),
                leaderboardByRank.size, leaderboardByRank.leaderboardApiName,
               leaderboardByRank.pageType.ToString(), LeaderboardSortType.aroundOnView.ToString()));
        }

        /// <summary>
        /// Write a leaderboard entry
        /// </summary>
        /// <param name="leaderboardEntry">leaderboardEntry item info </param>
        /// <returns></returns>
        public static YVRRequest LeaderboardWriteItem(LeaderboardEntry leaderboardEntry)
        {
            if (!YVRPlatform.IsInitialized)
                return null;

            if (leaderboardEntry.extraData == null)
            {
                leaderboardEntry.extraData = new sbyte[] { };
            }

            return new YVRRequest(YVRPlatform.YVR_Leaderboard_AddItem(leaderboardEntry.leaderboardApiName, leaderboardEntry.score,
                leaderboardEntry.extraData, leaderboardEntry.extraDataLength, (int)leaderboardEntry.forceUpdate));
        }
    }
}