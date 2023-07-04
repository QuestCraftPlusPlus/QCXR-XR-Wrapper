using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.UI;

namespace YVR.Core
{
    [ExcludeFromDocs]
    public class YVRTrackingDebug : YVRDebugHub
    {
        public Text CameraOrientationText = null;
        public Text CameraPositionText = null;
        public Text ControllerOrientationText = null;
        public Text ControllerPositionText = null;

        protected override void InitDebugHub()
        {
            base.InitDebugHub();

            if (!CameraOrientationText) CameraOrientationText = transform.Find("CameraOrientationText").GetComponent<Text>();
            if (!CameraPositionText) CameraPositionText = transform.Find("CameraPositionText").GetComponent<Text>();
            if (!ControllerOrientationText) ControllerOrientationText = transform.Find("ControllerOrientationText").GetComponent<Text>();
            if (!ControllerPositionText) ControllerPositionText = transform.Find("ControllerPositionText").GetComponent<Text>();
        }

        protected override void UpdateDebugInfo()
        {
            base.UpdateDebugInfo();
            string cameraOrientation = YVRManager.instance.cameraRig.trackingSpace.localEulerAngles.ToString("f2");
            string cameraPosition = YVRManager.instance.cameraRig.trackingSpace.localPosition.ToString("f2");
            string controllerOrientation = YVRManager.instance.controllerRig.leftControllerAnchor.localEulerAngles.ToString("f2");
            string controllerPosition = YVRManager.instance.controllerRig.leftControllerAnchor.localPosition.ToString("f2");


            if (CameraOrientationText) CameraOrientationText.text = "Camera Ori: " + cameraOrientation;
            if (CameraPositionText) CameraPositionText.text = "Camera Pos: " + cameraPosition;
            if (ControllerOrientationText) ControllerOrientationText.text = "Controller Ori: " + controllerOrientation;
            if (ControllerPositionText) ControllerPositionText.text = "Controller Pos: " + controllerPosition;
        }

    }
}