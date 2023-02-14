using UnityEngine;

namespace HurricaneVR.Framework.Core
{
    [RequireComponent(typeof(LineRenderer))]
    public class HVRForceGrabberLaser : MonoBehaviour
    {
        public LineRenderer LineRenderer;
        public int Points = 50;
        public Transform Target;
        public float Percent = .6f;

        private Vector3[] _points;
        private int _previousPoints;

        void Start()
        {
            if (!LineRenderer) LineRenderer = GetComponent<LineRenderer>();

            _points = new Vector3[Points];
            _previousPoints = Points;
            //LineRenderer.enabled = false;
        }

        void Update()
        {
            CheckResize();
            UpdateLaser();
        }

        private void UpdateLaser()
        {
            if (!Target) return;

            A = transform.position;
            D = Target.position;

            var distance = Vector3.Distance(A, D) * Percent;

            B = transform.position + transform.forward * distance;
            C = B;

            Gizmos.color = Color.white;
            var resolution = 1f / Points;

            for (var i = 1; i <= Mathf.FloorToInt(1f / resolution); i++)
            {
                var t = i * resolution;
                var point = GetPointOnBezierCurve(A, B, C, D, t);
                _points[i - 1] = point;
            }

            LineRenderer.positionCount = _points.Length;
            LineRenderer.SetPositions(_points);
        }

        private void CheckResize()
        {
            if (_previousPoints != Points)
            {
                _points = new Vector3[Points];
            }

            _previousPoints = Points;
        }

        public void Enable(Transform target)
        {
            Target = target;
            LineRenderer.enabled = true;
        }

        public void Disable()
        {
            Target = null;
            LineRenderer.enabled = false;
        }

        //Easier to use ABCD for the positions of the points so they are the same as in the tutorial image
        Vector3 A, B, C, D;

        //Display without having to press play
        void OnDrawGizmos()
        {
            if (!Target) return;
            A = transform.position;
            D = Target.position;

            var distance = Vector3.Distance(A, D) * Percent;

            B = transform.position + transform.forward * distance;
            C = B;

            Gizmos.color = Color.white;

            Vector3 lastPos = A;

            for (var i = 1; i <= Mathf.FloorToInt(1f / .02f); i++)
            {
                float t = i * .02f;
                Vector3 newPos = GetPointOnBezierCurve(A, B, C, D, t);
                Gizmos.DrawLine(lastPos, newPos);
                lastPos = newPos;
            }

            Gizmos.color = Color.green;

            Gizmos.DrawLine(A, B);
            Gizmos.DrawLine(C, D);
        }

        Vector3 GetPointOnBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float u = 1f - t;
            float t2 = t * t;
            float u2 = u * u;
            float u3 = u2 * u;
            float t3 = t2 * t;

            Vector3 result =
                (u3) * p0 +
                (3f * u2 * t) * p1 +
                (3f * u * t2) * p2 +
                (t3) * p3;

            return result;
        }
    }
}
