using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using YVR.Utilities;

namespace YVR.Utilities.Test
{
    public class UnityObjectReflectionTests
    {
        private MonoBehaviourForTest m_MonoForTest = null;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            GameObject go = new GameObject();
            m_MonoForTest = go.AddComponent<MonoBehaviourForTest>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Object.DestroyImmediate(m_MonoForTest);
        }

        private static string s_PrivateIntFieldName = "m_PrivateIntVal";
        private static string s_PrivateIntPropertyName = "privateIntVal";
        [Test]
        public void TryGetFieldValue_ExistPrivateField_GetCorrectValue()
        {
            bool valueGot = m_MonoForTest.TryGetFieldValue(s_PrivateIntFieldName, out int value);
            Assert.That(valueGot);
            Assert.That(value, Is.EqualTo(-1));
        }

        [Test]
        public void TrySetFieldValue_ExistPrivateField_SetValueSucceed()
        {
            int targetValue = 1;
            bool valueSet = m_MonoForTest.TrySetFieldValue(s_PrivateIntFieldName, targetValue);
            Assert.That(valueSet);

            m_MonoForTest.TryGetFieldValue(s_PrivateIntFieldName, out int value);
            Assert.That(value, Is.EqualTo(targetValue));
        }

        [Test]
        public void TryGetPropertyValue_ExistPrivateProperty_GetCorrectValue()
        {
            bool valueGot = m_MonoForTest.TryGetPropertyValue(s_PrivateIntPropertyName, out int value);
            Assert.That(valueGot);
            Assert.That(value, Is.EqualTo(-1));
        }

        [Test]
        public void TrySetPropertyValue_ExistPrivateProperty_SetValueSucceed()
        {
            int targetValue = 1;
            bool valueSet = m_MonoForTest.TrySetPropertyValue(s_PrivateIntPropertyName, targetValue);
            Assert.That(valueSet);

            m_MonoForTest.TryGetPropertyValue(s_PrivateIntPropertyName, out int value);
            Assert.That(value, Is.EqualTo(targetValue));
        }
    }
}
