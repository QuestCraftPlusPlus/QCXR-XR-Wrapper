# Controllers

`YVRInput` and `YVRControllerRig` are the two entrances to get the controller data. `YVRInput` implements all controllers' input state-related functions, and `YVRControllerRig` encapsulates all controllers' rigid related operations and information.

For more information, please refer to [YVRInput](xref:YVR.Core.YVRInput) and [YVRControllerRig](xref:YVR.Core.YVRControllerRig).

## YVR Touch Tracking

[YVRControllerRig](xref:YVR.Core.YVRControllerRig) provides touch position and orientation data through [GetPosition](xref:YVR.Core.YVRControllerRig.GetPosition(YVR.Core.ControllerType)) and [GetRotation](xref:YVR.Core.YVRControllerRig.GetRotation(YVR.Core.ControllerType)). Other data, like *Velocity*, *AngularVelocity*, *Acceleration* and *AngularAcceleration* can also be obtained from [YVRControllerRig](xref:YVR.Core.YVRControllerRig).

## YVRInput Usage

The primary usage of [YVRInput](xref:YVR.Core.YVRInput) is to access controller input state through `Get()`, `GetDown()`, and `GetUp()`.

* `Get()`: Queries the current state of a controller.
* `GetDown()`: Queries whether a button(touch) was pressed in this frame.
* `GetUp()`: Queries whether a button(touch) was released this frame.

### Control Input Enumerations

For `Get()`, `GetDown()`, and `GetUp()` functions, there are multiple variations to provide different access to different sets of controls. These sets of controls are divided into two categories: `Virtual Mapping` and `Raw Mapping`, and are all exposed through enumerations. The enumerations defined in `Virtual Mapping` categories are as follows:

* `VirtualButton`: Traditional buttons on YVR Touches.
* `VirtualTouch`: Capacitive-sensitive control surface on YVR Touches.
* `VirtualAxis1D`: One-dimensional controls that report a `float` state.
* `VirtualAxis2D`: Two-dimensional controls that report a `Vector2` state.

The enumeration defined in `Raw Mapping` categories are highly related to the enumerations in `Virtual Mapping`, as follows:

* `RawButton`
* `RawTouch`
* `VirtualAxis1D`
* `VirtualAxis2D`

The conversion between `Raw Mapping` and `Virtual Mapping`, please refer to the section.

### Touch Input Mapping

![Touch Input](Controllers/2021-12-14-15-41-15.png)