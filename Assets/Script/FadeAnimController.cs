using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimController : MonoBehaviour
{
    Animator _animator;
    Image _image;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _image = GetComponent<Image>();
        _image.enabled = false;
    }

    public void StartFade()
    {
        StartCoroutine(ScreenShot());
    }

    public void EffectEnd()
    {
        _image.enabled = false;
    }

    IEnumerator ScreenShot()
    {
        yield return new WaitForEndOfFrame();

        var tex = new Texture2D(Screen.width, Screen.height);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();

        _image.enabled = true;
        _image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        _animator.Play("Dissolve");
    }
}
