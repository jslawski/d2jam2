using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MouseImage : MonoBehaviour
{

    private Image _image;

    private void Awake()
    {
        this._image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || 
        Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C))
        {
            if (this._image.enabled == false)
            {
                this._image.enabled = true;
                Invoke("DisableObject", 15.0f);
            }
        }
    }

    private void DisableObject()
    {
        this.gameObject.SetActive(false);
    }
}
