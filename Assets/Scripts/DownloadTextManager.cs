using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class DownloadTextManager : MonoBehaviour
{
#if !UNITY_EDITOR
	TextMeshProUGUI text;
	void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
	}
	void FixedUpdate()
	{
		string currentFile = JNIStorage.apiClass.GetStatic<string>("currentDownload");
		double mbDownloaded = Math.Round(JNIStorage.apiClass.GetStatic<double>("downloadStatus"), 2);
		double mbTotal = Math.Round(JNIStorage.apiClass.GetStatic<double>("downloadStatusTotal"), 2);

		int hasDownloaded = JNIStorage.apiClass.GetStatic<int>("hasDownloaded");
		int isDownloading = JNIStorage.apiClass.GetStatic<int>("isDownloading");
		string downloadStep = JNIStorage.apiClass.GetStatic<string>("downloadStep");

		if (!string.IsNullOrWhiteSpace(currentFile))
			text.text = $"Downloading {currentFile}: {mbDownloaded}MB/{mbTotal}MB";
		else 
			text.text = "";

		if(isDownloading > 0)
		{
			JNIStorage.instance.progressBar.fillAmount = Mathf.Lerp(JNIStorage.instance.progressBar.fillAmount, (float)hasDownloaded / (float)isDownloading, 0.1f);
			JNIStorage.instance.progressBarText.text = $"{hasDownloaded}/{isDownloading} [{downloadStep}]";
		}
		else
		{
			JNIStorage.instance.progressBar.fillAmount = 0;
			JNIStorage.instance.progressBarText.text = "";
		}
	}
#endif
}
