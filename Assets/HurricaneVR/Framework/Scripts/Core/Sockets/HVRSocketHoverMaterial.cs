using System.Collections.Generic;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Sockets
{
    public class HVRSocketHoverMaterial : HVRSocketHoverAction
    {
        public Material InvalidMaterial;
        public Material ValidMaterial;

        private readonly Dictionary<Renderer, Material> _rendererMap = new Dictionary<Renderer, Material>();

        protected override void Start()
        {
            base.Start();
            foreach (var r in Target.GetComponentsInChildren<Renderer>())
            {
                _rendererMap[r] = r.material;
            }
        }


        public override void OnHoverEnter(HVRSocket socket, HVRGrabbable grabbable, bool isValid)
        {
            if (!InvalidMaterial && !isValid || !ValidMaterial && isValid) return;

            foreach (var r in _rendererMap.Keys)
            {
                r.material = isValid ? ValidMaterial : InvalidMaterial;
            }

        }

        public override void OnHoverExit(HVRSocket socket, HVRGrabbable grabbable, bool isValid)
        {
            if (!InvalidMaterial && !isValid || !ValidMaterial && isValid) return;

            foreach (var kvp in _rendererMap)
            {
                kvp.Key.material = kvp.Value;
            }
        }
    }
}