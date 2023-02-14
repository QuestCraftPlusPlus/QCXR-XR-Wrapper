using HurricaneVR.Framework.Core.Utils;
using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Used in conjunction with the HVRRotationTracker to play SFX based on rotation amount.
    /// </summary>
    public class HVRRotationSFX : MonoBehaviour
    {
        [Tooltip("Tracker required to know how far something has rotated")]
        public HVRRotationTracker Tracker;

        [Tooltip("Clip chosen at random from this list when rotated beyond 'AngleThreshold'")]
        public AudioClip[] SFX;

        [Tooltip("Rotation distance must exceed this to play another sfx clip")]
        public float AngleThreshold = 30f;

        [Header("Debug")]
        public float AngleAccumulated;

        protected virtual void Awake()
        {
            if (Tracker)
            {
                Tracker = GetComponent<HVRRotationTracker>();
            }

            if (Tracker)
            {
                Tracker.AngleChanged.AddListener(OnAngleChanged);
            }
        }

        private void OnAngleChanged(float angle, float delta)
        {
            if (SFX != null && SFX.Length > 0)
            {
                AngleAccumulated += Mathf.Abs(delta);
                if (AngleAccumulated > AngleThreshold)
                {
                    var index = Random.Range(0, SFX.Length);
                    var sfx = SFX[index];
                    AngleAccumulated = 0;
                    PlaySFX(sfx);
                }
            }
        }

        protected virtual void PlaySFX(AudioClip sfx)
        {
            if(SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(sfx, transform.position);
        }
    }
}