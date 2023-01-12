using UnityEngine;

namespace YVR.Utilities
{
    /// <summary>
    /// Base class for all logger controllers
    /// </summary>
    public abstract class LoggerControllerBase
    {
        private LoggerControllerBase wrappedLoggerController = null;

        /// <summary>
        /// Target logger which aimed to be controlled by this controller
        /// </summary>
        protected LoggerBase targetLogger = null;

        /// <summary>
        /// Constructor for LoggerController
        /// </summary>
        /// <param name="wrappedController"> Wrapped logger controller </param>
        public LoggerControllerBase(LoggerControllerBase wrappedController = null)
        {
            this.wrappedLoggerController = wrappedController;
        }

        /// <summary>
        /// Set target logger that to be controlled
        /// </summary>
        /// <param name="targetLogger">Target logger</param>
        public void SetTargetLogger(LoggerBase targetLogger)
        {
            this.targetLogger = targetLogger;
        }

        /// <summary>
        /// To determinate whether the log is valid to be output
        /// </summary>
        /// <param name="context">The context where output the log</param>
        /// <param name="log">The log message</param>
        /// <param name="priority">Log Priority</param>
        /// <returns>True if this log message should be output</returns>
        public virtual bool IsLogValid(in object context, in string log, in LogPriority priority, in string prefix = null)
        {
            return wrappedLoggerController == null || wrappedLoggerController.IsLogValid(context, log, priority, prefix);
        }
    }
}
