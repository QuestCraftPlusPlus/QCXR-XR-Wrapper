using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;

namespace YVR.Utilities
{
    [ExcludeFromDocs]
    // YLogLoggerEditor will not handle RamLog related operations
    public class YLogLoggerEditor : IYLogAdapter
    {
        private LogPriority m_LogcatPriority = LogPriority.HIGHEST;
        private LoggerControllerBase m_logcatOutputController = LogPrefixDummyController.defaultController;

        public LoggerControllerBase logcatOutputController { get => m_logcatOutputController; set => m_logcatOutputController = value ?? LogPrefixDummyController.defaultController; }

        public void DebugHandle(in object context, in string message, in string prefix = null)
        {
            if (m_LogcatPriority > LogPriority.DEBUG && !m_logcatOutputController.IsLogValid(context, message, LogPriority.DEBUG, prefix)) return;
            Debug.Log(string.IsNullOrEmpty(prefix) ? message : $"[{prefix}]  {message}");
        }

        public void InfoHandle(in object context, in string message, in string prefix = null)
        {
            if (m_LogcatPriority > LogPriority.INFO && !m_logcatOutputController.IsLogValid(context, message, LogPriority.INFO, prefix)) return;
            Debug.Log(string.IsNullOrEmpty(prefix) ? message : $"[{prefix}]  {message}");
        }

        public void ErrorHandle(in object context, in string message, in string prefix = null)
        {
            if (m_LogcatPriority > LogPriority.ERROR && !m_logcatOutputController.IsLogValid(context, message, LogPriority.ERROR, prefix)) return;
            Debug.LogError(string.IsNullOrEmpty(prefix) ? message : $"[{prefix}]  {message}");
        }

        public void WarnHandle(in object context, in string message, in string prefix = null)
        {
            if (m_LogcatPriority > LogPriority.WARN && !m_logcatOutputController.IsLogValid(context, message, LogPriority.WARN, prefix)) return;
            Debug.LogWarning(string.IsNullOrEmpty(prefix) ? message : $"[{prefix}]  {message}");
        }

        public LogPriority GetLogcatPriorityTHold()
        {
            return m_LogcatPriority;
        }

        public LogPriority GetRamLogPriorityTHold()
        {
            return LogPriority.HIGHEST;
        }

        public void SetLogcatPriority(LogPriority priority)
        {
            m_LogcatPriority = priority;
        }

        public void SetRamLogPriority(LogPriority priority) { }

        public void ConfigureYLog(string tag, int ramLogSize = 5) { }
        public void SaveLog() { }

        public void SetLogcatOutputHandler(bool viaUnity) { }
    }
}