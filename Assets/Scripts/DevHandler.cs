using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DevHandler : MonoBehaviour
{
    private const string DEVELOPER_MODS_PARAMETER_NAME = "developerMods";
    private const string CUSTOM_RAM_PARAMETER_NAME = "customRAMValue";
    private const string ADVANCED_DEBUGGER_PARAMETER_NAME = "advancedDebugger";
    
    [SerializeField, FormerlySerializedAs("DevToggle")]
    private Toggle _devToggle;
    [SerializeField, FormerlySerializedAs("ADBToggle")]
    private Toggle _adbToggle;
    [SerializeField, FormerlySerializedAs("RAMSetterToggle")]
    private Toggle _ramSetterToggle;
    [SerializeField, FormerlySerializedAs("RAMSetterField")]
    private GameObject _ramSetterField;

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
        _adbToggle.onValueChanged.AddListener(OnAdbToggleValueChanged);
    }
    
    private void DisposeButtonListeners()
    {
        _devToggle.onValueChanged.RemoveListener(OnDevModsToggleValueChanged);
        _ramSetterToggle.onValueChanged.RemoveListener(OnRamSetterToggleValueChanged);
        _adbToggle.onValueChanged.RemoveListener(OnAdbToggleValueChanged);
    }

    private void OnDevModsToggleValueChanged(bool isOn)
    {
        JNIStorage.apiClass.SetStatic(DEVELOPER_MODS_PARAMETER_NAME, isOn);
        JNIStorage.UpdateInstances();
    }    
    
    private void OnRamSetterToggleValueChanged(bool isOn)
    {
        JNIStorage.apiClass.SetStatic(CUSTOM_RAM_PARAMETER_NAME, isOn);
        _ramSetterField.SetActive(isOn);
    }    
    
    private void OnAdbToggleValueChanged(bool isOn)
    {
        JNIStorage.apiClass.SetStatic(ADVANCED_DEBUGGER_PARAMETER_NAME, isOn);
    }
}
