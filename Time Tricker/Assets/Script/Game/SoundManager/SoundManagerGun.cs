using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerGun : SoundManager
{
    //public AudioSource audioSource;
    public AudioClip fireClip;

    //private float mainVolume;

    //void Start()
    //{
    //    mainVolume = PlayerPrefs.GetFloat("MainVolume");
    //}

    // Update is called once per frame
    public void PlaySound()
    {
        audioSource.PlayOneShot(fireClip, mainVolume);
    }
}
