using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Bags
{
    /// <summary>
    /// Used by the hand to detect sockets for removing objects from them
    /// </summary>
    public class HVRSocketBag : MonoBehaviour, IComparer<HVRSocket>
    {
        private readonly Dictionary<HVRSocket, HashSet<Collider>> _map = new Dictionary<HVRSocket, HashSet<Collider>>();


        public HVRHandGrabber Grabber;
        public float MaxDistanceAllowed;
        public HVRSortMode hvrSortMode = HVRSortMode.SquareMagnitude;
        public HVRSocket[] IgnoredSockets;

        [Header("Transforms")]
        [Tooltip("If assigned, the position of this transform will be used to calculate the distance.")]
        public Transform DistanceSource;

        protected virtual void Start()
        {
            if (!Grabber)
            {
                Grabber = GetComponentInParent<HVRHandGrabber>();
            }

            if (Math.Abs(MaxDistanceAllowed) < .001)
            {
                MaxDistanceAllowed = 1.5f;
            }

            if (IgnoredSockets != null && IgnoredSockets.Length > 0)
            {
                _ignoredSockets = new HashSet<HVRSocket>(IgnoredSockets);
            }
        }

        [Header("Debugging")]
        public HVRSocket ClosestSocket;
        public readonly HashSet<HVRSocket> AllSockets = new HashSet<HVRSocket>();
        private readonly List<HVRSocket> _allSockets = new List<HVRSocket>(20);
        public List<HVRSocket> ValidSockets = new List<HVRSocket>(1000);
        private readonly List<HVRSocket> SocketsToRemove = new List<HVRSocket>(1000);
        private Dictionary<HVRSocket, float> DistanceMap = new Dictionary<HVRSocket, float>();
        private HashSet<HVRSocket> _ignoredSockets;

        private void FixedUpdate()
        {
            Calculate();
        }

        /// <summary>
        /// Causes the bag to ignore the provided socket when it's trigger collider overlaps ours.
        /// </summary>
        public void IgnoreSocket(HVRSocket socket)
        {
            if (_ignoredSockets == null)
            {
                _ignoredSockets = new HashSet<HVRSocket>();
            }

            _ignoredSockets.Add(socket);
        }

        /// <summary>
        /// Stops ignoring the provided socket
        /// </summary>
        public void UnIgnoreSocket(HVRSocket socket)
        {
            _ignoredSockets.Remove(socket);
        }

        protected void AddSocket(HVRSocket socket)
        {
            if (AllSockets.Contains(socket))
            {
                return;
            }

            _allSockets.Add(socket);
            AllSockets.Add(socket);
        }


        protected void RemoveSocket(HVRSocket socket)
        {
            if (AllSockets.Contains(socket))
            {
                AllSockets.Remove(socket);
                _allSockets.Remove(socket);
            }

            if (_map.ContainsKey(socket))
            {
                _map.Remove(socket);
            }
        }

        protected void Calculate()
        {
            ValidSockets.Clear();
            SocketsToRemove.Clear();

            var anyDestroyedOrDisabled = false;

            for (var i = 0; i < _allSockets.Count; i++)
            {
                var socket = _allSockets[i];
                if (!socket || !socket.gameObject.activeInHierarchy || !socket.enabled)
                {
                    anyDestroyedOrDisabled = true;
                    continue;
                }

                var distance = DistanceToSocket(socket);

                DistanceMap[socket] = distance;

                if (distance > MaxDistanceAllowed)
                {
                    SocketsToRemove.Add(socket);
                }
                else if (IsValid(socket))
                {
                    ValidSockets.Add(socket);
                }
            }

            if (anyDestroyedOrDisabled)
            {
                AllSockets.RemoveWhere(socket => !socket || !socket.gameObject.activeInHierarchy || !socket.enabled);
                _allSockets.RemoveAll(socket => !socket || !socket.gameObject.activeInHierarchy || !socket.enabled);
            }

            for (var index = 0; index < SocketsToRemove.Count; index++)
            {
                var invalid = SocketsToRemove[index];
                RemoveSocket(invalid);
            }

            // x->y ascending sort
            //ValidSockets.Sort(this);
            SortHelper.Sort(ValidSockets, 0, ValidSockets.Count, this);

            ClosestSocket = ValidSockets.FirstOrDefault();
        }

        public virtual float DistanceToSocket(HVRSocket socket)
        {
            var point = DistanceSource ? DistanceSource.position : Grabber.transform.position;

            if (hvrSortMode == HVRSortMode.Distance)
                return socket.GetDistanceToGrabber(point);
            return socket.GetSquareDistanceToGrabber(point);
        }

        protected bool IsValid(HVRSocket Socket)
        {
            return true;
        }

        private void OnTriggerEnter(Collider other)
        {
            var socket = other.GetComponent<HVRSocket>();

            if (socket)
            {
                if (_ignoredSockets != null && _ignoredSockets.Contains(socket))
                {
                    return;
                }

                if (!_map.TryGetValue(socket, out var colliders))
                {
                    colliders = new HashSet<Collider>();
                    _map[socket] = colliders;
                }

                if (colliders.Count == 0)
                {
                    AddSocket(socket);
                }

                colliders.Add(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var socket = other.GetComponent<HVRSocket>();

            if (socket)
            {
                if (_map.TryGetValue(socket, out var colliders))
                {
                    colliders.Remove(other);
                }

                if (colliders == null || colliders.Count == 0)
                {
                    RemoveSocket(socket);
                }
            }
        }

        public int Compare(HVRSocket x, HVRSocket y)
        {
            return DistanceMap[x].CompareTo(DistanceMap[y]);
        }
    }
}