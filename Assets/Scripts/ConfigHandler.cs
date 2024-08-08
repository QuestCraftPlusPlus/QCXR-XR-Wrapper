using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ConfigHandler : MonoBehaviour
{
    public WindowHandler windowHandler;
    public DevHandler devHandler;
    public Config config;
    public static string configPath;
    
    private void Start()
    {
        configPath = Application.persistentDataPath + "/launcher.conf";
    }

    public void LoadConfig()
    {
        if (File.Exists(configPath))
        { 
            config = JsonConvert.DeserializeObject<Config>(configPath);
            
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
            config.acceptedLegal = false;
            config.setDevMods = false;
            config.setCustomRAM = false;
            config.customRAMValue = "2048";

            string JSON = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(configPath, JSON);
            config = JsonConvert.DeserializeObject<Config>(configPath);
        }
    }
    
    public void SetMemoryValue()
    {
        config = JsonConvert.DeserializeObject<Config>(configPath);
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