using System;
using System.Linq;
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
    [SerializeField] private TextMeshProUGUI downloadText;
    [SerializeField] private GameObject errorMenu;
    [SerializeField] private GameObject downloadButton;
	
	private string currModSlug;

    private async void CreateMods()
    {
        ResetArray();
        SearchParser searchParser = apiHandler.GetSearchedProjects();

        foreach (SearchResults searchResults in searchParser.hits)
        {
            async Task SetModImage()
            {
                UnityWebRequest modImageLink = UnityWebRequestTexture.GetTexture(searchResults.icon_url);
                modImageLink.SendWebRequest();

                //TODO: Remove artificial wait. 
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
                    bool hasMod = JNIStorage.apiClass.CallStatic<bool>("hasMod", InstanceButton.GetInstance(), searchResults.project_id);

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
                    CreateModPage(mod.ToString().Replace("(UnityEngine.GameObject)", ""));
					currModSlug = mod.ToString().Replace("(UnityEngine.GameObject)", "");
                });
            }

            await SetModImage();
        }
    }

    public async void CreateModPage(string slug)
    {
        MetaParser mp = apiHandler.GetModInfo(slug);
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

            try
            {
                bool hasMod = JNIStorage.apiClass.CallStatic<bool>("hasMod", InstanceButton.GetInstance(), mp.slug);
                downloadText.text = hasMod ? "Installed" : "Install";
                downloadButton.GetComponent<Button>().enabled = !hasMod;
            }
            catch (Exception ex)
            {
                Debug.LogError($"An error occurred: {ex}");
                downloadText.text = "Install";
            }
        }

        await GetSetTexture();
    }
    
    public void AddMod()
    {
        MetaParser mp = apiHandler.GetModInfo(currModSlug);
        MetaInfo[] modInfos = apiHandler.GetModDownloads(mp.slug);
        string currentInstanceName = InstanceButton.currInstName;

        foreach (MetaInfo metaInfo in modInfos)
        {
            foreach (var file in metaInfo.files.Where(file => IsValidModFile(file, metaInfo, currentInstanceName)))
            {
                ProcessModFile(mp, file, metaInfo, currentInstanceName);
                return;
            }
        }
    }

    private bool IsValidModFile(FileInfo file, MetaInfo metaInfo, string currentInstanceName)
    {
        return metaInfo.game_versions.Contains(currentInstanceName) 
               && metaInfo.loaders.Contains("fabric") 
               && !file.url.Contains(".mrpack");
    }

    private void ProcessModFile(MetaParser mp, FileInfo file, MetaInfo metaInfo, string currentInstanceName)
    {
        Debug.Log($"modName: {mp.title} | modUrl: {file.url} | modVersion: {currentInstanceName}");

        AndroidJavaObject instance = LoadInstance(currentInstanceName);
        if (instance == null) return;

        DownloadDependenciesAndAddMod(mp, metaInfo, file.url, currentInstanceName);
    }

    private AndroidJavaObject LoadInstance(string currentInstanceName)
    {
        string currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", InstanceButton.currentVersion);
        AndroidJavaObject instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("load", $"{currInstName}-fabric", JNIStorage.home);

        if (instance == null)
        {
            ShowError("You must run this version of the game at least once before adding mods to the instance with ModManger!");
        }

        return instance;
    }

    private void DownloadDependenciesAndAddMod(MetaParser mp, MetaInfo metaInfo, string modUrl, string currentInstanceName)
    {
        var dependencies = apiHandler.GetModDeps(mp.slug, metaInfo.id);
        if (dependencies != null)
        {
            foreach (Deps dep in dependencies)
            {
                string slug = apiHandler.GetModInfo(dep.project_id).slug;
                foreach (MetaInfo depInfo in apiHandler.GetModDownloads(dep.project_id))
                {
                    var validDepFiles = depInfo.files.Where(depFile => IsValidDependency(depFile, depInfo, currentInstanceName, slug));
                    foreach (var depFile in validDepFiles)
                    {
                        JNIStorage.apiClass.CallStatic("addCustomMod", InstanceButton.GetInstance(), slug, currentInstanceName, depFile.url);
                        Debug.Log($"Downloading Dep with file url {depFile.url}");
                        break;
                    }
                }
            }
        }

        JNIStorage.apiClass.CallStatic("addCustomMod", InstanceButton.GetInstance(), mp.slug, currentInstanceName, modUrl);
        UpdateUIAfterModAddition(mp.slug);
    }

    private bool IsValidDependency(FileInfo depFile, MetaInfo depInfo, string currentInstanceName, string slug)
    {
        return depInfo.game_versions.Contains(currentInstanceName)
               && !depFile.url.Contains(".mrpack")
               && depInfo.loaders.Contains("fabric")
               && !JNIStorage.apiClass.CallStatic<bool>("hasMod", InstanceButton.GetInstance(), slug);
    }

    private void UpdateUIAfterModAddition(string slug)
    {
        bool hasMod = JNIStorage.apiClass.CallStatic<bool>("hasMod", InstanceButton.GetInstance(), slug);
        downloadText.text = "Installed";
        downloadButton.GetComponent<Button>().enabled = !hasMod;

        if (!hasMod)
        {
            ShowError("There has been an error attempting to add this mod, maybe this mod doesn't have support? Please try again later or contact our support staff.");
        }
    }

    private void ShowError(string message)
    {
        errorMenu.GetComponentInChildren<TextMeshProUGUI>().text = message;
        errorMenu.SetActive(true);
    }
    
    private void RemoveMod(string modName)
    {
        string currInstName = JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", InstanceButton.currentVersion);
        AndroidJavaObject instance = JNIStorage.apiClass.CallStatic<AndroidJavaObject>("load", currInstName + "-fabric", JNIStorage.home);
        JNIStorage.apiClass.CallStatic<bool>("removeMod", InstanceButton.GetInstance(), modName);
        downloadText.text = "Install";
        SearchMods();
    }

    public void SearchMods()
    {
        apiHandler.searchQuery = searchQuery.text;
        CreateMods();
    }

    private void ResetArray()
    {
        for (int i = modArray.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(modArray.transform.GetChild(i).gameObject);
        }
    }
}
