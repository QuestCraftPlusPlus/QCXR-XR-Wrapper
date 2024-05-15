using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ThreeDISevenZeroR.UnityGifDecoder;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GifLoader : MonoBehaviour
{

    private List<Texture> frames = new List<Texture>();
    private List<float> frameDelays = new List<float>();
    
    private float time = 0;
    private int index = 0;

    RawImage img;

    public string DEBUGURL;

    [ContextMenu("DEBUGFETCH")]
    void loady()
    {
        Debug.Log("Downloading gif at url: \n" + DEBUGURL);
        UnityWebRequest modImageLink = UnityWebRequest.Get(DEBUGURL);
        modImageLink.SendWebRequest();

        while (!modImageLink.isDone)
            Task.Delay(16);
        if (modImageLink.result != UnityWebRequest.Result.Success)
            Debug.Log(modImageLink.error);
        else
            LoadGifImage(modImageLink.downloadHandler.data);
    }
    
    public Texture2D LoadGifImage(byte[] bytes)
    {
        img = GetComponent<RawImage>();
        DecodeGif(bytes);
        return (Texture2D)img.texture;
    }

    private void DecodeGif(byte[] bytes)
    {
        var ms = new MemoryStream(bytes);
        
        frames = new List<Texture>();
        frameDelays = new List<float>();
        
        index = 0;

        using (var gifStream = new GifStream(ms))
        {
            while (gifStream.HasMoreData)
            {
                switch (gifStream.CurrentToken)
                {
                    case GifStream.Token.Image:
                        var image = gifStream.ReadImage();
                        var frame = new Texture2D(
                            gifStream.Header.width, 
                            gifStream.Header.height, 
                            TextureFormat.ARGB32, false); 

                        frame.SetPixels32(image.colors);
                        frame.Apply();

                        frames.Add(frame);
                        frameDelays.Add(image.SafeDelaySeconds);
                        
                        break;
                            
                    case GifStream.Token.Comment:
                        var comment = gifStream.ReadComment();
                        break;
        
                    default:
                        gifStream.SkipToken();
                        break;
                }
            }
        }
    }

    private void Update()
    {
        if (frames.Count > 0)
        {
            time += Time.deltaTime;
            if (time > frameDelays[index])
            {
                time = 0;
                index = index + 1 >= frameDelays.Count ? 0 : index + 1;
            }

            img.texture = frames[index];
        }
    }
}
