using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public RawImage pfpHolder;
    public TextMeshProUGUI profileNameHolder;
    public TextMeshProUGUI minuteHourText;
    public TextMeshProUGUI secondText;
    string pfpUrl;
    string profileName;

    void Update()
    {
        if (JNIStorage.accountObj != null)
        {
            if (pfpUrl == null)
            {
                pfpUrl = JNIStorage.apiClass.GetStatic<string>("profileImage");
            }

            if (profileName == null)
            {
                profileName = JNIStorage.apiClass.GetStatic<string>("profileName");
            }

            if (pfpHolder.texture == null)
            {
                GetTexturePlusName();
            }
        }
        
        if (System.DateTime.Now.Minute.ToString().Length == 1)
        {
            string hour = System.DateTime.Now.Hour.ToString();
            string minute = System.DateTime.Now.Minute.ToString().Insert(0, "0");
            minuteHourText.text = hour + ":" + minute;
        }
        else
        {
            string hour = System.DateTime.Now.Hour.ToString();
            string minute = System.DateTime.Now.Minute.ToString();
            minuteHourText.text = hour + ":" + minute;
        }
    }

    async Task GetTexturePlusName()
    {
        using UnityWebRequest pfp = UnityWebRequestTexture.GetTexture(pfpUrl);
        pfp.SetRequestHeader("User-Agent", "QuestCraft");
        var requestTask = pfp.SendWebRequest();

        while (!requestTask.isDone)
        {
            await Task.Yield();
        }

        if (pfp.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(pfp.error);
        }
        else
        {
            // Get downloaded texture
            var pfpTexture = DownloadHandlerTexture.GetContent(pfp);
            pfpHolder.texture = pfpTexture;
            profileNameHolder.text = profileName;
        }
    }
}
