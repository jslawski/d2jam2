using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ReplaySimulator : MonoBehaviour
{
    public static ReplaySimulator instance;    

    private int _currentFrameInputSettingIndex;

    private BodyPartController[] _bodyPartControllers;

    private ReplayData _replayData;

    private bool _replayClosed = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        this._bodyPartControllers = GetComponentsInChildren<BodyPartController>();
    }

    public void CleanupReplay()
    {
        for (int i = 0; i < this._bodyPartControllers.Length; i++)
        {
            this._bodyPartControllers[i].ResetBodyPart();
        }

        this._replayClosed = true;
    }

    public void SetupReplay(ReplayData replayData)
    {
        this._replayClosed = false;
        this._replayData = replayData;
        BackgroundManager.instance.SetBackgroundAtIndex(this._replayData.videoData.backgroundIndex);
        MusicManager.instance.SetSongIndex(this._replayData.videoData.bgmIndex);
        MusicManager.instance.SetSongSample(this._replayData.videoData.bgmSampleIndex);
        this.SetupBodyPartControllersForReplay();
        this.SetInitialTransforms();
    }

    private void SetupCharacter()
    {
        CharacterProfile replayCharacter = new CharacterProfile();

        replayCharacter.textureIndex = this._replayData.videoData.bodyTextureIndex;
        replayCharacter.hairIndex = this._replayData.videoData.hairIndex;
        replayCharacter.eyebrowsIndex = this._replayData.videoData.eyebrowsIndex;
        replayCharacter.eyesIndex = this._replayData.videoData.eyesIndex;
        replayCharacter.noseIndex = this._replayData.videoData.noseIndex;
        replayCharacter.mouthIndex = this._replayData.videoData.mouthIndex;

        CharacterGenerator.instance.LoadCharacterFromProfile(replayCharacter);
    }

    private void RestartReplay()
    {
        this.SetInitialTransforms();
        this.StartReplay();
    }

    public void StartReplay()
    {
        MusicManager.instance.PlaySongAtIndexAndSample(this._replayData.videoData.bgmIndex, this._replayData.videoData.bgmSampleIndex);
        StartCoroutine(this.SimulateReplay());
    }

    private IEnumerator SimulateReplay()
    {
        for (this._currentFrameInputSettingIndex = 0; this._currentFrameInputSettingIndex < this._replayData.frameInputSettings.Count; this._currentFrameInputSettingIndex++)
        {
            for (int i = 0; i < this._bodyPartControllers.Length; i++)
            {
                if (this._replayData.frameInputSettings[this._currentFrameInputSettingIndex].heldKeys[this._bodyPartControllers[i].targetKeycode] == true)
                { 
                    this._bodyPartControllers[i].SimulateReplay();
                }
            }

            yield return new WaitForFixedUpdate();
        }

        if (this._replayClosed == false)
        {
            this.RestartReplay();
        }
    }

    private void SetInitialTransforms()
    {
        for (int i = 0; i < this._bodyPartControllers.Length; i++)
        {
            this._bodyPartControllers[i].SetInitialTransform(this._replayData.initialPositions[this._bodyPartControllers[i].targetKeycode], this._replayData.initialRotations[this._bodyPartControllers[i].targetKeycode]);
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
        return this._replayData.frameInputSettings[this._currentFrameInputSettingIndex].diffVectors[targetKeyCode];
    }

    public float GetReplayNormalizedX(KeyCode targetKeyCode, float maxViewportDiff)
    {
        return Mathf.Abs(this._replayData.frameInputSettings[this._currentFrameInputSettingIndex].diffVectors[targetKeyCode].x / maxViewportDiff);
    }

    public float GetReplayNormalizedY(KeyCode targetKeyCode, float maxViewportDiff)
    {
        return Mathf.Abs(this._replayData.frameInputSettings[this._currentFrameInputSettingIndex].diffVectors[targetKeyCode].y / maxViewportDiff);
    }
}
