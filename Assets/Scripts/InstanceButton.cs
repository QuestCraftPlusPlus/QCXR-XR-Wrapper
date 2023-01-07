using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class InstanceButton : MonoBehaviour
{
    public AndroidJavaObject currentInstance;
    bool hasDefaulted = false;
    public AudioSource source;

    public void Update()
    {
        if(JNIStorage.instance1193 != null && !hasDefaulted)
        {
            currentInstance = JNIStorage.instance1193;
            GetComponentInChildren<TextMeshProUGUI>().text = "1.19.3-fabric";
            hasDefaulted = true;
        }
    }

    public void SwitchInstance()
    {
        if(currentInstance == JNIStorage.instance1193)
        {
            currentInstance = JNIStorage.instance1182;
            GetComponentInChildren<TextMeshProUGUI>().text = "1.18.2-fabric";
        } else
        {
            currentInstance = JNIStorage.instance1193;
            GetComponentInChildren<TextMeshProUGUI>().text = "1.19.3-fabric";
        }
    }

    public void LaunchCurrentInsance()
    {
        source.Stop();
        var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
        foreach (var xrDisplay in xrDisplaySubsystems)
        {
            xrDisplay.Destroy();
        }
        Application.Unload();
        JNIStorage.apiClass.CallStatic("launchInstance", JNIStorage.activity, JNIStorage.accountObj, currentInstance);
    }
}
