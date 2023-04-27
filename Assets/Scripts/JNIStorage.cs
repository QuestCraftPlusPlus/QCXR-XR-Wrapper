using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class JNIStorage : MonoBehaviour
{
    public static AndroidJavaClass apiClass;
    public static AndroidJavaObject accountObj;
    public static AndroidJavaObject activity;
    public static AndroidJavaObject[] instances;
    public static string home;
    public TMP_InputField RAMSetterField;

    private void Start()
    {
        OVRPlugin.systemDisplayFrequency = 90.0f;
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            Permission.RequestUserPermission(Permission.Microphone);
        apiClass = new AndroidJavaClass("pojlib.api.API_V1");
        AndroidJavaClass constants = new AndroidJavaClass("pojlib.util.Constants");
        home = constants.GetStatic<string>("MC_DIR");
        UpdateInstances();
    }

    public static void UpdateInstances()
    {
        AndroidJavaClass instance = new AndroidJavaClass("pojlib.instance.MinecraftInstance");
        instances = apiClass.CallStatic<AndroidJavaObject[]>("getQCSupportedVersions");
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
    }

    public void SetMemoryValue()
    {
        apiClass.SetStatic<string>("memoryValue", RAMSetterField.text);
    }
}
