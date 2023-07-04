Shader "Unlit/YVRHandOutlineBuildinRP"
{
    Properties
    {
        [HDR] _OutlineColor("Outline Color", Color) = (1,1,1,1)
        _OutlineSize("Outline Size",Range(0.01,1)) = 1
    }
    SubShader
    {
        Tags{"Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True"}
        LOD 300

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            float4 _OutlineColor;
            float _OutlineSize;

            v2f vert (appdata v)
            {
                v2f o;

                float3 normalWS = mul((float3x3)unity_ObjectToWorld, v.normal);
                float3 positionWS = mul(unity_ObjectToWorld, v.vertex) + normalWS * _OutlineSize * 0.01;

                o.vertex = UnityWorldToClipPos(positionWS);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half4 color = half4(0, 0, 0, 1);

                color.rgb = _OutlineColor.rgb;
                color.a = i.color.a;
                return color;
            }
            ENDCG
        }
    }
}
