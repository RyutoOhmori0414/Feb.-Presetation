using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AimingController : MonoBehaviour
{
    [SerializeField, Tooltip("攻撃判定を取るためのLayerMask")]
    LayerMask _layerMask;
    [SerializeField, Tooltip("Rayの目標のTransform")]
    Transform _targetTranform;
    [SerializeField, Tooltip("PlayerのTransform")]
    Transform _playerTransform;

    LineRenderer _lineRenderer;
    UIController _uiController;
    AudioSource _audioSource;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _uiController = FindObjectOfType<UIController>();
    }

    void Update()
    {
        RaycastHit hit;
        Debug.DrawLine(Camera.main.transform.position, _targetTranform.position, Color.cyan);
        bool hitCheck = Physics.Linecast(Camera.main.transform.position, _targetTranform.position, out hit) 
            && hit.collider.tag == "Enemy";

        if (hitCheck && Input.GetButtonDown("Fire1"))
        {
            _audioSource.Play();
            Debug.Log(hit.collider.name);
            var enemyController = hit.collider.gameObject.GetComponent<IEnemyHit>();
            enemyController?.IsAttacked();

            StartCoroutine(BulletShot(_playerTransform.position, hit.point));
        }
        else if (hitCheck)
        {
            _uiController.Aim();
        }
        else
        {
            _uiController.NotAim();
        }
    }

    IEnumerator BulletShot(Vector3 start, Vector3 end)
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);

        yield return new WaitForSeconds(2f);

        _lineRenderer.positionCount = 0;

        yield break;
    }
}
