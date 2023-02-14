using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Depecrecated and left in for older projects, superceded by HVRPhysicsLever and HVRRotationTracker
    /// </summary>
    [RequireComponent(typeof(HVRGrabbable))]
    [RequireComponent(typeof(HingeJoint))]
    public class HVRLever : MonoBehaviour
    {
        [Header("Transforms")]
        [Tooltip("Target transform to rotate if the visual handle is not this object")]
        public Transform RotationTarget;

        [Tooltip("Used to track the delta angle change, use with TrackerAxis.")]
        public Transform Tracker;

        [Header("Settings")]

        [Tooltip("When the lever is released with Snap mode, should the grabbable handle snap to the RotationTarget")]
        public bool DiscardAngle;

        [Tooltip("Amount of steps the lever will click")]
        public int Steps;

        [Tooltip("Rotation Target snaps to the step angle size")]
        public bool SnapTarget = true;

        [Tooltip("Angle offset the lever starts in relation to LocalAxisStart field")]
        public float Offset;

        [Tooltip("Vector of the starting 0 angle of the lever.")]
        public Vector3 LocalAxisStart;
        [Tooltip("Choose an axis that isn't the same as the hinge axis")]
        public Vector3 TrackerAxis = new Vector3(0, 1, 0);


        [Tooltip("SFX to play whenever the lever snaps")]
        public AudioClip AudioClip;

        protected HingeJoint HingeJoint;

        [Header("Debug")]
        public float MinAngle;
        public float MaxAngle;
        public Vector3 LocalRotationAxis;
        public float StepSize;
        public int CurrentStep;
        public float CurrentAngle;

        public LeverMovedEvent Moved = new LeverMovedEvent();
        public LeverSteppedEvent Stepped = new LeverSteppedEvent();

        private float _previousAngle;
        private Vector3 _previousVector;
        private Vector3 _originalVector;
        private float _previousAngleFromStart;

        public HVRGrabbable Grabbable { get; private set; }
        public Rigidbody Rigidbody => Grabbable.Rigidbody;
        public HVRGrabberBase PrimaryGrabber => Grabbable.PrimaryGrabber;

        public float Range { get; set; }

        protected void Start()
        {
            if (!RotationTarget)
                RotationTarget = transform;

            Grabbable = GetComponent<HVRGrabbable>();

            HingeJoint = GetComponent<HingeJoint>();

            MinAngle = HingeJoint.limits.min;
            MaxAngle = HingeJoint.limits.max;

            Range = MaxAngle - MinAngle;

            LocalRotationAxis = HingeJoint.axis;

            if (Steps <= 1)
            {
                StepSize = 0f;
            }
            else if (Steps > 1 && Mathf.Approximately(StepSize, 0f))
            {
                StepSize = Range / Steps;
            }

            _originalVector = transform.TransformDirection(LocalAxisStart);

            _previousVector = LocalAxisStart;

            SetupStartRotation();

            transform.rotation = RotationTarget.rotation;

            Grabbable.Grabbed.AddListener(OnGrabbed);
            Grabbable.Released.AddListener(OnReleased);
        }



        protected virtual void FixedUpdate()
        {
            CheckForRotation();
        }

        private void CheckForRotation(bool force = false)
        {
            if (!force && Rigidbody.isKinematic && !PrimaryGrabber)
                return;

            var currentVector = Tracker.TransformDirection(TrackerAxis);
            var worldRotationAxis = transform.TransformDirection(LocalRotationAxis);

            var angleFromStart = Vector3.SignedAngle(_originalVector, currentVector, worldRotationAxis);
            angleFromStart += Offset;
            if ((int)angleFromStart < 0)
                angleFromStart = 360 + angleFromStart;

            var deltaAngle = Vector3.SignedAngle(_previousVector, currentVector, worldRotationAxis);

            CurrentAngle = angleFromStart;
            var stepAngle = angleFromStart;
            if (Steps > 1)
            {
                CurrentStep = Mathf.RoundToInt(angleFromStart / StepSize);
                stepAngle = CurrentStep * StepSize;

                if (!Mathf.Approximately(stepAngle, _previousAngle))
                {
                    OnStepChanged(CurrentStep);
                    _previousAngle = stepAngle;

                    if (SnapTarget)
                    {
                        var percent = Mathf.Approximately(0f, Range) ? 0f : stepAngle / Range;
                        OnAngleChanged(stepAngle, deltaAngle, percent);
                    }
                }
            }

            _previousVector = currentVector;

            var steppedVector = Quaternion.AngleAxis(stepAngle - Offset, worldRotationAxis) * _originalVector;
            var steppedAngle = Vector3.SignedAngle(currentVector, steppedVector, worldRotationAxis);
            var steppedRotation = Quaternion.AngleAxis(steppedAngle, worldRotationAxis) * transform.rotation;

            if (!SnapTarget && !Mathf.Approximately(_previousAngleFromStart, angleFromStart))
            {
                var percent = Mathf.Approximately(0f, Range) ? 0f : angleFromStart / Range;
                OnAngleChanged(angleFromStart, deltaAngle, percent);
            }

            if (SnapTarget)
            {
                RotationTarget.rotation = steppedRotation;
            }
            else
            {
                RotationTarget.rotation = transform.rotation;
            }

            _previousAngleFromStart = angleFromStart;
        }

        private void SetupStartRotation()
        {
            var currentVector = transform.TransformDirection(TrackerAxis);
            var worldRotationAxis = transform.TransformDirection(LocalRotationAxis);

            var angleFromStart = Vector3.SignedAngle(_originalVector, currentVector, worldRotationAxis);
            angleFromStart += Offset;
            if ((int)angleFromStart < 0)
                angleFromStart = 360 + angleFromStart;

            CurrentAngle = angleFromStart;
            var stepAngle = angleFromStart;
            if (Steps > 1)
            {
                CurrentStep = Mathf.RoundToInt(angleFromStart / StepSize);
                stepAngle = CurrentStep * StepSize;

                if (!Mathf.Approximately(stepAngle, _previousAngle))
                {
                    _previousAngle = stepAngle;
                }
            }

            _previousVector = currentVector;

            var steppedVector = Quaternion.AngleAxis(stepAngle - Offset, worldRotationAxis) * _originalVector;
            var steppedAngle = Vector3.SignedAngle(currentVector, steppedVector, worldRotationAxis);
            var steppedRotation = Quaternion.AngleAxis(steppedAngle, worldRotationAxis) * transform.rotation;

            if (SnapTarget)
            {
                RotationTarget.rotation = steppedRotation;
            }
            else
            {
                RotationTarget.rotation = transform.rotation;
            }

            _previousAngleFromStart = angleFromStart;
        }

        protected void OnGrabbed(HVRGrabberBase grabber, HVRGrabbable hvrGrabbable)
        {
            if (DiscardAngle)
            {
                transform.rotation = RotationTarget.rotation;
                transform.position = RotationTarget.position;
            }
        }

        protected void OnReleased(HVRGrabberBase grabber, HVRGrabbable hvrGrabbable)
        {
            if (DiscardAngle)
            {
                transform.rotation = RotationTarget.rotation;
                transform.position = RotationTarget.position;
            }
        }

        protected virtual void OnStepChanged(int step, bool raiseEvents = true)
        {
            if (AudioClip)
                if(SFXPlayer.Instance) SFXPlayer.Instance.PlaySFXRandomPitch(AudioClip, transform.position, .9f, 1.1f);
            if (raiseEvents)
                Stepped.Invoke(step);
        }

        protected virtual void OnAngleChanged(float angle, float delta, float percent, bool raiseEvents = true)
        {
            if (raiseEvents)
                Moved.Invoke(angle, delta, percent);
        }
    }
}
