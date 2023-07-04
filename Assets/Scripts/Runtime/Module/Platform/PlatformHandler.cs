using UnityEngine;
using YVR.Platform;
using System;

public class PlatformHandler
{
    protected Action onInitAndEntitledCheckSuccess;
    protected Action onInitAndEntitledCheckFail;

    public virtual void InitAndEntitledCheck(Action onSuccess, Action onFail)
    {
        onInitAndEntitledCheckSuccess = onSuccess;
        onInitAndEntitledCheckFail = onFail;
    }
}