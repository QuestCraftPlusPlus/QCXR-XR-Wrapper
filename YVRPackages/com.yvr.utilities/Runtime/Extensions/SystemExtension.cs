using System;
using UnityEngine;

public static class SystemExtension
{
    #region MulticastDelegate

    /// <summary>
    /// Invokes each delegate in the invocation list of the specified MulticastDelegate object.
    /// </summary>
    /// <param name="action">The MulticastDelegate object whose invocation list is to be invoked.</param>
    /// <param name="parameters">The parameters to be passed to each delegate in the invocation list.</param>
    public static void SafeInvoke(this MulticastDelegate action, params object[] parameters)
    {
        Delegate[] handlerList = action.GetInvocationList();

        for (int i = 0; i != handlerList.Length; ++i)
        {
            ref Delegate handle = ref handlerList[i];

            try
            {
                // Convert parameter type to delegate required parameter type
                // E.g,If parameter type is int64, and delegate parameter is int32.
                // These two types will not be converted by default. Thus here needs manually conversion.
                if (parameters != null)
                {
                    for (int j = 0; j < parameters.Length; j++)
                    {
                        ref object param = ref parameters[j];
                        param = Convert.ChangeType(param, handle.Method.GetParameters()[j].ParameterType);
                    }
                }

                handle.Method.Invoke(handle.Target, parameters);
            } catch (Exception exception)
            {
                Debug.LogError("Exception in delegate: " + exception);
            }
        }
    }

    /// <summary>
    /// Returns the number of parameters of the <paramref name="action"/>
    /// </summary>
    /// <param name="action">The MulticastDelegate to examine.</param>
    /// <returns>The number of parameters of the <paramref name="action"/> </returns>
    /// <remarks>
    /// The examination of parameters number is based on the first delegate of <paramref name="action"/>.
    /// Thus, if <paramref name="action"/>'s invocation list is empty, -1 will be returned
    /// </remarks>
    public static int GetParametersCount(this MulticastDelegate action)
    {
        Delegate[] handlerList = action?.GetInvocationList();
        return handlerList == null || handlerList.Length == 0 ? -1 : handlerList[0].Method.GetParameters().Length;
    }

    #endregion
}