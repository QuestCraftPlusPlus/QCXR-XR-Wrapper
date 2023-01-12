using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace YVR.Core
{
    /// <summary>
    /// Raycaster used for raycasting physics targets
    /// </summary>
    /// <seealso cref="YVR.Core.YVRInputModule" />
    public class YVRPhysicsRaycaster : BaseRaycaster
    {
        [SerializeField]
        private Camera _eventCamera = null;

        /// <summary>
        /// The camera used to convert rayhit's world position to screen position
        /// </summary>
        public override Camera eventCamera { get { return _eventCamera ?? (_eventCamera = GetComponent<YVRCameraRenderer>()?.leftEyeCamera); } }

        /// <summary>
        /// The layer that physics ray should care
        /// </summary>
        public LayerMask eventMask = -1;

        /// <summary>
        /// Processing actual raycast, will be triggered by EventSystem.RaycastAll
        /// </summary>
        /// <param name="eventData"> Data comes from @YVR.Core.YVRInputModule </param>
        /// <param name="resultAppendList">List contains the raycasting result </param>
        public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
        {
            Raycast(eventData as YVRPointerEventData, resultAppendList);
        }

        private void Raycast(YVRPointerEventData eventData, List<RaycastResult> resultAppendList)
        {
            if (eventCamera == null) return;
            if (eventData == null) return;

            Ray ray = eventData.worldSpaceRay;
            float maxRayDistance = eventCamera.farClipPlane - eventCamera.nearClipPlane;

            RaycastHit[] hitsResult = Physics.RaycastAll(ray, maxRayDistance, eventMask);

            if (hitsResult.Length > 1)
                System.Array.Sort(hitsResult, (r1, r2) => r1.distance.CompareTo(r2.distance));

            if (hitsResult.Length != 0)
            {
                for (int i = 0; i != hitsResult.Length; ++i)
                {
                    RaycastResult raycastResult = new RaycastResult
                    {
                        gameObject = hitsResult[i].collider.gameObject,
                        module = this,
                        distance = hitsResult[i].distance,
                        index = resultAppendList.Count,
                        worldPosition = hitsResult[i].point,
                        worldNormal = hitsResult[i].normal
                    };

                    resultAppendList.Add(raycastResult);
                }
            }
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