using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using YVR.Core;

namespace YVR.Core.Test
{
    [TestFixture, Category("CompositeLayer")]
    public class YVRLayerCreateDestroyTests
    {
        private YVRLayerAndroidHandler m_AndroidHandler = new YVRLayerAndroidHandler();

        private int m_LayerId = -1;

        [UnityTest, Order(1)]
        public IEnumerator CreateLayer_GetCallbackInNextFrame()
        {
            YVRLayerCreateInfo createInfo = new YVRLayerCreateInfo(-1, 400, 400, 1, YVRRenderLayerType.Quad);
            m_AndroidHandler.CreateLayerAsync(createInfo, (layerId) =>
            {
                Debug.Log("On layer created");
                m_LayerId = layerId;
            });

            yield return null; // The Layer should has been created in the next frame

            LogAssert.Expect(LogType.Log, "On layer created");
            Assert.That(m_LayerId, Is.Not.EqualTo(-1));
        }

        [UnityTest, Order(2)]
        public IEnumerator DestroyLayer_GetCallbackInNextFrame()
        {
            m_AndroidHandler.DestroyLayerAsync(m_LayerId, true, () =>
            {
                Debug.Log("On layer destroyed");
                m_LayerId = -1;
            });

            yield return null;

            LogAssert.Expect(LogType.Log, "On layer destroyed");
            Assert.That(m_LayerId, Is.EqualTo(-1));
        }


        [UnityTest, Order(3)]
        public IEnumerator CreateLayer_LayerCountIncreasedByOne()
        {
            int previousLayerCount = 0;
            int afterCreateLayerCount = 0;
            m_AndroidHandler.SetLayerPreSubmitCallback(() => previousLayerCount = m_AndroidHandler.GetLayersCount());
            yield return null;
            yield return null;

            YVRLayerCreateInfo createInfo = new YVRLayerCreateInfo(-1, 400, 400, 1, YVRRenderLayerType.Quad);
            m_AndroidHandler.CreateLayerAsync(createInfo, (layerId) =>
            {
                m_LayerId = layerId;
                afterCreateLayerCount = m_AndroidHandler.GetLayersCount();
            });

            yield return null;

            Assert.That(afterCreateLayerCount - previousLayerCount, Is.EqualTo(1));
        }

        [UnityTest, Order(4)]
        public IEnumerator DestroyLayer_LayerCountDecreasedByOne()
        {
            int previousLayerCount = 0;
            int afterCreateLayerCount = 0;
            m_AndroidHandler.SetLayerPreSubmitCallback(() => previousLayerCount = m_AndroidHandler.GetLayersCount());
            yield return null;
            yield return null;

            m_AndroidHandler.DestroyLayerAsync(m_LayerId, true, () =>
            {
                m_LayerId = -1;
                afterCreateLayerCount = m_AndroidHandler.GetLayersCount();
            });

            yield return null;

            Assert.That(afterCreateLayerCount - previousLayerCount, Is.EqualTo(-1));
        }

        [UnityTest]
        public IEnumerator CreateLayer_AutoShowAfterCreatedIsFalse_LayerCountMaintain()
        {
            int previousLayerCount = 0;
            int afterCreateLayerCount = 0;
            m_AndroidHandler.SetLayerPreSubmitCallback(() => previousLayerCount = m_AndroidHandler.GetLayersCount());
            yield return null;
            yield return null;

            YVRLayerCreateInfo createInfo = new YVRLayerCreateInfo(-1, 400, 400, 1, YVRRenderLayerType.Quad);
            createInfo.autoShowAfterCreated = false;
            m_AndroidHandler.CreateLayerAsync(createInfo, (layerId) =>
            {
                m_LayerId = layerId;
                afterCreateLayerCount = m_AndroidHandler.GetLayersCount();
            });

            yield return null;

            Assert.That(afterCreateLayerCount, Is.EqualTo(previousLayerCount));

            m_AndroidHandler.DestroyLayerAsync(m_LayerId, true, () =>
            {
                m_LayerId = -1;
                afterCreateLayerCount = m_AndroidHandler.GetLayersCount();
            });

            yield return null;
            Assert.That(afterCreateLayerCount, Is.EqualTo(previousLayerCount));
        }


        private static int[] bufferCount = new int[] {1, 3};

        [UnityTest, Category("CompositeLayer")]
        public IEnumerator CreateLayer_SwapBufferCount_GetDifferentColorHandles(
            [ValueSource(nameof(bufferCount))] int swapBufferCount)
        {
            int[] colorHandlesArray = new int[swapBufferCount * 2];
            int afterCreateLayerCount = 0;
            int afterDestroyLayerCount = 0;


            YVRLayerCreateInfo createInfo = new YVRLayerCreateInfo(-1, 400, 400, swapBufferCount, YVRRenderLayerType.Quad);
            bool idIsDifferentWithPreviousOne = true;
            bool colorsHandleIsALoop = true;

            m_AndroidHandler.CreateLayerAsync(createInfo, (layerId) =>
            {
                m_LayerId = layerId;


                for (int i = 0; i < swapBufferCount * 2; i++)
                {
                    colorHandlesArray[i] = m_AndroidHandler.GetLayerColorHandle(m_LayerId, i % swapBufferCount);
                }

                for (int i = 0; i < swapBufferCount; i++)
                {
                    Debug.Log("Color handle is " + colorHandlesArray[i]);
                    idIsDifferentWithPreviousOne = idIsDifferentWithPreviousOne && (swapBufferCount == 1 ||
                        colorHandlesArray[i] !=
                        colorHandlesArray[(i + 1) % swapBufferCount]);

                    colorsHandleIsALoop =
                        colorsHandleIsALoop && colorHandlesArray[i] == colorHandlesArray[i + swapBufferCount];
                }

                afterCreateLayerCount = m_AndroidHandler.GetLayersCount();
            });

            yield return null;

            Assert.That(idIsDifferentWithPreviousOne);
            Assert.That(colorsHandleIsALoop);

            m_AndroidHandler.DestroyLayerAsync(m_LayerId,
                true, () => { afterDestroyLayerCount = m_AndroidHandler.GetLayersCount(); });
            yield return null;

            Assert.That(afterDestroyLayerCount, Is.EqualTo(afterCreateLayerCount - 1));
        }

        private static int[] requiredCreateCount = new[] {2, 3, 4};

        [UnityTest, Category("CompositeLayer")]
        public IEnumerator CreateLayer_SeveralLayerInARow_GetDifferenceLayerIds(
            [ValueSource(nameof(requiredCreateCount))]
            int createCount)
        {
            List<int> createdLayers = new List<int>();
            YVRLayerCreateInfo createInfo = new YVRLayerCreateInfo(-1, 400, 400, 1, YVRRenderLayerType.Quad);
            for (int i = 0; i < createCount; i++)
                m_AndroidHandler.CreateLayerAsync(createInfo, (layerId) => createdLayers.Add(layerId));

            yield return null;

            Assert.That(createdLayers.Count, Is.EqualTo(createCount));
            Assert.That(createdLayers.Count, Is.EqualTo(createdLayers.Distinct().Count())); // ALl layer id is different

            List<int> destroyedLayers = new List<int>();
            for (int i = 0; i < createCount; i++)
            {
                int index = i;
                m_AndroidHandler.DestroyLayerAsync(createdLayers[index],
                    true, () => destroyedLayers.Add(createdLayers[index]));
            }

            yield return null;
            Assert.That(createdLayers.SequenceEqual(destroyedLayers));
        }

        [UnityTest]
        public IEnumerator DestroyLayer_NonCreatedLayer_LayerCountRemain()
        {
            int previousLayersCount = 0;
            int currentLayersCount = 0;
            m_AndroidHandler.SetLayerPreSubmitCallback(() => previousLayersCount = m_AndroidHandler.GetLayersCount());

            yield return null;
            yield return null;

            m_AndroidHandler.DestroyLayerAsync(-1, true, () => currentLayersCount = m_AndroidHandler.GetLayersCount());
            yield return null;

            Assert.That(currentLayersCount, Is.EqualTo(previousLayersCount));
        }
    }
}