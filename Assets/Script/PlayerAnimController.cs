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
        // F‚ğ”½“]‚³‚¹‚éAnim‚ğŒÄ‚Ô
        _searchAnim.Play("PlayerSearchClip");
    }

    public void TransparentEnemy()
    {
        // ‚·‚×‚Ä‚Ì“G‚ğ“§‰ß‚³‚¹‚éˆ—
        Shader.EnableKeyword("_TRANSPARENT");

        foreach(var n in _enemyAnimControllers)
        {
            n.Transparent();
        }
    }

    public void NormalEnemy()
    {
        // ‚·‚×‚Ä‚Ì“G‚Ì“§‰ß‚ğ–ß‚·ˆ—
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
