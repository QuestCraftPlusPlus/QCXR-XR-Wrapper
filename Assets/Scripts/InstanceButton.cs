using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.XR.Management;

public class InstanceButton : MonoBehaviour
{
    public static string currInstName;
    private bool hasDefaulted;
    public ConfigHandler.Config config;
    public string configPath;

    public CanvasGroup ScreenFade;
    
    public void Update()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;
        currInstName = JNIStorage.instance.instancesDropdown.options[JNIStorage.instance.instancesDropdown.value].text;
    }

    public void SelectInstance()
    {
        if (Application.platform != RuntimePlatform.Android) return;
        currInstName = JNIStorage.instance.instancesDropdown.options[JNIStorage.instance.instancesDropdown.value].text;
        configPath = Application.persistentDataPath + "/launcher.conf";
        string configFile = File.ReadAllText(configPath);
        config = JsonConvert.DeserializeObject<ConfigHandler.Config>(configFile);
        config.lastSelectedInstance = JNIStorage.instance.instancesDropdown.value;
        string JSON = JsonConvert.SerializeObject(config, Formatting.Indented);
        File.WriteAllText(configPath, JSON);
    }

    private static void CreateDefaultInstance(string name)
    {
        JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, JNIStorage.instancesObj, name, true, name, "Fabric", null);
        JNIStorage.instance.uiHandler.SetAndShowError(currInstName + " is now installing.");
        JNIStorage.instance.UpdateInstances();
    }

    public void LaunchCurrentInstance()
    {
        if (JNIStorage.GetInstance(currInstName) == null)
        {
            Debug.Log("Instance is null!");
            CreateDefaultInstance(currInstName);
            return;
        }

        PojlibInstance instance = JNIStorage.GetInstance(currInstName);
        bool finishedDownloading = JNIStorage.apiClass.GetStatic<bool>("finishedDownloading");

        instance.raw.Call("updateMods", JNIStorage.instancesObj);
        
        if (!finishedDownloading)
        { 
            JNIStorage.instance.uiHandler.SetAndShowError(currInstName + " is still installing, please wait until the install has finished.");
            return; 
        }
        
        async Task FinishAnim()
        {
            await Task.Delay(200);
            XRGeneralSettings.Instance.Manager.activeLoader.Stop();
            XRGeneralSettings.Instance.Manager.activeLoader.Deinitialize();
            
            Application.Quit();
            JNIStorage.apiClass.CallStatic("launchInstance", JNIStorage.activity, JNIStorage.accountObj, instance.raw);
        }
        LeanTween.value(ScreenFade.gameObject,0, 1, 1).setOnUpdate(alpha => ScreenFade.alpha = alpha).setOnComplete(() => FinishAnim());
    }
}