using System.Collections;
using UnityEngine;
using System;

public class DownloadHandlerAndroid : DownloadHandler
{
    private Action onDownloadSuccess;
    private Action onDownloadFail;
    
    protected override void OnLoginSuccess()
    {
        Loom.QueueOnMainThread(() =>
        {
            if (AndroidBridge.instance.IsDownloadFinished)
            {
                AndroidBridge.instance.LoadInstance();
                EventManager.BrocastT(CommonDefine.EMEventType.DownloadSuccess);
            }
            else
            {
                Download(() =>
                {
                    AndroidBridge.instance.LoadInstance();
                    EventManager.BrocastT(CommonDefine.EMEventType.DownloadSuccess);
                }, null);
            }
        });
    }

    public override string GetDownloadingFile()
    {
        return AndroidBridge.instance.GetDownloadingFile;
    }

    public override double GetDownliadingStatus()
    {
        return AndroidBridge.instance.GetDownliadingStatus;
    }

    private void Download(Action onSuccess, Action onFail)
    {
        Debug.Log("YVRCraft --> start download");

        onDownloadSuccess = onSuccess;
        onDownloadFail = onFail;

        AndroidBridge.instance.StartDownload();

        GameLogicManager.instance.StartCoroutine(DownloadCheck());
    }

    private IEnumerator DownloadCheck()
    {
        yield return new WaitUntil(() => AndroidBridge.instance.IsDownloadFinished);

        Debug.Log("YVRCraft --> on download finish");
        onDownloadSuccess?.Invoke();
    }
}