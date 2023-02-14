using System.Collections.Generic;
using HurricaneVR.Framework.Core.HandPoser.Data;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.Serialization;
using Time = UnityEngine.Time;

namespace HurricaneVR.Framework.Core.HandPoser
{
    public class HVRHandAnimator : MonoBehaviour
    {
        [FormerlySerializedAs("PosePosAndRot")]
        [Header("Settings")]
        [Tooltip("If true the default poser will pose the hand's local position and rotation")]
        public bool DefaultPoseHand = true; //fyi - false is best, only left true for older users and their projects

        [Tooltip("Finger bend speed when dynamic pose is active")]
        public float DynamicPoseSpeed = 16f;

        [Header("Components")]
        public HVRPhysicsPoser PhysicsPoser;
        public HVRPosableHand Hand;
        public HVRHandPoser DefaultPoser;
        
        [Tooltip("Used if the HVRPosableHand component is not on the transform that needs to be posed. IKTarget for VRIK is one example.")]
        public Transform HandOverride;


        [Header("Debug View")]
        public HVRHandPoser CurrentPoser;

        public HVRHandPoser OverridePoser;

        /// <summary>
        /// Current hand pose, moves towards BlendedPose based on the speed defined on the Primary Pose
        /// </summary>
        private HVRHandPoseData CurrentPose;

        /// <summary>
        /// Resting hand pose when not holding anything
        /// </summary>
        private HVRHandPoseData DefaultPose;

        /// <summary>
        /// Maintains the state the hand should be moving towards
        /// </summary>
        private HVRHandPoseData BlendedPose;

        /// <summary>
        /// Current Primary Pose of the active Hand Poser
        /// </summary>
        private HVRHandPoseData PrimaryPose;

        /// <summary>
        /// Blend poses are copied to this to apply finger curls to.
        /// </summary>
        private HVRHandPoseData BlendTarget;

        /// <summary>
        /// Keeps tracking of each blend pose target current value
        /// </summary>
        private readonly List<HVRHandPoseData> Blends = new List<HVRHandPoseData>(10);


        public bool IsMine { get; set; } = true;

        /// <summary>
        /// Defaults to the finger curl arrays managed by the framework in Start(). Can be overriden after start with a float[5] array if you want to supply your own curl data.
        /// </summary>
        public float[] FingerCurlSource { get; set; }

        /// <summary>
        /// Enable to disable finger curl influence on the hand pose
        /// </summary>
        public bool IgnoreCurls { get; set; } = false;

        public bool DynamicPose { get; set; }

        /// <summary>
        /// Returns true if the CurrentPoser is a pose from a held object
        /// </summary>
        public bool HandHeldPose { get; internal set; }

        private bool _poseHand = true;
        private bool _poseHandOverride;
        private float[] _fingerCurls;

        protected virtual void Start()
        {
            _fingerCurls = new float[5];

            if (!PhysicsPoser)
            {
                PhysicsPoser = GetComponent<HVRPhysicsPoser>();
            }

            if (!DefaultPoser)
            {
                DefaultPoser = GetComponent<HVRHandPoser>();
            }

            if (!Hand)
            {
                Hand = GetComponent<HVRPosableHand>();
            }


            DefaultPose = DefaultPoser.PrimaryPose.Pose.GetPose(Hand.IsLeft).DeepCopy();
            CurrentPose = DefaultPose.DeepCopy();
            BlendedPose = DefaultPose.DeepCopy();
            BlendTarget = DefaultPose.DeepCopy();

            if (IsMine)
            {
                FingerCurlSource = Hand.IsLeft ? HVRController.LeftFingerCurls : HVRController.RightFingerCurls;
            }

            ValidateDefaultFingerType("Thumb", ref DefaultPoser.PrimaryPose.ThumbType);
            ValidateDefaultFingerType("Index", ref DefaultPoser.PrimaryPose.IndexType);
            ValidateDefaultFingerType("Middle", ref DefaultPoser.PrimaryPose.MiddleType);
            ValidateDefaultFingerType("Ring", ref DefaultPoser.PrimaryPose.RingType);
            ValidateDefaultFingerType("Pinky", ref DefaultPoser.PrimaryPose.PinkyType);

            ResetToDefault();
        }

        private void ValidateDefaultFingerType(string fingerName, ref HVRFingerType finger)
        {
            if (finger == HVRFingerType.Close)
            {
                //Debug.LogWarning($"{name} Default HVRHandPoser Primary Pose has Finger Curls [{fingerName}] set to Close. Setting to Static.");
                finger = HVRFingerType.Static;
            }
        }

        protected virtual void LateUpdate()
        {
            UpdateFingerCurls();
            UpdatePoser();
        }

        protected virtual void UpdateFingerCurls()
        {
            if (FingerCurlSource == null)
                return;

            for (int i = 0; i < 5; i++)
            {
                _fingerCurls[i] = FingerCurlSource[i];
            }
        }

        public void ZeroFingerCurls()
        {
            for (int i = 0; i < 5; i++)
            {
                _fingerCurls[i] = 0f;
            }
        }

        public void Enable()
        {
            enabled = true;
        }

        public void Disable()
        {
            enabled = false;
        }

        private void UpdatePoser()
        {
            if (DynamicPose)
            {
                PrimaryPose.CopyTo(BlendedPose);
                ApplyBlend(CurrentPose, BlendedPose, DefaultPoser.PrimaryPose, DynamicPoseSpeed);
                Hand.Pose(CurrentPose, false);
                return;
            }

            var poseHand = _poseHand;
            var poser = CurrentPoser;
            if (OverridePoser && !HandHeldPose)
            {
                poser = OverridePoser;
                poseHand = _poseHandOverride;
            }

            if (!poser)
                return;

            UpdateBlends(poser);
            ApplyBlending(poser);
            
            if (poseHand)
            {
                if (HandOverride)
                {
                    HandOverride.localPosition = CurrentPose.Position;
                    HandOverride.localRotation = CurrentPose.Rotation;
                }
                else
                {
                    Hand.Pose(CurrentPose, true);
                }
            }
            else
            {
                Hand.Pose(CurrentPose, false);
            }
        }

        private void UpdateBlends(HVRHandPoser poser)
        {
            if (!IsMine)
                return;

            UpdateBlendValue(poser.PrimaryPose);

            if (poser.Blends == null)
            {
                return;
            }

            for (int i = 0; i < poser.Blends.Count; i++)
            {
                UpdateBlendValue(poser.Blends[i]);
            }
        }

        /// <summary>
        /// Updates the blend value depending on it's settings, immediate, button parameter, string parameter etc.
        /// </summary>
        /// <param name="blend"></param>
        private void UpdateBlendValue(HVRHandPoseBlend blend)
        {
            if (blend.Disabled)
            {
                blend.Value = 0f;
                return;
            }

            if (blend.Type == BlendType.Manual) return;

            if (blend.Type == BlendType.Immediate)
            {
                blend.Value = 1f;
            }
            else if (blend.ButtonParameter)
            {
                var button = HVRController.GetButtonState(Hand.Side, blend.Button);
                if (blend.Type == BlendType.BooleanParameter)
                {
                    blend.Value = button.Active ? 1f : 0f;
                }
                else if (blend.Type == BlendType.FloatParameter)
                {
                    blend.Value = button.Value;
                }
            }
            else if (!string.IsNullOrWhiteSpace(blend.AnimationParameter) && blend.AnimationParameter != "None")
            {
                if (blend.Type == BlendType.BooleanParameter)
                {
                    blend.Value = HVRAnimationParameters.GetBoolParameter(Hand.Side, blend.AnimationParameter) ? 1f : 0f;
                }
                else if (blend.Type == BlendType.FloatParameter)
                {
                    blend.Value = HVRAnimationParameters.GetFloatParameter(Hand.Side, blend.AnimationParameter);
                }
            }
        }


        private void ApplyBlending(HVRHandPoser poser)
        {
            PrimaryPose.CopyTo(BlendedPose);
            ApplyFingerCurls(DefaultPose, PrimaryPose, BlendedPose, poser.PrimaryPose);

            if (poser.Blends != null)
            {
                for (int i = 0; i < poser.Blends.Count; i++)
                {
                    var blend = poser.Blends[i];

                    if (blend.Disabled || !blend.Pose)
                    {
                        continue;
                    }

                    var blendPose = blend.Pose.GetPose(Hand.Side);

                    if (blendPose == null) continue;

                    blendPose.CopyTo(BlendTarget); //copied to apply the finger curls to prevent mucking up the original pose

                    //apply finger curl to target pose
                    ApplyFingerCurls(PrimaryPose, blendPose, BlendTarget, blend);

                    //updated target pose by weighted value and speed
                    UpdateBlendTarget(PrimaryPose, BlendTarget, Blends[i], blend);

                    //update blended pose by weight to target
                    UpdateBlendedPose(Blends[i], BlendedPose, blend);
                }
            }

            ApplyBlend(CurrentPose, BlendedPose, poser.PrimaryPose, poser.PrimaryPose.Speed);
        }

        /// <summary>
        /// Adjusts the pose target by lerping between the hand relaxed pose and the target pose using finger curl tracking
        /// </summary>
        private void ApplyFingerCurls(HVRHandPoseData startPose, HVRHandPoseData endPose, HVRHandPoseData targetPose, HVRHandPoseBlend blend)
        {
            for (int i = 0; i < targetPose.Fingers.Length; i++)
            {
                var targetFinger = targetPose.Fingers[i];
                var startFinger = startPose.Fingers[i];
                var endFinger = endPose.Fingers[i];

                var fingerType = blend.GetFingerType(i);

                if (fingerType != HVRFingerType.Close)
                    continue;

                var fingerStart = blend.GetFingerStart(i);
                var curl = _fingerCurls[i];

                if (IgnoreCurls)
                {
                    curl = 0f;
                }

                var remainder = 1 - fingerStart;
                curl = fingerStart + curl * remainder;
                curl = Mathf.Clamp(curl, 0f, 1f);


                for (int j = 0; j < targetFinger.Bones.Count; j++)
                {
                    targetFinger.Bones[j].Position = Vector3.Lerp(startFinger.Bones[j].Position, endFinger.Bones[j].Position, curl);
                    targetFinger.Bones[j].Rotation = Quaternion.Lerp(startFinger.Bones[j].Rotation, endFinger.Bones[j].Rotation, curl);
                }
            }
        }

        /// <summary>
        /// Moves the blended pose towards the weight target lerp(start, end, value * weight) using the blend speed
        /// </summary>
        private void UpdateBlendTarget(HVRHandPoseData startPose, HVRHandPoseData endPose, HVRHandPoseData blendedPose, HVRHandPoseBlend blend)
        {
            var lerp = blend.Value * blend.Weight;
            var blendLerp = blend.Speed < .01f ? 1f : blend.Speed * Time.deltaTime;

            if (blend.Mask == HVRHandPoseMask.None || (blend.Mask & HVRHandPoseMask.Hand) == HVRHandPoseMask.Hand)
            {
                var targetPos = Vector3.Lerp(startPose.Position, endPose.Position, lerp);
                var targetRot = Quaternion.Lerp(startPose.Rotation, endPose.Rotation, lerp);

                blendedPose.Position = Vector3.Lerp(blendedPose.Position, targetPos, blendLerp);
                blendedPose.Rotation = Quaternion.Lerp(blendedPose.Rotation, targetRot, blendLerp);
            }

            for (var i = 0; i < blendedPose.Fingers.Length; i++)
            {
                var blendedFinger = blendedPose.Fingers[i];
                var endFinger = endPose.Fingers[i];
                var startFinger = startPose.Fingers[i];

                if (!TryGetMask(i, out var mask)) continue;

                if (blend.Mask == HVRHandPoseMask.None || (blend.Mask & mask) == mask)
                {
                    for (var j = 0; j < blendedFinger.Bones.Count; j++)
                    {
                        var blendedBone = blendedFinger.Bones[j];
                        var endBone = endFinger.Bones[j];
                        var startBone = startFinger.Bones[j];

                        var targetPos = Vector3.Lerp(startBone.Position, endBone.Position, lerp);
                        var targetRot = Quaternion.Lerp(startBone.Rotation, endBone.Rotation, lerp);

                        blendedBone.Position = Vector3.Lerp(blendedBone.Position, targetPos, blendLerp);
                        blendedBone.Rotation = Quaternion.Lerp(blendedBone.Rotation, targetRot, blendLerp);
                    }
                }
            }
        }

        /// <summary>
        /// Updates the blended pose to the blend target based on weighted value.
        /// </summary>
        private void UpdateBlendedPose(HVRHandPoseData targetPose, HVRHandPoseData blendedPose, HVRHandPoseBlend blend)
        {
            var lerp = blend.Value * blend.Weight;

            if (blend.Mask == HVRHandPoseMask.None || (blend.Mask & HVRHandPoseMask.Hand) == HVRHandPoseMask.Hand)
            {
                blendedPose.Position = Vector3.Lerp(blendedPose.Position, targetPose.Position, lerp);
                blendedPose.Rotation = Quaternion.Lerp(blendedPose.Rotation, targetPose.Rotation, lerp);
            }

            for (var i = 0; i < blendedPose.Fingers.Length; i++)
            {
                if (!TryGetMask(i, out var mask)) continue;

                if (blend.Mask == HVRHandPoseMask.None || (blend.Mask & mask) == mask)
                {
                    for (var j = 0; j < blendedPose.Fingers[i].Bones.Count; j++)
                    {
                        var blendedBone = blendedPose.Fingers[i].Bones[j];

                        blendedBone.Position = Vector3.Lerp(blendedBone.Position, targetPose.Fingers[i].Bones[j].Position, lerp);
                        blendedBone.Rotation = Quaternion.Lerp(blendedBone.Rotation, targetPose.Fingers[i].Bones[j].Rotation, lerp);
                    }
                }
            }
        }


        private static bool TryGetMask(int i, out HVRHandPoseMask mask)
        {
            mask = HVRHandPoseMask.None;
            if (i == 0) mask = HVRHandPoseMask.Thumb;
            else if (i == 1) mask = HVRHandPoseMask.Index;
            else if (i == 2) mask = HVRHandPoseMask.Middle;
            else if (i == 3) mask = HVRHandPoseMask.Ring;
            else if (i == 4) mask = HVRHandPoseMask.Pinky;
            else return false;
            return true;
        }

        private void ApplyBlend(HVRHandPoseData currentHand, HVRHandPoseData targetHandPose, HVRHandPoseBlend blend, float speed)
        {
            var blendLerp = speed < .01f ? 1f : speed * Time.deltaTime;

            if (blend.Mask == HVRHandPoseMask.None || (blend.Mask & HVRHandPoseMask.Hand) == HVRHandPoseMask.Hand)
            {
                currentHand.Position = Vector3.Lerp(currentHand.Position, targetHandPose.Position, blendLerp);
                currentHand.Rotation = Quaternion.Lerp(currentHand.Rotation, targetHandPose.Rotation, blendLerp);
            }

            for (var i = 0; i < currentHand.Fingers.Length; i++)
            {
                var currentFinger = currentHand.Fingers[i];
                var targetFinger = targetHandPose.Fingers[i];

                if (!TryGetMask(i, out var mask)) continue;

                if (blend.Mask == HVRHandPoseMask.None || (blend.Mask & mask) == mask)
                {
                    for (var j = 0; j < currentFinger.Bones.Count; j++)
                    {
                        var currentBone = currentFinger.Bones[j];
                        var targetBone = targetFinger.Bones[j];

                        currentBone.Position = Vector3.Lerp(currentBone.Position, targetBone.Position, blendLerp);
                        currentBone.Rotation = Quaternion.Lerp(currentBone.Rotation, targetBone.Rotation, blendLerp);
                    }
                }
            }
        }

        public void ResetToDefault()
        {
            DynamicPose = false;
            _poseHand = DefaultPoseHand;
            CurrentPoser = DefaultPoser;
            if (HandHeldPose || !OverridePoser)
            {
                SetupPoser(CurrentPoser);
            }
            else if (OverridePoser)
            {
                SetupPoser(OverridePoser);
            }
        }

        public void StartDynamicPose(HVRHandPoseData pose)
        {
            CurrentPoser = null;
            DynamicPose = true;
            PrimaryPose = pose;
            _poseHand = false;
        }

        /// <summary>
        /// Sets or reset (pass null) a poser that will take precedence over framework hover posers, but won't take precedence over held object poses.
        /// </summary>
        public virtual void SetOverridePoser(HVRHandPoser poser, bool poseHand = false)
        {
            _poseHandOverride = poseHand;
            OverridePoser = poser;

            if (!poser)
            {
                SetupPoser(CurrentPoser);
            }
            else if (!HandHeldPose)
            {
                SetupPoser(poser);
            }
        }

        public void SetHeldPoser(HVRHandPoser poser, bool poseHand = false)
        {
            HandHeldPose = true;
            _poseHand = poseHand;
            CurrentPoser = poser;
            SetupPoser(poser);
        }

        public void OnHeldObjectReleased()
        {
            HandHeldPose = false;
            DynamicPose = false;
            CurrentPoser = DefaultPoser;
            _poseHand = DefaultPoseHand;
            if (OverridePoser)
            {
                SetupPoser(OverridePoser);
            }
            else
            {
                SetupPoser(CurrentPoser);
            }
        }


        public virtual void SetCurrentPoser(HVRHandPoser poser, bool poseHand = false)
        {
            _poseHand = poseHand;
            CurrentPoser = poser;
            if (!OverridePoser || HandHeldPose)
                SetupPoser(poser);
        }

        protected virtual void SetupPoser(HVRHandPoser poser)
        {
            if (!poser || poser.PrimaryPose == null) return;

            PrimaryPose = poser.PrimaryPose.Pose.GetPose(Hand.IsLeft);

            //set the currentpose to the current hand state
            Hand.CopyHandData(CurrentPose);

            //add blends to cache if necessary
            if (poser.Blends.Count > Blends.Count)
            {
                var count = Blends.Count;
                for (int i = 0; i < poser.Blends.Count - count; i++)
                {
                    Blends.Add(DefaultPose.DeepCopy());
                }
            }


            for (var i = 0; i < poser.Blends.Count; i++)
            {
                poser.Blends[i].Value = 0f; //reset blend weight
                PrimaryPose.CopyTo(Blends[i]); //copy primary pose to the blend pose as a base
            }
        }
    }
}