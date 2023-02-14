using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

/// <summary>
/// Optional filter applied to HVRGrabbable object to decide if the potential hand can grab this object
/// </summary>
[RequireComponent(typeof(HVRGrabbable))]
public abstract class HVRHandGrabFilter : MonoBehaviour
{
    public HVRGrabbable Grabbable { get; set; }
    
    protected virtual void Awake()
    {
        if (TryGetComponent(out HVRGrabbable g))
            Grabbable = g;
    }

    public abstract bool CanBeGrabbed(HVRHandGrabber hand);
}