using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Tooltip("�������̕��������͈͂̃u���b�N��"), SerializeField]
    int _rawCount = 8;
    [Tooltip("�c�����̕��������͈̓u���b�N��"), SerializeField]
    int _columnCount = 5;

    [Tooltip("�܂������ȓ��̃v���n�u"), SerializeField]
    GameObject _straightAisle;
    [Tooltip("�p�̓��̃v���n�u"), SerializeField]
    GameObject _corner;
    [Tooltip("�����H�̃v���n�u"), SerializeField]
    GameObject _tAisle;
    [Tooltip("�\���H�̃v���n�u"), SerializeField]
    GameObject _cross;

    private void Start()
    {
        // x�������ɒʘH�����i�s�j
        for (int i = 0; i < _rawCount; i++)
        {
            if (i % 3 == 0)
            {
                GameObject temp = Instantiate(_straightAisle);
                temp.transform.position = new Vector3(10 * i, 0, 0);
            }
        }
    }
}
