using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AOT;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace YVR.Utilities.Test
{
    public class GfxHelperWriteTextureTests
    {
        [Test, Order(1)]
        public void GetTextureWritingFolder_InitValue_IsSdcard()
        {
            string path = GfxHelper.instance.GetTextureWritingFolderPath();
            Assert.That(path, Is.Not.Null);
            Assert.That(path, Is.EqualTo("/sdcard/"));
        }

        private static object[] s_FolderPathTestCases =
        {
            new string[]{"/sdcard/","/sdcard/"},
            new string[]{"/sdcard","/sdcard/"},
            new string[]{"",""},
            new string[]{"\\sdcard/","/sdcard/"},
            new string[]{"\\sdcard\\","/sdcard/"},
        };

        [Test, Order(2)]
        [TestCaseSource(nameof(s_FolderPathTestCases))]
        public void SetTextureWritingFolder_EndTrailingSplash_AllForwardSlash(string setPath, string expectPath)
        {
            GfxHelper.instance.SetTextureWringFolderPath(setPath);

            string resultPath = GfxHelper.instance.GetTextureWritingFolderPath();
            Assert.That(resultPath, Is.EqualTo(expectPath));
        }

        private static string[] s_TestingTextureNames = new string[] { "red", "cube_texture" };
        private static int s_TargetTextureId = -1;

        [UnityTest, Order(3)]
        public IEnumerator WriteTexture_ResourceTexture_CanWrite([ValueSource(nameof(s_TestingTextureNames))] string targetTextureName)
        {
            // Can get local resource texture
            Texture2D targetTexture = Resources.Load<Texture2D>(targetTextureName);
            Assert.That(targetTexture, Is.Not.Null);
            s_TargetTextureId = (int)targetTexture.GetNativeTexturePtr();
            Assert.That(s_TargetTextureId, Is.Not.EqualTo(-1));

            // File is not existed before writing
            string targetFilePath = GfxHelper.instance.GetTextureWritingFolderPath() + s_TargetTextureId.ToString() + ".jpg";
            if (File.Exists(targetFilePath))
                File.Delete(targetFilePath);
            Assert.That(!File.Exists(targetFilePath));

            GfxHelper.instance.TriggerGfxThreadCallback(() => GfxHelper.instance.WriteTexture(s_TargetTextureId));
            yield return null;
            yield return null;

            Assert.That(File.Exists(targetFilePath));
        }
    }
}
