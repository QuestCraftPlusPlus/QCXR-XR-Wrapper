using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using YVR.Core.Demo;

#if USE_GRAPHIC_TESTS
namespace YVR.Core.Test
{
    public class YVGraphicRraycasterTest : IPrebuildSetup
    {
        public void Setup()
        {
            GraphicsTestUtil.CopyTestFile(Path.GetFullPath("Packages/com.yvr.core/Tests/Runtime/Resources"), "/sdcard/");
        }

        private static object[] s_ButtonClickTestParm =
        {
            new TestCaseData("/sdcard/Resources/YTests.record","/sdcard/Resources/ButtonClickTest.png").SetName("ButtonClickTest").Returns(null)
        };

        [UnityTest, Order(1)]
        [TestCaseSource(nameof(s_ButtonClickTestParm))]
        public IEnumerator ButtonClickTest(string recordPath, string referenceImagePath)
        {
            var asyncLoad = SceneManager.LoadSceneAsync("Raycasting");
            yield return new WaitUntil(() => asyncLoad.isDone);
            Screen.SetResolution(GraphicsTestUtil.GetGraphicsTestSetting.ImageComparisonSettings.TargetWidth,
                GraphicsTestUtil.GetGraphicsTestSetting.ImageComparisonSettings.TargetHeight, false);
            GraphicsTestUtil.YVRStartReplay(recordPath);
            RaycastTest raycastTest = GameObject.FindObjectOfType<RaycastTest>();
            yield return new WaitUntil(() => { return raycastTest.clickMark; });
            Debug.Log("ButtonClickTest start");
            GraphicsTestUtil.ComparedGraphics(referenceImagePath);
        }

        private static object[] s_SliderDragTestParm =
        {
            new TestCaseData("/sdcard/Resources/SliderDragTest.png").SetName("SliderDragTest").Returns(null)
        };

        [UnityTest, Order(2)]
        [TestCaseSource(nameof(s_SliderDragTestParm))]
        public IEnumerator SliderDragTest(string referenceImagePath)
        {
            RaycastTest raycastTest = GameObject.FindObjectOfType<RaycastTest>();
            yield return new WaitUntil(() => { return raycastTest.sliderDrag.value==1; });
            GraphicsTestUtil.ComparedGraphics(referenceImagePath);
        }

        private static object[] s_ScrollViewDragTestParm =
        {
            new TestCaseData("/sdcard/Resources/ScrollViewDragTest.png").SetName("ScrollViewDragTest").Returns(null)
        };

        [UnityTest, Order(3)]
        [TestCaseSource(nameof(s_ScrollViewDragTestParm))]
        public IEnumerator ScrollViewDragTest(string referenceImagePath)
        {
            RaycastTest raycastTest = GameObject.FindObjectOfType<RaycastTest>();
            yield return new WaitUntil(() => { return raycastTest.scrollRect.verticalScrollbar.value == 0; });
            GraphicsTestUtil.ComparedGraphics(referenceImagePath);
        }

        private static object[] s_RaycastHitGameObjectTestParm =
        {
            new TestCaseData("/sdcard/Resources/RaycastHitGameObjectTest.png").SetName("RaycastHitGameObjectTest").Returns(null)
        };

        [UnityTest, Order(4)]
        [TestCaseSource(nameof(s_RaycastHitGameObjectTestParm))]
        public IEnumerator RaycastHitGameObjectTest(string referenceImagePath)
        {
            RaycastTest raycastTest = GameObject.FindObjectOfType<RaycastTest>();
            yield return new WaitUntil(() => { return raycastTest.RaycastHitGameObject(); });
            GraphicsTestUtil.ComparedGraphics(referenceImagePath);
        }
    }
}
#endif