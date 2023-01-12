# Entitlement Check

Entitlement check is used to check whether the current device user has purchased or owned your application.

To meet YVR publishing requirement, developer has to do the check within 10 seconds after the program starts. For more details about publishing requirements, please refer to [VRC Requirements](https://developer.yvrdream.com/yvrdoc/vrce/articles/Security1.html).

> [!Note]
> This check does not force users to be online while using the application. Even the device is offline, the check result is expected to be correct as long as the device has been online within 30 days.

## Perform the Entitlement Check

`GetViewerEntitled` API is used to check entitlement state, which is a async API: 

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
