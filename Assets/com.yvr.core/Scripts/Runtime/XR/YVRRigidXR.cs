using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.XR;
using YVR.Core;

[ExcludeFromDocs]
public class YVRRigidXR : MonoBehaviour
{
    private void Start()
    {
        Application.onBeforeRender += OnPreRender;
    }

    private void Update()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        InputDevice deviceCenterEye = devices.Find(device => device.name == "Headset");
        InputDevice deviceLeftController = devices.Find(device => device.name == "LeftController");
        InputDevice deviceRightController = devices.Find(device => device.name == "RightController");

        if (deviceCenterEye.isValid)
        {
            deviceCenterEye.TryGetFeatureValue(CommonUsages.centerEyePosition, out Vector3 centerPosition);
            deviceCenterEye.TryGetFeatureValue(CommonUsages.centerEyeRotation, out Quaternion centerRotation);

            deviceCenterEye.TryGetFeatureValue(CommonUsages.leftEyePosition, out Vector3 leftEyePosition);
            deviceCenterEye.TryGetFeatureValue(CommonUsages.leftEyeRotation, out Quaternion leftEyeRotation);

            transform.position = centerPosition;
            transform.rotation = centerRotation;
        }

        if (deviceLeftController.isValid)
        {
            deviceLeftController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 controllerPosition);
            deviceLeftController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion controllerRotation);
        }

        if (deviceRightController.isValid)
        {
            deviceLeftController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 controllerPosition);
            deviceLeftController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion controllerRotation);
        }
    }

    private void OnPreRender()
    {
        InputDevice deviceCenterEye = InputDevices.GetDeviceAtXRNode(XRNode.CenterEye);
        if (deviceCenterEye.isValid)
        {
            deviceCenterEye.TryGetFeatureValue(CommonUsages.centerEyePosition, out Vector3 position);
            deviceCenterEye.TryGetFeatureValue(CommonUsages.centerEyeRotation, out Quaternion rotation);

            transform.position = position;
            transform.rotation = rotation;
        }
    }
}