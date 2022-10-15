using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("プレイヤーのスピード"), SerializeField]
    float _moveSpeed = 5f;

    Rigidbody _rb;
    SingletonManager _singletonManager;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _singletonManager = FindObjectOfType<SingletonManager>();

        if (_singletonManager.PlayerTransform)
        {
            this.transform.position = _singletonManager.PlayerTransform.position;
            this.transform.rotation = _singletonManager.PlayerTransform.rotation;
        }
    }
    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = Vector3.forward * v + Vector3.right * h;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = _rb.velocity.y;
        _rb.velocity = dir * _moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _singletonManager.ToBattle(transform);
        }
    }
}
