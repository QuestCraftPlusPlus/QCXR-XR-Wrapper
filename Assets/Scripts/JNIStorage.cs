using System.Collections;
using System.Collections.Generic;
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
    public GameObject RAMSetterField;

    private void Start()
    {
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
        activity = instance.GetStatic<AndroidJavaObject>("context");
    }

    public void SetMemoryValue()
    {
        apiClass.SetStatic<int>("memoryValue", int.Parse(RAMSetterField.GetComponent<InputField>().text));
    }
}
