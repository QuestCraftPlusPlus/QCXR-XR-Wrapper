using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PluginInit : MonoBehaviour
{
    AndroidJavaClass unityClass;
    private AndroidJavaObject unityActivity;
    public static AndroidJavaObject _pluginInstance;
    
    // Start is called before the first frame update
    void Start()
    {
        InitializePlugin("quartz.pojlib.api.API_V1");
    }

    void InitializePlugin(string pluginName)
    {
        _pluginInstance = new AndroidJavaObject(pluginName);
        if (_pluginInstance == null)
        {
            Debug.Log("Plugin Instance Error!");
        }

        _pluginInstance.CallStatic("recieveUnityActivity", unityActivity);
    }

    public void Add()
    {
        if (_pluginInstance != null)
        {
            var result = _pluginInstance.Call<int>("Add", 5, 6);
            Debug.Log("Add Result from Unity: " +result);
        }
    }
}
