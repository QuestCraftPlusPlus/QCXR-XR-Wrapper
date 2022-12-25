using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ImageHandler : MonoBehaviour
{
    string pfpUrl;

    public void Update()
    {
        if (JNIStorage.accountObj != null)
        {
            if (pfpUrl == null)
            {
                pfpUrl = JNIStorage.apiClass.GetStatic<string>("profileImage");
            }
            StartCoroutine(GetTexture());
        }
    }

    IEnumerator GetTexture()
    {
        using (UnityWebRequest pfp = UnityWebRequestTexture.GetTexture(pfpUrl))
        {
            yield return pfp.SendWebRequest();

            if (pfp.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(pfp.error);
            }
            else
            {
                // Get downloaded asset bundle
                var pfpTexture = DownloadHandlerTexture.GetContent(pfp);
                gameObject.GetComponent<RawImage>().texture = pfpTexture;
            }
        }
    }
}
