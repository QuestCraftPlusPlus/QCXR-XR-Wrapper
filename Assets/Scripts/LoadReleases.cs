using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoadReleases : MonoBehaviour
{
    public TextMeshProUGUI tags;
    public ScrollRect changelog;
    public VersionChecker versionChecker;
    
    private List<ReleaseInfo> _releases;
    
    public class ReleaseInfo
    {
        public string tag_name;
        public string name;
        public string published_at;
        public string body;
    }

    private bool cooldown;

    private void Start() => LoadChangeLog();

    [ContextMenu("Generate Releases")] 
    public void LoadChangeLog()
    {
        if (cooldown)
            return;
        cooldown = true;
        
        async Task AwaitCooldown()
        {
            await Task.Delay(10000);
            cooldown = false;
        }
        AwaitCooldown();
        
        async Task PopulateReleases()
        {
            UnityWebRequest www = UnityWebRequest.Get("https://api.github.com/repos/QuestCraftPlusPlus/Questcraft/releases");
            www.SetRequestHeader("User-Agent", "QuestCraftPlusPlus/QuestCraft/" + Application.version + " (discord.gg/questcraft)");
            www.SendWebRequest();
            
            while (!www.isDone)
                await Task.Delay(16);
            
            if (www.result != UnityWebRequest.Result.Success)
                Debug.Log(www.error);
            else
            {
                string changelogs = null;
                _releases = JsonConvert.DeserializeObject<List<ReleaseInfo>>(www.downloadHandler.text);

                versionChecker.currentPublicVersion = _releases[0].tag_name;
                versionChecker.CheckVersion();

                foreach (var release in _releases)
                    changelogs +=
                        $"<size=75%>{release.published_at.Split('T')[0]}</size>\n" +
                        $"<size=120%><u>{release.name}</u></size>" +
                        $"<line-height=10>\n</line-height>\n" +
                        $"{release.body}\n\n\n\n";

                tags.text = changelogs;
            }
        }
        PopulateReleases();
    }

    public void ScrollTop() => StartCoroutine(ScrollToTop());
    
    IEnumerator ScrollToTop()
    {
            yield return new WaitForEndOfFrame();
            changelog.verticalNormalizedPosition = 1f;
    }
}
