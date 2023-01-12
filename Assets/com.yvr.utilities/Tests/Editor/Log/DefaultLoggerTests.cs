using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace YVR.Utilities.Test
{
    [Category("Log")]
    [TestFixture]
    public class DefaultLoggerTests
    {
        private DefaultLogger m_DefaultLogger = new DefaultLogger();

        [Test]
        public void Debug_DefaultLogger_TriggerUnityLog()
        {
            YVRLog.SetLogger(m_DefaultLogger);
            this.Debug("Call Debug");
            LogAssert.Expect(LogType.Log, "Call Debug");
        }

        [Test]
        public void Info_DefaultLogger_TriggerUnityLog()
        {
            YVRLog.SetLogger(m_DefaultLogger);
            this.Info("Call Info");
            LogAssert.Expect(LogType.Log, "Call Info");
        }

        [Test]
        public void Warn_DefaultLogger_TriggerUnityWarning()
        {
            YVRLog.SetLogger(m_DefaultLogger);
            this.Warn("Call Warning");
            LogAssert.Expect(LogType.Warning, "Call Warning");
        }

        [Test]
        public void Error_DefaultDebugger_TriggerUnityError()
        {
            YVRLog.SetLogger(m_DefaultLogger);
            this.Error("Call Error");
            LogAssert.Expect(LogType.Error, "Call Error");
        }
    }
}