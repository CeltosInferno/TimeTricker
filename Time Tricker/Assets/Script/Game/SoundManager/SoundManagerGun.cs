using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerGun : MonoBehaviour
{
    public AudioSource soundFire;
    public AudioClip fireClip;

    private float mainVolume;

    void Start()
    {
        mainVolume = PlayerPrefs.GetFloat("MainVolume");
    }

    // Update is called once per frame
    public void PlaySound()
    {
        soundFire.PlayOneShot(fireClip, mainVolume);
    }
}
