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
using WebP;

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
    public WindowHandler windowHandler;
    public Texture2D errorTexture;

    [FormerlySerializedAs("ModCounter")] public TextMeshProUGUI modCounter;
    [SerializeField] private GameObject modPrefab;
    [SerializeField] private GameObject modArray;

    public void CreateCustomInstance()
    {
        try
        {
            if (instanceName.text.Trim() == "")
            {
                JNIStorage.instance.uiHandler.SetAndShowError("Instance name cannot be blank, please enter a name.");
                return;
            }
            JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, JNIStorage.instancesObj, instanceName.text, defaultModsToggle.isOn, versionDropdown.options[versionDropdown.value].text, null);
            JNIStorage.instance.uiHandler.SetAndShowError(instanceName.text + " is now being created.");
            
            JNIStorage.instance.UpdateInstances();
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
                instanceGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = instance.instanceName;
                instanceGameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = instance.versionName + " - Fabric";
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
                    Texture modImageTexture = errorTexture;
                    if (instance.instanceImageURL.ToLower().EndsWith(".webp"))
                    {
                        UnityWebRequest modImageLink = UnityWebRequest.Get(instance.instanceImageURL);
                        modImageLink.SendWebRequest();
                
                        while (!modImageLink.isDone)
                            await Task.Delay(16);
                        if (modImageLink.result != UnityWebRequest.Result.Success)
                            Debug.Log(modImageLink.error);
                        else
                            modImageTexture = Texture2DExt.CreateTexture2DFromWebP(modImageLink.downloadHandler.data, lMipmaps: true, lLinear: false, lError: out Error lError);;
                    }
                    else
                    {
                        UnityWebRequest instanceImageLink = UnityWebRequestTexture.GetTexture(instance.instanceImageURL);
                        instanceImageLink.SendWebRequest();

                        while (!instanceImageLink.isDone)
                            await Task.Delay(16);
                        if (instanceImageLink.result != UnityWebRequest.Result.Success)
                            Debug.Log(instanceImageLink.error);
                        else
                            modImageTexture = ((DownloadHandlerTexture)instanceImageLink.downloadHandler).texture;   
                    }
                    
                    instanceGameObject.GetComponentInChildren<RawImage>().texture = modImageTexture;
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

        async Task GetSetTexture()
        {
            if (instance.instanceImageURL != null)
            {
                Texture instanceImageTex = errorTexture;
                if (instance.instanceImageURL.ToLower().EndsWith(".webp"))
                {
                    UnityWebRequest modImageLink = UnityWebRequest.Get(instance.instanceImageURL);
                    modImageLink.SendWebRequest();
                
                    while (!modImageLink.isDone)
                        await Task.Delay(16);
                    if (modImageLink.result != UnityWebRequest.Result.Success)
                        Debug.Log(modImageLink.error);
                    else
                        instanceImageTex = Texture2DExt.CreateTexture2DFromWebP(modImageLink.downloadHandler.data, lMipmaps: true, lLinear: false, lError: out Error lError);;
                }
                else
                {
                    UnityWebRequest instanceImageLink = UnityWebRequestTexture.GetTexture(instance.instanceImageURL);
                    instanceImageLink.SendWebRequest();

                    while (!instanceImageLink.isDone)
                        await Task.Delay(16);
                    instanceImageTex = ((DownloadHandlerTexture)instanceImageLink.downloadHandler).texture;
                }
                instanceImage.texture = instanceImageTex;
            }

            instanceVersion.text = instance.versionName + " - Fabric";
            instanceTitle.text = instance.instanceName;
        }

        await GetSetTexture();
        
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
            www.SendWebRequest();
            while (!www.isDone)
                await Task.Delay(16);
            if (www.result != UnityWebRequest.Result.Success)
                Debug.Log(www.error);
            else
                fetchedMods = new List<MetaParser>(JsonConvert.DeserializeObject<MetaParser[]>(www.downloadHandler.text));

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
                JNIStorage.apiClass.CallStatic<bool>("removeMod", JNIStorage.instancesObj, instance.raw, mod.slug);
                Destroy(modObject.gameObject);
                CountMods(modArray.transform.childCount - 1);
            });

            async Task SetModInfo()
            {
                modObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = mod.title;
                modObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = mod.description;
                Texture modImageTexture = errorTexture;
                if (mod.icon_url != "OFFLINE")
                {
                    if (mod.icon_url.ToLower().EndsWith(".webp"))
                    {
                        UnityWebRequest modImageLink = UnityWebRequest.Get(mod.icon_url);
                        modImageLink.SendWebRequest();
                
                        while (!modImageLink.isDone)
                            await Task.Delay(16);
                        if (modImageLink.result != UnityWebRequest.Result.Success)
                            Debug.Log(modImageLink.error);
                        else
                            modImageTexture = Texture2DExt.CreateTexture2DFromWebP(modImageLink.downloadHandler.data, lMipmaps: true, lLinear: false, lError: out Error lError);;
                    }
                    else
                    {
                        UnityWebRequest modImageLink = UnityWebRequestTexture.GetTexture(mod.icon_url);
                        modImageLink.SendWebRequest();
                        while (!modImageLink.isDone)
                            await Task.Delay(16);
                        if (modImageLink.result != UnityWebRequest.Result.Success)
                            Debug.Log(modImageLink.error);
                        else
                            modImageTexture = ((DownloadHandlerTexture)modImageLink.downloadHandler).texture;
                    }
                }
                modObject.GetComponentInChildren<RawImage>().texture = modImageTexture;
            }
            SetModInfo();
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