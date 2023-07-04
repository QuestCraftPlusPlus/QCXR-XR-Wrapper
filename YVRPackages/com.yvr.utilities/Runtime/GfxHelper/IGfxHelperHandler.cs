using System;
using UnityEngine;

namespace YVR.Utilities
{
    public partial interface IGfxHelperHandler
    {
        void TriggerGfxThreadCallback(Action callback);
        void SetGfxThreadCallback();
        void CopyTexture(int srcTextureId, int dstTextureId);
        void CopyTextureRect(int srcTextureId, int dstTextureId, bool onlyShowAlpha, Rect viewportRect);
        void CopyAndroidTexture(int sourceTextureId, int destTextureId, IntPtr androidMutex);

        string GetTextureWritingFolderPath();
        void SetTextureWritingFolderPath(string path);

        void WriteTexture(int targetTextureId, string targetPath);
        void WriteAndroidTexture(int targetTextureId, int width, int height, IntPtr androidMutex, string targetFilePath);
    }
}