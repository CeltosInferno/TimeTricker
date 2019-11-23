using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerMonster : MonoBehaviour
{
    public AudioSource soundScream;
    public AudioClip audioClipScream1;
    public AudioClip audioClipScream2;

    private float mainVolume;
    public float soundTimeMin;
    public float soundTimeMax;

    private bool isScreaming;

    // Start is called before the first frame update
    void Start()
    {
        mainVolume = PlayerPrefs.GetFloat("MainVolume");
        isScreaming = false;
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
                    soundScream.PlayOneShot(audioClipScream1, mainVolume);
                    break;
                case 1:
                    soundScream.PlayOneShot(audioClipScream2, mainVolume);
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
