using UnityEngine;
using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;

namespace YVR.Core
{
    [ExcludeFromDocs]
    public class YVRPluginAndroid : YVRPlugin
    {
        [DllImport("yvrplugin")]
        private static extern void YVRSetVSyncCount(int vSyncCount);

        [DllImport("yvrplugin")]
        private static extern void YVRRecenterPose();

        [DllImport("yvrplugin")]
        private static extern int YVRSetTrackingSpace(int trackingSpace);

        [DllImport("yvrplugin")]
        private static extern void YVRSetControllerVibration(uint controllerMask, float frequency, float amplitude);

        [DllImport("yvrplugin")]
        private static extern float YVRGetBatteryLevel();

        [DllImport("yvrplugin")]
        private static extern float YVRGetBatteryTemperature();

        [DllImport("yvrplugin")]
        private static extern int YVRGetBatteryStatus();

        [DllImport("yvrplugin")]
        private static extern float YVRGetVolumeLevel();

        [DllImport("yvrplugin")]
        private static extern float YVRGetGpuUtilization();

        [DllImport("yvrplugin")]
        private static extern float YVRGetCpuUtilization();

        [DllImport("yvrplugin")]
        private static extern void YVRSetPerformanceLevel(int cpulevel, int gpuLevel);

        [DllImport("yvrplugin")]
        private static extern int YVRGetCpuLevel();

        [DllImport("yvrplugin")]
        private static extern int YVRGetGpuLevel();

        [DllImport("yvrplugin")]
        private static extern void YVRSetUsingIPDInPositionTracking(bool usingIPD);

        [DllImport("yvrplugin")]
        private static extern float YVRGetDisplayFrequency();

        [DllImport("yvrplugin")]
        private static extern void YVRSetDisplayFrequency(float freshRate);

        [DllImport("yvrplugin")]
        private static extern int YVRGetDisplayAvailableFrequenciesNum();

        [DllImport("yvrplugin")]
        private static extern void YVRGetDisplayAvailableFrequencies(float[] frequenciesArray);

        [DllImport("yvrplugin")]
        private static extern void YVRGetLatencyData(ref YVRCameraRenderer.LatencyData latencyData);

        [DllImport("yvrplugin")]
        private static extern void YVRGetEyeResolution(ref Vector2 resolution);

        [DllImport("yvrplugin")]
        private static extern void YVRGetEyeFov(int eyeSide, ref YVRCameraRenderer.EyeFov eyeFov);

        [DllImport("yvrplugin")]
        private static extern bool YVRIsUserPresent();

        [DllImport("yvrplugin")]
        private static extern bool YVRIsFocusing();

        [DllImport("yvrplugin")]
        private static extern bool YVRIsVisible();

        [DllImport("yvrplugin")]
        private static extern void YVRSetEventCallback(Action<int> callback);

        [DllImport("yvrplugin")]
        private static extern bool YVRGetBoundaryConfigured();

        [DllImport("yvrplugin")]
        private static extern void YVRTestBoundaryNode(YVRBoundary.BoundaryNode targetNode,
            ref YVRBoundary.BoundaryTestResult testResult);

        [DllImport("yvrplugin")]
        private static extern void YVRTestBoundaryPoint(Vector3 targetPoint,
            ref YVRBoundary.BoundaryTestResult testResult);

        [DllImport("yvrplugin")]
        private static extern Vector3 YVRGetBoundaryDimensions();

        [DllImport("yvrplugin")]
        private static extern bool YVRGetBoundaryVisible();

        [DllImport("yvrplugin")]
        private static extern void YVRSetBoundaryVisible(bool visible);

        [DllImport("yvrplugin")]
        private static extern int YVRGetBoundaryGeometryPointsCount();

        [DllImport("yvrplugin")]
        private static extern void YVRGetBoundaryGeometry(Vector3[] geometry);

        [DllImport("yvrplugin")]
        private static extern void YVRSetFoveation(int ffrLevel);

        [DllImport("yvrplugin")]
        private static extern void YVRSetASWEnable(bool enable);

        [DllImport("yvrplugin")]
        private static extern bool YVRGetASWEnable();

        [DllImport("yvrplugin")]
        private static extern void YVRGetPrimaryController(ref uint controllerMask);

        [DllImport("yvrplugin")]
        private static extern void YVRSetPrimaryController(uint controllerMask);

        [DllImport("yvrplugin")]
        private static extern void YVRSetBoundaryDetectionEnable(bool enable);
        //---------------------------------------------------------------------------------------------

        public static YVRPluginAndroid Create()
        {
            return new YVRPluginAndroid();
        }

        public override void SetTrackingSpace(YVRTrackingStateManager.TrackingSpace trackingSpace)
        {
            YVRSetTrackingSpace((int) trackingSpace);
        }

        public override void SetVSyncCount(YVRQualityManager.VSyncCount vSyncCount)
        {
            YVRSetVSyncCount((int) vSyncCount);
        }

        public override void RecenterTracking() { YVRRecenterPose(); }

        public override void SetUsingIPDInPositionTracking(bool usingIPD)
        {
            YVRSetUsingIPDInPositionTracking(usingIPD);
        }

        public override void SetControllerVibration(uint controllerMask, float frequency, float amplitude)
        {
            YVRSetControllerVibration(controllerMask, frequency, amplitude);
        }

        public override float GetBatteryLevel() { return YVRGetBatteryLevel(); }

        public override float GetBatteryTemperature() { return YVRGetBatteryTemperature(); }

        public override int GetBatteryStatus() { return YVRGetBatteryStatus(); }

        public override float GetVolumeLevel() { return YVRGetVolumeLevel(); }

        public override float GetGPUUtilLevel() { return YVRGetGpuUtilization(); }

        public override float GetCPUUtilLevel() { return YVRGetCpuUtilization(); }

        public override int GetCPULevel() { return YVRGetCpuLevel(); }

        public override int GetGPULevel() { return YVRGetGpuLevel(); }

        public override void SetPerformanceLevel(int cpuLevel, int gpuLevel)
        {
            YVRSetPerformanceLevel(cpuLevel, gpuLevel);
        }

        public override void GetEyeResolution(ref Vector2 resolution) { YVRGetEyeResolution(ref resolution); }

        public override void GetEyeFov(int eyeSide, ref YVRCameraRenderer.EyeFov eyeFov)
        {
            YVRGetEyeFov(eyeSide, ref eyeFov);
        }

        private float[] m_CacheAvailableFrequencies = null;

        public override float[] GetDisplayFrequenciesAvailable()
        {
            if (m_CacheAvailableFrequencies == null)
            {
                int availableFrequenciesNum = YVRGetDisplayAvailableFrequenciesNum();
                m_CacheAvailableFrequencies = new float[availableFrequenciesNum];
                YVRGetDisplayAvailableFrequencies(m_CacheAvailableFrequencies);
            }

            return m_CacheAvailableFrequencies;
        }

        public override float GetDisplayFrequency() { return YVRGetDisplayFrequency(); }
        public override void SetDisplayFrequency(float displayFrequency) { YVRSetDisplayFrequency(displayFrequency); }


        public override YVRCameraRenderer.LatencyData GetLatencyData()
        {
            YVRCameraRenderer.LatencyData latencyData = new YVRCameraRenderer.LatencyData();
            YVRGetLatencyData(ref latencyData);
            return latencyData;
        }

        public override bool IsUserPresent() { return YVRIsUserPresent(); }

        public override bool IsFocusing() { return YVRIsFocusing(); }

        public override bool IsVisible() { return YVRIsVisible(); }

        public override void SetEventCallback(Action<int> eventCallback) { YVRSetEventCallback(eventCallback); }

        public override bool GetBoundaryConfigured() { return YVRGetBoundaryConfigured(); }

        public override void TestBoundaryNode(YVRBoundary.BoundaryNode targetNode,
            ref YVRBoundary.BoundaryTestResult testResult)
        {
            YVRTestBoundaryNode(targetNode, ref testResult);
        }

        public override void TestBoundaryPoint(Vector3 targetPoint, ref YVRBoundary.BoundaryTestResult testResult)
        {
            YVRTestBoundaryPoint(targetPoint, ref testResult);
        }

        public override Vector3 GetBoundaryDimensions() { return YVRGetBoundaryDimensions(); }

        public override bool GetBoundaryVisible() { return YVRGetBoundaryVisible(); }

        public override void SetBoundaryVisible(bool visible) { YVRSetBoundaryVisible(visible); }

        public override void SetBoundrayDetectionEnable(bool enable)
        {
            YVRSetBoundaryDetectionEnable(enable);
        }

        public override Vector3[] GetBoundaryGeometry()
        {
            int pointsCount = YVRGetBoundaryGeometryPointsCount();
            Vector3[] result = new Vector3[pointsCount];
            if (pointsCount > 0)
                YVRGetBoundaryGeometry(result);

            return result;
        }

        public override void SetFoveation(int ffrLevel) { YVRSetFoveation(ffrLevel); }

        public override void SetASWEnable(bool enable) { YVRSetASWEnable(enable); }
        public override bool GetASWEnable() { return YVRGetASWEnable(); }

        public override void GetPrimaryController(ref uint controllerMask)
        {
            YVRGetPrimaryController(ref controllerMask);
        }

        public override void SetPrimaryController(uint controllerMask) { YVRSetPrimaryController(controllerMask); }
    }
}