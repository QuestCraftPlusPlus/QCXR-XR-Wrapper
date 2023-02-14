using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ThirdSDKTest : MonoBehaviour
{
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 200, 100), "test"))
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.yvr.thirdsdk.AccountManager");
            AndroidJavaObject obj = jc.CallStatic<AndroidJavaObject>("getTestResult");

            Debug.Log(obj == null);

            string str = jc.CallStatic<string>("getStringResult", obj);
            Debug.Log(str);
        }
    }
}
