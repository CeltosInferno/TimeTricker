using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Parent class of the SoundManagers
 * will read the mainVolume and playSounds according to it
 */
public class SoundManager : MonoBehaviour
{
    //the "mainVolume", set by the menu
    static protected float mainVolume;
    //what emits the sound
    public AudioSource audioSource;

    // Start is called before the first frame update
    protected virtual 
        void Start()
    {
        mainVolume = PlayerPrefs.GetFloat("MainVolume");
    }

    //given a sound, will play it once
    public void PlaySound(AudioClip soundClip)
    {
        audioSource.PlayOneShot(soundClip, mainVolume);
    }
}
