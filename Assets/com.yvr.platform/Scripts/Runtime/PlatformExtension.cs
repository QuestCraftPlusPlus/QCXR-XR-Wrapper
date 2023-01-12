using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// The extensions class of platform sdk
    /// </summary>
    public static class PlatformExtension
    {
        /// <summary>
        /// Turns string array to android java object
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static AndroidJavaObject javaArrayFromCS(this string[] values)
        {
            AndroidJavaClass arrayClass = new AndroidJavaClass("java.lang.reflect.Array");

            AndroidJavaObject arrayObject = arrayClass.CallStatic<AndroidJavaObject>("newInstance", new AndroidJavaClass("java.lang.String"), values.Length);

            for (int i = 0; i < values.Length; ++i)
            {
                arrayClass.CallStatic("set", arrayObject, i, new AndroidJavaObject("java.lang.String", values[i]));
            }

            return arrayObject;
        }
    }
}