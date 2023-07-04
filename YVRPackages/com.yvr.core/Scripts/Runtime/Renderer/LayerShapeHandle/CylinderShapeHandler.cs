using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using YVR.Core;

public class CylinderShapeHandler : ILayerShapeHandler
{
    public void HandleLayerPose(IYVRLayerHandle layerHandle, params object[] data)
    {
        int renderLayerId = (int)data[0];
        Transform transform = data[1] as Transform;
        YVRManager yvrManager = data[2] as YVRManager;
        float angle = (float)data[3];

        float radius = CalculateRadius(angle, transform.lossyScale.x);

        Vector3 origin = transform.position - (transform.forward * radius);
        XRPose originPose = new XRPose() { orientation = transform.rotation, position = origin };

        XRPose xrHeadPose = XRPose.identity;
        YVRCameraRig.centerEyeDevice.TryGetFeatureValue(CommonUsages.centerEyePosition, out xrHeadPose.position);
        YVRCameraRig.centerEyeDevice.TryGetFeatureValue(CommonUsages.centerEyeRotation, out xrHeadPose.orientation);

        XRPose pose = xrHeadPose * yvrManager.cameraRenderer.centerEyeCamera.transform.ToYVRPose().Inverse() * originPose;

        layerHandle.SetLayerPose(renderLayerId, pose);
    }

    public void HandleLayerShape(IYVRLayerHandle layerHandle, params object[] data)
    {
        int renderLayerId = (int)data[0];
        Transform transform = data[1] as Transform;
        float angle = (float)data[2];

        float radius = CalculateRadius(angle, transform.lossyScale.x);
        float ratio = transform.lossyScale.x / transform.lossyScale.y;

        layerHandle.SetLayerCylinderParam(renderLayerId, radius, angle * Mathf.Deg2Rad, ratio);
    }

    private float CalculateRadius(float angle, float arcLength)
    {
        if (angle <= 0) return arcLength / 2f;
        return arcLength / ((2 * Mathf.PI) * (angle / 360f));
    }
}
