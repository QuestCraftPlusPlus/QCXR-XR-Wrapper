using System;
using Assets.HurricaneVR.Framework.Shared.Utilities;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.Framework.Core
{
    [RequireComponent(typeof(HVRGrabbable))]
    public class HVRSocketLink : MonoBehaviour
    {
        public HVRSocket Socket;
        public float ReturnTime = 0f;
        public bool PlaySocketedSFX = true;

        public HVRGrabbable Grabbable { get; private set; }

        private bool _returning;
        private float _time;
        private float _elapsed;
        private Vector3 _startPos;
        private Quaternion _startRot;

        protected virtual void Awake()
        {
            Setup();
        }

        public void Setup()
        {
            Grabbable = GetComponent<HVRGrabbable>();
            Grabbable.HandFullReleased.AddListener(OnGrabbableReleased);
            if (!Grabbable.Socketable)
            {
                //Socket code expects this, add dummy if not found.
                Grabbable.Socketable = Grabbable.gameObject.AddComponent<HVRLinkedSocketable>();
            }
            if (Socket)
            {
                Socket.LinkedGrabbable = Grabbable;
                this.ExecuteNextUpdate(() => Socket.TryGrab(Grabbable, true, true));
            }
        }

        protected virtual void Update()
        {
            if (_returning)
            {
                var pos = Socket.transform.TransformPoint(Socket.GetTargetPosition(Grabbable));
                var rot = Socket.transform.rotation * Socket.GetTargetRotation(Grabbable);
                var lerp = _elapsed / _time;
                _elapsed += Time.deltaTime;
                Grabbable.transform.position = Vector3.Lerp(_startPos, pos, lerp);
                Grabbable.transform.rotation = Quaternion.Lerp(_startRot, rot, lerp);

                if (lerp >= 1f)
                {
                    Grabbable.CanBeGrabbed = true;
                    _returning = false;
                    Grabbable.ResetToNonTrigger();
                    Socket.TryGrab(Grabbable, true, PlaySocketedSFX);
                }
            }
        }

        private void OnGrabbableReleased(HVRGrabberBase arg0, HVRGrabbable arg1)
        {
            if (!Socket)
                return;

            if (ReturnTime > 0f)
            {
                _returning = true;
                var pos = Socket.transform.TransformPoint(Socket.GetTargetPosition(Grabbable));
                _time = ReturnTime;
                _elapsed = 0f;
                Grabbable.SetAllToTrigger();
                _startPos = Grabbable.transform.position;
                _startRot = Grabbable.transform.rotation;
                Grabbable.CanBeGrabbed = false;
            }
            else
            {
                Socket.TryGrab(Grabbable, true, PlaySocketedSFX);
            }
        }
    }
}