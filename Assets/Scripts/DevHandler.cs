using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.IO;

public class DevHandler : MonoBehaviour
{
    private const string DEVELOPER_MODS_PARAMETER_NAME = "developerMods";
    private const string CUSTOM_RAM_PARAMETER_NAME = "customRAMValue";
    
    [SerializeField, FormerlySerializedAs("DevToggle")]
    public Toggle _devToggle;
    [SerializeField, FormerlySerializedAs("ADBToggle")]
    private Toggle _adbToggle;
    [SerializeField, FormerlySerializedAs("RAMSetterToggle")]
    public Toggle _ramSetterToggle;
    [SerializeField, FormerlySerializedAs("RAMSetterField")]
    public TMP_InputField _ramSetterField;

    public ConfigHandler configHandler;

    private void Start()
    {
        InitializeButtonListeners();
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
        configHandler.config = JsonConvert.DeserializeObject<ConfigHandler.Config>(ConfigHandler.configPath);

        JNIStorage.apiClass.SetStatic(DEVELOPER_MODS_PARAMETER_NAME, isOn);
        JNIStorage.instance.UpdateInstances();
        configHandler.config.setDevMods = isOn;
        string JSON = JsonConvert.SerializeObject(configHandler.config, Formatting.Indented);
        File.WriteAllText(ConfigHandler.configPath, JSON);
    }    
    
    private void OnRamSetterToggleValueChanged(bool isOn)
    {
        configHandler.config = JsonConvert.DeserializeObject<ConfigHandler.Config>(ConfigHandler.configPath);
        
        JNIStorage.apiClass.SetStatic(CUSTOM_RAM_PARAMETER_NAME, isOn);
        _ramSetterField.gameObject.SetActive(isOn);
        configHandler.config.setCustomRAM = isOn;
        string JSON = JsonConvert.SerializeObject(configHandler.config, Formatting.Indented);
        File.WriteAllText(ConfigHandler.configPath, JSON);
    }    
}
