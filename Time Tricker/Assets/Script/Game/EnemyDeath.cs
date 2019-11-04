using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public AudioClip audioClipDeath;
    private float mainVolume;
    // Start is called before the first frame update
    void Start()
    {
        mainVolume = PlayerPrefs.GetFloat("MainVolume");
        GetComponent<AudioSource>().PlayOneShot(audioClipDeath, mainVolume);
    }
}
