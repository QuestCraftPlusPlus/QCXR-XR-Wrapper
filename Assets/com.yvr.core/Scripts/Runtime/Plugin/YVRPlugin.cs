using UnityEngine;
using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;

namespace YVR.Core
{
    [ExcludeFromDocs]
    public abstract class YVRPlugin
    {
        private static YVRPlugin s_Instance;

        public static YVRPlugin Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    if (!Application.isEditor && Application.platform == RuntimePlatform.Android)
                        s_Instance = YVRPluginAndroid.Create();
                    else
                        s_Instance = YVRPluginWin.Create();
                }

                return s_Instance;
            }
        }

        // The Controller State Received from the native plugin
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ControllerState
        {
            public UInt32 Buttons; //4
            public UInt32 Touches; //8
            public float IndexTrigger; //12
            public Vector2 Thumbstick; //20
            public float BatteryPercentRemaining; //21
            public bool isCharging; // 22

            public void Clear()
            {
                Buttons = 0;
                Touches = 0;
                IndexTrigger = 0;
                Thumbstick.x = Thumbstick.y = 0;
                BatteryPercentRemaining = 0;
                isCharging = false;
            }
        }

        public virtual void SetTrackingSpace(YVRTrackingStateManager.TrackingSpace trackingSpace) { }
        public virtual void SetVSyncCount(YVRQualityManager.VSyncCount vSyncCount) { }
        public virtual void RecenterTracking() { }

        public virtual void SetControllerVibration(uint controllerMask, float frequency, float amplitude) { }

        public virtual void SetUsingIPDInPositionTracking(bool usingIPD) { }

        public virtual float GetBatteryLevel() { return -1; }

        public virtual float GetBatteryTemperature() { return -1; }

        public virtual int GetBatteryStatus() { return -1; }

        public virtual float GetVolumeLevel() { return -1; }

        public virtual float GetGPUUtilLevel() { return 0; }

        public virtual float GetCPUUtilLevel() { return 0; }

        public virtual int GetCPULevel() { return 0; }

        public virtual int GetGPULevel() { return 0; }

        public virtual void SetPerformanceLevel(int cpuLevel, int gpuLevel) { }

        public virtual bool IsUserPresent() { return false; }

        public virtual bool IsFocusing() { return true; }

        public virtual bool IsVisible() { return true; }

        public virtual void SetEventCallback(Action<int> eventCallback) { }
        public virtual void GetEyeResolution(ref Vector2 resolution) { }
        public virtual void GetEyeFov(int eyeSide, ref YVRCameraRenderer.EyeFov eyeFov) { }

        public virtual float GetDisplayFrequency() { return 0.0f; }

        public virtual void SetDisplayFrequency(float value) { }

        public virtual float[] GetDisplayFrequenciesAvailable() { return new float[2] {30, 60}; }

        public virtual YVRCameraRenderer.LatencyData GetLatencyData() { return default; }

        public virtual bool GetBoundaryConfigured() { return false; }

        public virtual void TestBoundaryNode(YVRBoundary.BoundaryNode node,
            ref YVRBoundary.BoundaryTestResult testResult) { }

        public virtual void TestBoundaryPoint(Vector3 targetPoint, ref YVRBoundary.BoundaryTestResult testResult) { }

        public virtual Vector3 GetBoundaryDimensions() { return Vector3.zero; }

        public virtual bool GetBoundaryVisible() { return false; }

        public virtual void SetBoundaryVisible(bool visible) { }

        public virtual void SetBoundrayDetectionEnable(bool enable) { }

        public virtual Vector3[] GetBoundaryGeometry() { return null; }

        public virtual void SetFoveation(int ffrLevel) { }

        public virtual void SetASWEnable(bool enable) { }
        public virtual bool GetASWEnable() { return false; }

        public virtual void GetPrimaryController(ref uint controllerMask) {}

        public virtual void SetPrimaryController(uint controllerMask) {}
    }
}