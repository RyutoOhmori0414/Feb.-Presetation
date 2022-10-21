using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : MonoBehaviour
{
    public static SingletonManager Instance;

    // 敵に遭遇した際に、Battleシーンに飛ぶ前の状態を保存しておく
    Vector3 _playerposition;
    Quaternion _playerrotation;

    private void Awake()
    {
        // インスタンスチェック
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
