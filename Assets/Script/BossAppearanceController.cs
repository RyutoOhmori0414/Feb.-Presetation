using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppearanceController : MonoBehaviour
{
    GameController _gameController;
    AudioSource _audio;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
        _audio = GetComponent<AudioSource>();
    }

    public void PlaySE()
    {
        _audio.Play();
    }

    public void GameEnd()
    {
        _gameController.GameEnd();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
