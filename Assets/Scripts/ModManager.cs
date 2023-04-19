using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
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
    public TMP_InputField searchQuery;
    public RawImage modImage;
    public GameObject modManagerMainpage;
    public GameObject modSearchMenu;
    public GameObject instanceMenu;
    

    public async void CreateMods()
    {
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
                    modObject.GetComponent<Button>().onClick.AddListener(CreateModPage);
                }
            }

            await GetSetTexture();
        }
    }
    
    public async void CreateModPage()
    {
        SearchParser sq = apiHandler.GetSearchedMods();
        instanceMenu.SetActive(false);
        modSearchMenu.SetActive(false);
        modManagerMainpage.SetActive(false);
        modPage.SetActive(true);
        Debug.Log("In method");

        foreach (SearchResults searchResults in sq.hits)
        {
            Debug.Log("In Loop");
            async Task GetSetTexture()
            {
                UnityWebRequest modImageLink = UnityWebRequestTexture.GetTexture(searchResults.icon_url);

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
                    modDescription.text = searchResults.description;
                    modTitle.text = searchResults.title;
                    modImage.texture = modImageTexture;
                }
            }

            await GetSetTexture();
        }
    }

    public void SearchMods()
    {
        apiHandler.searchQuery = searchQuery.text;
        CreateMods();
    }
}
