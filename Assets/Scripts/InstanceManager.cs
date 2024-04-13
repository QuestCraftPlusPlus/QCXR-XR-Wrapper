using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InstanceManager : MonoBehaviour
{
    [SerializeField] private GameObject modArray;
    public TMP_InputField instanceName;
    public TMP_Text modLoaderButton;
    private string currModSlug;

    public void ListInstances()
    {
        ResetArray();
    }
    
    public void ModLoaderSwitch()
    {
        switch (modLoaderButton.text)
        {
            case "Fabric":
                modLoaderButton.text = "Quilt";
                break;
            case "Quilt":
                modLoaderButton.text = "Fabric";
                break;
        }
    } 

    /* TODO: Refactor
    public AndroidJavaObject CreateCustomInstance()
    {
        try
        {
            AndroidJavaClass modloaderEnum = new AndroidJavaClass("pojlib.instance.InstanceHandler$ModLoader");
            AndroidJavaObject instance = null;

            switch (modLoaderButton.text)
            {
                case "Fabric":
                {
                    AndroidJavaObject fabric = modloaderEnum.GetStatic<AndroidJavaObject>("Fabric");
                    AndroidJavaObject currentVersion = InstanceButton.currentVersion;
                    instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, instanceName.text, JNIStorage.home, currentVersion, fabric);
                    Debug.Log("Creating " + currentVersion + " Fabric instance with name " + instanceName.name);
                    break;
                }
                case "Quilt":
                {
                    AndroidJavaObject quilt = modloaderEnum.GetStatic<AndroidJavaObject>("Quilt");
                    AndroidJavaObject currentVersion = InstanceButton.;
                    instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, instanceName.text, JNIStorage.home, currentVersion, quilt);
                    Debug.Log("Creating " + currentVersion + " Quilt instance with name " + instanceName.name);
                    break;
                }
            }

            return instance;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }
    }
    */

    private void ResetArray()
    {
        for (int i = modArray.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(modArray.transform.GetChild(i).gameObject);
        }
    }
}