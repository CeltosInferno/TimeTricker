using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerProfessorX : MonoBehaviour
{
    public AudioSource JumpSound;
    public AudioSource MoveSound;

    public AudioClip jumpClip;
    public AudioClip moveClip;

    private float mainVolume;

    void Start()
    {
        mainVolume = PlayerPrefs.GetFloat("MainVolume");

        MoveSound.clip = moveClip;
        MoveSound.volume = mainVolume;
    }

    // Update is called once per frame
    public void PlaySoundJump()
    {
        JumpSound.PlayOneShot(jumpClip, mainVolume);
    }

    public void PlaySoundMove()
    {
        MoveSound.Play();
    }
    public void StopSoundMove()
    {
        MoveSound.Stop();
    }
}
