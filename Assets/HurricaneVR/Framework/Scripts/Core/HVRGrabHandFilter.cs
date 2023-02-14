using System;
using HurricaneVR.Framework.Core.Grabbers;

/// <summary>
/// Filters the incoming grabbing hand by left or right hand only
/// </summary>
public class HVRGrabHandFilter : HVRHandGrabFilter
{

    public HandOptions AllowedHands;
    
    public override bool CanBeGrabbed(HVRHandGrabber hand)
    {
        if (AllowedHands == HandOptions.Both)
            return true;
        
        if (AllowedHands == HandOptions.Left && hand.IsLeftHand)
            return true;
        
        if (AllowedHands == HandOptions.Right && hand.IsRightHand)
            return true;

        return false;
    }

    //[Flags]
    public enum HandOptions
    {
        Left = 0, Right = 1, Both = 2
    }
}