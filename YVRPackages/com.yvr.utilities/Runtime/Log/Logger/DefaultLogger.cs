namespace YVR.Utilities
{
    /// <summary>
    /// Default logger which encapsulate Unity debug function
    /// </summary>
    public class DefaultLogger : LoggerBase
    {
        /// <summary>
        /// Constructor for Default Logger
        /// </summary>
        /// <param name="wrappedLogger"> Wrapped other warpped logger </param>
        /// <param name="prefix">Wrapped log prefix </param>
        /// <returns> DefaultLogger Instance</returns>
        public DefaultLogger(LoggerBase wrappedLogger = null, LogPrefixBase prefix = null) :
            base(wrappedLogger, prefix) { }


        /// <summary>
        /// Handle for output log in Debug priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected override void DebugHandle(object context, string message, string prefix = null) =>
            UnityEngine.Debug.Log(string.IsNullOrEmpty(prefix) ? message : $"[{prefix}]  {message}");

        /// <summary>
        /// Handle for output log in Info priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected override void InfoHandle(object context, string message, string prefix = null) =>
            UnityEngine.Debug.Log(string.IsNullOrEmpty(prefix) ? message : $"[{prefix}]  {message}");

        /// <summary>
        /// Handle for output log in Warn priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected override void WarnHandle(object context, string message, string prefix = null) =>
            UnityEngine.Debug.LogWarning(string.IsNullOrEmpty(prefix) ? message : $"[{prefix}]  {message}");

        /// <summary>
        /// Handle for output log in Error priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected override void ErrorHandle(object context, string message, string prefix = null) =>
            UnityEngine.Debug.LogError(string.IsNullOrEmpty(prefix) ? message : $"[{prefix}]  {message}");
    }
}