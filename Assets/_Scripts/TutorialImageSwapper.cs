using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialImageSwapper : MonoBehaviour
{
    [SerializeField]
    private Image _tutorialImage;
    [SerializeField]
    private Sprite _nominalSprite;
    [SerializeField]
    private Sprite _pressedSprite;   
    [SerializeField]
    private KeyCode _keyCode;

    private void Update()
    {
        if (Input.GetKey(this._keyCode))
        {
            this._tutorialImage.sprite = this._pressedSprite;
        }
        else
        {
            this._tutorialImage.sprite = this._nominalSprite;
        }
    }
}
