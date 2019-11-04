using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveVolume : MonoBehaviour
{
    private float volume;

    private void Start()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("MainVolume");
    }

    void Update()
    {
        volume = GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("MainVolume", volume);
    }
}
