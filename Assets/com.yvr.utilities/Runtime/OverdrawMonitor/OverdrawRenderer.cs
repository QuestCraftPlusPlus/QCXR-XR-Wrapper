using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if USE_URP
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class OverdrawRenderer : ScriptableRenderer
{
    private OverdrawRenderPass renderOpaquePass = default;
    private OverdrawRenderPass renderTransparentPass = default;

    public OverdrawRenderer(ScriptableRendererData data) : base(data)
    {
        renderOpaquePass = new OverdrawRenderPass(SortingCriteria.CommonOpaque, RenderQueueRange.opaque, (data as OverdrawRendererData).overdrawOpaqueMaterial);
        renderTransparentPass = new OverdrawRenderPass(SortingCriteria.CommonTransparent, RenderQueueRange.transparent, (data as OverdrawRendererData).overdrawTransparentMaterial);
    }

    public override void Setup(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        // As Skybox can not override material, here not rendering skybox
        EnqueuePass(renderOpaquePass);
        EnqueuePass(renderTransparentPass);
    }
}
#endif
