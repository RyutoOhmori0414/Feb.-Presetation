using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select1Controller : MonoBehaviour, ISelect
{
    [SerializeField]
    FadeAnimController _fadeAnimController;

    void ISelect.Select()
    {
        SceneManager.LoadScene("MapScene");
        _fadeAnimController.StartFade();
    }

}
