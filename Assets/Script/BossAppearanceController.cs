using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppearanceController : MonoBehaviour
{
    GameController _gameController;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    public void GameEnd()
    {
        _gameController.GameEnd();
    }
}
