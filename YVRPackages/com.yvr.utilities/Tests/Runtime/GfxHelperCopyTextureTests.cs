using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using AOT;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace YVR.Utilities.Test
{
    [TestFixture]
    public class GfxHelperCopyTextureTests
    {
        private static string[] s_SrcTestingTextureNames = new string[] { "red", "cube_texture", "Star" };

        [Test]
        public void CopyTexture_InvalidTextureId_ErrorLog()
        {
            GfxHelper.instance.CopyTexture(-1, 0);
            LogAssert.Expect(LogType.Error, "Invalid value for CopyTexture: src -> -1, dst -> 0");
        }

        [UnityTest]
        public IEnumerator CopyTexture_SamePixels([ValueSource(nameof(s_SrcTestingTextureNames))] string srcTextureName)
        {
            Texture2D srcTexture = Resources.Load<Texture2D>(srcTextureName);
            Assert.That(srcTexture, Is.Not.Null);

            RenderTexture dstTexture = new RenderTexture(srcTexture.width, srcTexture.height, 0);
            dstTexture.Create();

            int srcTextureId = (int)srcTexture.GetNativeTexturePtr();
            int dstTextureId = (int)dstTexture.GetNativeTexturePtr();

            Assert.That(srcTextureId, Is.Not.EqualTo(-1));
            Assert.That(dstTextureId, Is.Not.EqualTo(-1));

            GfxHelper.instance.TriggerGfxThreadCallback(() => GfxHelper.instance.CopyTexture(srcTextureId, dstTextureId));
            yield return null;
            yield return null;

            Assert.That(srcTexture.GetPixels().IsEqualTo(dstTexture.GetPixels()));

            dstTexture.Release();
        }

        [UnityTest]
        public IEnumerator CopyTexture_SemiTransparentTextureInARow_SamePixels()
        {
            Texture2D srcTexture = new Texture2D(100, 100);
            Color32[] semiRedPixels = Enumerable.Repeat(1, srcTexture.width * srcTexture.height).Select(i => (Color32)new Color(0.5f, 0, 0, 0.5f)).ToArray();
            srcTexture.SetPixels32(semiRedPixels);
            srcTexture.Apply();

            RenderTexture dstTexture = new RenderTexture(srcTexture.width, srcTexture.height, 0);
            dstTexture.Create();

            int srcTextureId = (int)srcTexture.GetNativeTexturePtr();
            int dstTextureId = (int)dstTexture.GetNativeTexturePtr();

            GfxHelper.instance.TriggerGfxThreadCallback(() => GfxHelper.instance.CopyTexture(srcTextureId, dstTextureId));
            yield return null;
            yield return null;

            Assert.That(semiRedPixels.IsEqualTo(dstTexture.GetPixels32()));

            GfxHelper.instance.TriggerGfxThreadCallback(() => GfxHelper.instance.CopyTexture(srcTextureId, dstTextureId));
            yield return null;
            yield return null;

            Assert.That(semiRedPixels.IsEqualTo(dstTexture.GetPixels32()));

            dstTexture.Release();
        }

        [UnityTest]
        public IEnumerator CopyTexture_SemiTransparentDifferentSizeInARow_ResultsNotBlend()
        {
            Texture2D srcTexture = new Texture2D(100, 100);
            Color32[] semiRedPixels = Enumerable.Repeat(1, srcTexture.width * srcTexture.height).Select(i => (Color32)new Color(0.5f, 0, 0, 0.5f)).ToArray();
            srcTexture.SetPixels32(semiRedPixels);
            srcTexture.Apply();

            RenderTexture dstTexture = new RenderTexture(srcTexture.width, srcTexture.height, 0);
            dstTexture.Create();

            int srcTextureId = (int)srcTexture.GetNativeTexturePtr();
            int dstTextureId = (int)dstTexture.GetNativeTexturePtr();

            GfxHelper.instance.TriggerGfxThreadCallback(() => GfxHelper.instance.CopyTexture(srcTextureId, dstTextureId));
            yield return null;
            yield return null;
            Assert.That(semiRedPixels.IsEqualTo(dstTexture.GetPixels32()));

            srcTexture = new Texture2D(20, 20);
            semiRedPixels = Enumerable.Repeat(1, srcTexture.width * srcTexture.height)
                            .Select(i => (Color32)new Color(0, 0, 0.5f, 0.5f)).ToArray();
            srcTexture.SetPixels32(semiRedPixels);
            srcTexture.Apply();

            srcTextureId = (int)srcTexture.GetNativeTexturePtr();
            GfxHelper.instance.TriggerGfxThreadCallback(() => GfxHelper.instance.CopyTexture(srcTextureId, dstTextureId));
            yield return null;
            yield return null;
            
            //As the dstTexture size is different with the srcTexture, thus here can not use isEqualTo
            Assert.That(dstTexture.GetPixels32().All(color => color.IsEqual(semiRedPixels[0])));
        }
    }
}

