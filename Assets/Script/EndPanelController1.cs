using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndPanelController1 : MonoBehaviour
{
    GameController _controller;

    List<UIBehaviour> _uIBehaviours;
    Animator _anim;

    private void Awake()
    {
        _controller = FindObjectOfType<GameController>();
        _controller.GameEnd += OpenPanel;
        _uIBehaviours = new List<UIBehaviour>(GetComponentsInChildren<UIBehaviour>());
        _anim = GetComponent<Animator>();

        foreach(var n in _uIBehaviours)
        {
            n.enabled = false;
        }
    }

    void OpenPanel()
    {
        _anim.Play("Clear");
        foreach(var n in _uIBehaviours)
        {
            n.enabled = true;
        }
    }
}
