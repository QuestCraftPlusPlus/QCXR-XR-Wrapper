using UnityEngine;
using UnityEngine.XR;

namespace YVR.Core
{
    public static class YVRRenderLayerExtensions
    {
        public static XRPose ToXRTrackingSpacePose(this Transform transform, in Camera camera)
        {
            XRPose xrHeadPose = XRPose.identity;

            YVRCameraRig.centerEyeDevice.TryGetFeatureValue(CommonUsages.centerEyePosition, out xrHeadPose.position);
            YVRCameraRig.centerEyeDevice.TryGetFeatureValue(CommonUsages.centerEyeRotation, out xrHeadPose.orientation);

            return xrHeadPose * transform.ToXRHeadSpacePose(camera);
        }

        private static XRPose ToXRHeadSpacePose(this Transform transform, in Camera camera)
        {
            return camera.transform.ToYVRPose().Inverse() * transform.ToYVRPose();
        }

        public static XRPose ToYVRPose(this Transform transform)
        {
            return new XRPose() {orientation = transform.rotation, position = transform.position};
        }
    }
}