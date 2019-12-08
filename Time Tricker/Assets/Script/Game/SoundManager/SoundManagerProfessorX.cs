using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerProfessorX : SoundManager
{
    //public AudioSource audioSource;
    public AudioSource MoveAudioSound;

    public AudioClip jumpClip;
    public AudioClip moveClip;
    public AudioClip hurtClip;
    public AudioClip deadClip;

    //private float mainVolume;

    protected override
        void Start()
    {
        base.Start();
        //mainVolume = PlayerPrefs.GetFloat("MainVolume");

        MoveAudioSound.clip = moveClip;
        MoveAudioSound.volume = mainVolume;
    }

    // Update is called once per frame
    public void PlaySoundJump()
    {
        audioSource.PlayOneShot(jumpClip, mainVolume);
    }

    public void PlaySoundMove()
    {
        MoveAudioSound.Play();
    }
    public void StopSoundMove()
    {
        MoveAudioSound.Stop();
    }

    public void PlaySoundHurt()
    {
        audioSource.PlayOneShot(hurtClip, mainVolume);
    }

    public void PlaySoundDead()
    {
        audioSource.PlayOneShot(deadClip, mainVolume);
    }
}
