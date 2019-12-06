using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SoundManager used to play musics, owned by thr camera
 */
public class SoundManagerGlobal : SoundManager
{
    //public AudioSource audioSource;
    public AudioClip MusicClip;

    //private float mainVolume;

    protected override 
        void Start()
    {
        base.Start();
        //mainVolume = PlayerPrefs.GetFloat("MainVolume");
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
