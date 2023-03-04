using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnim : BaseUIAnim
{
    [SerializeField]
    Texture2D _dissolveTex;
    [SerializeField, Range(0.0f, 1.0f)]
    float _dissolveAmount;
    [SerializeField, Range(0.0f, 1.0f)]
    float _dissolveRange;
    [SerializeField, ColorUsage(false, true)]
    Color _glawColor;
    [SerializeField]
    Shader _shader;

    int _dissolveTexId = Shader.PropertyToID("_DissolveTex");
    int _dissolveAmountId = Shader.PropertyToID("_DissolveAmount");
    int _dissolveRangeId = Shader.PropertyToID("_DissolveRange");
    int _glowColorId = Shader.PropertyToID("_DissolveColor");

    protected override void UpdateMaterial(Material baseMaterial)
    {
        if (!material)
        {
            material = new Material(_shader);
            material.CopyPropertiesFromMaterial(baseMaterial);
            material.hideFlags = HideFlags.HideAndDontSave;
        }

        material.SetTexture(_dissolveTexId, _dissolveTex);
        material.SetFloat(_dissolveAmountId, _dissolveAmount);
        material.SetFloat(_dissolveRangeId, _dissolveRange);
        material.SetColor(_glowColorId, _glawColor);
    }
}
