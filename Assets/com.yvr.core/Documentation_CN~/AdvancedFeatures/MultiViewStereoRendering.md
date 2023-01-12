# 多视图立体渲染

> [!TIP]
> 也被称为单通道立体渲染。

YVR 设备支持单通道立体渲染 (`Single Pass Stereo Rendering`)，这减少了 CPU 的负荷，而 CPU 的负荷来自于绘图调用。

在典型的立体渲染中，双眼的缓冲区必须依次进行渲染，这就导致了双重绘制调用。多视图立体渲染依赖于 OpenGL 中的多视图渲染 `MultiView Rendering` 功能，它允许绘制调用同时渲染阵列纹理的几个层。因此，左眼和右眼缓冲区的内容都可以在一次绘制调用中被绘制出来，而且顶点着色器可以知道它正在写入哪一层，所以每个眼睛缓冲区的渲染结果仍然是不同的，这保证了立体效果。

在 Unity 中用 YVR SDK 打开单通道需要两个步骤：
1. 在 YVR XR Plugin-in 配置窗口中，选择 `Multiview` 作为 Stereo Rendering Mode。
    ![启用 Multi View](MultiViewStereoRendering/2021-09-16-14-32-23.png)

> [!CAUTION]
> 单程立体渲染需要着色器的支持，因此你需要修改你自己的自定义着色器，以支持单程立体渲染。更多信息，请参考相关的 [Unity 文档](https://docs.unity3d.com/Manual/SinglePassInstancing.html)。