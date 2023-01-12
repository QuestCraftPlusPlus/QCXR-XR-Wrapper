using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.IO;
using System.Linq;

namespace YVR.Utilities.Test
{
    [Category("Log")]
    [TestFixture]
    public class YLogLoggerRuntimeTests
    {
        private YLogLogger m_YLogLogger = new YLogLogger();

        [OneTimeSetUp]
        public void SetYLogLoggerPriorityHighest()
        {
            YVRLog.SetLogger(m_YLogLogger);
            YLogLogger.logcatPriorityTHold = LogPriority.LOWEST;
            YLogLogger.ramLogPriorityTHold = LogPriority.HIGHEST;

            YLogLogger.yLogAdapter.SetLogcatOutputHandler(true);
        }

        [Test, Order(1)]
        public void Debug_LogcatWithHandler_TriggerUnityLog()
        {
            this.Debug("Call Debug");
            LogAssert.Expect(LogType.Log, "Call Debug");
        }

        [Test, Order(2)]
        public void Info_LogcatWithHandler_TriggerUnityLog()
        {
            this.Info("Call Info");
            LogAssert.Expect(LogType.Log, "Call Info");
        }

        [Test, Order(3)]
        public void Warn_LogcatWithHandler_TriggerUnityWarn()
        {
            this.Warn("Call Warning");
            LogAssert.Expect(LogType.Warning, "Call Warning");
        }

        [Test, Order(4)]
        public void Error_LogcatWithHandler_TriggerUnityError()
        {
            this.Error("Call Error");
            LogAssert.Expect(LogType.Error, "Call Error");
        }

        [Test, Order(6)]
        public void SetLogcatPriority_Debug_AllOutput()
        {
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
            YLogLogger.logcatPriorityTHold = LogPriority.HIGHEST;

            this.Debug("Call Debug");
            this.Info("Call Info");
            this.Warn("Call Warning");
            this.Error("Call Error");

            LogAssert.NoUnexpectedReceived();
        }

        [Test, Order(11)]
        public void SaveLog_Default_LogFilesIncreasedByOne()
        {
            int originLogsCount = Application.isEditor ? 0 : Directory.GetFiles("/sdcard/misc/yvr/YUnity").Length;

            YLogLogger.SaveLog();
            int afterSaveLogsCount = Application.isEditor ? 1 : Directory.GetFiles("/sdcard/misc/yvr/YUnity").Length;

            Assert.That(afterSaveLogsCount - originLogsCount, Is.EqualTo(1));
        }

        [UnityTest, Order(12)]
        public IEnumerator Debug_RamLog_GetDebugFromLogFile()
        {
            YLogLogger.ramLogPriorityTHold = LogPriority.LOWEST;
            YLogLogger.logcatPriorityTHold = LogPriority.HIGHEST;

            this.Debug("Call Debug");

            yield return new WaitForSecondsRealtime(1.0f);
            YLogLogger.SaveLog();

            string latestFilePath = GetLatestLogFilePath();
            string lastLine = GetFileLastSeveralLines(latestFilePath, 1)[0];

            Assert.That(lastLine.Contains("Call Debug"));
            Assert.That(lastLine.Contains("[Debug]"));
        }

        [UnityTest, Order(13)]
        public IEnumerator Info_RamLog_GetInfoFromLogFile()
        {
            this.Info("Call Info");

            YLogLogger.SaveLog();
            yield return new WaitForSecondsRealtime(1.0f);

            string latestFilePath = GetLatestLogFilePath();
            string lastLine = GetFileLastSeveralLines(latestFilePath, 1)[0];
            Assert.That(lastLine.Contains("Call Info"));
            Assert.That(lastLine.Contains("[Info]"));
        }

        [UnityTest, Order(14)]
        public IEnumerator Warn_RamLog_GetWarnFromLogFile()
        {
            this.Warn("Call Warning");

            YLogLogger.SaveLog();
            yield return new WaitForSecondsRealtime(1.0f);

            string latestFilePath = GetLatestLogFilePath();
            string lastLine = GetFileLastSeveralLines(latestFilePath, 1)[0];
            Assert.That(lastLine.Contains("Call Warning"));
            Assert.That(lastLine.Contains("[Warn]"));
        }

        [UnityTest, Order(15)]
        public IEnumerator Error_RamLog_GetErrorFromLogFile()
        {
            this.Error("Call Error");

            YLogLogger.SaveLog();
            yield return new WaitForSecondsRealtime(1.0f);

            string latestFilePath = GetLatestLogFilePath();
            string lastLine = GetFileLastSeveralLines(latestFilePath, 1)[0];
            Assert.That(lastLine.Contains("Call Error"));
            Assert.That(lastLine.Contains("[Error]"));
        }

        [UnityTest, Order(16)]
        public IEnumerator SetRamLogPriority_Debug_AllOutput()
        {
            YLogLogger.ramLogPriorityTHold = LogPriority.DEBUG;

            this.Debug("Call Debug");
            this.Info("Call Info");
            this.Warn("Call Warning");
            this.Error("Call Error");

            string latestFilePath = GetLatestLogFilePath();
            int previousLength = GetFileLinesCount(latestFilePath);

            YLogLogger.SaveLog();
            yield return new WaitForSecondsRealtime(1.0f);

            latestFilePath = GetLatestLogFilePath();
            int currentLinesCount = GetFileLinesCount(latestFilePath);
            string[] lastFourLines = GetFileLastSeveralLines(latestFilePath, 4);

            Assert.That(currentLinesCount - previousLength, Is.EqualTo(4));
            Assert.That(lastFourLines[0].Contains("Call Debug"));
            Assert.That(lastFourLines[1].Contains("Call Info"));
            Assert.That(lastFourLines[2].Contains("Call Warning"));
            Assert.That(lastFourLines[3].Contains("Call Error"));
        }

        [UnityTest, Order(17)]
        public IEnumerator SetRamLogPriority_Info_InfoWarnErrorOutput()
        {
            YLogLogger.ramLogPriorityTHold = LogPriority.INFO;

            this.Debug("Call Debug");
            this.Info("Call Info");
            this.Warn("Call Warning");
            this.Error("Call Error");

            string latestFilePath = GetLatestLogFilePath();
            int previousLength = GetFileLinesCount(latestFilePath);

            YLogLogger.SaveLog();
            yield return new WaitForSecondsRealtime(1.0f);

            latestFilePath = GetLatestLogFilePath();
            int currentLinesCount = GetFileLinesCount(latestFilePath);
            string[] lastFourLines = GetFileLastSeveralLines(latestFilePath, 3);

            Assert.That(currentLinesCount - previousLength, Is.EqualTo(3));
            Assert.That(lastFourLines[0].Contains("Call Info"));
            Assert.That(lastFourLines[1].Contains("Call Warning"));
            Assert.That(lastFourLines[2].Contains("Call Error"));
        }

        [UnityTest, Order(18)]
        public IEnumerator SetRamLogPriority_Warn_WarnErrorOutput()
        {
            YLogLogger.ramLogPriorityTHold = LogPriority.WARN;

            this.Debug("Call Debug");
            this.Info("Call Info");
            this.Warn("Call Warning");
            this.Error("Call Error");

            string latestFilePath = GetLatestLogFilePath();
            int previousLength = GetFileLinesCount(latestFilePath);

            YLogLogger.SaveLog();
            yield return new WaitForSecondsRealtime(1.0f);

            latestFilePath = GetLatestLogFilePath();
            int currentLinesCount = GetFileLinesCount(latestFilePath);
            string[] lastFourLines = GetFileLastSeveralLines(latestFilePath, 2);

            Assert.That(currentLinesCount - previousLength, Is.EqualTo(2));
            Assert.That(lastFourLines[0].Contains("Call Warning"));
            Assert.That(lastFourLines[1].Contains("Call Error"));
        }

        [UnityTest, Order(19)]
        public IEnumerator SetRamLogPriority_Error_ErrorOutput()
        {
            YLogLogger.ramLogPriorityTHold = LogPriority.ERROR;

            this.Debug("Call Debug");
            this.Info("Call Info");
            this.Warn("Call Warning");
            this.Error("Call Error");

            string latestFilePath = GetLatestLogFilePath();
            int previousLength = GetFileLinesCount(latestFilePath);

            YLogLogger.SaveLog();
            yield return new WaitForSecondsRealtime(1.0f);

            latestFilePath = GetLatestLogFilePath();
            int currentLinesCount = GetFileLinesCount(latestFilePath);
            string[] lastFourLines = GetFileLastSeveralLines(latestFilePath, 1);

            Assert.That(currentLinesCount - previousLength, Is.EqualTo(1));
            Assert.That(lastFourLines[0].Contains("Call Error"));
        }

        [UnityTest, Order(20)]
        public IEnumerator SetRamLogPriority_HIGHEST_NoOutput()
        {
            YLogLogger.ramLogPriorityTHold = LogPriority.HIGHEST;

            this.Debug("Call Debug");
            this.Info("Call Info");
            this.Warn("Call Warning");
            this.Error("Call Error");

            string latestFilePath = GetLatestLogFilePath();
            int previousLength = GetFileLinesCount(latestFilePath);

            YLogLogger.SaveLog();
            yield return new WaitForSecondsRealtime(1.0f);

            latestFilePath = GetLatestLogFilePath();
            int currentLinesCount = GetFileLinesCount(latestFilePath);

            Assert.That(currentLinesCount, Is.EqualTo(previousLength));
        }

        private int GetFileLinesCount(string filePath)
        {
            return File.ReadLines(filePath).ToArray().Length;
        }

        private string GetLatestLogFilePath(string dir = "/sdcard/misc/yvr/YUnity")
        {
            List<string> orderedFiles = Directory.GetFiles(dir).OrderByDescending(filePath => new FileInfo(filePath).CreationTime).ToList();
            if (!orderedFiles.Any()) return "";

            string latestFilePath = orderedFiles[0];

            return latestFilePath;
        }

        private string[] GetFileLastSeveralLines(string filePath, int targetLines)
        {
            string[] allLines = File.ReadLines(filePath).ToArray();
            int totalLength = allLines.Length;
            if (totalLength == 0 || totalLength < targetLines) return null;

            string[] lastLines = new string[targetLines];
            for (int line = 0; line < targetLines; line++)
                lastLines[line] = allLines[totalLength - targetLines + line];

            return lastLines;
        }
    }
}