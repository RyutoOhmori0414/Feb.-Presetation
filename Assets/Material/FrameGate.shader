Shader "Unlit/FrameGate"
{
    Properties
    {
        _Stencil ("Stencil ID", Float) = 0
    }
    SubShader
    {
        Tags 
        {
            "RenderType"="Opaque"
            "RenderQueue"="Transparent"
            "RenderPipeline"="UniversalPipeline"
        }
        LOD 100
        Blend Zero One
        ZWrite Off

        Pass
        {
            Stencil
            {
                Ref [_Stencil]
                Comp Always
                Pass Replace
                Fail Keep
            }
        }
    }
}
