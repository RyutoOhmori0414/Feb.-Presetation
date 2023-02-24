using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera)), ExecuteAlways, ImageEffectAllowedInSceneView]
public class ImageEffect : MonoBehaviour
{
    public Material EffectMaterial;

    // 後でシェーダのプロパティ名を書く
    private int _directionId = Shader.PropertyToID("");
    private int _bloomTexId = Shader.PropertyToID("");

}
