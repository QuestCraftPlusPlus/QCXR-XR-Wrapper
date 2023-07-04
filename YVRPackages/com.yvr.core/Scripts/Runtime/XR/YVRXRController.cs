#if UNITY_INPUT_SYSTEM
using UnityEngine.Scripting;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

/// <summary>
/// A YVR Controller
/// </summary>
[Preserve]
[InputControlLayout(displayName = "YVR Controller", commonUsages = new[] { "LeftHand", "RightHand" })]
public class YVRXRController : XRControllerWithRumble
{
    [Preserve]
    [InputControl(aliases = new[] { "Primary2DAxis", "Joystick" })]
    public Vector2Control thumbstick { get; private set; }
    [Preserve]
    [InputControl]
    public AxisControl trigger { get; private set; }
    [Preserve]
    [InputControl]
    public AxisControl batteryLevel { get; private set; }
    [Preserve]
    [InputControl(aliases = new[] { "Home", "Menu", "MenuButton" })]
    public ButtonControl menuButton { get; private set; }
    [Preserve]
    [InputControl(aliases = new[] { "TriggerTouch", "IndexTouch" })]
    public ButtonControl triggerTouch { get; private set; }
    [Preserve]
    [InputControl(aliases = new[] { "A", "X" })]
    public ButtonControl primaryButton { get; private set; }
    [Preserve]
    [InputControl(aliases = new[] { "B", "Y" })]
    public ButtonControl secondaryButton { get; private set; }
    [Preserve]
    [InputControl(aliases = new[] { "GripButton", "GripPress" })]
    public ButtonControl gripPressed { get; private set; }
    [Preserve]
    [InputControl(aliases = new[] { "Primary2DAxisClick", "JoystickClick" })]
    public ButtonControl thumbstickClicked { get; private set; }
    [Preserve]
    [InputControl(aliases = new[] { "ATouch", "XTouch" })]
    public ButtonControl primaryTouched { get; private set; }
    [Preserve]
    [InputControl(aliases = new[] { "BTouch", "YTouch" })]
    public ButtonControl secondaryTouched { get; private set; }
    [Preserve]
    [InputControl(aliases = new[] { "TriggerButton" })]
    public ButtonControl triggerPressed { get; private set; }
    [Preserve]
    [InputControl(aliases = new[] { "Primary2DAxisTouch", "JoystickTouch" })]
    public ButtonControl thumbstickTouched { get; private set; }
    [Preserve]
    [InputControl]
    public Vector3Control deviceVelocity { get; private set; }
    [Preserve]
    [InputControl]
    public Vector3Control deviceAngularVelocity { get; private set; }
    [Preserve]
    [InputControl]
    public Vector3Control deviceAcceleration { get; private set; }
    [Preserve]
    [InputControl]
    public Vector3Control deviceAngularAcceleration { get; private set; }

    protected override void FinishSetup()
    {
        base.FinishSetup();

        thumbstick = GetChildControl<Vector2Control>("thumbstick");
        trigger = GetChildControl<AxisControl>("trigger");
        batteryLevel = GetChildControl<AxisControl>("batteryLevel");
        menuButton = GetChildControl<ButtonControl>("menuButton");
        triggerTouch = GetChildControl<ButtonControl>("triggerTouch");
        primaryButton = GetChildControl<ButtonControl>("primaryButton");
        secondaryButton = GetChildControl<ButtonControl>("secondaryButton");
        gripPressed = GetChildControl<ButtonControl>("gripPressed");
        thumbstickClicked = GetChildControl<ButtonControl>("thumbstickClicked");
        primaryTouched = GetChildControl<ButtonControl>("primaryTouched");
        secondaryTouched = GetChildControl<ButtonControl>("secondaryTouched");
        thumbstickTouched = GetChildControl<ButtonControl>("thumbstickTouched");
        triggerPressed = GetChildControl<ButtonControl>("triggerPressed");
        deviceVelocity = GetChildControl<Vector3Control>("deviceVelocity");
        deviceAngularVelocity = GetChildControl<Vector3Control>("deviceAngularVelocity");
        deviceAcceleration = GetChildControl<Vector3Control>("deviceAcceleration");
        deviceAngularAcceleration = GetChildControl<Vector3Control>("deviceAngularAcceleration");
    }
}
#endif