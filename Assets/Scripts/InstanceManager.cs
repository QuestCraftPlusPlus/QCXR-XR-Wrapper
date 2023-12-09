using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InstanceManager : MonoBehaviour
{
    [SerializeField] private GameObject modArray;
	private string currModSlug;
    public TMP_InputField instanceName;

    public void ListInstances()
    {
        ResetArray();
    }

    public  void CreateInstance()
    {
        CreateCustomInstance();
    }

    public static AndroidJavaObject CreateDefaultInstance(AndroidJavaObject currentVersion)
    {
        string currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", currentVersion);
        AndroidJavaClass modloaderEnum = new AndroidJavaClass("pojlib.instance.MinecraftInstance$ModLoader");
        AndroidJavaObject fabric = modloaderEnum.GetStatic<AndroidJavaObject>("Fabric");

        
        AndroidJavaObject instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, currInstName + "-fabric", JNIStorage.home, currentVersion, fabric);

        return instance;
    }

    public AndroidJavaObject CreateCustomInstance()
    {
        try
        {
            AndroidJavaClass modloaderEnum = new AndroidJavaClass("pojlib.instance.MinecraftInstance$ModLoader");
            AndroidJavaObject fabric = modloaderEnum.GetStatic<AndroidJavaObject>("Fabric");
            AndroidJavaObject quilt = modloaderEnum.GetStatic<AndroidJavaObject>("Quilt");
            AndroidJavaObject forge = modloaderEnum.GetStatic<AndroidJavaObject>("Forge");
            AndroidJavaObject neoForge = modloaderEnum.GetStatic<AndroidJavaObject>("NeoForge");
            
            AndroidJavaObject currentVersion = InstanceButton.currentVersion;
            AndroidJavaObject instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, instanceName.text, JNIStorage.home, currentVersion);

            return instance;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void ResetArray()
    {
        for (int i = modArray.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(modArray.transform.GetChild(i).gameObject);
        }
    }
}