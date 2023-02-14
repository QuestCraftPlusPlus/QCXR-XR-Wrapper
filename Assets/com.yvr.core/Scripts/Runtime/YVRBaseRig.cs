using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.XR;

namespace YVR.Core
{
    /// <summary>
    /// Base class of rigid class
    /// </summary>
    public class YVRBaseRig : MonoBehaviour
    {
        public enum RigUpdateType { Dynamic, BeforeRender }

        private Transform m_Transform = null;
        private YVRManager m_YVRManager = null;

        /// <summary>
        /// The parent of all tracking anchors
        /// </summary>
        public Transform trackingSpace { get; private set; }

        [ExcludeFromDocs]
        protected enum YVRXRDeviceNode { Headset = 0, LeftController = 1, RightController = 2 }

        [ExcludeFromDocs]
        protected static PoseData hmdPose, leftEyePose, rightEyePose, lControllerPose, rControllerPose;

        [ExcludeFromDocs]
        protected static List<InputDevice> yvrXRDevices = new List<InputDevice>();

        private static bool s_ConnectActionRegistered = false;

        /// <summary>
        /// Initialize function which will be triggered by @YVR.Core.YVRManager
        /// </summary>
        /// <param name="yvrManager">The @YVR.Core.YVRManager trigger  </param>
        public virtual void Initialize(YVRManager yvrManager)
        {
            m_Transform = transform;
            m_YVRManager = yvrManager;
            yvrManager.eventsManager.onUpdate += UpdatePhysicalAnchorPose;
            Application.onBeforeRender += UpdateRenderingAnchorPose;
            InitializeAnchor();

            if (!s_ConnectActionRegistered)
            {
                InputDevices.GetDevices(yvrXRDevices);
                InputDevices.deviceConnected += _ => InputDevices.GetDevices(yvrXRDevices);
                InputDevices.deviceDisconnected += _ => InputDevices.GetDevices(yvrXRDevices);
                s_ConnectActionRegistered = true;
            }
        }

        /// <summary>
        /// Initialization function for derived classes to override
        /// </summary>
        protected virtual void InitializeAnchor()
        {
            trackingSpace = trackingSpace ? trackingSpace : ConfigureAnchor(null, "TrackingSpace");
        }

        public void UpdatePhysicalAnchorPose() { UpdateAnchorPose(RigUpdateType.Dynamic); }

        public void UpdateRenderingAnchorPose() { UpdateAnchorPose(RigUpdateType.BeforeRender); }

        /// <summary>
        /// Get the pose of the anchors and assign it to each anchor
        /// </summary>
        protected virtual void UpdateAnchorPose(RigUpdateType updateType) { }

        /// <summary>
        /// Configure an anchor
        /// </summary>
        /// <param name="parent"> Target anchor's parent </param>currentState.BatteryPercentRemaining
        /// <param name="anchorName"> Target anchor's name </param>
        /// <returns> Configured transform </returns>
        protected Transform ConfigureAnchor(Transform parent, string anchorName)
        {
            Transform anchor = parent?.Find(anchorName);
            if (!anchor) anchor = m_Transform.Find(anchorName);
            if (!anchor) anchor = new GameObject(anchorName).transform;

            anchor.name = anchorName;
            anchor.parent = parent ? parent : m_Transform;
            anchor.localPosition = Vector3.zero;
            anchor.localRotation = Quaternion.identity;
            anchor.localScale = Vector3.one;

            return anchor;
        }

        /// <summary>
        /// Equals to MonoBehavior.OnDestroy
        /// </summary>
        protected virtual void OnDestroy()
        {
            m_YVRManager.eventsManager.onUpdate -= UpdatePhysicalAnchorPose;
            Application.onBeforeRender -= UpdateRenderingAnchorPose;
        }
    }
}