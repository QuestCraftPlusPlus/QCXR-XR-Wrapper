using UnityEngine;

namespace HurricaneVR.Framework.Core.Stabbing
{
    [CreateAssetMenu(menuName = "HurricaneVR/Stabber Settings", fileName = "StabberSettings")]
    public class HVRStabberSettings : ScriptableObject
    {
        [Header("Stab Settings")]

        [Range(0, 1f)]
        [Tooltip("Dot product of the stab collision direction must be greater than this.")]
        public float AngleThreshold = .5f;
        
        [Range(0f, 1f)]
        [Tooltip("Joint damper modifier, 1 will have no friction, 0 has max friction")]
        public float Sharpness = .3f;

        [Tooltip("Distance between the entry point and the stabbed point before the object will be unstabbed")]
        public float UnstabThreshold = .01f;

        [Tooltip("Amount of time to wait after a stab before unstabbing is allowed")]
        public float UnstabDelay = .25f;

        [Tooltip("How many things can this thing stab?")]
        public int AllowedStabs = 5;

        [Tooltip("If the stabbale is pulled off the line by this distance unstab it")]
        public float PerpendicularThreshold = .2f;
    }
}