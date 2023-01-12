Shader "YVR/Hand/URP/HandUnlit"
{
    Properties
    {
        _BaseColor("Base Color",Color) = (1,1,1,1)
        [HDR]_OutlineColor("Outline Color", Color) = (1,1,1,1)
        _SoftMin("Soft Min",Range(0.01,1)) = 1
        _SoftMax("Soft Max",Range(0.01,2)) = 1
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
            ZWrite Off
            Cull Off

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
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float3 normalWS                 : TEXCOORD1;
                float3 viewDirWS                : TEXCOORD2;
                float4 positionCS               : SV_POSITION;
                half4  color                    : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            CBUFFER_START(UnityPerMaterial)
            half4 _BaseColor;
            half4 _OutlineColor;
            half _SoftMin;
            half _SoftMax;

            CBUFFER_END

            Varyings LitPassVertex(Attributes input)
            {
                Varyings output = (Varyings)0;

                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);

                // normalWS and tangentWS already normalize.
                // this is required to avoid skewing the direction during interpolation
                // also required for per-vertex lighting and SH evaluation

                half3 viewDirWS = GetWorldSpaceViewDir(vertexInput.positionWS);

                // already normalized from normal transform to WS.
                output.normalWS = TransformObjectToWorldNormal(input.normalOS);
                output.viewDirWS = viewDirWS;

                output.positionCS = vertexInput.positionCS;
                output.color = input.color;

                return output;
            }


            half4 LitPassFragment(Varyings input) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                half4 color = half4(0, 0, 0, 1);

                half NdotV = abs(dot(normalize(input.viewDirWS), input.normalWS));

                half factor = smoothstep(_SoftMin, _SoftMax, 1.0 - NdotV);

                color.rgb = _BaseColor.rgb;
                color.rgb += _OutlineColor.rgb * factor;

                color.a = _BaseColor.a * input.color.a;

                return color;
            }

            ENDHLSL
        }
    }

    FallBack "Hidden/Universal Render Pipeline/FallbackError"
}
