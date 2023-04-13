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
    [SerializeReference] public GameObject ModPrefab;
    [SerializeReference] public GameObject ModArray;
    [SerializeReference] public APIHandler APIHandler;
    public TextMeshPro ModDescription;
    public TextMeshPro ModTitle;
    public RawImage ModImage;
    

    public async void CreateMod()
    {
        APIParser.SearchParser sq = APIHandler.GetSearchedMods();
        
        foreach (APIParser.SearchResults searchResults in sq.hits)
        {
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
                    GameObject modObject = Instantiate(ModPrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                    modObject.GetComponentInChildren<RawImage>().texture = modImageTexture;
                    modObject.transform.SetParent(ModArray.transform, false);
                }
            }
        }
    }
    
    public async void CreateModPage()
    {
        APIParser.SearchParser sq = APIHandler.GetSearchedMods();
        
        foreach (APIParser.SearchResults searchResults in sq.hits)
        {
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
                    ModDescription.text = searchResults.description;
                    ModTitle.text = searchResults.title;
                    ModImage.texture = modImageTexture;
                }
            }
        }
    }
}
