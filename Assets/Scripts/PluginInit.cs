using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PluginInit : MonoBehaviour
{
    AndroidJavaClass unityClass;
    static private AndroidJavaObject unityActivity;

    private static AndroidJavaObject m_PluginInstance = null;
    public static AndroidJavaObject _pluginInstance
    {
        get
        {
            if(m_PluginInstance==null)
            {
                InitializePlugin("pojlib.api.API_V1");
            }
            return m_PluginInstance;
        }
    }
    
    // // Start is called before the first frame update
    // void Start()
    // {
    //     InitializePlugin("pojlib.api.API_V1");
    // }

    static void InitializePlugin(string pluginName)
    {
        Debug.Log("Try to initialize plugin");
        m_PluginInstance = new AndroidJavaObject(pluginName);
        if (m_PluginInstance == null)
        {
            Debug.Log("Plugin Instance Error!");
        }

        m_PluginInstance.CallStatic("recieveUnityActivity", unityActivity);
    }

    public void Add()
    {
        if (m_PluginInstance != null)
        {
            var result = m_PluginInstance.Call<int>("Add", 5, 6);
            Debug.Log("Add Result from Unity: " +result);
        }
    }
}
