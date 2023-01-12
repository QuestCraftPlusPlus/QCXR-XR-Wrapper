# Introduction

The YVR Platform SDK aims to help developers create a social game experience. For now, it provides the following features:

-   [Entitlement Check](./EntitlementCheck.md): Check used to check whether the current device user has purchased or owned your application.
-   [Account And Friend](./AccountAndFriends.md): Get logged user and users' friend information.
-   [Achievement](./Achievement.md): Handle achievements which set by developers on Developer center dashboard.
-   [Deep Link](./DeepLink.md): Deeplink allows users to join multiplayer session.

## Get Started

Before using any platform features, developers have to initialize Platform SDK firstly:

```csharp
YVR.Platform.YVRPlatform.Initialize(<appId>);

if (YVRPlatform.IsInitialized)
    Debug.Log("Platform SDK is Successfully initialized.");
else
    Debug.LogError("Platform SDK initialize failed.");
```

> [!Note]
> In order to get an AppID, you need to first register as a developer on our [developer platform](https://developer.yvrdream.com/yvrdvcenter/) and then create your application.
