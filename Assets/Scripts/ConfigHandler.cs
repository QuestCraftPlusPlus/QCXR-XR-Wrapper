using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ConfigHandler : MonoBehaviour
{
    public WindowHandler windowHandler;
    public DevHandler devHandler;
    public Config config;
    public string configPath;

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
        }
        else
        {
            config = new Config
            {
                acceptedLegal = false,
                setDevMods = false,
                setCustomRAM = false,
                customRAMValue = "2048"
            };

            string JSON = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(configPath, JSON);
            string configFile = File.ReadAllText(configPath);
            config = JsonConvert.DeserializeObject<Config>(configFile);
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

    public class Config
    {
        public bool acceptedLegal;
        public bool setDevMods;
        public bool setCustomRAM;
        public string customRAMValue;
    }
}