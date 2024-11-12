using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class ConfigHandler : MonoBehaviour
{
    public TMP_Dropdown accountsDropdown;
    public WindowHandler windowHandler;
    public DevHandler devHandler;
    public Config config;
    public static string configPath;

    public void LoadConfig()
    {
        configPath = Application.persistentDataPath + "/launcher.conf";
        
        if (File.Exists(configPath))
        { 
            string configFile = File.ReadAllText(configPath);
            config = JsonConvert.DeserializeObject<Config>(configFile);
            
            if (!config.acceptedLegal) { windowHandler.LegalSetter(); }
            if (config.setDevMods) { devHandler._devToggle.isOn = true; }
            if (config.setCustomRAM)
            {
                devHandler._ramSetterToggle.isOn = true;
                devHandler._ramSetterField.text = config.customRAMValue;
                JNIStorage.apiClass.SetStatic("memoryValue", config.customRAMValue);
            }
            
            JNIStorage.instance.instancesDropdown.value = config.lastSelectedInstance;
        }
        else
        {
            config = new Config
            {
                acceptedLegal = false,
                setDevMods = false,
                setCustomRAM = false,
                customRAMValue = "2048",
                lastSelectedInstance = 0,
                lastSelectedAccount = 0,
                accounts = new List<Accounts>()
            };

            string JSON = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(configPath, JSON);
            string configFile = File.ReadAllText(configPath);
            config = JsonConvert.DeserializeObject<Config>(configFile);
            windowHandler.LegalSetter();
        }
    }
    
    public void SetMemoryValue()
    {
        string configFile = File.ReadAllText(configPath);
        config = JsonConvert.DeserializeObject<Config>(configFile);
        string ramValue = devHandler._ramSetterField.text;
        
        if (ramValue != null)
        {
            config.customRAMValue = ramValue;
            string JSON = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(configPath, JSON);
            JNIStorage.apiClass.SetStatic("memoryValue", ramValue);
        }
    }

    public void AgreeToLegal()
    {
        string configFile = File.ReadAllText(configPath);
        config = JsonConvert.DeserializeObject<Config>(configFile);
        config.acceptedLegal = true;
        string JSON = JsonConvert.SerializeObject(config, Formatting.Indented);
        File.WriteAllText(configPath, JSON);
    }

    public class Config
    {
        public bool acceptedLegal;
        public bool setDevMods;
        public bool setCustomRAM;
        public string customRAMValue;
        public int lastSelectedInstance;
        public int lastSelectedAccount;
        public List<Accounts> accounts;
    }
    
    public class Accounts
    {
        public string username;
        public string uuid;
    }
}