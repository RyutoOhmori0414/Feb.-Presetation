using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int _currentEnemyCount = 0;

    /// <summary>�A�r���e�B�P�g�p���ɁA�Ă΂��Event</summary>
    Action _ability1;

    /// <summary>�A�r���e�B�P�g�p���ɁA�Ă΂��Event</summary>
    public Action Ability1
    {
        get => _ability1;
        set => _ability1 = value;
    }

    private void Update()
    {
        // �A�r���e�B1�̃{�^�����������ۂ̏���
        if (Input.GetButtonDown("Ability1"))
        {
            _ability1.Invoke();
            Debug.Log("Q");
        }
    }

    /// <summary>Enemy�̐��𑝂₷</summary>
    public void IncrementEnemy()
    {
        _currentEnemyCount++;
    }

    /// <summary>Enemy�̐������炷</summary>
    public void DecrementEnemy()
    {
        _currentEnemyCount--;

        // �G��0�ɂȂ�����Boss�̃V�[���ɔ��
    }
}
