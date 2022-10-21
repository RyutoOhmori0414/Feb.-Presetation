using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
