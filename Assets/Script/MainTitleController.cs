using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTitleController : MonoBehaviour
{
    [SerializeField, Tooltip("現在のVCam")]
    CinemachineVirtualCameraBase _currntVCam;
    [SerializeField, Tooltip("次のVCam")]
    CinemachineVirtualCameraBase _nextVCam;
    [SerializeField]
    float _layLnegth = 100f;
    [SerializeField]
    Animator _textAnim;

    TitleState _state;
    public TitleState State
    {
        get => _state;
        set => _state = value;
    }
    CinemachineVirtualCameraBase _selectVCam;

    void Start()
    {
        _state = TitleState.Start;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (_state ==TitleState.Start && Input.anyKeyDown)
        {
            _textAnim.SetTrigger("End");
        } // タイトルからステージ選択に遷移する際の処理
        else if (_state == TitleState.Select && Input.GetMouseButtonDown(0))
        {
            Vector3 targetPos = Input.mousePosition;
            targetPos.z = _layLnegth;
            targetPos = Camera.main.ScreenToWorldPoint(targetPos);

            RaycastHit hit;
            if (Physics.Linecast(Camera.main.transform.position, targetPos, out hit))
            {
                _selectVCam = hit.collider.GetComponent<SelectController>()?.SwitchCamera(_currntVCam);

                if(_selectVCam)
                {
                    _state = TitleState.Selected;
                }
            }
        }
        else if (_state == TitleState.Selected)
        {
            if (Input.GetButtonDown("Back"))
            {
                (_selectVCam.Priority, _currntVCam.Priority) = (_currntVCam.Priority, _selectVCam.Priority);
                _state = TitleState.Select;
                _selectVCam = null;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                Vector3 targetPos = Input.mousePosition;
                targetPos.z = _layLnegth;
                targetPos = Camera.main.ScreenToWorldPoint(targetPos);

                RaycastHit hit;
                if (Physics.Linecast(Camera.main.transform.position, targetPos, out hit))
                {
                    var temp = hit.collider.GetComponent<ISelect>();

                    if (temp != null)
                    {
                        temp.Select();
                    } // 選んだ際の処理
                }
            }
        }
    }

    public void TextAnimEnd()
    {
        (_currntVCam.Priority, _nextVCam.Priority) = (_nextVCam.Priority, _currntVCam.Priority);
        _state = TitleState.Select;
        _currntVCam = _nextVCam;
    }

    public enum TitleState
    {
        Start,
        Select,
        Selected,
        Selecting
    }
}
