using CabbageNetwork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendLikeAsyncRequest : AsyncRequest
{
    public SendLikeAsyncRequest(string username, int videoIndex, NetworkRequestSuccess successCallback = null, NetworkRequestFailure failureCallback = null)
    {        
        string url = ServerSecrets.ServerName + "d2jam2/SendLike.php";

        this.form = new WWWForm();
        this.form.AddField("username", username);
        this.form.AddField("videoIndex", videoIndex);

        this.SetupRequest(url, successCallback, failureCallback);
    }
}
