using CabbageNetwork;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UploadReplayDataAsyncRequest : AsyncRequest
{
    public UploadReplayDataAsyncRequest(string username, int videoId, string replayDataString, NetworkRequestSuccess successCallback = null, NetworkRequestFailure failureCallback = null)
    {
        string url = ServerSecrets.ServerName + "d2jam2/UploadReplayData.php";

        string fileName = username + videoId.ToString();

        this.form = new WWWForm();
        this.form.AddField("replayFileName", fileName);
        this.form.AddBinaryData("replayData", Encoding.UTF8.GetBytes(replayDataString));        

        this.SetupRequest(url, successCallback, failureCallback);
    }
}
