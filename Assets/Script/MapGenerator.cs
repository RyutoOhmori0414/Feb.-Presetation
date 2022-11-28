using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Tooltip("横方向の部屋を作る範囲のブロック数"), SerializeField]
    int _rawCount = 8;
    [Tooltip("縦方向の部屋を作る範囲ブロック数"), SerializeField]
    int _columnCount = 5;

    [Tooltip("まっすぐな道のプレハブ"), SerializeField]
    GameObject _straightAisle;
    [Tooltip("角の道のプレハブ"), SerializeField]
    GameObject _corner;
    [Tooltip("丁字路のプレハブ"), SerializeField]
    GameObject _tAisle;
    [Tooltip("十字路のプレハブ"), SerializeField]
    GameObject _cross;

    private void Start()
    {
        // x軸方向に通路を作る（行）
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
