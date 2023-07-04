Shader "Unlit/YVRHandBuildinRP"
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
        Tags{"Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True"}
        LOD 300

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 color : color;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
                float4 color : COLOR;
            };

            half4 _BaseColor;
            half4 _OutlineColor;
            half _SoftMin;
            half _SoftMax;

            v2f vert(appdata v) {
                v2f o;

                // transform the vertex position and normal to world space
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);

                // calculate the view direction in world space
                o.viewDir = normalize(_WorldSpaceCameraPos - mul(unity_ObjectToWorld, v.vertex).xyz);

                // pass the vertex color to the fragment function
                o.color = v.color;

                return o;
            }

            // define the fragment function
            fixed4 frag(v2f i) : SV_Target{

                fixed4 col;

            // calculate the dot product of the normal and view direction
            half NdotV = abs(dot(normalize(i.viewDir), i.normal));

            // apply a smoothstep function to create an outline effect
            half factor = smoothstep(_SoftMin, _SoftMax, 1.0 - NdotV);

            // mix the base color and outline color based on the factor
            col.rgb = lerp(_BaseColor.rgb, _OutlineColor.rgb, factor);

            // multiply the base color alpha with the vertex color alpha
            col.a = _BaseColor.a * i.color.a;

            return col;
            }

            ENDCG
        }
    }
}
