using System.Linq;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.HandPoser;
using HurricaneVR.Framework.Shared;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace HurricaneVR.Framework.Core.Utils
{
    public class HVRHandColliderController : MonoBehaviour
    {
        public Transform LeftParent;
        public Transform RightParent;

        public BoxCollider LeftHandBox;
        public BoxCollider RightHandBox;

        public HVRPosableHand LeftPosableHand;
        public HVRPosableHand RightPosableHand;

        public Transform LeftHand;
        public Transform LeftLowerArm;

        public Transform RightHand;
        public Transform RightLowerArm;

        public float RightHandShrink;
        public float LeftHandShrink;
        public AnimationCurve HandCurve;

        [Header("Lower Arm Capsule")]

        [Tooltip("Janky, not really ready for use yet, breaks down when climbing stuff.")]
        public bool EnableArmCapsule;

        public float Radius = .02f;
        public float LengthFactor = .5f;

        public CapsuleCollider LeftForearmCapsule;
        public CapsuleCollider RightForearmCapsule;

        public Animator SetupAnimator;

        private float _rightLength;
        private float _leftLength;

        private bool _curveValid;

        [SerializeField] private Vector3 _leftPos;
        [SerializeField] private Vector3 _rightPos;

        void Start()
        {
            if (HandCurve.keys.Length > 0)
                _curveValid = true;

            if (LeftHandBox)
            {
                _leftLength = LeftHandBox.size.z;
                _leftPos = LeftHandBox.transform.localPosition;
            }

            if (RightHandBox)
            {
                _rightLength = RightHandBox.size.z;
                _rightPos = RightHandBox.transform.localPosition;
            }

            if (!LeftPosableHand) LeftPosableHand = transform.GetComponentsInChildren<HVRPosableHand>().FirstOrDefault(e => e.IsLeft);
            if (!RightPosableHand) RightPosableHand = transform.GetComponentsInChildren<HVRPosableHand>().FirstOrDefault(e => !e.IsLeft);

            if (LeftForearmCapsule) LeftForearmCapsule.direction = 2;
            if (RightForearmCapsule) RightForearmCapsule.direction = 2;

            if (!EnableArmCapsule)
            {
                if (LeftForearmCapsule) LeftForearmCapsule.enabled = false;
                if (RightForearmCapsule) RightForearmCapsule.enabled = false;
            }
        }

        void Update()
        {

            if (_curveValid)
            {
                UpdateHand(HVRController.LeftFingerCurls, LeftHandBox, _leftLength, _leftPos, LeftHandShrink);
                UpdateHand(HVRController.RightFingerCurls, RightHandBox, _rightLength, _rightPos, RightHandShrink);
            }

            if (EnableArmCapsule)
            {
                if (LeftHand && LeftLowerArm && LeftForearmCapsule)
                    UpdateForearm(LeftHand, LeftLowerArm, LeftForearmCapsule);

                if (RightHand && RightLowerArm && RightForearmCapsule)
                    UpdateForearm(RightHand, RightLowerArm, RightForearmCapsule);
            }
        }

        private void UpdateForearm(Transform hand, Transform arm, CapsuleCollider capsule)
        {
            var end = Vector3.Lerp(hand.position, arm.position, LengthFactor);
            var center = (end + hand.position) * .5f;
            capsule.height = Vector3.Distance(end, hand.position);
            capsule.transform.position = center;
            capsule.transform.rotation = Quaternion.FromToRotation(capsule.transform.forward, end - hand.position) * capsule.transform.rotation;
        }

        private void UpdateHand(float[] curls, BoxCollider box, float length, Vector3 pos, float handShrink)
        {
            if (box)
            {
                var min = float.MaxValue;
                for (var i = 2; i < curls.Length; i++)
                {
                    if (curls[i] < min)
                    {
                        min = curls[i];
                    }
                }

                min = Mathf.Clamp01(min);

                var z = Mathf.Max(length - HandCurve.Evaluate(min) * handShrink, .01f);

                var center = pos - Vector3.forward * ((length - z) * .5f);

                box.transform.localPosition = center;
                box.size = new Vector3(box.size.x, box.size.y, z);
            }
        }

#if UNITY_EDITOR

        [InspectorButton("AddHandBoxesF")]
        public string AddHandBoxes;

        [InspectorButton("AddArmCapsulesF")]
        public string AddArmCapsules;

        public void AddHandBoxesF()
        {
            if (!LeftPosableHand) LeftPosableHand = GetComponentsInChildren<HVRPosableHand>().FirstOrDefault(e => e.IsLeft);
            if (!RightPosableHand) RightPosableHand = GetComponentsInChildren<HVRPosableHand>().FirstOrDefault(e => !e.IsLeft);

            AddHandBox(LeftPosableHand, ref LeftHandBox, LeftParent);
            AddHandBox(RightPosableHand, ref RightHandBox, RightParent);

            if (LeftHandBox)
            {
                LeftHandShrink = LeftHandBox.size.z * .4f;
            }

            if (RightHandBox)
            {
                RightHandShrink = RightHandBox.size.z * .4f;
            }
        }

        public void AddArmCapsulesF()
        {
            if (LeftParent)
            {
                var left = AddArmCapsule(LeftParent);
                if (left) LeftForearmCapsule = left;
                UpdateForearm(LeftHand, LeftLowerArm, LeftForearmCapsule);
            }

            if (RightParent)
            {
                var right = AddArmCapsule(RightParent);
                if (right) RightForearmCapsule = right;
                UpdateForearm(RightHand, RightLowerArm, RightForearmCapsule);
            }
        }

        private CapsuleCollider AddArmCapsule(Transform parent)
        {
            var capsuleT = Add(parent, "ForearmCapsule");
            var capsule = capsuleT.UndoEnsureComponent<CapsuleCollider>();
            capsule.radius = Radius;
            capsule.direction = 2;
            return capsule;
        }


        private void AddHandBox(HVRPosableHand posableHand, ref BoxCollider box, Transform parent)
        {
            var handBone = posableHand.transform;
            var index = posableHand.Index;
            HVRPosableFinger lastFinger;
            if (index == null)
            {
                Debug.LogWarning($"{posableHand.name} missing index finger assignment. Aborting..");
                return;
            }

            if (posableHand.Pinky != null && posableHand.Pinky.Bones.Count > 0)
                lastFinger = posableHand.Pinky;
            else if (posableHand.Ring != null && posableHand.Ring.Bones.Count > 0)
                lastFinger = posableHand.Ring;
            else if (posableHand.Middle != null && posableHand.Middle.Bones.Count > 0)
                lastFinger = posableHand.Middle;
            else
            {
                Debug.LogWarning($"{posableHand.name} missing pinky, middle, and ring fingers. Aborting...");
                return;
            }

            var tip = index.Tip;
            var length = Vector3.Distance(tip.position, handBone.position);

            var width = Vector3.Distance(index.Tip.position, lastFinger.Tip.position);

            Transform handBox;
            if (!box)
            {
                handBox = Add(parent, "HandBox");
                box = handBox.UndoEnsureComponent<BoxCollider>();
            }
            else
            {
                handBox = box.transform;
            }

            box.size = new Vector3(.035f, width, length);

            var midY = (index.Tip.position + lastFinger.Tip.position) / 2f;
            var rotTip = index.Tip;
            if (posableHand.Middle != null && posableHand.Middle.Tip != null)
                rotTip = posableHand.Middle.Tip;
            handBox.rotation = Quaternion.LookRotation(rotTip.position - handBone.position, index.Tip.position - lastFinger.Tip.position);
            var mid = ((rotTip.position + handBone.position) / 2f);
            handBox.position = new Vector3(mid.x, midY.y, mid.z);
        }

        private Transform Add(Transform parent, string name)
        {
            var existing = parent.Find(name);

            if (!existing)
            {
                var tip = new GameObject(name);
                tip.transform.parent = parent;
                tip.transform.ResetLocalProps();
                existing = tip.transform;
                Undo.RegisterCreatedObjectUndo(tip, $"Add {name} to {parent.name}");
                Debug.Log($"{tip.name} added to {parent.name}");
            }

            return existing;
        }

#endif
    }
}
