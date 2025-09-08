using CabbageNetwork;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UploadVideoDataAsyncRequest : AsyncRequest
{
    public UploadVideoDataAsyncRequest(VideoData videoData, NetworkRequestSuccess successCallback = null, NetworkRequestFailure failureCallback = null)
    {
        string url = ServerSecrets.ServerName + "d2jam2/UploadVideoData.php";        

        this.form = new WWWForm();
        this.form.AddField("username", videoData.username);
        this.form.AddField("videoIndex", videoData.videoIndex);
        this.form.AddField("backgroundIndex", videoData.backgroundIndex);
        this.form.AddField("bgmIndex", videoData.bgmIndex);
        this.form.AddField("bgmSampleIndex", videoData.bgmSampleIndex);
        this.form.AddField("hairIndex", videoData.hairIndex);
        this.form.AddField("eyebrowsIndex", videoData.eyebrowsIndex);
        this.form.AddField("eyesIndex", videoData.eyesIndex);
        this.form.AddField("noseIndex", videoData.noseIndex);
        this.form.AddField("mouthIndex", videoData.mouthIndex);
        this.form.AddField("bodyTextureIndex", videoData.bodyTextureIndex);

        this.SetupRequest(url, successCallback, failureCallback);
    }
}
