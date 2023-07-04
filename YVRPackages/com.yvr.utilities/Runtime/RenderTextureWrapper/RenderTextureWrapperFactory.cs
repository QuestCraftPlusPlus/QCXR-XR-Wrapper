using System;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Utilities
{
    /// <summary>
    /// Factory to allocate and destroy RenderTextureWrapper
    /// </summary>
    public class RenderTextureWrapperFactory : MonoBehaviorSingleton<RenderTextureWrapperFactory>
    {
        private List<RenderTextureWrapper> m_ReleasedOnPauseWrapperList = new List<RenderTextureWrapper>();

        /// <summary>
        /// Allocate a RenderTextureWrapper
        /// </summary>
        /// <param name="width"> Width for target RenderTexture</param>
        /// <param name="height"> Height for target RenderTexture</param>
        /// <param name="depthBits">  Depth bits for target RenderTexture </param>
        /// <param name="onReCreated"> Callback when target RenderTexture be re created. </param>
        /// <param name="releaseOnPause"> Should target RenderTexture be released automatically when application paused </param>
        /// <returns></returns>
        /// <remarks> <param name="onReCreated"></param> won't be triggered when RenderTexture first created. </remarks>
        public RenderTextureWrapper AllocRTWrapper(int width, int height, int depthBits,
            Action<RenderTexture> onReCreated, bool releaseOnPause = true)
        {
            var rt = new RenderTexture(width, height, depthBits);
            var wrapper = new RenderTextureWrapper(rt, onReCreated);

            if (releaseOnPause) m_ReleasedOnPauseWrapperList.Add(wrapper);
            return wrapper;
        }

        public RenderTextureWrapper AllocRTWrapper(RenderTexture rt, Action<RenderTexture> onReCreated,
            bool releaseOnPause = true)
        {
            var wrapper = new RenderTextureWrapper(rt, onReCreated);

            if (releaseOnPause) m_ReleasedOnPauseWrapperList.Add(wrapper);
            return wrapper;
        }

        /// <summary>
        /// Destroy target RenderTextureWrapper
        /// </summary>
        /// <param name="rtWrapper"> RenderTextureWrapper to be destroyed. </param>
        public void DestroyRTWrapper(RenderTextureWrapper rtWrapper)
        {
            if (rtWrapper == null) return;

            rtWrapper.ReleaseRT();
            m_ReleasedOnPauseWrapperList.Remove(rtWrapper);
            Destroy(rtWrapper.renderTexture);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            m_ReleasedOnPauseWrapperList.ForEach(wrapper =>
            {
                if (pauseStatus) wrapper.ReleaseRT();
                else wrapper.ReCreateRT();
            });
        }
    }
}