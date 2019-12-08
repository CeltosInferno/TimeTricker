using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Simple Script used to display a wave
 */
public class WaveCounter : TextMeshProGuiBip
{
    public void SetWaveValue(int waveCount)
    {
        textMeshPro.text = "Wave : " + waveCount.ToString();
    }
}
