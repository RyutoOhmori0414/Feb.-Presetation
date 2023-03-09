using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimCotroller : MonoBehaviour
{
    MainTitleController _mainTitleController;

    private void Awake()
    {
        _mainTitleController = FindObjectOfType<MainTitleController>();
    }

    void AnimEnd()
    {
        _mainTitleController.TextAnimEnd();
    }
}
