using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// いったんここでシーンを読み込む処理を書くが再考の余地あり
using UnityEngine.SceneManagement;

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

    public void ToBattle(Transform pTransform)
    {
        // マップからバトルに遷移する際に、保持しておくデータ
        _playerposition = pTransform.position;
        _playerrotation = pTransform.rotation;
        // 現在のプレイヤーの状態
        // マップの状態をどうにかしてバトルから戻ったときに取っておくようにする
        // 臨時のシーンロード
        SceneManager.LoadScene("BattleScene");
    }

    /// <summary>臨時のメソッド</summary>
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
