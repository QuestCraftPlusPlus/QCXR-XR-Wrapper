using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class JNIStorage : MonoBehaviour
{
    public static AndroidJavaClass apiClass;
    public static AndroidJavaObject accountObj;
    public static AndroidJavaObject activity;
    public static AndroidJavaObject instancesObj;
    public static JNIStorage instance;
    public UIHandler uiHandler;
    public TMP_InputField RAMSetterField;
    public TMP_Dropdown instancesDropdown;
    [SerializeField, FormerlySerializedAs("DevToggle")]
    private Toggle _devToggle;
    public static string home;

    private void Start()
    {
        instance = this;
        
        Microphone.Start(Microphone.devices[0], true, 1, 44100);
        apiClass = new AndroidJavaClass("pojlib.api.API_V1");
        AndroidJavaClass constants = new AndroidJavaClass("pojlib.util.Constants");
        home = constants.GetStatic<string>("MC_DIR");
        instancesObj = apiClass.CallStatic<AndroidJavaObject>("loadAll", JNIStorage.home);
        apiClass.SetStatic("developerMods", _devToggle.isOn);
        UpdateInstances(true);
		apiClass.SetStatic("model", OpenXRFeatureSystemInfo.GetHeadsetName());
    }

    private static void FillSupportedVersions(List<string> supportedVersions, string[] supportedVersionsArray)
    {
        supportedVersions.AddRange(supportedVersionsArray);
        AndroidJavaObject[] instances = instancesObj.Call<AndroidJavaObject[]>("toArray");
        foreach (var instance in instances)
        {
            string name = instance.Get<string>("instanceName");
            if (!supportedVersions.Contains(name))
            {
                supportedVersions.Add(name);
            }
        }
    }

    public static PojlibInstance GetInstance(string name)
    {
        AndroidJavaObject[] instances = instancesObj.Call<AndroidJavaObject[]>("toArray");
        foreach (var instance in instances)
        {
            PojlibInstance pojlibInstance = PojlibInstance.Parse(instance);
            if (pojlibInstance.instanceName.Equals(name))
            {
                return pojlibInstance;
            }
        }

        return null;
    }

    public void UpdateInstances(bool init)
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        
        uiHandler.ClearDropdowns();
        string[] supportedVersionsArray = apiClass.CallStatic<string[]>("getQCSupportedVersions");
        List<string> supportedVersions = new List<string>();
        FillSupportedVersions(supportedVersions, supportedVersionsArray);
        uiHandler.UpdateDropdowns(init, supportedVersions);
    }

    public void SetMemoryValue()
    {
        if (RAMSetterField.text != null)
        {
            apiClass.SetStatic("memoryValue", RAMSetterField.text);
        }
    }
}