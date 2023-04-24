using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.XR;
using UnityEngine.XR;

public class InstanceButton : MonoBehaviour
{
    public static AndroidJavaObject currentVersion;
    bool hasDefaulted = false;
    public AudioSource source;
    public int index = 0;
    public static string currInstName;
    public GameObject modManagerButton;
    public GameObject mainMenuButton;

    public void Update()
    {
        if(JNIStorage.instances != null && !hasDefaulted)
        {
            currentVersion = JNIStorage.instances[0];
            string currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", currentVersion);
            hasDefaulted = true;
            
            if (modManagerButton.activeSelf)
            {
                modManagerButton.GetComponentInChildren<TextMeshProUGUI>().text = currInstName + "-fabric";
                mainMenuButton.GetComponentInChildren<TextMeshProUGUI>().text = currInstName + "-fabric";
            }
            else if (mainMenuButton.activeSelf)
            {
                mainMenuButton.GetComponentInChildren<TextMeshProUGUI>().text = currInstName + "-fabric";
                modManagerButton.GetComponentInChildren<TextMeshProUGUI>().text = currInstName + "-fabric";
            }
        }
    }

    public void SwitchInstance()
    {
        index++;
        if (index > JNIStorage.instances.Length - 1)
        {
            index = 0;
        }

        currentVersion = JNIStorage.instances[index];
        currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", currentVersion);
        modManagerButton.GetComponentInChildren<TextMeshProUGUI>().text = currInstName + "-fabric";
        mainMenuButton.GetComponentInChildren<TextMeshProUGUI>().text = currInstName + "-fabric";

    }

    public static AndroidJavaObject GetInstance()
    {
        currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", currentVersion);
        AndroidJavaObject instance;
        instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("load", currInstName + "-fabric", JNIStorage.home);
        return instance;
    }

    public static AndroidJavaObject CreateInstance()
    {
        currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", currentVersion);
        AndroidJavaObject instance;
        instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("load", currInstName + "-fabric", JNIStorage.home);
        
        if(instance == null)
        {
            instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, currInstName + "-fabric", JNIStorage.home, currentVersion);
        }
        
        return instance;
    }

    public void LaunchCurrentInstance()
    {
        AndroidJavaObject instance = CreateInstance();
        string currentFile = JNIStorage.apiClass.GetStatic<string>("currentDownload");
        if(currentFile != null)
        {
            return;
        }

        source.Stop();
        var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
        foreach (var xrDisplay in xrDisplaySubsystems)
        {
            xrDisplay.Destroy();
        }
        Application.Unload();
        JNIStorage.apiClass.CallStatic("launchInstance", JNIStorage.activity, JNIStorage.accountObj, instance);
    }
}
