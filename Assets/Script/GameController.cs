using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int _currentEnemyCount = 0;

    /// <summary>アビリティ１使用時に、呼ばれるEvent</summary>
    Action _ability1;

    /// <summary>アビリティ１使用時に、呼ばれるEvent</summary>
    public Action Ability1
    {
        get => _ability1;
        set => _ability1 = value;
    }

    private void Update()
    {
        // アビリティ1のボタンを押した際の処理
        if (Input.GetButtonDown("Ability1"))
        {
            _ability1.Invoke();
            Debug.Log("Q");
        }
    }

    /// <summary>Enemyの数を増やす</summary>
    public void IncrementEnemy()
    {
        _currentEnemyCount++;
    }

    /// <summary>Enemyの数を減らす</summary>
    public void DecrementEnemy()
    {
        _currentEnemyCount--;

        // 敵が0になったらBossのシーンに飛ぶ
    }
}
