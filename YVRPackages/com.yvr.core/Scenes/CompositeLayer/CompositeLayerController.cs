using UnityEngine;
using UnityEngine.Internal;

namespace YVR.Core.Demo
{
    [ExcludeFromDocs]
    public class CompositeLayerController : MonoBehaviour
    {
        public int renderTextureWidth = 1700;
        public int renderTextureHeight = 1700;

        public float orthographicSize = 150;
        public float aspect = 2;

        public YVRCompositeLayer underlay = null;

        private Camera m_CompositeLayerCamera = null;

        private void Start()
        {
            m_CompositeLayerCamera = GetComponent<Camera>();

            RenderTexture compositeLayerRT = new RenderTexture(1700, 1700, 24);
            compositeLayerRT.hideFlags = HideFlags.DontSave;
            compositeLayerRT.useMipMap = true;
            compositeLayerRT.filterMode = FilterMode.Trilinear;
            compositeLayerRT.antiAliasing = 1;
            compositeLayerRT.Create();

            m_CompositeLayerCamera.allowMSAA = false;
            m_CompositeLayerCamera.targetTexture = compositeLayerRT;
            m_CompositeLayerCamera.orthographic = true;


            underlay.texture = compositeLayerRT;
        }

        private void Update()
        {
            m_CompositeLayerCamera.orthographicSize = orthographicSize;
            m_CompositeLayerCamera.aspect = aspect;
        }
    }
}
