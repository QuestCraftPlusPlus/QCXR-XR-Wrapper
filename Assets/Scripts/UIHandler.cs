using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI minuteHourText;
    public TextMeshProUGUI secondText;
    public Toggle modToggle;
    public Toggle modpacksToggle;
    public Toggle resourcePacksToggle;
    public Toggle datapacksToggle;
    static string pfpUrl;
    static string profileName;

    void Start()
    {
        // Add listeners for toggle buttons
        modToggle.onValueChanged.AddListener((value) => OnToggleClicked(value, modToggle));
        modpacksToggle.onValueChanged.AddListener((value) => OnToggleClicked(value, modpacksToggle));
        resourcePacksToggle.onValueChanged.AddListener((value) => OnToggleClicked(value, resourcePacksToggle));
        datapacksToggle.onValueChanged.AddListener((value) => OnToggleClicked(value, datapacksToggle));

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
}
