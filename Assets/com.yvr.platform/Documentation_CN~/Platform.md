# 简介

YVR 平台的 SDk 旨在帮助开发者创建社交游戏体验。目前，它提供了以下功能：

-   [鉴权](./EntitlementCheck.md)：检查当前设备用户是否已经购买或拥有你的应用程序。
-   [账号与好友](./AccountAndFriends.md)：获取登录用户和好友信息。
-   [成就](./Achievement.md)：处理开发者在开发者中心仪表板上设置的成就。
-   [深度链接](./Deeplink.md)：深度链接允许用户加入多人游戏会话。

## 入门

在使用任何平台功能之前，开发者首先需要初始化 Platform SDK：

```csharp
YVR.Platform.YVRPlatform.Initialize(<appId>);

if (YVRPlatform.IsInitialized)
    Debug.Log("Platform SDK is Successfully initialized.");
else
    Debug.LogError("Platform SDK initialize failed.");
```

> [!NOTE]
> 获得一个 AppID 之前，你需要先在我们的[开发者平台](https://developer.yvrdream.com/yvrdvcenter/)上注册为开发者，然后创建你的应用程序。
