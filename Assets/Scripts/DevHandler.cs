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
        _devToggle.onValueChanged.AddListener(OnDevModsToggleChangedHandler);
        _ramSetterToggle.onValueChanged.AddListener(OnRamSetterToggleChangedHandler);
        _adbToggle.onValueChanged.AddListener(OnAdbToggleChangedHandler);
    }
    
    private void DisposeButtonListeners()
    {
        _devToggle.onValueChanged.RemoveListener(OnDevModsToggleChangedHandler);
        _ramSetterToggle.onValueChanged.RemoveListener(OnRamSetterToggleChangedHandler);
        _adbToggle.onValueChanged.RemoveListener(OnAdbToggleChangedHandler);
    }

    private void OnDevModsToggleChangedHandler(bool isOn)
    {
        JNIStorage.apiClass.SetStatic(DEVELOPER_MODS_PARAMETER_NAME, isOn);
        JNIStorage.UpdateInstances();
    }    
    
    private void OnRamSetterToggleChangedHandler(bool isOn)
    {
        JNIStorage.apiClass.SetStatic(CUSTOM_RAM_PARAMETER_NAME, isOn);
        _ramSetterField.SetActive(isOn);
    }    
    
    private void OnAdbToggleChangedHandler(bool isOn)
    {
        JNIStorage.apiClass.SetStatic(ADVANCED_DEBUGGER_PARAMETER_NAME, isOn);
    }
}
