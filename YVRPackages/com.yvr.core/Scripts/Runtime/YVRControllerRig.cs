using System.Diagnostics;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.XR;

namespace YVR.Core
{
    /// <summary>
    /// Encapsulate all controllers rigid related operations and information
    /// </summary>
    public class YVRControllerRig : YVRBaseRig
    {
        private static InputDevice s_LeftControllerDevice = default;
        private static InputDevice s_RightControllerDevice = default;

        public static InputDevice leftControllerDevice => s_LeftControllerDevice == default
            ? s_LeftControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand)
            : s_LeftControllerDevice;


        public static InputDevice rightControllerDevice => s_RightControllerDevice == default
            ? s_RightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand)
            : s_RightControllerDevice;

        /// <summary>
        /// Transform of right controller
        /// </summary>
        public Transform leftControllerAnchor { get; private set; }

        /// <summary>
        /// Transform of left controller
        /// </summary>
        public Transform rightControllerAnchor { get; private set; }

        /// <summary>
        /// Override @YVR.Core.YVRBaseRig.InitializeAnchor
        /// </summary>
        protected override void InitializeAnchor()
        {
            base.InitializeAnchor();
            if (leftControllerAnchor != null && rightControllerAnchor != null) return;

            leftControllerAnchor = ConfigureAnchor(trackingSpace, "LeftControllerAnchor");
            rightControllerAnchor = ConfigureAnchor(trackingSpace, "RightControllerAnchor");
        }

        [ExcludeFromDocs]
        protected override void UpdateAnchorPose(RigUpdateType updateType)
        {
            base.UpdateAnchorPose(updateType);

            UpdateControllerPose();

            leftControllerAnchor.localPosition = lControllerPose.position;
            leftControllerAnchor.localRotation = lControllerPose.orientation;
            rightControllerAnchor.localPosition = rControllerPose.position;
            rightControllerAnchor.localRotation = rControllerPose.orientation;
        }

        private void UpdateControllerPose()
        {
            if (leftControllerDevice.isValid)
            {
                leftControllerDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 lPosition);
                leftControllerDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion lRotation);

                lControllerPose.position = lPosition;
                lControllerPose.orientation = lRotation;
            }
            else
                LoadSimulatedLeftControllerPose();

            if (rightControllerDevice.isValid)
            {
                rightControllerDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rControllerPosition);
                rightControllerDevice.TryGetFeatureValue(CommonUsages.deviceRotation,
                                                         out Quaternion rControllerRotation);

                rControllerPose.position = rControllerPosition;
                rControllerPose.orientation = rControllerRotation;
            }
            else
                LoadSimulatedRightControllerPose();
        }

        private void LoadSimulatedLeftControllerPose()
        {
            if (YVRControllerEmulator.createdInstance != null &&
                !YVRControllerEmulator.createdInstance.isEmulatingLeftController) return;

            lControllerPose.position = YVRControllerEmulator.instance.currentControllerPosition;
            lControllerPose.orientation = Quaternion.Euler(YVRControllerEmulator.instance.currentControllerRotation);
        }

        [Conditional("UNITY_EDITOR")]
        private void LoadSimulatedRightControllerPose()
        {
            if (YVRControllerEmulator.createdInstance != null &&
                !YVRControllerEmulator.createdInstance.isEmulatingRightController) return;
            rControllerPose.position = YVRControllerEmulator.instance.currentControllerPosition;
            rControllerPose.orientation = Quaternion.Euler(YVRControllerEmulator.instance.currentControllerRotation);
        }

        /// <summary>
        /// Whether controller's orientation is tracked
        /// </summary>
        /// <param name="controllerType"> target controller </param>
        /// <returns> true if <paramref name="controllerType"/>'s orientation is tracked </returns>
        public static bool GetOrientationTracked(ControllerType controllerType)
        {
            InputDevice device = controllerType == ControllerType.LeftTouch
                ? leftControllerDevice
                : rightControllerDevice;
            device.TryGetFeatureValue(CommonUsages.trackingState, out InputTrackingState result);
            return (result & InputTrackingState.Rotation) != 0;
        }

        /// <summary>
        /// Whether controller's position is tracked
        /// </summary>
        /// <param name="controllerType"> target controller </param>
        /// <returns> true if <paramref name="controllerType"/>'s position is tracked </returns>
        public static bool GetPositionTracked(ControllerType controllerType)
        {
            InputDevice device = controllerType == ControllerType.LeftTouch
                ? leftControllerDevice
                : rightControllerDevice;
            device.TryGetFeatureValue(CommonUsages.trackingState, out InputTrackingState result);

            return (result & InputTrackingState.Position) != 0;
        }

        /// <summary>
        /// Get controller's position
        /// </summary>
        /// <param name="controllerType"> target controller </param>
        /// <returns> <paramref name="controllerType"/>'s position </returns>
        public static Vector3 GetPosition(ControllerType controllerType)
        {
            InputDevice device = controllerType == ControllerType.LeftTouch
                ? leftControllerDevice
                : rightControllerDevice;

            bool succeed = InputDevices
                          .GetDeviceAtXRNode(controllerType == ControllerType.LeftTouch
                                                 ? XRNode.LeftHand
                                                 : XRNode.RightHand)
                          .TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 result);
            return succeed ? result : Vector3.zero;
        }

        /// <summary>
        /// Get controller's velocity
        /// </summary>
        /// <param name="controllerType"> target controller </param>
        /// <returns> <paramref name="controllerType"/>'s velocity </returns>
        public static Vector3 GetVelocity(ControllerType controllerType)
        {
            InputDevice device = controllerType == ControllerType.LeftTouch
                ? leftControllerDevice
                : rightControllerDevice;

            bool succeed = device.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 result);
            return succeed ? result : Vector3.zero;
        }

        /// <summary>
        /// Get controller's acceleration
        /// </summary>
        /// <param name="controllerType"> target controller </param>
        /// <returns> <paramref name="controllerType"/>'s acceleration </returns>
        public static Vector3 GetAcceleration(ControllerType controllerType)
        {
            InputDevice device = controllerType == ControllerType.LeftTouch
                ? leftControllerDevice
                : rightControllerDevice;

            bool succeed = device.TryGetFeatureValue(CommonUsages.deviceAcceleration, out Vector3 result);
            return succeed ? result : Vector3.zero;
        }

        /// <summary>
        /// Get controller's rotation
        /// </summary>
        /// <param name="controllerType"> target controller </param>
        /// <returns> <paramref name="controllerType"/>'s rotation </returns>
        public static Quaternion GetRotation(ControllerType controllerType)
        {
            InputDevice device = controllerType == ControllerType.LeftTouch
                ? leftControllerDevice
                : rightControllerDevice;

            bool succeed = device.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion result);
            return succeed ? result : Quaternion.identity;
        }

        /// <summary>
        /// Get controller's angular angular velocity
        /// </summary>
        /// <param name="controllerType"> target controller </param>
        /// <returns> <paramref name="controllerType"/>'s angular velocity </returns>
        public static Vector3 GetAngularVelocity(ControllerType controllerType)
        {
            InputDevice device = controllerType == ControllerType.LeftTouch
                ? leftControllerDevice
                : rightControllerDevice;

            bool succeed = device.TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out Vector3 result);
            return succeed ? result : Vector3.zero;
        }

        /// <summary>
        /// Get controller's angular acceleration
        /// </summary>
        /// <param name="controllerType"> target controller </param>
        /// <returns> <paramref name="controllerType"/>'s acceleration </returns>
        public static Vector3 GetAngularAcceleration(ControllerType controllerType)
        {
            InputDevice device = controllerType == ControllerType.LeftTouch
                ? leftControllerDevice
                : rightControllerDevice;

            bool succeed = device.TryGetFeatureValue(CommonUsages.deviceAngularAcceleration, out Vector3 result);
            return succeed ? result : Vector3.zero;
        }
    }
}