using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Animator _searchAnim;
    GameController _gameController;

    List<EnemyAnimController> _enemyAnimControllers = new List<EnemyAnimController>();

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
        _searchAnim = GetComponent<Animator>();
        _enemyAnimControllers.AddRange(FindObjectsOfType<EnemyAnimController>());
    }

    private void OnEnable()
    {
        _gameController.Ability1 += SearchAnimation;
    }

    void SearchAnimation()
    {
        // �F�𔽓]������Anim���Ă�
        _searchAnim.Play("PlayerSearchClip");
    }

    public void TransparentEnemy()
    {
        // ���ׂĂ̓G�𓧉߂����鏈��
        Shader.EnableKeyword("_TRANSPARENT");

        foreach(var n in _enemyAnimControllers)
        {
            n.Transparent();
        }
    }

    public void NormalEnemy()
    {
        // ���ׂĂ̓G�̓��߂�߂�����
        Shader.DisableKeyword("_TRANSPARENT");

        foreach (var n in _enemyAnimControllers)
        {
            n.Normal();
        }
    }

    private void OnDisable()
    {
        _gameController.Ability1 -= SearchAnimation;
    }
}
