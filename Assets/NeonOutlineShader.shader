Shader "Custom/NeonOutline"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (0, 1, 1, 1)  // Cyan Glow
        _OutlineWidth("Outline Width", Range(0.0, 0.1)) = 0.02
        _Glow("Glow Intensity", Range(0.0, 1.0)) = 0.5
    }

        SubShader
        {
            Tags {"Queue" = "Overlay" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off Lighting Off ZWrite Off

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _OutlineColor;
                float _OutlineWidth;
                float _Glow;

                v2f vert(appdata_t v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    float4 col = tex2D(_MainTex, i.uv);

                    // Berechnet den Umriss
                    float outline = (1.0 - col.a) * _OutlineWidth;
                    float glowEffect = smoothstep(0.1, 0.5, outline) * _Glow;

                    // Kombiniert den Glow mit der Originalfarbe
                    float4 outlineColor = _OutlineColor * glowEffect;
                    return col + outlineColor;
                }
                ENDCG
            }
        }
}
