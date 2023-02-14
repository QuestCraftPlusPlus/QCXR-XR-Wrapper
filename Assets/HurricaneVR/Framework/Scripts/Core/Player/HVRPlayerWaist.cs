using System;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Player
{
    public class HVRPlayerWaist : MonoBehaviour
    {
        [Header("Required Transforms")]
        public Transform PlayerController;
        public Transform Camera;

        [Header("Settings")]
        [Tooltip("The waist will be position this much lower than the camera.")]
        public float CameraOffset = .6f;
        [Tooltip("If your eyes are greater than this angle then the waist will not immediately snap with your camera rotation")]
        public float CameraAngleThreshold = 30f;
        [Tooltip("If the delta between the camera forward and waist forward is greater than this, the waist will rotate at WaistSpeed")]
        public float WaistAngleThreshold = 70;
        [Tooltip("Speed of the waist catchup when too far from the camera gaze")]
        public float WaistSpeed = 90f;

        public void Update()
        {
            FollowPlayer();
        }

        public void FollowPlayer()
        {
            var waistPosition = PlayerController.position;
            waistPosition.y = Camera.position.y - CameraOffset;
            transform.position = waistPosition;

            var from = Camera.forward;
            @from.y = 0f;
            var angle = Vector3.SignedAngle(@from, Camera.forward, Camera.right);

            if (angle < CameraAngleThreshold)
            {
                transform.rotation = Quaternion.Euler(0.0f, Camera.rotation.eulerAngles.y, 0.0f);
            }
            else
            {
                angle = Vector3.Angle(Camera.forward, transform.forward);
                if (angle > WaistAngleThreshold)
                {
                    var waistRotation = Quaternion.RotateTowards(transform.rotation, Camera.rotation, WaistSpeed * Time.deltaTime).eulerAngles;
                    waistRotation.x = 0f;
                    waistRotation.z = 0f;
                    transform.eulerAngles = waistRotation;
                }
            }
        }
    }
}