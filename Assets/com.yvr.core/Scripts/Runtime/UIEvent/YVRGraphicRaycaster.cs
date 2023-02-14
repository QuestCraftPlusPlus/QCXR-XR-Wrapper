using System;
using System.Collections;
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
        private struct RaycastHit
        {
            public Graphic graphic;
            public Vector3 hitPointWorldPos;

            public RaycastHit(Graphic graphic, Vector3 hitPointWorldPos)
            {
                this.graphic = graphic;
                this.hitPointWorldPos = hitPointWorldPos;
            }
        }

        private List<RaycastHit> raycastHitResults = new List<RaycastHit>();
        private Canvas _canvas = null;
        private Canvas canvas { get { return _canvas ?? (_canvas = GetComponent<Canvas>()); } }

        /// <summary>
        /// The camera used to convert rayhit's world position to screen position
        /// </summary>
        public override Camera eventCamera { get { return canvas.worldCamera; } }

        /// <summary>
        /// Processing actual raycast, will be triggered by EventSystem.RaycastAll
        /// </summary>
        /// <param name="eventData"> Data comes from @YVR.Core.YVRInputModule </param>
        /// <param name="resultAppendList"> List contains the raycasting result </param>
        public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
        {
            Raycast(eventData as YVRPointerEventData, resultAppendList);
        }

        private void Raycast(YVRPointerEventData eventData, List<RaycastResult> resultAppendList)
        {
            if (canvas == null) return;
            if (eventData == null) return;
            if (eventCamera == null) return;

            float maxHitDistanceSqr = eventCamera.farClipPlane * eventCamera.farClipPlane;
            Ray ray = eventData.worldSpaceRay;

            raycastHitResults.Clear();
            GraphicRaycast(canvas, ray, raycastHitResults);

            raycastHitResults.ForEach(raycastHit =>
            {
                RaycastHit hit = raycastHit; // Closure problem
                GameObject graphicGO = hit.graphic.gameObject;

                // the graphic GO is not valid, if the ray intersects it from the back
                bool graphicGOValid = ignoreReversedGraphics ? Vector3.Dot(ray.direction, graphicGO.transform.forward) > 0 : true;

                // the graphic GO is not valid, if it is behind us
                if (eventCamera.transform.InverseTransformPoint(hit.hitPointWorldPos).z <= 0)
                    graphicGOValid = false;

                if (graphicGOValid)
                {
                    float hitDistanceSqr = (hit.hitPointWorldPos - ray.origin).sqrMagnitude;
                    if (hitDistanceSqr < maxHitDistanceSqr)
                    {
                        RaycastResult result = new RaycastResult()
                        {
                            // Event systems rely these data to sort the raycast results
                            gameObject = graphicGO,
                            module = this,
                            distance = Mathf.Sqrt(hitDistanceSqr),
                            index = resultAppendList.Count,
                            depth = hit.graphic.depth,
                            sortingLayer = canvas.sortingLayerID,
                            sortingOrder = canvas.sortingOrder,
                            worldPosition = hit.hitPointWorldPos
                        };
                        resultAppendList.Add(result);
                    }
                }
            });
        }

        private void GraphicRaycast(Canvas canvas, Ray ray, List<RaycastHit> results)
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

                if (graphic.rectTransform.RayIntersects(ray, out Vector3 hitPointWorldPos))
                {
                    Vector2 hitPointScreenPos = eventCamera.WorldToScreenPoint(hitPointWorldPos);
                    if (graphic.Raycast(hitPointScreenPos, eventCamera))
                    {
                        RaycastHit hitResult = new RaycastHit(graphic, hitPointWorldPos);
                        results.Add(hitResult);
                    }
                }
            }

            results.Sort((g1, g2) => g2.graphic.depth.CompareTo(g1.graphic.depth));
        }

        /// <summary>
        /// Convert hit point position from world space to the screen space
        /// </summary>
        /// <param name="raycastResult"> Target raycastResult </param>
        /// <returns> Raycast hit point in the screen space of @YVR.Core.YVRGraphicRaycaster.eventCamera </returns>
        public Vector2 GetRaycastResultScreenPosition(RaycastResult raycastResult)
        {
            return eventCamera.WorldToScreenPoint(raycastResult.worldPosition);
        }
    }
}