using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Core.Stabbing
{
    [RequireComponent(typeof(Rigidbody))]
    public class HVRStabber : MonoBehaviour
    {
        [Header("Objects / Components")]

        [Tooltip("Starting tip point of the stabber")]
        public Transform Tip;

        [Tooltip("Base of the stabber, limits the stab depth unless CanRunThrough is set to true.")]
        public Transform Base;

        public HVRStabberSettings Settings;

        [Tooltip("Wanna stab anything and everything for fun or testing?!")]
        public bool StabAnything;

        [Tooltip("Ignores stabbable velocity requirement")]
        public bool IgnoreVelocityCheck;

        public HVRStabbableSettings FallbackSettings;

        public JointProjectionMode ProjectionMode = JointProjectionMode.PositionAndRotation;
        public float ProjectionDistance = .01f;
        public float ProjectionAngle = 20f;

        [Tooltip("Both base and tip can stab something")]
        public bool IsDualStabber;

        [Tooltip("If true the stabber can exit through the base")]
        public bool CanRunThrough;

        [Header("Collision")]

        [Tooltip("Colliders to disable collision with the stabbed object")]
        public Collider[] CollidersToIgnore;

        [Tooltip("Colliders involved in stab detection")]
        public Collider[] StabbingColliders;

        [Tooltip("Stabbing colliders contact offset will be modified to this if current offset is larger")]
        public float ContactOffset = .001f;

        [Header("Debug")]
        public bool DrawGizmos;
        public bool VerboseDebugging;
        public bool LogFailedAngle;
        public bool LogFailedVelocity;
        public bool LogJointLock;
        public bool LogJointUnlock;
        public bool LogJointForce;
        public float LogJointForceThreshold = 100;
        public float Depth;
        public float DepthPercent;

        public HVRStabEvent Stabbed = new HVRStabEvent();
        public HVRStabEvents UnStabbed = new HVRStabEvents();
        public HVRStabEvents FullStabbed = new HVRStabEvents();

        public Vector3 StabLineWorld => Tip.position - Base.position;
        public Vector3 StabLineLocal => Rigidbody.transform.InverseTransformPoint(Tip.position) - Rigidbody.transform.InverseTransformPoint(Base.position);

        public float Length => StabLineWorld.magnitude;

        public Rigidbody Rigidbody { get; private set; }

        public bool IsStabbing => _trackers.Count > 0;

        private readonly List<HVRStabTracker> _trackers = new List<HVRStabTracker>(10);
        private readonly List<HVRStabTracker> _cleanup = new List<HVRStabTracker>();
        public List<GameObject> StabbedObjects { get; private set; }
        public List<HVRStabbable> StabbedStabbables { get; private set; }

        private Vector3 _velocity;


        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();

            if (StabAnything && !FallbackSettings)
            {
                FallbackSettings = ScriptableObject.CreateInstance<HVRStabbableSettings>();
            }

            if (FallbackSettings)
            {
                FallbackSettings.CheckCurve();
            }

            if (CollidersToIgnore == null || CollidersToIgnore.Length == 0)
            {
                CollidersToIgnore = GetComponentsInChildren<Collider>();
            }

            if (!Settings)
            {
                Settings = ScriptableObject.CreateInstance<HVRStabberSettings>();
            }

            if (!FallbackSettings)
            {
                FallbackSettings = ScriptableObject.CreateInstance<HVRStabbableSettings>();
            }

            StabbedObjects = new List<GameObject>(Settings.AllowedStabs);
            StabbedStabbables = new List<HVRStabbable>(Settings.AllowedStabs);

            foreach (var sc in StabbingColliders)
            {
                if (sc.contactOffset > ContactOffset)
                {
                    sc.contactOffset = ContactOffset;
                }
            }
        }

        public void FixedUpdate()
        {
            if (Rigidbody)
            {
                _velocity = Rigidbody.velocity;
            }

            foreach (var tracker in _trackers)
            {
                if (!tracker.Update())
                {
                    Destroy(tracker.Joint);
                    _cleanup.Add(tracker);
                    IgnoreCollision(tracker.StabbedColliders, false);
                    OnStabExit(tracker.Stabbable);
                }
            }

            for (var i = 0; i < _cleanup.Count; i++)
            {
                var tracker = _cleanup[i];
                _trackers.Remove(tracker);
                StabbedObjects.Remove(tracker.StabbedObject);
                if (tracker.Stabbable)
                {
                    StabbedStabbables.Remove(tracker.Stabbable);
                }
            }

            CleanupStabbableList();

            _cleanup.Clear();
        }

        public void ForceUnstab(bool fireEvents = false)
        {
            foreach (var tracker in _trackers)
            {
                Destroy(tracker.Joint);
                _cleanup.Add(tracker);
                IgnoreCollision(tracker.StabbedColliders, false);
                if (fireEvents)
                {
                    OnStabExit(tracker.Stabbable);
                }
            }

            for (var i = 0; i < _cleanup.Count; i++)
            {
                var tracker = _cleanup[i];
                _trackers.Remove(tracker);
                StabbedObjects.Remove(tracker.StabbedObject);
                if (tracker.Stabbable)
                {
                    StabbedStabbables.Remove(tracker.Stabbable);
                }
            }

            _cleanup.Clear();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_trackers.Count >= Settings.AllowedStabs)
                return;

            for (var i = 0; i < collision.contactCount; i++)
            {
                var contact = collision.GetContact(i);

                //check if the collided collider is one that causes stabbing
                var anyStabCollider = false;
                foreach (var stabCollider in StabbingColliders)
                {
                    if (stabCollider == contact.thisCollider)
                        anyStabCollider = true;
                }

                if (!anyStabCollider)
                    continue;

                GameObject stabbedObject;
                HVRStabbable stabbable;
                HVRNotStabbable notStabbable;

                var otherRB = contact.otherCollider.attachedRigidbody;
                if (otherRB)
                {
                    stabbedObject = otherRB.gameObject;
                }
                else
                {
                    stabbedObject = contact.otherCollider.gameObject;
                }

                if (StabbedObjects.Contains(stabbedObject))
                    continue;

                HVRStabbableSettings settings;

                stabbedObject.TryGetComponent(out stabbable);
                stabbedObject.TryGetComponent(out notStabbable);


                if (stabbable)
                {
                    if (!stabbable.enabled)
                    {
                        if (VerboseDebugging) Debug.Log($"{stabbable.name} stabbable disabled.");
                        return;
                    }
                    settings = stabbable.Settings;
                }
                else
                {
                    if (!StabAnything || StabAnything && notStabbable)
                    {
                        continue;
                    }

                    settings = FallbackSettings;
                }

                var stabDirection = StabLineWorld.normalized;
                var stabTransform = Tip;
                var dot = Vector3.Dot(StabLineWorld.normalized, -contact.normal);

                if (dot < Settings.AngleThreshold)
                {
                    if (IsDualStabber)
                    {
                        var dt = Vector3.Distance(contact.point, Tip.position);
                        var db = Vector3.Distance(contact.point, Base.position);

                        if (dt < db || Vector3.Dot(StabLineWorld.normalized, contact.normal) < Settings.AngleThreshold)
                        {
                            if (LogFailedAngle)
                                Debug.Log($"stab failed dot product {dot}");
                            continue;
                        }

                        stabTransform = Base;
                        stabDirection = -stabDirection;
                    }
                    else
                    {
                        if (LogFailedAngle)
                            Debug.Log($"stab failed dot product {dot}");
                        continue;
                    }
                }

                if (!IgnoreVelocityCheck && collision.relativeVelocity.magnitude < settings.RequiredVelocity)
                {
                    if (LogFailedVelocity)
                        Debug.Log($"stab failed velocity : {collision.relativeVelocity.magnitude}");
                    continue;
                }

                Rigidbody.velocity = _velocity;
                if (stabbable && otherRB)
                {
                    otherRB.velocity = stabbable.Velocity;
                    //Debug.Log($"{stabbable.Velocity}");
                }

                var joint = SetupStabJoint(settings, stabTransform, otherRB);
                //var joint = SetupStabJoint(settings, contact.point, otherRB);

                StabbedObjects.Add(stabbedObject);
                if (stabbable)
                {
                    StabbedStabbables.Add(stabbable);
                }

                List<Collider> stabbedColliders;
                if (stabbable)
                {
                    stabbedColliders = stabbable.Ignorecolliders;
                }
                else
                {
                    if (otherRB)
                    {
                        stabbedColliders = otherRB.GetColliders().ToList();
                    }
                    else
                    {
                        stabbedColliders = contact.otherCollider.gameObject.GetColliders();
                    }
                }

                IgnoreCollision(stabbedColliders);

                var tracker = new HVRStabTracker(this,
                    stabbable, settings,
                    joint,
                    stabbedObject,
                    stabDirection,
                    stabTransform,
                    stabbedColliders
                    );

                _trackers.Add(tracker);

                OnStabEnter(stabbable, collision, contact);
            }
        }

        protected internal virtual void OnStabEnter(HVRStabbable stabbable, Collision collision, ContactPoint contact)
        {
            var args = new StabArgs(this, stabbable, collision, contact.point, contact.normal);

            Stabbed.Invoke(args);

            if (VerboseDebugging)
            {
                Debug.Log($"{name} -> {stabbable?.name} OnStabEnter");
            }

            if (stabbable)
            {
                stabbable.OnStabberEnter(this, collision, contact);
                stabbable.Stabbed.Invoke(args);
            }
        }

        protected internal virtual void OnStabExit(HVRStabbable stabbable)
        {
            if (VerboseDebugging)
            {
                Debug.Log($"{name} -> {stabbable?.name}  OnStabExit");
            }

            if (stabbable)
            {
                stabbable.OnStabberExit(this);
            }

            UnStabbed.Invoke(this, stabbable);
        }

        protected internal virtual void OnFullStab(HVRStabbable stabbable)
        {
            if (VerboseDebugging)
            {
                Debug.Log($"{name} -> {stabbable?.name}  OnFullStab");
            }

            if (stabbable)
            {
                FullStabbed.Invoke(this, stabbable);
                stabbable.OnFullStabReached(this);
            }
        }

        protected virtual ConfigurableJoint SetupStabJoint(HVRStabbableSettings settings, Transform tip, Rigidbody otherRB)
        {
            var joint = Rigidbody.gameObject.AddComponent<ConfigurableJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.axis = StabLineLocal.normalized;
            joint.secondaryAxis = HVRUtilities.OrthogonalVector(joint.axis);

            if (settings.OverrideStabberProjection)
            {
                joint.projectionMode = settings.ProjectionMode;
                joint.projectionDistance = settings.ProjectionDistance;
                joint.projectionAngle = settings.ProjectionAngle;
            }
            else
            {
                joint.projectionMode = ProjectionMode;
                joint.projectionDistance = ProjectionDistance;
                joint.projectionAngle = ProjectionAngle;
            }

            joint.yMotion = ConfigurableJointMotion.Locked;
            joint.zMotion = ConfigurableJointMotion.Locked;
            joint.angularXMotion = ConfigurableJointMotion.Locked;
            joint.angularYMotion = ConfigurableJointMotion.Locked;
            joint.angularZMotion = ConfigurableJointMotion.Locked;

            //if (IsDualStabber || CanRunThrough)
            //{
            //    joint.xMotion = ConfigurableJointMotion.Free;
            //}
            //else
            {
                joint.anchor = Rigidbody.transform.InverseTransformPoint(tip.position);
                joint.xMotion = ConfigurableJointMotion.Limited;
            }

            var limit = joint.linearLimit;



            if (settings.LimitStabDepth)
            {
                limit.limit = settings.StabDepthLimit;
            }
            else
            {
                limit.limit = Length; //using world because local magnitude is affect by scaled objects    
                if (IsDualStabber || CanRunThrough)
                {
                    limit.limit += .3f;
                }
            }

            joint.linearLimit = limit;

            joint.connectedBody = otherRB;
            if (joint.connectedBody)
            {
                joint.connectedAnchor = joint.connectedBody.transform.InverseTransformPoint(tip.position);
            }
            else
            {
                joint.connectedAnchor = tip.position;
            }
            return joint;
        }

        private void IgnoreCollision(List<Collider> stabbedColliders, bool ignore = true)
        {
            for (var i = 0; i < stabbedColliders.Count; i++)
            {
                var stabbedCollider = stabbedColliders[i];
                foreach (var stabCollider in StabbingColliders)
                {
                    if (stabbedCollider)
                        Physics.IgnoreCollision(stabCollider, stabbedCollider, ignore);
                }

                foreach (var stabberCollider in CollidersToIgnore)
                {
                    if (stabbedCollider)
                        Physics.IgnoreCollision(stabberCollider, stabbedCollider, ignore);
                }
            }
        }


        public void OnDrawGizmos()
        {
            if (DrawGizmos)
            {
                foreach (var tracker in _trackers)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(tracker.StabEntryPosition, .015f);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(tracker.StabExitPosition, .01f);
                }

                if (Tip && Base)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(Tip.position, Base.position);
                }
            }

        }

        public bool ManuallyLocked { get; private set; }

        /// <summary>
        /// Locks each joint on the stabber and prevents it from being unlocked until Unlock is called
        /// </summary>
        public void Lock()
        {
            for (var i = 0; i < _trackers.Count; i++)
            {
                var tracker = _trackers[i];
                tracker.Lock();
                ManuallyLocked = true;
            }
        }

        /// <summary>
        /// Unlocks each joint on the stabber
        /// </summary>
        public void Unlock()
        {
            for (var i = 0; i < _trackers.Count; i++)
            {
                var tracker = _trackers[i];
                tracker.Unlock();
                ManuallyLocked = false;
            }
        }

        public static IEnumerable<Collider> GetColliders(Rigidbody rigidbody, Transform transform)
        {
            var rb = transform.GetComponent<Rigidbody>();
            if (rb && rb != rigidbody)
                yield break;

            foreach (var c in transform.GetComponents<Collider>())
            {
                yield return c;
            }

            foreach (Transform child in transform)
            {
                foreach (var c in GetColliders(rigidbody, child))
                {
                    yield return c;
                }
            }
        }


        private void CleanupStabbableList()
        {
            var needsCleaning = false;
            foreach (var st in StabbedStabbables)
            {
                if (!st)
                {
                    needsCleaning = true;
                }
            }

            if (needsCleaning)
            {
                StabbedStabbables.RemoveAll(e => !e);
            }
        }


    }

    [Serializable]
    public class HVRStabEvents : UnityEvent<HVRStabber, HVRStabbable>
    {

    }

    [Serializable]
    public class HVRStabEvent : UnityEvent<StabArgs>
    {

    }

    public struct StabArgs
    {
        public StabArgs(HVRStabber stabber, HVRStabbable stabbable, Collision collision, Vector3 point, Vector3 normal)
        {
            Stabber = stabber;
            Stabbable = stabbable;
            Collision = collision;
            Point = point;
            Normal = normal;
        }


        public HVRStabber Stabber;
        public HVRStabbable Stabbable;
        public Collision Collision;
        public Vector3 Point;
        public Vector3 Normal;
    }
}