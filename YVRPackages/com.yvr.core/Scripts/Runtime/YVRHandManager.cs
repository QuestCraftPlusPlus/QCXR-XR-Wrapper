using System;
using UnityEngine;

namespace YVR.Core
{
    public class YVRHandManager : MonoBehaviorSingleton<YVRHandManager>
    {
        /// <summary>
        /// Left hand tracking
        /// </summary>
        public YVRHand leftHand { get; private set; }

        /// <summary>
        /// Right hand tracking
        /// </summary>
        public YVRHand rightHand { get; private set; }

        public HandJointLocations leftHandData = new HandJointLocations();
        public HandJointLocations rightHandData = new HandJointLocations();
        [NonSerialized] private HandStatus m_PreLeftHandStatus;
        [NonSerialized] private HandStatus m_PreRightHandStatus;

        /// <summary>
        /// Initialize hand Transform
        /// </summary>
        public void Initialize()
        {
            YVRPlugin.Instance.SetAPPHandEnable(YVRManager.instance.handTrackingSupport !=
                                                YVRManager.HandTrackingSupport.ControllersOnly);
            YVRPlugin.Instance.SetAPPControllerEnable(YVRManager.instance.handTrackingSupport !=
                                                      YVRManager.HandTrackingSupport.HandsOnly);
            Application.onBeforeRender += UpdateHandData;

            leftHand = FindHandTracking("TrackingSpace/HandRoot/YVRLeftHand");
            rightHand = FindHandTracking("TrackingSpace/HandRoot/YVRRightHand");
        }

        /// <summary>
        /// This method returns a boolean value indicating whether hand tracking is enabled or not
        /// </summary>
        /// <returns></returns>
        public bool GetHandTrackingEnable() { return YVRPlugin.Instance.GetHandEnable(); }

        /// <summary>
        /// Returns the pinch strength of the specified finger for the given hand type.
        /// </summary>
        /// <param name="handType">hand type </param>
        /// <param name="fingerID">finger type </param>
        /// <returns></returns>
        public float GetFingerPinchStrength(HandType handType, XRHandFingerID fingerID)
        {
            HandJointLocations handJointLocations = handType == HandType.HandLeft ? leftHandData : rightHandData;
            if (handJointLocations.isActive != 0)
            {
                switch (fingerID)
                {
                    case XRHandFingerID.Index:
                        return handJointLocations.aimState.pinchStrengthIndex;
                    case XRHandFingerID.Middle:
                        return handJointLocations.aimState.pinchStrengthMiddle;
                    case XRHandFingerID.Ring:
                        return handJointLocations.aimState.pinchStrengthRing;
                    case XRHandFingerID.Little:
                        return handJointLocations.aimState.pinchStrengthLittle;
                    default:
                        return 0;
                }
            }

            return 0.0f;
        }

        public bool GetPinch(HandType handType, XRHandFingerID fingerID)
        {
            HandJointLocations currentHandData = handType == HandType.HandLeft ? leftHandData : rightHandData;

            switch (fingerID)
            {
                case XRHandFingerID.Index:
                    return (currentHandData.aimState.status & HandStatus.IndexPinching) != 0;
                case XRHandFingerID.Middle:
                    return (currentHandData.aimState.status & HandStatus.MiddlePinching) != 0;
                case XRHandFingerID.Ring:
                    return (currentHandData.aimState.status & HandStatus.RingPinching) != 0;
                case XRHandFingerID.Little:
                    return (currentHandData.aimState.status & HandStatus.PinkPinching) != 0;
                default:
                    return false;
            }
        }

        public bool GetPinchDown(HandType handType, XRHandFingerID fingerID)
        {
            HandStatus lastHandStatus = handType == HandType.HandLeft ? m_PreLeftHandStatus : m_PreRightHandStatus;
            HandJointLocations currentHandData = handType == HandType.HandLeft ? leftHandData : rightHandData;
            switch (fingerID)
            {
                case XRHandFingerID.Index:
                    return (lastHandStatus & HandStatus.IndexPinching) == 0 &&
                           (currentHandData.aimState.status & HandStatus.IndexPinching) != 0;
                case XRHandFingerID.Middle:
                    return (lastHandStatus & HandStatus.MiddlePinching) == 0 &&
                           (currentHandData.aimState.status & HandStatus.MiddlePinching) != 0;
                case XRHandFingerID.Ring:
                    return (lastHandStatus & HandStatus.RingPinching) == 0 &&
                           (currentHandData.aimState.status & HandStatus.RingPinching) != 0;
                case XRHandFingerID.Little:
                    return (lastHandStatus & HandStatus.PinkPinching) == 0 &&
                           (currentHandData.aimState.status & HandStatus.PinkPinching) != 0;
                default:
                    return false;
            }
        }

        public bool GetPinchUp(HandType handType, XRHandFingerID fingerID)
        {
            HandStatus lastHandStatus = handType == HandType.HandLeft ? m_PreLeftHandStatus : m_PreRightHandStatus;
            HandJointLocations currentHandData = handType == HandType.HandLeft ? leftHandData : rightHandData;
            switch (fingerID)
            {
                case XRHandFingerID.Index:
                    return (lastHandStatus & HandStatus.IndexPinching) != 0 &&
                           (currentHandData.aimState.status & HandStatus.IndexPinching) == 0;
                case XRHandFingerID.Middle:
                    return (lastHandStatus & HandStatus.MiddlePinching) != 0 &&
                           (currentHandData.aimState.status & HandStatus.MiddlePinching) == 0;
                case XRHandFingerID.Ring:
                    return (lastHandStatus & HandStatus.RingPinching) != 0 &&
                           (currentHandData.aimState.status & HandStatus.RingPinching) == 0;
                case XRHandFingerID.Little:
                    return (lastHandStatus & HandStatus.PinkPinching) != 0 &&
                           (currentHandData.aimState.status & HandStatus.PinkPinching) == 0;
                default:
                    return false;
            }
        }

        private void UpdateHandData()
        {
            if (YVRManager.instance.handTrackingSupport == YVRManager.HandTrackingSupport.ControllersOnly) return;

            m_PreLeftHandStatus = leftHandData.aimState.status;
            m_PreRightHandStatus = rightHandData.aimState.status;
            YVRPlugin.Instance.GetHandJointLocations(HandType.HandLeft, ref leftHandData);
            YVRPlugin.Instance.GetHandJointLocations(HandType.HandRight, ref rightHandData);
            UpdateJoints(leftHandData);
            UpdateJoints(rightHandData);

            leftHand?.UpdateHandTransform();
            rightHand?.UpdateHandTransform();
        }

        /// <summary>
        /// This method finds a hand tracking component by its anchor name
        /// </summary>
        /// <param name="anchorName"></param>
        /// <returns></returns>
        private YVRHand FindHandTracking(string anchorName)
        {
            Transform anchor = this.transform.Find(anchorName);
            return anchor?.GetComponent<YVRHand>();
        }

        private void UpdateJoints(HandJointLocations handJointLocations)
        {
            if (handJointLocations.isActive == 0) return;

            var wristPose = Pose.identity;
            UpdateJoint(handJointLocations, (int) HandJoint.JointWrist, ref wristPose);
            for (int fingerIndex = (int) XRHandFingerID.Thumb;
                 fingerIndex <= (int) XRHandFingerID.Little;
                 ++fingerIndex)
            {
                var parentPose = wristPose;
                var fingerId = (XRHandFingerID) fingerIndex;
                int jointIndexBack = (int) fingerId.GetBackJointID();
                for (int jointIndex = (int) fingerId.GetFrontJointID();
                     jointIndex <= jointIndexBack;
                     ++jointIndex)
                {
                    UpdateJoint(handJointLocations, jointIndex, ref parentPose);
                }
            }
        }

        private void UpdateJoint(HandJointLocations handJointLocations, int jointIndex, ref Pose parentPose)
        {
            HandJointLocation xform = handJointLocations.jointLocations[jointIndex];
            Posef pose = xform.pose;
            var inverseParentRotation = Quaternion.Inverse(parentPose.rotation);
            Vector3 inversePosition = inverseParentRotation * (pose.position - parentPose.position);
            xform.pose.position = inversePosition;
            Quaternion rotation = inverseParentRotation * pose.orientation;
            xform.pose.orientation = rotation;
            parentPose = new Pose();
            parentPose.position = xform.pose.position;
            parentPose.rotation = xform.pose.orientation;
        }

        public ActiveInputDevice GetInputDevice()
        {
            ActiveInputDevice activeInputDevice = ActiveInputDevice.ControllerActive;
            YVRPlugin.Instance.GetCurrentInputDevice(ref activeInputDevice);
            return activeInputDevice;
        }

        private void OnDestroy()
        {
            Application.onBeforeRender -= UpdateHandData;
        }
    }
}