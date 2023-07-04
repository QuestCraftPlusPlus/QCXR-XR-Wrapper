using UnityEngine;
using UnityEngine.Internal;

namespace YVR.Utilities
{
    [ExcludeFromDocs]
    // YLogLoggerEditor will not handle RamLog related operations
    public class YLogLoggerEditor : IYLogAdapter
    {
        private LogPriority m_LogcatPriority = LogPriority.Highest;
        private LoggerControllerBase m_LogcatOutputController = LogPrefixDummyController.defaultController;

        public LoggerControllerBase logcatOutputController
        {
            get => m_LogcatOutputController;
            set => m_LogcatOutputController = value ?? LogPrefixDummyController.defaultController;
        }

        public void DebugHandle(in object context, in string message, in string prefix = null)
        {
            if (m_LogcatPriority > LogPriority.Debug &&
                !m_LogcatOutputController.IsLogValid(context, message, LogPriority.Debug, prefix)) return;
            Debug.Log(string.IsNullOrEmpty(prefix) ? message : $"[{prefix}]  {message}");
        }

        public void InfoHandle(in object context, in string message, in string prefix = null)
        {
            if (m_LogcatPriority > LogPriority.Info &&
                !m_LogcatOutputController.IsLogValid(context, message, LogPriority.Info, prefix)) return;
            Debug.Log(string.IsNullOrEmpty(prefix) ? message : $"[{prefix}]  {message}");
        }

        public void ErrorHandle(in object context, in string message, in string prefix = null)
        {
            if (m_LogcatPriority > LogPriority.Error &&
                !m_LogcatOutputController.IsLogValid(context, message, LogPriority.Error, prefix)) return;
            Debug.LogError(string.IsNullOrEmpty(prefix) ? message : $"[{prefix}]  {message}");
        }

        public void WarnHandle(in object context, in string message, in string prefix = null)
        {
            if (m_LogcatPriority > LogPriority.Warn &&
                !m_LogcatOutputController.IsLogValid(context, message, LogPriority.Warn, prefix)) return;
            Debug.LogWarning(string.IsNullOrEmpty(prefix) ? message : $"[{prefix}]  {message}");
        }

        public LogPriority GetLogcatPriorityTHold() { return m_LogcatPriority; }

        public LogPriority GetRamLogPriorityTHold() { return LogPriority.Highest; }

        public void SetLogcatPriority(LogPriority priority) { m_LogcatPriority = priority; }

        public void SetRamLogPriority(LogPriority priority) { }

        public void ConfigureYLog(string tag, int ramLogSize = 5) { }
        public void SaveLog() { }

        public void SetLogcatOutputHandler(bool viaUnity) { }
    }
}