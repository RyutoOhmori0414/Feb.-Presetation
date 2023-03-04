Shader "Unlit/Dissolve"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _DissolveTex ("DissolveTex", 2D) = "white" {}
        _DissolveAmount ("DissolveAmount", Range(0, 1)) = 0.5
        _DissolveRange ("DissolveRange", Range(0, 1)) = 0.5
        _DissolveColor ("DissolveColor", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;

            sampler2D _DissolveTex;
            float4 _DissolveTex_ST;

            float _DissolveAmount;
            float _DissolveRange;
            float4 _DissolveColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color;
                return o;
            }

            float remap(float value, float outMin)
            {
                return value * ((1 - outMin) / 1) + outMin;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 color = i.color * tex2D(_MainTex, i.uv);

                half DissolveAlpha = tex2D(_DissolveTex, i.uv).r;
                DissolveAlpha -= 0.001;
                _DissolveAmount = remap(_DissolveAmount, -_DissolveRange);
                if (DissolveAlpha < _DissolveAmount + _DissolveRange)
                {
                    color.rgb += _DissolveColor.rgb;
                }

                if (DissolveAlpha < _DissolveAmount)
                {
                    color.a = 0;
                }

                color.rgb *= color.a;
                return color;
            }
            ENDCG
        }
    }
}
