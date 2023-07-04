namespace YVR.Utilities
{
    /// <summary>
    /// Base class for all loggers
    /// </summary>
    public abstract class LoggerBase
    {
        private LoggerBase m_WrappedLogger = null;
        private LogPrefixBase m_LogPrefix = null;
        private LoggerControllerBase m_LoggerController = null;

        private static LoggerControllerBase s_DummyController = new LoggerDummyController();

        /// <summary>
        /// Constructor for YLogLogger
        /// </summary>
        /// <param name="wrappedLogger"> Wrapped other wrapped logger</param>
        /// <param name="logPrefix">Wrapped log prefix</param>
        /// <param name="loggerController">Controller for logger</param>
        protected LoggerBase(LoggerBase wrappedLogger = null, LogPrefixBase logPrefix = null,
                             LoggerControllerBase loggerController = null)
        {
            m_WrappedLogger = wrappedLogger;
            m_LogPrefix = logPrefix;
            SetLoggerController(loggerController);
        }

        /// <summary>
        /// Set logger controller
        /// </summary>
        /// <param name="loggerController">Logger controller</param>
        public void SetLoggerController(LoggerControllerBase loggerController)
        {
            m_LoggerController = loggerController ?? s_DummyController;
            m_LoggerController.SetTargetLogger(this);
        }

        /// <summary>
        /// Get log which contains target prefix
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="log"> The log message </param>
        /// <param name="priority"> Log Priority </param>
        /// <returns> Log contains target prefix </returns>
        protected string GetFormattedLog(object context, string log, LogPriority priority)
        {
            if (m_LogPrefix == null) return log;
            return $"[{m_LogPrefix.GetCombinedPrefix(context, log, priority)}]  {log}";
        }

        protected string GetCombinedPrefix(object context, string log, LogPriority priority)
        {
            if (m_LogPrefix == null) return string.Empty;
            return m_LogPrefix.GetCombinedPrefix(context, log, priority);
        }

        /// <summary>
        /// Handle for output log in Debug priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected abstract void DebugHandle(object context, string message, string prefix = null);

        /// <summary>
        /// Handle for output log in Info priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected abstract void InfoHandle(object context, string message, string prefix = null);

        /// <summary>
        /// Handle for output log in Warn priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected abstract void WarnHandle(object context, string message, string prefix = null);

        /// <summary>
        /// Handle for output log in Error priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected abstract void ErrorHandle(object context, string message, string prefix = null);

        /// <summary>
        /// Output log in Debug priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        public virtual void Debug(object context, string message)
        {
            if (!m_LoggerController.IsLogValid(context, message, LogPriority.Debug)) return;

            if (m_WrappedLogger != null) m_WrappedLogger.DebugHandle(context, message);

            DebugHandle(context, message, GetCombinedPrefix(context, message, LogPriority.Debug));
        }

        /// <summary>
        /// Output log in Info priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        public virtual void Info(object context, string message)
        {
            if (!m_LoggerController.IsLogValid(context, message, LogPriority.Info)) return;

            if (m_WrappedLogger != null) m_WrappedLogger.InfoHandle(context, message);

            InfoHandle(context, message, GetCombinedPrefix(context, message, LogPriority.Info));
        }

        /// <summary>
        /// Output log in Warn priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        public virtual void Warn(object context, string message)
        {
            if (!m_LoggerController.IsLogValid(context, message, LogPriority.Warn)) return;

            if (m_WrappedLogger != null) m_WrappedLogger.WarnHandle(context, message);
            WarnHandle(context, message, GetCombinedPrefix(context, message, LogPriority.Error));
        }

        /// <summary>
        /// Output log in Error priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        public virtual void Error(object context, string message)
        {
            if (!m_LoggerController.IsLogValid(context, message, LogPriority.Error)) return;

            if (m_WrappedLogger != null) m_WrappedLogger.ErrorHandle(context, message);
            ErrorHandle(context, message, GetCombinedPrefix(context, message, LogPriority.Error));
        }
    }
}