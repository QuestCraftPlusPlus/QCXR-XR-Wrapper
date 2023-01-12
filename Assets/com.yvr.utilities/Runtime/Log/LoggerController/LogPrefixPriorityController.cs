using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Utilities
{
    public class LogPrefixPriorityController : LoggerControllerBase
    {

        private Dictionary<string, LogPriority> m_prefixPriorityDic = null;

        public LogPrefixPriorityController(LoggerControllerBase wrappedController = null) : base(wrappedController)
        {
            m_prefixPriorityDic = new Dictionary<string, LogPriority>();
        }

        public override bool IsLogValid(in object context, in string log, in LogPriority priority, in string prefix = null)
        {
            if (TryGetPriority(m_prefixPriorityDic, prefix, out LogPriority prefixPriority)) return prefixPriority > priority;
            return false;
        }

        public void AddPrefixPriority(string prefix, LogPriority priority)
        {
            if (!m_prefixPriorityDic.ContainsKey(prefix))
            {
                m_prefixPriorityDic.Add(prefix, priority);
            }
            else
            {
                m_prefixPriorityDic[prefix] = priority;
            }
        }

        public void RemovePrefixPriority(string prefix, LogPriority priority)
        {
            if (m_prefixPriorityDic.ContainsKey(prefix))
            {
                m_prefixPriorityDic.Remove(prefix);
            }
        }

        public void ClearPrefixPriority()
        {
            m_prefixPriorityDic.Clear();
        }

        private bool TryGetPriority(Dictionary<string, LogPriority> dic, string prefix, out LogPriority logPriority)
        {
            logPriority = LogPriority.LOWEST;
            foreach (var key in dic.Keys)
            {
                if (prefix.Contains(key))
                {
                    logPriority = m_prefixPriorityDic[key];
                    return true;
                }
            }
            return false;
        }

    }
}