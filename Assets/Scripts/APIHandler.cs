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

	public async void DownloadImage(string url, RawImage image)
	{
		Destroy(image.GetComponent<GifLoader>());
		image.texture = errorTexture;
		if (url.ToLower().EndsWith(".webp"))
		{
			Debug.Log("Downloading webp at url: \n" + url);
			UnityWebRequest modImageLink = UnityWebRequest.Get(url);
			modImageLink.SetRequestHeader("User-Agent", "QuestCraftPlusPlus/QuestCraft/" + Application.version + " (discord.gg/questcraft)");
			await modImageLink.SendWebRequest();

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
			await modImageLink.SendWebRequest();

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
			await modImageLink.SendWebRequest();

			if (modImageLink.result != UnityWebRequest.Result.Success)
				Debug.Log(modImageLink.error);
			else
				image.texture = ((DownloadHandlerTexture)modImageLink.downloadHandler).texture;
		}
		await Task.CompletedTask;
	}

	public async Task<MetaParser> GetModInfoAsync(string modID)
	{
		MetaParser result = null;

		UnityWebRequest www = UnityWebRequest.Get("https://api.modrinth.com/v2/project/" + modID);
		www.SetRequestHeader("User-Agent", "QuestCraftPlusPlus/QuestCraft/" + Application.version + " (discord.gg/questcraft)");
		await www.SendWebRequest();

		if (www.result != UnityWebRequest.Result.Success)
			Debug.Log(www.error);
		else
			result = JsonConvert.DeserializeObject<MetaParser>(www.downloadHandler.text);

		await Task.CompletedTask;
		return result;
    }
    public MetaParser GetModInfo(string modID)
    {
        Task<MetaParser> task = Task.Run(async () => await GetModInfoAsync(modID));
        return task.Result;
    }

    public async Task<MetaInfo[]> GetModDownloadsAsync(string modID)
	{
		MetaInfo[] result = null;

		UnityWebRequest www = UnityWebRequest.Get("https://api.modrinth.com/v2/project/" + modID + "/version");
		www.SetRequestHeader("User-Agent", "QuestCraftPlusPlus/QuestCraft/" + Application.version + " (discord.gg/questcraft)");
		await www.SendWebRequest();

		if (www.result != UnityWebRequest.Result.Success)
			Debug.Log(www.error);
		else
			result = JsonConvert.DeserializeObject<MetaInfo[]>(www.downloadHandler.text);

		await Task.CompletedTask;
		return result;
    }
    public MetaInfo[] GetModDownloads(string modID)
    {
        Task<MetaInfo[]> task = Task.Run(async () => await GetModDownloadsAsync(modID));
        return task.Result;
    }

    public async Task<List<Deps>> GetModDepsAsync(string modID, string versionID)
	{
		List<Deps> result = null;

		UnityWebRequest www = UnityWebRequest.Get("https://api.modrinth.com/v2/project/" + modID + "/version/" + versionID);
		www.SetRequestHeader("User-Agent", "QuestCraftPlusPlus/QuestCraft/" + Application.version + " (discord.gg/questcraft)");
		await www.SendWebRequest();

		if (www.result != UnityWebRequest.Result.Success)
			Debug.Log(www.error);
		else
			result = JsonConvert.DeserializeObject<MetaInfo>(www.downloadHandler.text).dependencies;

		await Task.CompletedTask;
		return null;
	}
	public List<Deps> GetModDeps(string modID, string versionID)
	{
		Task<List<Deps>> task = Task.Run(async () => await GetModDepsAsync(modID, versionID));
		return task.Result;
	}
} 
