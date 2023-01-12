using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace YVR.Utilities.Test
{
    [Category("Log")]
    [TestFixture]
    public class YLogLoggerWithPrefixRuntimeTests
    {
        private ContextInfoLogPrefix m_contextInfoLogPrefix = new ContextInfoLogPrefix();
        private YLogLogger m_YLogLogger = new YLogLogger(null, new ContextInfoLogPrefix());

        private TestPrefix m_testPrefix = new TestPrefix();
        private class TestPrefix { }

        [OneTimeSetUp]
        public void SetYLogLoggerPriority()
        {
            YVRLog.SetLogger(m_YLogLogger);
            YLogLogger.logcatPriorityTHold = LogPriority.LOWEST;
            YLogLogger.ramLogPriorityTHold = LogPriority.HIGHEST;
            YLogLogger.yLogAdapter.SetLogcatOutputHandler(true);
        }

        [Test, Order(1)]
        public void PrefixDebug_LogcatWithHandler_TriggerUnityLog()
        {
            m_testPrefix.Debug("Call Debug");
            LogAssert.Expect(LogType.Log, $"[{m_contextInfoLogPrefix.GetCombinedPrefix(m_testPrefix, "Call Debug", LogPriority.DEBUG)}]  Call Debug");
        }

        [Test, Order(2)]
        public void PrefixInfo_LogcatWithHandler_TriggerUnityLog()
        {
            m_testPrefix.Debug("Call Info");
            LogAssert.Expect(LogType.Log, $"[{m_contextInfoLogPrefix.GetCombinedPrefix(m_testPrefix, "Call Info", LogPriority.INFO)}]  Call Info");
        }

        [Test, Order(3)]
        public void PrefixWarn_LogcatWithHandler_TriggerUnityLog()
        {
            m_testPrefix.Warn("Call Warning");
            LogAssert.Expect(LogType.Warning, $"[{m_contextInfoLogPrefix.GetCombinedPrefix(m_testPrefix, "Call Warning", LogPriority.WARN)}]  Call Warning");
        }

        [Test, Order(4)]
        public void PrefixError_LogcatWithHandler_TriggerUnityLog()
        {
            m_testPrefix.Error("Call Error");
            LogAssert.Expect(LogType.Error, $"[{m_contextInfoLogPrefix.GetCombinedPrefix(m_testPrefix, "Call Error", LogPriority.ERROR)}]  Call Error");
        }

        [Test, Order(5)]
        public void SetPrefixPriorityInfo_InfoLogcatPriority_PrefixDebugOutPut()
        {
            YLogLogger.logcatPriorityTHold = LogPriority.INFO;
            LogPrefixPriorityController logPrefixPriorityController = new LogPrefixPriorityController();
            YLogLogger.yLogAdapter.logcatOutputController = logPrefixPriorityController;

            this.Debug("Call Debug");
            m_testPrefix.Debug("Call Debug");

            this.Info("Call Info");
            m_testPrefix.Info("Call Info");

            logPrefixPriorityController.AddPrefixPriority(m_testPrefix.GetType().Name, LogPriority.INFO);

            this.Debug("Call Debug");
            m_testPrefix.Debug("Call Debug");

            this.Info("Call Info");
            m_testPrefix.Info("Call Info");


            LogAssert.Expect(LogType.Log, $"[{m_contextInfoLogPrefix.GetCombinedPrefix(this, "Call Info", LogPriority.INFO)}]  Call Info");
            LogAssert.Expect(LogType.Log, $"[{m_contextInfoLogPrefix.GetCombinedPrefix(m_testPrefix, "Call Info", LogPriority.INFO)}]  Call Info");

            LogAssert.Expect(LogType.Log, $"[{m_contextInfoLogPrefix.GetCombinedPrefix(m_testPrefix, "Call Debug", LogPriority.DEBUG)}]  Call Debug");

            LogAssert.Expect(LogType.Log, $"[{m_contextInfoLogPrefix.GetCombinedPrefix(this, "Call Info", LogPriority.INFO)}]  Call Info");
            LogAssert.Expect(LogType.Log, $"[{m_contextInfoLogPrefix.GetCombinedPrefix(m_testPrefix, "Call Info", LogPriority.INFO)}]  Call Info");

            LogAssert.NoUnexpectedReceived();
        }


    }


}

