namespace YVR.Utilities
{
    /// <summary>
    /// Base class for all logger controllers
    /// </summary>
    public abstract class LoggerControllerBase
    {
        private LoggerControllerBase m_WrappedLoggerController = null;

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
            m_WrappedLoggerController = wrappedController;
        }

        /// <summary>
        /// Set target logger that to be controlled
        /// </summary>
        /// <param name="targetLogger">Target logger</param>
        public void SetTargetLogger(LoggerBase targetLogger) { this.targetLogger = targetLogger; }

        /// <summary>
        /// To determinate whether the log is valid to be output
        /// </summary>
        /// <param name="context">The context where output the log</param>
        /// <param name="log">The log message</param>
        /// <param name="priority">Log Priority</param>
        /// <param name="prefix">The Log prefix</param>
        /// <returns>True if this log message should be output</returns>
        public virtual bool IsLogValid(object context, string log, LogPriority priority, string prefix = null)
        {
            return m_WrappedLoggerController == null ||
                   m_WrappedLoggerController.IsLogValid(context, log, priority, prefix);
        }
    }
}