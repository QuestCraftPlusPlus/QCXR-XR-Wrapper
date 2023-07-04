using System;
using System.Collections;
using System.Collections.Generic;
using AOT;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace YVR.Utilities.Test
{
    [TestFixture]
    public class GfxHelperCallbackTests
    {
        private static int s_CallbackOrderedCounter = 0;
        private static string s_CallbackOrderedResultStr = "";

        [UnityTest]
        public IEnumerator TriggerGfxThreadCallback_BeCalled()
        {
            GfxHelper.instance.TriggerGfxThreadCallback(new Action(GfxCallback));
            yield return null; // Wait two frame to ensure the callback be triggered
            yield return null;
            LogAssert.Expect(LogType.Log, "Rendering thread callback triggered");
        }

        [UnityTest]
        public IEnumerator TriggerGfxThreadCallback_BeOrderedCalled()
        {
            int testCount = 10;
            string expcetedOrderedResultStr = "";
            for (int i = 0; i != testCount; ++i)
            {
                GfxHelper.instance.TriggerGfxThreadCallback(new Action(GfxCallbackOutputCounter));
                yield return null; // Wait two frame to ensure the callback be triggered
                yield return null;
                expcetedOrderedResultStr += $"{s_CallbackOrderedCounter}";
                s_CallbackOrderedCounter++;
            }

            Debug.Log(s_CallbackOrderedResultStr);
            LogAssert.Expect(LogType.Log, expcetedOrderedResultStr);
        }


        [MonoPInvokeCallback(typeof(Action))]
        public static void GfxCallback()
        {
            Debug.Log("Rendering thread callback triggered");
        }

        [MonoPInvokeCallback(typeof(Action))]
        public static void GfxCallbackOutputCounter() { s_CallbackOrderedResultStr += $"{s_CallbackOrderedCounter}"; }
    }
}
