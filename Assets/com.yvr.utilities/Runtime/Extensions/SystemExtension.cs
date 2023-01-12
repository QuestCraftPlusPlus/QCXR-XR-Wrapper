using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemExtension
{
    public static void SafeInvoke(this MulticastDelegate action, params object[] parameters)
    {
        Delegate[] handlerList = action.GetInvocationList();

        for (int i = 0; i != handlerList.Length; ++i)
        {
            try
            {
                handlerList[i].Method.Invoke(handlerList[i].Target, parameters);
            }
            catch (Exception exception)
            {
                Debug.LogError("Exception in delegate: " + exception.ToString());
            }
        }
    }
}
