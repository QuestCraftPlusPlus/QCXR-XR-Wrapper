using UnityEngine;
using System;

public class PlatformHandlerEditor : PlatformHandler
{
    public override void InitAndEntitledCheck(Action onSuccess, Action onFail)
    {
        base.InitAndEntitledCheck(onSuccess, onFail);
        
        bool result = UnityEngine.Random.Range(0, 2) == 1;
        
        Debug.Log($"YVRCraft --> fake entitle check result: {result}");
        
        if (result)
            onInitAndEntitledCheckSuccess?.Invoke();
        else
            onInitAndEntitledCheckFail?.Invoke();
    }
}
