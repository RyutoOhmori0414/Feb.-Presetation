using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �������񂱂��ŃV�[����ǂݍ��ޏ������������čl�̗]�n����
using UnityEngine.SceneManagement;

public class SingletonManager : MonoBehaviour
{
    public static SingletonManager Instance;

    // �G�ɑ��������ۂɁABattle�V�[���ɔ�ԑO�̏�Ԃ�ۑ����Ă���
    Transform _playerTransform;
    public Transform PlayerTransform
    {
        get => _playerTransform;
    }

    private void Awake()
    {
        // �C���X�^���X�`�F�b�N
        if (Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ToBattle(Transform pTransform)
    {
        // �}�b�v����o�g���ɑJ�ڂ���ۂɁA�ێ����Ă����f�[�^
        _playerTransform = pTransform;
        // ���݂̃v���C���[�̏��
        // �}�b�v�̏�Ԃ��ǂ��ɂ����ăo�g������߂����Ƃ��Ɏ���Ă����悤�ɂ���
        // �Վ��̃V�[�����[�h
        SceneManager.LoadScene("BattleScene");
    }

    /// <summary>�Վ��̃��\�b�h</summary>
    public void ToMap()
    {

    }
}
