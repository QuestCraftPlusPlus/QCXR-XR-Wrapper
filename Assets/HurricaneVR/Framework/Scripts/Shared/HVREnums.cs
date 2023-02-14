using System;

namespace HurricaneVR.Framework.Shared
{
    public enum HVRHoldType
    {
        OneHand, Swap, TwoHanded, ManyHands
    }

    public enum PoseType
    {
        HandPoser,
        PhysicPoser,
        Offset 
    }

    public enum HVRGrabTracking
    {
        ConfigurableJoint,
        FixedJoint,
        None
    }

    public enum HVRHandSide
    {
        Left, Right
    }

    public enum HVRSortMode
    {
        Distance, SquareMagnitude
    }

    public enum HVRGrabTrigger
    {
        Active, Toggle, ManualRelease
    }

    //public enum HVRGrabTriggerOverride
    //{
    //    Active, Toggle, ManualRelease
    //}

    public enum HVRGrabControls
    {
        GripOrTrigger,
        GripOnly,
        TriggerOnly,
    }

    public enum HVRGrabDetection
    {
        Grabbable, Socket
    }

    //the order of these cannot change, they are used in serialization
    public enum HVRButtons
    {
        Grip,
        Trigger,
        Primary,
        PrimaryTouch,
        Secondary,
        SecondaryTouch,
        Menu,
        JoystickButton,
        TrackPadButton,
        JoystickTouch,
        TriggerTouch,
        ThumbTouch,
        TriggerNearTouch,
        ThumbNearTouch,
        TrackPadLeft,
        TrackPadRight,
        TrackPadUp,
        TrackPadDown,
        TrackPadTouch
    }

    [Serializable]
    public struct HVRButtonState
    {
        public bool Active;
        public bool JustActivated;
        public bool JustDeactivated;
        public float Value;
    }

    public enum HVRLayers
    {
        Grabbable, Hand, DynamicPose, Player
    }

    public enum HVRAxis
    {
        X, Y, Z,
        NegX, NegY, NegZ
    }

    public enum HVRXRInputFeatures
    {
        None = 0,
        MenuButton,
        Trigger,
        Grip,
        TriggerPressed,
        GripPressed,
        PrimaryButton,
        PrimaryTouch,
        SecondaryButton,
        SecondaryTouch,
        Primary2DAxisTouch,
        Primary2DAxisClick,
        Secondary2DAxisTouch,
        Secondary2DAxisClick,
        PrimaryAxis2DUp,
        PrimaryAxis2DDown,
        PrimaryAxis2DLeft,
        PrimaryAxis2DRight,
        SecondaryAxis2DUp,
        SecondaryAxis2DDown,
        SecondaryAxis2DLeft,
        SecondaryAxis2DRight
    };
}
