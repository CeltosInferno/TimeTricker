﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerMonster : SoundManager
{
    //public AudioSource audioSource;
    public AudioClip audioClipScream1;
    public AudioClip audioClipScream2;

    public AudioClip audioClipHurt = null;

    //private float mainVolume;
    public float soundTimeMin;
    public float soundTimeMax;

    private bool isScreaming;

    // Start is called before the first frame update
    protected override 
        void Start()
    {
        base.Start();
        //mainVolume = PlayerPrefs.GetFloat("MainVolume");
        isScreaming = false;
    }

    public void playSoundHurt()
    {
        if(audioClipHurt)
            audioSource.PlayOneShot(audioClipHurt, mainVolume);
    }

    public IEnumerator Scream()
    {
        float time = Random.Range(soundTimeMin, soundTimeMax);
        yield return new WaitForSeconds(time);
        time = Random.Range(soundTimeMin, soundTimeMax);
        if (!isScreaming)
        {
            isScreaming = true;

            switch (Random.Range(0, 2))
            {
                case 0:
                    audioSource.PlayOneShot(audioClipScream1, mainVolume);
                    break;
                case 1:
                    audioSource.PlayOneShot(audioClipScream2, mainVolume);
                    break;
                default:
                    Debug.LogError("Monster scream - Error");
                    break;
            }

            yield return new WaitForSeconds(time);
            isScreaming = false;
        }
    }
}
