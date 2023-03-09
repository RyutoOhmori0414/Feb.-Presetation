Shader "Unlit/Boss"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [HDR]_Color ("Color", Color) = (1, 1, 1, 1)
         [Space]
        _LocalTime("Animation Time", Float) = 0.0
        _Range ("Range", Float) = 0.0
        [Space]
        _DissolveTex ("DissolveTex", 2D) = "white" {}
        _DissolveAmount ("DissolveAmout", Range(0, 1)) = 0.5
        _DissolveRange ("DissolveRange", Range(0, 1)) = 0.5
        _DissolveColor ("DissolveColor", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags
        { 
            "RenderType"="Opaque"
            "RenderPipeline"="UniversalPipeline"
        }
        LOD 100
        blend One OneMinusSrcAlpha

       

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

            sampler2D _DissolveTex;
            float4 _DissolveTex_ST;
            float _DissolveAmount;
            float _DissolveRange;
            float4 _DissolveColor;

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

            float remap (float value, float outMin)
            {
                return value * ((1 - outMin) / 1) + outMin;
            }

            float4 frag (Varyings i) : SV_Target
            {
                float4 col = _Color;

                float4 shadowCoord = TransformWorldToShadowCoord(i.posW);
                Light mainLight = GetMainLight(shadowCoord);
                float3 normal = normalize(i.normal);
                float3 lightDir = normalize(mainLight.direction);
                float diffuse = saturate(dot(normal, lightDir));
                col.rgb *= diffuse;

                col.rgb = MixFog(col.rgb, i.fogFactor);

                // Disolve
                float dissolve = tex2D(_DissolveTex, i.texcoord).r;

                _DissolveAmount = remap(_DissolveAmount, -_DissolveRange);

                if (dissolve < _DissolveAmount + _DissolveRange)
                {
                    col += _DissolveColor;
                }

                if (dissolve < _DissolveAmount)
                {
                    col.a = 0;
                }

                col.rgb *= col.a; 

                return col;
            }
            ENDHLSL
        }

        Pass
        {
            Tags { "LightMode"="ShadowCaster" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag

            #pragma multi_compile_instancing
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            // ShadowsCasterPass.hlsl に定義されているグローバルな変数
            float3 _LightDirection;
            
            float _LocalTime;
            float _Range;

            struct Attributes
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                //UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings {
                float4 pos : SV_POSITION;
            };

            Attributes vert(Attributes v)
            {
                v.normal = TransformObjectToWorldNormal(v.normal);
                return v;
            }

            Varyings OutVaryings(float3 wpos, float3 wnrm)
            {
                Varyings o;

                float3 positionWS = TransformObjectToWorld(wpos);
                float3 normalWS = TransformObjectToWorldNormal(wnrm);
                float4 positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, _LightDirection));
#if UNITY_REVERSED_Z
                positionCS.z = min(positionCS.z, positionCS.w * UNITY_NEAR_CLIP_VALUE);
#else
                positionCS.z = max(positionCS.z, positionCS.w * UNITY_NEAR_CLIP_VALUE);
#endif
                o.pos = positionCS;

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
                float3 wp0 = input[0].vertex.xyz;
                float3 wp1 = input[1].vertex.xyz;
                float3 wp2 = input[2].vertex.xyz;

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
                    temp.vertex.xyz += extNormal;
                    extPositions[j] = temp.vertex.xyz;
                    temp.normal = lerp(temp.normal, worldNormal, np);
                    outStream.Append(OutVaryings(extPositions[j], temp.normal));
                }

                outStream.RestartStrip(); // 新たに三角メッシュを生成する際に必要

                // 側面
                [unroll]
                for(int i = 0; i < 3; i++)
                {
                    float3 tempNormal = ConstructNormal(extPositions[i], input[i].vertex.xyz, extPositions[(i + 1) % 3]);
                    outStream.Append(OutVaryings(extPositions[i], worldNormal));
                    outStream.Append(OutVaryings(input[i].vertex.xyz, worldNormal));
                    outStream.Append(OutVaryings(extPositions[(i + 1) % 3], worldNormal));
                    outStream.Append(OutVaryings(input[(i + 1) % 3].vertex.xyz, worldNormal));
                    outStream.RestartStrip();
                }
            }

            float4 frag(Varyings i) : SV_Target
            {
                return 0.0;
            }

            ENDHLSL
        }
    }
}
