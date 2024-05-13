using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
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
    public WindowHandler windowHandler;
    public Texture2D errorTexture;
    
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
    
    public async void CreateInstanceArray()
    {
        ResetArray();

        foreach (var instanceObj in JNIStorage.instancesObj.Call<AndroidJavaObject[]>("toArray"))
        {
            PojlibInstance instance = PojlibInstance.Parse(instanceObj);
            
            async Task SetInstanceData()
            {
                GameObject instanceGameObject = Instantiate(instancePrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                
                if (instance.instanceImageURL != null)
                {
                    UnityWebRequest instanceImageLink = UnityWebRequestTexture.GetTexture(instance.instanceImageURL);
                    instanceImageLink.SendWebRequest();

                    //TODO: Remove artificial wait. 
                    while (!instanceImageLink.isDone)
                    {
                        await Task.Delay(50);
                    }

                    if (instanceImageLink.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log(instanceImageLink.error);
                        return;
                    }

                    Texture modImageTexture = ((DownloadHandlerTexture)instanceImageLink.downloadHandler).texture;
                    instanceGameObject.GetComponentInChildren<RawImage>().texture = modImageTexture;
                }

                instanceGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = instance.instanceName;
                instanceGameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = instance.versionName + " - Fabric";
                instanceGameObject.transform.SetParent(instanceArray.transform, false);
                instanceGameObject.name = instance.instanceName;

                instanceGameObject.GetComponent<Button>().onClick.AddListener(delegate
                {
                    EventSystem.current.SetSelectedGameObject(instanceGameObject);
                    GameObject InstanceObject = GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.name);
                    CreateInstanceInfoPage(InstanceObject.name);
                });
            }

            await SetInstanceData();
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

    public async void CreateInstanceInfoPage(string slug)
    {
        PojlibInstance instance = JNIStorage.GetInstance(slug);
        windowHandler.InstanceInfoSetter();

        async Task GetSetTexture()
        {
            if (instance.instanceImageURL != null)
            {
                UnityWebRequest instanceImageLink = UnityWebRequestTexture.GetTexture(instance.instanceImageURL);
                instanceImageLink.SendWebRequest();

                while (!instanceImageLink.isDone)
                {
                    await Task.Delay(50);
                }

                Texture instanceImageTex = ((DownloadHandlerTexture)instanceImageLink.downloadHandler).texture;
                instanceImage.texture = instanceImageTex;
            }

            instanceVersion.text = instance.versionName + " - Fabric";
            instanceTitle.text = instance.instanceName;
        }
        
        await GetSetTexture();
        for (int i = modArray.transform.childCount - 1; i >= 0; i--)
            if (modArray.transform.GetChild(i).name != "BaseItems")
                Destroy(modArray.transform.GetChild(i).gameObject);
        
        PojlibMod[] ModList = instance.GetMods();
        foreach (PojlibMod Mod in ModList)
        {
            Debug.Log("Loading Mod " + Mod.slug);
            MetaParser metaParser = null;
            async Task GetModInfo()
            {
                UnityWebRequest www = UnityWebRequest.Get("https://api.modrinth.com/v2/project/" + Mod.slug);
                www.SendWebRequest();
                while (!www.isDone)
                    await Task.Delay(16);
                if (www.result != UnityWebRequest.Result.Success)
                    Debug.Log(www.error);
                else
                    metaParser = JsonConvert.DeserializeObject<MetaParser>(www.downloadHandler.text);
                
                SetModInfo();
            }

            async Task SetModInfo()
            {
                GameObject modObject = Instantiate(modPrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                modObject.name = Mod.slug;
                modObject.transform.SetParent(modArray.transform, false);
                modObject.GetComponent<RectTransform>().sizeDelta = new(850 ,modObject.GetComponent<RectTransform>().sizeDelta.y);
                modObject.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate
                {
                    //public static boolean removeMod(MinecraftInstances instances, MinecraftInstances.Instance instance, String gameDir, String name)
                    JNIStorage.apiClass.CallStatic<bool>("removeMod", JNIStorage.instancesObj, instance.raw, Mod.slug);
                    Destroy(modObject.gameObject);
                });
                
                if (metaParser != null)
                {
                    modObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = metaParser.title;
                    modObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = metaParser.description;
                    
                    UnityWebRequest modImageLink = UnityWebRequestTexture.GetTexture(metaParser.icon_url);
                    modImageLink.SendWebRequest();

                    while (!modImageLink.isDone)
                        await Task.Delay(16);

                    Texture modImageTexture;
                    if (modImageLink.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log(modImageLink.error);
                        modImageTexture = errorTexture;
                    }
                    else
                        modImageTexture = ((DownloadHandlerTexture)modImageLink.downloadHandler).texture;
                    
                    modObject.GetComponentInChildren<RawImage>().texture = modImageTexture;
                }
                else
                {
                    modObject.GetComponentInChildren<RawImage>().texture = errorTexture;
                    modObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Mod.slug;
                    modObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
                }
                modObject.transform.GetChild(3).gameObject.SetActive(true);
            }
            
            GetModInfo();
        }
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