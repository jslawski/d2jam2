using CabbageNetwork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetReplayDataAsyncRequest : AsyncRequest
{
    public GetReplayDataAsyncRequest(string username, int videoIndex, NetworkRequestSuccess successCallback = null, NetworkRequestFailure failureCallback = null)
    {
        string filename = username + videoIndex.ToString() + ".txt";

        string url = ServerSecrets.ServerName + "d2jam2/replays/" + filename;

        this.form = new WWWForm();
        
        this.SetupRequest(url, successCallback, failureCallback);
    }
}
