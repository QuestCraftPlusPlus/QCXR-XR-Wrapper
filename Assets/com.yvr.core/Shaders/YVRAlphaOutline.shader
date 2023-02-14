Shader "YVR/Hand/Built-in/Alpha Outline"
{
  Properties
  {
    _ColorPrimary ("Color Primary", Color) = (0.278962, 0.65210, 1)
    _ColorTop ("Color Top", Color) = (0.028873, 0.0283251, 0.0238195)
    _ColorBottom ("Color Bottom", Color) = (0.0217121, 0.0243531, 0.0213097)
    _RimFactor ("Rim Factor", Range(0.01, 1.0)) = 0.6
    _FresnelPower ("Fresnel Power", Range(0.01,2.0)) = 0.15

    _HandAlpha ("Hand Alpha", Range(0, 1)) = 1.0
    _MinVisibleAlpha ("Minimum Visible Alpha", Range(0,1)) = 0.2
  }
  SubShader
  {
    Tags {"Queue" = "Transparent" "Render" = "Transparent" "IgnoreProjector" = "True"}
    LOD 100

    Pass
    {
      ZWrite On
      ColorMask 0

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"

      float4 vert(float4 vertex : POSITION) : SV_POSITION
      {
        return UnityObjectToClipPos(vertex);
      }

      fixed4 frag() : SV_Target
      {
        return 0;
      }
      ENDCG
    }

    CGPROGRAM
    #include "UnityCG.cginc"

    #pragma surface surf BlinnPhong alpha:fade exclude_path:prepass noforwardadd halfasview nolightmap

    sampler2D _MainTex;

    struct Input {
      float2 uv_MainTex;
      float3 worldRefl;
      float3 viewDir;
      INTERNAL_DATA
    };

    #define ColorBlack half3(0, 0, 0)
    #define EmissionFactor (0.90)

    fixed3 _ColorPrimary;
    fixed3 _ColorTop;
    fixed3 _ColorBottom;
    float _RimFactor;
    float _FresnelPower;

    float _HandAlpha;
    float _MinVisibleAlpha;

    float3 SafeNormalize(float3 normal) {
      float magSq = dot(normal, normal);
      if (magSq == 0) {
        return 0;
      }
      return normalize(normal);
    }

    void surf(Input IN, inout SurfaceOutput o) {
      float3 normalDirection = SafeNormalize(o.Normal);
      float3 viewDir = SafeNormalize(IN.viewDir);
      half viewDotNormal = saturate(dot(viewDir, normalDirection));

      half rim = pow(1.0 - viewDotNormal, 0.5) * (1.0 - _RimFactor) + _RimFactor;
      rim = saturate(rim);

      half3 emission = lerp(ColorBlack, _ColorPrimary, rim);

      emission += rim * 0.5;
      emission *= EmissionFactor;

      float fresnel = saturate(pow(1.0 - viewDotNormal, _FresnelPower));
      fixed3 color = lerp(_ColorTop, _ColorBottom, fresnel);

      fixed alphaValue = step(_MinVisibleAlpha, _HandAlpha) * _HandAlpha;

      o.Albedo = 0;
      o.Gloss = 0;
      o.Specular = 0;
      o.Alpha = alphaValue;
      o.Emission = color * emission;
    }
    ENDCG
  }
}