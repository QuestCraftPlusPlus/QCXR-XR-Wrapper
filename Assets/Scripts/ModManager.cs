using System;
using System.IO;
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
                
                try
                {
                    bool hasMod = JNIStorage.apiClass.CallStatic<bool>("hasMod", JNIStorage.GetInstance(InstanceButton.currInstName).raw, searchResults.project_id);

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
                bool hasMod = JNIStorage.apiClass.CallStatic<bool>("hasMod", JNIStorage.GetInstance(InstanceButton.currInstName).raw, mp.slug);
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
            ShowError("You must run this version of the game at least once before adding mods to the instance with ModManger!");
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
