using System.IO;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using YVR.Utilities;

namespace YVR.Core.Test
{
    [TestFixture, Category("CompositeLayer")]
    public class YVRCompositeLayerCreateDestroyTests : IPrebuildSetup
    {
        private const string k_AutoInitPrefabPath = "Assets/Resources/CompositeLayer/AutoInit.prefab";
        private const string k_NotAutoInitPrefabPath = "Assets/Resources/CompositeLayer/NotAutoInit.prefab";

        private IYVRLayerHandle m_LayerHandle = new YVRLayerAndroidHandler();
        private GameObject m_GameObject = null;
        private YVRCompositeLayer m_CompositeLayer = null;

        public void Setup()
        {
#if UNITY_EDITOR
            GameObject compositeLayerGO = new GameObject("CompositeLayer");
            YVRCompositeLayer compositeLayer = compositeLayerGO.AddComponent<YVRCompositeLayer>();
            compositeLayer.TrySetFieldValue("autoInitLayer", true);

            if (!Directory.Exists("Assets/Resources/CompositeLayer/"))
                Directory.CreateDirectory("Assets/Resources/CompositeLayer/");

            PrefabUtility.SaveAsPrefabAsset(compositeLayerGO, k_AutoInitPrefabPath);

            compositeLayer.TrySetFieldValue("autoInitLayer", false);
            PrefabUtility.SaveAsPrefabAsset(compositeLayerGO, k_NotAutoInitPrefabPath);

            Object.DestroyImmediate(compositeLayerGO);
#endif
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
#if UNITY_EDITOR
            AssetDatabase.DeleteAsset(k_AutoInitPrefabPath);
            AssetDatabase.DeleteAsset(k_NotAutoInitPrefabPath);
#endif
        }

        [UnityTest, Order(1)]
        public IEnumerator InstantiateCompositeLayer_AutoInit_LayerCountIncreasedByOne()
        {
            int beforeInstantiateLayerCount = 0;
            int afterInstantiateLayerCount = 0;
            GfxHelper.instance.TriggerGfxThreadCallback(() => beforeInstantiateLayerCount = m_LayerHandle.GetLayersCount());
            yield return null;
            yield return null;

            m_GameObject = Object.Instantiate(Resources.Load("CompositeLayer/AutoInit")) as GameObject;
            m_CompositeLayer = m_GameObject?.GetComponent<YVRCompositeLayer>();
            yield return null;
            yield return null;

            GfxHelper.instance.TriggerGfxThreadCallback(() => afterInstantiateLayerCount = m_LayerHandle.GetLayersCount());
            yield return null;
            yield return null;

            Assert.That(afterInstantiateLayerCount, Is.EqualTo(beforeInstantiateLayerCount + 1));
        }

        [UnityTest, Order(2)]
        public IEnumerator DestroyCompositeLayer_InitializedCompLYR_LayerCountDecreasedByOne()
        {
            int beforeDestroyedLayerCount = 0;
            int afterDestroyedLayerCount = 0;
            GfxHelper.instance.TriggerGfxThreadCallback(() => beforeDestroyedLayerCount = m_LayerHandle.GetLayersCount());
            yield return null;
            yield return null;

            Object.DestroyImmediate(m_GameObject);
            yield return null;
            yield return null;

            GfxHelper.instance.TriggerGfxThreadCallback(() => afterDestroyedLayerCount = m_LayerHandle.GetLayersCount());
            yield return null;
            yield return null;

            Assert.That(afterDestroyedLayerCount, Is.EqualTo(beforeDestroyedLayerCount - 1));
        }

        [UnityTest, Order(3)]
        public IEnumerator InstantiateCompositeLayer_NonAutoInit_LayerCountMaintain()
        {
            int beforeInstantiateLayerCount = 0;
            int afterInstantiateLayerCount = 0;
            GfxHelper.instance.TriggerGfxThreadCallback(() => beforeInstantiateLayerCount = m_LayerHandle.GetLayersCount());
            yield return null;
            yield return null;

            m_GameObject = Object.Instantiate(Resources.Load("CompositeLayer/NotAutoInit")) as GameObject;
            m_CompositeLayer = m_GameObject?.GetComponent<YVRCompositeLayer>();
            yield return null;
            yield return null;

            GfxHelper.instance.TriggerGfxThreadCallback(() => afterInstantiateLayerCount = m_LayerHandle.GetLayersCount());
            yield return null;
            yield return null;

            Assert.That(afterInstantiateLayerCount, Is.EqualTo(beforeInstantiateLayerCount));
        }

        [UnityTest, Order(4)]
        public IEnumerator DestroyCompositeLayer_NonInitializedCompLYR_LayerCountMaintain()
        {
            int beforeDestroyedLayerCount = -1;
            int afterDestroyedLayerCount = -1;
            GfxHelper.instance.TriggerGfxThreadCallback(() => beforeDestroyedLayerCount = m_LayerHandle.GetLayersCount());
            yield return null;
            yield return null;

            Object.DestroyImmediate(m_GameObject);
            yield return null;
            yield return null;

            GfxHelper.instance.TriggerGfxThreadCallback(() => afterDestroyedLayerCount = m_LayerHandle.GetLayersCount());
            yield return null;
            yield return null;

            Assert.That(afterDestroyedLayerCount, Is.EqualTo(beforeDestroyedLayerCount));
        }

        [UnityTest, Order(5)]
        public IEnumerator InstantiateCompositeLayer_AutoInit_CreatedCallback_AddedCallback()
        {
            const string layerAddedMsg = "Layer Added";
            const string layerCreatedMsg = "Layer Created";
            m_GameObject = Object.Instantiate(Resources.Load("CompositeLayer/AutoInit")) as GameObject;
            m_CompositeLayer = m_GameObject?.GetComponent<YVRCompositeLayer>();
            m_CompositeLayer.onLayerAddedGfx += () => Debug.Log(layerAddedMsg);
            m_CompositeLayer.onLayerCreatedGfx += (layerId) => Debug.Log(layerCreatedMsg);

            yield return null;
            yield return null;

            LogAssert.Expect(LogType.Log, layerCreatedMsg);
            LogAssert.Expect(LogType.Log, layerAddedMsg);
        }

        [UnityTest, Order(6)]
        public IEnumerator DestroyCompositeLayer_AutoInit_DestroyCallback_RemovedCallback()
        {
            const string layerRemovedMsg = "Layer Removed";
            const string layerDestroyedMsg = "Layer Destroyed";

            m_CompositeLayer.onLayerRemovedGfx += () => Debug.Log(layerRemovedMsg);
            m_CompositeLayer.onLayerDestroyedGfx += () => Debug.Log(layerDestroyedMsg);

            Object.DestroyImmediate(m_GameObject);

            yield return null;
            yield return null;

            LogAssert.Expect(LogType.Log, layerRemovedMsg);
            LogAssert.Expect(LogType.Log, layerDestroyedMsg);
        }
    }
}