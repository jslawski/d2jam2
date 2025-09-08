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

    [SerializeField]
    private GameObject _replayParent;

    [SerializeField]
    private GameObject _backgroundImage;

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
        this._backgroundImage.SetActive(false);
    }

    public void EnableCard()
    {
        this._backgroundImage.SetActive(true);
    }

    public void LoadReplayData(string username, int videoIndex)
    {
        GetReplayDataAsyncRequest replayDataRequest = new GetReplayDataAsyncRequest(username, videoIndex, this.LoadReplayDataSuccess, this.LoadReplayDataFailure);
        replayDataRequest.Send();
    }

    public void LoadThumbnailData(string username, int videoIndex)
    {
        StartCoroutine(this.RequestThumbnailTexture(username, videoIndex));
    }

    private void LoadReplayDataSuccess(string data)
    {        
        File.WriteAllText(Application.persistentDataPath + "\\" + this._replayData.videoData.username + this._replayData.videoData.videoIndex + ".txt", data, Encoding.UTF8);

        StreamReader fileReader = new StreamReader(Application.persistentDataPath + "\\" + this._replayData.videoData.username + this._replayData.videoData.videoIndex + ".txt");

        this._replayData.SetupReplayData(fileReader);

        fileReader.Close();
    }

    private void LoadReplayDataFailure()
    {
        Debug.LogError("ERROR: Unable to load replay data");
    }

    private IEnumerator RequestThumbnailTexture(string username, int videoIndex)
    {
        string url = ServerSecrets.ServerName + "d2jam2/thumbnails/" + username + videoIndex.ToString() + ".png";
        
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

    public void ButtonClicked()
    {
        ReplaySimulator.instance.SetupReplay(this._replayData);
        this._replayParent.SetActive(true);
        ReplaySimulator.instance.StartReplay();
    }
}
