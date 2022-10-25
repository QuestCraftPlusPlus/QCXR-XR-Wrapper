using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginHandler : MonoBehaviour
{
    public string ACCESS_TOKEN;
    public string OAUTH_CODE;
    AndroidJavaClass unityClass;
    private AndroidJavaObject unityActivity;

    
    public void SwitchTask()
    {
        unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        PluginInit._pluginInstance.CallStatic("activity", unityActivity);
    }
}