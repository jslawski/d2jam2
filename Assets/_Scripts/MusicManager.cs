using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;    

    private AudioClip[] _randomTracks;

    [SerializeField]
    private AudioClip _menuMusic;

    private AudioSource _audioSource;

    private int _currentTrackIndex = 0;

    private void Awake()
    {
        this._randomTracks = Resources.LoadAll<AudioClip>("RandomMusic");
        this._audioSource = GetComponent<AudioSource>();

        this._currentTrackIndex = Random.Range(0, this._randomTracks.Length);

        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        this.PlayMenuMusic();
    }

    public void PlayMenuMusic()
    {
        if (this._audioSource.clip != this._menuMusic)
        {
            this._audioSource.Stop();
            this._audioSource.clip = this._menuMusic;
            this._audioSource.Play();
        }
    }

    public void StartBackgroundMusic()
    {
        this._audioSource.Stop();
        this._audioSource.clip = this._randomTracks[this._currentTrackIndex];
        this._audioSource.Play();
    }

    public void NextSongPressed()
    {
        this._audioSource.Stop();

        this._currentTrackIndex++;

        if (this._currentTrackIndex % this._randomTracks.Length == 0)
        {
            this._currentTrackIndex = 0;
        }

        this._audioSource.clip = this._randomTracks[this._currentTrackIndex];
        this._audioSource.Play();
    }

    public void PrevSongPressed()
    {
        this._audioSource.Stop();

        this._currentTrackIndex--;

        if (this._currentTrackIndex < 0)
        {
            this._currentTrackIndex = this._randomTracks.Length - 1;
        }

        this._audioSource.clip = this._randomTracks[this._currentTrackIndex];
        this._audioSource.Play();
    }

    public void SetSongIndex(int index)
    {
        this._currentTrackIndex = index;
    }

    public void SetSongSample(int startSample)
    {
        this._audioSource.timeSamples = startSample;
    }

    public void PlaySongAtIndex(int index)
    {
        this._audioSource.Stop();
        
        this._audioSource.clip = this._randomTracks[index];

        this._audioSource.Play();
    }

    public void PlaySongAtIndexAndSample(int index, int startSample)
    {
        this._audioSource.Stop();

        this._audioSource.clip = this._randomTracks[index];

        this._audioSource.Play();

        this._audioSource.timeSamples = startSample;
    }

    public void StopMusic()
    {
        this._audioSource.Stop();
    }
    public int GetCurrentIndex()
    {
        return this._currentTrackIndex;
    }

    public int GetStartingSample()
    {
        return this._audioSource.timeSamples;
    }
}
