using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace YVR.Utilities
{
    /// <summary>
    /// Logger which encapsulate native YLog
    /// </summary>
    public class YLogLogger : LoggerBase
    {
        public static IYLogAdapter yLogAdapter { get; private set; }

        /// <summary>
        /// Priority threshold for logcat
        /// </summary>
        public static LogPriority logcatPriorityTHold
        {
            set => yLogAdapter.SetLogcatPriority(value);
            get => yLogAdapter.GetLogcatPriorityTHold();
        }

        /// <summary>
        /// Priority threshold for RamLog
        /// </summary>
        public static LogPriority ramLogPriorityTHold
        {
            set => yLogAdapter.SetRamLogPriority(value);
            get => yLogAdapter.GetRamLogPriorityTHold();
        }

        /// <summary>
        /// Constructor for YLogLogger
        /// </summary>
        /// <param name="wrappedLogger"> Wrapped other wrapped logger </param>
        /// <param name="prefix">Wrapped log prefix </param>
        /// <returns></returns>
        public YLogLogger(LoggerBase wrappedLogger = null, LogPrefixBase prefix = null) : base(wrappedLogger, prefix)
        {
#if UNITY_EDITOR
            yLogAdapter = new YLogLoggerEditor();
#else
            yLogAdapter = new YLogAndroidAdapter();
#endif
        }

        /// <summary>
        /// Configure YLog
        /// </summary>
        /// <param name="tag"> Tag used by Ramlog </param>
        /// <param name="ramLogSize">The maximum memory size(in mb) used by ramlog </param>
        public static void ConfigureYLog(string tag, int ramLogSize = 3)
        {
            yLogAdapter.ConfigureYLog(tag, ramLogSize);
        }

        /// <summary>
        /// Save the log in memory to local IO
        /// </summary>
        public static void SaveLog()
        {
            yLogAdapter.SaveLog();
        }

        /// <summary>
        /// Handle to output log in Debug priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected override void DebugHandle(in object context, in string message, in string prefix = null)
        {
            yLogAdapter.DebugHandle(in context, in message, prefix);
        }

        /// <summary>
        /// Handle to output log in Info priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected override void InfoHandle(in object context, in string message, in string prefix = null)
        {
            yLogAdapter.InfoHandle(in context, in message, prefix);
        }

        /// <summary>
        /// Handle to Output log in Warn priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected override void WarnHandle(in object context, in string message, in string prefix = null)
        {
            yLogAdapter.WarnHandle(in context, in message, prefix);
        }

        /// <summary>
        /// Output log in Error priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected override void ErrorHandle(in object context, in string message, in string prefix = null)
        {
            yLogAdapter.ErrorHandle(in context, in message, prefix);
        }
    }
}