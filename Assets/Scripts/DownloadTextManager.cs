using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class DownloadTextManager : MonoBehaviour
{
#if !UNITY_EDITOR
	void Update()
    {
		string currentFile = JNIStorage.apiClass.GetStatic<string>("currentDownload");
		double mbDownloaded = Math.Round(JNIStorage.apiClass.GetStatic<double>("downloadStatus"), 3);

		if (!string.IsNullOrWhiteSpace(currentFile))
			GetComponent<TextMeshProUGUI>().text = "Downloading " + currentFile + ": " + mbDownloaded + " MB";
		else 
			GetComponent<TextMeshProUGUI>().text = "";
    }
#endif
}
