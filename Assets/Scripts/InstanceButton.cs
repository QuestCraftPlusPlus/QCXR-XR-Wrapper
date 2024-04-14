using TMPro;
using UnityEngine;
using UnityEngine.XR.Management;

public class InstanceButton : MonoBehaviour
{
    public int index;
    public static string currInstName;

    private bool hasDefaulted;

    private void Update()
    {
        Debug.Log(JNIStorage.instance.instancesDropdown.value);
        currInstName = JNIStorage.instance.instancesDropdown.options[JNIStorage.instance.instancesDropdown.value].text;
    }

    private static void CreateDefaultInstance(string name)
    {
        JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, name, JNIStorage.home, true, name, null);
        JNIStorage.instance.UpdateInstances(false);
    }

    public static void LaunchCurrentInstance()
    {
        if (JNIStorage.GetInstance(currInstName) == null)
        {
            Debug.Log("Instance is null!");
            CreateDefaultInstance(currInstName);
            return;
        }

        PojlibInstance instance = JNIStorage.GetInstance(currInstName);
        bool finishedDownloading = JNIStorage.apiClass.GetStatic<bool>("finishedDownloading");
        instance.raw.Call("updateMods", JNIStorage.home, JNIStorage.instancesObj);
        
        if (!finishedDownloading) { return; }
	    XRGeneralSettings.Instance.Manager.activeLoader.Stop();
        XRGeneralSettings.Instance.Manager.activeLoader.Deinitialize();

        Application.Quit();
        JNIStorage.apiClass.CallStatic("launchInstance", JNIStorage.activity, JNIStorage.accountObj, instance.raw);
    }
}