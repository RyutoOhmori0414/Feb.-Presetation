using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera)), ExecuteAlways, ImageEffectAllowedInSceneView]
public class ImageEffect : MonoBehaviour
{
    public Material EffectMaterial;

    // ��ŃV�F�[�_�̃v���p�e�B��������
    private int _directionId = Shader.PropertyToID("");
    private int _bloomTexId = Shader.PropertyToID("");

}
