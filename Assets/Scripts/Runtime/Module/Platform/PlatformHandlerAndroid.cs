using UnityEngine;
using YVR.Platform;
using System;

public class PlatformHandlerAndroid : PlatformHandler
{
    public override void InitAndEntitledCheck(Action onSuccess, Action onFail)
    {
        base.InitAndEntitledCheck(onSuccess, onFail);
        
        YVRPlatform.Initialize();

        if (!YVRPlatform.IsInitialized)
        {
            Debug.LogError($"YVRCraft --> platform failed to initialize");
            onInitAndEntitledCheckFail?.Invoke();
            return;
        }

        PlatformCore.GetViewerEntitled().OnComplete(GetViewerEntitledCallback);
    }
    
    private void GetViewerEntitledCallback(YVRMessage<Entitlement> msg)
    {
        if (msg.isError)
        {
            Debug.LogError("YVRCraft --> curr msg is error!");
            onInitAndEntitledCheckFail?.Invoke();
            return;
        }
        
        Debug.Log("YVRCraft --> is entitled ? " + msg.data.isEntitled);

#if ENTITLEMENT_PASS
        onInitAndEntitledCheckSuccess?.Invoke();
        return;
#endif
        
        if (!msg.data.isEntitled)
            onInitAndEntitledCheckFail?.Invoke();
        else
            onInitAndEntitledCheckSuccess?.Invoke();
    }
}
