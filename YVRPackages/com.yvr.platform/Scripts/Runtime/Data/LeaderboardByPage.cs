namespace YVR.Platform
{
    /// <summary>
    /// Leaderboard by page request data
    /// </summary>
    public class LeaderboardByPage
    {
        /// <summary>
        /// Current page
        /// </summary>
        public long current;

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

    public enum LeaderboardDataDirection
    {
        none,
        forward,
        backward
    }

    public enum LeaderboardPageType
    {
        none,
        friends
    }

    public enum LeaderboardSortType
    {
        none,
        aroundOnView
    }
}