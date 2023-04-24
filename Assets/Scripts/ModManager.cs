using System;
using System.Threading.Tasks;
using Oculus.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ModManager : MonoBehaviour
{
    [SerializeReference] public GameObject modPrefab;
    [SerializeReference] public GameObject modArray;
    [SerializeReference] public GameObject modPage;
    [SerializeReference] public APIHandler apiHandler;
    public TextMeshProUGUI modDescription;
    public TextMeshProUGUI modTitle;
    public TextMeshProUGUI modIDObject;
    public TMP_InputField searchQuery;
    public RawImage modImage;
    public GameObject modManagerMainpage;
    public GameObject modSearchMenu;
    public GameObject instanceMenu;
    public GameObject DLDImage;
    public GameObject DLImage;
    public GameObject errorMenu;

    public async void CreateMods()
    {
        ResetArray();
        SearchParser sq = apiHandler.GetSearchedMods();
        
        foreach (SearchResults searchResults in sq.hits)
        {
            async Task GetSetTexture()
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
                }
                else
                {
                    Texture modImageTexture = ((DownloadHandlerTexture)modImageLink.downloadHandler).texture;
                    GameObject modObject = Instantiate(modPrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                    modObject.GetComponentInChildren<RawImage>().texture = modImageTexture;
                    modObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = searchResults.title;
                    modObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = searchResults.description;
                    modObject.transform.SetParent(modArray.transform, false);
                    modObject.name = searchResults.project_id;
                    string currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", InstanceButton.currentVersion);
                    AndroidJavaObject instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("load", currInstName + "-fabric", JNIStorage.home);

                    // if (!JNIStorage.apiClass.CallStatic<Boolean>("hasMod", InstanceButton.GetInstance(), searchResults.title))
                    // {
                    //     modObject.GetComponentInChildren<Button>().interactable = false;
                    // }
                    // else
                    // {
                    //     modObject.GetComponentInChildren<Button>().interactable = true;
                    //     modObject.GetComponentInChildren<InteractableUnityEventWrapper>().WhenSelect.AddListener(delegate
                    //     {
                    //         RemoveMod();
                    //     });
                    // }
                    
                    modObject.GetComponent<InteractableUnityEventWrapper>().WhenSelect.AddListener(delegate
                    {
                        EventSystem.current.SetSelectedGameObject(modObject);
                        GameObject mod = GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.name);
                        apiHandler.modID = mod.ToString().Replace("(UnityEngine.GameObject)", "");
                        CreateModPage();
                    });
                }
            }

            await GetSetTexture();
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

            // if (!JNIStorage.apiClass.CallStatic<bool>("hasMod", InstanceButton.GetInstance(), mp.title))
            // {
            //     DLDImage.SetActive(false);
            //     DLImage.SetActive(true);
            // }
            // else
            // {
            //     DLImage.SetActive(false);
            //     DLDImage.SetActive(true);
            // }
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
                Debug.Log("In loop");
                if (metaInfo.game_versions.Contains(currentInstanceName))
                {
                    Debug.Log("Version found!");
                    string modName = mp.title;
                    string modUrl = file.url;
                    string modVersion = file.version;
                    string currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", InstanceButton.currentVersion);
                    Debug.Log("Line 148");
                    AndroidJavaObject instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("load", currInstName + "-fabric", JNIStorage.home);
                    Debug.Log("Line 150");
                    
                    if (instance == null)
                    {
                        Debug.Log("Line 154");
                        errorMenu.GetComponentInChildren<TextMeshProUGUI>().text = "You must run this version of the game at least once before adding mods to the instance with ModManger!";
                        errorMenu.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("Line 160");
                        JNIStorage.apiClass.CallStatic("addCustomMod", InstanceButton.GetInstance(), modName, modUrl, modVersion);
                        Debug.Log("Line 160");
                        DLImage.SetActive(false);
                        DLDImage.SetActive(true);
                    }
                    
                    return;
                }
            }
        }
    }
    
    public void RemoveMod()
    {
        string name = modIDObject.text;
        string currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", InstanceButton.currentVersion);
        AndroidJavaObject instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("load", currInstName + "-fabric", JNIStorage.home);
        JNIStorage.apiClass.CallStatic("removeMod", InstanceButton.GetInstance(), name);
        DLDImage.SetActive(false);
        DLImage.SetActive(true);
    }

    public void SearchMods()
    {
        apiHandler.searchQuery = searchQuery.text;
        CreateMods();
    }

    public void ResetArray()
    {
        int childCount = modArray.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--) {
            Transform child = modArray.transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }
}
