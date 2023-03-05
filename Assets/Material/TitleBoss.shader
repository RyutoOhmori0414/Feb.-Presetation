Shader "Unlit/TitleBoss"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
         [Space]
        _LocalTime("Animation Time", Float) = 0.0
        _Range ("Range", Float) = 0.0
    }
    SubShader
    {
        Tags
        { 
            "RenderType"="Opaque"
            "RenderPipeline"="UniversalPipeline"
        }
        LOD 100

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"



        CBUFFER_START(UnityPerMaterial)
        CBUFFER_END
        ENDHLSL

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            
            // Universal Pipeline shadow keywords
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"


            half4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _LocalTime;
            float _Range;

            struct Attributes
            {
                float4 position : POSITION;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };

            struct Varyings
            {
                float4 position : SV_POSITION;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
                float fogFactor: TEXCOORD1;
                float3 posW : TEXCOORD2;
            };

            Attributes vert (Attributes input)
            {
                //input.position = float4(TransformObjectToWorld(input.position.xyz), 1);
                input.normal = TransformObjectToWorldNormal(input.normal);
                //input.tangent.xyz = UnityObjectToWorldDir(input.tangent.xyz);
                //input.texcoord = TRANSFORM_TEX(input.texcoord, _MainTex);
                return input;
            }

            Varyings OutVaryings(float3 wpos, float3 wnrm, float2 uv)
            {
                Varyings o;
                o.position = TransformObjectToHClip(wpos);
                o.posW = wpos;
                o.normal = wnrm;
                o.texcoord = uv;
                o.fogFactor = ComputeFogFactor(o.position.z);

                return o;
            }

            float3 ConstructNormal(float3 v1, float3 v2, float3 v3)
            {
                // 面に直行した法線を再計算している
                return normalize(cross(v2 - v1, v3 - v1));
            }

            // pidは各ポリゴンのID　それぞれのポリゴンに異なる処理を行うために使用
            [maxvertexcount(15)]
            void geom (triangle Attributes input[3], uint pid : SV_PrimitiveID, inout TriangleStream<Varyings> outStream)
            {
                float3 wp0 = input[0].position.xyz;
                float3 wp1 = input[1].position.xyz;
                float3 wp2 = input[2].position.xyz;

                float2 uv0 = input[0].texcoord;
                float2 uv1 = input[1].texcoord;
                float2 uv2 = input[2].texcoord;

                // どれだけ外に押し出すか
                float extrusion = saturate(0.4 - cos(_LocalTime * 3.14 * 2) * 0.4);
                extrusion *= 1 + 0.3 * sin(pid + _LocalTime);

                float3 worldNormal = ConstructNormal(wp0, wp1, wp2);
                float3 extNormal = worldNormal * extrusion * _Range;
                float np = saturate(extrusion * 10);

                float3 extPositions[3];

                // 外側
                [unroll]
                for(int j = 0; j < 3; j++)
                {
                    Attributes temp = input[j];
                    temp.position.xyz += extNormal;
                    extPositions[j] = temp.position.xyz;
                    temp.normal = lerp(temp.normal, worldNormal, np);
                    outStream.Append(OutVaryings(extPositions[j], temp.normal, temp.texcoord));
                }

                outStream.RestartStrip(); // 新たに三角メッシュを生成する際に必要

                // 側面
                [unroll]
                for(int i = 0; i < 3; i++)
                {
                    float3 tempNormal = ConstructNormal(extPositions[i], input[i].position.xyz, extPositions[(i + 1) % 3]);
                    outStream.Append(OutVaryings(extPositions[i], worldNormal, input[i].texcoord));
                    outStream.Append(OutVaryings(input[i].position.xyz, worldNormal, input[i].texcoord));
                    outStream.Append(OutVaryings(extPositions[(i + 1) % 3], worldNormal, input[(i + 1) % 3].texcoord));
                    outStream.Append(OutVaryings(input[(i + 1) % 3].position.xyz, worldNormal, input[(i + 1) % 3].texcoord));
                    outStream.RestartStrip();
                }
            }

            float4 frag (Varyings i) : SV_Target
            {
                float4 col = _Color;

                float4 shadowCoord = TransformWorldToShadowCoord(i.posW);
                Light mainLight = GetMainLight(shadowCoord);
                float3 view = GetWorldSpaceNormalizeViewDir(i.posW.xyz);
                float3 normal = normalize(i.normal);
                float diffuse = saturate(dot(normal, view));
                col.rgb *= diffuse;

                col.rgb = MixFog(col.rgb, i.fogFactor);

                return col;
            }
            ENDHLSL
        }
    }
}
