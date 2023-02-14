#if HVR_OCULUS
using System;
using System.Collections.Generic;
using HurricaneVR.Framework.Shared;
using UnityEngine;
using UnityEngine.Events;

#if HVR_INPUT_SYSTEM && ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace HurricaneVR.Framework.Oculus
{
    public class HVRGestureDetector : MonoBehaviour
    {
        public OVRSkeleton Skeleton;
        public List<HVRGesture> Gestures = new List<HVRGesture>();
        private HVRGesture CurrentGesture;
        private HVRGesture _previousGesture;

        public float BoneThreshold = .1f;
        public float GestureTimeNeeded = 1.5f;
        public float GestureTimer;
        public HVRHandSide HandSide;
        private bool _activated;

        private void Start()
        {
        }

        private void Update()
        {
#if UNITY_EDITOR

            CheckAddGesture();

#endif
            if (!Skeleton || !Skeleton.IsInitialized || !Skeleton.IsDataValid)
            {
                return;
            }

            var closest = DetectGesture();

            if (CurrentGesture == null && closest != null)
            {
                CurrentGesture = closest;
            }

            if (CurrentGesture == closest && CurrentGesture != null)
            {
                GestureTimer += Time.deltaTime;
            }
            else
            {
                GestureTimer -= Time.deltaTime;
            }

            if (_activated && CurrentGesture != _previousGesture)
            {
                GestureTimer = 0f;

            }
            GestureTimer = Mathf.Clamp(GestureTimer, 0, GestureTimeNeeded + .1f);
            if (Mathf.Approximately(GestureTimer, 0f))
            {
                CurrentGesture = closest;
            }

            if (!_activated && GestureTimer > GestureTimeNeeded && CurrentGesture != null)
            {
                _activated = true;
                CurrentGesture.GestureCompleted.Invoke();
                Debug.Log($"{CurrentGesture.Name} gestured!");
            }

            if (CurrentGesture != _previousGesture && _activated)
            {
                _activated = false;
            }

            _previousGesture = CurrentGesture;

        }

        private void CheckAddGesture()
        {
            bool pressed;
#if ENABLE_LEGACY_INPUT_MANAGER
            pressed = Input.GetKeyDown(KeyCode.L) && HandSide == HVRHandSide.Left || Input.GetKeyDown(KeyCode.R) && HandSide == HVRHandSide.Right;
#elif HVR_INPUT_SYSTEM && ENABLE_INPUT_SYSTEM
            pressed = Keyboard.current[Key.L].wasPressedThisFrame && HandSide == HVRHandSide.Left ||
                Keyboard.current[Key.R].wasPressedThisFrame && HandSide == HVRHandSide.Right;
#endif

            if (pressed)
            {
                var gesture = new HVRGesture();
                gesture.Name = "NoName";
                foreach (var bone in Skeleton.Bones)
                {
                    gesture.PositionOffsets.Add(Skeleton.transform.InverseTransformPoint(bone.Transform.position));
                }

                Gestures.Add(gesture);
                DetectGesture();

#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }

        private HVRGesture DetectGesture()
        {
            HVRGesture closest = null;

            var currentMin = Mathf.Infinity;
            for (var j = 0; j < Gestures.Count; j++)
            {
                var gesture = Gestures[j];
                var total = 0f;
                var isInvalid = false;

                for (var i = 0; i < Skeleton.Bones.Count; i++)
                {
                    var bone = Skeleton.Bones[i];
                    var position = Skeleton.transform.InverseTransformPoint(bone.Transform.position);
                    var distance = Vector3.Distance(position, gesture.PositionOffsets[i]);

                    if (distance > BoneThreshold)
                    {
                        isInvalid = true;
                        break;
                    }

                    total += distance;
                }

                if (!isInvalid && total < currentMin)
                {
                    currentMin = total;
                    closest = gesture;
                }
            }

            return closest;
        }
    }

    [Serializable]
    public class HVRGesture
    {
        public string Name;
        public List<Vector3> PositionOffsets = new List<Vector3>();
        public UnityEvent GestureCompleted = new UnityEvent();
    }
}
#endif