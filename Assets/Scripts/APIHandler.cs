using System.Collections.Generic;
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
                modImageLink.SetRequestHeader("User-Agent", "QuestCraftPlusPlus/QuestCraft/" + Application.version + " (discord.gg/questcraft)");
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
                modImageLink.SetRequestHeader("User-Agent", "QuestCraftPlusPlus/QuestCraft/" + Application.version + " (discord.gg/questcraft)");
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
                modImageLink.SetRequestHeader("User-Agent", "QuestCraftPlusPlus/QuestCraft/" + Application.version + " (discord.gg/questcraft)");
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
        UnityWebRequest www = UnityWebRequest.Get("https://api.modrinth.com/v2/project/" + modID);
        www.SetRequestHeader("User-Agent", "QuestCraftPlusPlus/QuestCraft/" + Application.version + " (discord.gg/questcraft)");
        www.SendWebRequest();

        while (!www.isDone)
            Task.Delay(16);
        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);
        else
            return JsonConvert.DeserializeObject<MetaParser>(www.downloadHandler.text);
        return null;
    }

    public MetaInfo[] GetModDownloads(string modID)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://api.modrinth.com/v2/project/" + modID + "/version");
        www.SetRequestHeader("User-Agent", "QuestCraftPlusPlus/QuestCraft/" + Application.version + " (discord.gg/questcraft)");
        www.SendWebRequest();

        while (!www.isDone)
            Task.Delay(16);
        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);
        else
            return JsonConvert.DeserializeObject<MetaInfo[]>(www.downloadHandler.text);
        return null;
    }
    
    public List<Deps> GetModDeps(string modID, string versionID)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://api.modrinth.com/v2/project/" + modID + "/version/" + versionID);
        www.SetRequestHeader("User-Agent", "QuestCraftPlusPlus/QuestCraft/" + Application.version + " (discord.gg/questcraft)");
        www.SendWebRequest();

        while (!www.isDone)
            Task.Delay(16);
        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);
        else
            return JsonConvert.DeserializeObject<MetaInfo>(www.downloadHandler.text).dependencies;
        return null;
    }
} 
