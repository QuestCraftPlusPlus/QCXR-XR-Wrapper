using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.Serialization;
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
    [SerializeField] private GameObject modSearchMenu;
    [SerializeField] private GameObject instanceMenu;
    [SerializeField] private TextMeshProUGUI downloadText;
    [SerializeField] private GameObject errorMenu;
    [SerializeField] private GameObject downloadButton;
    public Texture2D errorTexture;
    [SerializeField] private TMP_Dropdown instanceDropdown;
    [SerializeField] private TMP_Dropdown mainMenuModDropdown;
    [SerializeField] private TextMeshProUGUI instanceLabel;
    
    public GameObject modButton;
    public GameObject modPacksButton;
    public GameObject resourcePacksButton;
    
    private string currModSlug;
    private bool isSearching;
    
    private void Start()
    {
        instanceDropdown.onValueChanged.AddListener(delegate
        {
            async Task RefreshStat()
            {
                Task.Delay(100);
                InstanceButton.currInstName = instanceLabel.text;
                if (modPage.activeSelf)
                    HasModCheck(currModSlug);
            }
            RefreshStat();
        });
        
        mainMenuModDropdown.onValueChanged.AddListener(delegate { SearchMods(); });
    }
    
    public string GetFilterOption()
    {
        List<Toggle> toggleButtons = new List<Toggle>
        {
            modButton.GetComponent<Toggle>(),
            modPacksButton.GetComponent<Toggle>(),
            resourcePacksButton.GetComponent<Toggle>(),
        };

        return (from button in toggleButtons where !button.interactable select button.name).FirstOrDefault();
    }
    
    private async void CreateMods()
    {
        if (isSearching)
            return;
        isSearching = true;
        ResetArray();
        async Task GetResults()
        {
            string currInstName;
            string filterOption = GetFilterOption();

            try
            {
                currInstName = JNIStorage.GetInstance(InstanceButton.currInstName).versionName ?? JNIStorage.instance
                    .instancesDropdown.options[JNIStorage.instance.instancesDropdown.value].text;
            }
            catch (NullReferenceException)
            {
                ShowError(
                    "You must run this version of the game at least once before adding mods to the instance with Mod Manager!");
                return;
            }

            List<string> facets = new List<string>
            {
                "[\"versions:" + currInstName + "\"]",
                "[\"project_type:" + filterOption + "\"]"
            };

            if (filterOption != "datapack" && filterOption != "resourcepack")
                facets.Add("[\"categories:fabric\"]");

            string url = "https://api.modrinth.com/v2/search?query=" + searchQuery.text + "&facets=[" +
                         String.Join(", ", facets) + "]";

            UnityWebRequest queryDownload = UnityWebRequest.Get(url);
            queryDownload.SendWebRequest();

            while (!queryDownload.isDone)
                await Task.Delay(16);
            if (queryDownload.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(queryDownload.error);
                return;
            }

            SearchParser searchParser = JsonConvert.DeserializeObject<SearchParser>(queryDownload.downloadHandler.text);
            
            
            foreach (SearchResults searchResults in searchParser.hits)
            {
                GameObject modObject = Instantiate(modPrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                modObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = searchResults.title;
                modObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = searchResults.description;
                modObject.transform.SetParent(modArray.transform, false);
                modObject.name = searchResults.project_id;
                modObject.transform.GetChild(3).gameObject.SetActive(false);
                modObject.GetComponent<Button>().onClick.AddListener(delegate
                {
                    EventSystem.current.SetSelectedGameObject(modObject);
                    GameObject mod = GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.name);
                    currModSlug = mod.ToString().Replace("(UnityEngine.GameObject)", "");
                    CreateModPage(currModSlug, mod.GetComponentInChildren<RawImage>());
                });
            
                apiHandler.DownloadImage(searchResults.icon_url, modObject.GetComponentInChildren<RawImage>());

                modObject.transform.GetChild(3).gameObject.SetActive(false);
            }

            if (searchParser.hits.Count == 0)
            {
                GameObject modObject = Instantiate(modPrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                modObject.GetComponentInChildren<RawImage>().texture = errorTexture;
                modObject.GetComponentInChildren<RawImage>().color = Color.yellow;
                modObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "No mods could be found!";
                modObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Are you sure its the right name?";
                modObject.transform.GetChild(3).gameObject.SetActive(false);
                modObject.transform.SetParent(modArray.transform, false);
                modObject.name = "ERROR";
            }
        }
        await GetResults();
        isSearching = false;
    }

    async Task HasModCheck(string ModSlug)
    {
        if (JNIStorage.GetInstance(InstanceButton.currInstName) == null)
        {
            downloadText.text = "Instance Not Downloaded";
            downloadButton.GetComponent<Button>().enabled = false;
            return;
        }

        Debug.Log("Current instance: " + InstanceButton.currInstName + "\nChecking for mod " + ModSlug);
        bool hasMod = JNIStorage.apiClass.CallStatic<bool>("hasMod",
            JNIStorage.GetInstance(InstanceButton.currInstName).raw, ModSlug);
        downloadText.text = hasMod ? "Installed" : "Install";
        downloadButton.GetComponent<Button>().enabled = !hasMod;
    }

    private void CreateModPage(string slug, RawImage image)
    {
        MetaParser mp = apiHandler.GetModInfo(slug);
        instanceMenu.SetActive(false);
        modSearchMenu.SetActive(false);
        modPage.SetActive(true);
        
        modDescription.text = mp.description;
        modTitle.text = mp.title;
        modIDObject.text = mp.slug;

        
        modImage.texture = image.texture;
        
        Destroy(modImage.GetComponent<GifLoader>());
        if (image.gameObject.TryGetComponent<GifLoader>(out GifLoader loader))
        {
            GifLoader gifLoader = modImage.AddComponent<GifLoader>();
            gifLoader.LoadGifImage(loader.bytes);
        }
        
        
        currModSlug = mp.slug;
        HasModCheck(currModSlug);
    }
    
    public void AddMod()
    {
        MetaParser mp = apiHandler.GetModInfo(currModSlug);
        MetaInfo[] modInfos = apiHandler.GetModDownloads(mp.slug);
        string currentInstanceVer = JNIStorage.GetInstance(InstanceButton.currInstName).versionName;

        foreach (MetaInfo metaInfo in modInfos)
        {
            foreach (var file in metaInfo.files.Where(file => IsValidModFile(metaInfo, currentInstanceVer)))
            {
                if (file.url.Contains(".mrpack"))
                {
                    ProcessModpack(mp, file, currentInstanceVer);
                } else if (file.url.Contains(".zip"))
                {
                    //ProcessResourcePack();
                }
                else
                {
                    ProcessModFile(mp, file, metaInfo, currentInstanceVer);
                }
                
                return;
            }
        }
    }
    
    private bool IsValidModFile(MetaInfo metaInfo, string currentInstanceName)
    {
        return metaInfo.game_versions.Contains(currentInstanceName)
               && metaInfo.loaders.Contains("fabric");
    }

    private void ProcessModFile(MetaParser mp, FileInfo file, MetaInfo metaInfo, string currentInstanceVer)
    {
        AndroidJavaObject instance = LoadInstance();
        if (instance == null) return;

        DownloadDependenciesAndAddMod(mp, metaInfo, file.url, currentInstanceVer);
    }    
    
    private void ProcessResourcePack(MetaParser mp, FileInfo file, MetaInfo metaInfo, string currentInstanceVer)
    {
        Debug.Log($"modName: {mp.title} | modUrl: {file.url} | modVersion: {currentInstanceVer}");

        AndroidJavaObject instance = LoadInstance();
        if (instance == null) return;

        DownloadDependenciesAndAddMod(mp, metaInfo, file.url, currentInstanceVer);
    }
    
    private async void ProcessModpack(MetaParser mp, FileInfo file, string currentInstanceVer)
    {
        string path = Path.Combine(Application.persistentDataPath);
        path = Path.Combine(path, mp.title + ".mrpack");
        Debug.Log($"modName: {mp.title} | modUrl: {file.url} | modVersion: {currentInstanceVer} | modPatch: {path}");
        
        Task DownloadModpackFile()
        {
            UnityWebRequest modpackFile = new UnityWebRequest(file.url);
            modpackFile.method = UnityWebRequest.kHttpVerbGET;
            DownloadHandlerFile dh = new DownloadHandlerFile(path);
            dh.removeFileOnAbort = true;
            modpackFile.downloadHandler = dh;
            modpackFile.SendWebRequest();
            return Task.CompletedTask;
        }

        await DownloadModpackFile();

        AndroidJavaObject instance = LoadInstance();
        if (instance == null) return;

        JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, JNIStorage.instancesObj, mp.title, mp.icon_url, path);
        JNIStorage.instance.uiHandler.SetAndShowError(mp.title + " is now being created.");
        JNIStorage.instance.UpdateInstances();
        File.Delete(path);
    }

    private AndroidJavaObject LoadInstance()
    {
        PojlibInstance currInst = JNIStorage.GetInstance(InstanceButton.currInstName);

        if (currInst == null)
        {
            ShowError("You must run this version of the game at least once before adding mods to the instance with Mod Manager!");
            return null;
        }

        return currInst.raw;
    }

    private void DownloadDependenciesAndAddMod(MetaParser mp, MetaInfo metaInfo, string modUrl, string currentInstanceVer)
    {
        var dependencies = apiHandler.GetModDeps(mp.slug, metaInfo.id);
        if (dependencies != null)
        {
            foreach (Deps dep in dependencies)
            {
                if (!dep.dependency_type.Equals("required")) {continue;}
                string slug = apiHandler.GetModInfo(dep.project_id).slug;
                foreach (MetaInfo depInfo in apiHandler.GetModDownloads(dep.project_id))
                {
                    var validDepFiles = depInfo.files.Where(depFile => IsValidDependency(depFile, depInfo, currentInstanceVer, slug));
                    foreach (var depFile in validDepFiles)
                    {
                        PojlibInstance currInst = JNIStorage.GetInstance(InstanceButton.currInstName);
                        JNIStorage.apiClass.CallStatic("addMod", JNIStorage.instancesObj, currInst.raw, slug, currentInstanceVer, depFile.url);
                        Debug.Log($"Downloading Dep with file url {depFile.url}");
                        break;
                    }
                }
            }
        }

        PojlibInstance inst = JNIStorage.GetInstance(InstanceButton.currInstName);
        JNIStorage.apiClass.CallStatic("addMod", JNIStorage.instancesObj, inst.raw, mp.slug, currentInstanceVer, modUrl);
        UpdateUIAfterModAddition(mp.slug);
    }

    private bool IsValidDependency(FileInfo depFile, MetaInfo depInfo, string currentInstanceVer, string slug)
    {
        return depInfo.game_versions.Contains(currentInstanceVer)
               && !depFile.url.Contains(".mrpack")
               && depInfo.loaders.Contains("fabric")
               && !JNIStorage.apiClass.CallStatic<bool>("hasMod", JNIStorage.GetInstance(InstanceButton.currInstName).raw, slug);
    }

    private void UpdateUIAfterModAddition(string slug)
    {
        bool hasMod = JNIStorage.apiClass.CallStatic<bool>("hasMod", JNIStorage.GetInstance(InstanceButton.currInstName).raw, slug);
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
        PojlibInstance currInstName = JNIStorage.GetInstance(InstanceButton.currInstName);
        JNIStorage.apiClass.CallStatic<bool>("removeMod", JNIStorage.instancesObj, currInstName.raw, modName);
             downloadText.text = "Install";
             SearchMods();
         }

    public void SearchMods()
    {
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
