using System;
using System.Collections;
using System.Collections.Generic;
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

                    if (!JNIStorage.apiClass.CallStatic<Boolean>("hasMod", InstanceButton.CreateOrGetInstance(), searchResults.title))
                    {
                        modObject.GetComponentInChildren<Button>().interactable = false;
                    }
                    else
                    {
                        modObject.GetComponentInChildren<Button>().interactable = true;
                        modObject.GetComponentInChildren<InteractableUnityEventWrapper>().WhenSelect.AddListener(delegate
                        {
                            RemoveMod();
                        });
                    }
                    
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

            if (!JNIStorage.apiClass.CallStatic<bool>("hasMod", InstanceButton.CreateOrGetInstance(), mp.title))
            {
                DLDImage.SetActive(false);
                DLImage.SetActive(true);
            }
            else
            {
                DLImage.SetActive(false);
                DLDImage.SetActive(true);
            }
        }

        await GetSetTexture();
    }
    
    public void AddMod()
    {
        apiHandler.modID = modIDObject.text;
        MetaParser mp = apiHandler.GetModInfo();
        MetaInfo mi = apiHandler.GetModDownloads();

        foreach (FileInfo fileInfo in mi.files)
        {
            string name = mp.title;
            string url = fileInfo.url;
            string version = fileInfo.version;
            JNIStorage.apiClass.CallStatic("addCustomMod", InstanceButton.CreateOrGetInstance(), name, url, version);
        }
        
        DLImage.SetActive(false);
        DLDImage.SetActive(true);
    }
    
    public void RemoveMod()
    {
        string name = modIDObject.text;
        JNIStorage.apiClass.CallStatic("removeMod", InstanceButton.CreateOrGetInstance(), name);
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
