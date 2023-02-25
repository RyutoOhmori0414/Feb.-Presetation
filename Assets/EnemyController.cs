using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void IsAttacked()
    {
        Debug.Log(name + transform.position.ToString());
    }
}
