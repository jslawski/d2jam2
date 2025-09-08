using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager instance;

    private MeshRenderer _backgroundRenderer;

    private Texture2D[] _randomBackgrounds;

    private int _currentBackgroundIndex;

    private void Awake()
    {
        if (instance == null)
        { 
            instance = this;
        }

        this._randomBackgrounds = Resources.LoadAll<Texture2D>("Backgrounds");
        this._currentBackgroundIndex = Random.Range(0, this._randomBackgrounds.Length);

        this._backgroundRenderer = GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this._backgroundRenderer.material.mainTexture = this._randomBackgrounds[this._currentBackgroundIndex];
    }

    public void NextButtonPressed()
    {
        this._currentBackgroundIndex++;

        if (this._currentBackgroundIndex % this._randomBackgrounds.Length == 0)
        {
            this._currentBackgroundIndex = 0;
        }

        this._backgroundRenderer.material.mainTexture = this._randomBackgrounds[this._currentBackgroundIndex];
    }

    public void PrevButtonPressed()
    {
        this._currentBackgroundIndex--;

        if (this._currentBackgroundIndex < 0)
        {
            this._currentBackgroundIndex = this._randomBackgrounds.Length - 1;
        }

        this._backgroundRenderer.material.mainTexture = this._randomBackgrounds[this._currentBackgroundIndex];
    }

    public void SetBackgroundAtIndex(int index)
    {
        this._backgroundRenderer.material.mainTexture = this._randomBackgrounds[this._currentBackgroundIndex];
    }

    public int GetCurrentIndex()
    {
        return this._currentBackgroundIndex;
    }
}
