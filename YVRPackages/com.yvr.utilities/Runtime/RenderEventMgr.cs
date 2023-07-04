using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Utilities
{
    public class RenderEventMgr : Singleton<RenderEventMgr>
    {
        private SortedDictionary<int, List<Action>> m_OnBeforeRenderCallbacks
            = new SortedDictionary<int, List<Action>>();

        private SortedDictionary<int, List<Action>> m_ToRemoveCallbacks
            = new SortedDictionary<int, List<Action>>();

        protected override void OnInit()
        {
            base.OnInit();
            Application.onBeforeRender += OnBeforeRender;
        }

        private void OnBeforeRender()
        {
            foreach (KeyValuePair<int, List<Action>> pair in m_ToRemoveCallbacks)
            {
                if (!m_OnBeforeRenderCallbacks.ContainsKey(pair.Key)) continue;

                foreach (Action callback in pair.Value)
                    m_OnBeforeRenderCallbacks[pair.Key].Remove(callback);
            }

            m_ToRemoveCallbacks.Clear();

            foreach (Action call in m_OnBeforeRenderCallbacks.Values.SelectMany(callbacks => callbacks))
            {
                call?.Invoke();
            }
        }

        public void RegisterBeforeRenderCallback(Action callback, int priority = 0)
        {
            if (!m_OnBeforeRenderCallbacks.ContainsKey(priority))
                m_OnBeforeRenderCallbacks[priority] = new List<Action>();

            m_OnBeforeRenderCallbacks[priority].Add(callback);
        }

        public void UnregisterBeforeRenderCallback(Action callback, int priority = 0)
        {
            if (!m_ToRemoveCallbacks.ContainsKey(priority))
                m_ToRemoveCallbacks[priority] = new List<Action>();
            m_ToRemoveCallbacks[priority].Add(callback);
        }
    }
}