using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if USE_URP
using UnityEngine.Rendering.Universal;
[CreateAssetMenu(fileName = "OverDrawRendererData", menuName = "Rendering/OverDrawRendererData")]
public class OverdrawRendererData : ScriptableRendererData
{
    [SerializeField]
    public Material _overdrawOpaqueMaterial = null;
    [SerializeField]
    public Material _overdrawTransparentMaterial = null;

    public Material overdrawOpaqueMaterial => _overdrawOpaqueMaterial;
    public Material overdrawTransparentMaterial => _overdrawTransparentMaterial;

    protected override ScriptableRenderer Create()
    {
        return new OverdrawRenderer(this);
    }
}
#endif
