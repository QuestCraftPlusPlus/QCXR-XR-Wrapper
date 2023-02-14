namespace YVR.Utilities
{
    /// <summary>
    /// Interface that intend to encapsulate YLog operations on different plantform
    /// </summary>
    public interface IYLogAdapter
    {
        /// <summary>
        /// The Controller to control output log
        /// </summary>
        LoggerControllerBase logcatOutputController { get; set; }

        /// <summary>
        /// Get Priority threshold for logcat
        /// </summary>
        /// <returns> Priority threshold for logcat </returns>
        LogPriority GetLogcatPriorityTHold();

        /// <summary>
        /// Get Priority threshold for ramlog
        /// </summary>
        /// <returns> Priority threshold for ramlog </returns>
        LogPriority GetRamLogPriorityTHold();

        /// <summary>
        /// Set priority threshold for logcat
        /// </summary>
        //<param name="priority">Target priority threshold</param>
        void SetLogcatPriority(LogPriority priority);

        /// <summary>
        /// Set priority threshold for ramlog
        /// </summary>
        //<param name="priority">Target priority threshold</param>
        void SetRamLogPriority(LogPriority priority);

        /// <summary>
        /// Configure native YLog module
        /// </summary>
        /// <param name="tag"> Tag used by Ramlog </param>
        /// <param name="ramLogSize">The maximum memory size(in mb) used by ramlog </param>
        void ConfigureYLog(string tag, int ramLogSize = 5);

        /// <summary>
        /// Save the log in memory to local IO
        /// </summary>
        void SaveLog();

        /// <summary>
        /// Basically used for debugging native log
        /// </summary>
        /// <param name="viaUnity"> if true, native log will be output via Unity Debug, otherwise directly via android logcat </param>
        void SetLogcatOutputHandler(bool viaUnity);

        /// <summary>
        /// Handle to output log in Debug priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        void DebugHandle(in object context, in string message, in string prefix = null);

        /// <summary>
        /// Handle to output log in Info priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        void InfoHandle(in object context, in string message, in string prefix = null);

        /// <summary>
        /// Handle to output log in Warn priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        void WarnHandle(in object context, in string message, in string prefix = null);

        /// <summary>
        /// Handle to output log in Error priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        void ErrorHandle(in object context, in string message, in string prefix = null);
    }
}
