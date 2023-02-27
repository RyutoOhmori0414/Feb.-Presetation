using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField, Tooltip("Ability1のアイコンのSpriteのGameObj")]
    GameObject _ability1SpriteGObj;

    [SerializeField, Tooltip("AbilityのアイコンIndexが残り使用可能回数")]
    Sprite[] _abilitySprite;

    Image _ability1Image;
    Animator _ability1SpriteAnim;

    GameController _controller;
    private void Awake()
    {
        _controller = FindObjectOfType<GameController>();
    }

    private void OnEnable()
    {
        _controller.Ability1 += UIAbility1;
    }

    void Start()
    {
        _ability1SpriteAnim = _ability1SpriteGObj.GetComponent<Animator>();
        _ability1Image = _ability1SpriteGObj.GetComponent<Image>();
    }

    void UIAbility1()
    {
        _ability1SpriteAnim.Play("IconExcute");
        _ability1Image.sprite = _abilitySprite[_controller.AbilityConnt];
    }

    private void OnDisable()
    {
        _controller.Ability1 -= UIAbility1;
    }
}
