using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitController : MonoBehaviour, IEnemyHit
{
    Animator _anim;
    [SerializeField]
    BossController _controller;

    public void IsAttacked()
    {
        _anim.SetTrigger("Break");
        _controller.DecrementLife();

    }

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
    }
}
