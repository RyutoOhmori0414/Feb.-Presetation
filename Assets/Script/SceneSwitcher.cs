using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadButtleScene()
    {
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Additive);
    }

    public void UnloadButtleScene()
    {
        SceneManager.UnloadSceneAsync("BattleScene");
    }
}
