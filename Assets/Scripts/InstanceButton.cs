using TMPro;
using UnityEngine;
using UnityEngine.XR.Management;

public class InstanceButton : MonoBehaviour
{
    public static AndroidJavaObject currentVersion;
    public int index;
    public static string currInstName;
    public GameObject modManagerButton;
    public GameObject mainMenuButton;
    public GameObject searchMenuButton;

    private bool hasDefaulted;

    private void Update()
    {
        if (JNIStorage.instances != null && !hasDefaulted)
        {
            currentVersion = JNIStorage.instances[0];
            currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", currentVersion);
            hasDefaulted = true;
            UpdateMenuButtons(currInstName);
        }
    }

    private void UpdateMenuButtons(string instName)
    {
        modManagerButton.GetComponentInChildren<TextMeshProUGUI>().text = instName + "-fabric";
        mainMenuButton.GetComponentInChildren<TextMeshProUGUI>().text = instName + "-fabric";
        searchMenuButton.GetComponentInChildren<TextMeshProUGUI>().text = instName + "-fabric";
    }

    public void SwitchInstance()
    {
        index = (index + 1) % JNIStorage.instances.Length;
        currentVersion = JNIStorage.instances[index];
        currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", currentVersion);
        UpdateMenuButtons(currInstName);
    }

    public static AndroidJavaObject GetInstance()
    {
        currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", currentVersion);
        return JNIStorage.apiClass.CallStatic<AndroidJavaObject>("load", currInstName + "-fabric", JNIStorage.home);
    }

    public static AndroidJavaObject CreateInstance()
    {
        currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", currentVersion);
        AndroidJavaObject instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("load", currInstName + "-fabric", JNIStorage.home);

        if (instance == null)
        {
            instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, currInstName + "-fabric", JNIStorage.home, currentVersion);
        }

        return instance;
    }

    public void LaunchCurrentInstance()
    {
        if (GetInstance() == null)
        {
            CreateInstance();
            return;
        }

        AndroidJavaObject instance = GetInstance();
        bool finishedDownloading = JNIStorage.apiClass.GetStatic<bool>("finishedDownloading");

        instance.Call("updateOrDownloadMods");
        
        if (!finishedDownloading)
        {
            return;
        }
	    XRGeneralSettings.Instance.Manager.activeLoader.Stop();
        XRGeneralSettings.Instance.Manager.activeLoader.Deinitialize();

        Application.Quit();
        JNIStorage.apiClass.CallStatic("launchInstance", JNIStorage.activity, JNIStorage.accountObj, instance);
    }
}