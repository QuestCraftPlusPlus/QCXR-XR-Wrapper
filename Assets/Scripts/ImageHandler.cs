using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ImageHandler : MonoBehaviour
{
    public GameObject pfpHolder;
    public string pfpUrl = JNIStorage.apiClass.GetStatic<string>("profileImage");

    public void GetPfp()
    {
        if (JNIStorage.apiClass.GetStatic<bool>("finishedDownloading"))
        {
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
                pfpHolder.GetComponent<RawImage>().texture = pfpTexture;
            }
        }
    }
}
