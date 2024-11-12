using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.CrashReportHandler;
using UnityEngine.EventSystems;

public class JNIStorage : MonoBehaviour
{
    public static AndroidJavaClass apiClass;
    public static AndroidJavaObject accountObj;
    public static AndroidJavaObject activity;
    public static AndroidJavaObject instancesObj;
    public APIHandler apiHandler;
    public static JNIStorage instance;
    public List<string> supportedVersions;
    public UIHandler uiHandler;
    public TMP_Dropdown instancesDropdown;
    public ConfigHandler configHandler;
    public GameObject instancePrefab;
    public GameObject instanceArray;

    private void Start()
    {
		CrashReportHandler.enableCaptureExceptions = false;
        instance = this;

        // If the user has not granted Microphone permission, don't try to start the clip.
        if (Microphone.devices.Length != 0)
        {
            Microphone.Start(Microphone.devices[0], true, 1, 44100);
        }

        apiClass = new AndroidJavaClass("pojlib.API");
        instancesObj = apiClass.CallStatic<AndroidJavaObject>("loadAll");
        configHandler.LoadConfig();
        UpdateInstances();
	    apiClass.SetStatic("model", OpenXRFeatureSystemInfo.GetHeadsetName());
    }

    private void FillSupportedVersions(string[] supportedVersionsArray)
    {
        supportedVersions.Clear();
        supportedVersions.AddRange(supportedVersionsArray);
        AndroidJavaObject[] instances = instancesObj.Call<AndroidJavaObject[]>("toArray");
        foreach (var instanceObj in instances)
        {
            string name = instanceObj.Get<string>("instanceName");
            string image = instanceObj.Get<string>("instanceImageURL");
            
            if (!supportedVersions.Contains(name))
            {
                supportedVersions.Add(name);

                /*GameObject instanceGameObject = Instantiate(instancePrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                instanceGameObject.transform.SetParent(instanceArray.transform, false);
                Debug.Log(name);
                instanceGameObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = name;
                instanceGameObject.GetComponent<Toggle>().group = instanceArray.GetComponent<ToggleGroup>();
                instanceGameObject.name = name;

                instanceGameObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate
                {
                    EventSystem.current.SetSelectedGameObject(instanceGameObject);
                    GameObject instanceObject = GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.name);
                    if (instanceObject.GetComponent<Toggle>().isOn)
                    {
                        InstanceButton.SelectInstance(instanceObject.name);
                    }
                });
                
                if (image != null)
                {
                    apiHandler.DownloadImage(image, instanceGameObject.GetComponentInChildren<RawImage>());
                }*/
            }
        }
    }

    public static PojlibInstance GetInstance(string name)
    {
        AndroidJavaObject[] instances = instancesObj.Call<AndroidJavaObject[]>("toArray");
        foreach (var instanceObj in instances)
        {
            PojlibInstance pojlibInstance = PojlibInstance.Parse(instanceObj);
            if (pojlibInstance.instanceName.Equals(name))
            {
                return pojlibInstance;
            }
        }

        return null;
    }

    public void UpdateInstances()
    {
        if (Application.platform != RuntimePlatform.Android) return;
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        uiHandler.ClearDropdowns();
        string[] supportedVersionsArray = apiClass.CallStatic<string[]>("getQCSupportedVersions");
        FillSupportedVersions(supportedVersionsArray);
        uiHandler.UpdateDropdowns(true, supportedVersions);
    }
}
