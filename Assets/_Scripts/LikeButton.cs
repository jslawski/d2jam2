using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LikeButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        this._button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        this._button.interactable = true;
    }

    public void LikeButtonPressed()
    {
        SendLikeAsyncRequest request = new SendLikeAsyncRequest(ReplaySimulator.instance._replayData.videoData.username, ReplaySimulator.instance._replayData.videoData.videoIndex);
        request.Send();

        this._button.interactable = false;
    }
}
