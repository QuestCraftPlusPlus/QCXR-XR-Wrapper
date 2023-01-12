using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.AccessControl;
using UnityEngine.Rendering;
using UnityEngine.XR;

namespace YVR.Core
{
    /// <summary>
    /// Encapsulate all camera rigid related operations and data
    /// </summary>
    public class YVRCameraRig : YVRBaseRig
    {
        /// <summary>
        /// Transform of center eye
        /// </summary>
        public Transform centerEyeAnchor { get; set; }

        /// <summary>
        /// Transform of left eye
        /// </summary>
        public Transform leftEyeAnchor { get; set; }

        /// <summary>
        /// Transform of right eye
        /// </summary>
        public Transform rightEyeAnchor { get; set; }

        /// <summary>
        /// Override @YVR.Core.YVRBaseRig.Initialize
        /// </summary>
        public override void Initialize(YVRManager yvrManager)
        {
            base.Initialize(yvrManager);
            InitializeEyePose();
        }

        public System.Action afterRigBeforeRenderUpdated = null;
        public System.Action afterRigDynamicUpdated = null;

        /// <summary>
        /// Override @YVR.Core.YVRBaseRig.InitializeAnchor
        /// </summary>
        protected override void InitializeAnchor()
        {
            base.InitializeAnchor();

            // Initialize Eye Transform
            centerEyeAnchor = centerEyeAnchor ?? ConfigureAnchor(trackingSpace, "CenterEyeAnchor");
            leftEyeAnchor = leftEyeAnchor ?? ConfigureAnchor(trackingSpace, "LeftEyeAnchor");
            rightEyeAnchor = rightEyeAnchor ?? ConfigureAnchor(trackingSpace, "RightEyeAnchor");
        }

        private void InitializeEyePose()
        {
            leftEyeAnchor.localRotation = Quaternion.identity;
            leftEyeAnchor.localPosition = Vector3.zero;

            rightEyeAnchor.localRotation = Quaternion.identity;
            rightEyeAnchor.localPosition = Vector3.zero;

            centerEyeAnchor.localRotation = Quaternion.identity;
            centerEyeAnchor.localPosition = Vector3.zero;
        }

        /// <summary>
        /// Get the pose of the anchors(left eye,right eye,center eye) and assign it to each anchor
        /// </summary>
        public override void UpdateAnchorPose(RigUpdateType updateType)
        {
            base.UpdateAnchorPose(updateType);
            UpdateHeadsetPose();

            leftEyeAnchor.localPosition = leftEyePose.position;
            leftEyeAnchor.localRotation = leftEyePose.orientation;

            rightEyeAnchor.localPosition = rightEyePose.position;
            rightEyeAnchor.localRotation = rightEyePose.orientation;

            centerEyeAnchor.localPosition = hmdPose.position;
            centerEyeAnchor.localRotation = hmdPose.orientation;

            if (updateType == RigUpdateType.Dynamic)
                afterRigDynamicUpdated?.Invoke();
            if (updateType == RigUpdateType.BeforeRender)
                afterRigBeforeRenderUpdated?.Invoke();
        }

        private void UpdateHeadsetPose()
        {
            InputDevice deviceCenterEye
                = yvrXRDevices.Count > 0 ? yvrXRDevices[(int) YVRXRDeviceNode.Headset] : default;
            if (!deviceCenterEye.isValid)
            {
                LoadSimulatedHeadsetPose();
                return;
            }

            deviceCenterEye.TryGetFeatureValue(CommonUsages.centerEyePosition, out Vector3 centerPosition);
            deviceCenterEye.TryGetFeatureValue(CommonUsages.centerEyeRotation, out Quaternion centerRotation);

            deviceCenterEye.TryGetFeatureValue(CommonUsages.leftEyePosition, out Vector3 leftEyePosition);
            deviceCenterEye.TryGetFeatureValue(CommonUsages.leftEyeRotation, out Quaternion leftEyeRotation);

            deviceCenterEye.TryGetFeatureValue(CommonUsages.rightEyePosition, out Vector3 rightEyePosition);
            deviceCenterEye.TryGetFeatureValue(CommonUsages.rightEyeRotation, out Quaternion rightEyeRotation);


            hmdPose.position = centerPosition;
            hmdPose.orientation = centerRotation;

            leftEyePose.position = leftEyePosition;
            leftEyePose.orientation = leftEyeRotation;

            rightEyePose.position = rightEyePosition;
            rightEyePose.orientation = rightEyeRotation;
        }

        private void LoadSimulatedHeadsetPose()
        {
            hmdPose.position = YVRManager.instance.headPoseRelativeOffsetTranslation;
            hmdPose.orientation = Quaternion.Euler(YVRManager.instance.headPoseRelativeOffsetRotation);
        }

        /// <summary>
        /// Whether head's position is tracked
        /// </summary>
        /// <returns> true if head's position is tracked </returns>
        public static bool GetPositionTracked() { return YVRPlugin.Instance.GetHeadsetPositionTracked(); }

        /// <summary>
        /// Whether head's orientation is tracked
        /// </summary>
        /// <returns> true if head's position is tracked </returns>
        public static bool GetOrientationTracked() { return YVRPlugin.Instance.GetHeadsetOrientationTracked(); }

        /// <summary>
        /// Whether head's position is valid
        /// </summary>
        /// <returns> true if head's position is valid </returns>
        public static bool GetValid() { return yvrXRDevices[(int) YVRXRDeviceNode.Headset].isValid; }

        /// <summary>
        /// Get the position of the eye Center
        /// </summary>
        /// <returns> Position of the eye Center</returns>
        public static Vector3 GetPosition()
        {
            bool succeed = yvrXRDevices[(int) YVRXRDeviceNode.Headset]
                .TryGetFeatureValue(CommonUsages.centerEyePosition, out Vector3 result);
            return succeed ? result : Vector3.zero;
        }

        /// <summary>
        /// Get the rotation of the eye Center
        /// </summary>
        /// <returns> Rotation of the eye Center</returns>
        public static Quaternion GetRotation()
        {
            bool succeed = yvrXRDevices[(int) YVRXRDeviceNode.Headset]
                .TryGetFeatureValue(CommonUsages.centerEyeRotation, out Quaternion result);
            return succeed ? result : Quaternion.identity;
        }

        /// <summary>
        /// Get the acceleration of the eye Center
        /// </summary>
        /// <returns> Acceleration of the eye Center</returns>
        public static Vector3 GetAcceleration()
        {
            bool succeed = yvrXRDevices[(int) YVRXRDeviceNode.Headset]
                .TryGetFeatureValue(CommonUsages.centerEyeAcceleration, out Vector3 result);
            return succeed ? result : Vector3.zero;
        }

        /// <summary>
        /// Get the angular acceleration of the eye Center
        /// </summary>
        /// <returns> Angular acceleration of the eye Center</returns>
        public static Vector3 GetAngularAcceleration()
        {
            bool succeed = yvrXRDevices[(int) YVRXRDeviceNode.Headset]
                .TryGetFeatureValue(CommonUsages.centerEyeAngularAcceleration, out Vector3 result);
            return succeed ? result : Vector3.zero;
        }

        /// <summary>
        /// Get the velocity of the eye Center
        /// </summary>
        /// <returns> Velocity of the eye Center</returns>
        public static Vector3 GetVelocity()
        {
            bool succeed = yvrXRDevices[(int) YVRXRDeviceNode.Headset]
                .TryGetFeatureValue(CommonUsages.centerEyeVelocity, out Vector3 result);
            return succeed ? result : Vector3.zero;
        }

        /// <summary>
        /// Get the angular velocity of the eye Center
        /// </summary>
        /// <returns> Angular velocity of the eye Center</returns>
        public static Vector3 GetAngularVelocity()
        {
            bool succeed = yvrXRDevices[(int) YVRXRDeviceNode.Headset]
                .TryGetFeatureValue(CommonUsages.centerEyeAngularVelocity, out Vector3 result);
            return succeed ? result : Vector3.zero;
        }

        /// <summary>
        /// Recenter the pose
        /// </summary>
        public static void RecenterPose() { YVRPlugin.Instance.RecenterTracking(); }
    }
}