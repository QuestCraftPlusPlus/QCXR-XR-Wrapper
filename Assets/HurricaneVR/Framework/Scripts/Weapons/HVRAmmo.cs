using System;
using HurricaneVR.Framework.Components;
using UnityEngine;

namespace HurricaneVR.Framework.Weapons
{
    public class HVRAmmo : HVRDamageProvider
    {
        [Tooltip("Magazine Starting Count")]
        public int StartingCount = 15;
        [Tooltip("Max bullets allowed in the magazine")]
        public int MaxCount;

        [Tooltip("Bullet Range")] 
        public float MaxRange = 40f;

        [Header("Magazine Cleanup")]
        [Tooltip("Should the empty magazine be destroyed")]
        public bool DestroyIfEmpty = true;
        [Tooltip("How long to wait after ejecting the magazine to destroy it")]
        public float EmptyDestroyTimer = 3f;

        [Header("Debug")]
        public int CurrentCount;

        public bool HasAmmo => CurrentCount > 0;
        public bool IsEmpty => CurrentCount <= 0;

        protected virtual void Awake()
        {
            CurrentCount = StartingCount;
        }

        public virtual void AddBullet()
        {
            CurrentCount++;
        }

        public virtual bool CanAddBullet()
        {
            return CurrentCount < MaxCount;
        }

        public virtual bool TryAddBullet()
        {
            if (CanAddBullet())
            {
                AddBullet();
                return true;
            }
            return false;
        }

        public virtual void RemoveBullet()
        {
            CurrentCount--;
            if (CurrentCount < 0)
                CurrentCount = 0;
        }

        public virtual void StartDestroy()
        {
            Destroy(gameObject, EmptyDestroyTimer);
        }
    }
}