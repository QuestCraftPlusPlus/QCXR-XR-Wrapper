using YVR.Platform;

namespace YVR.Platform
{
    /// <summary>
    /// Leaderboard by rank request data
    /// </summary>
    public class LeaderboardByRank
    {
        /// <summary>
        /// The number of data start
        /// </summary>
        public long currentStart;

        /// <summary>
        /// Data direction: forward/backward
        /// </summary>
        public LeaderboardDataDirection dataDirection;

        /// <summary>
        /// Page size
        /// </summary>
        public long size;

        /// <summary>
        /// Leaderboard name
        /// </summary>
        public string leaderboardApiName;

        /// <summary>
        /// Page type, all data: none, friends data: friends
        /// </summary>
        public LeaderboardPageType pageType;
    }
}