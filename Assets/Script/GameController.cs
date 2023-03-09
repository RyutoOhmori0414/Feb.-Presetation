using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField, Tooltip("�A�r���e�B�̎g�p�\��")]
    int _abilityCount = 3;
    [SerializeField, Tooltip("BossStage�̃X�|�[���n�_")]
    Transform _bossStageStart;

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

    /// <summary>�G�̃J�E���g</summary>
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
        // �A�r���e�B1�̃{�^�����������ۂ̏���
        if (Input.GetButtonDown("Ability1") && _abilityCount > 0)
        {
            _abilityCount--;
            _ability1.Invoke();
            Debug.Log("Q");
            _audioSource.Play();
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
