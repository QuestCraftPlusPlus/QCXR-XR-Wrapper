using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using YVR.Utilities;

namespace YVR.Utilities.Test
{
    [Category("Log")]
    public class YVRLogTests
    {
        DefaultLogger m_DefaultLogger = new DefaultLogger();

        [Test]
        public void ClearLoggers_DefaultDebug_NoUnityDebug()
        {
            YVRLog.ClearLoggers();
            this.Debug("Call Debug");
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void RegisterLogger_TwoDefaultLogger_TriggerTwice()
        {
            YVRLog.ClearLoggers();
            YVRLog.RegisterLogger(m_DefaultLogger);
            YVRLog.RegisterLogger(m_DefaultLogger);

            this.Debug("Call Debug");

            LogAssert.Expect(LogType.Log, "Call Debug");
            LogAssert.Expect(LogType.Log, "Call Debug");
        }

        [Test]
        public void UnRegisterLogger_UnRegisterOneFromTwoDefaultLogger_TriggerOnce()
        {
            // Firstly register two default loggers
            YVRLog.ClearLoggers();
            YVRLog.RegisterLogger(m_DefaultLogger);
            YVRLog.RegisterLogger(m_DefaultLogger);

            // Unregister one default logger
            YVRLog.UnregisterLogger(m_DefaultLogger);

            this.Debug("Call Debug");

            LogAssert.Expect(LogType.Log, "Call Debug");
        }

        [Test]
        public void SetRegister_DummyRegister_NoUnityDebug()
        {
            YVRLog.ClearLoggers();
            // Firstly register two default loggers
            YVRLog.ClearLoggers();
            YVRLog.RegisterLogger(m_DefaultLogger);
            YVRLog.RegisterLogger(m_DefaultLogger);

            DummyLogger dummyLogger = new DummyLogger();
            YVRLog.SetLogger(dummyLogger);

            this.Debug("Call Debug");

            LogAssert.NoUnexpectedReceived();
        }
    }
}