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
        // 色を反転させるAnimを呼ぶ
        _searchAnim.Play("PlayerSearchClip");
    }

    public void TransparentEnemy()
    {
        // すべての敵を透過させる処理
        Shader.EnableKeyword("_TRANSPARENT");

        foreach(var n in FindObjectsOfType<EnemyAnimController>())
        {
            n.Transparent();
        }
    }

    public void NormalEnemy()
    {
        // すべての敵の透過を戻す処理
        Shader.DisableKeyword("_TRANSPARENT");

        foreach (var n in FindObjectsOfType<EnemyAnimController>())
        {
            n?.Normal();
        }
    }

    private void OnDisable()
    {
        _gameController.Ability1 -= SearchAnimation;
    }
}
