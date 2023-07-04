public class DownloadHandler
{
    public DownloadHandler()
    {
        EventManager.AddListenerT(CommonDefine.EMEventType.LoginSuccess, OnLoginSuccess);
    }

    public virtual string GetDownloadingFile()
    {
        return "null";
    }

    public virtual double GetDownliadingStatus()
    {
        return 1.00;
    }

    protected virtual void OnLoginSuccess()
    {
    }
}