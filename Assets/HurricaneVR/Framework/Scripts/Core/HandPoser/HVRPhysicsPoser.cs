using System.Collections.Generic;
using System.Diagnostics;
using HurricaneVR.Framework.Core.HandPoser.Data;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace HurricaneVR.Framework.Core.HandPoser
{
    [ExecuteInEditMode]
    public class HVRPhysicsPoser : MonoBehaviour
    {
        [Header("Settings")]
        public int Iterations = 50;
        public int RaysPerBone = 2;
        public float SphereRadius = .008f;
        public bool FingerTipOnly;


        [Header("Components")]
        public HVRPosableHand Hand;
        public HVRHandPose OpenPose;
        public HVRHandPose ClosedPose;
        [Tooltip("Forward vector of the palm for aiming")]
        public Transform Palm;


        [Header("Debug")]
        public bool DrawSpheres;
        public bool DrawTips;
        public bool DrawCollisionPoints;
        public bool LogHitColliderNames;
        public int _fingerIndex;
        public LayerMask CurrentMask;


        private readonly Collider[] colliders = new Collider[5];
        private Vector3[] CollisionPoints;
        private readonly List<Vector3> _dummy = new List<Vector3>();
        private bool _validated;

        [SerializeField]
        private List<Vector3>[] _sphereMap;
        [SerializeField]
        private bool[] _collidedBoneTracker;
        [SerializeField]
        private int[] _fingerIndices;

        private int[] _boneIteration;

        public bool LiveUpdate;
        public int[] FingerBends = new int[5];

        private HVRHandPoseData OpenPoseData;
        private HVRHandPoseData ClosedPoseData;

        void Start()
        {
            if (Application.isPlaying && Validate())
                Setup();
        }

        public void Setup()
        {
            if (!Hand)
            {
                Hand = GetComponent<HVRPosableHand>();
            }

            if (OpenPose)
            {
                OpenPoseData = OpenPose.GetPose(Hand.IsLeft);
            }

            if (ClosedPose)
            {
                ClosedPoseData = ClosedPose.GetPose(Hand.IsLeft);
            }

            SetupCollision();
        }

        private void SetupBoneTracker()
        {
            if (Hand)
            {
                var count = 0;
                _fingerIndices = new int[Hand.Fingers.Length];
                for (var i = 0; i < Hand.Fingers.Length; i++)
                {
                    _fingerIndices[i] = count;
                    var finger = Hand.Fingers[i];
                    count += finger.Bones.Count;
                }

                _collidedBoneTracker = new bool[count];
                _sphereMap = new List<Vector3>[count];
                _boneIteration = new int[count];
                CollisionPoints = new Vector3[count];

            }
        }

        void Update()
        {
            UpdateLive();
        }

        private void UpdateLive()
        {
            if (LiveUpdate)
            {
                ResetCollidedBones();

                // var watch = Stopwatch.StartNew();
                for (var f = 0; f < Hand.Fingers.Length; f++)
                {
                    var finger = Hand.Fingers[f];
                    var anyBoneHit = false;

                    for (var i = FingerBends[f]; i < Iterations; i++)
                    {
                        for (var b = 0; b < finger.Bones.Count; b++)
                        {
                            if (CheckBone(f, b, i))
                            {
                                anyBoneHit = true;
                                break;
                            }
                        }

                        if (anyBoneHit)
                            break;
                        FingerBends[f] = i;
                    }


                    for (var i = FingerBends[f]; i >= 0; i--)
                    {
                        anyBoneHit = false;
                        FingerBends[f] = i;
                        for (var b = 0; b < finger.Bones.Count; b++)
                        {
                            if (CheckBone(f, b, i))
                            {
                                anyBoneHit = true;
                                break;
                            }
                        }

                        if (!anyBoneHit)
                            break;
                    }
                }

                CloseHand();

                // watch.Stop();
                //Debug.Log($"{watch.ElapsedMilliseconds}");
            }
        }

        private void ResetCollidedBones()
        {
            for (int i = 0; i < _collidedBoneTracker.Length; i++)
            {
                _collidedBoneTracker[i] = false;
            }
        }

        private bool IsFingerDone(int finger)
        {
            var i = _fingerIndices[finger];
            for (; i < Hand.Fingers[finger].Bones.Count; i++)
            {
                if (!_collidedBoneTracker[i])
                    return false;
            }

            return true;
        }

        private void SetCollisionPoint(int finger, int bone, Vector3 point)
        {
            var i = _fingerIndices[finger];
            CollisionPoints[i + bone] = point;
        }

        private void SetBoneCollided(int finger, int bone)
        {
            var i = _fingerIndices[finger];
            _collidedBoneTracker[i + bone] = true;
        }

        private bool IsBoneCollided(int finger, int bone)
        {
            var i = _fingerIndices[finger];
            return _collidedBoneTracker[i + bone];
        }

        private void SetBoneIteration(int finger, int bone, int iteration)
        {
            var i = _fingerIndices[finger];
            _boneIteration[i + bone] = iteration;
        }

        private int GetBoneIteration(int finger, int bone)
        {
            var i = _fingerIndices[finger];
            return _boneIteration[i + bone];
        }

        private List<Vector3> GetSpheres(int finger, int bone)
        {
            if (_fingerIndices == null || _sphereMap == null)
                return _dummy;

            if (finger >= _fingerIndices.Length)
                return _dummy;

            var i = _fingerIndices[finger];

            if (i + bone >= _sphereMap.Length)
                return _dummy;

            return _sphereMap[i + bone];
        }

        public void SetupCollision()
        {
            if (!Hand)
                return;
            SetupBoneTracker();

            var index = 0;
            foreach (var finger in Hand.Fingers)
            {
                if (!finger.Tip)
                    continue;
                for (var i = 0; i < finger.Bones.Count; i++)
                {
                    _sphereMap[index] = new List<Vector3>();

                    var current = Vector3.zero;
                    var next = i == finger.Bones.Count - 1 ? finger.Tip.localPosition : finger.Bones[i + 1].Transform.localPosition;

                    for (var j = 0; j < RaysPerBone; j++)
                    {
                        var point = Vector3.Lerp(current, next, (j + 1f) / RaysPerBone);
                        _sphereMap[index].Add(point);
                    }

                    index++;
                }
            }
        }

        public void OpenFingers()
        {
            if (Hand && OpenPose)
            {
                Hand.PoseFingers(OpenPose);
            }
        }

        public void TestClose()
        {
            if (!Validate())
                return;

            ResetHand();
            SetupCollision();

            var watch = Stopwatch.StartNew();
            ResetHand();
            SimulateClose(~LayerMask.GetMask("Hand"));
            watch.Stop();
            Debug.Log(watch.ElapsedMilliseconds);
        }


        public void SimulateClose(LayerMask mask)
        {
            if (!_validated)
                return;

            CurrentMask = mask;
            ResetHand();

            CloseHand();
        }

        private void CloseHand()
        {
            for (var f = 0; f < Hand.Fingers.Length; f++)
            {
                for (int j = FingerBends[f]; j < Iterations; j++)
                {
                    CheckFinger(f, ++FingerBends[f]);
                }
            }
        }


        public void NextFinger()
        {
            if (_fingerIndex + 1 < 5)
            {
                _fingerIndex++;
            }
        }

        public void StepIteration()
        {
            if (_fingerIndex > 4)
                return;

            if (!Validate())
                return;

            CheckFinger(_fingerIndex, ++FingerBends[_fingerIndex]);
        }

        public void BackStepIteration()
        {
            if (_fingerIndex > 4)
                return;

            if (!Validate())
                return;

            CheckFinger(_fingerIndex, --FingerBends[_fingerIndex]);
        }

        public bool Validate()
        {
            _validated = false;
            if (Hand == null)
                return false;

            if (OpenPose)
            {
                OpenPoseData = OpenPose.GetPose(Hand.IsLeft);
            }

            if (ClosedPose)
            {
                ClosedPoseData = ClosedPose.GetPose(Hand.IsLeft);
            }

            if (OpenPoseData == null || ClosedPoseData == null)
            {
                Debug.LogWarning($"Physics Poser Missing Open and/or Closed Poses.");
                return false;
            }


            if (ClosedPoseData.Fingers.Length < Hand.Fingers.Length)
            {
                Debug.LogWarning($"ClosedPose Finger Count is {ClosedPoseData.Fingers.Length} while hand is {Hand.Fingers.Length}");
                return false;
            }

            if (OpenPoseData.Fingers.Length < Hand.Fingers.Length)
            {
                Debug.LogWarning($"OpenPose Finger Count is {ClosedPoseData.Fingers.Length} while hand is {Hand.Fingers.Length}");
                return false;
            }

            for (int i = 0; i < Hand.Fingers.Length; i++)
            {
                var finger = Hand.Fingers[i];

                var closedFinger = ClosedPoseData.Fingers[i];
                var openFinger = OpenPoseData.Fingers[i];

                if (finger.Bones.Count != openFinger.Bones.Count)
                {
                    Debug.LogWarning($"OpenPose Finger {i} bone count incorrect: {openFinger.Bones.Count} expected {finger.Bones.Count}");
                    return false;
                }

                if (finger.Bones.Count != closedFinger.Bones.Count)
                {
                    Debug.LogWarning($"ClosedPose Finger {i} bone count incorrect: {openFinger.Bones.Count} expected {finger.Bones.Count}");
                    return false;
                }
            }
            _validated = true;
            return true;
        }

        private bool CheckFinger(int fingerIndex, int iteration)
        {
            var currentFinger = Hand.Fingers[fingerIndex];

            for (var boneIndex = 0; boneIndex < currentFinger.Bones.Count; boneIndex++)
            {
                if (IsBoneCollided(fingerIndex, boneIndex))
                    continue;

                if (CheckBone(fingerIndex, boneIndex, iteration))
                {
                    //stop moving this and any higher bones
                    for (var y = boneIndex; y >= 0; y--)
                    {
                        SetBoneCollided(fingerIndex, y);
                    }
                }
            }

            return false;
        }

        private bool CheckBone(int fingerIndex, int boneIndex, int iteration)
        {
            var percent = (float)iteration / Iterations;

            var currentFinger = Hand.Fingers[fingerIndex];
            var currentBone = currentFinger.Bones[boneIndex];

            var currentPosition = currentBone.Transform.localPosition;
            var currentRotation = currentBone.Transform.localRotation;

            var openBone = OpenPoseData.Fingers[fingerIndex].Bones[boneIndex];
            var closedBone = ClosedPoseData.Fingers[fingerIndex].Bones[boneIndex];

            currentBone.Transform.localPosition = Vector3.Lerp(openBone.Position, closedBone.Position, percent);
            currentBone.Transform.localRotation = Quaternion.Lerp(openBone.Rotation, closedBone.Rotation, percent);

            var points = GetSpheres(fingerIndex, boneIndex);

            if (boneIndex != currentFinger.Bones.Count - 1 && FingerTipOnly)
                return false;

            for (var i = 0; i < points.Count; i++)
            {
                var point = points[i];

                var world = currentBone.Transform.TransformPoint(point);

                int hits;
                //is there a better way to know if we're in scene or prefab view ? this is required to work in prefab view.
                //if (editor)
                //{
                //    hits = gameObject.scene.GetPhysicsScene().OverlapSphere(world, SphereRadius, colliders, CurrentMask, QueryTriggerInteraction.Ignore);
                //}
                //else
                {
                    hits = Physics.OverlapSphereNonAlloc(world, SphereRadius, colliders, CurrentMask, QueryTriggerInteraction.Ignore);
                }

                if (hits > 0)
                {
                    if (DrawCollisionPoints)
                    {
                        SetCollisionPoint(fingerIndex, boneIndex, world);
                    }

                    if (LogHitColliderNames)
                    {
                        for (int h = 0; h < hits; h++)
                        {
                            Debug.Log(currentBone.Transform.name + " collided with " + colliders[h].name);
                        }
                    }

                    currentBone.Transform.localPosition = currentPosition;
                    currentBone.Transform.localRotation = currentRotation;

                    return true;
                }
                else
                {
                    if (DrawCollisionPoints)
                    {
                        SetCollisionPoint(fingerIndex, boneIndex, Vector3.zero);
                    }
                }
            }

            return false;
        }

        public void ResetHand()
        {
            for (int i = 0; i < FingerBends.Length; i++)
            {
                FingerBends[i] = 0;
            }

            ResetCollidedBones();

            _fingerIndex = 0;
        }

        private void OnDrawGizmos()
        {
            if (!Hand)
            {
                return;
            }

            for (var f = 0; f < Hand.Fingers.Length; f++)
            {
                var finger = Hand.Fingers[f];
                if (DrawSpheres)
                {
                    for (var b = 0; b < finger.Bones.Count; b++)
                    {
                        if (FingerTipOnly && b != finger.Bones.Count - 1)
                            continue;
                        var bone = finger.Bones[b];

                        //if (!SphereMap.TryGetValue(bone.Transform, out var points)) continue;

                        var points = GetSpheres(f, b);

                        for (var i = 0; i < points.Count; i++)
                        {
                            var point = points[i];
                            var worldPosition = bone.Transform.TransformPoint(point);
                            Gizmos.color = Color.cyan;
                            Gizmos.DrawWireSphere(worldPosition, SphereRadius);
                        }
                    }
                }

                if (DrawTips)
                {
                    if (finger.Tip)
                    {
                        Gizmos.color = Color.magenta;
                        Gizmos.DrawWireSphere(finger.Tip.position, SphereRadius);
                    }
                }
            }

            if (DrawCollisionPoints)
            {
                foreach (var point in CollisionPoints)
                {
                    if (point != Vector3.zero)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawWireSphere(point, SphereRadius);
                    }
                }
            }
        }
    }
}
