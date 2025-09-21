using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class InstanceButton : MonoBehaviour
{
    public static string currInstName;
    private bool hasDefaulted;
    public ConfigHandler configHandler;
    public UIHandler uiHandler;
    public CanvasGroup ScreenFade;
    bool gameReady = false;

    public void Update()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;
        currInstName = JNIStorage.instance.instancesDropdown.options[JNIStorage.instance.instancesDropdown.value].text;

        if (JNIStorage.apiClass.GetStatic<bool>("gameReady") && !gameReady) {
            gameReady = true;
            async Task FinishAnim()
            {
                await Task.Delay(200);
                XRGeneralSettings.Instance.Manager.DeinitializeLoader();
            }

            LeanTween.value(ScreenFade.gameObject,0, 1, 1).setOnUpdate(alpha => ScreenFade.alpha = alpha).setOnComplete(() => FinishAnim());
        }
    }

    public void SelectInstance()
    {
        if (Application.platform != RuntimePlatform.Android) return;
        currInstName = JNIStorage.instance.instancesDropdown.options[JNIStorage.instance.instancesDropdown.value].text;
        configHandler.SetLastSelectedInstance(JNIStorage.instance.instancesDropdown.value);
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
        uiHandler.PlaySetter();
        ProgressBarManager.started = true;
        new Thread(() =>
        {
            AndroidJNI.AttachCurrentThread();
            if (!JNIStorage.apiClass.CallStatic<bool>("prelaunch", JNIStorage.activity, JNIStorage.instancesObj,
                    instance.raw))
            {
                JNIStorage.instance.uiHandler.SetAndShowError("Prelaunch checks failed!");
            }
            else
            {
                JNIStorage.apiClass.CallStatic("launchInstance", JNIStorage.activity, JNIStorage.accountObj,
                    instance.raw);
            }
        }).Start();
    }

    public void KillInstance()
    {
        JNIStorage.apiClass.CallStatic("restartLauncher", JNIStorage.activity);
    }
}