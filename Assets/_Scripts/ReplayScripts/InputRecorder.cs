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
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (_isRecording == true)
            {
                this.StopRecording();
            }
            else
            {
                this.StartRecording();
            }
        }
    }

    public void StartRecording()
    {
        RecordedData.CreateNewRecordedData(this._bodyPartControllers);

        StopAllCoroutines();

        StartCoroutine(this.UpdateCoroutine());

        this._recordingText.text = "RECORDING!";
        this._recordingText.color = Color.red;

        this._isRecording = true;

        Invoke("StopRecording", 10.0f);
    }

    public void StopRecording()
    {
        Debug.LogError("Stopping Recording");
    
        StopAllCoroutines();

        RecordedData.SaveDataToFile();

        this._recordingText.text = "Recording File Saved at: " + Application.persistentDataPath;

        this._isRecording = false;
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
}
