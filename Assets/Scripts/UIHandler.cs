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
    static string pfpUrl;
    static string profileName;

    void Start()
    {
        // Add listeners for toggle buttons
        modToggle.onValueChanged.AddListener((value) => OnToggleClicked(value, modToggle));
        modpacksToggle.onValueChanged.AddListener((value) => OnToggleClicked(value, modpacksToggle));
        resourcePacksToggle.onValueChanged.AddListener((value) => OnToggleClicked(value, resourcePacksToggle));
    }
    
    void Update()
    {
        if (System.DateTime.Now.Minute.ToString().Length == 1)
        {
            string hour = System.DateTime.Now.Hour.ToString();
            string minute = System.DateTime.Now.Minute.ToString().Insert(0, "0");
            minuteHourText.text = hour + ":" + minute;
        }
        else
        {
            string hour = System.DateTime.Now.Hour.ToString();
            string minute = System.DateTime.Now.Minute.ToString();
            minuteHourText.text = hour + ":" + minute;
        }
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
            Toggle[] allToggles = { modToggle, modpacksToggle, resourcePacksToggle };

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
