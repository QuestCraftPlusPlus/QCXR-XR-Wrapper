using System.IO;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;

public class LoadLog : MonoBehaviour
{
    public TMPro.TextMeshProUGUI linkbox;

    private class LogResponse
    {
        public string id;
    }
    
    public void UploadLog()
    {
        async Task Upload()
        {
            string logtext;
            try
            {
                logtext = File.ReadAllText(Application.persistentDataPath + "/latestlog.txt");
            }
            catch (FileNotFoundException)
            {
                linkbox.text = $"No log to upload!";
                throw;
            }
        
            WWWForm form = new WWWForm();
            form.AddField("content", logtext);

            using UnityWebRequest www = UnityWebRequest.Post("https://api.mclo.gs/1/log", form);
            www.SendWebRequest();

            while (!www.isDone)
                await Task.Delay(16);
            
            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError(www.error);
            else
            {
                string id = JsonUtility.FromJson<LogResponse>(www.downloadHandler.text).id;
                linkbox.text = $"https://mclo.gs/" + id;
            } 
        }
        Upload();
    }
}