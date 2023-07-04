using UnityEngine;

public class DownloadHandlerEditor : DownloadHandler
{
    public override string GetDownloadingFile()
    {
        return "client.jar";
    }

    public override double GetDownliadingStatus()
    {
        return 100;
    }

    protected override void OnLoginSuccess()
    {
        float downloadTime = Random.Range(1f, 10f);
        
        Debug.Log($"YVRCraft --> in fake downloading: duration: {downloadTime}");
        
        Loom.QueueOnMainThread(() =>
        {
            EventManager.BrocastT(CommonDefine.EMEventType.DownloadSuccess);
        }, downloadTime);
    }
}
