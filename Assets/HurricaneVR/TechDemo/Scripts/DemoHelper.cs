using System.Collections.Generic;
using System.Linq;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    public class DemoHelper : MonoBehaviour
    {
        class ResetState
        {
            public HVRGrabbable Grabbable;
            public HVRGrabbable Clone;
            public Vector3 Position;
            public Quaternion Rotation;
            public Vector3 Scale { get; set; }
            public Transform Parent;
        }

        public List<Transform> Parents = new List<Transform>();
        public List<HVRGrabbable> Grabbables = new List<HVRGrabbable>();

        [Header("Debug")] public bool ForceReset;

        private readonly List<ResetState> _grabbableState = new List<ResetState>();


        void Start()
        {
            foreach (var parent in Parents)
            {
                if (parent)
                    AddResetGrabbable(parent);
            }

            foreach (var grabbable in Grabbables)
            {
                SaveResetGrabbable(grabbable.transform.parent, grabbable);
            }
        }

        private void AddResetGrabbable(Transform parent)
        {
            foreach (var grabbable in parent.GetComponentsInChildren<HVRGrabbable>().Where(e => e.transform.parent == parent))
            {
                SaveResetGrabbable(parent, grabbable);
            }
        }

        private void SaveResetGrabbable(Transform parent, HVRGrabbable grabbable)
        {
            var clone = Instantiate(grabbable);
            clone.gameObject.SetActive(false);
            clone.gameObject.hideFlags = HideFlags.HideInHierarchy;
            var state = new ResetState()
            {
                Grabbable = grabbable,
                Clone = clone,
                Position = grabbable.transform.position,
                Rotation = grabbable.transform.rotation,
                Scale = grabbable.transform.localScale,
                Parent = parent
            };

            _grabbableState.Add(state);
        }

        void Update()
        {
            if (ForceReset)
            {
                ResetGrabbables();
                ForceReset = false;
            }
        }

        private List<HVRGrabbable> _balls = new List<HVRGrabbable>();

        public void BallSpawned(HVRSocket socket, GameObject ball)
        {
            var grabbable = ball.GetComponent<HVRGrabbable>();
            _balls.Add(grabbable);
        }

        public void ResetGrabbables()
        {
            foreach (var state in _grabbableState)
            {
                if (!state.Grabbable)
                {
                    state.Grabbable = state.Clone;
                    state.Grabbable.gameObject.SetActive(true);
                    state.Clone = Instantiate(state.Clone);
                    state.Clone.gameObject.SetActive(false);
                    state.Clone.gameObject.hideFlags = HideFlags.HideInHierarchy;
                    state.Grabbable.transform.parent = state.Parent;
                }

                if (!state.Grabbable.IsBeingHeld)
                {
                    state.Grabbable.transform.parent = state.Parent;
                    state.Grabbable.transform.position = state.Position;
                    state.Grabbable.transform.rotation = state.Rotation;
                    state.Grabbable.transform.localScale = state.Scale;
                    state.Grabbable.Rigidbody.velocity = Vector3.zero;
                    state.Grabbable.Rigidbody.angularVelocity = Vector3.zero;
                }

            }

            var remove = new List<HVRGrabbable>();
            foreach (var ball in _balls)
            {
                if (ball.IsBeingHeld)
                    continue;
                Destroy(ball.gameObject);
                remove.Add(ball);
            }

            remove.ForEach(grabbable => _balls.Remove(grabbable));
        }
    }
}
