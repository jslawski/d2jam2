using CabbageNetwork;
using UnityEngine;

public class DeletePostedVideoAsyncRequest : AsyncRequest
{
    public DeletePostedVideoAsyncRequest(string username, int videoIndex, NetworkRequestSuccess successCallback = null, NetworkRequestFailure failureCallback = null)
    {
        string url = ServerSecrets.ServerName + "d2jam2/DeleteVideoData.php";

        this.form = new WWWForm();
        this.form.AddField("username", username);
        this.form.AddField("videoIndex", videoIndex);        

        this.SetupRequest(url, successCallback, failureCallback);
    }
}
