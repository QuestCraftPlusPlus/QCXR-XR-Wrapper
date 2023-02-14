using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Specialized hover behaviour that scales this transform over time when a hand or force grabber is hovering it.
    /// </summary>
    public class HVRScaleHighlight : HVRGrabbableHoverBase
    {
        public Vector3 HoverScale = new Vector3(1.3f, 1.3f, 1.3f);
        public Vector3 UnhoverScale = new Vector3(.1f, .1f, .1f);
        public float ScaleSpeed = .05f;

        private Vector3 _targetScale;
        private Vector3 _originalScale;

        private void Awake()
        {
            _originalScale = transform.localScale;
            _targetScale = _originalScale;
        }

        protected override void Start()
        {
            base.Start();

        }

        protected override void Update()
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, ScaleSpeed);
        }

        public override void Hover()
        {
            _targetScale = HoverScale;
        }

        public override void Unhover()
        {
            _targetScale = UnhoverScale;
        }

        public override void Enable()
        {
            _targetScale = UnhoverScale;
        }

        public override void Disable()
        {
            _targetScale = Vector3.zero;
        }
    }
}