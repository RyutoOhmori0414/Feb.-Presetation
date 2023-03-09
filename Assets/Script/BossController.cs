using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField]
    int _currentLife;
    [SerializeField]
    Animator _anim;
    [SerializeField]
    CinemachineVirtualCameraBase _playerVCam;
    [SerializeField]
    CinemachineVirtualCameraBase _bossVCam;

    public void DecrementLife()
    {
        _currentLife--;

        if (_currentLife <= 0)
        {
            _anim.SetTrigger("Break");
            (_playerVCam.Priority, _bossVCam.Priority) = (_bossVCam.Priority, _playerVCam.Priority);
        }
    }
}
