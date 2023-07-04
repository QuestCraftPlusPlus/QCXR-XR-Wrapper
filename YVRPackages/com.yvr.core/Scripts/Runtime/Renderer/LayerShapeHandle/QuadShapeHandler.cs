using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using YVR.Core;

public class QuadShapeHandler : ILayerShapeHandler
{
    public void HandleLayerPose(IYVRLayerHandle layerHandle, params object[] data)
    {
        int renderLayerId = (int)data[0];
        Transform transform = data[1] as Transform;
        YVRManager yvrManager = data[2] as YVRManager;
        XRPose pose = transform.ToXRTrackingSpacePose(yvrManager.cameraRenderer.centerEyeCamera);
        layerHandle.SetLayerPose(renderLayerId, pose);
    }

    public void HandleLayerShape(IYVRLayerHandle layerHandle, params object[] data)
    {
        int renderLayerId = (int)data[0];
        Transform transform = data[1] as Transform;
        layerHandle.SetLayerSize(renderLayerId, new XRSize(transform.lossyScale.x, transform.lossyScale.y));
    }
}
