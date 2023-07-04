using UnityEngine;

namespace YVR.Core
{
    /// <summary>
    /// The general manager class of whole sdk which holds other managers and controls the lifecycle of the vr mode
    /// </summary>
    public class YVRManager : MonoBehaviorSingleton<YVRManager>
    {
        public enum HandTrackingSupport
        {
            ControllersOnly = 0,
            ControllersAndHands = 1,
            HandsOnly = 2
        }

        [SerializeField] private bool m_UseUnityRig = false;

        /// <summary>
        /// The instance of class @YVR.Core.YVRControllerRig
        /// </summary>
        public YVRControllerRig controllerRig { get; private set; }

        /// <summary>
        /// The instance of class @YVR.Core.YVRCameraRig
        /// </summary>
        public YVRCameraRig cameraRig { get; private set; }

        /// <summary>
        /// The instance of class @YVR.Core.YVRCameraRenderer
        /// </summary>
        public YVRCameraRenderer cameraRenderer = new YVRCameraRenderer();

        /// <summary>
        /// The instance of class @YVR.Core.YVRBoundary
        /// </summary>
        public YVRBoundary boundary = new YVRBoundary();

        /// <summary>
        /// The instance of class @YVR.Core.YVRQualityManager
        /// </summary>
        public YVRQualityManager qualityManager = new YVRQualityManager();

        /// <summary>
        /// The instance of class @YVR.Core.YVRTrackingStateManager
        /// </summary>
        public YVRTrackingStateManager trackingManager = new YVRTrackingStateManager();

        /// <summary>
        /// The instance of class @YVR.Core.YVRHMDManager
        /// </summary>
        public YVRHMDManager hmdManager = new YVRHMDManager();

        /// <summary>
        /// The instance of class @YVR.Core.YVREventsManager
        /// </summary>
        public YVREventsManager eventsManager = new YVREventsManager();

        /// <summary>
        /// The instance of class @YVR.Core.YVRHandManager
        /// </summary>
        public YVRHandManager handManager { get; set; }

        /// <summary>
        /// The position offset of the center eye, only works in editor mode
        /// </summary>
        public Vector3 headPoseRelativeOffsetTranslation { get; set; }

        /// <summary>
        /// The rotation offset of the center eye (in euler angle), only works in editor mode
        /// </summary>
        public Vector3 headPoseRelativeOffsetRotation { get; set; }

        #region External Settings

        /// <summary>
        /// Set or get current cpu level(0-4), see also @YVR.Core.YVRPerformanceManager
        /// </summary>
        public int cpuLevel
        {
            get => YVRPerformanceManager.cpuLevel;
            set => YVRPerformanceManager.cpuLevel = value;
        }

        /// <summary>
        /// Set or get current gpu level(0-5), see also @YVR.Core.YVRPerformanceManager
        /// </summary>
        public int gpuLevel
        {
            get => YVRPerformanceManager.gpuLevel;
            set => YVRPerformanceManager.gpuLevel = value;
        }

        /// <summary>
        /// Set or get current vSync count, see also @YVR.Core.YVRQualityManager
        /// </summary>
        public YVRQualityManager.VSyncCount vSyncCount
        {
            get => qualityManager.vSyncCount;
            set => qualityManager.vSyncCount = value;
        }

        /// <summary>
        /// Set or get current fixed foveated rendering level, see also @YVR.Core.YVRQualityManager
        /// </summary>
        public YVRQualityManager.FixedFoveatedRenderingLevel fixedFoveatedRenderingLevel
        {
            get => qualityManager.fixedFoveatedRenderingLevel;
            set => qualityManager.fixedFoveatedRenderingLevel = value;
        }

        /// <summary>
        /// Get whether fixed foveation rendering is enabled or not
        /// </summary>
        public bool fixedFoveationRenderingEnabled =>
            fixedFoveatedRenderingLevel != YVRQualityManager.FixedFoveatedRenderingLevel.Off;

        /// <summary>
        /// Get whether to use recommend MSAA level, see also @YVR.Core.YVRQualityManager
        /// </summary>
        public bool useRecommendedMSAALevel => qualityManager.useRecommendedMSAALevel;

        /// <summary>
        /// Set or get whether to consider ipd camera's position tracking, see also @YVR.Core.YVRTrackingStateManager
        /// </summary>
        public bool useIPDInPositionTracking
        {
            get => trackingManager.useIPDInPositionTracking;
            set => trackingManager.useIPDInPositionTracking = value;
        }

        public YVRTrackingStateManager.TrackingSpace trackingSpace
        {
            get => trackingManager.trackingSpace;
            set => trackingManager.trackingSpace = value;
        }

        /// <summary>
        /// Get current battery level, see also @YVR.Core.YVRHMDManager
        /// </summary>
        public float batteryLevel => hmdManager.batteryLevel;

        /// <summary>
        /// Get current battery temperature, see also @YVR.Core.YVRHMDManager
        /// </summary>
        public float batteryTemperature => hmdManager.batteryTemperature;

        /// <summary>
        /// Get current battery status(charge or not), see also @YVR.Core.YVRHMDManager
        /// </summary>
        public int batteryStatus => hmdManager.batteryStatus;

        /// <summary>
        /// Get current volume level, see also @YVR.Core.YVRHMDManager
        /// </summary>
        public float volumeLevel => hmdManager.volumeLevel;

        /// <summary>
        /// Get whether user is currently wearing the display, see also @YVR.Core.YVRHMDManager
        /// </summary>
        public bool isUserPresent => hmdManager.isUserPresent;

        /// <summary>
        /// Get current gpu utilizing level(0.0-1.0), see also @YVR.Core.YVRHMDManager
        /// </summary>
        public float gpuUtilLevel => YVRPerformanceManager.gpuUtilLevel;

        /// <summary>
        /// Get current cpu utilizing level（0.0-1.0）, see also @YVR.Core.YVRHMDManager
        /// </summary>
        public float cpuUtilLevel => YVRPerformanceManager.cpuUtilLevel;

        public HandTrackingSupport handTrackingSupport;

        #endregion

        /// <summary>
        /// The override function of @YVR.Core.MonoBehaviorSingleton`1.Init
        /// </summary>
        protected override void Init()
        {
            base.Init();

            if (!m_UseUnityRig)
            {
                cameraRig = this.AutoAddingGetComponent<YVRCameraRig>();
                controllerRig = this.AutoAddingGetComponent<YVRControllerRig>();
                trackingManager.Initialize();
                qualityManager.Initialize();
                cameraRig.Initialize(this);
                controllerRig.Initialize(this);
                cameraRenderer.Initialize(this);
            }
            else
            {
                // While using unity rig, we should disable the cameraRig to avoid the rendering twice
                cameraRig = GetComponent<YVRCameraRig>();
                cameraRig?.centerEyeAnchor.gameObject.SetActive(false);
                cameraRig?.leftEyeAnchor.gameObject.SetActive(false);
                cameraRig?.rightEyeAnchor.gameObject.SetActive(false);
            }

            handManager = this.AutoAddingGetComponent<YVRHandManager>();
            handManager.Initialize();

            eventsManager.Initialize();

            if (useRecommendedMSAALevel) QualitySettings.antiAliasing = qualityManager.recommendAntiAlisingLevel;

            PackageVersion.PrintPackagesVersion(typeof(YVRManager));
            PackageVersion.PrintPackagesVersion(typeof(PackageVersion));
        }


        private void Update()
        {
            YVRInput.Update();

            eventsManager.Update();
        }
    }
}