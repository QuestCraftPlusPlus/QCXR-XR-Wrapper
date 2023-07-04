using System;
using UnityEngine;

namespace YVR.Utilities
{
    public partial class GfxHelperEditorHandler : IGfxHelperHandler
    {
        public void TriggerGfxThreadCallback(Action callback) { }
        public void SetGfxThreadCallback() { }
        public void CopyTexture(int srcTextureId, int dstTextureId) { }
        public void CopyAndroidTexture(int sourceTextureId, int destTextureId, IntPtr androidMutex) { }
        public string GetTextureWritingFolderPath() { return ""; }
        public void SetTextureWritingFolderPath(string path) { }

        public void WriteTexture(int targetTextureId, string targetPath) { }

        public void WriteAndroidTexture(int targetTextureId, int width, int height, IntPtr androidMutex,
                                        string targetFilePath) { }

        public void CopyTextureRect(int srcTextureId, int dstTextureId, bool onlyShowAlpha, Rect viewportRect) { }
    }
}