using System.Collections;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Utils
{
    public static class Extensions
    {

        public static Bounds GetRendererBounds(this Transform transform, bool requireEnabled = true)
        {
            var bounds = new Bounds(transform.position, Vector3.zero);
            var renderers = transform.GetComponentsInChildren<MeshRenderer>();
            foreach (var r in renderers)
            {
                if (!requireEnabled || r.enabled)
                    bounds.Encapsulate(r.bounds);
            }

            return bounds;
        }

        public static void ResetLocalProps(this Transform transform, bool resetScale = true)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            if (resetScale)
                transform.localScale = Vector3.one;
        }

        /// <summary>
        /// Sets layer recursively until another component of type T is found then stops.
        /// </summary>
        public static void SetLayerRecursiveEx<T>(this Transform transform, HVRLayers layer, T component) where T : Component
        {
            var newLayer = LayerMask.NameToLayer(layer.ToString());
            if (newLayer < 0)
                return;

            SetLayerRecursiveEx<T>(transform, newLayer, component);
        }

        /// <summary>
        /// Sets layer recursively until another component of type T is found then stops.
        /// </summary>
        public static void SetLayerRecursiveEx<T>(this Transform transform, int newLayer, T king) where T : Component
        {
            if (!transform)
                return;

            transform.TryGetComponent(out T comp);
            if (comp && comp != king) return;

            transform.gameObject.layer = newLayer;
            for (int i = 0; i < transform.childCount; i++)
            {
                SetLayerRecursiveEx<T>(transform.GetChild(i), newLayer, king);
            }
        }

        public static void SetLayerRecursive(this Transform transform, HVRLayers layer, Transform except = null)
        {
            var newLayer = LayerMask.NameToLayer(layer.ToString());
            if (newLayer < 0)
                return;

            SetLayerRecursive(transform, newLayer, except);
        }

        public static void SetLayerRecursive(this Transform transform, int newLayer, Transform except = null)
        {
            if (!transform || transform == except)
                return;

            transform.gameObject.layer = newLayer;
            for (int i = 0; i < transform.childCount; i++)
            {
                SetLayerRecursive(transform.GetChild(i), newLayer, except);
            }
        }

        public static IEnumerator SetLayerTimeout(Transform transform, HVRLayers layer, float timeout)
        {
            yield return new WaitForSeconds(timeout);
            transform.SetLayerRecursive(layer);
        }
        
        /// <summary>
        /// Locates a component by checking the collider first, then the collider's rigidbody, then finally uses GetComponentInParent.
        /// </summary>
        public static T FindComponent<T>(this Collider collider, bool tryParent = true) where T : Component
        {
            if (collider.TryGetComponent(out T c) || collider.attachedRigidbody && collider.attachedRigidbody.TryGetComponent(out c))
            {
                return c;
            }

            return tryParent ? collider.GetComponentInParent<T>() : default(T);
        }
    }
}

