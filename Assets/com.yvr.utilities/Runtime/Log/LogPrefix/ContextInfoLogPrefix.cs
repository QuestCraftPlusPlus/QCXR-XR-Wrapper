using UnityEngine;
namespace YVR.Utilities
{
    /// <summary>
    /// Used for adding context information as log prefix
    /// </summary>
    public class ContextInfoLogPrefix : LogPrefixBase
    {
        /// <summary>
        /// Constructor for ContextInfoLogPrefix
        /// </summary>
        /// <param name="prefix"> wrapped log prefix </param>
        /// <returns></returns>
        public ContextInfoLogPrefix(LogPrefixBase prefix = null) : base(prefix) { }

        /// <summary>
        /// Get context info prefix for log
        /// </summary>
        /// <param name="context"> The context where output the log </param>
        /// <param name="log"> The log message </param>
        /// <param name="priority"> Log Priority </param>
        /// <returns> The context info prefix in format 'Context: {contextInfo}' </returns>
        public override string GetPrefix(in object context, in string log, in LogPriority priority)
        {
            if (context == null) return log;

            string contextInfo;

            if (context is MonoBehaviour mono)
                contextInfo = $"{mono.gameObject.name}-{mono.GetType().Name}";
            else if (context is GameObject go)
                contextInfo = $"{go.name}";
            else if (context is string)
                contextInfo = "String";
            else
                contextInfo = context.GetType().Name;

            return contextInfo;
        }
    }
}