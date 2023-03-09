using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField, Tooltip("アビリティの使用可能回数")]
    int _abilityCount = 3;
    [SerializeField, Tooltip("BossStageのスポーン地点")]
    Transform _bossStageStart;

    public int AbilityConnt
    { get => _abilityCount; }

    int _currentEnemyCount = 0;

    /// <summary>アビリティ１使用時に、呼ばれるEvent</summary>
    Action _ability1;
    /// <summary>アビリティ１使用時に、呼ばれるEvent</summary>
    public Action Ability1
    {
        get => _ability1;
        set => _ability1 = value;
    }

    /// <summary>敵のカウント</summary>
    Action _toBossStage;
    public Action ToBossStage
    {
        get => _toBossStage;
        set => _toBossStage = value;
    }

    Action _gameEnd;

    public Action GameEnd
    {
        get => _gameEnd;
        set => _gameEnd = value;
    }

    Transform _playerTransform;
    AudioSource _audioSource;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _audioSource = GetComponent<AudioSource>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // アビリティ1のボタンを押した際の処理
        if (Input.GetButtonDown("Ability1") && _abilityCount > 0)
        {
            _abilityCount--;
            _ability1.Invoke();
            Debug.Log("Q");
            _audioSource.Play();
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
        if (_currentEnemyCount <= 0)
        {
            _toBossStage();
            _playerTransform.position = _bossStageStart.position;
        }
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
