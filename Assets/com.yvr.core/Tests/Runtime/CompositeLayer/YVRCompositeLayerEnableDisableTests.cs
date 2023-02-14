using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using YVR.Utilities;

namespace YVR.Core.Test
{
    [TestFixture, Category("CompositeLayer")]
    public class YVRCompositeLayerEnableDisableTests : IPrebuildSetup
    {
        private const string k_AutoInitPrefabPath = "Assets/Resources/CompositeLayer/AutoInit.prefab";
        private GameObject m_GameObject = null;
        private YVRCompositeLayer m_CompositeLayer = null;
        private IYVRLayerHandle m_LayerHandle = new YVRLayerAndroidHandler();

        public void Setup()
        {
#if UNITY_EDITOR
            GameObject compositeLayerGO = new GameObject("CompositeLayer");
            YVRCompositeLayer compositeLayer = compositeLayerGO.AddComponent<YVRCompositeLayer>();
            compositeLayer.TrySetFieldValue("autoInitLayer", true);

            if (!Directory.Exists("Assets/Resources/CompositeLayer/"))
                Directory.CreateDirectory("Assets/Resources/CompositeLayer/");

            PrefabUtility.SaveAsPrefabAsset(compositeLayerGO, k_AutoInitPrefabPath);

            Object.DestroyImmediate(compositeLayerGO);
#endif
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
#if UNITY_EDITOR
            AssetDatabase.DeleteAsset(k_AutoInitPrefabPath);
#endif
        }

        [SetUp]
        public void TestSetup()
        {
            m_GameObject = Object.Instantiate(Resources.Load("CompositeLayer/AutoInit")) as GameObject;
            m_CompositeLayer = m_GameObject?.GetComponent<YVRCompositeLayer>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(m_GameObject);
        }

        [UnityTest, Order(1)]
        public IEnumerator DisableCompositeLayer_Initialized_Enabled_LayerCountDecreasedByOne()
        {
            int beforeDisabledLayersCount = 0;
            int afterDisabledLayersCount = 0;
            GfxHelper.instance.TriggerGfxThreadCallback(() => beforeDisabledLayersCount = m_LayerHandle.GetLayersCount());
            yield return null;
            yield return null;

            m_GameObject.SetActive(false);
            yield return null;
            yield return null;

            GfxHelper.instance.TriggerGfxThreadCallback(() => afterDisabledLayersCount = m_LayerHandle.GetLayersCount());
            yield return null;
            yield return null;

            Assert.That(afterDisabledLayersCount, Is.EqualTo(beforeDisabledLayersCount - 1));
        }

        [UnityTest, Order(2)]
        public IEnumerator EnableCompositeLayer_Initialized_Disabled_LayerCountIncreasedByOne()
        {
            int beforeEnabledLayersCount = 0;
            int afterEnabledLayersCount = 0;

            m_GameObject.SetActive(false);
            yield return null;
            yield return null;

            GfxHelper.instance.TriggerGfxThreadCallback(() => beforeEnabledLayersCount = m_LayerHandle.GetLayersCount());
            yield return null;
            yield return null;

            m_GameObject.SetActive(true);
            yield return null;
            yield return null;

            GfxHelper.instance.TriggerGfxThreadCallback(() => afterEnabledLayersCount = m_LayerHandle.GetLayersCount());
            yield return null;
            yield return null;

            Assert.That(afterEnabledLayersCount, Is.EqualTo(beforeEnabledLayersCount + 1));
        }

        [UnityTest, Order(3)]
        public IEnumerator DisableCompositeLayer_Initialized_Enabled_GetLayerRemovedCallback()
        {
            m_CompositeLayer.onLayerRemovedGfx += () => Debug.Log("Layer removed for disable");
            m_GameObject.SetActive(false);
            yield return null;
            yield return null;

            LogAssert.Expect(LogType.Log, "Layer removed for disable");
        }

        [UnityTest, Order(4)]
        public IEnumerator EnableCompositeLayer_Initialized_Disabled_GetLayerAddedCallback()
        {
            m_CompositeLayer.onLayerAddedGfx += () => Debug.Log("Layer added for enable");
            m_GameObject.SetActive(false);
            yield return null;
            yield return null;
            m_GameObject.SetActive(true);
            yield return null;
            yield return null;

            LogAssert.Expect(LogType.Log, "Layer added for enable");
        }

        // [UnityTest, Order(5)]
        // public IEnumerator EnabledCompositeLayer_AutoInit_GetLayerMatrixUpdatedCallback()
        // {
        //     m_CompositeLayer.onLayerMatrixUpdated += () => Debug.Log("Layer matrix updated when enabled");
        //     yield return null;
        //     yield return null;
        //     LogAssert.Expect(LogType.Log, "Layer matrix updated when enabled");
        // }
        //
        // [UnityTest, Order(6)]
        // public IEnumerator DisabledCompositeLayer_AutoInit_NotGetLayerMatrixUpdatedCallback()
        // {
        //     m_GameObject.SetActive(false);
        //     m_CompositeLayer.onLayerMatrixUpdated += () => Debug.Log("Layer matrix updated when disabled");
        //     yield return null;
        //     yield return null;
        //     LogAssert.NoUnexpectedReceived();
        // }
    }
}