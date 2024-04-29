using System.Collections;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SkinHandler : MonoBehaviour
{
    public TextMeshProUGUI usernamebox;
    public Material skin;
    private string skinType;

    public GameObject rSlim;
    public GameObject rClassic;
    public GameObject lSlim;
    public GameObject lClassic;

    private void Start()
    {
        previousText = usernamebox.text;
        StartCoroutine(CheckTextChange());
    }
    
    public void LoadSkin(string username)
    {
        StartCoroutine(LoadImage(username));
    }

    IEnumerator LoadImage(string username)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture("https://minotar.net/skin/" + username);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
            Debug.Log(request.error);
        else
        {
            Debug.Log("Loading skin");
            skin.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            skin.mainTexture.filterMode = FilterMode.Point;
            
            StartCoroutine(SetSkinType(username));
        }
    }
    
    public IEnumerator SetSkinType(string username)
    {
        UnityWebRequest www =
            UnityWebRequest.Get("https://starlightskins.lunareclipse.studio/info/user/" + username);
        
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);
        else
        {
            JObject SkinType = JObject.Parse(www.downloadHandler.text);
            skinType = (string)SkinType["skinType"];
            Debug.Log(skinType);
        }
        
        if (skinType != "wide")
        {
            rSlim.SetActive(true);
            lSlim.SetActive(true);

            rClassic.SetActive(false);
            lClassic.SetActive(false);
        }
        else
        {
            rSlim.SetActive(false);
            lSlim.SetActive(false);

            rClassic.SetActive(true);
            lClassic.SetActive(true);
        }
    }

    private string previousText;
    IEnumerator CheckTextChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            if (previousText != usernamebox.text)
            {   
                previousText = usernamebox.text;
                LoadSkin(previousText);
            }
        }
    }
}
