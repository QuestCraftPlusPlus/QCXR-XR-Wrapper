using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Core
{
    /// <summary>
    /// Encapsulate all tracking related operation
    /// </summary>
    [Serializable]
    public partial class YVRTrackingStateManager
    {
        [SerializeField] private TrackingSpace _trackingSpace = TrackingSpace.EyeLevel;

        /// <summary>
        /// The event which will be triggered when using ipd mode changed
        /// </summary>
        public event Action<bool> onUseIPDInPositionTrackingChanged = null;


        // When false, YVRCameraRig will ignore ipd position
        private bool _useIPDInPositionTracking = true;

        /// <summary>
        /// Set or get whether uses ipd in position tracking or not
        /// </summary>
        public bool useIPDInPositionTracking
        {
            get { return _useIPDInPositionTracking; }
            set
            {
                if (_useIPDInPositionTracking == value) return;
                _useIPDInPositionTracking = value;
                ApplyIPDSetting();
            }
        }

        /// <summary>
        /// Set or get the tracking space of the rigid poses
        /// </summary>
        public TrackingSpace trackingSpace
        {
            get { return _trackingSpace; }
            set
            {
                if (_trackingSpace == value) return;
                _trackingSpace = value;
                ApplyTrackingSpace();
            }
        }
        /// <summary>
        ///Initialize tracking space and IPD setting
        /// </summary>
        public void Initialize()
        {
            ApplyIPDSetting();
            ApplyTrackingSpace();
        }

        private void ApplyIPDSetting()
        {
            YVRPlugin.Instance.SetUsingIPDInPositionTracking(useIPDInPositionTracking);
            onUseIPDInPositionTrackingChanged?.SafeInvoke(useIPDInPositionTracking);
        }

        private void ApplyTrackingSpace()
        {
            YVRPlugin.Instance.SetTrackingSpace(_trackingSpace);
        }
    }
}