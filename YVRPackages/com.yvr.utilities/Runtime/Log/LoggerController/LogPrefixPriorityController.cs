using System.Collections.Generic;

namespace YVR.Utilities
{
    public class LogPrefixPriorityController : LoggerControllerBase
    {
        private Dictionary<string, LogPriority> m_PrefixPriorityDic = null;

        public LogPrefixPriorityController(LoggerControllerBase wrappedController = null) : base(wrappedController)
        {
            m_PrefixPriorityDic = new Dictionary<string, LogPriority>();
        }

        public override bool IsLogValid(object context, string log, LogPriority priority, string prefix = null)
        {
            if (TryGetPriority(m_PrefixPriorityDic, prefix, out LogPriority prefixPriority))
                return prefixPriority > priority;
            return false;
        }

        public void AddPrefixPriority(string prefix, LogPriority priority)
        {
            if (!m_PrefixPriorityDic.ContainsKey(prefix))
            {
                m_PrefixPriorityDic.Add(prefix, priority);
            }
            else
            {
                m_PrefixPriorityDic[prefix] = priority;
            }
        }

        public void RemovePrefixPriority(string prefix, LogPriority priority)
        {
            if (m_PrefixPriorityDic.ContainsKey(prefix))
            {
                m_PrefixPriorityDic.Remove(prefix);
            }
        }

        public void ClearPrefixPriority() { m_PrefixPriorityDic.Clear(); }

        private bool TryGetPriority(Dictionary<string, LogPriority> dic, string prefix, out LogPriority logPriority)
        {
            logPriority = LogPriority.Lowest;
            foreach (var key in dic.Keys)
            {
                if (prefix.Contains(key))
                {
                    logPriority = m_PrefixPriorityDic[key];
                    return true;
                }
            }

            return false;
        }
    }
}