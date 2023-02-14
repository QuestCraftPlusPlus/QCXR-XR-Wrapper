using System.Runtime.InteropServices;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using YVR.Utilities;

namespace YVR.Core.Test
{
    [TestFixture, Category("Log")]
    public class YVRNativeLog2UnityLogAdapterTests
    {
        [DllImport("yvrplugin")]
        private static extern void debugTest();

        [DllImport("yvrplugin")]
        private static extern void infoTest();

        [DllImport("yvrplugin")]
        private static extern void warnTest();

        [DllImport("yvrplugin")]
        private static extern void errorTest();

        private YLogLogger m_YLogLogger = new YLogLogger();

        [OneTimeSetUp]
        public void InstantiateYVRNativeLog2UnityLogAdapter()
        {
            YVRLog.SetLogger(m_YLogLogger);
            YVRNativeLog2YLogLoggerAdapter loggerAdapter = (new GameObject()).AddComponent<YVRNativeLog2YLogLoggerAdapter>();

#if UNITY_ANDROID && !UNITY_EDITOR
            YLogAndroidAdapter androidAdapter = (YLogAndroidAdapter) YLogLogger.yLogAdapter;
            androidAdapter.SetLogcatOutputHandler(true);
#endif
        }

        [Test]
        public void NativeDebug_TriggerUnityLog()
        {
            debugTest();
            LogAssert.Expect(LogType.Log, "Call Debug");
        }

        [Test]
        public void NativeInfo_TriggerUnityLog()
        {
            infoTest();
            LogAssert.Expect(LogType.Log, "Call Info");
        }

        [Test]
        public void NativeWarn_TriggerUnityWarn()
        {
            warnTest();
            LogAssert.Expect(LogType.Warning, "Call Warning");
        }

        [Test]
        public void NativeError_TriggerUnityError()
        {
            errorTest();
            LogAssert.Expect(LogType.Error, "Call Error");
        }
    }
}