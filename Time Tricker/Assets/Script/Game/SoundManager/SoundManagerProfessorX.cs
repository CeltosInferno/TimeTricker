using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerProfessorX : MonoBehaviour
{
    public AudioSource JumpAudioSound;
    public AudioSource MoveAudioSound;

    public AudioClip jumpClip;
    public AudioClip moveClip;
    public AudioClip hurtClip;
    public AudioClip deadClip;

    private float mainVolume;

    void Start()
    {
        mainVolume = PlayerPrefs.GetFloat("MainVolume");

        MoveAudioSound.clip = moveClip;
        MoveAudioSound.volume = mainVolume;
    }

    // Update is called once per frame
    public void PlaySoundJump()
    {
        JumpAudioSound.PlayOneShot(jumpClip, mainVolume);
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
        JumpAudioSound.PlayOneShot(hurtClip, mainVolume);
    }

    public void PlaySoundDead()
    {
        JumpAudioSound.PlayOneShot(deadClip, mainVolume);
        Debug.Log("DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD");
    }
}
