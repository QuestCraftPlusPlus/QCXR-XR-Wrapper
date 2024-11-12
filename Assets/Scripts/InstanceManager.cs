using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InstanceManager : MonoBehaviour
{
    [SerializeField] private GameObject instanceArray;
    [SerializeField] private GameObject instancePrefab;
    [SerializeField] private TextMeshProUGUI instanceVersion;
    [SerializeField] private TextMeshProUGUI instanceTitle;
    [SerializeField] private RawImage instanceImage;
    [SerializeField] private GameObject instanceRemoveMenu;
    public TMP_InputField instanceName;
    public Toggle defaultModsToggle;
    public TMP_Dropdown versionDropdown;
    public TMP_Dropdown loaderDropdown;
    public WindowHandler windowHandler;
    public Texture2D errorTexture;

    [FormerlySerializedAs("ModCounter")] public TextMeshProUGUI modCounter;
    [SerializeField] private GameObject modPrefab;
    [SerializeField] private GameObject modArray;
    [SerializeField] private APIHandler apiHandler;
    
    public void Start()
    {
        List<string> modLoaders = new List<string>
        {
            "Fabric",
            "Forge",
            "Quilt",
            "NeoForge"
        };
        
        loaderDropdown.AddOptions(modLoaders);
    }
    
    public void UpdateMenu()
    {
        string selectedModloader = loaderDropdown.options[loaderDropdown.value].text;
        defaultModsToggle.interactable = selectedModloader is "Fabric" or "Quilt";
        defaultModsToggle.isOn = selectedModloader is "Fabric" or "Quilt";
    }

    public void CreateCustomInstance()
    {
        try
        {
            string selectedModloader = loaderDropdown.options[loaderDropdown.value].text;

            instanceName.text = instanceName.text.Trim();
            if (instanceName.text == "")
            {
                JNIStorage.instance.uiHandler.SetAndShowError("Instance name cannot be blank, please enter a name.");
                return;
            }
            
            HashSet<string> instanceNames = new HashSet<string>
                (JNIStorage.instancesObj.Call<AndroidJavaObject[]>("toArray")
                    .Select(instance => PojlibInstance.Parse(instance).instanceName.ToLower()));

            if (instanceNames.Add(instanceName.text.ToLower()))
            {
                JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, JNIStorage.instancesObj, instanceName.text, defaultModsToggle.isOn, versionDropdown.options[versionDropdown.value].text, selectedModloader, null);
                JNIStorage.instance.uiHandler.SetAndShowError(instanceName.text + " is now being created.");
            
                JNIStorage.instance.UpdateInstances();
            }
            else
                JNIStorage.instance.uiHandler.SetAndShowError("Instance already exists, please choose another name.");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }
    }
    
    public void CreateInstanceArray()
    {
        ResetArray();

        foreach (var instanceObj in JNIStorage.instancesObj.Call<AndroidJavaObject[]>("toArray"))
        {
            async Task SetInstanceData()
            {
                PojlibInstance instance = PojlibInstance.Parse(instanceObj);
                GameObject instanceGameObject = Instantiate(instancePrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                instanceGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = instance.instanceName; instanceGameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = instance.versionName + " - Fabric";
                instanceGameObject.transform.SetParent(instanceArray.transform, false);
                instanceGameObject.name = instance.instanceName;

                instanceGameObject.GetComponent<Button>().onClick.AddListener(delegate
                {
                    EventSystem.current.SetSelectedGameObject(instanceGameObject);
                    GameObject instanceObject = GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.name);
                    CreateInstanceInfoPage(instanceObject.name);
                });

                if (instance.instanceImageURL != null)
                {
                    apiHandler.DownloadImage(instance.instanceImageURL, instanceGameObject.GetComponentInChildren<RawImage>());
                }
            }
            SetInstanceData();
        }

        if (instanceArray.transform.childCount == 0)
        {
            GameObject instanceGameObject = Instantiate(instancePrefab, new Vector3(-10, -10, -10), Quaternion.identity);
            instanceGameObject.GetComponentInChildren<RawImage>().texture = errorTexture;
            instanceGameObject.GetComponentInChildren<RawImage>().color = Color.yellow;
            instanceGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "No instances could be found!";
            instanceGameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Make sure you have an instance downloaded.";
            instanceGameObject.transform.SetParent(instanceArray.transform, false);
            instanceGameObject.name = "ERROR";
        }
    }

    async void CreateInstanceInfoPage(string slug)
    {
        PojlibInstance instance = JNIStorage.GetInstance(slug);
        windowHandler.InstanceInfoSetter();

        instanceVersion.text = instance.versionName + " - Fabric";
        instanceTitle.text = instance.instanceName;

        if (instance.instanceImageURL != null)
            apiHandler.DownloadImage(instance.instanceImageURL, instanceImage);

        for (int i = modArray.transform.childCount - 1; i >= 0; i--)
            Destroy(modArray.transform.GetChild(i).gameObject);

        PojlibMod[] modList = instance.GetMods();

        void CountMods(int count)
        {
            Color counterColor;
            if (count < 50)
                counterColor = Color.Lerp(new Color(3, 152, 252), new Color(255, 108, 10), (float)count / 50);
            else if (count == 69)
                counterColor = Color.green;
            else
                counterColor = Color.Lerp(new Color(255, 108, 10), new Color(255, 0, 0), (count - 50) / 50.0f);
            modCounter.text = $"Currently Installed mods: <color={ColorToHex(counterColor)}>{count}";
        }

        CountMods(modList.Length);

        List<MetaParser> fetchedMods = new();

        async Task GetModInfoArray()
        {
            string request = "https://api.modrinth.com/v2/projects?ids=[";
            foreach (PojlibMod mod in modList)
                request += $"\"{mod.slug}\",";
            request = request.Remove(request.Length - 1, 1) + "]";

            UnityWebRequest www = UnityWebRequest.Get(request);
            www.SetRequestHeader("User-Agent", "QuestCraftPlusPlus/QuestCraft/" + Application.version + " (discord.gg/questcraft)");
            www.SendWebRequest();
            while (!www.isDone)
                await Task.Delay(16);
            if (www.result != UnityWebRequest.Result.Success)
                Debug.Log(www.error);
            else
                fetchedMods =
                    new List<MetaParser>(JsonConvert.DeserializeObject<MetaParser[]>(www.downloadHandler.text));

            HashSet<string> fetchedSlugs = new HashSet<string>(fetchedMods.Select(mod => mod.slug.ToLower()));
            foreach (PojlibMod mod in modList)
            {
                string currentSlug = mod.slug.ToLower();
                if (fetchedSlugs.Add(currentSlug))
                {
                    MetaParser newMod = new MetaParser
                    {
                        slug = currentSlug,
                        title = currentSlug,
                        description = "",
                        icon_url = "OFFLINE"
                    };
                    fetchedMods.Add(newMod);
                }
            }
        }

        await GetModInfoArray();

        //sort it
        fetchedMods = fetchedMods.OrderBy(mod => mod.title).ToList();

        foreach (MetaParser mod in fetchedMods)
        {
            GameObject modObject = Instantiate(modPrefab, new Vector3(-10, -10, -10), Quaternion.identity);
            modObject.name = mod.slug;
            modObject.transform.SetParent(modArray.transform, false);
            modObject.GetComponent<RectTransform>().sizeDelta =
                new(675, modObject.GetComponent<RectTransform>().sizeDelta.y);
            modObject.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate
            {
                JNIStorage.apiClass.CallStatic<bool>("removeExtraProject", JNIStorage.instancesObj, instance.raw, mod.slug);
                Destroy(modObject.gameObject);
                CountMods(modArray.transform.childCount - 1);
            });

            modObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = mod.title;
            modObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = mod.description;
            if (mod.icon_url != "OFFLINE")
            {
                apiHandler.DownloadImage(mod.icon_url, modObject.GetComponentInChildren<RawImage>());
            }
        }
    }

    string ColorToHex(Color color)
    {
        int r = (int)color.r;
        int g = (int)color.g;
        int b = (int)color.b;
        return "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
    }

    public void RemoveInstancePrompt()
    {
        instanceRemoveMenu.GetComponentInChildren<TextMeshProUGUI>().text = "Pressing continue will delete this instance forever! Are you sure you want to do this?";
        instanceRemoveMenu.SetActive(true);
    }

    public void RemoveInstance()
    {
        JNIStorage.apiClass.CallStatic<bool>("deleteInstance", JNIStorage.instancesObj, JNIStorage.GetInstance(instanceTitle.text).raw);
        instanceRemoveMenu.SetActive(false);
        windowHandler.InstanceInfoUnsetter();
    }

    private void ResetArray()
    {
        for (int i = instanceArray.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(instanceArray.transform.GetChild(i).gameObject);
        }
    }
}