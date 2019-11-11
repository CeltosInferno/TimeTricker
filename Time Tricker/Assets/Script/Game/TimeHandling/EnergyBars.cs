using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The Energy Renderer
 */
public class EnergyBars : MonoBehaviour
{
    public HealthBar slowBar;
    public HealthBar speedBar;

    /*
     * Given a percentage of the bar, adjust the sprite
     */
    public void setSize(float normalizedEnergy)
    {
        slowBar.SetSize(normalizedEnergy);
        speedBar.SetSize(1f - normalizedEnergy);
    }
}
