using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField]
    UnityEvent _onTransitionToButtle;
    [SerializeField]
    UnityEvent _onTransitionToMap;
    
    public void TransitonToButtle()
    {
        _onTransitionToButtle.Invoke();
    }

    public void TransitonToMap()
    {
        _onTransitionToMap.Invoke();
    }
}
