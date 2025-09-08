using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI _timerText;    

    private bool _timerStarted;

    private float _timeLeft = 10.0f;
    
    [SerializeField]
    private GameObject _endingAnimation;
    [SerializeField]
    private GameObject _createModeParent;

    private void Awake()
    {
        this._timerText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        this._timeLeft = 10.0f;
        this._timerStarted = false;
        this._timerText.text = "10s";
        this._endingAnimation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (this._timerStarted == false)
            { 
                this._timerStarted = true;
                StartCoroutine(this.RunTimer());
            }
        }
    }

    private IEnumerator RunTimer()
    {
        while (this._timeLeft >= 0.0f)
        {
            this._timerText.text = Mathf.CeilToInt(this._timeLeft).ToString() + "s";
            this._timeLeft -= Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        this._timerText.text = "0s";
        this._endingAnimation.SetActive(true);
    }
}
