using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAnimController : MonoBehaviour
{
    MainTitleController _cotroller;

    private void Start()
    {
        _cotroller = FindObjectOfType<MainTitleController>();
    }

    void CloseEvent()
    {
        _cotroller.State = MainTitleController.TitleState.Selected;
    }
}
