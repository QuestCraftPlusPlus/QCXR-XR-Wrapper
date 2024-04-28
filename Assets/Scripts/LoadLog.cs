using System.Collections;
using System.IO;
using UnityEngine.Networking;
using UnityEngine;

public class LoadLog : MonoBehaviour
{
    public TMPro.TextMeshProUGUI linkbox;
    public TMPro.TextMeshProUGUI logbox;

    private class LogResponse
    {
        public string id;
    }

    public void UploadLog()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        string logtext;
        try
        {
            logtext = File.ReadAllText(Application.persistentDataPath + "/latestlog.txt");
        }
        catch (FileNotFoundException e)
        {
            linkbox.text = $"Latest log link:\nNo log to upload!";
            throw;
        }
        
        WWWForm form = new WWWForm();
        form.AddField("content", logtext);

        using UnityWebRequest www = UnityWebRequest.Post("https://api.mclo.gs/1/log", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
            Debug.LogError(www.error);
        else
        {
            linkbox.text = $"Latest log link:\nhttps://mclo.gs/" + 
                           JsonUtility.FromJson<LogResponse>(www.downloadHandler.text).id;
        }
    }

    //view log has an effect on performance, i disabled for rn
    public void ViewLog()
    {
        try
        {
            logbox.text = "Log:\n" + File.ReadAllText(Application.persistentDataPath + "/latestlog.txt");
        }
        catch (FileNotFoundException)
        {
            logbox.text  = "Log:\nNo log file!";
        }
    }
}