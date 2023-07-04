namespace YVR.Utilities
{
    /// <summary>
    /// Dummy logger which will not output anything
    /// </summary>
    public class DummyLogger : LoggerBase
    {
        /// <summary>
        /// Handle for output log in Debug priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected override void DebugHandle(object context, string message, string prefix = null) { }

        /// <summary>
        /// Handle for output log in Info priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected override void InfoHandle(object context, string message, string prefix = null) { }

        /// <summary>
        /// Handle for output log in Warn priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected override void WarnHandle(object context, string message, string prefix = null) { }

        /// <summary>
        /// Handle for output log in Error priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        /// <param name="prefix"> The log prefix </param>
        protected override void ErrorHandle(object context, string message, string prefix = null) { }
    }
}