using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class represent a chrono, it displays a time, given
 * a value
 * It does not update the time on its own !
 */
public class Chrono : TextMeshProGuiBip
{
    public Color alertColor = Color.red;
    public Color spawningColor = Color.yellow;

    //value in seconds
    public void setTimeText(float value)
    {
        System.TimeSpan time = System.TimeSpan.FromSeconds((double) value);
        //string minutes = (value / 60f).ToString("d2");
        //string seconds = (value % 60f).ToString("d2");
        textMeshPro.text = time.ToString("mm':'ss");
    }

    public void AlertMode()
    {
        bippingFrequency = 2f;
        setBippingColor(alertColor);
        setBip(true);
    }

    public void SpawningMode()
    {
        bippingFrequency = 1f;
        setBippingColor(spawningColor);
        setBip(true);
    }

    public void DefaultMode()
    {
        setBip(false);
    }
}
