using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;

public class LoadReleases : MonoBehaviour
{
    public TextMeshProUGUI tags;
    public ScrollRect Changelog;
    public VersionChecker _versionChecker;
    
    private List<ReleaseInfo> releases;
    
    public class ReleaseInfo
    {
        public string tag_name;
        public string name;
        public string published_at;
        public string body;
    }

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
            Debug.Log(www.error);
        else
        {
            string changelogs = null;
            releases = JsonConvert.DeserializeObject<List<ReleaseInfo>>(www.downloadHandler.text);

            _versionChecker.currentPublicVersion = releases[0].tag_name;
            _versionChecker.CheckVersion();
            
            foreach (var release in releases)
                changelogs += 
                    $"<size=75%>{release.published_at.Split('T')[0]}</size>\n" +
                    $"<size=120%><u>{release.name}</u></size>" +
                    $"<line-height=10>\n</line-height>\n" +
                    $"{release.body}\n\n\n\n";

            tags.text = changelogs;
            yield return new WaitForEndOfFrame();
            Changelog.verticalNormalizedPosition = 1f;      
        }
    }
}
