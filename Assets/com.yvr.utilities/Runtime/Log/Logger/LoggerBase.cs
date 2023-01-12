namespace YVR.Utilities
{
    /// <summary>
    /// Base class for all loggers
    /// </summary>
    public abstract class LoggerBase
    {
        private LoggerBase wrappeedLogger = null;
        private LogPrefixBase logPrefix = null;
        private LoggerControllerBase loggerController = null;

        private static LoggerControllerBase dummyController = new LoggerDummyController();

        /// <summary>
        /// Constructor for YLogLogger
        /// </summary>
        /// <param name="wrappedLogger"> Wrapped other warpped logger</param>
        /// <param name="logPrefix">Wrapped log prefix</param>
        /// <param name="loggerController">Controller for logger</param>
        public LoggerBase(LoggerBase wrappedLogger = null, LogPrefixBase logPrefix = null, LoggerControllerBase loggerController = null)
        {
            this.wrappeedLogger = wrappedLogger;
            this.logPrefix = logPrefix;
            SetLoggerController(loggerController);
        }

        /// <summary>
        /// Set logger controller
        /// </summary>
        /// <param name="loggerController">Logger controller</param>
        public void SetLoggerController(LoggerControllerBase loggerController)
        {
            this.loggerController = loggerController ?? dummyController;
            this.loggerController.SetTargetLogger(this);
        }

        /// <summary>
        /// Get log which contains target prefix
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="log"> The log message </param>
        /// <param name="priority"> Log Priority </param>
        /// <returns> Log contains target prefix </returns>
        protected string GetFormattedLog(in object context, in string log, in LogPriority priority)
        {
            if (logPrefix == null) return log;
            return $"[{logPrefix.GetCombinedPrefix(in context, in log, in priority)}]  {log}";
        }

        protected string GetCombinedPrefix(in object context, in string log, in LogPriority priority)
        {
            if (logPrefix == null) return string.Empty;
            return logPrefix.GetCombinedPrefix(in context, in log, in priority);
        }

        /// <summary>
        /// Handle for output log in Debug priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected abstract void DebugHandle(in object context, in string message, in string prefix = null);

        /// <summary>
        /// Handle for output log in Info priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected abstract void InfoHandle(in object context, in string message, in string prefix = null);

        /// <summary>
        /// Handle for output log in Warn priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected abstract void WarnHandle(in object context, in string message, in string prefix = null);

        /// <summary>
        /// Handle for output log in Error priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected abstract void ErrorHandle(in object context, in string message, in string prefix = null);

        /// <summary>
        /// Output log in Debug priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        public virtual void Debug(in object context, in string message)
        {
            if (!loggerController.IsLogValid(in context, in message, LogPriority.DEBUG)) return;

            if (wrappeedLogger != null) wrappeedLogger.DebugHandle(in context, in message);

            DebugHandle(in context, in message, GetCombinedPrefix(in context, in message, LogPriority.DEBUG));
        }

        /// <summary>
        /// Output log in Info priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        public virtual void Info(in object context, in string message)
        {
            if (!loggerController.IsLogValid(in context, in message, LogPriority.INFO)) return;

            if (wrappeedLogger != null) wrappeedLogger.InfoHandle(in context, in message);

            InfoHandle(in context, in message, GetCombinedPrefix(in context, in message, LogPriority.INFO));
        }

        /// <summary>
        /// Output log in Warn priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        public virtual void Warn(in object context, in string message)
        {
            if (!loggerController.IsLogValid(in context, in message, LogPriority.WARN)) return;

            if (wrappeedLogger != null) wrappeedLogger.WarnHandle(in context, in message);
            WarnHandle(in context, in message, GetCombinedPrefix(in context, in message, LogPriority.ERROR));
        }

        /// <summary>
        /// Output log in Error priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        public virtual void Error(in object context, in string message)
        {
            if (!loggerController.IsLogValid(in context, in message, LogPriority.ERROR)) return;

            if (wrappeedLogger != null) wrappeedLogger.ErrorHandle(in context, in message);
            ErrorHandle(in context, in message, GetCombinedPrefix(in context, in message, LogPriority.ERROR));
        }
    }
}