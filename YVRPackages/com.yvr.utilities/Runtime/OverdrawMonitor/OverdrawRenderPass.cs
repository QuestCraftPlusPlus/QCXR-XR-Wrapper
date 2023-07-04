#if USE_URP
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OverdrawRenderPass : ScriptableRenderPass
{
    private Material m_OverdrawMaterial;
    private SortingCriteria m_Criteria;
    private FilteringSettings m_FilteringSettings;
    private RenderStateBlock m_RenderStateBlock;

    private List<ShaderTagId> m_ShaderTagIds = new List<ShaderTagId>()
    {
        new ShaderTagId("SRPDefaultUnlit"),
        new ShaderTagId("LightweightForward"),
        new ShaderTagId("UniversalForward")
    };


    public OverdrawRenderPass(SortingCriteria criteria, RenderQueueRange renderQueueRange, Material overdrawMaterial)
    {
        m_OverdrawMaterial = overdrawMaterial;
        m_Criteria = criteria;
        m_FilteringSettings = new FilteringSettings(renderQueueRange);
        m_RenderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        DrawingSettings drawingSettings = CreateDrawingSettings(m_ShaderTagIds, ref renderingData, m_Criteria);

        if ((renderingData.cameraData.isSceneViewCamera &&
             (OverdrawMonitor.instance == null || OverdrawMonitor.instance.overrideInSceneView))
            || (!renderingData.cameraData.isSceneViewCamera &&
                (OverdrawMonitor.instance != null && OverdrawMonitor.instance.overrideInGame))
           )
            drawingSettings.overrideMaterial = m_OverdrawMaterial;

        context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref m_FilteringSettings,
            ref m_RenderStateBlock);
    }
}
#endif