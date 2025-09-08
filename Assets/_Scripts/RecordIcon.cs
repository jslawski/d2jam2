using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordIcon : MonoBehaviour
{
    private Image _image;

    private bool _recordHit = false;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (this._recordHit == false)
            {
                this._image.rectTransform.DOScale(1.1f, 0.5f).SetLoops(20, LoopType.Yoyo);
                this._recordHit = true;
            }
        }
    }

}
