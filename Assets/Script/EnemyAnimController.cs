using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAnimController : MonoBehaviour
{
    MeshRenderer _meshRenderer;
    LocalKeyword _localKeyword;
    AudioSource _audioSource;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }


    public void RandamDiffusion()
    {
        _meshRenderer.material.EnableKeyword("_DIFFUSION");
        _audioSource.Play();
    }

    public void DestroyObj()
    {
        Destroy(transform.parent.gameObject);
    }

    public void Transparent()
    {

        _meshRenderer.material.SetFloat("_ZTest", (float)CompareFunction.Always);
    }

    public void Normal()
    {
        _meshRenderer.material.SetFloat("_ZTest", (float)CompareFunction.Less);
    }
}
