using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace YVR.Utilities
{
    public partial class GfxHelperEditorHandler : IGfxHelperHandler
    {
        public void TriggerGfxThreadCallback(Action callback)
        {
            throw new NotImplementedException();
        }

        public void SetGfxThreadCallback()
        {
            throw new NotImplementedException();
        }

        public void CopyTexture(in int srcTextureId, in int dstTextureId)
        {
            throw new NotImplementedException();
        }

        public void CopyAndroidTexture(in int sourceTextureId, in int destTextureId, IntPtr androidMutex)
        {
            throw new NotImplementedException();
        }

        public string GetTextureWritingFolderPath()
        {
            throw new NotImplementedException();
        }

        public void SetTextureWritingFolderPath(string path)
        {
            throw new NotImplementedException();
        }

        public void WriteTexture(int targetTextureId, string targetPath)
        {
            throw new NotImplementedException();
        }

        public void WriteAndroidTexture(int targetTextureId, int width, int height, IntPtr androidMutex, string targetFilePath)
        {
            throw new NotImplementedException();
        }
    }
}
