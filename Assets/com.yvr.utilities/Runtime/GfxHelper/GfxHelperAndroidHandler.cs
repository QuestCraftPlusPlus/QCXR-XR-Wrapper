using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
using UnityEngine.Rendering;

namespace YVR.Utilities
{
    public partial class GfxHelperAndroidHandler : IGfxHelperHandler
    {
        [DllImport("yvrutilities")]
        private static extern void copyTexture(int sourceTextureId, int destTextureID);

        [DllImport("yvrutilities")]
        private static extern void copyAndroidTexture(int srcTextureId, int dstTextureId, IntPtr androidMutex);

        [DllImport("yvrutilities")]
        public static extern IntPtr getRenderEventAndDataFunc();

        [DllImport("yvrutilities")]
        public static extern IntPtr getRenderEventFunc();

        [DllImport("yvrutilities")]
        public static extern IntPtr getTextureWritingFolderPath();

        [DllImport("yvrutilities")]
        public static extern void setTextureWritingFolderPath(string path);

        [DllImport("yvrutilities")]
        public static extern void releaseArrayMemory(IntPtr addr);

        [DllImport("yvrutilities")]
        public static extern void writeTexture(int targetTextureId, string targetFilePath);

        [DllImport("yvrutilities")]
        public static extern void writeAndroidTexture(int targetTextureId, int width, int height, IntPtr androidMutex, string targetPath);

        [DllImport("yvrutilities")]
        public static extern void setRenderEventCallback(Action onRenderEventCallback);

        private static readonly object m_LockObject = new object();
        private static Queue<Action> m_GfxTheadCallbackQueue = new Queue<Action>();
        private Action onGfxCallback = null;

        public GfxHelperAndroidHandler()
        {
            onGfxCallback += OnGfxThread;
            SetGfxThreadCallback();
        }

        public void TriggerGfxThreadCallback(Action callback)
        {
            lock (m_LockObject)
            {
                m_GfxTheadCallbackQueue.Enqueue(callback);
                GL.IssuePluginEvent(getRenderEventFunc(), 0);
            }
        }

        public void SetGfxThreadCallback()
        {
            setRenderEventCallback(OnGfxThread);
        }

        private bool IsCopyTextureIdValid(in int srcTextureId, in int dstTextureId)
        {
            bool valid = srcTextureId > 0 && dstTextureId > 0;
            if (!valid) Debug.LogError($"Invalid value for CopyTexture: src -> {srcTextureId}, dst -> {dstTextureId}");
            return valid;
        }

        public void CopyTexture(in int srcTextureId, in int dstTextureId)
        {
            if (!IsCopyTextureIdValid(srcTextureId, dstTextureId)) return;
            copyTexture(srcTextureId, dstTextureId);
        }

        public void CopyAndroidTexture(in int srcTextureId, in int dstTextureId, IntPtr androidMutex)
        {
            if (!IsCopyTextureIdValid(srcTextureId, dstTextureId)) return;
            copyAndroidTexture(srcTextureId, dstTextureId, androidMutex);
        }

        public string GetTextureWritingFolderPath()
        {
            IntPtr folderPath = getTextureWritingFolderPath();
            string path = Marshal.PtrToStringAuto(folderPath);
            releaseArrayMemory(folderPath);
            return path;
        }

        public void SetTextureWritingFolderPath(string path)
        {
            setTextureWritingFolderPath(path);
        }

        public void WriteTexture(int targetTextureId, string targetPath)
        {
            writeTexture(targetTextureId, targetPath);
        }

        public void WriteAndroidTexture(int targetTextureId, int width, int height, IntPtr androidMutex, string targetFilePath)
        {
            writeAndroidTexture(targetTextureId, width, height, androidMutex, targetFilePath);
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnGfxThread()
        {
            lock (m_LockObject)
            {
                m_GfxTheadCallbackQueue.Dequeue()?.Invoke();
            }
        }
    }
}
