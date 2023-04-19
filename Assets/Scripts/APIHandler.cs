using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class APIHandler : MonoBehaviour
{
    public string searchQuery;
    public string modID;
    
    public SearchParser GetSearchedMods()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.modrinth.com/v2/search?query=" + searchQuery);
        using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        using StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<SearchParser>(json);
    }

    public SearchParser GetModInfo()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.modrinth.com/v2/project/" + modID);
        using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        using StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<SearchParser>(json);
    }
    
    public SearchParser GetModDownloads()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.modrinth.com/v2/project/" + modID + "/version");
        using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        using StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<SearchParser>(json);
    }
}
