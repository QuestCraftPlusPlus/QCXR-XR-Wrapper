using System;
using System.Collections;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.HandPoser;
using HurricaneVR.Framework.Shared;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace HurricaneVR.Framework.Components
{
    public class HVRHandPoseRecorder : MonoBehaviour
    {
#if ENABLE_LEGACY_INPUT_MANAGER
        public KeyCode LeftHandSaveKey = KeyCode.L;
        public KeyCode RightHandSaveKey = KeyCode.R;
#endif

#if ENABLE_INPUT_SYSTEM

        public Key LeftSaveKey = Key.L;
        public Key RightSaveKey = Key.R;

#endif


        public HVRPosableHand LeftHand;
        public HVRPosableHand RightHand;

        public HVRHandPhysics LeftPhysics;
        public HVRHandPhysics RightPhysics;

        public float FadeTimer = 10f;
        public bool RemoveClones = true;

        public bool DisablePhysics;

        private bool _previousDisable;

        public string Folder;
        public int Counter = 0;

        public void Start()
        {
            Folder = DateTime.Now.ToString("yyyyMMdd_HH_mm");
        }
#if UNITY_EDITOR
        void Update()
        {


            if (DisablePhysics && !_previousDisable)
            {
                if (LeftPhysics)
                {
                    LeftPhysics.DisableCollision();
                }

                if (RightPhysics)
                {
                    RightPhysics.DisableCollision();
                }
            }
            else if (!DisablePhysics && _previousDisable)
            {
                if (LeftPhysics)
                {
                    LeftPhysics.EnableCollision();
                }

                if (RightPhysics)
                {
                    RightPhysics.EnableCollision();
                }
            }

            _previousDisable = DisablePhysics;

            CheckSnapshot();

        }
#endif
        private void CheckSnapshot()
        {
            HVRPosableHand hand = null;

#if ENABLE_LEGACY_INPUT_MANAGER
            if (Input.GetKeyDown(LeftHandSaveKey))
            {
                hand = LeftHand;
            }
            else if (Input.GetKeyDown(RightHandSaveKey))
            {
                hand = RightHand;
            }
            else
                return;
#elif ENABLE_INPUT_SYSTEM

            if (Keyboard.current[LeftSaveKey].wasPressedThisFrame)
            {
                hand = LeftHand;
            }
            else if (Keyboard.current[RightSaveKey].wasPressedThisFrame)
            {
                hand = RightHand;
            }
            else
                return;
#endif

            if (!hand)
                return;

            Snapshot(hand);
        }

        public void SnapshotLeft()
        {
            if (!gameObject.activeSelf)
                return;
            if (LeftHand)
            {
                Snapshot(LeftHand);
            }
        }

        public void SnapshotRight()
        {
            if (!gameObject.activeSelf)
                return;

            if (RightHand)
            {
                Snapshot(RightHand);
            }
        }

        private void Snapshot(HVRPosableHand hand)
        {

#if UNITY_EDITOR
            var pose = hand.CreateFullHandPoseWorld(hand.MirrorAxis);

            HVRSettings.Instance.SaveRunTimePose(pose, Counter++.ToString(), Folder);

            var clone = Instantiate(HVRSettings.Instance.GetPoserHand(hand.Side));

            var posableHand = clone.GetComponent<HVRPosableHand>();
            if (posableHand != null)
            {
                posableHand.Pose(pose.GetPose(hand.Side));
                clone.transform.position = hand.transform.position;
                clone.transform.rotation = hand.transform.rotation;
            }

            var colliders = clone.GetComponentsInChildren<Collider>();
            foreach (var c in colliders)
            {
                c.enabled = false;
            }

            if (RemoveClones)
            {
                StartCoroutine(RemoveClone(clone));
            }
#endif
        }

        public IEnumerator RemoveClone(GameObject clone)
        {
            yield return new WaitForSeconds(FadeTimer);
            Destroy(clone);
        }
    }
}
