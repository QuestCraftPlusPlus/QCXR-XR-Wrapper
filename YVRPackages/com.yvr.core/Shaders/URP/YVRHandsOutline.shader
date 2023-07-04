Shader "YVR/Shaders/HandOutline"
{
    Properties
    {
        [HDR]_OutlineColor("Outline Color", Color) = (1,1,1,1)
        _OutlineSize("Outline Size",Range(0.01,1)) = 1
        _ForceState("Force State",Range(0,1)) = 0
        
        _Mask("_Mask",2D) = "white"{}
        _MaskStength("_MaskStength",Float) = 1
    }

    SubShader
    {
        // Universal Pipeline tag is required. If Universal render pipeline is not set in the graphics settings
        // this Subshader will fail. One can add a subshader below or fallback to Standard built-in to make this
        // material work with both Universal Render Pipeline and Builtin Unity Pipeline
        Tags{"Queue" = "Transparent" "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" "UniversalMaterialType" = "Lit" "IgnoreProjector" = "True" "ShaderModel" = "2.0"}
        LOD 300

        // ------------------------------------------------------------------
        //  Forward pass. Shades all light in a single pass. GI + emission + Fog
        Pass
        {   
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            Cull Front
            
            HLSLPROGRAM
            #pragma target 2.0

            // -------------------------------------
            // Unity defined keywords
       
            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            
            #pragma vertex LitPassVertex
            #pragma fragment LitPassFragment

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct Attributes
            {
                float4 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
                half4  color        : COLOR;
                float2 uv           : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS               : SV_POSITION;
                half4  color                    : COLOR;
                float2 uv                       : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            CBUFFER_START(UnityPerMaterial)
            half4 _OutlineColor;
            half  _OutlineSize;
            half  _ForceState;
            half  _MaskStength;
            CBUFFER_END


            TEXTURE2D(_Mask);
            SAMPLER(sampler_Mask);

            Varyings LitPassVertex(Attributes input)
            {
                Varyings output = (Varyings)0;

                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                // already normalized from normal transform to WS.
                float3 normalWS = TransformObjectToWorldNormal(input.normalOS);
                float3 positionWS = TransformObjectToWorld(input.positionOS) + normalWS * _OutlineSize * 0.01;

                output.positionCS = TransformWorldToHClip(positionWS);
                output.color = input.color;
                output.uv = input.uv;
                return output;
            }


            half4 LitPassFragment(Varyings input) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                half4 color = half4(0, 0, 0, 1);
                
                color.rgb = _OutlineColor.rgb;
                color.a = input.color.a;

                half mask = SAMPLE_TEXTURE2D_LOD(_Mask, sampler_Mask, input.uv, 0).r;
                //half mask = input.color.r;
                color.rgb *= lerp(1, 1 + mask * _MaskStength, _ForceState);

                return color;
            }

            ENDHLSL
        }
    }

    FallBack "Hidden/Universal Render Pipeline/FallbackError"
}
