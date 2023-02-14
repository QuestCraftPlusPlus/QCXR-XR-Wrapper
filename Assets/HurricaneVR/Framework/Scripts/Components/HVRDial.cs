using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Utils;
using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Deprecated and left in for older projects. Superceded by HVRPhysicsDial and HVRRotationTracker
    /// </summary>
    [RequireComponent(typeof(HVRGrabbable))]
    public class HVRDial : MonoBehaviour
    {
        public DialSteppedEvent DialStepChanged = new DialSteppedEvent();
        public DialTurnedEvent DialTurned = new DialTurnedEvent();

        public Transform RotationTarget;
        public int Steps;
        public bool SnapTarget = true;
        public bool ClampMaxAngle;
        public float MaximumAngle = 360f;
        public AudioClip AudioClip;
        public float Deadzone = 1f;
        public Vector3 LocalRotationAxis;
        public Vector3 LocalAxisStart;
        public bool DiscardAngle;

        public float StepSize;

        private float _previousAngle;
        private Vector3 _originalVector;
        private Rigidbody _rigidBody;
        private Rigidbody _targetRigidBody;
        private float _previousAngleFromStart;

        public int Step { get; protected set; }

        public HVRGrabbable Grabbable { get; private set; }
        public HVRHandGrabber PrimaryGrabber { get; private set; }

        protected virtual void Start()
        {
            _rigidBody = transform.GetComponent<Rigidbody>();
            _targetRigidBody = RotationTarget.GetComponent<Rigidbody>();

            if (Steps <= 1)
                StepSize = 0f;
            else if (Steps > 1 && Mathf.Approximately(StepSize, 0f))
                StepSize = MaximumAngle / Steps;

            LocalAxisStart.Normalize();
            LocalRotationAxis.Normalize();

            _originalVector = transform.localRotation * LocalAxisStart;

            Grabbable = GetComponent<HVRGrabbable>();
            Grabbable.Grabbed.AddListener(OnGrabbed);
            Grabbable.Released.AddListener(OnReleased);


            _previousAngle = 0f;
            _previousAngleFromStart = 0f;
        }

        protected virtual void FixedUpdate()
        {
            //Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(currentVector).normalized * 1f, Color.red);
            CheckForRotation();
        }


        protected void CheckForRotation(bool force = false)
        {
            if (force || PrimaryGrabber)
            {
                if (!force && Mathf.Abs(PrimaryGrabber.HVRTrackedController.DeltaEulerZ) < Deadzone)
                    return;

                var currentVector = transform.localRotation * LocalAxisStart;
                var rotationAxis = LocalRotationAxis;

                var controllerRotation = new Vector3(
                    PrimaryGrabber.HVRTrackedController.DeltaEulerZ,
                    PrimaryGrabber.HVRTrackedController.DeltaEulerZ,
                    PrimaryGrabber.HVRTrackedController.DeltaEulerZ);

                controllerRotation.Scale(-rotationAxis);

                var rotation = Quaternion.Euler(controllerRotation);

                var newRotation = transform.localRotation * rotation;

                var newVector = newRotation * LocalAxisStart;

                var delta = -PrimaryGrabber.HVRTrackedController.DeltaEulerZ;

                var angleFromStart = Vector3.SignedAngle(_originalVector, newVector, rotationAxis);

                if (angleFromStart < 0)
                    angleFromStart = 360 + angleFromStart;

                var clockWise = delta >= 0f;

                if (ClampMaxAngle)
                {

                    if (clockWise && _previousAngleFromStart + delta >= MaximumAngle - .1)
                    {
                        angleFromStart = MaximumAngle;
                    }
                    else if (!clockWise && _previousAngleFromStart + delta <= 0.1)
                    {
                        angleFromStart = 0;
                    }
                    else if (angleFromStart > MaximumAngle)
                    {
                        float upDiff = 360 - angleFromStart;
                        float lowerDiff = angleFromStart - MaximumAngle;

                        if (upDiff < lowerDiff) angleFromStart = 0;
                        else angleFromStart = MaximumAngle;
                    }
                }

                var stepAngle = angleFromStart;
                if (Steps > 1)
                {
                    Step = Mathf.RoundToInt(angleFromStart / StepSize);
                    stepAngle = Step * StepSize;
                    if (Mathf.Approximately(stepAngle, 360))
                    {
                        Step = 0;
                    }
                }

                var finalVector = Quaternion.AngleAxis(angleFromStart, rotationAxis) * _originalVector;
                var syncAngle = Vector3.SignedAngle(currentVector, finalVector, rotationAxis);
                var syncRotation = Quaternion.AngleAxis(syncAngle, rotationAxis) * transform.localRotation;

                if (Steps > 1)
                {
                    if (!Mathf.Approximately(stepAngle, _previousAngle))
                    {
                        OnStepChanged(Step, true);
                        _previousAngle = stepAngle;

                        if (SnapTarget)
                        {
                            var percent = Mathf.Approximately(0f, MaximumAngle) ? 0f : stepAngle / MaximumAngle;
                            OnAngleChanged(stepAngle, syncAngle, percent, true);
                        }
                    }
                }

                if (!SnapTarget && !Mathf.Approximately(_previousAngleFromStart, angleFromStart))
                {
                    var percent = Mathf.Approximately(0f, MaximumAngle) ? 0f : angleFromStart / MaximumAngle;
                    OnAngleChanged(angleFromStart, syncAngle, angleFromStart / percent, true);
                }

                var steppedVector = Quaternion.AngleAxis(stepAngle, rotationAxis) * _originalVector;
                var steppedAngle = Vector3.SignedAngle(currentVector, steppedVector, rotationAxis);
                var steppedRotation = Quaternion.AngleAxis(steppedAngle, rotationAxis) * transform.localRotation;

                var targetRotation = SnapTarget ? steppedRotation : syncRotation;

                //if (_targetRigidBody)
                //{
                //    _targetRigidBody.MoveRotation(_targetRigidBody.rotation * targetRotation);
                //}
                //else
                {
                    RotationTarget.localRotation = targetRotation;
                }

                //if (_rigidBody)
                //{
                //    _rigidBody.MoveRotation(_rigidBody.rotation * syncRotation);
                //}
                //else
                {
                    transform.localRotation = syncRotation;
                }

                _previousAngleFromStart = angleFromStart;
            }
        }



        protected void OnGrabbed(HVRGrabberBase grabber, HVRGrabbable hvrGrabbable)
        {
            PrimaryGrabber = grabber as HVRHandGrabber;

            if (DiscardAngle)
            {
                if (_rigidBody)
                {
                    _rigidBody.MoveRotation(RotationTarget.rotation);
                }
                else
                {
                    transform.localRotation = RotationTarget.localRotation;
                }
            }
        }

        protected void OnReleased(HVRGrabberBase grabber, HVRGrabbable hvrGrabbable)
        {
            PrimaryGrabber = null;

            if (DiscardAngle)
            {
                if (_rigidBody)
                {
                    _rigidBody.MoveRotation(RotationTarget.rotation);
                }
                else
                {
                    transform.localRotation = RotationTarget.localRotation;
                }
            }
        }

        protected virtual void OnStepChanged(int step, bool raiseEvents)
        {
            if (AudioClip)
                if(SFXPlayer.Instance) SFXPlayer.Instance.PlaySFXRandomPitch(AudioClip, transform.position, .9f, 1.1f);
            if (raiseEvents)
                DialStepChanged.Invoke(step);
        }

        protected virtual void OnAngleChanged(float angle, float delta, float percent, bool raiseEvents)
        {
            if (raiseEvents)
                DialTurned.Invoke(angle, delta, percent);
        }


        protected virtual void Update()
        {
        }
    }
}
