using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarManager : MonoBehaviour
{
    public Slider progressBar;
    public TextMeshProUGUI downloadText;
    public Button playButton;
    public TMP_Dropdown accountDropdown;
    
    public static bool started = false;
    
    void Update()
    {
        if (Application.platform != RuntimePlatform.Android || started) return;
        string currentFile = JNIStorage.apiClass.GetStatic<string>("currentDownload");
        
        if (!JNIStorage.apiClass.GetStatic<bool>("finishedDownloading"))
        {
            downloadText.gameObject.SetActive(true);
            progressBar.gameObject.SetActive(true);
            playButton.interactable = false;
            accountDropdown.interactable = false;
            downloadText.text = "Downloading: " + currentFile;
            progressBar.value = (float)JNIStorage.apiClass.GetStatic<double>("downloadStatus");
        }
        else
        {
            downloadText.gameObject.SetActive(false);
            progressBar.gameObject.SetActive(false);
            playButton.interactable = true;
            accountDropdown.interactable = true;
        }
    }
}
