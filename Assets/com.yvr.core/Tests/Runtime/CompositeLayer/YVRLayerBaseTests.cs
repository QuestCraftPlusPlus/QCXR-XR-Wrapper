using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace YVR.Core.Test
{
    [TestFixture, Category("CompositeLayer")]
    public class YVRLayerBaseTests
    {
        private YVRLayerAndroidHandler m_AndroidHandle = new YVRLayerAndroidHandler();

        [UnityTest, Order(1)]
        public IEnumerator GetLayersCount_OnlyEyeBuffer_BeOne()
        {
            int layersCount = 0;
            m_AndroidHandle.SetLayerPreSubmitCallback(() => layersCount = m_AndroidHandle.GetLayersCount());
            yield return null;
            yield return null;
            Assert.That(layersCount, Is.EqualTo(1));
        }

        [Test, Order(2)]
        public void SetLayerPreSubmitCallback_NullCallback_WontCrash()
        {
            m_AndroidHandle.SetLayerPreSubmitCallback(null);
        }

        // If preSubmit callback is set in frame x, it may not be triggered in frame (x + 1)
        // As the frame x may be submit while the frame (x+1) is executing, and it must be submitted before frame (x + 2)
        // Thus, here we check the callback in frame (x+2)
        [UnityTest, Order(3)]
        public IEnumerator SetLayerPreSubmitCallback_GetCallbackInNext2Frames()
        {
            m_AndroidHandle.SetLayerPreSubmitCallback(() => Debug.Log("Trigger PreSubmit callback"));
            yield return null;
            yield return null;
            LogAssert.Expect(LogType.Log, "Trigger PreSubmit callback");
        }

        [UnityTest, Order(4)]
        public IEnumerator SetLayerPreSubmitCallback_GetCallbackInOrder()
        {
            string result = "";

            for (int i = 0; i != 10; ++i)
            {
                int index = i;
                m_AndroidHandle.SetLayerPreSubmitCallback(() => result += index.ToString());
            }

            yield return null;
            yield return null;

            Debug.Log(result);

            LogAssert.Expect(LogType.Log, "0123456789");
        }
    }
}