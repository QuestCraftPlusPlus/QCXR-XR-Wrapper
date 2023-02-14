using HurricaneVR.Framework.Components;
using TMPro;
using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    public class DemoKeypadButton : HVRPhysicsButton
    {
        public char Key;
        public TextMeshPro TextMeshPro;

        protected override void Awake()
        {
            ConnectedBody = transform.parent.GetComponentInParent<Rigidbody>();
            base.Awake();
        }
    }
}