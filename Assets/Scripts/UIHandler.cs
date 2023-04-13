using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public RawImage pfpHolder;
    public TextMeshProUGUI profileNameHolder;
    string pfpUrl;
    string profileName;

    public void Update()
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

            GetTexturePlusName();
        }
    }

    async Task GetTexturePlusName()
    {
        using (UnityWebRequest pfp = UnityWebRequestTexture.GetTexture(pfpUrl))
        {
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
}
