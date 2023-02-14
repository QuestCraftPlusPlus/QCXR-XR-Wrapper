using System.Collections;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Sockets
{
    public class HVRSocketHoverScale : HVRSocketHoverAction
    {
        [Tooltip("Target scale when hovered.")]
        public Vector3 Scale = Vector3.one;

        [Tooltip("How long it takes to reach the target scale.")]
        public float ScaleTime = .25f;

        [Tooltip("If the hovered item is invalid, do we scale?")]
        public bool ScaleIfInvalid;

        private Vector3 _originalHoverTargetScale;
        private Coroutine _hoverRoutine;
        private Vector3 _target;

        protected override void Start()
        {
            base.Start();
            if (Target)
                _originalHoverTargetScale = Target.localScale;
            _target = _originalHoverTargetScale;
        }

        protected override void Update()
        {
            // UpdateScale();
        }

        //protected virtual void UpdateScale()
        //{
        //    if (Vector3.SqrMagnitude(_target - Target.transform.localScale) > .0001f)
        //    {
        //        Target.transform.localScale = Vector3.MoveTowards(Target.transform.localScale, _target, 1f / ScaleTime * Time.deltaTime);

        //        if (Vector3.Distance(_target, Target.transform.localScale) < .01f)
        //        {
        //            Target.transform.localScale = _target;
        //        }
        //    }
        //}

        public override void OnHoverEnter(HVRSocket socket, HVRGrabbable grabbable, bool isValid)
        {
            if (!isValid && !ScaleIfInvalid)
                return;

            _target = Scale;
            if (_hoverRoutine != null) StopCoroutine(_hoverRoutine);
            if (Target) _hoverRoutine = StartCoroutine(ScaleHoverTarget(Scale));
        }

        public override void OnHoverExit(HVRSocket socket, HVRGrabbable grabbable, bool isValid)
        {
            _target = _originalHoverTargetScale;
            if (_hoverRoutine != null) StopCoroutine(_hoverRoutine);
            if (Target) _hoverRoutine = StartCoroutine(ScaleHoverTarget(_originalHoverTargetScale));
        }

        private IEnumerator ScaleHoverTarget(Vector3 targetScale)
        {
            var start = Target.transform.localScale;
            var elapsed = 0f;

            while (elapsed < ScaleTime)
            {
                elapsed += Time.deltaTime;
                Target.transform.localScale = Vector3.Lerp(start, targetScale, elapsed / ScaleTime);
                yield return null;
            }

            Target.transform.localScale = targetScale;

            _hoverRoutine = null;
        }
    }
}