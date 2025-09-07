using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ReplaySimulator : MonoBehaviour
{
    public static ReplaySimulator instance;    

    private int _currentFrameInputSettingIndex;

    private BodyPartController[] _bodyPartControllers;

    private Dictionary<KeyCode, Vector3> _initialPositions;
    private Dictionary<KeyCode, Vector3> _initialRotations;
    private List<FrameInputSettings> _frameInputSettings;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        this._initialPositions = new Dictionary<KeyCode, Vector3>();
        this._initialRotations = new Dictionary<KeyCode, Vector3>();
        this._frameInputSettings = new List<FrameInputSettings>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {                   
            this.LoadReplayData();
            this.SetupReplay();
        }
    }

    private void LoadReplayData()
    {
        StreamReader fileReader = new StreamReader(Application.persistentDataPath + "/testFilename.txt");

        this.LoadInitialPositions(fileReader);
        this.LoadInitialRotations(fileReader);
    }

    private void LoadInitialPositions(StreamReader fileReader)
    {
        string initialPositionsString = fileReader.ReadLine();
        string[] initialPositionValues = initialPositionsString.Split(",");

        Vector3 headInitialPosition = new Vector3(float.Parse(initialPositionValues[0]), float.Parse(initialPositionValues[1]), float.Parse(initialPositionValues[2]));
        Vector3 rightArmInitialPosition = new Vector3(float.Parse(initialPositionValues[3]), float.Parse(initialPositionValues[4]), float.Parse(initialPositionValues[5]));
        Vector3 spineInitialPosition = new Vector3(float.Parse(initialPositionValues[6]), float.Parse(initialPositionValues[7]), float.Parse(initialPositionValues[8]));
        Vector3 leftArmInitialPosition = new Vector3(float.Parse(initialPositionValues[9]), float.Parse(initialPositionValues[10]), float.Parse(initialPositionValues[11]));
        Vector3 rightLegInitialPosition = new Vector3(float.Parse(initialPositionValues[12]), float.Parse(initialPositionValues[13]), float.Parse(initialPositionValues[14]));
        Vector3 hipsInitialPosition = new Vector3(float.Parse(initialPositionValues[15]), float.Parse(initialPositionValues[16]), float.Parse(initialPositionValues[17]));
        Vector3 leftLegInitialPosition = new Vector3(float.Parse(initialPositionValues[18]), float.Parse(initialPositionValues[19]), float.Parse(initialPositionValues[20]));

        this._initialPositions.Add(KeyCode.W, headInitialPosition);
        this._initialPositions.Add(KeyCode.A, rightArmInitialPosition);
        this._initialPositions.Add(KeyCode.S, spineInitialPosition);
        this._initialPositions.Add(KeyCode.D, leftArmInitialPosition);
        this._initialPositions.Add(KeyCode.Z, rightLegInitialPosition);
        this._initialPositions.Add(KeyCode.X, hipsInitialPosition);
        this._initialPositions.Add(KeyCode.C, leftLegInitialPosition);
    }

    private void LoadInitialRotations(StreamReader fileReader)
    {
        string initialRotationsString = fileReader.ReadLine();
        string[] initialRotationValues = initialRotationsString.Split(",");

        Vector3 headInitialRotation = new Vector3(float.Parse(initialRotationValues[0]), float.Parse(initialRotationValues[1]), float.Parse(initialRotationValues[2]));
        Vector3 rightArmInitialRotation = new Vector3(float.Parse(initialRotationValues[3]), float.Parse(initialRotationValues[4]), float.Parse(initialRotationValues[5]));
        Vector3 spineInitialRotation = new Vector3(float.Parse(initialRotationValues[6]), float.Parse(initialRotationValues[7]), float.Parse(initialRotationValues[8]));
        Vector3 leftArmInitialRotation = new Vector3(float.Parse(initialRotationValues[9]), float.Parse(initialRotationValues[10]), float.Parse(initialRotationValues[11]));
        Vector3 rightLegInitialRotation = new Vector3(float.Parse(initialRotationValues[12]), float.Parse(initialRotationValues[13]), float.Parse(initialRotationValues[14]));
        Vector3 hipsInitialRotation = new Vector3(float.Parse(initialRotationValues[15]), float.Parse(initialRotationValues[16]), float.Parse(initialRotationValues[17]));
        Vector3 leftLegInitialRotation = new Vector3(float.Parse(initialRotationValues[18]), float.Parse(initialRotationValues[19]), float.Parse(initialRotationValues[20]));

        this._initialRotations.Add(KeyCode.W, headInitialRotation);
        this._initialRotations.Add(KeyCode.A, rightArmInitialRotation);
        this._initialRotations.Add(KeyCode.S, spineInitialRotation);
        this._initialRotations.Add(KeyCode.D, leftArmInitialRotation);
        this._initialRotations.Add(KeyCode.Z, rightLegInitialRotation);
        this._initialRotations.Add(KeyCode.X, hipsInitialRotation);
        this._initialRotations.Add(KeyCode.C, leftLegInitialRotation);
    }

    private void SetupReplay()
    {
        this.SetupBodyPartControllersForReplay();
        this.SetInitialTransforms();
    }

    public void StartReplay()
    { 
    
    }

    private void SetInitialTransforms()
    {
        for (int i = 0; i < this._bodyPartControllers.Length; i++)
        {
            this._bodyPartControllers[i].SetInitialTransform(this._initialPositions[this._bodyPartControllers[i].targetKeycode], this._initialRotations[this._bodyPartControllers[i].targetKeycode]);
        }
    }

    private void SetupBodyPartControllersForReplay()
    {
        this._bodyPartControllers = GetComponentsInChildren<BodyPartController>();

        for (int i = 0; i < this._bodyPartControllers.Length; i++)
        {
            this._bodyPartControllers[i].isReplay = true;
        }
    }

    public Vector3 GetReplayDiffVector(KeyCode targetKeyCode)
    {
        return this._frameInputSettings[this._currentFrameInputSettingIndex].diffVectors[targetKeyCode];
    }

    public float GetReplayNormalizedX(KeyCode targetKeyCode, float maxViewportDiff)
    {
        return (this._frameInputSettings[this._currentFrameInputSettingIndex].diffVectors[targetKeyCode].x / maxViewportDiff);
    }

    public float GetReplayNormalizedY(KeyCode targetKeyCode, float maxViewportDiff)
    {
        return (this._frameInputSettings[this._currentFrameInputSettingIndex].diffVectors[targetKeyCode].y / maxViewportDiff);
    }
}
