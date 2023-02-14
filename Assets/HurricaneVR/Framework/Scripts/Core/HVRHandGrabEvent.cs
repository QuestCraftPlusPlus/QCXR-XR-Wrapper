using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Core
{
    [RequireComponent(typeof(HVRGrabbable))]
    public class HVRHandGrabEvent : MonoBehaviour
    {
        public HVRGrabbable Grabbable { get; private set; }

        public UnityEvent Grabbed = new UnityEvent();

        protected virtual void Awake()
        {
            Grabbable = GetComponent<HVRGrabbable>();
            Grabbable.HandGrabbed.AddListener(OnHandGrabbed);
            Grabbable.TrackingType = HVRGrabTracking.None;
            Grabbable.ForceGrabbable = false;
        }

        protected virtual void Update()
        {
            Grabbable.CanBeGrabbed = CheckEnableGrab();
        }

        protected virtual bool CheckEnableGrab()
        {
            return true;
        }
        
        protected virtual void OnHandGrabbed(HVRHandGrabber hand, HVRGrabbable arg1)
        {
            arg1.ForceRelease();
            Grabbed.Invoke();
        }
    }
}