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
using UnityEngine.UI;

public class ModManager : MonoBehaviour
{
    [SerializeField] private GameObject modPrefab;
    [SerializeField] private GameObject modPagePrefab;
    [SerializeField] private GameObject modArray;
    [SerializeField] private GameObject modInfoArray;
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

    public TextMeshProUGUI statusText;

    public int page;
    private string currModSlug;
    private string[] currModVersions;
    public bool isSearching;
    
    private void Start()
    {
        instanceDropdown.onValueChanged.AddListener(delegate
        {
            async Task RefreshStat()
            {
                Task.Delay(100);
                InstanceButton.currInstName = instanceLabel.text;
                if (modPage.activeSelf)
                    HasModCheck(currModSlug, currModVersions);
            }
            RefreshStat();
        });
        
        mainMenuModDropdown.onValueChanged.AddListener(delegate { SearchMods(); });

        if (statusText == null)
            statusText = new GameObject().AddComponent<TextMeshProUGUI>();
        statusText.text = "";
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
        statusText.text = "Searching..";
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
                ShowError("You must run this version of the game at least once before adding mods to the instance with Mod Manager!");
                return;
            }

            List<string> facets = new List<string>
            {
                "[\"versions:" + currInstName + "\"]",
                "[\"project_type:" + filterOption + "\"]"
            };

            if (filterOption != "datapack" && filterOption != "resourcepack")
                facets.Add("[\"categories:fabric\"]");

            //https://docs.modrinth.com/#tag/projects/operation/searchProjects
            
            string url = "https://api.modrinth.com/v2/search" +
                         $"?query={searchQuery.text}" +
                         $"&facets=[{String.Join(", ", facets)}]" +
                         $"&offset={page*20}" +
                         "&limit=20";

            UnityWebRequest queryDownload = UnityWebRequest.Get(url);
            queryDownload.SetRequestHeader("User-Agent", "QuestCraftPlusPlus/QuestCraft/" + Application.version + " (discord.gg/questcraft)");
            queryDownload.SendWebRequest();

            while (!queryDownload.isDone)
                await Task.Delay(16);
            if (queryDownload.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(queryDownload.error);
                statusText.text = "<color=red>An error occured";
                return;
            }

            SearchParser searchParser = JsonConvert.DeserializeObject<SearchParser>(queryDownload.downloadHandler.text);
            statusText.text = $"Found {searchParser.total_hits.ToString()} Results";
            
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
                return;
            } 
            
            foreach (SearchResults searchResults in searchParser.hits)
            {
                GameObject modObject = Instantiate(modPrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                modObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = searchResults.title + " <size=75%>by " + searchResults.author;
                modObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = searchResults.description;
                modObject.transform.SetParent(modArray.transform, false);
                modObject.name = searchResults.project_id;
                modObject.transform.GetChild(3).gameObject.SetActive(false);
                modObject.GetComponent<Button>().onClick.AddListener(delegate
                {
                    EventSystem.current.SetSelectedGameObject(modObject);
                    GameObject mod = GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.name);
                    currModSlug = mod.ToString().Replace("(UnityEngine.GameObject)", "");
                    CreateModPage(currModSlug, mod.GetComponentInChildren<RawImage>(), searchResults.author);
                });
            
                apiHandler.DownloadImage(searchResults.icon_url, modObject.GetComponentInChildren<RawImage>());

                modObject.transform.GetChild(3).gameObject.SetActive(false);
            }
            
            if (searchParser.total_hits > 20)
            {
                GameObject modPages = Instantiate(modPagePrefab, modArray.transform, false);
                modPages.name = "ModPages";
                
                //updating visibility
                if (page == 0)
                    modPages.transform.GetChild(0).gameObject.SetActive(false);
                if (page-2 < 0)
                    modPages.transform.GetChild(1).gameObject.SetActive(false);
                if (page-1 < 0)
                    modPages.transform.GetChild(2).gameObject.SetActive(false);
                if (page+1 < 0)
                    modPages.transform.GetChild(4).gameObject.SetActive(false);
                if (page+2 < 0)
                    modPages.transform.GetChild(5).gameObject.SetActive(false);
                if (page == searchParser.total_hits / 20)
                    modPages.transform.GetChild(6).gameObject.SetActive(false);
                
                //updating text
                modPages.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = (page - 2).ToString();
                modPages.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = (page - 1).ToString();
                modPages.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = page.ToString();
                modPages.transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>().text = (page + 1).ToString();
                modPages.transform.GetChild(5).GetComponentInChildren<TextMeshProUGUI>().text = (page + 2).ToString();
                
                //button listeners
                modPages.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(PageUp);
                modPages.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(PageDown);
            }
        }
        await GetResults();
        isSearching = false;
    }

    public void PageUp()
    {
        page++;
        CreateMods();
    }
    
    public void PageDown()
    {
        page--;
        CreateMods();
    }

    async Task HasModCheck(string modSlug, string[]  fetchedmod = null)
    {
        if (fetchedmod != null)
        {
            if (!fetchedmod.Contains(JNIStorage.GetInstance(InstanceButton.currInstName).versionName))
            {
                downloadText.text = "No mod for version";
                downloadButton.GetComponent<Button>().enabled = false;
                return;
            }
        }
        
        if (JNIStorage.GetInstance(InstanceButton.currInstName) == null)
        {
            downloadText.text = "Instance Not Downloaded";
            downloadButton.GetComponent<Button>().enabled = false;
            return;
        }

        Debug.Log("Current instance: " + InstanceButton.currInstName + "\nChecking for mod " + modSlug);
        bool hasMod = JNIStorage.apiClass.CallStatic<bool>("hasExtraProject",
            JNIStorage.GetInstance(InstanceButton.currInstName).raw, modSlug);
        downloadText.text = hasMod ? "Installed" : "Install";
        downloadButton.GetComponent<Button>().enabled = !hasMod;
    }

    private void CreateModPage(string slug, RawImage image, string author)
    {
        MetaParser mp = apiHandler.GetModInfo(slug);
        instanceMenu.SetActive(false);
        modSearchMenu.SetActive(false);
        modPage.SetActive(true);
        
        modDescription.text = mp.description;
        modTitle.text = mp.title + " <size=75%>by " + author;
        modIDObject.text = mp.slug;
        
        modImage.texture = image.texture;
        
        Destroy(modImage.GetComponent<GifLoader>());
        if (image.gameObject.TryGetComponent(out GifLoader loader))
        {
            GifLoader gifLoader = modImage.AddComponent<GifLoader>();
            gifLoader.LoadGifImage(loader.bytes);
        }
        
        currModSlug = mp.slug;
        currModVersions = mp.game_versions.ToArray();
        HasModCheck(currModSlug, currModVersions);
    }
    
    public void AddMod()
    {
        MetaParser mp = apiHandler.GetModInfo(currModSlug);
        MetaInfo[] modInfos = apiHandler.GetModDownloads(mp.slug);
        string currentInstanceVer = JNIStorage.GetInstance(InstanceButton.currInstName).versionName;

        foreach (MetaInfo metaInfo in modInfos)
        {
            foreach (var file in metaInfo.files.Where(_ => IsValidModFile(metaInfo, mp, currentInstanceVer)))
            {
                if (file.url.Contains(".mrpack"))
                {
                    ProcessModpack(mp, file, currentInstanceVer);
                }
                else
                {
                    ProcessModFile(mp, file, metaInfo, currentInstanceVer);
                }
                
                return;
            }
        }
    }
    
    private bool IsValidModFile(MetaInfo metaInfo, MetaParser metaParser, string instanceVersion)
    {
        if (metaParser.project_type is "mod" or "modpack")
        {
            return metaInfo.game_versions.Contains(instanceVersion)
                   && metaInfo.loaders.Contains("fabric"); 
        }
        else
        {
            return true;
        }

    }

    private void ProcessModFile(MetaParser mp, FileInfo file, MetaInfo metaInfo, string currentInstanceVer)
    {
        AndroidJavaObject instance = LoadInstance();
        if (instance == null) return;

        DownloadDependenciesAndAddMod(mp, metaInfo, file.url, currentInstanceVer);
    }    
    
    private void ProcessModpack(MetaParser mp, FileInfo file, string currentInstanceVer)
    {
        string path = Path.Combine(Application.persistentDataPath, "downloads");
        path = Path.Combine(path, mp.title + ".mrpack");
        Debug.Log($"modName: {mp.title} | modUrl: {file.url} | modVersion: {currentInstanceVer} | modPath: {path}");
        
        UnityWebRequest modpackFile = UnityWebRequest.Get(file.url);
        modpackFile.SetRequestHeader("User-Agent", "QuestCraftPlusPlus/QuestCraft/" + Application.version + " (discord.gg/questcraft)");
        DownloadHandlerFile dh = new DownloadHandlerFile(path);
        dh.removeFileOnAbort = true;
        modpackFile.downloadHandler = dh;
        UnityWebRequestAsyncOperation op = modpackFile.SendWebRequest();

        JNIStorage.instance.uiHandler.SetAndShowError(mp.title + " is downloading, please wait.");
        op.completed += _ =>
        {
            AndroidJavaObject instance = LoadInstance();
            if (instance == null) return;

            JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, JNIStorage.instancesObj, mp.title, mp.icon_url, "Fabric", path);
            JNIStorage.instance.uiHandler.SetAndShowError(mp.title + " is now being created.");
            JNIStorage.instance.UpdateInstances();
        };
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
                        JNIStorage.apiClass.CallStatic("addExtraProject", JNIStorage.instancesObj, currInst.raw, slug, depInfo.version_number, depFile.url, mp.project_type);
                        Debug.Log($"Downloading Dep with file url {depFile.url}");
                        break;
                    }
                }
            }
        }

        PojlibInstance inst = JNIStorage.GetInstance(InstanceButton.currInstName);
        JNIStorage.apiClass.CallStatic("addExtraProject", JNIStorage.instancesObj, inst.raw, mp.slug, metaInfo.version_number, modUrl, mp.project_type);
        UpdateUIAfterModAddition(mp.slug);
    }

    private bool IsValidDependency(FileInfo depFile, MetaInfo depInfo, string currentInstanceVer, string slug)
    {
        return depInfo.game_versions.Contains(currentInstanceVer)
               && !depFile.url.Contains(".mrpack")
               && depInfo.loaders.Contains("fabric")
               && !JNIStorage.apiClass.CallStatic<bool>("hasExtraProject", JNIStorage.GetInstance(InstanceButton.currInstName).raw, slug);
    }

    private void UpdateUIAfterModAddition(string slug)
    {
        bool hasMod = JNIStorage.apiClass.CallStatic<bool>("hasExtraProject", JNIStorage.GetInstance(InstanceButton.currInstName).raw, slug);
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
        JNIStorage.apiClass.CallStatic<bool>("removeExtraProject", JNIStorage.instancesObj, currInstName.raw, modName);
             downloadText.text = "Install";
             SearchMods();
    }

    public void SearchMods()
    {
        page = 0;
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
