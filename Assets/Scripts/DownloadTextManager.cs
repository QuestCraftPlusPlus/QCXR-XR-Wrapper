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
        string currentFile = JNIStorage.apiClass.GetStatic<string>("currentDownload");
        double mbDowbloaded = TruncateDouble(JNIStorage.apiClass.GetStatic<double>("downloadStatus"), 3);

        if (!string.IsNullOrWhiteSpace(currentFile))
            GetComponent<TextMeshProUGUI>().text = "Downloading " + currentFile + ": " + mbDowbloaded + " MB";
        else 
            GetComponent<TextMeshProUGUI>().text = "";

    }

    public double TruncateDouble(double value, int precision)
    {
        double step = (double)Math.Pow(10, precision);
        double tmp = Math.Truncate(step * value);
        return tmp / step;
    }
}