namespace YVR.Utilities
{
    /// <summary>
    /// Dummy LogPrefix which will not add any prefix
    /// </summary>
    public class DummyLogPrefix : LogPrefixBase
    {
        /// <summary>
        /// Function to get prefix
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="log"> The log message </param>
        /// <param name="priority"> Log Priority </param>
        /// <returns>Empty string</returns>
        public override string GetPrefix(object context, string log, LogPriority priority)
        {
            return string.Empty;
        }
    }
}
