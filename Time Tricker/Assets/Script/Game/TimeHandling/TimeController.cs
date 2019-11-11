using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script used to choose which button change the time and how
//Should be linked to the player and should have the gestion of energy in the future
public class TimeController : MonoBehaviour
{
    public float SlowValue = 0.5f;
    public float SpeedValue = 2f;

    public float totalPower = 300f;
    public float activatePowerCost = 15f;
    public float costPerSeconds = 10f;

    public EnergyBars energyBars;

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

    void CalculateEnergy()
    {
        float cost = costPerSeconds * Time.deltaTime;
        if (isSlowingDowm) currentEnergy = Mathf.Max(0f, currentEnergy - cost);
        else if (isSpeedingUp) currentEnergy = Mathf.Min(totalPower, currentEnergy + cost);
        if (currentEnergy <= 0f || currentEnergy >= totalPower)
        {
            disactivatePower();
        }
    }
    
    void disactivatePower()
    {
        TimeManager.globalTimeMultiplier = 1f;
        isSpeedingUp = false;
        isSlowingDowm = false;
    }

    void activatePower(bool speedUp)
    {
        if(speedUp)
        {
            TimeManager.globalTimeMultiplier = SpeedValue;
            isSpeedingUp = true;
            isSlowingDowm = false;
            currentEnergy += activatePowerCost;
        }
        else
        {
            TimeManager.globalTimeMultiplier = SlowValue;
            isSpeedingUp = false;
            isSlowingDowm = true;
            currentEnergy -= activatePowerCost;
        }
    }

    void InputGestion()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isSpeedingUp) disactivatePower();
            else if (currentEnergy < totalPower - activatePowerCost)
                activatePower(true);
        }
        //speed up button
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (isSlowingDowm) disactivatePower();
            else if (currentEnergy > 0 + activatePowerCost)
                activatePower(false);
        }
        //clear state
        else if (Input.GetKeyDown(KeyCode.W))
        {
            disactivatePower();
        }
    }
}