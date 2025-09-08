using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunityTabManager : MonoBehaviour
{
    private int _currentPageNum = 0;    

    private int _maxNumVideosToRequest = 15;

    private PostedVideosData _currentPostedVideos;

    private VideoCard[] _videoCards;

    private void Awake()
    {
        this._videoCards = GetComponentsInChildren<VideoCard>(true);
    }

    public void LoadTopVideos()
    {
        GetPostedVideosAsyncRequest leaderboardRequest = new GetPostedVideosAsyncRequest(this._currentPageNum, this._maxNumVideosToRequest, "DESC", this.GetPostedVideosSuccess, this.GetPostedVideosFailure);
        leaderboardRequest.Send();
    }

    public void LoadNewestVideos()
    {
        GetPostedVideosAsyncRequest leaderboardRequest = new GetPostedVideosAsyncRequest(this._currentPageNum, this._maxNumVideosToRequest, "ASC", this.GetPostedVideosSuccess, this.GetPostedVideosFailure);
        leaderboardRequest.Send();
    }

    public void LoadRandomVideos()
    {
        GetPostedVideosAsyncRequest leaderboardRequest = new GetPostedVideosAsyncRequest(0, this._maxNumVideosToRequest, "RAND", this.GetPostedVideosSuccess, this.GetPostedVideosFailure);
        leaderboardRequest.Send();
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

    public void IncrementPageNum()
    {
        //Add Logic to not increment past the max page num (once we know it)
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
