using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using YVR.Core;

public class GameLogicManager : MonoBehaviorSingleton<GameLogicManager>
{
    public PlatformHandler platformHandler;
    public LoginHandler loginHandler;
    public DownloadHandler downloadHandler;
    public AudioSource audioSource;

    protected override void Init()
    {
        base.Init();

        AndroidBridge.instance.Init();

        platformHandler = Application.platform == RuntimePlatform.Android ? new PlatformHandlerAndroid() : new PlatformHandlerEditor();
        loginHandler = Application.platform == RuntimePlatform.Android ? new LoginHandlerAndroid() : new LoginHandlerEditor();
        downloadHandler = Application.platform == RuntimePlatform.Android ? new DownloadHandlerAndroid() : new DownloadHandlerEditor();

        EventManager.AddListenerT(CommonDefine.EMEventType.StartGameButtonClicked, OnClickStartGameButton);

        YVRManager.instance.eventsManager.onFocusGained += OnFocusGained;
        YVRManager.instance.eventsManager.onFocusLost += OnFocusLost;

        YVRManager.instance.cpuLevel = 15;
        YVRManager.instance.gpuLevel = 5;

        platformHandler.InitAndEntitledCheck(OnInitAndEntitledCheckSuccess, OnInitAndEntitledCheckFail);
    }

    private void OnInitAndEntitledCheckSuccess()
    {
        EventManager.BrocastT(CommonDefine.EMEventType.PlatformCheckSuccess);
    }

    private void OnInitAndEntitledCheckFail()
    {
        Application.Quit();
    }

    private void OnClickStartGameButton()
    {
        Debug.Log("YVRCraft --> destroy xr instance");
        audioSource.Stop();
        var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
        foreach (var xrDisplay in xrDisplaySubsystems)
            xrDisplay.Destroy();
        
        AndroidBridge.instance.StartGame();

        Application.Unload();
    }

    private void OnFocusGained()
    {
        Debug.Log("YVRCraft -->  on focus gained");
    }
    
    private void OnFocusLost()
    {
        Debug.Log("YVRCraft -->  on focus lost");
    }
}