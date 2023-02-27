using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField, Tooltip("�A�r���e�B�̎g�p�\��")]
    int _abilityCount = 3;

    public int AbilityConnt
    { get => _abilityCount; }

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
        if (Input.GetButtonDown("Ability1") && _abilityCount > 0)
        {
            _abilityCount--;
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
