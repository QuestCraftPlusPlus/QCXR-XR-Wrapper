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
    public GameObject legalContent;
    public GameObject errorMenu;
    public GameObject logPanel;
    public Button legalContinue;
    public Toggle modToggle;
    public Toggle modpacksToggle;
    public Toggle resourcePacksToggle;
    public Button modsButton;
    public Button instancesButton;
    public Button playButton;
    public Button killButton;
    public Button logoutButton;
    public Button needHelpButton;
    public static int selectedInstance;
    public string pfpUrl;
    static string profileName;
    public ModManager modManager;
    public LoginHandler loginHandler;

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
        
        dropdownMain.interactable = false;
        modsButton.interactable = false;
        instancesButton.interactable = false;
        playButton.interactable = false;
    }
    
    void Update()
    {
        string time = DateTime.Now.ToString("hh:mm tt");
        minuteHourText.text = time;
        UILoginCheck();
    }

    public void UILoginCheck()
    {
        if (loginHandler.selectedAccountUsername != null && loginHandler.selectedAccountUsername != "Add Account" && !loginHandler.isDemoMode)
        {
            dropdownMain.interactable = true;
            modsButton.interactable = true;
            instancesButton.interactable = true;
            playButton.interactable = true;
        }
        else if (loginHandler.selectedAccountUsername == "Add Account")
        {
            dropdownMain.interactable = false;
            modsButton.interactable = false;
            instancesButton.interactable = false;
            playButton.interactable = false;
        }
        else if (loginHandler.selectedAccountUsername != null && loginHandler.isDemoMode)
        {
            dropdownMain.interactable = false;
            modsButton.interactable = false;
            instancesButton.interactable = false;
            playButton.interactable = true;
        }
        else
        {
            dropdownMain.interactable = false;
            modsButton.interactable = false;
            instancesButton.interactable = false;
            playButton.interactable = false;
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

    public void UpdateLegalButton()
    {
        if (legalContent.transform.localPosition.y > 6800f)
        {
            legalContinue.interactable = true;
        }
    }

    public void PlaySetter()
    {
        needHelpButton.interactable = false;
        logoutButton.interactable = false;
        dropdownMain.interactable = false;
        modsButton.interactable = false;
        instancesButton.interactable = false;
        playButton.gameObject.SetActive(false);
        killButton.gameObject.SetActive(true);
        logPanel.SetActive(true);
    }   
    
    public void KillSetter()
    {
        needHelpButton.interactable = true;
        logoutButton.interactable = true;
        dropdownMain.interactable = true;
        modsButton.interactable = true;
        instancesButton.interactable = true;
        playButton.gameObject.SetActive(true);
        killButton.gameObject.SetActive(false);
        logPanel.SetActive(false);
    }
}
