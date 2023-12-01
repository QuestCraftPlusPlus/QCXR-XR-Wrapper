using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;


public class APIHandler : MonoBehaviour
{
    public GameObject modButton;
    public GameObject modpacksButton;
    public GameObject resourcePacksButton;
    public string searchQuery;

    public SearchParser GetSearchedMods()
    {
        string currInstName =
            JNIStorage.apiClass.CallStatic<string>("getQCSupportedVersionName", InstanceButton.currentVersion);
        var request = (HttpWebRequest)WebRequest.Create("https://api.modrinth.com/v2/search?query=" + searchQuery + "&facets=[[\"versions:" + currInstName + "\"], [\"project_type:" + GetFilterOption() + "\"], [\"categories:fabric\"]]");
        using var response = (HttpWebResponse)request.GetResponse();
        using var reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<SearchParser>(json);
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

    public Deps GetModDeps(string modID)
    {
        var request =
            (HttpWebRequest)WebRequest.Create("https://api.modrinth.com/v2/project/" + modID + "/dependencies");
        using var response = (HttpWebResponse)request.GetResponse();
        using var reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<Deps>(json);
    }

    public string GetFilterOption()
    {
        List<Toggle> toggleButtons = new List<Toggle>
        {
            modButton.GetComponent<Toggle>(),
            modpacksButton.GetComponent<Toggle>(),
            resourcePacksButton.GetComponent<Toggle>()
        };

        foreach (Toggle button in toggleButtons)
        {
            if (!button.interactable)
            {
                return button.name;
            }
        }

        return null;
    }

} 
