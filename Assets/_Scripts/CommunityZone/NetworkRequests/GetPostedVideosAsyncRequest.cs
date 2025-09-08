using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CabbageNetwork;

public class GetPostedVideosAsyncRequest : AsyncRequest
{
    public GetPostedVideosAsyncRequest(int pageNum, int numEntries, string sortType, NetworkRequestSuccess successCallback = null, NetworkRequestFailure failureCallback = null)
    {
        string url = ServerSecrets.ServerName + "d2jam2/GetPostedVideos.php";

        this.form = new WWWForm();
        this.form.AddField("pageNum", pageNum);
        this.form.AddField("numEntries", numEntries);
        this.form.AddField("sortType", sortType);

        this.SetupRequest(url, successCallback, failureCallback);
    }
}
