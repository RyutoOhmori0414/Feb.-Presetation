using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTitleController : MonoBehaviour
{
    [SerializeField, Tooltip("���݂�VCam")]
    CinemachineVirtualCamera _currntVCam;
    [SerializeField, Tooltip("����VCam")]
    CinemachineVirtualCamera _nextVCam;
    [SerializeField]
    float _layLnegth = 100f;

    TitleState _state;

    void Start()
    {
        _state = TitleState.Start;
    }

    // Update is called once per frame
    void Update()
    {
        if (_state ==TitleState.Start && Input.anyKeyDown)
        {
            (_currntVCam.Priority, _nextVCam.Priority) = (_nextVCam.Priority, _currntVCam.Priority);
            _state = TitleState.Select;
            _currntVCam = _nextVCam;
        } // �^�C�g������X�e�[�W�I���ɑJ�ڂ���ۂ̏���
        else if (_state == TitleState.Select && Input.GetMouseButtonDown(0))
        {
            Vector3 targetPos = Input.mousePosition;
            targetPos.z = _layLnegth;
            targetPos = Camera.main.ScreenToWorldPoint(targetPos);

            RaycastHit hit;
            if (Physics.Linecast(Camera.main.transform.position, targetPos, out hit))
            {
                
            }
        }
    }

    enum TitleState
    {
        Start,
        Select,
    }
}
