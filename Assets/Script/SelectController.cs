using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SelectController : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCameraBase _vCam;

    public CinemachineVirtualCameraBase SwitchCamera(CinemachineVirtualCameraBase currentVCam)
    {
        (_vCam.Priority, currentVCam.Priority) = (currentVCam.Priority, _vCam.Priority);
        return _vCam;
    }
}
