using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YVR.Core
{
    /// <summary>
    /// Raycaster used for raycasting graphic targets
    /// </summary>
    /// <seealso cref="YVR.Core.YVRInputModule" />
    /// <seealso cref="YVR.Core.YVRPhysicsRaycaster" />
    [RequireComponent(typeof(Canvas))]
    public class YVRGraphicRaycaster : GraphicRaycaster
    {
        protected readonly struct RaycastHit
        {
            public readonly Graphic graphic;
            public readonly Vector3 hitPointWorldPos;

            public RaycastHit(Graphic graphic, Vector3 hitPointWorldPos)
            {
                this.graphic = graphic;
                this.hitPointWorldPos = hitPointWorldPos;
            }
        }

        protected List<RaycastHit> m_RaycastHitResults = new List<RaycastHit>();
        private Canvas m_UICanvas = null;
        protected Canvas canvas => m_UICanvas ? m_UICanvas : m_UICanvas = GetComponent<Canvas>();

        /// <summary>
        /// The camera used to convert rayhit's world position to screen position
        /// </summary>
        public override Camera eventCamera => canvas.worldCamera;

        /// <summary>
        /// Processing actual raycast, will be triggered by EventSystem.RaycastAll
        /// </summary>
        /// <param name="eventData"> Data comes from @YVR.Core.YVRInputModule </param>
        /// <param name="resultAppendList"> List contains the ray casting result </param>
        public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
        {
            Raycast(eventData as YVRPointerEventData, resultAppendList);
        }

        protected virtual void Raycast(YVRPointerEventData eventData, ICollection<RaycastResult> resultAppendList)
        {
            if (canvas == null) return;
            if (eventData == null) return;
            if (eventCamera == null) return;

            float farClipPlane = eventCamera.farClipPlane;
            float maxHitDistanceSqr = farClipPlane * farClipPlane;
            Ray ray = eventData.worldSpaceRay;

            m_RaycastHitResults.Clear();
            GraphicRaycast(canvas, ray, m_RaycastHitResults);

            foreach (RaycastHit hitResult in m_RaycastHitResults)
            {
                // the graphic GO is not valid, if the ray intersects it from the back
                bool graphicGOValid = !ignoreReversedGraphics ||
                                      Vector3.Dot(ray.direction, hitResult.graphic.rectTransform.forward) > 0;

                // the graphic GO is not valid, if it is behind us
                if (eventCamera.transform.InverseTransformPoint(hitResult.hitPointWorldPos).z <= 0)
                    graphicGOValid = false;

                if (!graphicGOValid) continue;
                float hitDistanceSqr = (hitResult.hitPointWorldPos - ray.origin).sqrMagnitude;
                if (!(hitDistanceSqr < maxHitDistanceSqr)) continue;
                var result = new RaycastResult()
                {
                    // Event systems rely these data to sort the raycast results
                    gameObject = hitResult.graphic.gameObject,
                    module = this,
                    distance = Mathf.Sqrt(hitDistanceSqr),
                    index = resultAppendList.Count,
                    depth = hitResult.graphic.depth,
                    sortingLayer = canvas.sortingLayerID,
                    sortingOrder = canvas.sortingOrder,
                    worldPosition = hitResult.hitPointWorldPos
                };
                resultAppendList.Add(result);
            }
        }

        protected void GraphicRaycast(Canvas canvas, Ray ray, List<RaycastHit> results)
        {
            IList<Graphic> allGraphics = GraphicRegistry.GetGraphicsForCanvas(canvas);
            if (allGraphics == null || allGraphics.Count == 0)
                return;

            for (int i = 0; i != allGraphics.Count; ++i)
            {
                Graphic graphic = allGraphics[i];

                // graphic with -1 depth means it will not be handled by canvas
                if (graphic.depth == -1 || !graphic.raycastTarget || graphic.canvasRenderer.cull)
                    continue;

                if (!graphic.rectTransform.RayIntersects(ray, out Vector3 hitPointWorldPos)) continue;
                Vector2 hitPointScreenPos = eventCamera.WorldToScreenPoint(hitPointWorldPos);
                if (!graphic.Raycast(hitPointScreenPos, eventCamera)) continue;
                var hitResult = new RaycastHit(graphic, hitPointWorldPos);
                results.Add(hitResult);
            }

            results.Sort((g1, g2) => g2.graphic.depth.CompareTo(g1.graphic.depth));
        }

        /// <summary>
        /// Convert hit point position from world space to the screen space
        /// </summary>
        /// <param name="raycastResult"> Target raycastResult </param>
        /// <returns> Raycast hit point in the screen space of @YVR.Core.YVRGraphicRaycaster.eventCamera </returns>
        public virtual Vector2 GetRaycastResultScreenPosition(RaycastResult raycastResult)
        {
            return eventCamera.WorldToScreenPoint(raycastResult.worldPosition);
        }
    }
}