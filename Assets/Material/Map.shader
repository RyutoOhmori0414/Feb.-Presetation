Shader "Unlit/MyUnlit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [Enum(UnityEngine.Rendering.CompareFunction)]
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        [Enum(UnityEngine.Rendering.StencilOp)]
        _StencilOp ("Stencil Operation", Float) = 0
        }

    SubShader
    {
        Tags {
            "Queue" = "Geometry+100"
            "RenderType"="Opaque"
            "RenderPipeline"="UniversalPipeline"
        }
        LOD 100

        
        
        // �ePass��cbuffer���ς��Ȃ��悤�ɂ����ɒ�`����
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        // �}�N���Œ�`���Ă���
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);

        CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_ST;
        CBUFFER_END
        ENDHLSL
        


        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            Stencil
            {
                Ref [_Stencil]
                Comp [_StencilComp]
                Pass [_StencilOp]
            }


            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            
            // URP�ŉe�����邽�߂ɕK�v�ȃL�[���[�h�Q
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float fogFactor: TEXCOORD1;
                float3 posWS : TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.fogFactor = ComputeFogFactor(o.vertex.z);
                o.posWS = TransformObjectToWorld(v.vertex.xyz);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                
                float4 shadowCoord = TransformWorldToShadowCoord(i.posWS);
                Light mainLight = GetMainLight(shadowCoord);
                half shadow = mainLight.shadowAttenuation;
                Light addLight0 = GetAdditionalLight(0, i.posWS);
                shadow *= addLight0.shadowAttenuation;
                col.rgb *= shadow;
                
                col.rgb = MixFog(col.rgb, i.fogFactor);
                return col;
            }
            ENDHLSL
        }
    
//        Pass
//        {
//            Tags { "LightMode"="ShadowCaster" }

//            HLSLPROGRAM
//            #pragma vertex vert
//            #pragma fragment frag

//            #pragma multi_compile_instancing
            
//            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

//            // ShadowsCasterPass.hlsl �ɒ�`����Ă���O���[�o���ȕϐ�
//            float3 _LightDirection;
            
//            struct appdata
//            {
//                float4 vertex : POSITION;
//                float3 normal : NORMAL;
//                UNITY_VERTEX_INPUT_INSTANCE_ID
//            };

//            struct v2f {
//                float4 pos : SV_POSITION;
//            };

//            v2f vert(appdata v)
//            {
//                UNITY_SETUP_INSTANCE_ID(v);
//                v2f o;
//                // ShadowsCasterPass.hlsl �� GetShadowPositionHClip() ���Q�l��
//                float3 positionWS = TransformObjectToWorld(v.vertex.xyz);
//                float3 normalWS = TransformObjectToWorldNormal(v.normal);
//                float4 positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, _LightDirection));
//#if UNITY_REVERSED_Z
//                positionCS.z = min(positionCS.z, positionCS.w * UNITY_NEAR_CLIP_VALUE);
//#else
//                positionCS.z = max(positionCS.z, positionCS.w * UNITY_NEAR_CLIP_VALUE);
//#endif
//                o.pos = positionCS;

//                return o;
//            }

//            float4 frag(v2f i) : SV_Target
//            {
//                return 0.0;
//            }

//            ENDHLSL
//        }
    }
}