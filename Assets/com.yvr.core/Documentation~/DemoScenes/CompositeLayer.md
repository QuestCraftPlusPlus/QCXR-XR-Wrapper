# [DebugScene](../DemoScenes.md).Composite Layer

*Located in YVR/Scenes/CompositeLayer/CompositeLayer.scene*

* SeeAlso :
  * [YVRCompositeLayer](xref:YVR.Core.YVRCompositeLayer)

---

CompositeLayer scene is used to illustrate composite layer effect. Composite layer is designed to improve the clarity of flat UI. In traditional rendering pipeline, UI texture has to be firstly rendered into Unity managed color buffer, then the Unity managed color buffer will be rendered into screen buffer. While using composite layer, the target UI texture can be directly rendered by native. Therefore, the distortion caused by sampling can be reduced.

The screenshot while CompositeLayer scene running on YVR device is as follows:
![Composite Layer](./CompositeLayer/2022-04-19-13-51-11.png)

> [!Caution]
> As composite layer is rendered by YVR device native system, the content cannot be displayed in Unity Editor.

The lower part is the UI rendered by Unity eye buffer, and the upper part is rendered using composite layer. And the left part is dynamic texture while right part is static texture.

The detail difference between rendering using composite (upper) and Unity eye buffer (lower) is shown as following:
![Detail Difference](./CompositeLayer/2022-04-19-13-56-47.png)

> [!Caution]
> Some color dispersion can be seen in the detailed image, which is generated to cancel out the dispersion caused by lens. So all the color dispersion will not be seen while using devices.

## Underlay vs Overlay

As native system is required to composite several layers, it needs to know the order of all layers. The order is determined by the depth of the composite layer, and the depth of Unity eye buffer is set to zero. And all layers with depth greater than 0 are called overlay while layers with depth less than 0 are called underlays.

Underlays will be drawn firstly, then the Unity eye buffer, and finally the overlays. Thus, Unity eye buffer will cover the underlays, and the overlays will cover the eye buffer.

In order to make underlay to be displayed normally instead of being completely covered, a hole needs to be made in Unity eye buffer. The shader `YVR/UnderlayPuncher` is designed to generate such hole: 
![Underlay Puncher](./CompositeLayer/2022-04-19-14-58-37.png)

> [!Tip]
> The upper-left layer in demo scene is Underlay.