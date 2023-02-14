using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    [RequireComponent(typeof(MeshRenderer))]
    public class HVRMeshRendererHighlight : HVRGrabbableHoverBase
    {
        public MeshRenderer Renderer { get; private set; }
        
        protected override void Start()
        {
            base.Start();
            Renderer = GetComponent<MeshRenderer>();

        }

        public override void Hover()
        {
            Renderer.enabled = true;
        }

        public override void Unhover()
        {
            Renderer.enabled = false;
        }

        public override void Enable()
        {
            Renderer.enabled = true;
        }

        public override void Disable()
        {
            Renderer.enabled = false;
        }
    }
}