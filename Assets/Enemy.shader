Shader "Custom/Enemy"
{
    Properties
    {
        _Color ("MainColor", Color) = (1, 1, 1, 1)
        _BackColor ("BackColor", Color) = (1, 1, 1, 1)
        _PositionFactor ("PositionFactor", Float) = 0.5
        [Space]
        _TransparentColor ("TransparentColor", Color) = (1, 1, 1, 1)
        [Space]
        [Enum(UnityEngine.Rendering.CompareFunction)]
        _ZTest("ZTest", Float) = 1
        [KeywordEnum(Normal, Transparent)]
        _Type("Type", Float) = 0
        [Toggle(_DIFFUSION)]
        _Diffusion ("random Diffusion", Float) = 0
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
            #pragma multi_compile _ _DIFFUSION

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct g2f
            {
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            uniform fixed4 _Color;
            uniform fixed4 _BackColor;
            uniform fixed4 _TransparentColor;
            uniform float _PositionFactor;

            appdata vert (appdata v)
            {
                return v;
            }

            fixed2 random2(fixed2 st)
            {
                st = fixed2( dot(st,fixed2(127.1,311.7)),
                               dot(st,fixed2(269.5,183.3)) );
                return -1.0 + 2.0*frac(sin(st)*43758.5453123);
            }

            float perlinNoise(fixed2 st) 
            {
                fixed2 p = floor(st);
                fixed2 f = frac(st);
                fixed2 u = f*f*(3.0-2.0*f);

                float v00 = random2(p+fixed2(0,0));
                float v10 = random2(p+fixed2(1,0));
                float v01 = random2(p+fixed2(0,1));
                float v11 = random2(p+fixed2(1,1));

                return lerp( lerp( dot( v00, f - fixed2(0,0) ), dot( v10, f - fixed2(1,0) ), u.x ),
                             lerp( dot( v01, f - fixed2(0,1) ), dot( v11, f - fixed2(1,1) ), u.x ), 
                             u.y)+0.5f;
            }


            [maxvertexcount(3)]
            void geom (triangle appdata input[3], inout TriangleStream<g2f> stream)
            {
                float3 vec1 = input[1].vertex - input[0].vertex;
                float3 vec2 = input[2].vertex - input[0].vertex;
                float3 normal = normalize(cross(vec1, vec2));
                float random = 1;

            #ifdef _DIFFUSION
                random = perlinNoise(input[0].uv) * 3;

            #endif
                [unroll]
                for (int i = 0; i < 3; i++)
                {
                    appdata v = input[i];
                    g2f o;

                    v.vertex.xyz += normal * _PositionFactor * random;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
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
                col = _TransparentColor;
            #endif
                col.rgb *= col.a;
                return col;
            }
            ENDCG
        }
    }
}
