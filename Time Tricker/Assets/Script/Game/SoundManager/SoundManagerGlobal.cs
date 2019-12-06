using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerGlobal : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip MusicClip;

    private float mainVolume;

    void Start()
    {
        mainVolume = PlayerPrefs.GetFloat("MainVolume");
        audioSource.clip = MusicClip;
        audioSource.volume = mainVolume;

        PlayLevelMusic();
    }

    // Update is called once per frame
    public void PlayLevelMusic()
    {
        audioSource.Play();
    }
}
