using Assets.HurricaneVR.Framework.Shared.Utilities;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Grabbers
{
    /// <summary>
    /// Example code to grab an object in code on start
    /// </summary>
    public class HVRHandGrabOnStart : MonoBehaviour
    {
        public HVRHandGrabber Grabber;
        public HVRGrabbable Grabbable;
        public void Start()
        {
            this.ExecuteNextUpdate(() =>
            {
                if (Grabbable && Grabber)
                {
                    Grabber.Grab(Grabbable, HVRGrabTrigger.Toggle);
                    if (!Grabber.GrabbedTarget)
                    {
                        Grabbable.MainTransform.position = Grabber.transform.position;
                        Grabber.TryGrab(Grabbable, true);
                        Grabber.GrabToggleActive = true;
                    }
                }
            });
        }
    }
}