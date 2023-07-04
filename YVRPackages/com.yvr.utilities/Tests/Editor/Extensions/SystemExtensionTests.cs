// ReSharper disable ExpressionIsAlwaysNull

using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace YVR.Utilities.Test
{
    [TestFixture]
    public class SystemExtensionTests
    {
        [Test]
        public void SafeInvoke_EmptyActionDefaultParams_Triggered()
        {
            string expectLog = "Dummy Method";
            MulticastDelegate action = new Action(DummyMethod);
            action.SafeInvoke();

            LogAssert.Expect(LogType.Log, expectLog);

            void DummyMethod() { Debug.Log(expectLog); }
        }

        [Test]
        public void SafeInvoke_EmptyActionEmptyParams_Triggered()
        {
            string expectLog = "Dummy Method";
            MulticastDelegate action = new Action(DummyMethod);
            action.SafeInvoke(null);

            LogAssert.Expect(LogType.Log, expectLog);

            void DummyMethod() { Debug.Log(expectLog); }
        }


        [Test]
        public void SafeInvoke_StringAction_Triggered()
        {
            MulticastDelegate action = new Action<string>(DummyMethod);
            action.SafeInvoke("abc");

            LogAssert.Expect(LogType.Log, "Value is abc");

            void DummyMethod(string value) { Debug.Log($"Value is {value}"); }
        }

        [Test]
        public void GetParameterCount_EmptyAction_GetNegativeOne()
        {
            Action dummyAction = null;
            MulticastDelegate action = dummyAction;
            int count = action.GetParametersCount();

            Assert.That(count == -1);
        }

        [Test]
        public void GetParameterCount_Action_Get0()
        {
            MulticastDelegate action = new Action(DummyMethod);
            int count = action.GetParametersCount();

            Assert.That(count == 0);

            void DummyMethod() { }
        }

        [Test]
        public void GetParameterCount_MultiAction_GetCorrectCount()
        {
            MulticastDelegate action = new Action<int, string, float>(DummyMethod);
            int count = action.GetParametersCount();
            Assert.That(count == 3);

            void DummyMethod(int i, string s, float f) { }
        }
    }
}