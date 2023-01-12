using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;

namespace YVR.Utilities
{
    /// <summary>
    /// Extension class for all object to provider 
    /// </summary>
    public static class YVRLog
    {
        public static List<LoggerBase> loggersList { get; } = new List<LoggerBase>();

        /// <summary>
        /// Output log within all registered logger in Debug priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        public static void Debug(this object context, string message) => loggersList?.ForEach(logger => logger.Debug(in context, in message));

        /// <summary>
        /// Output log within all registered logger in info priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        public static void Info(this object context, string message) => loggersList?.ForEach(logger => logger.Info(in context, in message));

        /// <summary>
        /// Output log within all registered logger in warn priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        public static void Warn(this object context, string message) => loggersList?.ForEach(logger => logger.Warn(in context, in message));

        /// <summary>
        /// Output log within all registered logger in error priority
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="message"> The log message </param>
        public static void Error(this object context, string message) => loggersList?.ForEach(logger => logger.Error(in context, in message));

        /// <summary>
        /// Constructor for YLog
        /// </summary>
        static YVRLog()
        {
            RegisterLogger(new DefaultLogger());
        }

        /// <summary>
        /// Set target logger as the only used logger
        /// </summary>
        /// <param name="targetLogger">Target logger</param>
        public static void SetLogger(LoggerBase targetLogger)
        {
            ClearLoggers();
            RegisterLogger(targetLogger);
        }

        /// <summary>
        /// Clear all registered loggers
        /// </summary>
        public static void ClearLoggers()
        {
            loggersList.Clear();
        }

        /// <summary>
        /// Register target logger
        /// </summary>
        /// <param name="targetLogger">Target logger</param>
        public static void RegisterLogger(LoggerBase targetLogger)
        {
            loggersList.Add(targetLogger);
        }

        /// <summary>
        /// Register target logger
        /// </summary>
        /// <param name="targetLogger">Target logger</param>
        public static void UnregisterLogger(LoggerBase targetLogger)
        {
            loggersList.Remove(targetLogger);
        }
    }
}