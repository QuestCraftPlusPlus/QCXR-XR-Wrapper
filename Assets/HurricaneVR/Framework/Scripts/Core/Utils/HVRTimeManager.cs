using System;
using System.Collections;
using HurricaneVR.Framework.Shared.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Core.Utils
{
    public class HVRTimeManager : MonoBehaviour
    {
        [Tooltip("Called when slow motion is requested with the amount of time until full slow motion takes effect")]
        public HVRTimeEvent TimedSlowed = new HVRTimeEvent();
        [Tooltip("Called when time is requested to reset with the amount of time until timescale is reset")]
        public HVRTimeEvent TimeResumed = new HVRTimeEvent();

        [Header("Smoothing Settings")]
        [Tooltip("If true, the fixed time step will be averaged by FPS")]
        public bool SmoothFixedTimeStep = true;
        [Tooltip("Amount of frames to average time step over")]
        public int FrameSamples = 30;
        [Tooltip("Minimum time step allowed")]
        public int MinTimeStep = 72;
        [Tooltip("Maximum time step allowed")]
        public int MaxTimeStep = 144;


        [Header("Slow Motion Settings")]
        public float TimeScale = .5f;

        public bool SlowImmediately;
        public bool ResumeImmediately;

        [DrawIf("SlowImmediately", false)]
        public float TimeToSlow = .25f;
        [DrawIf("ResumeImmediately", false)]
        public float TimeToResume = .25f;

        public bool IsTimeSlowed { get; private set; }
        public float RefreshRate { get; private set; }

        [Header("Debug")] public bool Debug;
        public float DebugRefreshRate;
        public float DebugTimeScale;
        public bool SlowDown;

        private CircularBuffer<float> _buffer;
        private float averageFrameDelta;
        private Coroutine _slowRoutine;
        private Coroutine _resetRoutine;

        public static HVRTimeManager Instance { get; private set; }

        private void Awake()
        {

            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this);
                return;
            }
        }

        private void Start()
        {
            SetTimeStep();

            _buffer = new CircularBuffer<float>(FrameSamples);
            ResetBuffer();

            DebugRefreshRate = RefreshRate;
            DebugTimeScale = Time.timeScale;
        }

        private void SetTimeStep()
        {
            RefreshRate = UnityEngine.XR.XRDevice.refreshRate;
            if (RefreshRate < 60f)
            {
                RefreshRate = 90f;
            }

            Time.fixedDeltaTime = 1f / RefreshRate;
        }

        private void ResetBuffer()
        {
            for (int i = 0; i < FrameSamples; i++)
            {
                _buffer.Enqueue(Time.fixedDeltaTime);
            }
        }

        private void Update()
        {
            if (Debug)
            {
                DebugRefreshRate = RefreshRate;
                DebugTimeScale = Time.timeScale;
            }

            _buffer.Enqueue(Time.deltaTime);

            var sum = 0f;
            for (var i = 0; i < FrameSamples; i++)
            {
                sum += _buffer[i];
            }

            if (Mathf.Approximately(sum, 0f))
                return;

            var average = sum / FrameSamples;

            //if (HVRGlobalInputs.Instance.LeftPrimaryButtonState.JustActivated || HVRGlobalInputs.Instance.LeftTrackPadDown.JustActivated)
            //{
            //    ToggleTime();
            //}

            var min = 1f / MaxTimeStep * Time.timeScale;
            var max = 1f / MinTimeStep * Time.timeScale;

            averageFrameDelta = Mathf.Clamp(average, min, max);
            if (SmoothFixedTimeStep)
            {
                Time.fixedDeltaTime = averageFrameDelta;
            }
            else
            {
                SetTimeStep();
            }

            if (SlowDown)
            {
                ToggleTime();
                SlowDown = false;
            }
        }

        public void OverrideRefreshRate(float refresh)
        {
            RefreshRate = refresh;
            if (!SmoothFixedTimeStep)
            {
                Time.fixedDeltaTime = 1 / refresh;
            }
        }

        public void ResetRefreshRate()
        {
            RefreshRate = UnityEngine.XR.XRDevice.refreshRate;
            if (!SmoothFixedTimeStep)
            {
                Time.fixedDeltaTime = 1 / RefreshRate;
            }
        }

        public void ToggleTime()
        {
            if (IsTimeSlowed && _resetRoutine == null)
            {
                ResetTimeScale();
            }
            else if (!IsTimeSlowed && _slowRoutine == null)
            {
                SlowMo();
            }
        }

        private void SlowMo(float? timeScale = null)
        {
            if (IsTimeSlowed)
                return;

            IsTimeSlowed = true;

            TimedSlowed.Invoke(TimeToSlow);

            var scale = timeScale ?? TimeScale;

            if (SlowImmediately || TimeToResume < .001f)
            {
                Time.timeScale = scale;
                Time.fixedDeltaTime = RefreshRate * Time.timeScale;

                if (SmoothFixedTimeStep)
                {
                    ResetBuffer();
                }

                return;
            }

            _slowRoutine = StartCoroutine(SlowMotionRoutine(scale));
        }

        private void ResetTimeScale()
        {
            if (!IsTimeSlowed)
                return;

            IsTimeSlowed = false;

            TimeResumed.Invoke(TimeToResume);

            if (ResumeImmediately || TimeToResume < .001f)
            {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = RefreshRate;

                if (SmoothFixedTimeStep)
                {
                    ResetBuffer();
                }

                return;
            }

            _resetRoutine = StartCoroutine(ResetTimeRoutine());
        }

        private IEnumerator SlowMotionRoutine(float scale)
        {
            var elapsed = 0f;
            while (elapsed < TimeToSlow)
            {
                elapsed += Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Lerp(1f, scale, elapsed / TimeToSlow);
                Time.fixedDeltaTime = 1f / RefreshRate * Time.timeScale;
                if (SmoothFixedTimeStep)
                    ResetBuffer();
                yield return null;
            }

            Time.timeScale = scale;
            Time.fixedDeltaTime = 1f / RefreshRate * Time.timeScale;

            _slowRoutine = null;
        }

        private IEnumerator ResetTimeRoutine()
        {
            var elapsed = 0f;
            var start = Time.timeScale;
            while (elapsed < TimeToSlow)
            {
                elapsed += Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Lerp(start, 1f, elapsed / TimeToResume);
                Time.fixedDeltaTime = 1f / RefreshRate * Time.timeScale;
                if (SmoothFixedTimeStep)
                    ResetBuffer();
                yield return null;
            }

            Time.timeScale = 1f;
            Time.fixedDeltaTime = 1f / RefreshRate;
            _resetRoutine = null;
        }
    }

    [Serializable]
    public class HVRTimeEvent : UnityEvent<float> { }
}
