using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [Tooltip("バトルシーンのバーチャルカメラ"), SerializeField]
    CinemachineVirtualCamera _buttleVirtualCamera;

    public void CameraTransitionToButtle()
    {
        _buttleVirtualCamera.Priority += 10;
    }

    public void CameraTransitionToMap()
    {
        _buttleVirtualCamera.Priority -= 10;
    }
}
