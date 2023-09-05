using System.IO;
using System.Net;
using Newtonsoft.Json;
using UnityEngine;


public class APIHandler : MonoBehaviour
{
    public string searchQuery;

    public SearchParser GetSearchedMods()
    {
        var request = (HttpWebRequest)WebRequest.Create("https://api.modrinth.com/v2/search?query=" + searchQuery + "&filters=categories=fabric");
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
        var request = (HttpWebRequest)WebRequest.Create("https://api.modrinth.com/v2/project/" + modID + "/dependencies");
        using var response = (HttpWebResponse)request.GetResponse();
        using var reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<Deps>(json);
    }
}
