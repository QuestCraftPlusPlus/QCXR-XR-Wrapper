using System;
using UnityEngine;

namespace YVR.Utilities
{
    /// <summary>
    /// Wrapper for RenderTexture that encapsulate operations while RenderTexture be recreated.
    /// </summary>
    public class RenderTextureWrapper
    {
        /// <summary>
        /// Wrapped RenderTexture
        /// </summary>
        public readonly RenderTexture renderTexture = null;

        private Action<RenderTexture> m_OnReCreated;

        /// <summary>
        /// Constructor for RenderTextureWrapper
        /// </summary>
        /// <param name="renderTexture"> RenderTexture to be wrapped </param>
        /// <param name="onReCreated"> Callback when target RenderTexture be re created. </param>
        public RenderTextureWrapper(RenderTexture renderTexture, Action<RenderTexture> onReCreated)
        {
            this.renderTexture = renderTexture;
            this.m_OnReCreated = onReCreated;
        }

        /// <summary>
        /// Release wrapped RenderTexture
        /// </summary>
        public void ReleaseRT() { renderTexture.Release(); }

        /// <summary>
        /// Recreate released wrapped RenderTexture
        /// </summary>
        public void ReCreateRT()
        {
            renderTexture.Create();
            Graphics.SetRenderTarget(renderTexture);
            GL.Clear(false, true, Color.clear);
            Graphics.SetRenderTarget(null);
            m_OnReCreated(renderTexture);
        }
    }
}