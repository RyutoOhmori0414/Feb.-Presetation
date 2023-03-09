using System;
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

    [SerializeField]
    FadeAnimController _fadeAnimCon;

    [SerializeField, Tooltip("Sight")]
    Image[] _sights;
    [SerializeField, Tooltip("狙っている際の色")]
    Color _aimingColor;
    [SerializeField, Tooltip("狙っていないときの色")]
    Color _notAimingColor;

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
        _controller.ToBossStage += ToBossStage;
        _controller.GameEnd += UIGameEnd;
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

    void ToBossStage()
    {
        _fadeAnimCon.StartFade();
        RenderSettings.fog = false;
    }

    public void Aim()
    {
        // Sightの色を変える
        if (_sights[0].color != _aimingColor)
        {
            foreach(var s in _sights)
            {
                s.color = _aimingColor;
            }
        }
    }

    public void NotAim()
    {
        if (_sights[0].color != _notAimingColor)
        {
            foreach (var s in _sights)
            {
                s.color = _notAimingColor;
            }
        }
    }

    private void OnDisable()
    {
        _controller.Ability1 -= UIAbility1;
        _controller.ToBossStage -= ToBossStage;
        _controller.GameEnd -= UIGameEnd;
    }

    void UIGameEnd()
    {

    }
}
