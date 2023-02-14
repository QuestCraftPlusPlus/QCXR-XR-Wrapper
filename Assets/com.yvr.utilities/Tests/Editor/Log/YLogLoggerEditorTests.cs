using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace YVR.Utilities.Test
{
    [Category("Log")]
    [TestFixture]
    public class YLogLoggerEditorTests
    {
        private YLogLogger m_YLogLogger = new YLogLogger();

        [OneTimeSetUp]
        public void SetYLogLoggerPriorityHighest()
        {
            YLogLogger.logcatPriorityTHold = LogPriority.LOWEST;
        }

        [Test, Order(1)]
        public void Debug_YLogLogger_TriggerUnityLog()
        {
            YVRLog.SetLogger(m_YLogLogger);
            this.Debug("Call Debug");
            LogAssert.Expect(LogType.Log, "Call Debug");
        }

        [Test, Order(2)]
        public void Info_YLogLogger_TriggerUnityLog()
        {
            YVRLog.SetLogger(m_YLogLogger);
            this.Info("Call Info");
            LogAssert.Expect(LogType.Log, "Call Info");
        }

        [Test, Order(3)]
        public void Warn_YLogLogger_TriggerUnityWarn()
        {
            YVRLog.SetLogger(m_YLogLogger);
            this.Warn("Call Warning");
            LogAssert.Expect(LogType.Warning, "Call Warning");
        }

        [Test, Order(4)]
        public void Error_YLogLogger_TriggerUnityError()
        {
            YVRLog.SetLogger(m_YLogLogger);
            this.Error("Call Error");
            LogAssert.Expect(LogType.Error, "Call Error");
        }

        [Test, Order(5)]
        public void SetLogcatPriority_Lowest_AllOutput()
        {
            YVRLog.SetLogger(m_YLogLogger);
            YLogLogger.logcatPriorityTHold = LogPriority.LOWEST;
            
            this.Debug("Call Debug");
            this.Info("Call Info");
            this.Warn("Call Warning");
            this.Error("Call Error");

            LogAssert.Expect(LogType.Log, "Call Debug");
            LogAssert.Expect(LogType.Log, "Call Info");
            LogAssert.Expect(LogType.Warning, "Call Warning");
            LogAssert.Expect(LogType.Error, "Call Error");

            LogAssert.NoUnexpectedReceived();
        }

        [Test, Order(6)]
        public void SetLogcatPriority_Debug_AllOutput()
        {
            YVRLog.SetLogger(m_YLogLogger);
            YLogLogger.logcatPriorityTHold = LogPriority.DEBUG;

            this.Debug("Call Debug");
            this.Info("Call Info");
            this.Warn("Call Warning");
            this.Error("Call Error");

            LogAssert.Expect(LogType.Log, "Call Debug");
            LogAssert.Expect(LogType.Log, "Call Info");
            LogAssert.Expect(LogType.Warning, "Call Warning");
            LogAssert.Expect(LogType.Error, "Call Error");
            LogAssert.NoUnexpectedReceived();
        }

        [Test, Order(7)]
        public void SetLogcatPriority_Info_InfoWarnErrorOutput()
        {
            YVRLog.SetLogger(m_YLogLogger);
            YLogLogger.logcatPriorityTHold = LogPriority.INFO;

            this.Debug("Call Debug");
            this.Info("Call Info");
            this.Warn("Call Warning");
            this.Error("Call Error");

            LogAssert.Expect(LogType.Log, "Call Info");
            LogAssert.Expect(LogType.Warning, "Call Warning");
            LogAssert.Expect(LogType.Error, "Call Error");
            LogAssert.NoUnexpectedReceived();
        }

        [Test, Order(8)]
        public void SetLogcatPriority_Warn_WarnErrorOutput()
        {
            YVRLog.SetLogger(m_YLogLogger);
            YLogLogger.logcatPriorityTHold = LogPriority.WARN;

            this.Debug("Call Debug");
            this.Info("Call Info");
            this.Warn("Call Warning");
            this.Error("Call Error");

            LogAssert.Expect(LogType.Warning, "Call Warning");
            LogAssert.Expect(LogType.Error, "Call Error");
            LogAssert.NoUnexpectedReceived();
        }

        [Test, Order(9)]
        public void SetLogcatPriority_Error_OnlyError()
        {
            YVRLog.SetLogger(m_YLogLogger);
            YLogLogger.logcatPriorityTHold = LogPriority.ERROR;

            this.Debug("Call Debug");
            this.Info("Call Info");
            this.Warn("Call Warning");
            this.Error("Call Error");

            LogAssert.Expect(LogType.Error, "Call Error");
            LogAssert.NoUnexpectedReceived();
        }


        [Test, Order(10)]
        public void SetLogcatPriority_Highest_NoAnyOutput()
        {
            YVRLog.SetLogger(m_YLogLogger);
            YLogLogger.logcatPriorityTHold = LogPriority.HIGHEST;

            LogAssert.NoUnexpectedReceived();
        }
    }
}