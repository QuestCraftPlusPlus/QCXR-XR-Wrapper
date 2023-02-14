using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(HingeJoint))]
    public class DemoLockedDoor : MonoBehaviour
    {
        private HingeJoint _hinge;
        private Rigidbody _rigidbody;

        public float MinAngle;
        public float MaxAngle;

        public bool LockOnStart = true;

        void Start()
        {
            _hinge = GetComponent<HingeJoint>();
            _rigidbody = GetComponent<Rigidbody>();

            if (LockOnStart)
                Lock();
        }

        private void Lock()
        {
            var limit = _hinge.limits;
            limit.min = 0f;
            limit.max = 0f;
            _hinge.limits = limit;

            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        public void Unlock()
        {
            var limits = _hinge.limits;
            limits.min = MinAngle;
            limits.max = MaxAngle;
            _hinge.limits = limits;

            _rigidbody.constraints = RigidbodyConstraints.None;
        }
    }
}
