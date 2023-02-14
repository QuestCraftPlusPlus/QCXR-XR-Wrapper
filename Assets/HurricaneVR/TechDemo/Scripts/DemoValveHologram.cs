using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    public class DemoValveHologram : MonoBehaviour
    {
        public float Interval = .2f;
        public Quaternion StartRotation;
        public Quaternion EndRotation;

        private Quaternion _current;
        private Quaternion _next;
        private float _elapsed;

#if UNITY_EDITOR
        [InspectorButton("SaveStartRotationMethod")]
        public string SaveStartRotation;

        [InspectorButton("SaveEndRotationMethod")]
        public string SaveEndRotation;

        public void SaveStartRotationMethod()
        {
            StartRotation = transform.localRotation;
        }

        public void SaveEndRotationMethod()
        {
            EndRotation = transform.localRotation;
        }
#endif

        void Start()
        {
            _current = StartRotation;
            _next = EndRotation;
        }

        void Update()
        {
            _elapsed += Time.deltaTime;
            if (_elapsed > Interval)
            {
                var temp = _current;
                _current = _next;
                _next = temp;
                _elapsed = 0f;
            }

            transform.localRotation = Quaternion.Lerp(_current, _next, _elapsed / Interval);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
