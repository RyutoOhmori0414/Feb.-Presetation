Shader "Custom/Player"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _PositionFactor ("Position Factor", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom

            #include "UnityCG.cginc"

            uniform fixed4 _Color;
            uniform float _PositionFactor;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct g2f
            {
                float4 vertex : SV_POSITION;
            };

            appdata vert (appdata v)
            {
                return v;
            }

            // ジオメトリーシェーダ
            // 引数のinputは文字通り頂点シェーダからの出力
            // streamは参照渡しで次の処理に値を受け渡している。TriangleStream<>で三角面を出力している
            [maxvertexcount(3)] // 出力する頂点数
            void geom(triangle appdata input[3], inout TriangleStream<g2f> stream)
            {
                // [0]の頂点を基準にしベクトルを算出して、その外積を取ることで法線を求めている
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
                    stream.Append(o);
                }
            }

            fixed4 frag (g2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
