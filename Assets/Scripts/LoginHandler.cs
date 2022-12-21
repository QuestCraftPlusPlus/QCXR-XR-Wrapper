using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginHandler : MonoBehaviour
{
    public WindowHandler handler;
    public JNIStorage storage;

    public void Login()
    {
        storage.accountObj = storage.apiClass.CallStatic<AndroidJavaObject>("login", "d17a73a2-707c-40f5-8c90-d3eda0956f10");
        if (storage.accountObj != null)
        {
            storage.instance1182 = storage.apiClass.CallStatic<AndroidJavaObject>("load", "1.18.2-fabric", storage.home);
            storage.instance1193 = storage.apiClass.CallStatic<AndroidJavaObject>("load", "1.19.3-fabric", storage.home);
            handler.MainPanelSwitch();
        } else {
            AndroidJavaClass modloaderEnum = new AndroidJavaClass("pojlib.api.API_V1$ModLoader");
            AndroidJavaObject fabricModloader = modloaderEnum.GetStatic<AndroidJavaObject>("Fabric");
            AndroidJavaObject[] versions = storage.apiClass.CallStatic<AndroidJavaObject[]>("getMinecraftVersions");
            foreach(AndroidJavaObject obj in versions)
            {
                if(obj.Get<string>("id") == "1.19.3")
                {
                    storage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", storage.activity, "1.19.3-fabric", storage.home, obj, fabricModloader);
                }
                if (obj.Get<string>("id") == "1.18.2")
                {
                    storage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", storage.activity, "1.18.2-fabric", storage.home, obj, fabricModloader);
                }
            }
        }
    }
}