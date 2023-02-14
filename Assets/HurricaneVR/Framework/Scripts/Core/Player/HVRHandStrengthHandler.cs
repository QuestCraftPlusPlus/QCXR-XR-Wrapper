using System;
using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Core.Utils;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Player
{
    public class HVRHandStrengthHandler : MonoBehaviour
    {
        [Header("Debug")]
        
        [Tooltip("If true will update the joint every update - useful for tweaking HVRJointSettings in play mode.")]
        public bool AlwaysUpdateJoint;
        
        [Tooltip("Active strength profile")]
        public PDStrength CurrentStrength;
        
        [Tooltip("Deprecated active strength profile")]
        public HVRJointSettings CurrentSettings;

        [Tooltip("Enable to print to console when the strength profile changes")]
        public bool LogStrengthChanges;

        public HVRJointSettings JointSettings { get; private set; }

        public HVRJointSettings JointOverride { get; private set; }

        public HVRJointSettings HandGrabberOverride { get; private set; }

        public PDStrength DefaultStrength { get; private set; }

        public PDStrength StrengthOverride { get; private set; }

        public PDStrength HandGrabOverride { get; private set; }

        public ConfigurableJoint Joint { get; set; }

        public bool Stopped { get; private set; }

        private JointDrive _stoppedDrive;

        protected virtual void Awake()
        {
            _stoppedDrive = new JointDrive();
            _stoppedDrive.maximumForce = 0f;
            _stoppedDrive.positionSpring = 0f;
            _stoppedDrive.positionDamper = 0f;

            if (AlwaysUpdateJoint)
            {
                Debug.LogWarning($"AlwaysUpdateJoint is enabled on {name}, was this intentional?");
            }
        }

        public void Initialize(HVRJointSettings defaultSettings)
        {
            JointSettings = defaultSettings;
            UpdateJoint();
        }
        
        public void Initialize(PDStrength defaultSettings)
        {
            DefaultStrength = defaultSettings;
            UpdateJoint();
        }

        protected virtual void FixedUpdate()
        {
            if (AlwaysUpdateJoint)
            {
                UpdateJoint();
            }
        }

        protected virtual void UpdateJoint()
        {
            if (Stopped)
                return;

            if (StrengthOverride)
            {
                UpdateStrength(StrengthOverride);
            }
            else if (HandGrabOverride)
            {
                UpdateStrength(HandGrabOverride);
            }
            else if (HandGrabberOverride)
            {
                UpdateStrength(HandGrabberOverride);
            }
            else if (JointOverride)
            {
                UpdateStrength(JointOverride);
            }
            else if (JointSettings)
            {
                UpdateStrength(JointSettings);
            }
            else if (DefaultStrength)
            {
                UpdateStrength(DefaultStrength);
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected virtual void UpdateStrength(PDStrength settings)
        {
            if (!settings) return;
            
            CurrentStrength = settings;
            if(settings.Mode != JointApply.Rotation)
                Joint.SetLinearDrive(settings.Spring, settings.Damper, settings.MaxForce);
            if(settings.Mode != JointApply.Position)
                Joint.SetSlerpDrive(settings.TorqueSpring, settings.TorqueDamper, settings.MaxTorque);
            
            if (LogStrengthChanges)
            {
                Debug.Log($"{settings.name} applied.");
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected virtual void UpdateStrength(HVRJointSettings settings)
        {
            if (settings)
                settings.ApplySettings(Joint);

            CurrentSettings = settings;

            if (LogStrengthChanges && settings)
            {
                Debug.Log($"{settings.name} applied.");
            }
        }


        public virtual void OverrideSettings(HVRJointSettings settings)
        {
            JointOverride = settings;
            UpdateJoint();
        }

        internal virtual void OverrideHandSettings(HVRJointSettings settings)
        {
            HandGrabberOverride = settings;
            UpdateJoint();
        }

        public virtual void StopOverride()
        {
            JointOverride = null;
            StrengthOverride = null;
            UpdateJoint();
        }

        public virtual void ResetHandOverride()
        {
            var needsReset = HandGrabberOverride || HandGrabOverride;
            HandGrabberOverride = null;
            HandGrabOverride = null;
            if(needsReset)
                UpdateJoint();
        }
        
        /// <summary>
        /// Override the strength profile. Takes precedence over grabbable strength overrides.
        /// Call StopOverride to revert to default strength. 
        /// </summary>
        public virtual void OverrideSettings(PDStrength settings)
        {
            StrengthOverride = settings;
            UpdateJoint();
        }

        public virtual void OverrideHandSettings(PDStrength settings)
        {
            HandGrabOverride = settings;
            UpdateJoint();
        }

        public virtual void Stop()
        {
            Stopped = true;
            Joint.xDrive = Joint.yDrive = Joint.zDrive = Joint.angularXDrive = Joint.angularYZDrive = Joint.slerpDrive = _stoppedDrive;
        }

        public virtual void Restart()
        {
            Stopped = false;
            UpdateJoint();
        }

        
    }

    [Serializable]
    public class HandStrength
    {
        public bool UseOneHandStrength;
        public bool UseTwoHandStrength;
        public PDStrength OneHandStrength;
        public PDStrength TwoHandStrength;
    }

   
}