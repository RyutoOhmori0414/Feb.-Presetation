using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    /// <summary>
    /// シーンを追加で読み込む
    /// </summary>
    public void LoadButtleScene()
    {
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Additive);
    }

    /// <summary>
    /// 追加したシーンを削除する
    /// </summary>
    public void UnloadButtleScene()
    {
        SceneManager.UnloadSceneAsync("BattleScene");
    }
}
