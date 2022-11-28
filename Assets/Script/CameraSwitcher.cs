using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [Tooltip("バトルシーンのバーチャルカメラ"), SerializeField]
    CinemachineVirtualCamera _battleVirtualCamera;

    public void CameraTransitionToBattle()
    {
        _battleVirtualCamera.Priority += 10;
    }

    public void CameraTransitionToMap()
    {
        _battleVirtualCamera.Priority -= 10;
    }
}
