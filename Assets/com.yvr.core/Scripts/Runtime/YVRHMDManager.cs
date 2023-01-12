using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Core
{
    /// <summary>
    /// The manager for hmd device data
    /// </summary>
    public class YVRHMDManager
    {

        /// <summary>
        /// Get the battery level
        /// </summary>
        public float batteryLevel { get { return YVRPlugin.Instance.GetBatteryLevel(); } }

        /// <summary>
        /// Get the battery temperature
        /// </summary>
        public float batteryTemperature { get { return YVRPlugin.Instance.GetBatteryTemperature(); } }

        /// <summary>
        /// Get the battery state(-1:get fail;1:unkown;2:Charging;3:discharging;4:not charging;5:full )
        /// </summary>
        public int batteryStatus { get { return YVRPlugin.Instance.GetBatteryStatus(); } }

        /// <summary>
        /// Get the volume level
        /// </summary>
        public float volumeLevel { get { return YVRPlugin.Instance.GetVolumeLevel(); } }

        /// <summary>
        /// Get whether is in power saving mode or not
        /// </summary>
        public bool isPowerSavingActive { get { return YVRPlugin.Instance.IsPowerSavingActive(); } }

        /// <summary>
        /// Get whether user is currently wearing the display.
        /// </summary>
        public bool isUserPresent { get { return YVRPlugin.Instance.IsUserPresent(); } }
    }
}
