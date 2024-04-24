using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    
    public void CreateCustomInstance()
    {
        try
        {
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
    }
    
    public async void CreateInstanceInfoPage(string slug)
    {
        PojlibInstance instance = JNIStorage.GetInstance(slug);
        windowHandler.instanceInfoSetter();

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
        windowHandler.instanceInfoUnsetter();
    }

    private void ResetArray()
    {
        for (int i = instanceArray.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(instanceArray.transform.GetChild(i).gameObject);
        }
    }
}