using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using WebP;


public class APIHandler : MonoBehaviour
{
    public GameObject errorMenu;
    public Texture2D errorTexture;
    
    private void ShowError(string message)
    {
        errorMenu.GetComponentInChildren<TextMeshProUGUI>().text = message;
        errorMenu.SetActive(true);
    }

    public void DownloadImage(string url, RawImage image)
    {
        async Task DownloadImageTask()
        {
            Destroy(image.GetComponent<GifLoader>());
            image.texture = errorTexture;
            if (url.ToLower().EndsWith(".webp"))
            {
                Debug.Log("Downloading webp at url: \n" + url);
                UnityWebRequest modImageLink = UnityWebRequest.Get(url);
                modImageLink.SendWebRequest();

                while (!modImageLink.isDone)
                    await Task.Delay(16);
                if (modImageLink.result != UnityWebRequest.Result.Success)
                    Debug.Log(modImageLink.error);
                else
                    image.texture = Texture2DExt.CreateTexture2DFromWebP(modImageLink.downloadHandler.data,
                        lMipmaps: true,
                        lLinear: false, lError: out Error lError);
            }
            else if (url.ToLower().EndsWith(".gif"))
            {
                Debug.Log("Downloading gif at url: \n" + url);

                UnityWebRequest modImageLink = UnityWebRequest.Get(url);
                modImageLink.SendWebRequest();

                while (!modImageLink.isDone)
                    await Task.Delay(16);
                if (modImageLink.result != UnityWebRequest.Result.Success)
                    Debug.Log(modImageLink.error);
                else
                    image.texture = image.AddComponent<GifLoader>().LoadGifImage(modImageLink.downloadHandler.data);
            }
            else
            {
                Debug.Log("Downloading a random format at url: \n" + url);
                UnityWebRequest modImageLink = UnityWebRequestTexture.GetTexture(url);
                modImageLink.SendWebRequest();

                while (!modImageLink.isDone)
                    await Task.Delay(16);

                if (modImageLink.result != UnityWebRequest.Result.Success)
                    Debug.Log(modImageLink.error);
                else
                    image.texture = ((DownloadHandlerTexture)modImageLink.downloadHandler).texture;
            }
        }

        DownloadImageTask();
    }

    public MetaParser GetModInfo(string modID)
    {
        var request = (HttpWebRequest)WebRequest.Create("https://api.modrinth.com/v2/project/" + modID);
        using var response = (HttpWebResponse)request.GetResponse();
        using var reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd(); 
        return JsonConvert.DeserializeObject<MetaParser>(json);
    }

    public MetaInfo[] GetModDownloads(string modID)
    {
        var request = (HttpWebRequest)WebRequest.Create("https://api.modrinth.com/v2/project/" + modID + "/version");
        using var response = (HttpWebResponse)request.GetResponse();
        using var reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<MetaInfo[]>(json);
    }
    
    public List<Deps> GetModDeps(string modID, string versionID)
    {
        var request = (HttpWebRequest)WebRequest.Create("https://api.modrinth.com/v2/project/" + modID + "/version/" + versionID);
        using var response = (HttpWebResponse)request.GetResponse();
        using var reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<MetaInfo>(json).dependencies;
    }
} 
