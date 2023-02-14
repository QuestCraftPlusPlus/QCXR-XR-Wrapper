using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.HandPoser;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    public class DemoPoseSqueeze : MonoBehaviour
    {
        public Transform Target;
        public Vector3 ScaleTarget;
        public HVRGrabbable Grabbable;

        private Vector3 _scale;

        void Start()
        {
            TryGetComponent(out Grabbable);
            Grabbable.HandReleased.AddListener(OnReleased);
            _scale = Target.localScale;
        }

        private void OnReleased(HVRHandGrabber arg0, HVRGrabbable arg1)
        {
            //clear the parameter when no longer holding the object
            HVRAnimationParameters.ClearFloatParameter(arg0.HandSide, "Squeeze");
            Target.localScale = _scale;
        }


        void Update()
        {
            if (Grabbable.HandGrabbers.Count > 0)
            {
                var hand = Grabbable.HandGrabbers[0];
                var value = hand.Controller.ControllerType == HVRControllerType.Knuckles ? hand.Controller.GripForce : hand.Controller.Trigger;

                //setting the 'Squeeze' parameter that coincides with the parameter name set on the grabpoint's HVRHandPoser
                HVRAnimationParameters.SetFloatParameter(hand.HandSide, "Squeeze", value);
                Target.localScale = Vector3.Lerp(_scale, ScaleTarget, value);
            }

        }
    }
}
