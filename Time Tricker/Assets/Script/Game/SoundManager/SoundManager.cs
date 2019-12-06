using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //the "mainVolume"
    static protected float mainVolume;
    //what emits the sound
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        mainVolume = PlayerPrefs.GetFloat("MainVolume");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
