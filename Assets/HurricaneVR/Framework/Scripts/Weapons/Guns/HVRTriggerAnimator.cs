using UnityEngine;

namespace HurricaneVR.Framework.Weapons.Guns
{
    public class HVRTriggerAnimator : MonoBehaviour
    {
        public bool Rotate = true;
        public Quaternion StartRotation;
        public Quaternion EndRotation;

        public bool Move = false;
        public Vector3 ForwardPosition;
        public Vector3 BackwardPosition;

        protected virtual void Awake()
        {
            if (StartRotation.w == 0f)
            {
                StartRotation = transform.localRotation;
            }

            if (EndRotation.w == 0f)
            {
                EndRotation = transform.localRotation;
            }
        }

        public virtual void Animate(float trigger)
        {
            if (Rotate)
            {
                transform.localRotation = Quaternion.Lerp(StartRotation, EndRotation, trigger);
            }

            if (Move)
            {
                transform.localPosition = Vector3.Lerp(ForwardPosition, BackwardPosition, trigger);
            }
        }

    }
}