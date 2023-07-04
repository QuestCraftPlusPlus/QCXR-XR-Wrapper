using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Core
{
    /// <summary>
    /// Class for drawing raycasting laser beam
    /// </summary>
    /// <remarks> It's highly recommended to implement your own version based on this script </remarks>
    public class YVRRaycastLaserBeam : MonoBehaviour
    {
        /// <summary>
        /// The anchor of laser beam which is the same with YVRInputModule.rayTransform
        /// </summary>
        private Transform laserBeamAnchor = null;

        /// <summary>
        /// The display length of the laser beam
        /// </summary>
        public float laserLength = 1.0f;

        private YVRInputModule _inputModule = null;

        private YVRInputModule inputModule
        {
            get { return _inputModule ?? (_inputModule = FindObjectOfType<YVRInputModule>()); }
        }

        /// <summary>
        /// The line render used for rendering the laser beam
        /// </summary>
        protected LineRenderer lineRenderer = null;

        /// <summary>
        /// The Gameobject instance which indicating the cursor
        /// </summary>
        protected GameObject cursorGameObject = null;

        /// <summary>
        /// The transform of [cursorGameObject](xref:YVR.Core.YVRRaycastLaserBeam.cursorGameObject)
        /// </summary>
        protected Transform cursorTransform = null;

        /// <summary>
        /// The Material of [cursorGameObject](xref:YVR.Core.YVRRaycastLaserBeam.cursorGameObject)
        /// </summary>
        protected Material cursorMaterial = null;

        /// <summary>
        /// Virtual version of Unity life cycle function `Start`
        /// </summary>
        protected virtual void Start()
        {
            inputModule.onInputDataProviderChanged += (inputDataProvider) =>
            {
                // To ensure there is only one UpdateLeaserBeam register
                YVRManager.instance.eventsManager.onUpdate -= UpdateLaserBeam;

                bool isHMDInput = inputDataProvider is YVRInputModule.YVRInputDataHMDProvider;
                lineRenderer.enabled = !isHMDInput;

                if (!isHMDInput)
                    YVRManager.instance.eventsManager.onUpdate += UpdateLaserBeam;

                laserBeamAnchor = inputModule.rayTransform;
            };

            GenerateLaserBeamLineRender();
            GenerateCursor();
            YVRManager.instance.eventsManager.onUpdate += UpdateLaserBeam;
            inputModule.onRaycastHit += UpdateCursor;
        }

        /// <summary>
        /// Generate @YVR.Core.YVRRaycastLaserBeam.lineRenderer
        /// </summary>
        protected virtual void GenerateLaserBeamLineRender()
        {
            lineRenderer = this.AutoAddingGetComponent<LineRenderer>();
        }

        /// <summary>
        /// Generate @YVR.Core.YVRRaycastLaserBeam.cursorGameObject and @YVR.Core.YVRRaycastLaserBeam.cursorTransform
        /// </summary>
        protected virtual void GenerateCursor()
        {
            cursorGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            cursorGameObject.name = "LaserBeamPoint";
            DestroyImmediate(cursorGameObject.GetComponent<SphereCollider>());

            cursorTransform = cursorGameObject.transform;
            cursorTransform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            cursorMaterial = cursorGameObject.GetComponent<MeshRenderer>().material;
        }

        /// <summary>
        /// Updateing laser beam
        /// </summary>
        /// <remarks> Laser beam will only be updated when @YVR.Core.YVRInputModule.inputDataProvider is not YVR.Core.YVRInputModule.YVRInputDataHMDProvider </remarks>
        protected virtual void UpdateLaserBeam()
        {
            try
            {
                if (laserBeamAnchor!=null)
                {
                    lineRenderer.SetPosition(0, laserBeamAnchor.position);
                    lineRenderer.SetPosition(1, laserBeamAnchor.position + inputModule.inputDataProvider.rayDirection * laserLength);
                }
            } catch (System.Exception exception)
            {
                Debug.LogError("Exception in UpdateLaserBeam: " + exception.ToString());
            }
        }

        /// <summary>
        /// Updateing cursor
        /// </summary>
        /// <param name="targetGO"> Current raycast hitting game objects </param>
        /// <param name="hitPosition"> Current raycast hitting point's position </param>
        /// <param name="hitNormal"> Current raycast hitting point's normal </param>
        protected virtual void UpdateCursor(GameObject targetGO, Vector3 hitPosition, Vector3 hitNormal)
        {
            if (cursorTransform == null) return;

            if (cursorGameObject.activeSelf && targetGO == null)
                cursorGameObject.SetActive(false);
            else if (!cursorGameObject.activeSelf && targetGO != null)
                cursorGameObject.SetActive(true);

            if (inputModule.inputDataProvider.pressed)
                cursorMaterial.color = Color.red;
            else if (inputModule.inputDataProvider.released)
                cursorMaterial.color = Color.white;

            if (hitNormal != Vector3.zero)
            {
                Quaternion rotation = cursorTransform.rotation;
                rotation.SetLookRotation(hitNormal, cursorTransform.forward);
                cursorTransform.rotation = rotation;
            }

            cursorTransform.position = hitPosition - inputModule.inputDataProvider.rayDirection * 0.1f;
        }

        /// <summary>
        /// Virtual version of Unity life cycle function `OnDestroy`
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (YVRManager.instance)
                YVRManager.instance.eventsManager.onUpdate -= UpdateLaserBeam;
            if (inputModule)
                inputModule.onRaycastHit -= UpdateCursor;
        }
    }
}