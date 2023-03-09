using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInformationController : MonoBehaviour, ISelect
{
    [SerializeField]
    Animator _anim;
    MainTitleController _controller;

    private void Start()
    {
        _controller = FindObjectOfType<MainTitleController>();
    }
    public void Select()
    {
        _anim.Play("OpenInformation");
        _controller.State = MainTitleController.TitleState.Selecting;
    }
}
