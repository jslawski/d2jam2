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
        this.form.AddField("backgroundName", videoData.backgroundName);
        this.form.AddField("bgmName", videoData.bgmName);
        this.form.AddField("bgmSampleIndex", videoData.bgmSampleIndex);
        this.form.AddField("hairName", videoData.hairName);
        this.form.AddField("eyebrowsName", videoData.eyebrowsName);
        this.form.AddField("eyesName", videoData.eyesName);
        this.form.AddField("noseName", videoData.noseName);
        this.form.AddField("mouthName", videoData.mouthName);
        this.form.AddField("bodyTextureName", videoData.bodyTextureName);

        this.SetupRequest(url, successCallback, failureCallback);
    }
}
