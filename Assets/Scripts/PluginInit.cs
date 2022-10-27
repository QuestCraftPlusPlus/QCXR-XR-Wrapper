using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PluginInit : MonoBehaviour
{
    static private AndroidJavaObject unityActivity;
    private static AndroidJavaObject m_PluginInstance = null;

    public static AndroidJavaObject _pluginInstance
    {
        get
        {
            if(m_PluginInstance==null)
            {
                InitializePlugin("pojlib/api/PluginInstance");
            }
            return m_PluginInstance;
        }
    }

    static void InitializePlugin(string pluginName)
    {
        Debug.Log("Try to initialize plugin");
        m_PluginInstance = new AndroidJavaObject(pluginName);
        if (m_PluginInstance == null)
        {
            Debug.Log("Plugin Instance Error!");
        }

        m_PluginInstance.CallStatic("receiveUnityActivity", unityActivity);
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
