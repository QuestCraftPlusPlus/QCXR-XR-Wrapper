using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.IO;

public class DevHandler : MonoBehaviour
{
    [SerializeField, FormerlySerializedAs("DevToggle")]
    public Toggle _devToggle;
    [SerializeField, FormerlySerializedAs("ADBToggle")]
    private Toggle _adbToggle;
    [SerializeField, FormerlySerializedAs("RAMSetterToggle")]
    public Toggle _ramSetterToggle;
    [SerializeField, FormerlySerializedAs("RAMSetterField")]
    public TMP_InputField _ramSetterField;

    public ConfigHandler configHandler;
    public string configPath;

    private void Start()
    {
        InitializeButtonListeners();
        configPath = Application.persistentDataPath + "/launcher.conf";
    }
    
    private void OnDestroy()
    {
        DisposeButtonListeners();
    }

    private void InitializeButtonListeners()
    {
        _devToggle.onValueChanged.AddListener(OnDevModsToggleValueChanged);
        _ramSetterToggle.onValueChanged.AddListener(OnRamSetterToggleValueChanged);
    }
    
    private void DisposeButtonListeners()
    {
        _devToggle.onValueChanged.RemoveListener(OnDevModsToggleValueChanged);
        _ramSetterToggle.onValueChanged.RemoveListener(OnRamSetterToggleValueChanged);
    }

    private void OnDevModsToggleValueChanged(bool isOn)
    {
        string configFile = File.ReadAllText(configPath);
        configHandler.config = JsonConvert.DeserializeObject<ConfigHandler.Config>(configFile);
        configHandler.config.setDevMods = isOn;
        string JSON = JsonConvert.SerializeObject(configHandler.config, Formatting.Indented);
        File.WriteAllText(configPath, JSON);
        
        JNIStorage.apiClass.SetStatic("developerMods", isOn);
        JNIStorage.instance.UpdateInstances();
    }    
    
    private void OnRamSetterToggleValueChanged(bool isOn)
    {
        string configFile = File.ReadAllText(configPath);
        configHandler.config = JsonConvert.DeserializeObject<ConfigHandler.Config>(configFile);
        configHandler.config.setCustomRAM = isOn;
        string JSON = JsonConvert.SerializeObject(configHandler.config, Formatting.Indented);
        File.WriteAllText(configPath, JSON);
        
        JNIStorage.apiClass.SetStatic("customRAMValue", isOn);
        _ramSetterField.gameObject.SetActive(isOn);
    }    
}
