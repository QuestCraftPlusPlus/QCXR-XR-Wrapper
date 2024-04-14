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
    public TextMeshProUGUI secondText;
    public TMP_Dropdown dropdownMain;
    public TMP_Dropdown dropdownModSearch;
    public TMP_Dropdown dropdownModInfo;
    public TMP_Dropdown dropdownInstanceCreator;
    public Toggle modToggle;
    public Toggle modpacksToggle;
    public Toggle resourcePacksToggle;
    public Toggle datapacksToggle;
    public static int selectedInstance;
    static string pfpUrl;
    static string profileName;

    void Start()
    {
        // Add listeners for toggle buttons
        modToggle.onValueChanged.AddListener((value) => OnToggleClicked(value, modToggle));
        modpacksToggle.onValueChanged.AddListener((value) => OnToggleClicked(value, modpacksToggle));
        resourcePacksToggle.onValueChanged.AddListener((value) => OnToggleClicked(value, resourcePacksToggle));
        datapacksToggle.onValueChanged.AddListener((value) => OnToggleClicked(value, datapacksToggle));
        
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
        string time = System.DateTime.Now.ToString("hh:mm tt");
        minuteHourText.text = time;
    }

    public static async Task GetTexturePlusName(RawImage pfpHolder, TextMeshProUGUI profileNameHolder)
    {
        if (JNIStorage.accountObj != null)
        {
            pfpUrl ??= JNIStorage.apiClass.GetStatic<string>("profileImage");
            profileName ??= JNIStorage.apiClass.GetStatic<string>("profileName");

            if (pfpHolder.texture == null)
            {
                using UnityWebRequest pfp = UnityWebRequestTexture.GetTexture(pfpUrl);
                pfp.SetRequestHeader("User-Agent", "QuestCraft v5");
                var requestTask = pfp.SendWebRequest();
                Debug.Log("Making URL request for PFP...");

                while (!requestTask.isDone)
                {
                    await Task.Yield();
                }
                
                if (pfp.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(pfp.error);
                }
                else
                {
                    // Get downloaded texture
                    var pfpTexture = DownloadHandlerTexture.GetContent(pfp);
                    pfpHolder.texture = pfpTexture;
                    profileNameHolder.text = profileName;
                }
            }
        }
    }

    void OnToggleClicked(bool value, Toggle clickedToggle)
    {
        if (value)
        {
            // Disable the clicked toggle
            clickedToggle.interactable = false;

            // Enable the rest of the toggles
            Toggle[] allToggles = { modToggle, modpacksToggle, resourcePacksToggle, datapacksToggle};

            foreach (Toggle toggle in allToggles)
            {
                if (toggle != clickedToggle)
                {
                    toggle.interactable = true;
                }
            }
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
}
