# 鉴权

鉴权是用来检查当前设备用户是否已购买或拥有你的应用程序。

为了满足 YVR 的发布要求，开发者必须在程序启动后 10 秒内进行检查。关于发布要求的更多细节，请参考  [VRC Requirements](https://developer.yvrdream.com/yvrdoc/vrce/articles/Security1.html)。

> [!NOTE]
> 这个检查并不强迫用户在使用该应用程序时必须在线。即使设备处于离线状态，只要设备在 30 天内处于在线的状态，检查结果也是正确的。

## 执行鉴权

`GetViewerEntitled` 是一个异步接口，用于检查鉴权状态：

```csharp
YVR.Platform.PlatformCore.GetViewerEntitled().OnComplete(GetViewerEntitledCallback);

private void GetViewerEntitledCallback(YVR.Platform.YVRMessage<YVR.Platform.Entitlement> msg)
{
    if (msg.isError || !msg.data.isEntitled)
    {
        // Entitlement check failed. Quit app.
    }
}
```
