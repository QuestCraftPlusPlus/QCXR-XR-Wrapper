using System;
using UnityEngine;
using UnityEngine.UI;

public class DownloadPanel : MonoBehaviour
{
    [SerializeField] private Text m_TxtDownloadName;
    [SerializeField] private Text m_TxtDownloadProgress;

    private string downloadingFile;

    void Update()
    {
        string currentFile = GameLogicManager.instance.downloadHandler.GetDownloadingFile();
        double mbDowbloaded = TruncateDouble(GameLogicManager.instance.downloadHandler.GetDownliadingStatus(), 3);

        if (currentFile != downloadingFile)
            Debug.Log($"YVRCraft --> downloading file: {currentFile}");

        downloadingFile = currentFile;

        m_TxtDownloadName.text = currentFile;
        m_TxtDownloadProgress.text = $"{mbDowbloaded} MB";
    }

    private double TruncateDouble(double value, int precision)
    {
        double step = Math.Pow(10, precision);
        double tmp = Math.Truncate(step * value);
        return tmp / step;
    }
}