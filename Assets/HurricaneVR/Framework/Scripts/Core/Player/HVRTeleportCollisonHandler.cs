using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.HurricaneVR.Framework.Shared.Utilities;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using HurricaneVR.Framework.Shared.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Core.Player
{
    /// <summary>
    /// Handles moving hands and the objects they are holding after a teleport with post collision handling behaviours. 
    /// </summary>
    [RequireComponent(typeof(HVRTeleporter))]
    public class HVRTeleportCollisonHandler : MonoBehaviour
    {
        /// <summary>
        /// BoundingBoxSweep: Uses bounding box of all grabbable colliders to to find a safe position between the controller and the ResetTarget transform.
        /// DisableCollision: Disables the held object's collision until it's free and clear, releasing the object is prevented until clear.
        /// </summary>
        [Tooltip("Post teleport collision handling")]
        public AfterTeleportOptions AfterTeleportOption = AfterTeleportOptions.BoundingBoxSweep;

        /// <summary>
        /// After teleporting, checks are used against these layers to determine if hand's and held objects are stuck and need to be backed out safely.
        /// </summary>
        [Tooltip("Layers that hands and objects can hit after a teleport.")]
        public LayerMask LayerMask;

        /// <summary>
        /// Used when AfterTeleportOption is set to DisableCollision, this amount of time must be past after a teleport before checking if the object is stuck.
        /// </summary>
        [Tooltip("Time required after teleporting to invoke stuck / unstuck events")]
        public float StuckTime = .1f;

        /// <summary>
        /// After the hand sweeps from the ResetTarget toward the desired target, the hand will back out when holding an object until it's clear.
        /// This defines the amount of each backout incremenet before checking the next Physics.Overlap
        /// </summary>
        [Tooltip("When backing out held objects, how granular should each move be when checking for overlap.")]
        public float BackoutIncrement = .05f;

        /// <summary>
        /// This tranform's position is used as the starting point to safely move the hand toward's the controller after a teleport.
        /// </summary>
        [Header("Required Objects")]
        [Tooltip("After teleporting, the hand will start at this position and sweep towards the final hand destination")]
        public Transform ResetTarget;

        /// <summary>
        /// The Left HVRHandGrabber component.
        /// </summary>
        public HVRHandGrabber LeftHand;

        /// <summary>
        /// The Left Physics Hand component.
        /// </summary>
        public HVRJointHand LeftJointHand;

        /// <summary>
        /// The Right HVRHandGrabber component.
        /// </summary>
        public HVRHandGrabber RightHand;

        /// <summary>
        /// The Right Physics Hand component.
        /// </summary>
        public HVRJointHand RightJointHand;

        /// <summary>
        /// Drops gizmos lines of the post teleport sweep and bounding box positions to debug issues with collision handling
        /// </summary>
        [Header("Debugging")]
        public bool VerboseDebug;

        /// <summary>
        /// Invoked if a held object is overlapping colliders after a teleport when DisableCollision is chosen.
        /// </summary>
        [Header("Events")]
        public GrabbableStuck GrabbableStuck = new GrabbableStuck();

        /// <summary>
        /// Invoked once a previously stuck object is clear.
        /// </summary>
        public GrabbableStuck GrabbableUnstuck = new GrabbableStuck();

        public List<GrabbableCollisionTracker> LeftTrackers = new List<GrabbableCollisionTracker>();
        public List<GrabbableCollisionTracker> RightTrackers = new List<GrabbableCollisionTracker>();

        private readonly List<GrabbableCollisionTracker> _cleanup = new List<GrabbableCollisionTracker>();
        private readonly Collider[] _colliders = new Collider[100];

        private HVRGrabbable leftGrabbable;
        private HVRGrabbable rightGrabbable;

        private Vector3 _teleportStart;
        private Vector3 _teleportEnd;
        private Vector3 _previousPosition;

        private readonly RaycastHit[] _hits = new RaycastHit[50];

        protected virtual void Start()
        {
            if (!LeftHand)
            {
                LeftHand = transform.root.GetComponentsInChildren<HVRHandGrabber>().FirstOrDefault(e => e.HandSide == HVRHandSide.Left);
            }

            if (LeftHand && !LeftJointHand)
            {
                LeftJointHand = LeftHand.gameObject.GetComponent<HVRJointHand>();
            }

            if (!RightHand)
            {
                RightHand = transform.root.GetComponentsInChildren<HVRHandGrabber>().FirstOrDefault(e => e.HandSide == HVRHandSide.Right);
            }

            if (RightHand && !RightJointHand)
            {
                RightJointHand = RightHand.gameObject.GetComponent<HVRJointHand>();
            }

            if (!ResetTarget)
            {
                var o = new GameObject("TeleportHandReset");
                o.transform.parent = transform;
                ResetTarget = o.transform;
                ResetTarget.ResetLocalProps();
                ResetTarget.localPosition = new Vector3(0f, 1.5f, 0f);
            }

            var teleporter = GetComponent<HVRTeleporter>();
            teleporter.AfterTeleport.AddListener(AfterTeleport);
            teleporter.PositionUpdate.AddListener(TeleportUpdate);
            teleporter.BeforeTeleport.AddListener(BeforeTeleport);
        }

        protected virtual void FixedUpdate()
        {
            //Debug.DrawLine(s, e, Color.red);
            //Debug.DrawLine(s, s + Vector3.up * .3f, Color.blue);

            CheckTrackers(RightTrackers, RightHand);
            CheckTrackers(LeftTrackers, LeftHand);
        }

        #region Public

        /// <summary>
        /// Called before teleporting to snapshot the hand and held object state, and the position of the player
        /// </summary>
        /// <param name="position">Player controller position before teleporting</param>
        public virtual void BeforeTeleport(Vector3 position)
        {
            _teleportStart = position;

            leftGrabbable = null;
            rightGrabbable = null;

            if (LeftHand && LeftHand.GrabbedTarget)
            {
                leftGrabbable = LeftHand.GrabbedTarget;
                if (leftGrabbable.MasterGrabbable) leftGrabbable = leftGrabbable.MasterGrabbable;
            }

            if (RightHand && RightHand.GrabbedTarget)
            {
                rightGrabbable = RightHand.GrabbedTarget;
                if (rightGrabbable.MasterGrabbable) rightGrabbable = rightGrabbable.MasterGrabbable;
            }

            if (leftGrabbable && leftGrabbable.TryGetComponent<HVRTeleportOptions>(out var o) && o.BeforeTeleportOption == BeforeTeleportOptions.DropsGrabbable)
            {
                leftGrabbable.ForceRelease();
                leftGrabbable = null;
            }

            if (rightGrabbable && rightGrabbable != leftGrabbable && rightGrabbable.TryGetComponent(out o) && o.BeforeTeleportOption == BeforeTeleportOptions.DropsGrabbable)
            {
                rightGrabbable.ForceRelease();
                rightGrabbable = null;
            }

            if (leftGrabbable && leftGrabbable.Rigidbody)
            {
                leftGrabbable.Rigidbody.detectCollisions = false;
            }

            if (rightGrabbable && rightGrabbable.Rigidbody)
            {
                rightGrabbable.Rigidbody.detectCollisions = false;
            }

            if (LeftHand)
            {
                LeftHand.CanRelease = false;
            }

            if (RightHand)
            {
                RightHand.CanRelease = false;
            }

            if (LeftJointHand)
            {
                LeftJointHand.RigidBody.detectCollisions = false;
            }

            if (RightJointHand)
            {
                RightJointHand.RigidBody.detectCollisions = false;
            }

            _previousPosition = position;
        }

        /// <summary>
        /// Call while a teleport is in progress.
        /// </summary>
        /// <param name="position">Player controller updated position</param>
        public virtual void TeleportUpdate(Vector3 position)
        {
            _teleportEnd = position;
            var delta = position - _previousPosition;

            if (leftGrabbable && leftGrabbable.Rigidbody)
            {
                leftGrabbable.transform.position += delta;
                leftGrabbable.Rigidbody.position = leftGrabbable.transform.position;
            }

            if (rightGrabbable && rightGrabbable.Rigidbody && rightGrabbable != leftGrabbable)
            {
                rightGrabbable.transform.position += delta;
                rightGrabbable.Rigidbody.position = rightGrabbable.transform.position;
            }

            if (LeftJointHand)
            {
                LeftJointHand.transform.position += delta;
                LeftJointHand.RigidBody.position = LeftJointHand.RigidBody.position;
            }

            if (RightJointHand)
            {
                RightJointHand.transform.position += delta;
                RightJointHand.RigidBody.position = RightJointHand.RigidBody.position;
            }

            _previousPosition = position;
        }

        /// <summary>
        /// Call after a teleport sequence has completed. Post teleport collision behaviour sequence will execute.
        /// </summary>
        public virtual void AfterTeleport()
        {
            if (LeftHand)
            {
                LeftHand.CanRelease = true;
            }

            if (RightHand)
            {
                RightHand.CanRelease = true;
            }

            if (leftGrabbable && leftGrabbable.Rigidbody && LeftTrackers.Count == 0)
            {
                leftGrabbable.Rigidbody.detectCollisions = true;
            }

            if (LeftHand && leftGrabbable && leftGrabbable.Rigidbody)
            {
                var leftOption = GetAfterOption(leftGrabbable, out var o);

                if (leftOption == AfterTeleportOptions.DisableCollision && LeftTrackers.Count == 0)
                {
                    var tracker = new GrabbableCollisionTracker(leftGrabbable);
                    LeftTrackers.Add(tracker);
                    LeftHand.CanRelease = false;
                }
            }

            if (rightGrabbable && rightGrabbable.Rigidbody && RightTrackers.Count == 0)
            {
                rightGrabbable.Rigidbody.detectCollisions = true;
            }

            if (RightHand && rightGrabbable && rightGrabbable.Rigidbody)
            {
                var rightOption = GetAfterOption(rightGrabbable, out var o);

                if (rightOption == AfterTeleportOptions.DisableCollision && RightTrackers.Count == 0)
                {
                    var tracker = new GrabbableCollisionTracker(rightGrabbable);
                    RightTrackers.Add(tracker);
                    RightHand.CanRelease = false;
                }
            }

            if (LeftHand)
            {
                LeftJointHand.RigidBody.detectCollisions = true;
            }

            if (RightJointHand)
            {
                RightJointHand.RigidBody.detectCollisions = true;
            }


            Physics.SyncTransforms();

            var direction = (_teleportStart - _teleportEnd).normalized;
            direction.y = 0f;

            var offset = Vector3.zero;
            if (leftGrabbable) offset = leftGrabbable.transform.InverseTransformPoint(RightHand.transform.position);

            PostTeleportSweep(LeftHand, leftGrabbable, direction);

            if (leftGrabbable && leftGrabbable == rightGrabbable)
            {
                RightHand.transform.position = leftGrabbable.transform.TransformPoint(offset);
                RightHand.Rigidbody.position = RightHand.transform.position;
            }
            else
            {
                PostTeleportSweep(RightHand, rightGrabbable, direction);
            }


            leftGrabbable = null;
            rightGrabbable = null;
        }


        /// <summary>
        /// Sweeps the hand and held grabbable from the ResetTarget towards the hand's current position, then backs out in the opposite direction until clear.
        /// </summary>
        public virtual void Sweep(HVRHandGrabber hand)
        {
            SweepHand(hand, hand.GrabbedTarget, hand.Rigidbody.position);
        }

        /// <summary>
        /// Sweeps the hand and held grabbable from the ResetTarget towards the target position, then backs out in the opposite direction until clear.
        /// </summary>
        /// /// <param name="target">Position that the hand will sweep toward.</param>
        public virtual void Sweep(HVRHandGrabber hand, Vector3 target)
        {
            SweepHand(hand, hand.GrabbedTarget, target);
        }

        #endregion

        #region Protected

        /// <summary>
        /// Sweeps the hand and grabbable from the ResetTarget towards the target position, then backs out in the opposite direction until clear.
        /// </summary>
        /// <param name="target">Position that the hand will sweep toward.</param>
        protected virtual void SweepHand(HVRHandGrabber hand, HVRGrabbable g, Vector3 target)
        {
            if (!g || !g.Rigidbody)
            {
                SweepHand(hand, target);
                return;
            }

            if (g.MasterGrabbable && g.MasterGrabbable.Rigidbody)
                g = g.MasterGrabbable;

            GetAfterOption(g, out var options);
            SweepHandAndGrabbable(hand, g, ResetTarget.position - target, options, target);
        }


        /// <summary>
        /// Sweeps the hand from the ResetTarget towards the target position, then backs out in the opposite direction until clear.
        /// </summary>
        /// <param name="target">Position that the hand will sweep toward.</param>
        protected virtual void SweepHand(HVRHandGrabber hand, Vector3 target)
        {
            var origin = ResetTarget ? ResetTarget : transform;
            var direction = (target - origin.position).normalized;

            hand.Rigidbody.position = origin.position;

            var bounds = hand.HandPhysics.HandColliders.GetColliderBounds();
            var maxSide = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
            var start = bounds.center;
            var length = Vector3.Distance(target, start);
            var collided = false;
            var backoutDistance = length;
            //sweep test seems to collide with concave environment colliders where box cast doesn't?
            //if (hand.Rigidbody.SweepTest(direction, out var hit, length, QueryTriggerInteraction.Ignore))

            var count = Physics.BoxCastNonAlloc(start, bounds.extents, direction, _hits, Quaternion.identity, length, LayerMask, QueryTriggerInteraction.Ignore);

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (IgnoreCollider(_hits[i].collider, hand, hand.GrabbedTarget)) continue;
                    collided = true;
                    if (VerboseDebug) Debug.Log($"Collided: {_hits[i].collider.name}");
                    break;
                }

                if (collided)
                {
                    var distance = float.MaxValue;

                    for (int i = 0; i < count; i++)
                    {
                        if (_hits[i].distance < distance) distance = _hits[i].distance;
                    }

                    if (distance < .001f) distance = maxSide;

                    hand.Rigidbody.position = start + direction * (distance);
                    hand.transform.position = hand.Rigidbody.position;
                    backoutDistance = distance;

                    //var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    //sphere.transform.localScale = new Vector3(.05f, .05f, .05f);
                    //sphere.transform.position = start + direction * distance;
                    //Destroy(sphere.GetComponent<SphereCollider>());

                    //var box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //box.transform.localScale = new Vector3(.05f, .05f, .05f);
                    //box.transform.position = start;
                    //Destroy(box.GetComponent<BoxCollider>());

                    if (VerboseDebug) MakeBox("Collided", bounds.size, start + direction * distance, hand);
                }
            }

            if (!collided)
            {
                hand.transform.position = hand.Rigidbody.position = target;
            }

            bounds = hand.HandPhysics.HandColliders.GetColliderBounds();

            if (VerboseDebug) DrawSweepLines(hand, start, direction, length);
            if (VerboseDebug) CastEnd(hand, bounds, start, direction, length);

            if (!CheckOverlap(hand, bounds, bounds.center))
                return;

            //backout after forward sweep


            var offset = Vector3.zero;

            for (var d = 0f; d < backoutDistance; d += .05f)
            {
                offset = -direction * d;

                if (VerboseDebug) MakeBox("unstucking " + d, bounds.size, bounds.center + offset, hand);

                if (!CheckOverlap(hand, bounds, bounds.center + offset))
                {
                    break;
                }
            }

            hand.Rigidbody.position += offset;
            hand.transform.position = hand.Rigidbody.position;
        }

        /// <summary>
        /// Handles sweeping the hand and grabbable after a teleport sequence.
        /// </summary>
        /// <param name="backoutDirection">Direction to reverse the grabbable if it's overlapping something after teleporting.</param>
        protected virtual void PostTeleportSweep(HVRHandGrabber hand, HVRGrabbable g, Vector3 backoutDirection)
        {
            if (!hand) return;
            if (!g || !g.Rigidbody)
            {
                SweepHand(hand, hand.Rigidbody.position);
                return;
            }

            if (g.MasterGrabbable && g.MasterGrabbable.Rigidbody)
                g = g.MasterGrabbable;

            var option = GetAfterOption(g, out var options);

            if (option == AfterTeleportOptions.BoundingBoxSweep)
            {
                SweepHandAndGrabbable(hand, g, backoutDirection, options, hand.Rigidbody.position);
                return;
            }

            //DisableCollision only affects the held object, sweep the hand to see if it collides with something
            SweepHand(hand, hand.Rigidbody.position);
        }

        /// <summary>
        /// Determine if the provided collider should be ignored when doing an overlap check.
        /// </summary>
        /// <param name="grabbable">The grabbable that is being tested for overlapping other geometry</param>
        /// <returns>Returns true if the provided collider should be ignored</returns>
        protected virtual bool IgnoreCollider(Collider c, HVRHandGrabber hand, HVRGrabbable grabbable)
        {
            if (grabbable && grabbable.HasCollider(c)) return true;

            var other = GetOtherHand(hand);

            return other && other.GrabbedTarget && other.GrabbedTarget.HasCollider(c);
        }

        #endregion

        #region Private

        private void CheckTrackers(List<GrabbableCollisionTracker> trackers, HVRHandGrabber hand)
        {
            var count = trackers.Count;

            for (var i = 0; i < trackers.Count; i++)
            {
                var tracker = trackers[i];
                tracker.Frame++;

                if (tracker.Frame == 1)
                {
                    tracker.Rb.detectCollisions = false;
                }
                else if (tracker.Frame < 3)
                {
                    continue;
                }

                if (Time.time - tracker.StartTime > StuckTime && !tracker.Stuck)
                {
                    //Debug.Log($"{tracker.Rb.gameObject.name} is stuck.");
                    GrabbableStuck.Invoke(tracker.Grabbable);
                    tracker.Stuck = true;
                }

                if (CheckOverlap(tracker))
                {
                    continue;
                }


                _cleanup.Add(tracker);

                if (tracker.Stuck)
                {
                    //Debug.Log($"{tracker.Rb.gameObject.name} is free.");
                    GrabbableUnstuck.Invoke(tracker.Grabbable);
                    tracker.Stuck = false;
                }

                tracker.Rb.detectCollisions = true;
                tracker.Grabbable = null;
            }

            for (var i = 0; i < _cleanup.Count; i++)
            {
                var tracker = _cleanup[i];
                trackers.Remove(tracker);
            }

            if (count > 0 && trackers.Count == 0 && hand)
            {
                hand.CanRelease = true;
            }

            _cleanup.Clear();
        }

        private bool CheckOverlap(GrabbableCollisionTracker tracker)
        {
            //var bounds = tracker.Rb.GetColliders().ToArray().GetColliderBounds();
            //var overlaps = Physics.OverlapBoxNonAlloc(bounds.center, bounds.extents, _colliders, Quaternion.identity, LayerMask);
            var overlaps = Physics.OverlapBoxNonAlloc(tracker.Rb.transform.TransformPoint(tracker.Center), tracker.Bounds.extents, _colliders, Quaternion.identity, LayerMask, QueryTriggerInteraction.Ignore);
            //bounds.DrawBounds();
            for (var j = 0; j < overlaps; j++)
            {
                var c = _colliders[j];

                if (tracker.Colliders.Contains(c))
                    continue;

                //concave colliders don't work correctly with compute penetration
                if (c is MeshCollider mc && !mc.convex)
                    return true;

                foreach (var tc in tracker.Colliders)
                {
                    if (Physics.ComputePenetration(tc, tc.transform.position, tc.transform.rotation, c, c.transform.position, c.transform.rotation, out var dir, out var dis))
                        return true;
                }
            }

            return false;
        }

        private void SweepHandAndGrabbable(HVRHandGrabber hand, HVRGrabbable g, Vector3 backoutDirection, HVRTeleportOptions options, Vector3 target)
        {
            var grabbableOffset = g.Rigidbody.position - hand.Rigidbody.position;

            SweepHand(hand, target);

            g.Rigidbody.position = g.transform.position = hand.Rigidbody.position + grabbableOffset;

            Backout(hand, g, backoutDirection, options);
        }

        private void Backout(HVRHandGrabber hand, HVRGrabbable g, Vector3 direction, HVRTeleportOptions options)
        {
            var bounds = hand.HandPhysics.HandColliders.GetColliderBounds();

            if (options && options.CustomBoundingBox)
            {
                bounds.Encapsulate(options.CustomBoundingBox.bounds);
            }
            else
            {
                if (g.Colliders != null && g.Colliders.Count > 0)
                    bounds.Encapsulate(g.Colliders.GetColliderBounds());
            }

            var offset = Vector3.zero;

            if (BackoutIncrement < .0001f) BackoutIncrement = .05f;

            for (var d = 0f; d < Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) * 2f; d += BackoutIncrement)
            {
                offset = direction * d;

                if (VerboseDebug) MakeBox("backup " + d, bounds.size, bounds.center + offset, hand);

                if (!CheckOverlap(hand, bounds, bounds.center + offset))
                {
                    break;
                }
            }

            g.transform.position = g.Rigidbody.position += offset;
            hand.transform.position = hand.Rigidbody.position += offset;

            if (VerboseDebug)
            {
                bounds = hand.HandPhysics.HandColliders.GetColliderBounds();

                if (options && options.CustomBoundingBox)
                {
                    bounds.Encapsulate(options.CustomBoundingBox.bounds);
                }
                else
                {
                    if (g.Colliders != null && g.Colliders.Count > 0)
                        bounds.Encapsulate(g.Colliders.GetColliderBounds());
                }

                MakeBox("backed up", bounds.size, bounds.center, hand);
            }
        }

        private bool CheckOverlap(HVRHandGrabber hand, Bounds bounds, Vector3 center)
        {
            var overlaps = Physics.OverlapBoxNonAlloc(center, bounds.extents, _colliders, Quaternion.identity, LayerMask, QueryTriggerInteraction.Ignore);
            if (overlaps == 0) return false;

            for (int i = 0; i < overlaps; i++)
            {
                if (IgnoreCollider(_colliders[i], hand, hand.GrabbedTarget)) continue;
                if (VerboseDebug) Debug.Log($"CheckOverlap: {_colliders[i].name}");
                return true;
            }

            return false;
        }

        private HVRHandGrabber GetOtherHand(HVRHandGrabber hand)
        {
            if (hand == LeftHand) return RightHand;
            return LeftHand;
        }

        private AfterTeleportOptions GetAfterOption(HVRGrabbable g, out HVRTeleportOptions options)
        {
            if (!g.gameObject.TryGetComponent(out options) || options.AfterTeleportOption == AfterTeleportOptions.TeleporterDefault)
            {
                return AfterTeleportOption == AfterTeleportOptions.TeleporterDefault ? AfterTeleportOptions.BoundingBoxSweep : AfterTeleportOption;
            }

            return options.AfterTeleportOption;
        }

        #endregion


        #region Debugging

        private void MakeBox(string name, Vector3 size, Vector3 pos, HVRHandGrabber hand)
        {
            var go = new GameObject(name + " " + hand?.name);
            var box = go.AddComponent<BoxCollider>();
            box.size = size;
            box.transform.position = pos;
            box.isTrigger = true;
        }

        private static void DrawSweepLines(HVRHandGrabber hand, Vector3 start, Vector3 direction, float length)
        {
            Debug.DrawLine(start, start + direction * length, Color.blue, 100000f);
            Debug.DrawLine(start, start + Vector3.up * .1f, hand.HandSide == HVRHandSide.Left ? Color.green : Color.yellow, 100000f);
        }

        private static void CastEnd(HVRHandGrabber hand, Bounds bounds, Vector3 start, Vector3 direction, float length)
        {
            var go = new GameObject("cast end? " + hand.name);
            var box = go.AddComponent<BoxCollider>();
            box.size = bounds.size;
            box.transform.position = start + direction * length;
            box.isTrigger = true;
        }

        #endregion
    }


    [Serializable]
    public class GrabbableStuck : UnityEvent<HVRGrabbable>
    {
    }

    public class GrabbableCollisionTracker
    {
        public HVRGrabbable Grabbable;
        public Rigidbody Rb => Grabbable.Rigidbody;
        public float StartTime;
        public int Frame;
        public Bounds Bounds;
        public Collider[] Colliders;
        public Vector3 Center;
        public bool Stuck;

        public GrabbableCollisionTracker(HVRGrabbable g)
        {
            Grabbable = g;
            var rb = g.Rigidbody;
            Colliders = rb.GetColliders().ToArray();
            Bounds = Colliders.GetColliderBounds();
            Center = rb.transform.InverseTransformPoint(Bounds.center);
            StartTime = Time.time;
        }
    }
}