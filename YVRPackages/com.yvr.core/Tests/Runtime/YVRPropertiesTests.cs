using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace YVR.Core.Test
{
    [TestFixture]
    public class YVRPropertiesTests
    {
        [OneTimeSetUp]
        public void Init() { }


        [Test]
        public void GetEyeResolution_NotZero()
        {
            YVRCameraRenderer renderer = YVRManager.instance.cameraRenderer;
            YVRCameraRenderer.EyeRenderDescription description = renderer.GetEyeRenderDesc(EyeSide.Left);
            Debug.Log($"Eye resolution is {description.resolution}");

            Assert.That(!Mathf.Approximately(description.resolution.x, 0));
            Assert.That(!Mathf.Approximately(description.resolution.y, 0));
        }

        [Test]
        public void GetCPULevel_NotNegative()
        {
            int level = YVRManager.instance.cpuLevel;
            Debug.Log($"CPU Level is {level}");
            Assert.That(level >= 0);
        }

        private static int[] s_TargetCPULevel = new int[] {0, 1, 2, 3, 4};

        [Test]
        public void GetCPULevel_JustSet_EqualsSetValue([ValueSource(nameof(s_TargetCPULevel))] int cpuLevel)
        {
            YVRManager.instance.cpuLevel = cpuLevel;
            int level = YVRManager.instance.cpuLevel;
            Debug.Log($"CPU Level is {level}");
            Assert.That(level >= 0 && level == cpuLevel);
        }

        private static int[] s_TargetGPULevel = new int[] {0, 1, 2, 3, 4};

        [Test]
        public void GetGPULevel_JustSet_EqualsSetValue([ValueSource(nameof(s_TargetGPULevel))] int gpuLevel)
        {
            YVRManager.instance.gpuLevel = gpuLevel;
            int level = YVRManager.instance.gpuLevel;
            Debug.Log($"CPU Level is {level}");
            Assert.That(level >= 0 && level == gpuLevel);
        }

        [Test]
        public void GetGPULevel_NotNegative()
        {
            int level = YVRManager.instance.gpuLevel;
            Debug.Log($"GPU Level is {level}");
            Assert.That(level >= 0);
        }

        [Test]
        public void GetCPUUtilities_NotNegative()
        {
            float utilities = YVRManager.instance.cpuUtilLevel;
            Debug.Log($"CPU utilities is {utilities}");
            Assert.That(utilities >= 0);
        }

        [Test]
        public void GetGPUUtilities_NotNegative()
        {
            float utilities = YVRManager.instance.gpuUtilLevel;
            Debug.Log($"GPU utilities is {utilities}");
            Assert.That(utilities >= 0);
        }

        [Test]
        public void GetVolume_NotNegative()
        {
            float volume = YVRManager.instance.volumeLevel;
            Debug.Log($"Volume is {volume}");
            Assert.That(volume > 0);
        }

        [Test]
        public void GetBatteryStatus_NotNegative()
        {
            int status = YVRManager.instance.batteryStatus;
            Debug.Log($"Battery State is {status}");
            Assert.That(status >= 0);
        }

        [Test]
        public void GetBatteryTemperature_NotNegative()
        {
            LogAssert.ignoreFailingMessages = true;
            float temperature = YVRManager.instance.batteryTemperature;
            Debug.Log($"Battery temperature is {temperature}");
            Assert.That(temperature > 0);
        }

        [Test]
        public void GetBatteryLevel_NotNegative()
        {
            float battery = YVRManager.instance.batteryLevel;
            Debug.Log($"Battery Level is {battery}");
            Assert.That(battery > 0);
        }

        private static int[] s_TestDisplayFrequency = new int[] {72, 90};

        [UnityTest]
        public IEnumerator GetDisplayRate_JustSet_EqualsSetValue(
            [ValueSource(nameof(s_TestDisplayFrequency))]
            int targetRate)
        {
            YVRManager.instance.cameraRenderer.displayFrequency = targetRate;
            yield return new WaitForSecondsRealtime(1);
            Assert.That(Math.Abs(targetRate - YVRManager.instance.cameraRenderer.displayFrequency) < 0.01f);
        }

        [Test]
        public void GetAvailableDisplayFrequenciesNum_NotNegative()
        {
            int num = YVRManager.instance.cameraRenderer.displayFrequenciesAvailable.Length;
            Debug.Log($"Available Frequencies number is {num}");
            Assert.That(num > 0);
        }

        [Test]
        public void GetAvailableDisplayFrequenciesNum_ElementNotNegative()
        {
            float[] frequencies = YVRManager.instance.cameraRenderer.displayFrequenciesAvailable;
            bool allPositive = true;
            foreach (float frequency in frequencies)
            {
                Debug.Log($"Frequency is {frequency}");
                allPositive &= frequency > 0;
            }

            Assert.That(allPositive);
        }
    }
}