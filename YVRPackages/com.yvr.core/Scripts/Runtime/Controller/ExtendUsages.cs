using UnityEngine;
using UnityEngine.XR;

namespace YVR.Core
{
    public class ExtendUsages : MonoBehaviour
    {
        public static InputFeatureUsage<bool> batteryCharging = new InputFeatureUsage<bool>("BatteryCharging");
    }
}