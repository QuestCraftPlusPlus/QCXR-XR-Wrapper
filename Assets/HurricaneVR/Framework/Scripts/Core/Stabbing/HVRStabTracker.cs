using System.Collections.Generic;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Stabbing
{
    public class HVRStabTracker
    {
        public HVRStabbable Stabbable { get; private set; }
        public HVRStabbableSettings Settings { get; }

        public HVRStabber Stabber { get; private set; }

        public ConfigurableJoint Joint { get; private set; }
        public Vector3 StabEntryPosition { get; private set; }

        public Vector3 StabExitPosition { get; private set; }

        public GameObject StabbedObject { get; private set; }
        public List<Collider> StabbedColliders { get; private set; }

        internal bool ManuallyLocked => Stabber.ManuallyLocked;

        public Vector3 StabDirection => StabbedObject.transform.TransformDirection(_stabDirectionLocal);

        private readonly float _stabberLength;
        private bool _outerShellBreached;
        private float _previousDepth;
        private bool _canFullStab;
        private bool _hasFullStabbed;
        private readonly Vector3 _stabLocalPosition;
        private float _lockTimer;
        private float _stabbedTime;
        private Vector3 _stabExitLocalPosition;
        private readonly Vector3 _stabDirectionLocal;
        private readonly Transform _tip;
        private readonly Transform _base;
        private readonly float _unstabThreshold;
        private bool _hasEntered;
        private Vector3 _entryToTip;
        private Vector3 _exitToBase;
        private bool _tipValid;
        private bool _baseValid;


        public bool Update()
        {
            if (!StabbedObject)
                return false;

            UpdateEntryAndExit();

            _entryToTip = _tip.position - StabEntryPosition;
            _exitToBase = _base.position - StabExitPosition;



            _tipValid = Vector3.Dot(_entryToTip, StabDirection) > 0f;
            _baseValid = Vector3.Dot(_exitToBase, -StabDirection) > 0f;

            ComputeDepth(out var percentStabbed, out var stabDepth);

            Stabber.Depth = stabDepth;
            Stabber.DepthPercent = percentStabbed;

            if (CheckUnstab())
            {
                return false;
            }

            CheckFullStab(stabDepth);
            UpdateFriction(stabDepth, percentStabbed);

            CheckLock();

            _previousDepth = stabDepth;

            return true;
        }

        private void ComputeDepth(out float percentStabbed, out float stabDepth)
        {
            if (Settings.CanBeImpaled || Stabber.IsDualStabber)
            {
                var tipInside = IsCBetweenAB(StabEntryPosition, StabExitPosition, _tip.position);
                var baseInside = IsCBetweenAB(StabEntryPosition, StabExitPosition, _base.position);

                var width = (StabEntryPosition - StabExitPosition).magnitude;
                width = Mathf.Min(width, _stabberLength);

                if (tipInside && baseInside || (!tipInside && !baseInside))
                {
                    stabDepth = width;
                    percentStabbed = 1f;
                }
                else if (tipInside)
                {
                    _hasEntered = true;
                    stabDepth = _entryToTip.magnitude;
                    if (width > 0)
                    {
                        percentStabbed = stabDepth / width;
                    }
                    else
                    {
                        percentStabbed = stabDepth / _stabberLength;
                    }
                }
                else
                {
                    _hasEntered = true;
                    stabDepth = _exitToBase.magnitude;
                    if (width > 0)
                    {
                        percentStabbed = stabDepth / width;
                    }
                    else
                    {
                        percentStabbed = stabDepth / _stabberLength;
                    }
                }
            }
            else
            {
                stabDepth = _entryToTip.magnitude;
                percentStabbed = stabDepth / _stabberLength;
            }
        }

        bool IsCBetweenAB(Vector3 A, Vector3 B, Vector3 C)
        {
            return Vector3.Dot((B - A).normalized, (C - B).normalized) < 0f && Vector3.Dot((A - B).normalized, (C - A).normalized) < 0f;
        }

        private bool CheckUnstab()
        {
            if (_stabbedTime < Stabber.Settings.UnstabDelay)
            {
                _stabbedTime += Time.deltaTime;
                return false;
            }

            var tipCheck = !_tipValid && _entryToTip.magnitude > _unstabThreshold;

            if (tipCheck)
                return true;

            var point = HVRUtilities.FindNearestPointOnLine(_tip.position, _base.position, StabEntryPosition);
            var line = StabEntryPosition - point;
            if (line.sqrMagnitude > Stabber.Settings.PerpendicularThreshold * Stabber.Settings.PerpendicularThreshold)
            {
                if (Stabber.VerboseDebugging)
                {
                    Debug.Log($"{Stabber.name} -> {Stabbable?.name} unstabbed at d {line.magnitude}.");
                }

                return true;
            }

            if (Stabber.DrawGizmos)
            {
                Debug.DrawLine(point, StabEntryPosition, Color.red);
            }

            if (Stabber.IsDualStabber || Stabber.CanRunThrough)
            {
                var baseCheck = !_baseValid && _exitToBase.magnitude > _unstabThreshold;
                if (baseCheck)
                    return true;
            }

            return false;
        }

        private void UpdateEntryAndExit()
        {
            if (Joint.connectedBody)
            {
                StabEntryPosition = Joint.connectedBody.transform.TransformPoint(_stabLocalPosition);
            }
            else
            {
                StabEntryPosition = _stabLocalPosition;
            }

            StabExitPosition = StabbedObject.transform.TransformPoint(_stabExitLocalPosition);
        }

        private void CheckLock()
        {
            if (!Settings.CanLock || ManuallyLocked)
                return;

            if (Joint.xMotion != ConfigurableJointMotion.Locked && Stabber.Rigidbody.velocity.sqrMagnitude < Settings.LockVelocitySquared)
            {
                if (Stabber.LogJointLock)
                {
                    Debug.Log($"Stabber locked: square mag {Stabber.Rigidbody.velocity.sqrMagnitude} < {Settings.LockVelocitySquared}");
                }

                Lock();
            }
            else if (Joint.xMotion == ConfigurableJointMotion.Locked)
            {
                if (Stabber.LogJointForce && Joint.currentForce.magnitude > Stabber.LogJointForceThreshold)
                {
                    Debug.Log($"StabJoint CurrentForce: {Joint.currentForce.magnitude}");
                }

                if (Joint.currentForce.sqrMagnitude > Settings.UnlockForceSquared)
                {
                    _lockTimer += Time.deltaTime;
                    if (_lockTimer > Settings.LockTime)
                    {
                        if (Stabber.LogJointUnlock)
                        {
                            Debug.Log($"Stabber unlocked: {Joint.currentForce.sqrMagnitude}");
                        }

                        Unlock();
                    }
                }
                else
                {
                    _lockTimer = 0f;
                }
            }
        }

        public void Unlock()
        {

            //if (Stabber.IsDualStabber || Stabber.CanRunThrough)
            //{
            //    Joint.xMotion = ConfigurableJointMotion.Free;
            //}
            //else
            {
                Joint.xMotion = ConfigurableJointMotion.Limited;
                Joint.anchor = Stabber.transform.InverseTransformPoint(_tip.position);
            }
        }

        public void Lock()
        {
            Joint.xMotion = ConfigurableJointMotion.Locked;
            Joint.anchor = Stabber.transform.InverseTransformPoint(StabEntryPosition);
        }

        private void UpdateFriction(float depth, float percent)
        {
            var damper = 0f;
            if (!_outerShellBreached)
            {
                damper = Settings.OuterShellDamper;
                _outerShellBreached = depth > Settings.OuterShellThickness;
            }

            if (_outerShellBreached)
            {
                if (Settings.UseDamperCurve)
                {
                    damper = Settings.InnerDamperCurve.Evaluate(percent) * Settings.Damper;
                }
                else
                {
                    damper = Settings.Damper;
                }
            }

            var drive = Joint.xDrive;
            drive.positionDamper = damper * (1 - Stabber.Settings.Sharpness);
            Joint.xDrive = drive;
        }




        private void CheckFullStab(float depth)
        {
            if ((Stabber.IsDualStabber || Settings.CanBeImpaled) && !_hasEntered)
                return;

            if (_canFullStab && _tipValid)
            {
                if (!_hasFullStabbed && _previousDepth < Settings.FullStabDepth && depth >= Settings.FullStabDepth)
                {
                    _hasFullStabbed = true;
                    if (!Settings.ManyFullStabs)
                        _canFullStab = false;

                    Stabber.OnFullStab(Stabbable);

                }
                else if (_hasFullStabbed)
                {
                    if (_previousDepth > Settings.FullStabResetDepth && depth <= Settings.FullStabResetDepth)
                    {
                        _hasFullStabbed = false;
                        if (Stabber.VerboseDebugging)
                        {
                            Debug.Log($"{Stabber.name} -> {Stabbable?.name} Full Stab Reset");
                        }
                    }
                }
            }
        }

        public HVRStabTracker(HVRStabber stabber, HVRStabbable stabbable, HVRStabbableSettings settings,
            ConfigurableJoint joint, GameObject stabbedObject, Vector3 stabDirection, Transform tip, List<Collider> stabbedColliders)
        {
            Stabber = stabber;
            Stabbable = stabbable;
            Settings = settings;

            Joint = joint;
            StabbedObject = stabbedObject;
            StabbedColliders = stabbedColliders;

            _stabberLength = stabber.Length; //use world in case stabber is scaled
            _canFullStab = true;
            _stabLocalPosition = Joint.connectedAnchor;
            _stabDirectionLocal = stabbedObject.transform.InverseTransformDirection(stabDirection);
            _tip = tip;
            _base = tip == stabber.Tip ? stabber.Base : stabber.Tip;

            _unstabThreshold = Stabber.Settings.UnstabThreshold;

            UpdateEntryAndExit();

            if (Stabber.IsDualStabber || settings.CanBeImpaled || Stabber.CanRunThrough)
            {
                TryFindExitPoint(stabDirection, stabbedColliders);
            }
        }

        private void TryFindExitPoint(Vector3 stabDirection, List<Collider> stabbedColliders)
        {

            Vector3 point = StabEntryPosition;


            var distance = 0f;

            Bounds bounds = new Bounds();
            var first = true;
            foreach (var sc in stabbedColliders)
            {
                if (first)
                {
                    bounds = sc.bounds;
                    first = false;
                }
                else
                {

                    bounds.Encapsulate(sc.bounds);
                }
            }

            var size = bounds.size;
            var rayLength = Mathf.Sqrt(size.x * size.x + size.y * size.y + size.z * size.z);

            foreach (var sc in stabbedColliders)
            {
                var buffer = 0f;

                var mesh = sc as MeshCollider;
                float length;
                Vector3 origin;
                Ray ray;

                if (mesh && mesh != null && !mesh.convex)
                {
                    var count = 0;
                    var didHit = false;
                    while (count < 5)
                    {
                        length = _stabberLength + buffer;
                        origin = StabEntryPosition + stabDirection.normalized * length;
                        ray = new Ray(origin, -stabDirection);

                        if (sc.Raycast(ray, out var concaveHit, length + buffer))
                        {
                            var delta = Vector3.Distance(StabEntryPosition, concaveHit.point);
                            if (delta > distance)
                            {
                                point = concaveHit.point;
                                distance = delta;
                            }

                            didHit = true;
                            break;
                        }

                        buffer += .3f;
                        count++;
                    }

                    if (didHit)
                    {
                        continue;
                    }
                }


                length = rayLength;

                origin = StabEntryPosition + stabDirection.normalized * length;
                ray = new Ray(origin, -stabDirection);

                if (sc.Raycast(ray, out var hit, length))
                {
                    var delta = Vector3.Distance(StabEntryPosition, hit.point);
                    if (delta > distance)
                    {
                        point = hit.point;
                        distance = delta;
                    }
                }
            }

            _stabExitLocalPosition = StabbedObject.transform.InverseTransformPoint(point);

        }

        //private bool CheckCapsuleOverlap()
        //{
        //    if (Stabber.CapsuleCheckColliders == null)
        //        return false;

        //    foreach (var capsule in Stabber.CapsuleCheckColliders)
        //    {
        //        var distanceToPoint = capsule.height * .5f - capsule.radius;
        //        var center = capsule.transform.TransformPoint(capsule.center);

        //        var p0 = center + capsule.transform.up * distanceToPoint;
        //        var p1 = center - capsule.transform.up * distanceToPoint;

        //        var count = Physics.OverlapCapsuleNonAlloc(p0, p1, capsule.radius, Stabber.OverlapColliders, ~0, QueryTriggerInteraction.Ignore);

        //        for (var i = 0; i < count; i++)
        //        {
        //            foreach (var collider in StabbedColliders)
        //            {
        //                if (collider == Stabber.OverlapColliders[i])
        //                    return true;
        //            }
        //        }
        //    }

        //    return false;
        //}
    }
}