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

    public void Update()
    {
        if(JNIStorage.instances != null && !hasDefaulted)
        {
            currentVersion = JNIStorage.instances[0];
            string currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", currentVersion);
            GetComponentInChildren<TextMeshProUGUI>().text = currInstName + "-fabric";
            hasDefaulted = true;
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
        string currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", currentVersion);
        GetComponentInChildren<TextMeshProUGUI>().text = currInstName + "-fabric";
    }

    public static AndroidJavaObject CreateOrGetInstance()
    {
        string currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", currentVersion);
        AndroidJavaObject instance;
        instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("load", currInstName + "-fabric", JNIStorage.home);
        if(instance == null)
        {
            instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, currInstName + "-fabric", JNIStorage.home, currentVersion);
        }

        return instance;
    }

    public void LaunchCurrentInsance()
    {
        AndroidJavaObject instance = CreateOrGetInstance();
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
