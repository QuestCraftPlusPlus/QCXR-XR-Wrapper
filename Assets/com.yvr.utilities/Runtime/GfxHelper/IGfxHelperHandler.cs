using System;

namespace YVR.Utilities
{
    public partial interface IGfxHelperHandler
    {
        void TriggerGfxThreadCallback(Action callback);
        void SetGfxThreadCallback();
        void CopyTexture(in int srcTextureId, in int dstTextureId);
        void CopyAndroidTexture(in int sourceTextureId, in int destTextureId, IntPtr androidMutex);

        string GetTextureWritingFolderPath();
        void SetTextureWritingFolderPath(string path);

        void WriteTexture(int targetTextureId, string targetPath);
        void WriteAndroidTexture(int targetTextureId, int width, int height, IntPtr androidMutex, string targetFilePath);
    }
}