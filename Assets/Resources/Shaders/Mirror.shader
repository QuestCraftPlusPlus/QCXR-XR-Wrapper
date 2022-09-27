// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Unlit shader. Simplest possible textured shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Unlit/Mirror" {
Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
	[Toggle(MIRROR_X)] _FlipX ("Mirror X", Float) = 0
    [Toggle(MIRROR_Y)] _FlipY ("Mirror Y", Float) = 0
}

SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 100

    Pass {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_fog
			#pragma multi_compile ___ MIRROR_X
            #pragma multi_compile ___ MIRROR_Y

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
				float2 untransformedUV = v.texcoord;
                #ifdef MIRROR_X
                    untransformedUV.x = 1.0 - untransformedUV.x;
                #endif // MIRROR_X
                #ifdef MIRROR_Y
                    untransformedUV.y = 1.0 - untransformedUV.y;
                #endif // MIRROR_Y
				o.texcoord = TRANSFORM_TEX(untransformedUV, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.texcoord);
                UNITY_APPLY_FOG(i.fogCoord, col);
                UNITY_OPAQUE_ALPHA(col.a);
                return col;
            }
        ENDCG
    }
}

}
