Shader "Custom/Player"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _BackColor ("BackColor", Color) = (1, 1, 1, 1)
        _PositionFactor ("Position Factor", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            uniform fixed4 _Color;
            uniform fixed4 _BackColor;
            uniform float _PositionFactor;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct g2f
            {
                float4 vertex : SV_POSITION;
                UNITY_FOG_COORDS(1)
            };

            appdata vert (appdata v)
            {
                
                return v;
            }

            // �W�I���g���[�V�F�[�_
            // ������input�͕����ʂ蒸�_�V�F�[�_����̏o��
            // stream�͎Q�Ɠn���Ŏ��̏����ɒl���󂯓n���Ă���BTriangleStream<>�ŎO�p�ʂ��o�͂��Ă���
            [maxvertexcount(3)] // �o�͂��钸�_��
            void geom(triangle appdata input[3], inout TriangleStream<g2f> stream)
            {
                // [0]�̒��_����ɂ��x�N�g�����Z�o���āA���̊O�ς���邱�ƂŖ@�������߂Ă���
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
                float4 col = facing > 0 ? _Color : _BackColor;
                
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
