using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.Framework.Core
{
    public class HVRGrabberManager : MonoBehaviour
    {
        protected virtual void Awake()
        {
           
        }

        public void RegisterGrabber(HVRGrabberBase grabber)
        {
            grabber.BeforeGrabbed.AddListener(OnBeforeGrabberGrabbed);
            grabber.Grabbed.AddListener(OnGrabberGrabbed);
            grabber.Released.AddListener(OnGrabberReleased);
        }

        protected virtual void OnBeforeGrabberGrabbed(HVRGrabberBase grabber, HVRGrabbable grabbable)
        {
            
        }

        public void UnregisterGrabber(HVRGrabberBase grabber)
        {
            grabber.Grabbed.RemoveListener(OnGrabberGrabbed);
            grabber.Released.RemoveListener(OnGrabberReleased);
        }

        protected virtual void OnGrabberReleased(HVRGrabberBase grabber, HVRGrabbable grabbable)
        {
        }

        protected virtual void OnGrabberGrabbed(HVRGrabberBase grabber, HVRGrabbable grabbable)
        {

        }

       
    }
}