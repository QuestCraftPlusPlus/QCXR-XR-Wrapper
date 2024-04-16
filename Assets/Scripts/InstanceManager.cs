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
    [SerializeField] private GameObject modArray;
    [SerializeField] private GameObject instancePrefab;
    public TMP_InputField instanceName;
    public Toggle defaultModsToggle;
    public TMP_Dropdown versionDropdown; 
    private string currModSlug;
    
    public void CreateCustomInstance()
    {
        try
        {
            JNIStorage.apiClass.CallStatic<AndroidJavaObject>("createNewInstance", JNIStorage.activity, JNIStorage.instancesObj, instanceName.text, JNIStorage.home, defaultModsToggle.isOn, versionDropdown.options[versionDropdown.value].text, instanceName.text, null);
            JNIStorage.instance.UpdateInstances();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }
    }
    
    private async void CreateInstanceArray()
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
                instanceGameObject.transform.SetParent(modArray.transform, false);
                instanceGameObject.name = instance.instanceName;

                instanceGameObject.GetComponent<Button>().onClick.AddListener(delegate
                {
                    EventSystem.current.SetSelectedGameObject(instanceGameObject);
                    GameObject instance = GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.name);
                    //CreateInstanceInfoPage(instance.ToString().Replace("(UnityEngine.GameObject)", ""));
					currModSlug = instance.ToString().Replace("(UnityEngine.GameObject)", "");
                });
            }

            await SetInstanceData();
        }
    }
    
    /*public async void CreateInstanceInfoPage(string slug)
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
    }*/

    private void ResetArray()
    {
        for (int i = modArray.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(modArray.transform.GetChild(i).gameObject);
        }
    }
}