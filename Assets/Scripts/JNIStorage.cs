using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class JNIStorage : MonoBehaviour
{
    public static AndroidJavaClass apiClass;
    public static AndroidJavaObject accountObj;
    public static AndroidJavaObject activity;
    public static AndroidJavaObject instanceList;
    public static List<PojlibInstance> instances;
    public TMP_InputField RAMSetterField;
    public static TMP_Dropdown instancesDropdown;
    [SerializeField, FormerlySerializedAs("DevToggle")]
    private Toggle _devToggle;
    public static string home;

    private void Start()
    {
        Microphone.Start(Microphone.devices[0], true, 1, 44100);
        apiClass = new AndroidJavaClass("pojlib.api.API_V1");
        AndroidJavaClass constants = new AndroidJavaClass("pojlib.util.Constants");
        home = constants.GetStatic<string>("MC_DIR");
        apiClass.SetStatic("developerMods", _devToggle.isOn);
        UpdateInstances();
		apiClass.SetStatic("model", OpenXRFeatureSystemInfo.GetHeadsetName());
    }

    public static void UpdateInstances()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        
        instanceList = apiClass.CallStatic<AndroidJavaObject>("loadAll");
        AndroidJavaObject[] availableInstances = instanceList.Call<AndroidJavaObject[]>("toArray");
        instances = new List<PojlibInstance>();
        
        foreach (AndroidJavaObject instance in availableInstances)
        {
            instances.Add(PojlibInstance.parse(instance));
        }

        List<string> instanceNames = new List<string>();
        foreach (PojlibInstance instance in instances)
        {
            instanceNames.Add(instance.instanceName);
        }
        
        instancesDropdown.AddOptions(instanceNames);
    }

    public void SetMemoryValue()
    {
        if (RAMSetterField.text != null)
        {
            apiClass.SetStatic("memoryValue", RAMSetterField.text);
        }
    }
}