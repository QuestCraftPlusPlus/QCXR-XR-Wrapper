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
        
        if (!JNIStorage.apiClass.CallStatic<bool>("isDownloadsCompleted"))
        {
            downloadText.gameObject.SetActive(true);
            progressBar.gameObject.SetActive(true);
            playButton.interactable = false;
            accountDropdown.interactable = false;
            downloadText.text = "Downloading files...";
            progressBar.value = JNIStorage.apiClass.CallStatic<float>("getDownloadPercentage");
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
