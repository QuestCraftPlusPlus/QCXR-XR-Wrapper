using System.Collections.Generic;
using UnityEngine;

namespace HurricaneVR.Framework.Components
{
    public class HVRMeshGlowHighlight : HVRGrabbableHoverBase
    {
        private readonly HashSet<GameObject> _highlightObjects = new HashSet<GameObject>();
        private readonly HashSet<MeshRenderer> _renderers = new HashSet<MeshRenderer>();

        void Awake()
        {
            var meshes = GetComponentsInChildren<MeshFilter>();

            foreach (var mesh in meshes)
            {
                var go = mesh.gameObject;
                var meshRenderer = go.GetComponent<MeshRenderer>();
                if (meshRenderer)
                {
                    _renderers.Add(meshRenderer);
                }
                var highlightObj = new GameObject("HighlightMesh");
                highlightObj.transform.SetParent(go.transform, false);

                var clone = highlightObj.AddComponent<MeshFilter>();
                clone.sharedMesh = mesh.sharedMesh;
                var newRenderer = highlightObj.AddComponent<MeshRenderer>();
                newRenderer.sharedMaterial = Resources.Load<Material>("GrabbableHighlight");

                _highlightObjects.Add(highlightObj);
                highlightObj.SetActive(false);
            }
        }

        public override void Hover()
        {
            foreach (var highlight in _highlightObjects)
            {
                highlight.SetActive(true);
            }

            foreach (var r in _renderers)
            {
                r.enabled = false;
            }
        }

        public override void Unhover()
        {
            foreach (var highlight in _highlightObjects)
            {
                highlight.SetActive(false);
            }

            foreach (var r in _renderers)
            {
                r.enabled = true;
            }
        }

        public override void Enable()
        {

        }

        public override void Disable()
        {

        }
    }
}
