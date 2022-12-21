using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JNIStorage : MonoBehaviour
{
    public AndroidJavaClass apiClass;
    public AndroidJavaObject accountObj;
    public AndroidJavaObject activity;
    public AndroidJavaObject instance1193;
    public AndroidJavaObject instance1182;
    public string home;

    private void Start()
    {
        apiClass = new AndroidJavaClass("pojlib.api.API_V1");
        AndroidJavaClass constants = new AndroidJavaClass("pojlib.util.Constants");
        home = constants.GetStatic<string>("MC_DIR");
        AndroidJavaClass instance = new AndroidJavaClass("pojlib.instance.MinecraftInstance");
        activity = instance.GetStatic<AndroidJavaObject>("context");
    }
}
