using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AimingController : MonoBehaviour
{
    [SerializeField, Tooltip("çUåÇîªíËÇéÊÇÈÇΩÇﬂÇÃLayerMask")]
    LayerMask _layerMask;
    [SerializeField, Tooltip("RayÇÃñ⁄ïWÇÃTransform")]
    Transform _targetTranform;
    [SerializeField, Tooltip("PlayerÇÃTransform")]
    Transform _playerTransform;

    LineRenderer _lineRenderer;
    UIController _uiController;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
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
            Debug.Log(hit.collider.name);
            var enemyController = hit.collider.gameObject.GetComponent<EnemyController>();
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
