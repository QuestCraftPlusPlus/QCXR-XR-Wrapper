using UnityEngine;
using UnityEngine.Android;

public class AndroidBridge : Singleton<AndroidBridge>
{
    private AndroidJavaClass m_ApiClass;
    private AndroidJavaObject m_AccountObj;
    private AndroidJavaObject m_Activity;
    private AndroidJavaObject m_Instance1182;
    private AndroidJavaObject m_Instance1193;
    private string m_Directory;
    private string m_CurrentInstance = "1.19.3-fabric";

    public bool isLogin => m_AccountObj != null;

    public void Init()
    {
        if (Application.isEditor)
            return;

        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            Permission.RequestUserPermission(Permission.Microphone);
        m_ApiClass = new AndroidJavaClass("pojlib.api.API_V1");
        AndroidJavaClass constants = new AndroidJavaClass("pojlib.util.Constants");
        
        m_Directory = constants.GetStatic<string>("MC_DIR");

        AndroidJavaClass instance = new AndroidJavaClass("pojlib.instance.MinecraftInstance");
        m_Activity = instance.GetStatic<AndroidJavaObject>("context");
    }

    #region Login

    public bool Login()
    {
        m_AccountObj = m_ApiClass.CallStatic<AndroidJavaObject>("login", "d17a73a2-707c-40f5-8c90-d3eda0956f10");

        return m_AccountObj != null;
    }

    public string GetLoginText => m_ApiClass.GetStatic<string>("msaMessage")
        .Replace("To sign in, use a web browser to open the page", "请在手机或电脑上登录网址：")
        .Replace("and enter the code", "并输入代码").Replace("to authenticate.", "  以验证账号。");

    public bool Logout()
    {
        return m_ApiClass.CallStatic<bool>("logout", $"{m_Directory}/accounts");
    }

    #endregion

    #region Download

    public bool IsDownloadFinished => m_ApiClass.GetStatic<bool>("finishedDownloading");

    public string GetDownloadingFile => m_ApiClass.GetStatic<string>("currentDownload");

    public double GetDownliadingStatus => m_ApiClass.GetStatic<double>("downloadStatus");

    public void LoadInstance()
    {
        m_Instance1182 = m_ApiClass.CallStatic<AndroidJavaObject>("load", "1.18.2-fabric", m_Directory);
        m_Instance1193 = m_ApiClass.CallStatic<AndroidJavaObject>("load", "1.19.3-fabric", m_Directory);

        Debug.Log($"YVRCraft --> instance is null ? 1182: {m_Instance1182 == null}  1193: {m_Instance1193 == null}");
    }

    public void StartDownload()
    {
        AndroidJavaClass modloaderEnum = new AndroidJavaClass("pojlib.api.API_V1$ModLoader");
        AndroidJavaObject fabricModloader = modloaderEnum.GetStatic<AndroidJavaObject>("Fabric");
        AndroidJavaObject[] versions = m_ApiClass.CallStatic<AndroidJavaObject[]>("getMinecraftVersions");
        foreach (AndroidJavaObject obj in versions)
        {
            if (obj.Get<string>("id") == "1.19.3")
            {
                m_ApiClass.CallStatic<AndroidJavaObject>("createNewInstance", m_Activity,
                    "1.19.3-fabric", m_Directory, obj, fabricModloader);
            }
            
            if (obj.Get<string>("id") == "1.18.2")
            {
                m_ApiClass.CallStatic<AndroidJavaObject>("createNewInstance", m_Activity,
                    "1.18.2-fabric", m_Directory, obj, fabricModloader);
            }
        }
    }

    #endregion

    public string GetProfileImgUrl => m_ApiClass.GetStatic<string>("profileImage");

    public string GetCurrentInstance => m_CurrentInstance;

    public string SwitchInstance()
    {
        switch (m_CurrentInstance)
        {
            case "1.18.2-fabric":
                m_CurrentInstance = "1.19.3-fabric";
                break;
            case "1.19.3-fabric":
                m_CurrentInstance = "1.18.2-fabric";
                break;
        }

        return m_CurrentInstance;
    }

    public void StartGame()
    {
        AndroidJavaObject currentInstance;
        switch (m_CurrentInstance)
        {
            case "1.18.2-fabric":
                currentInstance = m_Instance1182;
                break;
            case "1.19.3-fabric":
                currentInstance = m_Instance1193;
                break;
            default:
                currentInstance = m_Instance1193;
                break;
        }

        Debug.Log($"YVRCraft --> start game: instance: {m_CurrentInstance}  instance is null ? {currentInstance == null}");

        m_ApiClass.CallStatic("launchInstance", m_Activity, m_AccountObj, currentInstance);
    }
}