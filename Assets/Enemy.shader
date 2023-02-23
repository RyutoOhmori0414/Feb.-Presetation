Shader "Custom/Enemy"
{
    Properties
    {
        _Color ("MainColor", Color) = (1, 1, 1, 1)
        _BackColor ("BackColor", Color) = (1, 1, 1, 1)
        _PositionFactor ("PositionFactor", Range(0, 1)) = 0.5
        [Enum(UnityEngine.Rendering.CompareFunction)]
        _ZTest("ZTest", Float) = 1
        [KeywordEnum(Normal, Transparent)]
        _Type("Type", Float) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue" = "Transparent"
            "RenderPipeline"="UniversalPipeline"
        }
        LOD 100
        blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZTest [_ZTest]

        // UnityのDeferredレンダリングは複数パスに対応しておらず最初のパスが出力されるのみ
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom
            // make fog work
            #pragma multi_compile_fog
            #pragma multi_compile _TYPE_NORMAL _TYPE_TRANSPARENT

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct g2f
            {
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            uniform fixed4 _Color;
            uniform fixed4 _BackColor;
            uniform float _PositionFactor;

            appdata vert (appdata v)
            {
                return v;
            }

            [maxvertexcount(3)]
            void geom (triangle appdata input[3], inout TriangleStream<g2f> stream)
            {
                float3 vec1 = input[1].vertex - input[0].vertex;
                float3 vec2 = input[2].vertex - input[0].vertex;
                float3 normal = normalize(cross(vec1, vec2));

                [unroll]
                for (int i = 0; i < 3; i++)
                {
                    appdata v = input[i];
                    g2f o;

                    v.vertex.xyz += normal * _PositionFactor;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    UNITY_TRANSFER_FOG(o, o.vertex);
                    stream.Append(o);
                }
            }

            fixed4 frag (g2f i, fixed facing : VFACE) : SV_Target
            {
                float4 col;
            #ifdef _TYPE_NORMAL
                col = facing > 0 ? _Color : _BackColor;
                
                UNITY_APPLY_FOG(i.fogCoord, col);
            #elif _TYPE_TRANSPARENT
                col = float4(0, 1, 1, 1);
            #endif

                return col;
            }
            ENDCG
        }
    }
}
