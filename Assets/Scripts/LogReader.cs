using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogReader : MonoBehaviour
{
    public TextMeshProUGUI logText;
    public ScrollRect rect;
    public string logFilePath;
    private FileSystemWatcher fileWatcher;
    private string logContent;
    private bool updateLog = false;

    private void Start()
    {
        logFilePath = Application.persistentDataPath + "/latestlog.txt";

        if (!File.Exists(logFilePath))
        {
            File.Create(logFilePath).Close();
        }
        
        logContent = File.ReadAllText(logFilePath);
        UpdateLogText();

        fileWatcher = new FileSystemWatcher(Path.GetDirectoryName(logFilePath))
        {
            Filter = Path.GetFileName(logFilePath),
            NotifyFilter = NotifyFilters.LastWrite
        };

        fileWatcher.Changed += OnLogFileChanged;
        fileWatcher.EnableRaisingEvents = true;
    }

    private void OnLogFileChanged(object sender, FileSystemEventArgs e)
    {
        updateLog = true;
    }

    private void Update()
    {
        if (updateLog)
        {
            updateLog = false;
            StartCoroutine(ReadLogFile());
        }
    }

    private IEnumerator ReadLogFile()
    {
        yield return new WaitForEndOfFrame(); // Prevent IO conflicts
        logContent = File.ReadAllText(logFilePath);
        UpdateLogText();
    }

    private void UpdateLogText()
    {
        if (logText != null)
        {
            logText.text = logContent;
            Canvas.ForceUpdateCanvases();
            rect.verticalNormalizedPosition  = 0f;
        }
    }

    private void OnDestroy()
    {
        if (fileWatcher != null)
        {
            fileWatcher.Changed -= OnLogFileChanged;
            fileWatcher.Dispose();
        }
    }
}