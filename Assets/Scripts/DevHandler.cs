using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevHandler : MonoBehaviour
{
    public Toggle DevToggle;
    public Toggle ADBToggle;
    public Toggle RAMSetterToggle;
    public GameObject RAMSetterField;

    void Start()
    {
        DevToggle.onValueChanged.AddListener(delegate
        {
            DevModsToggleChanged(DevToggle);
        });
        
        RAMSetterToggle.onValueChanged.AddListener(delegate
        {
            RAMSetterToggleChanged(RAMSetterToggle);
        });
        
        ADBToggle.onValueChanged.AddListener(delegate
        {
            ADBToggleChanged(ADBToggle);
        });
    }
    
    void DevModsToggleChanged(Toggle change)
    {
        if (DevToggle.isOn)
        {
            JNIStorage.apiClass.SetStatic<bool>("developerMods", true);
        }
        else
        {
            JNIStorage.apiClass.SetStatic<bool>("developerMods", false);
        }
    }    
    
    void RAMSetterToggleChanged(Toggle change)
    {
        if (RAMSetterToggle.isOn)
        {
            JNIStorage.apiClass.SetStatic<bool>("customRAMSetter", true);
            RAMSetterField.SetActive(true);
        }
        else
        {
            JNIStorage.apiClass.SetStatic<bool>("customRAMSetter", false);
            RAMSetterField.SetActive(false);
        }
    }    
    
    void ADBToggleChanged(Toggle change)
    {
        if (ADBToggle.isOn)
        {
            JNIStorage.apiClass.SetStatic<bool>("advancedDebugger", true);
        }
        else
        {
            JNIStorage.apiClass.SetStatic<bool>("advancedDebugger", false);
        }
    }

    public void DevModsButton()
    {
        if (DevToggle.isOn)
        {
            DevToggle.isOn = false;
        }
        else
        {
            DevToggle.isOn = true;
        }
    }    
        
    public void ADBButton()
    {
        if (ADBToggle.isOn)
        {
            ADBToggle.isOn = false;
        }
        else
        {
            ADBToggle.isOn = true;
        }
    }
    
    public void RAMButton()
    {
        if (RAMSetterToggle.isOn)
        {
            RAMSetterToggle.isOn = false;
        }
        else
        {
            RAMSetterToggle.isOn = true;
        }
    }    

}
