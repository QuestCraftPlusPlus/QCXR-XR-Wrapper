using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    //Useful if this grabbable is a single handed grabbable attached to another.
    //If the other grabbable is grabbed this one will have it's two handed settings forced to be used

    [RequireComponent(typeof(HVRGrabbable))]
    public class HVRTwoHandStrength : MonoBehaviour
    {
        public HVRGrabbable[] Others;

        public HVRGrabbable Grabbable { get; private set; }

        protected virtual void Awake()
        {
            Grabbable = GetComponent<HVRGrabbable>();

            Grabbable.HandGrabbed.AddListener(OnHandGrabbed);
            Grabbable.HandReleased.AddListener(OnHandReleased);

            if (Others != null)
            {
                foreach (var other in Others)
                {
                    if (other)
                    {
                        other.HandGrabbed.AddListener(OnOtherGrabbableHandGrabbed);
                        other.HandReleased.AddListener(OnOtherGrabbableHandReleased);
                    }
                }
            }
        }

        private void OnHandReleased(HVRHandGrabber arg0, HVRGrabbable arg1)
        {
            Grabbable.ForceTwoHandSettings = false;
        }

        private void OnHandGrabbed(HVRHandGrabber arg0, HVRGrabbable arg1)
        {
            foreach (var other in Others)
            {
                if (other.HandGrabbers.Count > 0)
                {
                    Grabbable.ForceTwoHandSettings = true;
                    break;
                }
            }
        }

        private void OnOtherGrabbableHandReleased(HVRHandGrabber arg0, HVRGrabbable g)
        {
            //handle scenario if two "others" are held, then one was released.
            var force = false;
            for (int i = 0; i < Others.Length; i++)
            {
                var other = Others[i];
                if (other == g || other.HandGrabbers.Count == 0)
                    continue;

                force = true;
                break;
            }

            if (!force)
                Grabbable.ForceTwoHandSettings = false;
        }

        private void OnOtherGrabbableHandGrabbed(HVRHandGrabber arg0, HVRGrabbable arg1)
        {
            Grabbable.ForceTwoHandSettings = true;
        }
    }
}