using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSwitcher : MonoBehaviour
{
    public void ToMap()
    {
        SingletonManager.Instance.ToMap();
    }
}
