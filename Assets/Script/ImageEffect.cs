using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera)), ExecuteAlways, ImageEffectAllowedInSceneView]
public class ImageEffect : MonoBehaviour
{
    [SerializeField, Tooltip("エフェクトのマテリアル")]
    Material _effectMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Debug.Log("呼ばれた");
        Graphics.Blit(source, destination, _effectMaterial);
    }
}
