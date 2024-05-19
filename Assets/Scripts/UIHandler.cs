using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI minuteHourText;
    public TMP_Dropdown dropdownMain;
    public TMP_Dropdown dropdownModSearch;
    public TMP_Dropdown dropdownModInfo;
    public TMP_Dropdown dropdownInstanceCreator;
    public GameObject errorMenu;
    public Toggle modToggle;
    public Toggle modpacksToggle;
    public Toggle resourcePacksToggle;
    public static int selectedInstance;
    static string pfpUrl;
    static string profileName;

    public ModManager modManager;

    void Start()
    {
        // Add listeners for toggle buttons
        modToggle.onValueChanged.AddListener((value) => OnToggleClicked(value, modToggle));
        modpacksToggle.onValueChanged.AddListener((value) => OnToggleClicked(value, modpacksToggle));
        resourcePacksToggle.onValueChanged.AddListener((value) => OnToggleClicked(value, resourcePacksToggle));
        
        dropdownMain.onValueChanged.AddListener(delegate
        {
            selectedInstance = dropdownMain.value;
            UpdateDropdowns(false, null);
        });
        dropdownInstanceCreator.onValueChanged.AddListener(delegate
        {
            selectedInstance = dropdownInstanceCreator.value;
            UpdateDropdowns(false, null);
        });
        dropdownModInfo.onValueChanged.AddListener(delegate
        {
            selectedInstance = dropdownModInfo.value;
            UpdateDropdowns(false, null);
        });
        dropdownModSearch.onValueChanged.AddListener(delegate
        {
            selectedInstance = dropdownModSearch.value;
            UpdateDropdowns(false, null);
        });

    }
    
    void Update()
    {
        string time = DateTime.Now.ToString("hh:mm tt");
        minuteHourText.text = time;
    }

    public static async Task GetTexturePlusName(RawImage pfpHolder, TextMeshProUGUI profileNameHolder)
    {
        if (JNIStorage.accountObj != null)
        {
            profileName ??= JNIStorage.apiClass.GetStatic<string>("profileName");
            profileNameHolder.text = profileName;
        }

    }

    void OnToggleClicked(bool value, Toggle clickedToggle)
    {
        if (modManager.isSearching)
        {
            clickedToggle.isOn = false;
            return;
        }
        
        if (value)
        {
            // Enable the toggles
            Toggle[] allToggles = { modToggle, modpacksToggle, resourcePacksToggle };
            foreach (Toggle toggle in allToggles)
                toggle.interactable = true;

            clickedToggle.isOn = false;
            // Disable the clicked toggle
            clickedToggle.interactable = false;
            modManager.SearchMods();
        }
    }

    public void UpdateDropdowns(bool init, List<string> list)
    {
        if (init)
        {
            dropdownMain.AddOptions(list);
            dropdownInstanceCreator.AddOptions(list);
            dropdownModInfo.AddOptions(list);
            dropdownModSearch.AddOptions(list);
        }
        else
        {
            dropdownMain.SetValueWithoutNotify(selectedInstance);
            dropdownInstanceCreator.SetValueWithoutNotify(selectedInstance);
            dropdownModInfo.SetValueWithoutNotify(selectedInstance);
            dropdownModSearch.SetValueWithoutNotify(selectedInstance);
        }
    }

    public void ClearDropdowns()
    {
        dropdownMain.ClearOptions();
        dropdownInstanceCreator.ClearOptions();
        dropdownModInfo.ClearOptions();
        dropdownModSearch.ClearOptions();
    }

    public void SetAndShowError(String errorMessage)
    {
        errorMenu.GetComponentInChildren<TextMeshProUGUI>().text = errorMessage;
        errorMenu.SetActive(true);
    }
}
