using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class LoadReleases : MonoBehaviour
{
    public TextMeshProUGUI tags;

    public class ReleaseInfo
    {
        public string tag_name;
        public string name;
        public string published_at;
        public string body;
    }

    private List<ReleaseInfo> releases;


    [ContextMenu("Generate Releases")]
    private void Start()
    {
        StartCoroutine(PopulateReleases());
    }
    
    IEnumerator PopulateReleases()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://api.github.com/repos/QuestCraftPlusPlus/Questcraft/releases");
        yield return www.SendWebRequest();
            
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string changelogs = null;
            releases = JsonConvert.DeserializeObject<List<ReleaseInfo>>(www.downloadHandler.text);
            foreach (var release in releases)
                changelogs += $"{release.published_at}\n{release.name}\n{release.body}\n\n\n";

            tags.text = changelogs;
        }
    }
}
