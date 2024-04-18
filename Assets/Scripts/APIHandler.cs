using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;


public class APIHandler : MonoBehaviour
{
    public GameObject modButton;
    public GameObject modPacksButton;
    public GameObject resourcePacksButton;
    public string searchQuery;

    //TODO: Add error handling
    
    public SearchParser GetSearchedProjects()
    {
        string currInstName = JNIStorage.GetInstance(InstanceButton.currInstName).versionName;
        string filterOption = GetFilterOption();

        List<string> facets = new List<string>
        {
            "[\"versions:" + currInstName + "\"]",
            "[\"project_type:" + filterOption + "\"]"
        };

        if (filterOption != "datapack" && filterOption != "resourcepack")
        {
            facets.Add("[\"categories:fabric\"]");
        }

        string url = "https://api.modrinth.com/v2/search?query=" + searchQuery + "&facets=[" + String.Join(", ", facets) + "]";

        var request = (HttpWebRequest)WebRequest.Create(url);
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
    
    public List<Deps> GetModDeps(string modID, string versionID)
    {
        var request = (HttpWebRequest)WebRequest.Create("https://api.modrinth.com/v2/project/" + modID + "/version/" + versionID);
        using var response = (HttpWebResponse)request.GetResponse();
        using var reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<MetaInfo>(json).dependencies;
    }


    private string GetFilterOption()
    {
        List<Toggle> toggleButtons = new List<Toggle>
        {
            modButton.GetComponent<Toggle>(),
            modPacksButton.GetComponent<Toggle>(),
            resourcePacksButton.GetComponent<Toggle>(),
        };

        return (from button in toggleButtons where !button.interactable select button.name).FirstOrDefault();
    }

} 
