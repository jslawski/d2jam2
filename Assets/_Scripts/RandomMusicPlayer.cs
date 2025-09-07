using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusicPlayer : MonoBehaviour
{
    private AudioClip[] _randomTracks;

    private AudioSource _audioSource;

    private int _currentTrackIndex = 0;

    [SerializeField]
    private List<BodyPartController> _bodyPartControllers;

    private void Awake()
    {
        this._randomTracks = Resources.LoadAll<AudioClip>("RandomMusic");
        this._audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            this.GoToNextSong();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            this.ResetRagdoll();
        }
    }

    private void GoToNextSong()
    {
        this._currentTrackIndex++;

        if (this._currentTrackIndex % this._randomTracks.Length == 0)
        {
            this._currentTrackIndex = 0;
        }

        this._audioSource.Stop();
        this._audioSource.clip = this._randomTracks[this._currentTrackIndex];
        this._audioSource.Play();
    }

    private void ResetRagdoll()
    {
        for (int i = 0; i < this._bodyPartControllers.Count; i++)
        {
            this._bodyPartControllers[i].ResetBodyPart();
        }
    }
}
