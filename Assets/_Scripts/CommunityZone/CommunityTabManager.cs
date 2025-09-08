using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunityTabManager : MonoBehaviour
{
    private int _currentPageNum = 0;    

    private int _maxNumVideosToRequest = 15;

    private PostedVideosData _currentPostedVideos;

    private VideoCard[] _videoCards;

    [SerializeField]
    private Button _pageIncrementButton;
    [SerializeField]
    private Button _pageDecrementButton;

    private void Awake()
    {
        this._videoCards = GetComponentsInChildren<VideoCard>(true);
    }

    public void LoadTopVideos()
    {
        GetPostedVideosAsyncRequest postedVideosRequest = new GetPostedVideosAsyncRequest(this._currentPageNum, this._maxNumVideosToRequest, "DESC", this.GetPostedVideosSuccess, this.GetPostedVideosFailure);
        postedVideosRequest.Send();
    }

    public void LoadNewestVideos()
    {
        GetPostedVideosAsyncRequest postedVideosRequest = new GetPostedVideosAsyncRequest(this._currentPageNum, this._maxNumVideosToRequest, "ASC", this.GetPostedVideosSuccess, this.GetPostedVideosFailure);
        postedVideosRequest.Send();
    }

    public void LoadRandomVideos()
    {
        GetPostedVideosAsyncRequest postedVideosRequest = new GetPostedVideosAsyncRequest(0, this._maxNumVideosToRequest, "RAND", this.GetPostedVideosSuccess, this.GetPostedVideosFailure);
        postedVideosRequest.Send();
    }

    private void GetPostedVideosSuccess(string data)
    {
        //Empty leaderboard, return
        if (data == "[]")
        {
            return;
        }

        this._currentPostedVideos = JsonUtility.FromJson<PostedVideosData>(data);

        this.UpdateAllVideoCards();
        this.UpdatePageButtons();
    }

    private void GetPostedVideosFailure()
    {
        Debug.LogError("ERROR: Unable to get posted videos");
    }

    private void UpdateAllVideoCards()
    {
        for (int i = 0; i < this._currentPostedVideos.entries.Count; i++)
        {
            this._videoCards[i].UpdateVideoCard(this._currentPostedVideos.entries[i]);
        }
    }

    private void UpdatePageButtons()
    {
        if (this._currentPostedVideos.entries.Count < this._maxNumVideosToRequest)
        {
            this._pageIncrementButton.interactable = false;
        }
        else
        {
            this._pageIncrementButton.interactable = true;
        }

        if (this._currentPageNum == 0)
        {
            this._pageDecrementButton.interactable = false;
        }
        else
        {
            this._pageDecrementButton.interactable = true;
        }
    }

    public void IncrementPageNum()
    {        
        this._currentPageNum++;
    }

    public void DecrementPageNum()
    {
        if (this._currentPageNum > 0)
        {
            this._currentPageNum--;
        }
    }    
}
