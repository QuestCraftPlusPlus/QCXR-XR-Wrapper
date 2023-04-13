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
    [SerializeReference] public APIHandler apiHandler;
    public TextMeshPro modDescription;
    public TextMeshPro modTitle;
    public RawImage modImage;
    

    public async void CreateMod()
    {
        APIParser.SearchParser sq = apiHandler.GetSearchedMods();
        
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
                    GameObject modObject = Instantiate(modPrefab, new Vector3(-10, -10, -10), Quaternion.identity);
                    modObject.GetComponentInChildren<RawImage>().texture = modImageTexture;
                    modObject.transform.SetParent(modArray.transform, false);
                }
            }
        }
    }
    
    public async void CreateModPage()
    {
        APIParser.SearchParser sq = apiHandler.GetSearchedMods();
        
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
                    modDescription.text = searchResults.description;
                    modTitle.text = searchResults.title;
                    modImage.texture = modImageTexture;
                }
            }
        }
    }
}
