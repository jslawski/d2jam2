using CabbageNetwork;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UploadThumbnailAsyncRequest : AsyncRequest
{
    public UploadThumbnailAsyncRequest(string username, int videoId, Texture2D thumbnailTexture, NetworkRequestSuccess successCallback = null, NetworkRequestFailure failureCallback = null)
    {
        string url = ServerSecrets.ServerName + "d2jam2/UploadThumbnail.php";

        string fileName = username + videoId.ToString();

        this.form = new WWWForm();
        this.form.AddField("thumbnailFileName", fileName);
        this.form.AddBinaryData("thumbnailData", thumbnailTexture.EncodeToPNG(), fileName);

        this.SetupRequest(url, successCallback, failureCallback);
    }
}
