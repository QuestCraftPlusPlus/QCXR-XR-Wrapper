using System.Collections;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// This component helps setup the basic functionality of a hinged door.
    /// Includes locking and latching capability. Handle required rotation for unlatching.
    /// </summary>
    [HelpURL("https://cloudwalker2020.github.io/HurricaneVR-Docs/manual/components/door.html")]
    [RequireComponent(typeof(HVRRotationTracker))]
    [RequireComponent(typeof(Rigidbody))]
    public class HVRPhysicsDoor : MonoBehaviour
    {
        [Header("Settings")] [Tooltip("Local axis of rotation")]
        public HVRAxis Axis;

        [Tooltip("Door's rigidbody mass.")] public float Mass = 10f;

        public bool DisableGravity = true;

        [Tooltip("If true the door and it's handle will have their joint limit's locked on start.")]
        public bool StartLocked;

        [Tooltip("Rigidbody to connect the joint to")]
        public Rigidbody ConnectedBody;

        [Header("Door Closing Settings")] [Tooltip("Angle threshold to determine if the door is closed or not.")]
        public float CloseAngle = 5f;

        [Tooltip("The door will automatically shut over this amount of time once it's close enough to be closed.")]
        public float CloseOverTime = .25f;

        [Tooltip("How long the door angle must be below 'CloseAngle' to become closed.")]
        public float CloseDetectionTime = .5f;

        [Header("SFX")] [Tooltip("Angle threshold to play opening and closing sound effects.")]
        public float SFXThresholdAngle = 2.5f;

        public float SFXResetThreshold = 1f;
        public AudioClip SFXOpened;
        public AudioClip SFXClosed;

        [Tooltip("Delay before the open / close sfx can be played again")]
        public float SFXTimeout = 1f;

        [Tooltip("Optional transform to define the position of the open / close sound fx.")]
        public Transform SFXPosition;

        [Header("Handle")] [Tooltip("If true the handle must rotate beyond 'HandThreshold' amount of degrees before it will unlatch, if false the door will not latch automatically.")]
        public bool HandleRequiresRotation;

        [Tooltip("Required handle rotation to unlatch the door.")]
        public float HandleThreshold = 45f;

        [Tooltip("The rotation tracker that reports the amount of rotation of the handle.")]
        public HVRRotationTracker HandleRotationTracker;

        [Tooltip("If provided (and held) the door will not automatically shut when it is below 'CloseAngle' in degrees.")]
        public HVRGrabbable HandleGrabbable;

        [Tooltip("Rotational physics component that let's this door component lock the door handle's rotation when the door locks.")]
        public HVRPhysicsDial DoorKnob;


        [Tooltip("The rotation tracker that reports the amount of rotation of the handle.")]
        public HVRRotationTracker SecondHandleRotationTracker;

        [Tooltip("If provided (and held) the door will not automatically shut when it is below 'CloseAngle' in degrees.")]
        public HVRGrabbable SecondHandleGrabbable;

        [Tooltip("Rotational physics component that let's this door component lock the door handle's rotation when the door locks.")]
        public HVRPhysicsDial SecondDoorKnob;

        [Header("Joint Limits")] public bool LimitRotation = true;

        [Tooltip("Minimum Angle about the axis of rotation")]
        public float MinAngle;

        [Tooltip("Maximum rotation about the axis of rotation")]
        public float MaxAngle;

        [Header("Joint Settings")] [Tooltip("Angular Damper of the door hinge.")]
        public float Damper = 10;

        [Tooltip("Angular Spring that will return the door to it's starting rotation")]
        public float Spring;

        //[Header("Editor")]
        //[SerializeField]
        //protected Quaternion JointStartRotation;

        [Header("Debugging")] public float TargetAngularVelocity = 0f;
        public bool DoorLatched;
        public bool DoorClosed;
        public bool Opened;
        public bool Closed;
        public bool PreviousDoorLatched;
        public bool PreviousClosed;
        public bool VerboseLogging;
        public bool Locked;

        public Rigidbody Rigidbody { get; private set; }

        public HVRRotationTracker Tracker { get; private set; }

        protected ConfigurableJoint Joint { get; set; }

        private Quaternion _startRotation;
        private bool _doorClosing;
        private float _detectionTimer;
        private float _lastClosedSFXTime;
        private float _lastOpenedSFXTime;

        public virtual void Start()
        {
            Rigidbody = this.GetRigidbody();
            Rigidbody.mass = Mass;
            Rigidbody.useGravity = !DisableGravity;

            Tracker = GetComponent<HVRRotationTracker>();

            if (MinAngle > 0)
            {
                MinAngle *= -1;
            }

            if (MaxAngle < 0)
            {
                MaxAngle *= -1;
            }

            MinAngle = Mathf.Clamp(MinAngle, MinAngle, 0);
            MaxAngle = Mathf.Clamp(MaxAngle, 0, MaxAngle);

            if (HandleRequiresRotation)
            {
                if (!HandleRotationTracker)
                {
                    Debug.LogError("HandleRotationTracker not assigned.");
                }

                DoorLatched = true;
            }

            DoorClosed = true;
            PreviousDoorLatched = DoorLatched;
            PreviousClosed = DoorClosed;

            SetupJoint();

            _startRotation = transform.localRotation;

            if (DoorLatched)
            {
                LockDoorJoint();
            }

            if (StartLocked)
            {
                Lock();
            }

            //set initial values to prevent sfx on start
            if (Tracker.UnsignedAngle < SFXThresholdAngle)
            {
                Closed = true;
            }
            else if (Tracker.UnsignedAngle > SFXThresholdAngle)
            {
                Opened = true;
            }
        }

        protected virtual void Update()
        {
            Joint.targetAngularVelocity = new Vector3(TargetAngularVelocity, 0f, 0f);

            if (_doorClosing)
                return;

            if (Tracker.UnsignedAngle < CloseAngle)
            {
                _detectionTimer += Time.deltaTime;
            }
            else if (Tracker.UnsignedAngle >= CloseAngle)
            {
                _detectionTimer = 0f;
                DoorClosed = false;
            }

            if (HandleGrabbable && HandleGrabbable.IsBeingHeld || SecondHandleGrabbable && SecondHandleGrabbable.IsBeingHeld)
            {
                _detectionTimer = 0f;
            }

            if (_detectionTimer > CloseDetectionTime)
            {
                DoorClosed = true;
            }

            if (!PreviousClosed && DoorClosed)
            {
                OnDoorClosed();
            }
            else if (PreviousClosed && !DoorClosed)
            {
                OnDoorOpened();
            }

            var reset = SFXResetThreshold;
            if (SFXResetThreshold > SFXThresholdAngle)
            {
                reset = SFXThresholdAngle * .5f;
            }

            if (!Opened && Tracker.UnsignedAngle > SFXThresholdAngle && Time.time - _lastOpenedSFXTime > SFXTimeout)
            {
                _lastOpenedSFXTime = Time.time;
                Opened = true;
                PlayOpenedSFX();
            }
            else if (!Closed && Tracker.UnsignedAngle < SFXThresholdAngle && Time.time - _lastClosedSFXTime > SFXTimeout)
            {
                _lastClosedSFXTime = Time.time;
                Closed = true;
                PlayClosedSFX();
            }
            else if (Opened && Tracker.UnsignedAngle < SFXThresholdAngle - reset)
            {
                Opened = false;
            }
            else if (Closed && Tracker.UnsignedAngle > SFXThresholdAngle + reset)
            {
                Closed = false;
            }

            if (HandleRequiresRotation)
            {
                if (HandleRotationTracker.UnsignedAngle >= HandleThreshold ||
                    (SecondHandleRotationTracker && SecondHandleRotationTracker.UnsignedAngle >= HandleThreshold))
                {
                    DoorLatched = false;
                }
                else if (HandleRotationTracker.UnsignedAngle < HandleThreshold &&
                         (!SecondHandleRotationTracker || SecondHandleRotationTracker.UnsignedAngle < HandleThreshold) &&
                         Tracker.UnsignedAngle < CloseAngle)
                {
                    DoorLatched = true;
                }

                if (!Locked)
                {
                    if (PreviousDoorLatched && !DoorLatched)
                    {
                        OnDoorUnLatched();
                    }
                    else if (!PreviousDoorLatched && DoorLatched)
                    {
                        OnDoorLatched();
                    }
                }
            }

            PreviousDoorLatched = DoorLatched;
            PreviousClosed = DoorClosed;
        }

        protected virtual Vector3 GetSFXPosition()
        {
            var position = transform.position;
            if (SFXPosition)
            {
                position = SFXPosition.position;
            }

            return position;
        }

        protected virtual void PlayClosedSFX()
        {
            if (SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(SFXClosed, GetSFXPosition());
        }


        protected virtual void PlayOpenedSFX()
        {
            if (SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(SFXOpened, GetSFXPosition());
        }

        public virtual void OnDoorUnLatched()
        {
            if (VerboseLogging)
                Debug.Log($"OnDoorUnLatched");
            UnlockDoorJoint();
        }

        public virtual void OnDoorLatched()
        {
            if (VerboseLogging)
                Debug.Log($"OnDoorLatched");
            LockDoorJoint();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected virtual void OnDoorClosed()
        {
            if (VerboseLogging)
                Debug.Log($"OnDoorClosed");

            StartCoroutine(DoorCloseRoutine());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected virtual void OnDoorOpened()
        {
            if (VerboseLogging)
                Debug.Log($"OnDoorOpened");
        }

        protected virtual void LockDoorJoint()
        {
            if (!LimitRotation)
                return;

            Joint.SetAngularXHighLimit(0);
            Joint.SetAngularXLowLimit(0);
        }

        protected virtual void UnlockDoorJoint()
        {
            Joint.SetAngularXHighLimit(-MinAngle);
            Joint.SetAngularXLowLimit(-MaxAngle);
        }

        /// <summary>
        /// Locks the door joint, and the door knob's joint.
        /// </summary>
        public virtual void Lock()
        {
            Locked = true;
            LockDoorJoint();
            LockDoorKnob();
        }

        /// <summary>
        /// Unlocks the door and allows the door handle to rotate.
        /// </summary>
        public virtual void Unlock()
        {
            Locked = false;

            if (!DoorLatched)
            {
                UnlockDoorJoint();
            }

            UnlockDoorKnob();
        }

        protected virtual void LockDoorKnob()
        {
            if (DoorKnob) DoorKnob.SetLimits(0, 0);
            if (SecondDoorKnob) SecondDoorKnob.SetLimits(0, 0);
        }

        protected virtual void UnlockDoorKnob()
        {
            if (DoorKnob) DoorKnob.ResetLimits();
            if (SecondDoorKnob) SecondDoorKnob.ResetLimits();
        }

        protected IEnumerator DoorCloseRoutine()
        {
            _doorClosing = true;
            var startRotation = transform.localRotation;
            var elapsed = 0f;
            while (elapsed < CloseOverTime)
            {
                transform.localRotation = Quaternion.Lerp(startRotation, _startRotation, elapsed / CloseOverTime);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localRotation = _startRotation;
            _doorClosing = false;
        }

        protected virtual void SetupJoint()
        {
            var currentRotation = transform.localRotation;
            //transform.localRotation = JointStartRotation;
            Joint = gameObject.AddComponent<ConfigurableJoint>();
            Joint.connectedBody = ConnectedBody;
            Joint.LockLinearMotion();
            Joint.LockAngularYMotion();
            Joint.LockAngularZMotion();
            Joint.anchor = Vector3.zero;
            Joint.axis = Axis.GetVector();

            if (LimitRotation)
            {
                Joint.LimitAngularXMotion();
                Joint.SetAngularXHighLimit(-MinAngle);
                Joint.SetAngularXLowLimit(-MaxAngle);
            }
            else
            {
                Joint.angularXMotion = ConfigurableJointMotion.Free;
            }

            Joint.secondaryAxis = Joint.axis.OrthogonalVector();
            Joint.SetAngularXDrive(Spring, Damper, 10000f);

            transform.localRotation = currentRotation;
        }
    }
}