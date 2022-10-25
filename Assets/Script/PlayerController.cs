using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("プレイヤーのスピード"), SerializeField]
    float _moveSpeed = 5f;

    Rigidbody _rb;
    SceneSwitcher _sceneSwitcher;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _sceneSwitcher = FindObjectOfType<SceneSwitcher>();
    }
    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = Vector3.forward * v + Vector3.right * h;
        dir = Camera.main.transform.TransformDirection(dir);
       dir.y = 0;
        _rb.velocity = dir.normalized * _moveSpeed + _rb.velocity.y * Vector3.up;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _sceneSwitcher.LoadButtleScene();
        }
    }
}
