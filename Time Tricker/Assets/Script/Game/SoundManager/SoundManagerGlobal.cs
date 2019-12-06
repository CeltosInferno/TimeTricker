using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerGlobal : MonoBehaviour
{
    public AudioSource MusicAudio;
    public AudioClip MusicClip;

    private float mainVolume;

    void Start()
    {
        mainVolume = PlayerPrefs.GetFloat("MainVolume");
        MusicAudio.clip = MusicClip;
        MusicAudio.volume = mainVolume;

        PlayLevelMusic();
    }

    // Update is called once per frame
    public void PlayLevelMusic()
    {
        MusicAudio.Play();
    }
}
