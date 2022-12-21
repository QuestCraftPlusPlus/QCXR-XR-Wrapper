using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.XR;
using UnityEngine.XR;

public class InstanceButton : MonoBehaviour
{
    public AndroidJavaObject currentInstance;
    public JNIStorage storage;
    bool hasDefaulted = false;

    public void Update()
    {
        if(storage.instance1193 != null && !hasDefaulted)
        {
            currentInstance = storage.instance1193;
            GetComponentInChildren<TextMeshProUGUI>().text = "1.19.3-fabric";
            hasDefaulted = true;
        }
    }

    public void SwitchInstance()
    {
        if(currentInstance == storage.instance1193)
        {
            currentInstance = storage.instance1182;
            GetComponentInChildren<TextMeshProUGUI>().text = "1.18.2-fabric";
        } else
        {
            currentInstance = storage.instance1193;
            GetComponentInChildren<TextMeshProUGUI>().text = "1.19.3-fabric";
        }
    }

    public void LaunchCurrentInsance()
    {
        var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
        foreach (var xrDisplay in xrDisplaySubsystems)
        {
            xrDisplay.Destroy();
        }
        Application.Unload();
        storage.apiClass.CallStatic("launchInstance", storage.activity, storage.accountObj, currentInstance);
    }
}
