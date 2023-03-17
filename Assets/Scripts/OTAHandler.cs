using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class OTAHandler : MonoBehaviour
{
    /*public GameObject releaseInfoJson;
    
    void Start()
    {
        APIParser ri = OTAHandler.GetLatestVersion();

        /*if (ri.version != currentVersion)
        {
            new DownloadHandlerFile("test");
        }#1#
    }

    public static APIParser GetLatestVersion()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://apihere.com/release_info/");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        Debug.Log(json);

        return JsonUtility.FromJson<APIParser>(json);
    }*/
}
