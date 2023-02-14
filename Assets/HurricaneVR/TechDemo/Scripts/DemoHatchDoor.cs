using HurricaneVR.Framework.Core.Utils;
using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    public class DemoHatchDoor : MonoBehaviour
    {
        public float Timer = 5f;
        public float Degrees = 180;

        public bool Unlocked;
        public bool DoneRotating;

        public AudioClip SFXOpen;

        private float _elapsed;

        void Update()
        {
            if (!Unlocked || DoneRotating)
                return;

            _elapsed += UnityEngine.Time.deltaTime;

            transform.localRotation = transform.localRotation * Quaternion.Euler(new Vector3(-Degrees / Timer * Time.deltaTime, 0f, 0f));
            if (_elapsed > Timer)
            {
                _elapsed = 0f;
                DoneRotating = true;
            }
        }

        public void Unlock()
        {
            if (!Unlocked)
            {
                if(SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(SFXOpen, transform.position);
            }
            Unlocked = true;
        }
    }
}
