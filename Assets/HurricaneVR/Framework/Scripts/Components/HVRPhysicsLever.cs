using System;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    /// <summary>
    /// Lever behaves similar to the dial in rigidbody joint behaviour at this time...
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class HVRPhysicsLever : HVRPhysicsDial
    {
        public bool DrawGizmos = true;


        public void OnDrawGizmosSelected()
        {
            //if (DrawGizmos)
            //{
            //    var minVector = Quaternion.Euler(MinAngle * _axis) * _orthogonal;
            //    var maxVector = Quaternion.Euler(MaxAngle * _axis) * _orthogonal;

            //    Gizmos.color = Color.green;
            //    Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(minVector) * .3f);
            //    Gizmos.color = Color.yellow;
            //    Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(maxVector) * .3f);
            //}
        }
    }
}