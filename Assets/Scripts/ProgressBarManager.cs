using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarManager : MonoBehaviour
{
    public Slider progressBar;
    public TextMeshProUGUI downloadText;
    
    void Update()
    {
        if (Application.platform != RuntimePlatform.Android) return;
        string currentFile = JNIStorage.apiClass.GetStatic<string>("currentDownload");

        if (!string.IsNullOrWhiteSpace(currentFile))
        {
            downloadText.gameObject.SetActive(true);
            progressBar.gameObject.SetActive(true);
            downloadText.text = "Downloading: " + currentFile;
            progressBar.value = (float)JNIStorage.apiClass.GetStatic<double>("downloadStatus");
        }
        else
        {
            downloadText.gameObject.SetActive(false);
            progressBar.gameObject.SetActive(false);
        }
    }
}
