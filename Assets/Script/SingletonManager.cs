using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �������񂱂��ŃV�[����ǂݍ��ޏ������������čl�̗]�n����
using UnityEngine.SceneManagement;

public class SingletonManager : MonoBehaviour
{
    public static SingletonManager Instance;

    // �G�ɑ��������ۂɁABattle�V�[���ɔ�ԑO�̏�Ԃ�ۑ����Ă���
    Vector3 _playerposition;
    Quaternion _playerrotation;

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
        _playerposition = pTransform.position;
        _playerrotation = pTransform.rotation;
        // ���݂̃v���C���[�̏��
        // �}�b�v�̏�Ԃ��ǂ��ɂ����ăo�g������߂����Ƃ��Ɏ���Ă����悤�ɂ���
        // �Վ��̃V�[�����[�h
        SceneManager.LoadScene("BattleScene");
    }

    /// <summary>�Վ��̃��\�b�h</summary>
    public void ToMap()
    {
        SceneManager.LoadScene("MapScene");
        SceneManager.sceneLoaded += PlayerTransformChange;
    }

    void PlayerTransformChange(Scene a, LoadSceneMode b)
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        go.transform.position = _playerposition;
        go.transform.rotation = _playerrotation;
        SceneManager.sceneLoaded -= PlayerTransformChange;
    }
}
