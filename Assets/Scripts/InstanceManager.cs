using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstanceManager : MonoBehaviour
{
    [SerializeField] private GameObject modArray;
    public TMP_InputField instanceName;
    public Toggle defaultModsToggle;
    public TMP_Dropdown versionDropdown; 
    private string currModSlug;

    public void ListInstances()
    {
        ResetArray();
    }
    
    public void CreateCustomInstance()
    {
        try
        {
            JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, instanceName.text, JNIStorage.home, 
                defaultModsToggle.isOn, versionDropdown.options[versionDropdown.value].text, instanceName.text);
        }
        catch (Exception e)
        {
            Debug.Log(e);
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