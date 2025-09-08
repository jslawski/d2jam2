using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject _createTab;
    [SerializeField]
    private GameObject _replayTab;

    public void CommunityClicked()
    {
        ReplaySimulator.instance.CleanupReplay();
        CommunityTabManager.instance.LoadTopVideos();
        this._createTab.SetActive(false);
        this._replayTab.SetActive(false);
        MusicManager.instance.PlayMenuMusic();
    }

    public void MyContentClicked()
    {
        ReplaySimulator.instance.CleanupReplay();
        CommunityTabManager.instance.LoadMyVideos();
        this._createTab.SetActive(false);
        this._replayTab.SetActive(false);
        MusicManager.instance.PlayMenuMusic();
    }

    public void CreateClicked()
    {
        ReplaySimulator.instance.CleanupReplay();
        MusicManager.instance.StartBackgroundMusic();

        CharacterGenerator.instance.LoadCharacterFromPlayerPrefs();

        this._createTab.SetActive(true);
        this._replayTab.SetActive(false);
    }
}
