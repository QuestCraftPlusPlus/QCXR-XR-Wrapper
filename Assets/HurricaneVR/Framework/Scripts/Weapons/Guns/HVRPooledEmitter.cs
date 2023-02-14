using System;
using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HurricaneVR.Framework.Weapons.Guns
{
    public class HVRPooledEmitter : HVRGunEmitterBase
    {
        public int MinLife = 5;
        public int MaxLife = 10;
        public int MaxObjects = 30;

        public HideFlags HideFlags = HideFlags.HideInHierarchy;

        private readonly List<HVRPooledObjectTracker> _objects = new List<HVRPooledObjectTracker>();

        protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < MaxObjects; i++)
            {
                if (Prefab)
                {
                    _objects.Add(new HVRPooledObjectTracker());
                    _objects[i].Object = Instantiate(Prefab);
                    _objects[i].Object.SetActive(false);
                    _objects[i].Rigidbody = _objects[i].Object.GetRigidbody();
                    _objects[i].Object.hideFlags = HideFlags;
                    _objects[i].Colliders = _objects[i].Object.GetComponentsInChildren<Collider>();
                }
            }
        }

        public virtual void Update()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                if (_objects[i].Object.activeSelf)
                {
                    _objects[i].Elapsed += Time.deltaTime;
                    if (_objects[i].Elapsed > _objects[i].TimeToLive)
                    {
                        _objects[i].Object.SetActive(false);
                    }
                }
            }
        }

        public override void Emit()
        {
            HVRPooledObjectTracker tracker = null;

            var time = -1f;

            for (int i = 0; i < _objects.Count; i++)
            {
                if (!_objects[i].Object.activeSelf)
                {
                    tracker = _objects[i];
                    break;
                }

                if (_objects[i].Elapsed > time)
                {
                    tracker = _objects[i];
                    time = _objects[i].Elapsed;
                }
            }

            if (tracker != null)
            {
                tracker.Elapsed = 0f;
                tracker.TimeToLive = Random.Range(MinLife, MaxLife);
                tracker.Object.SetActive(true);
                var rb = tracker.Rigidbody;
                if (rb)
                {
                    Launch(rb);
                    if (Gun)
                    {
                        Gun.IgnoreCollision(tracker.Colliders, 1f);
                    }
                }
            }
        }

        private class HVRPooledObjectTracker
        {
            public GameObject Object;
            public Rigidbody Rigidbody;
            public float Elapsed;
            public float TimeToLive;
            public Collider[] Colliders;
        }
    }
}