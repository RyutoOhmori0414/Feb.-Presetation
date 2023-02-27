using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera)), ExecuteAlways, ImageEffectAllowedInSceneView]
public class ImageEffect : MonoBehaviour
{
    [SerializeField, Tooltip("�G�t�F�N�g�̃}�e���A��")]
    Material _effectMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Debug.Log("�Ă΂ꂽ");
        Graphics.Blit(source, destination, _effectMaterial);
    }
}
