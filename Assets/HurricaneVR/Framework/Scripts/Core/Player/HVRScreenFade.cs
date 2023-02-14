using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Core.Player
{
    public abstract class HVRScreenFade : MonoBehaviour
    {
        [SerializeField]
        private float _fadeLevel;
        public float Speed = 5f;
        public float FadeInSpeed = 5f;
        public float FadeOutSpeed = 5f;

        public UnityEvent FadeStart = new UnityEvent();
        public UnityEvent FadeEnd = new UnityEvent();

        private Coroutine _coroutine;
        
        public abstract float CurrentFade { get; }

        public float FadeLevel
        {
            get
            {
                return _fadeLevel;
            }
            set
            {
                _fadeLevel = value;

                Fade(value, Speed);
            }
        }

        protected abstract void Disable();
        protected abstract void Enable();

        public abstract void UpdateFade(float alpha);

        public void Fade(float fade, float speed)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(FadeRoutine(fade, speed));
        }

        protected virtual IEnumerator FadeRoutine(float fadeLevel, float speed)
        {
            FadeStart.Invoke();

            fadeLevel = Mathf.Clamp(fadeLevel, 0f, 1f);
            var alpha = CurrentFade;
            var sign = Mathf.Sign(fadeLevel - CurrentFade);
            var delta = Mathf.Abs(fadeLevel - CurrentFade);
            var total = 0f;

            while (total < delta)
            {
                alpha += sign * Time.deltaTime * speed;
                total += Time.deltaTime * speed;
                var min = fadeLevel > CurrentFade ? 0f : fadeLevel;
                var max = fadeLevel > CurrentFade ? fadeLevel : 1f;
                alpha = Mathf.Clamp(alpha, min, max);
                UpdateFade(alpha);
                if (Mathf.Approximately(alpha, 0f))
                {
                    Disable();
                }
                else
                {
                    Enable();
                }
                yield return new WaitForEndOfFrame();
            }

            FadeEnd.Invoke();
        }
    }
}