using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBars : MonoBehaviour
{
    public HealthBar slowBar;
    public HealthBar speedBar;

    public void setSize(float normalizedEnergy)
    {
        slowBar.SetSize(normalizedEnergy);
        speedBar.SetSize(1f - normalizedEnergy);
    }
}
