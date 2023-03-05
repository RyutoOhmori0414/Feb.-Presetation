using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTitleController : MonoBehaviour
{
    [SerializeField, Tooltip("現在のVCam")]
    CinemachineVirtualCamera _currntVCam;
    [SerializeField, Tooltip("次のVCam")]
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
        } // タイトルからステージ選択に遷移する際の処理
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
