using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField]
    UnityEvent _onTransitionToBattle;
    [SerializeField]
    UnityEvent _onTransitionToMap;
    
    public void TransitonToBattle()
    {
        _onTransitionToBattle.Invoke();
    }

    public void TransitonToMap()
    {
        _onTransitionToMap.Invoke();
    }
}
