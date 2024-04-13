using TMPro;
using UnityEngine;
using UnityEngine.XR.Management;

public class InstanceButton : MonoBehaviour
{
    public int index;
    public static string currInstName;
    public GameObject modManagerButton;
    public GameObject mainMenuButton;
    public GameObject searchMenuButton;
    public GameObject instanceCreatorButton;

    private bool hasDefaulted;

    private void Update()
    {
        if (!hasDefaulted)
        {
            currInstName = JNIStorage.instance.instancesDropdown.itemText.text;
            hasDefaulted = true;
            UpdateMenuButtons(currInstName);
        }
    }

    private void UpdateMenuButtons(string instName)
    {
        modManagerButton.GetComponentInChildren<TextMeshProUGUI>().text = instName;
        mainMenuButton.GetComponentInChildren<TextMeshProUGUI>().text = instName;
        searchMenuButton.GetComponentInChildren<TextMeshProUGUI>().text = instName;
        instanceCreatorButton.GetComponentInChildren<TextMeshProUGUI>().text = instName;
    }
    private static void CreateDefaultInstance(string name)
    {
        JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, name, JNIStorage.home, true, name, null);
    }

    public static void LaunchCurrentInstance()
    {
        if (JNIStorage.GetInstance(currInstName) == null)
        {
            CreateDefaultInstance(currInstName);
            return;
        }

        PojlibInstance instance = JNIStorage.GetInstance(currInstName);
        bool finishedDownloading = JNIStorage.apiClass.GetStatic<bool>("finishedDownloading");
        instance.raw.Call("updateMods", JNIStorage.home);
        
        if (!finishedDownloading) { return; }
	    XRGeneralSettings.Instance.Manager.activeLoader.Stop();
        XRGeneralSettings.Instance.Manager.activeLoader.Deinitialize();

        Application.Quit();
        JNIStorage.apiClass.CallStatic("launchInstance", JNIStorage.activity, JNIStorage.accountObj, instance);
    }
}