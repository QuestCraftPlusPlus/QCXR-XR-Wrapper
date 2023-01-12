using UnityEngine;
using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;

namespace YVR.Core
{
    [ExcludeFromDocs]
    public abstract partial class YVRPlugin
    {
        private static YVRPlugin instance;

        public static YVRPlugin Instance
        {
            get
            {
                if (instance == null)
                {
                    if (!Application.isEditor && Application.platform == RuntimePlatform.Android)
                        instance = YVRPluginAndroid.Create();
                    else
                        instance = YVRPluginWin.Create();
                }

                return instance;
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
            public byte BatteryPercentRemaining; //21
            public byte isCharging; // 22
            public byte Padding1; // 23
            public byte Padding2; // 24
        }

        public virtual void SetTrackingSpace(YVRTrackingStateManager.TrackingSpace trackingSpace) { }
        public virtual void SetVSyncCount(YVRQualityManager.VSyncCount vSyncCount) { }
        public virtual void RecenterTracking() { }

        public virtual ControllerState GetControllerState(uint controllerMask) { return new ControllerState(); }

        public virtual bool GetControllerConnected(uint controllerMask) { return false; }

        public virtual bool GetControllerPositionTracked(uint controllerMask) { return false; }

        public virtual bool GetControllerOrientationTracked(uint controllerMask) { return false; }

        public virtual bool GetHeadsetPositionTracked() { return false; }

        public virtual bool GetHeadsetOrientationTracked() { return false; }

        public virtual void SetControllerVibration(uint controllerMask, float frequency, float amplitude) { return; }

        public virtual void SetUsingIPDInPositionTracking(bool usingIPD) { }

        public virtual float GetBatteryLevel() { return -1; }

        public virtual float GetBatteryTemperature() { return -1; }

        public virtual int GetBatteryStatus() { return -1; }

        public virtual float GetVolumeLevel() { return -1; }

        public virtual bool IsPowerSavingActive() { return false; }

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

        public virtual float[] GetDisplayFrequenciesAvailable() { return new float[2] { 30, 60 }; }

        public virtual YVRCameraRenderer.LatencyData GetLatencyData() { return default; }

        public virtual bool GetBoundaryConfigured() { return false; }

        public virtual void TestBoundaryNode(YVRBoundary.BoundaryNode node,
            ref YVRBoundary.BoundaryTestResult testResult) { }

        public virtual void TestBoundaryPoint(Vector3 targetPoint, ref YVRBoundary.BoundaryTestResult testResult) { }

        public virtual Vector3 GetBoundaryDimensions() { return Vector3.zero; }

        public virtual bool GetBoundaryVisible() { return false; }

        public virtual void SetBoundaryVisible(bool visible) { }

        public virtual Vector3[] GetBoundaryGeometry() { return null; }

        public virtual void SetFoveation(int ffrLevel) { }

        public virtual void SetASWEnable(bool enable) { }
        public virtual bool GetASWEnable() { return false; }

        public virtual int GetPrimaryController(ref uint controllerMask) { return -1; }

        public virtual int SetPrimaryController(uint controllerMask) { return -1; }
    }
}