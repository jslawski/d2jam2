using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Text;
using UnityEngine.Networking;

public class VideoCard : MonoBehaviour
{
    [SerializeField]
    private RawImage _thumbnailImage;
    [SerializeField]
    private Image _likesImage;
    [SerializeField]
    private TextMeshProUGUI _usernameLabel;
    [SerializeField]
    private TextMeshProUGUI _likesCountLabel;

    private ReplayData _replayData;

    private void Awake()
    {
        this._replayData = new ReplayData();
    }

    public void UpdateVideoCard(VideoData videoData)
    {
        this._replayData.videoData = videoData;
        this._usernameLabel.text = "@" + videoData.username;
        this._likesCountLabel.text = videoData.numLikes.ToString();

        this.LoadReplayData(videoData.username, videoData.videoIndex);
        this.LoadThumbnailData(videoData.username, videoData.videoIndex);

        this.gameObject.SetActive(true);
    }

    public void DisableCard()
    {
        this.gameObject.SetActive(false);
    }

    public void LoadReplayData(string username, int videoIndex)
    {
        GetReplayDataAsyncRequest replayDataRequest = new GetReplayDataAsyncRequest(username, videoIndex, this.LoadReplayDataSuccess, this.LoadReplayDataFailure);
        replayDataRequest.Send();
    }

    public void LoadThumbnailData(string username, int videoIndex)
    {
        StartCoroutine(this.RequestThumbnailTexture());
    }

    private void LoadReplayDataSuccess(string data)
    {        
        File.WriteAllText(Application.persistentDataPath + "\\testFilename.txt", data, Encoding.UTF8);

        StreamReader fileReader = new StreamReader(Application.persistentDataPath + "\\testFilename.txt");

        this._replayData.SetupReplayData(fileReader);        
    }

    private void LoadReplayDataFailure()
    {
        Debug.LogError("ERROR: Unable to load replay data");
    }

    private IEnumerator RequestThumbnailTexture()
    {
        string url = ServerSecrets.ServerName + "d2jam2/thumbnails/" + PlayerPrefs.GetString("username", "test") + this._replayData.videoData.videoIndex.ToString() + ".png";

        Debug.LogError("URL: " + url);
        
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Texture2D downloadedTexture = DownloadHandlerTexture.GetContent(request);
            this._thumbnailImage.texture = downloadedTexture;
        }
    }
}
