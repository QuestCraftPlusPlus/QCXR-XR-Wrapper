using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ModManager : MonoBehaviour
{
    [SerializeField] private GameObject modPrefab;
    [SerializeField] private GameObject modArray;
    [SerializeField] private GameObject modPage;
    [SerializeField] private APIHandler apiHandler;
    [SerializeField] private TextMeshProUGUI modDescription;
    [SerializeField] private TextMeshProUGUI modTitle;
    [SerializeField] private TextMeshProUGUI modIDObject;
    [SerializeField] private TMP_InputField searchQuery;
    [SerializeField] private RawImage modImage;
    [SerializeField] private GameObject modManagerMainpage;
    [SerializeField] private GameObject modSearchMenu;
    [SerializeField] private GameObject instanceMenu;
    [SerializeField] private GameObject DLDImage;
    [SerializeField] private GameObject DLImage;
    [SerializeField] private GameObject errorMenu;
    [SerializeField] private GameObject downloadButton;

    public async void CreateMods()
    {
        ResetArray();
        SearchParser searchParser = apiHandler.GetSearchedMods();

        foreach (SearchResults searchResults in searchParser.hits)
        {
            async Task SetModImage()
            {
                UnityWebRequest modImageLink = UnityWebRequestTexture.GetTexture(searchResults.icon_url);
                modImageLink.SendWebRequest();

                while (!modImageLink.isDone)
                {
                    await Task.Delay(50);
                }

                if (modImageLink.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(modImageLink.error);
                    return;
                }

                Texture modImageTexture = ((DownloadHandlerTexture)modImageLink.downloadHandler).texture;

                GameObject modObject = Instantiate(modPrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                modObject.GetComponentInChildren<RawImage>().texture = modImageTexture;
                modObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = searchResults.title;
                modObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = searchResults.description;
                modObject.transform.SetParent(modArray.transform, false);
                modObject.name = searchResults.project_id;

                string currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", InstanceButton.currentVersion);
                AndroidJavaObject instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("load", currInstName + "-fabric", JNIStorage.home);

                try
                {
                    bool hasMod = JNIStorage.apiClass.CallStatic<Boolean>("hasMod", InstanceButton.GetInstance(), searchResults.title);

                    if (!hasMod)
                    {
                        modObject.transform.GetChild(3).gameObject.SetActive(false);
                    }
                    else 
                    {
                        modObject.transform.GetChild(3).gameObject.SetActive(true);
                        modObject.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { RemoveMod(searchResults.title); });
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"An error occurred: {ex}");
                    
                    if (modObject != null)
                    {
                        modObject.transform.GetChild(3).gameObject.SetActive(false);
                    }
                }

                modObject.GetComponent<Button>().onClick.AddListener(delegate
                {
                    EventSystem.current.SetSelectedGameObject(modObject);
                    GameObject mod = GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.name);
                    apiHandler.modID = mod.ToString().Replace("(UnityEngine.GameObject)", "");
                    CreateModPage();
                });
            }

            await SetModImage();
        }
    }

    public async void CreateModPage()
    {
        MetaParser mp = apiHandler.GetModInfo();
        instanceMenu.SetActive(false);
        modSearchMenu.SetActive(false);
        modManagerMainpage.SetActive(false);
        modPage.SetActive(true);

        async Task GetSetTexture()
        {
            UnityWebRequest modImageLink = UnityWebRequestTexture.GetTexture(mp.icon_url);
            modImageLink.SendWebRequest();

            while (!modImageLink.isDone)
            {
                await Task.Delay(50);
            }

            Texture modImageTexture = ((DownloadHandlerTexture)modImageLink.downloadHandler).texture;
            modDescription.text = mp.description;
            modTitle.text = mp.title;
            modImage.texture = modImageTexture;
            modIDObject.text = mp.slug;
            string currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", InstanceButton.currentVersion);
            AndroidJavaObject instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("load", currInstName + "-fabric", JNIStorage.home);

            try
            {
                bool hasMod = JNIStorage.apiClass.CallStatic<bool>("hasMod", instance, mp.title);
                DLDImage.SetActive(hasMod);
                DLImage.SetActive(!hasMod);
                downloadButton.GetComponent<Button>().enabled = !hasMod;
            }
            catch (Exception ex)
            {
                Debug.LogError($"An error occurred: {ex}");
                DLDImage.SetActive(false);
                DLImage.SetActive(true);
            }
        }

        await GetSetTexture();
    }
    
    public void AddMod()
    {
        apiHandler.modID = modIDObject.text;
        MetaParser mp = apiHandler.GetModInfo();
        MetaInfo[] mi = apiHandler.GetModDownloads();

        string currentInstanceName = InstanceButton.currInstName;
        foreach (MetaInfo metaInfo in mi)
        {
            foreach (FileInfo file in metaInfo.files)
            {
                if (metaInfo.game_versions.Contains(currentInstanceName) && metaInfo.loaders.Contains("fabric") && !file.url.Contains(".mrpack"))
                {
                    string modName = mp.title;
                    string modUrl = file.url;
                    string modVersion = currentInstanceName;
                    Debug.Log($"modName: {modName} | modUrl: {modUrl} | modVersion: {modVersion}");

                    string currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", InstanceButton.currentVersion);
                    AndroidJavaObject instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("load", $"{currInstName}-fabric", JNIStorage.home);

                    if (instance == null)
                    {
                        errorMenu.GetComponentInChildren<TextMeshProUGUI>().text = "You must run this version of the game at least once before adding mods to the instance with ModManger!";
                        errorMenu.SetActive(true);
                    }
                    else
                    {
                        JNIStorage.apiClass.CallStatic("addCustomMod", InstanceButton.GetInstance(), modName, modVersion, modUrl);
                        DLImage.SetActive(false);
                        DLDImage.SetActive(true);
                    }

                    return;
                }
            }
            
            bool hasMod = JNIStorage.apiClass.CallStatic<bool>("hasMod", InstanceButton.GetInstance(), mp.title);
            if (!hasMod)
            {
                errorMenu.GetComponentInChildren<TextMeshProUGUI>().text = "There has been an error attempting to add this mod, maybe this mod doesn't have " + currentInstanceName + " support? Please try again later or contact our support staff at https://discord.gg/QuestCraft.";
                errorMenu.SetActive(true);
            }
        }
    }
    
    public void RemoveMod(string name)
    {
        string currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", InstanceButton.currentVersion);
        AndroidJavaObject instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("load", currInstName + "-fabric", JNIStorage.home);
        JNIStorage.apiClass.CallStatic<bool>("removeMod", InstanceButton.GetInstance(), name);
        DLDImage.SetActive(false);
        DLImage.SetActive(true);
        SearchMods();
    }

    public void SearchMods()
    {
        apiHandler.searchQuery = searchQuery.text;
        CreateMods();
    }

    public void ResetArray()
    {
        for (int i = modArray.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(modArray.transform.GetChild(i).gameObject);
        }
    }
}
