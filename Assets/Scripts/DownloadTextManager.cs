using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class DownloadTextManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
            return;
        string currentFile = JNIStorage.apiClass.GetStatic<string>("currentDownload");
        double mbDownloaded = TruncateDouble(JNIStorage.apiClass.GetStatic<double>("downloadStatus"), 3);

        if (!string.IsNullOrWhiteSpace(currentFile))
            GetComponent<TextMeshProUGUI>().text = "Downloading " + currentFile + ": " + mbDownloaded + " MB";
        else 
            GetComponent<TextMeshProUGUI>().text = "";
    
    }

    private double TruncateDouble(double value, int precision)
    {
        double step = Math.Pow(10, precision);
        double tmp = Math.Truncate(step * value);
        return tmp / step;
    }
}
