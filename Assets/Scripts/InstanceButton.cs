using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.Management;

public class InstanceButton : MonoBehaviour
{
    public static string currInstName;
    private bool hasDefaulted;

    public CanvasGroup ScreenFade;
    
    private void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
            return;
        currInstName = JNIStorage.instance.instancesDropdown.options[JNIStorage.instance.instancesDropdown.value].text;
    }

    private static void CreateDefaultInstance(string name)
    {
        JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, JNIStorage.instancesObj, name, true, name, null);
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
            
            Application.Unload();
            JNIStorage.apiClass.CallStatic("launchInstance", JNIStorage.activity, JNIStorage.accountObj, instance.raw);
        }
        LeanTween.value(ScreenFade.gameObject,0, 1, 1).setOnUpdate(alpha => ScreenFade.alpha = alpha).setOnComplete(() => FinishAnim());
    }
}