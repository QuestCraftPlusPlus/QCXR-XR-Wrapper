using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InstanceManager : MonoBehaviour
{
    [SerializeField] private GameObject modArray;
    public TMP_InputField instanceName;
    public Button ModLoaderButton;
    private string currModSlug;

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
        try
        {
            string currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", currentVersion);
            AndroidJavaClass modloaderEnum = new AndroidJavaClass("pojlib.instance.MinecraftInstance$ModLoader");
            AndroidJavaObject fabric = modloaderEnum.GetStatic<AndroidJavaObject>("Fabric");

            AndroidJavaObject instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, currInstName + "-fabric", JNIStorage.home, currentVersion, fabric);

            return instance;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public AndroidJavaObject CreateCustomInstance()
    {
        try
        {
            AndroidJavaClass modloaderEnum = new AndroidJavaClass("pojlib.instance.MinecraftInstance$ModLoader");
            AndroidJavaObject instance = null;
            
            switch (ModLoaderButton.name)
            {
                case "Fabric":
                {
                    AndroidJavaObject fabric = modloaderEnum.GetStatic<AndroidJavaObject>("Fabric");
                    AndroidJavaObject currentVersion = InstanceButton.currentVersion;
                    instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, instanceName.text, JNIStorage.home, currentVersion, fabric);
                    break;
                }
                case "Quilt":
                {
                    AndroidJavaObject quilt = modloaderEnum.GetStatic<AndroidJavaObject>("Quilt");
                    AndroidJavaObject currentVersion = InstanceButton.currentVersion;
                    instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, instanceName.text, JNIStorage.home, currentVersion, quilt);
                    break;
                }
            }

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