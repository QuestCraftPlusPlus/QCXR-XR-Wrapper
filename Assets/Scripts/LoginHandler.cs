using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginHandler : MonoBehaviour
{
    public WindowHandler handler;

    public void Login()
    {
        JNIStorage.accountObj = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("login", "d17a73a2-707c-40f5-8c90-d3eda0956f10");
        if (JNIStorage.accountObj != null)
        {
            if(!JNIStorage.apiClass.GetStatic<bool>("finishedDownloading"))
            {
                JNIStorage.apiClass.SetStatic("msaMessage", "Please wait before pressing again, instances are still downloading.");
                return;
            }
            JNIStorage.instance1182 = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("load", "1.18.2-fabric", JNIStorage.home);
            JNIStorage.instance1193 = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("load", "1.19.3-fabric", JNIStorage.home);
            handler.MainPanelSwitch();
        } else {
            AndroidJavaClass modloaderEnum = new AndroidJavaClass("pojlib.api.API_V1$ModLoader");
            AndroidJavaObject fabricModloader = modloaderEnum.GetStatic<AndroidJavaObject>("Fabric");
            AndroidJavaObject[] versions = JNIStorage.apiClass.CallStatic<AndroidJavaObject[]>("getMinecraftVersions");
            foreach(AndroidJavaObject obj in versions)
            {
                if(obj.Get<string>("id") == "1.19.3")
                {
                    JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, "1.19.3-fabric", JNIStorage.home, obj, fabricModloader);
                }
                if (obj.Get<string>("id") == "1.18.2")
                {
                    JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, "1.18.2-fabric", JNIStorage.home, obj, fabricModloader);
                }
            }
        }
    }
}