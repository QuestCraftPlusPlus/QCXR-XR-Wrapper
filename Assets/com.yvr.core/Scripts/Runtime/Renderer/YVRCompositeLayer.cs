using System;
using UnityEngine;
using UnityEngine.Internal;
using YVR.Utilities;

namespace YVR.Core
{
    /// <summary>
    /// Used to represent composite layer, which contains overlay / underlay
    /// </summary>
    public class YVRCompositeLayer : MonoBehaviour
    {
        /// <summary>
        /// The displayed texture on composite layer
        /// </summary>
        public Texture texture = null;

        /// <summary>
        /// Composite layer depth.
        /// If depth less-than 0, the layer will work as underLayer, otherwise, the layer will works as overlay
        /// </summary>
        [SerializeField] protected int compositionDepth = -1;

        /// <summary>
        /// Render scale for composite layer resolution.
        /// While render scale is 1.0, composite layer resolution will equal to the resolution of [texture](xref: YVR.Core.YVRCompositeLayer.texture)
        /// </summary>
        [SerializeField] protected float renderScale = 1.0f;

        /// <summary>
        /// Render content is dynamic
        /// If the content you are rendering is dynamic, set this value to true, otherwise the rendered image will remain static even if the data content is updated.
        /// This is done to reduce the performance cost of static page rendering.
        /// </summary>
        [SerializeField] protected bool isDynamic = false;

        /// <summary>
        /// Should update composite layer texture to native automatically
        /// </summary>
        [SerializeField] protected bool autoUpdateContent = false;

        /// <summary>
        /// Should init native composite layer automatically
        /// </summary>
        [SerializeField] protected bool autoInitLayer = true;

        [SerializeField] protected bool requireToForceUpdateContent = false;

        private YVRManager m_YVRManager = null;
        private Texture m_CachedTexture = null;
        protected IYVRLayerHandle layerHandler = null;
        private int m_TextureHandle = -1;
        private int m_tempCacheDepth;

        public int depth
        {
            get => compositionDepth;
        }

        /// <summary>
        /// ID of the texture
        /// </summary>
        protected virtual int textureHandle
        {
            get
            {
                if (m_CachedTexture == texture || texture == null) return m_TextureHandle;
                m_TextureHandle = (int) texture.GetNativeTexturePtr();
                m_CachedTexture = texture;
                return m_TextureHandle;
            }
        }

        /// <summary>
        /// The mask id of render layer
        /// </summary>
        public int renderLayerId { get; private set; } = -1;

        private Transform m_Transform = null;

        /// <summary>
        /// The width of the actual texture used in the compositeLayer
        /// </summary>
        protected virtual int width => texture ? (int) (texture.width * renderScale) : 0;

        /// <summary>
        /// The height of the actual texture used in the compositeLayer
        /// </summary>
        protected virtual int height => texture ? (int) (texture.height * renderScale) : 0;

        protected int swapChainBufferCount => isDynamic ? 3 : 1;

        public Action<int> onLayerCreatedGfx = null;
        public Action onLayerDestroyedGfx = null;
        public Action onLayerAddedGfx = null;
        public Action onLayerRemovedGfx = null;
        public Action<bool, int> onLayerDepthSetGfx = null;


        [ExcludeFromDocs]
        protected void OnEnable() { Show(); }

        private void Awake()
        {
            m_Transform = transform;
            m_YVRManager = YVRManager.instance;

#if UNITY_ANDROID && !UNITY_EDITOR
            layerHandler = new YVRLayerAndroidHandler();
#else
            layerHandler = new YVRLayerEditorHandle();
#endif
        }

        private void Start()
        {
            if (autoInitLayer)
                InitCompositeLayer(compositionDepth);
        }

        protected virtual void LateUpdate()
        {
            if (renderLayerId != -1) layerHandler.SwapBufferLayer(renderLayerId);

            if (!requireToForceUpdateContent && (!isDynamic || !autoUpdateContent)) return;
            UpdateCompositeLayerContent();
        }

        /// <summary>
        /// Init native composite layer, register composite layer update operations.
        /// </summary>
        /// <param name="depth">The depth of the composite layer</param>
        public void InitCompositeLayer(int depth = int.MinValue)
        {
            if (!isActiveAndEnabled) return;

            if (depth != int.MinValue) compositionDepth = depth;

            if (renderLayerId == -1)
            {
                YVRLayerCreateInfo layerCreateInfo
                    = new YVRLayerCreateInfo(compositionDepth, width, height, swapChainBufferCount);
                layerHandler.CreateLayerAsync(layerCreateInfo, (layerId) =>
                {
                    OnLayerCreatedGfx(layerId);
                    OnLayerAddedGfx();
                    if (!isDynamic)
                        requireToForceUpdateContent = true;
                });
            }
        }

        /// <summary>
        /// Show the composite layer
        /// </summary>
        protected void Show()
        {
            if (renderLayerId != -1)
                layerHandler.AddLayerAsync(renderLayerId, OnLayerAddedGfx);
        }

        /// <summary>
        /// Hide the composite layer
        /// </summary>
        protected void Hide()
        {
            if (renderLayerId != -1)
                layerHandler.RemoveLayerAsync(renderLayerId, OnLayerRemovedGfx);
        }

        /// <summary>
        /// Set the render depth of the composite layer
        /// </summary>
        /// <param name="depth">The new render depth</param>
        public void SetLayerDepth(int depth)
        {
            m_tempCacheDepth = depth;
            if (renderLayerId != -1)
                layerHandler.SetLayerDepthAsync(renderLayerId, depth, OnLayerDepthSetGfx);
        }

        protected virtual void OnLayerCreatedGfx(int layerId)
        {
            renderLayerId = layerId;
            onLayerCreatedGfx?.Invoke(layerId);
        }

        protected virtual void OnLayerDestroyedGfx()
        {
            renderLayerId = -1;
            onLayerDestroyedGfx?.Invoke();
        }

        protected virtual void OnLayerAddedGfx()
        {
            onLayerAddedGfx?.Invoke();
            if (!isDynamic || !autoUpdateContent) requireToForceUpdateContent = true;

            m_YVRManager.cameraRig.afterRigBeforeRenderUpdated += UpdateLayerPose;
        }

        protected virtual void OnLayerDepthSetGfx(bool result)
        {
            if (result) compositionDepth = m_tempCacheDepth;
            onLayerDepthSetGfx?.Invoke(result, compositionDepth);
        }

        protected virtual void OnLayerRemovedGfx()
        {
            onLayerRemovedGfx?.Invoke();

            m_YVRManager.cameraRig.afterRigBeforeRenderUpdated -= UpdateLayerPose;
        }


        private void UpdateLayerPose()
        {
            // In some cases, this game object has been destroyed, however layer has not been removed (due to the gfx thread delay, about 1 or 2 frame).
            // In these cases, this function has not been unRegistered from afterRigBeforeRenderUpdate.
            // In these cases, when this function be called, the real native data in m_Transform may has been released. Thus, we manually set m_Transform to be null, and do null check here to avoid null reference.
            if (m_Transform == null) return;

            XRPose pose = m_Transform.ToXRTrackingSpacePose(m_YVRManager.cameraRenderer.centerEyeCamera);
            layerHandler.SetLayerPose(renderLayerId, pose);

            UpdateLayerSize();
        }

        private void UpdateLayerSize()
        {
            if (m_Transform == null) return;

            Vector3 lossyScale = m_Transform.lossyScale;
            layerHandler.SetLayerSize(renderLayerId, new XRSize(lossyScale.x, lossyScale.y));
        }


        public virtual void UpdateCompositeLayerContent()
        {
            if (renderLayerId == -1 || textureHandle == -1) return;
            GfxHelper.instance.TriggerGfxThreadCallback(() =>
            {
                int destTextureId = layerHandler.GetLayerColorHandle(renderLayerId, -1);
                GfxHelper.instance.CopyTexture(textureHandle, destTextureId);
                requireToForceUpdateContent = false;
            });
        }

        [ExcludeFromDocs]
        protected void OnDisable() { Hide(); }

        [ExcludeFromDocs]
        protected virtual void OnDestroy()
        {
            m_Transform = null;
            layerHandler.DestroyLayerAsync(renderLayerId, OnLayerDestroyedGfx);
        }
    }
}