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

    float colorChangeSpeed = 0.2f;
    public Color intenseFast;
    public Color intenseSlow;

    Color normalFast;
    Color normalSlow;

    public ColorChanger speedBarCol;
    public ColorChanger slowBarCol;

    public void Start()
    {
        normalFast = speedBarCol.spriteRenderer.color;
        normalSlow = slowBarCol.spriteRenderer.color;
    }

    /*
     * Given a percentage of the bar, adjust the sprite
     */
    public void setSize(float normalizedEnergy)
    {
        slowBar.SetSize(normalizedEnergy);
        speedBar.SetSize(1f - normalizedEnergy);
    }

    /*
     * Change the sprite color and add particles 
     * when the speed power is activated
     */
    public void speedVisualEffect(bool activate = true)
    {
        if (activate)
            speedBarCol.changeColor(intenseFast, colorChangeSpeed);
        else
            speedBarCol.changeColor(normalFast, colorChangeSpeed);
    }

    /*
     * Change the sprite color and add particles 
     * when the slow power is activated
     */
    public void slowVisualEffect(bool activate = true)
    {
        if(activate)
            slowBarCol.changeColor(intenseSlow, colorChangeSpeed);
        else
            slowBarCol.changeColor(normalSlow, colorChangeSpeed);
    }

    public void normalVisualEffect()
    {
        speedBarCol.changeColor(normalFast, colorChangeSpeed);
        slowBarCol.changeColor(normalSlow, colorChangeSpeed);
    }
}
