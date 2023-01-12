using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
using YVR.Core;
using UnityEngine.Rendering;

namespace YVR.Core.Demo
{
    [ExcludeFromDocs]
    public class CompositeLayerController : MonoBehaviour
    {
        public YVRCompositeLayer underlay = null;

        private Camera m_compositeLayerCamera = null;

        private int m_defaultDepth = 0;

        private void Start()
        {
            m_compositeLayerCamera = GetComponent<Camera>();

            RenderTexture compositeLayerRT = new RenderTexture(1700, 1700, 24);
            compositeLayerRT.hideFlags = HideFlags.DontSave;
            compositeLayerRT.useMipMap = true;
            compositeLayerRT.filterMode = FilterMode.Trilinear;
            compositeLayerRT.antiAliasing = 1;
            compositeLayerRT.Create();

            m_compositeLayerCamera.allowMSAA = false;
            m_compositeLayerCamera.targetTexture = compositeLayerRT;
            m_compositeLayerCamera.orthographic = true;
            m_compositeLayerCamera.orthographicSize = 300 / 2.0f;
            m_compositeLayerCamera.aspect = 2.0f;

            underlay.texture = compositeLayerRT;
            m_defaultDepth = underlay.depth;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                underlay.enabled = !underlay.enabled;
            }

            if (Input.GetKeyDown(KeyCode.Y) || YVRInput.GetDown(YVRInput.RawButton.Y))
            {
                if (underlay.depth == m_defaultDepth)
                {
                    underlay.SetLayerDepth(m_defaultDepth - 3);
                }
                else
                {
                    underlay.SetLayerDepth(m_defaultDepth);
                }

            }

        }
    }
}
