using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if USE_URP
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class OverdrawRenderPass : ScriptableRenderPass
{
    private Material overdrawMaterial;
    private SortingCriteria criteria;
    private FilteringSettings filteringSettings;
    private RenderStateBlock renderStateBlock;

    private List<ShaderTagId> shaderTagIds = new List<ShaderTagId>()
    {
        new ShaderTagId("SRPDefaultUnlit"),
        new ShaderTagId("LightweightForward"),
        new ShaderTagId("UniversalForward")
    };


    public OverdrawRenderPass(SortingCriteria criteria, RenderQueueRange renderQueueRange, Material overdrawMaterial)
    {
        this.overdrawMaterial = overdrawMaterial;
        this.criteria = criteria;
        this.filteringSettings = new FilteringSettings(renderQueueRange);
        this.renderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        DrawingSettings drawingSettings = CreateDrawingSettings(shaderTagIds, ref renderingData, criteria);

        if ((renderingData.cameraData.isSceneViewCamera && (OverdrawMonitor.instance == null || OverdrawMonitor.instance.overrideInSceneView))
         || (!renderingData.cameraData.isSceneViewCamera && (OverdrawMonitor.instance != null && OverdrawMonitor.instance.overrideInGame))
        )
            drawingSettings.overrideMaterial = overdrawMaterial;

        context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings, ref renderStateBlock);
    }
}
#endif