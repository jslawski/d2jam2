using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputRecorder : MonoBehaviour
{
    public static InputRecorder instance;
    
    [SerializeField]
    private List<BodyPartController> _bodyPartControllers;

    private FrameInputSettings _currentFrameInputSettings;

    private KeyboardCommand _currentKeyboardCommand;
    private MouseCommand _currentMouseCommand;

    [SerializeField]
    private TextMeshProUGUI _recordingText;

    private bool _isRecording = false;

    private VideoData _currentVideoData;

    [SerializeField]
    private GameObject _createMenu;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (instance == null)
        {
            instance = this;
        }

        this._currentFrameInputSettings = new FrameInputSettings();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isRecording == false)
            {
                this.StartRecording();
            }            
        }
    }

    public void StartRecording()
    {
        RecordedData.CreateNewRecordedData(this._bodyPartControllers);

        this.SetupVideoData();

        StopAllCoroutines();

        StartCoroutine(this.UpdateCoroutine());

        this._recordingText.text = "RECORDING!";
        this._recordingText.color = Color.red;

        this._isRecording = true;

        Invoke("TakeScreenshot", 5.0f);
        Invoke("StopRecording", 10.0f);
    }

    private void TakeScreenshot()
    {
        ScreenshotManager.instance.TakeImmediateScreenshot();
    }

    private void SetupVideoData()
    { 
        this._currentVideoData = new VideoData();
        this._currentVideoData.username = PlayerPrefs.GetString("username", "test");
        this._currentVideoData.videoIndex = PlayerPrefs.GetInt("videoCount");
        this._currentVideoData.backgroundIndex = BackgroundManager.instance.GetCurrentIndex();
        this._currentVideoData.bgmIndex = MusicManager.instance.GetCurrentIndex();
        this._currentVideoData.bgmSampleIndex = MusicManager.instance.GetStartingSample();
        this._currentVideoData.hairIndex = PlayerPrefs.GetInt("hair");
        this._currentVideoData.eyebrowsIndex = PlayerPrefs.GetInt("eyebrows");
        this._currentVideoData.eyesIndex = PlayerPrefs.GetInt("eyes");
        this._currentVideoData.noseIndex = PlayerPrefs.GetInt("nose");
        this._currentVideoData.mouthIndex = PlayerPrefs.GetInt("mouth");
        this._currentVideoData.bodyTextureIndex = PlayerPrefs.GetInt("body");
    }

    public void StopRecording()
    {    
        StopAllCoroutines();

        //RecordedData.SaveDataToFile();

        this.UploadFileToDatabase();

        this._recordingText.text = "Recording File Saved at: " + Application.persistentDataPath;

        this._isRecording = false;

        PlayerPrefs.SetInt("videoCount", this._currentVideoData.videoIndex + 1);
    }

    private IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            this._currentKeyboardCommand = this.RecordKeyboardInput();
            this._currentFrameInputSettings = this._currentKeyboardCommand.Execute();

            this._currentMouseCommand = this.RecordMouseInput();
            this._currentFrameInputSettings = this._currentMouseCommand.Execute();

            RecordedData.AddFrameInputSettings(this._currentFrameInputSettings);

            yield return new WaitForFixedUpdate();
        }
    }

    private KeyboardCommand RecordKeyboardInput()
    {
        Dictionary<KeyCode, bool> heldKeysThisFrame = new Dictionary<KeyCode, bool>();

        for (int i = 0; i < this._bodyPartControllers.Count; i++)
        {
            if (Input.GetKey(this._bodyPartControllers[i].targetKeycode) == true)
            {
                heldKeysThisFrame.Add(this._bodyPartControllers[i].targetKeycode, true);
            }
            else
            {
                heldKeysThisFrame.Add(this._bodyPartControllers[i].targetKeycode, false);
            }
        }

        return new KeyboardCommand(new FrameInputSettings(this._currentFrameInputSettings), heldKeysThisFrame);
    }

    private MouseCommand RecordMouseInput()
    {
        Dictionary<KeyCode, Vector3> diffVectorsThisFrame = new Dictionary<KeyCode, Vector3>();

        for (int i = 0; i < this._bodyPartControllers.Count; i++)
        {
            diffVectorsThisFrame.Add(this._bodyPartControllers[i].targetKeycode, this._bodyPartControllers[i].GetDiffVector());
        }

        return new MouseCommand(new FrameInputSettings(this._currentFrameInputSettings), diffVectorsThisFrame);
    }

    private void UploadFileToDatabase()
    {
        UploadReplayDataAsyncRequest replayDataRequest = new UploadReplayDataAsyncRequest(PlayerPrefs.GetString("username", "test"), PlayerPrefs.GetInt("videoCount"), RecordedData.GetReplayDataString());
        replayDataRequest.Send();

        UploadVideoDataAsyncRequest videoDataRequest = new UploadVideoDataAsyncRequest(this._currentVideoData, this.UploadSuccess, this.UploadFailure);
        videoDataRequest.Send();
    }

    private void UploadSuccess(string data)
    {
        Invoke("ReturnToMenu", 1.0f);
    }

    private void ReturnToMenu()
    {
        MusicManager.instance.StopMusic();
        this._createMenu.SetActive(false);
    }

    private void UploadFailure()
    {
        Debug.LogError("Failed to upload files");
    }
}
