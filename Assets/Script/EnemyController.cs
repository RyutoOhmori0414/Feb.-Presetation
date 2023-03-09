using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour, IEnemyHit
{
    Animator _anim;
    GameController _controller;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _controller = FindObjectOfType<GameController>();
        _controller.IncrementEnemy();
    }

    private void OnEnable()
    {
        _controller.Ability1 += Ability1;
    }

    public void IsAttacked()
    {
        // 破壊されるアニメーションを呼ぶ
        Debug.Log(name + transform.position.ToString());
        _anim.SetTrigger("Break");
    }

    void Ability1()
    {
        // 敵がSearchされているアニメーションを呼ぶ
        //_anim.SetTrigger("Search");
    }

    private void OnDisable()
    {
        _controller.Ability1 -= Ability1;
    }

    private void OnDestroy()
    {
        _controller.DecrementEnemy();
    }
}
