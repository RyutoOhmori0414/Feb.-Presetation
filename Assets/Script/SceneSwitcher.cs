using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    /// <summary>
    /// �V�[����ǉ��œǂݍ���
    /// </summary>
    public void LoadButtleScene()
    {
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Additive);
    }

    /// <summary>
    /// �ǉ������V�[�����폜����
    /// </summary>
    public void UnloadButtleScene()
    {
        SceneManager.UnloadSceneAsync("BattleScene");
    }
}
