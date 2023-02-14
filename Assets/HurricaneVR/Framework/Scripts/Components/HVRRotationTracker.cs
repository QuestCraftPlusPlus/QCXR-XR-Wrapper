using System;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Used to track the rotation in degrees about a defined axis of rotation.
    /// Degrees are reported from the starting rotation of the transform.
    /// </summary>
    public class HVRRotationTracker : MonoBehaviour
    {
        [Tooltip("Local axis of rotation")]
        public HVRAxis Axis;

        [Tooltip("Number of Steps")]
        public int Steps;

        [Tooltip("Threshold to reach to fire the Angle changed method and events")]
        public float AngleThreshold = 1f;

        [Tooltip("Max angle for Step Size calculation, should match limits set on whatever is controlling the rotational limits of this object")]
        public float MaximumAngle = 360f;

        public RotationTrackerStepEvent StepChanged = new RotationTrackerStepEvent();
        public RotationTrackerAngleEvent AngleChanged = new RotationTrackerAngleEvent();

        [Header("Debug")]

        public float StepSize;
        public int Step;

        private Vector3 _angleVector;
        private float _previousAngle;
        private Vector3 _trackAxis;
        private Vector3 _startVector;

        [SerializeField]
        private float _clampedAngle;
        [SerializeField]
        private float _angleFromStart;

        public float ClampedAngle { get; private set; }
        public float Angle { get; private set; }

        public float UnsignedAngle => Mathf.Abs(Angle);

        public Vector3 AxisOfRotation { get; private set; }

        protected virtual void Start()
        {
            AxisOfRotation = Axis.GetVector();
            AxisOfRotation.Normalize();
            _trackAxis = AxisOfRotation.OrthogonalVector();

            _startVector = transform.localRotation * _trackAxis;

            if (Steps <= 1)
            {
                StepSize = 0f;
            }
            else if (Steps > 1 && Mathf.Approximately(StepSize, 0f))
            {
                StepSize = MaximumAngle / Steps;
            }

            _angleVector = _startVector;
        }

        protected virtual void Update()
        {
            var currentVector = transform.localRotation * _trackAxis;

            ClampedAngle = Vector3.SignedAngle(_startVector, currentVector, AxisOfRotation);

            if (ClampedAngle < 0)
            {
                ClampedAngle += 360;
            }

            var angleDelta = Vector3.SignedAngle(_angleVector, currentVector, AxisOfRotation);
            if (Mathf.Abs(angleDelta) > AngleThreshold)
            {
                _angleVector = currentVector;
                Angle += angleDelta;
                OnAngleChanged(ClampedAngle, angleDelta);
            }

            var stepAngle = ClampedAngle;
            if (Steps > 1)
            {
                Step = Mathf.RoundToInt(ClampedAngle / StepSize);
                stepAngle = Step * StepSize;
                if (Mathf.Approximately(stepAngle, 360))
                {
                    Step = 0;
                }
            }


            if (Steps > 1)
            {
                if (!Mathf.Approximately(stepAngle, _previousAngle))
                {
                    OnStepChanged(Step, true);
                    _previousAngle = stepAngle;
                }
            }

            _angleFromStart = Angle;
            _clampedAngle = ClampedAngle;
        }

        protected virtual void OnStepChanged(int step, bool raiseEvents)
        {
            StepChanged.Invoke(step);
        }

        protected virtual void OnAngleChanged(float angle, float delta)
        {
            AngleChanged.Invoke(angle, delta);
        }
    }

    [Serializable]
    public class RotationTrackerAngleEvent : UnityEvent<float, float> { }


    [Serializable]
    public class RotationTrackerStepEvent : UnityEvent<int> { }
}