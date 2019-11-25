using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerSpawner : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClipSpawn;

    private float mainVolume;

    private bool isScreaming;

    // Start is called before the first frame update
    void Start()
    {
        mainVolume = PlayerPrefs.GetFloat("MainVolume");
    }

    public void PlaySpawn()
    {
        audioSource.PlayOneShot(audioClipSpawn, mainVolume);
    }

}
