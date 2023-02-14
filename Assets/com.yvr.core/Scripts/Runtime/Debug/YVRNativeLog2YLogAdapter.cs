using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
using YVR.Utilities;

namespace YVR.Core
{
    /// <summary>
    /// Make native log output by YLog rather than android logcat
    /// </summary>
    public class YVRNativeLog2YLogLoggerAdapter : MonoBehaviour
    {
        [DllImport("yvrplugin")]
        private static extern void setDebugOutputHandler(Action<string> debugHandler);

        [DllImport("yvrplugin")]
        private static extern void setInfoOutputHandler(Action<string> infoHandler);

        [DllImport("yvrplugin")]
        private static extern void setWarnOutputHandler(Action<string> warnHandler);

        [DllImport("yvrplugin")]
        private static extern void setErrorOutputHandler(Action<string> errorHandler);

        private static YVRNativeLog2YLogLoggerAdapter s_LoggerAdapterInstance = null;

        private void Awake()
        {
            s_LoggerAdapterInstance = this;
            HandleNativeLogViaYLogLogger();
        }

        private void HandleNativeLogViaYLogLogger()
        {
            setDebugOutputHandler(DebugViaYLogger);
            setInfoOutputHandler(InfoViaYLogger);
            setWarnOutputHandler(WarnViaYLogger);
            setErrorOutputHandler(ErrorViaYLogger);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void DebugViaYLogger(string msg)
        {
            s_LoggerAdapterInstance.Debug(msg);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void InfoViaYLogger(string msg)
        {
            s_LoggerAdapterInstance.Info(msg);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void WarnViaYLogger(string msg)
        {
            s_LoggerAdapterInstance.Warn(msg);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void ErrorViaYLogger(string msg)
        {
            s_LoggerAdapterInstance.Error(msg);
        }
    }
}