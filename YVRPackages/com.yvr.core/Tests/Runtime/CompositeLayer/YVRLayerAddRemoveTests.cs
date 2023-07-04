using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace YVR.Core.Test
{
    [TestFixture, Category("CompositeLayer")]
    public class YVRLayerAddRemoveTests
    {
        private readonly IYVRLayerHandle m_AndroidHandler = new YVRLayerAndroidHandler();

        [UnityTest]
        public IEnumerator AddLayer_NonAddLayer_LayerCountIncreasedByOne()
        {
            int previousLayersCount = 0;
            int currentLayersCount = 0;
            int layerId = -1;
            YVRLayerCreateInfo createInfo = new YVRLayerCreateInfo(1, 20, 20, 3, YVRRenderLayerType.Quad)
            {
                autoShowAfterCreated = false
            };

            // Create Layer and record layers count
            m_AndroidHandler.CreateLayerAsync(createInfo, (id) =>
            {
                layerId = id;
                previousLayersCount = m_AndroidHandler.GetLayersCount();
            });
            yield return null;

            // Layer Count increased by one after add layer
            m_AndroidHandler.AddLayerAsync(layerId, () => currentLayersCount = m_AndroidHandler.GetLayersCount());
            yield return null;
            Assert.That((currentLayersCount - previousLayersCount), Is.EqualTo(1));


            m_AndroidHandler.DestroyLayerAsync(layerId, true, () => currentLayersCount = m_AndroidHandler.GetLayersCount());
            yield return null;
        }

        [UnityTest]
        public IEnumerator AddLayer_AlreadyAddedLayer_LayerCountRemain()
        {
            int previousLayersCount = 0;
            int currentLayersCount = 0;
            int layerId = -1;
            YVRLayerCreateInfo createInfo = new YVRLayerCreateInfo(1, 20, 20, 3, YVRRenderLayerType.Quad)
            {
                autoShowAfterCreated = true
            };

            // Create Layer and record layers count
            m_AndroidHandler.CreateLayerAsync(createInfo, (id) =>
            {
                layerId = id;
                previousLayersCount = m_AndroidHandler.GetLayersCount();
            });
            yield return null;

            // Layer Count increased by one after add layer
            m_AndroidHandler.AddLayerAsync(layerId, () => currentLayersCount = m_AndroidHandler.GetLayersCount());
            yield return null;
            Assert.That(currentLayersCount, Is.EqualTo(previousLayersCount));


            // After destroy added layer, layer count should equal to recorded count
            m_AndroidHandler.DestroyLayerAsync(layerId, true, () => currentLayersCount = m_AndroidHandler.GetLayersCount());
            yield return null;
        }

        [UnityTest]
        public IEnumerator AddLayer_NonCreatedLayer_LayerCountRemain()
        {
            int previousLayersCount = 0;
            int currentLayersCount = 0;
            m_AndroidHandler.SetLayerPreSubmitCallback(() => previousLayersCount = m_AndroidHandler.GetLayersCount());
            yield return null;
            yield return null;

            m_AndroidHandler.AddLayerAsync(-1, () => currentLayersCount = m_AndroidHandler.GetLayersCount());
            yield return null;

            Assert.That(currentLayersCount, Is.EqualTo(previousLayersCount));
        }


        [UnityTest]
        public IEnumerator RemoveLayer_AddedLayer_LayerCountDecreaseByOne()
        {
            int afterCreateLayerCount = 0;
            int afterRemoveLayerCount = 0;
            int afterDestroyLayerCount = 0;
            int layerId = -1;
            YVRLayerCreateInfo createInfo = new YVRLayerCreateInfo(1, 20, 20, 3, YVRRenderLayerType.Quad)
            {
                autoShowAfterCreated = true
            };

            // Create Layer and record layers count
            m_AndroidHandler.CreateLayerAsync(createInfo, (id) =>
            {
                layerId = id;
                afterCreateLayerCount = m_AndroidHandler.GetLayersCount();
            });
            yield return null;

            m_AndroidHandler.RemoveLayerAsync(layerId, () => afterRemoveLayerCount = m_AndroidHandler.GetLayersCount());
            yield return null;
            Assert.That(afterRemoveLayerCount, Is.EqualTo(afterCreateLayerCount - 1));

            m_AndroidHandler.DestroyLayerAsync(layerId, true, () => afterDestroyLayerCount = m_AndroidHandler.GetLayersCount());
            yield return null;
        }

        [UnityTest]
        public IEnumerator RemoveLayer_NonCreatedLayer_LayerCountRemain()
        {
            int previousLayersCount = 0;
            int afterRemoveLayersCount = 0;
            m_AndroidHandler.SetLayerPreSubmitCallback(() => previousLayersCount = m_AndroidHandler.GetLayersCount());
            yield return null;
            yield return null;

            m_AndroidHandler.RemoveLayerAsync(UnityEngine.Random.Range(-100, -1), () => afterRemoveLayersCount = m_AndroidHandler.GetLayersCount());
            yield return null;

            Assert.That(previousLayersCount, Is.EqualTo(afterRemoveLayersCount));
        }

        [UnityTest]
        public IEnumerator RemoveLayer_RemovedLayer_LayerCountRemain()
        {
            int afterCreateLayerCount = 0;
            int afterRemoveLayerCount = 0;
            int afterRepeatRemoveLayerCount = 0;
            int afterDestroyLayerCount = 0;
            int layerId = -1;
            YVRLayerCreateInfo createInfo = new YVRLayerCreateInfo(1, 20, 20, 3, YVRRenderLayerType.Quad);

            // Create Layer and record layers count
            m_AndroidHandler.CreateLayerAsync(createInfo, (id) =>
            {
                layerId = id;
                afterCreateLayerCount = m_AndroidHandler.GetLayersCount();
            });
            yield return null;

            // Remove just added layer
            m_AndroidHandler.RemoveLayerAsync(layerId, () => afterRemoveLayerCount = m_AndroidHandler.GetLayersCount());
            yield return null;

            // Remove removed layer, the layer count should maintain
            m_AndroidHandler.RemoveLayerAsync(layerId, () => afterRepeatRemoveLayerCount = m_AndroidHandler.GetLayersCount());
            yield return null;
            Assert.That(afterRepeatRemoveLayerCount, Is.EqualTo(afterRemoveLayerCount));

            m_AndroidHandler.DestroyLayerAsync(layerId, true, () => afterDestroyLayerCount = m_AndroidHandler.GetLayersCount());
            yield return null;
        }

        private static int[] requiredCreateCount = new[] {2, 3, 4};

        [UnityTest, Category("CompositeLayer")]
        public IEnumerator AddLayer_RemoveLayer_SeveralCreatedLayerInARow_LayerCountCorrect([ValueSource(nameof(requiredCreateCount))] int createCount)
        {
            int previousLayersCount = 0;
            int afterCreateLayersCount = 0;
            int afterAddedLayersCount = 0;
            int afterRemovedLayersCount = 0;
            int afterDestroyedLayersCount = 0;

            m_AndroidHandler.SetLayerPreSubmitCallback(() => previousLayersCount = m_AndroidHandler.GetLayersCount());
            yield return null;
            yield return null;


            List<int> createdLayers = new List<int>();
            YVRLayerCreateInfo createInfo = new YVRLayerCreateInfo(-1, 400, 400, 1, YVRRenderLayerType.Quad)
            { autoShowAfterCreated = false };

            for (int i = 0; i < createCount; i++)
            {
                m_AndroidHandler.CreateLayerAsync(createInfo, (layerId) =>
                {
                    createdLayers.Add(layerId);
                    afterCreateLayersCount = m_AndroidHandler.GetLayersCount();
                });
            }

            yield return null;

            // As auto show is false, layers count should remain
            Assert.That(afterCreateLayersCount, Is.EqualTo(previousLayersCount));

            // After added, layers count should be previous + addedCount
            for (int i = 0; i < createCount; i++)
            {
                m_AndroidHandler.AddLayerAsync(createdLayers[i], () => { afterAddedLayersCount = m_AndroidHandler.GetLayersCount(); });
            }

            yield return null;
            Assert.That(afterAddedLayersCount, Is.EqualTo(previousLayersCount + createCount));

            // Added layer
            for (int i = 0; i < createCount; i++)
            {
                m_AndroidHandler.AddLayerAsync(createdLayers[i], () => { afterAddedLayersCount = m_AndroidHandler.GetLayersCount(); });
            }

            yield return null;
            Assert.That(afterAddedLayersCount, Is.EqualTo(previousLayersCount + createCount));

            // Removed Layer
            for (int i = 0; i < createCount; i++)
            {
                m_AndroidHandler.RemoveLayerAsync(createdLayers[i], () => { afterRemovedLayersCount = m_AndroidHandler.GetLayersCount(); });
            }

            yield return null;
            Assert.That(afterRemovedLayersCount, Is.EqualTo(previousLayersCount));

            // Delete created layers
            for (int i = 0; i < createCount; i++)
            {
                m_AndroidHandler.DestroyLayerAsync(createdLayers[i], true, () => { afterDestroyedLayersCount = m_AndroidHandler.GetLayersCount(); });
            }

            yield return null;
            Assert.That(afterDestroyedLayersCount, Is.EqualTo(afterRemovedLayersCount));
        }
    }
}