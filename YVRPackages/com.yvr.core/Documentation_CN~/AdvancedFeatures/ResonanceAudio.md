# Resonance Audio

这是一份使用 Resonance Audio 开发 Unity 项目的指南。 更多详情，请参考 [Resonance Audio Unity's documentation](https://resonance-audio.github.io/resonance-audio/develop/unity/getting-started.html)。


## 创建新的 Unity 项目并导入 SDK

1. 打开 Unity Hub 并创建一个 3D 项目。 

2. 选择 `Assets -> Import Package -> Custom Package`。

3. 选择已下载的 [ResonanceAudioForUnity_*.unitypackage](https://github.com/resonance-audio/resonance-audio-unity-sdk/releases) 文件。

4. 在 `Importing Package` 中，点击 `Import`。如有提示，接受任何 API 升级。


## 配置 Unity 项目以使用 Resonance Audio

1. 通过 `Edit -> Project Settings -> Audio` 打开 AudioManager 设置。

2. 选择 `Resonance Audio` 作为 `Spatializer Plugin`。

3. 选择 `Resonance Audio` 作为 `Ambisonic Decoder Plugin`。