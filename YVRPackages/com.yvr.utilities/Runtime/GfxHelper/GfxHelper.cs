using System;
using UnityEngine;

namespace YVR.Utilities
{
    public partial class GfxHelper : Singleton<GfxHelper>
    {
        private IGfxHelperHandler m_GfxHelperHandler = null;

        public GfxHelper()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        m_GfxHelperHandler = new GfxHelperAndroidHandler();
#else
            m_GfxHelperHandler = new GfxHelperEditorHandler();
#endif
        }

        public void TriggerGfxThreadCallback(Action callbackAction)
        {
            m_GfxHelperHandler.TriggerGfxThreadCallback(callbackAction);
        }

        public void CopyTexture(int srcTextureId, int dstTextureId)
        {
            m_GfxHelperHandler.CopyTexture(srcTextureId, dstTextureId);
        }

        public void CopyTextureRect(int srcTextureId, int dstTextureId, bool onlyShowAlpha, Rect rect)
        {
            m_GfxHelperHandler.CopyTextureRect(srcTextureId, dstTextureId, onlyShowAlpha, rect);
        }

        public void CopyAndroidTexture(int sourceTextureId, int destTextureId, IntPtr androidMutex)
        {
            m_GfxHelperHandler.CopyAndroidTexture(sourceTextureId, destTextureId, androidMutex);
        }

        public string GetTextureWritingFolderPath()
        {
            return m_GfxHelperHandler.GetTextureWritingFolderPath();
        }

        public void SetTextureWringFolderPath(string path)
        {
            m_GfxHelperHandler.SetTextureWritingFolderPath(path);
        }

        public void WriteTexture(int targetTextureId, string targetPath = "")
        {
            m_GfxHelperHandler.WriteTexture(targetTextureId, targetPath);
        }

        public void WriteAndroidTexture(int targetTextureId, int width, int height, IntPtr androidMutex, string targetPath = "")
        {
            m_GfxHelperHandler.WriteAndroidTexture(targetTextureId, width, height, androidMutex, targetPath);
        }

    }
}
