using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class JNIStorage : MonoBehaviour
{
    public static AndroidJavaClass apiClass;
    public static AndroidJavaObject accountObj;
    public static AndroidJavaObject activity;
    public static AndroidJavaObject[] instances;
    public static string home;
    
    private void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            Permission.RequestUserPermission(Permission.Microphone);
        apiClass = new AndroidJavaClass("pojlib.api.API_V1");
        AndroidJavaClass constants = new AndroidJavaClass("pojlib.util.Constants");
        home = constants.GetStatic<string>("MC_DIR");
        AndroidJavaClass instance = new AndroidJavaClass("pojlib.instance.MinecraftInstance");
        activity = instance.GetStatic<AndroidJavaObject>("context");
        instances = apiClass.CallStatic<AndroidJavaObject[]>("getQCSupportedVersions");
    }
}
