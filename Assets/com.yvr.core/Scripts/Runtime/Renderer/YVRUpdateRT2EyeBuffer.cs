#if USE_URP
using UnityEngine;
using UnityEngine.Rendering.Universal;
using YVR.Core;
using YVR.Utilities;

/// <summary>
/// This script should only be used while required to render different layer in separate eye with Urp
/// If so, this script should be attached to left/right camera.
/// While using this script, please ensure that left/right camera is enable, center camera is disabled,
/// and `Use Monoscopic` is turned on in YVR XR Settings, `Stereo Rendering Mode` is `Multi Pass`
/// </summary>
public class YVRUpdateRT2EyeBuffer : MonoBehaviour
{
    [SerializeField] private bool m_Left = true;

    private Camera m_Camera = null;
    private IYVRLayerHandle m_LayerHandle = null;

    private void Start()
    {
        Vector2 fov = YVRManager.instance.cameraRenderer.GetEyeRenderDesc(EyeSide.Left).fov;
        Vector2 resolution = YVRManager.instance.cameraRenderer.GetEyeRenderDesc(EyeSide.Left).resolution;
        m_LayerHandle = new YVRLayerAndroidHandler();
        m_Camera = GetComponent<Camera>();
        m_Camera.fieldOfView = fov.x;
        m_Camera.aspect = fov.y / fov.x;
        m_Camera.targetTexture = new RenderTexture((int) resolution.x, (int) resolution.y, 24);
        m_Camera.GetUniversalAdditionalCameraData().allowXRRendering = false;

        YVRManager.instance.eventsManager.onPreSubmitGfx += OnEventsManagerOnPreSubmitGfx;
    }

    private void OnEventsManagerOnPreSubmitGfx()
    {
        int eyeBufferLayerId = m_LayerHandle.GetEyeBufferLayerId();
        if (eyeBufferLayerId == -1) return;

        GfxHelper.instance.CopyTexture((int) m_Camera.activeTexture.GetNativeTexturePtr(),
            m_LayerHandle.GetLayerColorHandle(eyeBufferLayerId, m_Left ? EyeSide.Left : EyeSide.Right));
    }

    private void OnDestroy()
    {
        RenderTexture rt = m_Camera.targetTexture;
        rt.Release();

        YVRManager.instance.eventsManager.onPreSubmitGfx -= OnEventsManagerOnPreSubmitGfx;
    }
}
#endif