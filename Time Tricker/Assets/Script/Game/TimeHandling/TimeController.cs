using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script used to choose which button change the time and how
//Should be linked to the player and should have the gestion of energy in the future
public class TimeController : MonoBehaviour
{
    public float SlowValue = 0.5f;
    public float SpeedValue = 2f;

    //value of the energy bars
    public float totalPower = 300f;
    //cost to activate a power
    public float activatePowerCost = 15f;
    //cost of the power for each second
    public float costPerSeconds = 10f;

    //the sprite and script of the energy bars
    public EnergyBars energyBars;
    //the background color when a power is used
    public ColorChanger backgroundRenderer;
    public Color speedColor;
    public Color slowColor;
    public float colorChangeTime = 1f;

    //current energy value (half of the totalPower)
    float currentEnergy;

    bool isSlowingDowm = false;
    bool isSpeedingUp = false;

    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = totalPower / 2;
    }

    // Update is called once per frame
    void Update()
    {
        InputGestion();
        CalculateEnergy();
        energyBars.setSize(currentEnergy / totalPower);
    }

    /*
     * Called once per frame to adjust the energy bars when
     * a power is used
     * When the bar is empty the powers are disactivated
     */
    void CalculateEnergy()
    {
        float cost = costPerSeconds * Time.deltaTime;
        if (isSlowingDowm) { 
            currentEnergy = Mathf.Max(0f, currentEnergy - cost);
        }
        else if (isSpeedingUp) { 
            currentEnergy = Mathf.Min(totalPower, currentEnergy + cost);
        }
        if (currentEnergy <= 0f || currentEnergy >= totalPower)
        {
            disactivatePower();
        }
    }

    /*
     * Should called when disactivating a power
     */
    void disactivatePower()
    {
        TimeManager.globalTimeMultiplier = 1f;
        backgroundRenderer.changeColor(Color.clear, colorChangeTime);
        energyBars.normalVisualEffect();
        isSpeedingUp = false;
        isSlowingDowm = false;
    }

    /*
     * Should called when acivating a power, 
     * does not verify if it is allowed
     * speedUp = true -> acceleration
     * speedUp = false -> acceleration
     */
    void activatePower(bool speedUp)
    {
        if(speedUp)
        {
            TimeManager.globalTimeMultiplier = SpeedValue;
            isSpeedingUp = true;
            isSlowingDowm = false;
            currentEnergy += activatePowerCost;
            backgroundRenderer.changeColor(speedColor, colorChangeTime);
            energyBars.speedVisualEffect();
            energyBars.slowVisualEffect(false);
        }
        else
        {
            TimeManager.globalTimeMultiplier = SlowValue;
            isSpeedingUp = false;
            isSlowingDowm = true;
            currentEnergy -= activatePowerCost;
            backgroundRenderer.changeColor(slowColor, colorChangeTime);
            energyBars.slowVisualEffect();
            energyBars.speedVisualEffect(false);
            // backgroundRenderer.color = slowColor;
        }
    }

    /*
     * Calls the powers when a key is pressed
     */
    void InputGestion()
    {
        //speed up butten
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isSpeedingUp) disactivatePower();
            else if (currentEnergy < totalPower - activatePowerCost)
                activatePower(true);
        }
        //slow down button
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (isSlowingDowm) disactivatePower();
            else if (currentEnergy > 0 + activatePowerCost)
                activatePower(false);
        }
        //disactivate the powers
        else if (Input.GetKeyDown(KeyCode.W))
        {
            disactivatePower();
        }
    }
}